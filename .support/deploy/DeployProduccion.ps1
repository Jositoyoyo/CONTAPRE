# Set-ExecutionPolicy RemoteSigned -Scope Process
# .\DeployProduccion.ps1

$ErrorActionPreference = 'Stop'

function Stop-Deployment {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Message
    )

    throw $Message
}

function New-ProductionBackup {
    param(
        [Parameter(Mandatory = $true)]
        [string]$SourcePath,

        [Parameter(Mandatory = $true)]
        [string]$BackupDirectory,

        [Parameter(Mandatory = $true)]
        [string]$BackupZipPath
    )

    try {
        if (-not (Test-Path -LiteralPath $SourcePath -PathType Container)) {
            Stop-Deployment "El sitio IIS de origen no existe o no es accesible: $SourcePath"
        }

        if (-not (Test-Path -LiteralPath $BackupDirectory -PathType Container)) {
            New-Item -ItemType Directory -Path $BackupDirectory -Force | Out-Null
        }

        if (Test-Path -LiteralPath $BackupZipPath -PathType Leaf) {
            Stop-Deployment "Ya existe un backup con el mismo nombre y no se sobrescribira: $BackupZipPath"
        }

        Add-Type -AssemblyName System.IO.Compression.FileSystem
        Write-Host "Creando backup ZIP desde $SourcePath..."
        [System.IO.Compression.ZipFile]::CreateFromDirectory(
            $SourcePath,
            $BackupZipPath,
            [System.IO.Compression.CompressionLevel]::Optimal,
            $false
        )

        $archive = $null
        try {
            $archive = [System.IO.Compression.ZipFile]::OpenRead($BackupZipPath)
            $entryCount = $archive.Entries.Count
        }
        finally {
            if ($archive) {
                $archive.Dispose()
            }
        }

        if ($entryCount -le 0) {
            Stop-Deployment "El backup se creo vacio: $BackupZipPath"
        }

        Write-Host "Backup creado y verificado: $BackupZipPath ($entryCount entradas)"
    }
    catch {
        if (Test-Path -LiteralPath $BackupZipPath -PathType Leaf) {
            Remove-Item -LiteralPath $BackupZipPath -Force -ErrorAction SilentlyContinue
        }

        Stop-Deployment "No se pudo crear o verificar el backup: $($_.Exception.Message)"
    }
}

function Show-RemoteDeploymentInfo {
    param(
        [Parameter(Mandatory = $true)]
        [string]$ComputerName,

        [Parameter(Mandatory = $true)]
        [string]$DestinationPath
    )

    $cimSession = $null
    try {
        $cimSession = New-CimSession -ComputerName $ComputerName -ErrorAction Stop
        $systemDrive = Get-CimInstance -ClassName Win32_LogicalDisk `
            -Filter "DeviceID='C:'" -CimSession $cimSession -ErrorAction Stop

        if (-not $systemDrive -or -not $systemDrive.Size) {
            throw "No se encontro informacion de almacenamiento para C: en $ComputerName."
        }

        $networkAdapters = @(
            Get-CimInstance -ClassName Win32_NetworkAdapterConfiguration `
                -Filter 'IPEnabled=True' -CimSession $cimSession -ErrorAction Stop
        )

        if ($networkAdapters.Count -eq 0) {
            throw "No se encontro configuracion IP activa en $ComputerName."
        }

        $totalGigabytes = [math]::Round($systemDrive.Size / 1GB, 2)
        $freeGigabytes = [math]::Round($systemDrive.FreeSpace / 1GB, 2)
        $freePercentage = [math]::Round(($systemDrive.FreeSpace / $systemDrive.Size) * 100, 1)

        Write-Host ''
        Write-Host "Informacion previa del servidor $ComputerName"
        Write-Host ("Almacenamiento C: {0:N2} GB libres de {1:N2} GB ({2:N1}% libre)" -f `
            $freeGigabytes, $totalGigabytes, $freePercentage)
        Write-Host 'Configuracion IP:'

        foreach ($adapter in $networkAdapters) {
            Write-Host "  Interfaz: $($adapter.Description)"
            Write-Host "    Direcciones IP: $($adapter.IPAddress -join ', ')"
            Write-Host "    Subredes: $($adapter.IPSubnet -join ', ')"
            Write-Host "    Puertas de enlace: $($adapter.DefaultIPGateway -join ', ')"
            Write-Host "    DNS: $($adapter.DNSServerSearchOrder -join ', ')"
        }

        Write-Host "Directorio de despliegue remoto: $DestinationPath"
        Write-Host ''
    }
    catch {
        Stop-Deployment "No se pudo consultar la informacion previa en $ComputerName mediante CIM/WinRM: $($_.Exception.Message)"
    }
    finally {
        if ($cimSession) {
            Remove-CimSession -CimSession $cimSession -ErrorAction SilentlyContinue
        }
    }
}

function Publish-VerifiedSupportFile {
    param(
        [Parameter(Mandatory = $true)]
        [string]$SourcePath,

        [Parameter(Mandatory = $true)]
        [string]$DestinationPath
    )

    $destinationDirectory = Split-Path -Parent $DestinationPath
    if (-not (Test-Path -LiteralPath $destinationDirectory -PathType Container)) {
        New-Item -ItemType Directory -Path $destinationDirectory -Force | Out-Null
    }

    Copy-Item -LiteralPath $SourcePath -Destination $DestinationPath -Force -ErrorAction Stop

    $sourceHash = (Get-FileHash -LiteralPath $SourcePath -Algorithm SHA256 -ErrorAction Stop).Hash
    $destinationHash = (Get-FileHash -LiteralPath $DestinationPath -Algorithm SHA256 -ErrorAction Stop).Hash
    if ($sourceHash -ne $destinationHash) {
        Stop-Deployment "La verificacion SHA-256 fallo para $DestinationPath"
    }
}

function Publish-VerifiedSupportDirectory {
    param(
        [Parameter(Mandatory = $true)]
        [string]$SourcePath,

        [Parameter(Mandatory = $true)]
        [string]$DestinationPath
    )

    $sourceRoot = (Get-Item -LiteralPath $SourcePath -ErrorAction Stop).FullName.TrimEnd('\')
    $sourceDirectories = @(Get-ChildItem -LiteralPath $sourceRoot -Directory -Recurse -Force -ErrorAction Stop)
    $sourceFiles = @(Get-ChildItem -LiteralPath $sourceRoot -File -Recurse -Force -ErrorAction Stop)

    if (-not (Test-Path -LiteralPath $DestinationPath -PathType Container)) {
        New-Item -ItemType Directory -Path $DestinationPath -Force | Out-Null
    }

    foreach ($sourceDirectory in $sourceDirectories) {
        $relativePath = $sourceDirectory.FullName.Substring($sourceRoot.Length).TrimStart('\')
        $targetDirectory = Join-Path $DestinationPath $relativePath
        if (-not (Test-Path -LiteralPath $targetDirectory -PathType Container)) {
            New-Item -ItemType Directory -Path $targetDirectory -Force | Out-Null
        }
    }

    foreach ($sourceFile in $sourceFiles) {
        $relativePath = $sourceFile.FullName.Substring($sourceRoot.Length).TrimStart('\')
        $targetPath = Join-Path $DestinationPath $relativePath
        Publish-VerifiedSupportFile -SourcePath $sourceFile.FullName -DestinationPath $targetPath
    }

    return $sourceFiles.Count
}

# Local project paths.
$scriptDirectory = $PSScriptRoot
$searchPath = (Get-Item -LiteralPath $scriptDirectory).FullName
$projectRoot = $null

while ($searchPath) {
    $candidateSolutionPath = Join-Path $searchPath 'Dimatica.ContaPre.sln'

    if (Test-Path -LiteralPath $candidateSolutionPath -PathType Leaf) {
        $projectRoot = $searchPath
        break
    }

    $parentPath = Split-Path -Parent $searchPath
    if (-not $parentPath -or $parentPath -eq $searchPath) {
        break
    }

    $searchPath = $parentPath
}

if (-not $projectRoot) {
    Stop-Deployment 'No se encontro Dimatica.ContaPre.sln.'
}

$solutionPath = Join-Path $projectRoot 'Dimatica.ContaPre.sln'
$presentationPath = Join-Path $projectRoot 'Dimatica.ContaPre.Presentation'
$artifactsPath = Join-Path $projectRoot 'artifacts'
$versionJsonPath = Join-Path $presentationPath 'JSON.json'
$unitTestProjectPath = Join-Path $projectRoot 'Dimatica.ContaPre.Tests\Dimatica.ContaPre.UnitTest\Dimatica.ContaPre.UnitTest.csproj'
$unitTestAssemblyPath = Join-Path $artifactsPath 'bin\Dimatica.ContaPre.UnitTest\Debug\Dimatica.ContaPre.UnitTest.dll'
$publishProfilePath = Join-Path $presentationPath 'Properties\PublishProfiles\PRODUCCION.pubxml'
$supportUtilsPath = Join-Path $projectRoot '.support\utils'
$supportDeployPath = Join-Path $projectRoot '.support\deploy'
$deploymentHistoryPath = Join-Path $supportDeployPath 'Deployments.md'
$deploymentHistoryHelperPath = Join-Path $supportDeployPath 'DeploymentHistory.ps1'
$supportDeployFileNames = @('RollbackProduccion.ps1', 'OpenIIS.ps1', 'ClearLogs.ps1')

if (-not (Test-Path -LiteralPath $deploymentHistoryHelperPath -PathType Leaf)) {
    Stop-Deployment "No se encontro el helper de historial: $deploymentHistoryHelperPath"
}

. $deploymentHistoryHelperPath

$destinationPath = '\\suimpappmad041\C$\inetpub\wwwroot\CONTAPRE'
$supportUtilsDestinationPath = Join-Path $destinationPath 'Support\Utils'
$supportDeployDestinationPath = Join-Path $destinationPath 'Support\Deploy'
$backupDirectory = '\\suimpappmad041\CONTAPRE\backups'
$backupTimestamp = Get-Date -Format 'ddMMyyyyHHmm'
$backupZipPath = Join-Path $backupDirectory "backup_$backupTimestamp.zip"

# Find MSBuild in the available Visual Studio installation.
$msbuildCandidates = @(
    'C:\Program Files\Microsoft Visual Studio\18\Insiders\MSBuild\Current\Bin\MSBuild.exe',
    'C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe',
    'C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe',
    'C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe',
    'C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\MSBuild.exe'
)

$vstestCandidates = @(
    'C:\Program Files\Microsoft Visual Studio\18\Insiders\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe',
    'C:\Program Files\Microsoft Visual Studio\2022\Enterprise\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe',
    'C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe',
    'C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe',
    'C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe'
)

$msbuildPath = $msbuildCandidates |
    Where-Object { Test-Path -LiteralPath $_ -PathType Leaf } |
    Select-Object -First 1

if (-not (Test-Path -LiteralPath $solutionPath -PathType Leaf)) {
    Stop-Deployment "La solucion no existe: $solutionPath"
}

if (-not (Test-Path -LiteralPath $publishProfilePath -PathType Leaf)) {
    Stop-Deployment "El perfil de publicacion no existe: $publishProfilePath"
}

if (-not (Test-Path -LiteralPath $supportUtilsPath -PathType Container)) {
    Stop-Deployment "La carpeta de utilidades no existe: $supportUtilsPath"
}

foreach ($supportDeployFileName in $supportDeployFileNames) {
    $supportDeployFilePath = Join-Path $supportDeployPath $supportDeployFileName
    if (-not (Test-Path -LiteralPath $supportDeployFilePath -PathType Leaf)) {
        Stop-Deployment "No se encontro el archivo auxiliar requerido: $supportDeployFilePath"
    }
}

if (-not (Test-Path -LiteralPath $versionJsonPath -PathType Leaf)) {
    Stop-Deployment "El fichero de version no existe: $versionJsonPath"
}

if (-not $msbuildPath) {
    Stop-Deployment 'No se encontro MSBuild.exe en las instalaciones conocidas de Visual Studio.'
}

if (-not (Test-Path -LiteralPath $destinationPath -PathType Container)) {
    Stop-Deployment "El destino remoto no existe o no es accesible: $destinationPath"
}

$destinationVersionPath = Join-Path $destinationPath 'JSON.json'
if (-not (Test-Path -LiteralPath $destinationVersionPath -PathType Leaf)) {
    Stop-Deployment "El fichero de version de produccion no existe o no es accesible: $destinationVersionPath"
}

try {
    $versionData = ConvertFrom-Json -InputObject ([System.IO.File]::ReadAllText($destinationVersionPath)) -ErrorAction Stop
}
catch {
    Stop-Deployment "El fichero de version de produccion no contiene un JSON valido: $($_.Exception.Message)"
}

if ($versionData -isnot [PSCustomObject] -or $versionData.version -isnot [string] -or $versionData.version -notmatch '^\d+$') {
    Stop-Deployment 'La propiedad version del JSON.json de produccion debe ser un contador numerico expresado como texto.'
}

if ($versionData.releaseDate -isnot [string]) {
    Stop-Deployment 'La propiedad releaseDate del JSON.json de produccion debe ser una fecha con formato yyyy-MM-dd.'
}

try {
    [System.DateTime]::ParseExact(
        $versionData.releaseDate,
        'yyyy-MM-dd',
        [System.Globalization.CultureInfo]::InvariantCulture,
        [System.Globalization.DateTimeStyles]::None
    ) | Out-Null

    $currentVersionNumber = [long]::Parse(
        $versionData.version,
        [System.Globalization.NumberStyles]::None,
        [System.Globalization.CultureInfo]::InvariantCulture
    )

    if ($currentVersionNumber -eq [long]::MaxValue) {
        Stop-Deployment 'El contador de version ha alcanzado el valor maximo permitido.'
    }

    $nextVersion = ($currentVersionNumber + 1).ToString([System.Globalization.CultureInfo]::InvariantCulture)
}
catch {
    Stop-Deployment "No se pudo validar o incrementar la version de produccion: $($_.Exception.Message)"
}

Show-RemoteDeploymentInfo -ComputerName 'suimpappmad041' -DestinationPath $destinationPath

Write-Host "Solucion: $solutionPath"
Write-Host "Perfil: $publishProfilePath"
Write-Host "Destino directo: $destinationPath"
Write-Host "MSBuild: $msbuildPath"

try {
    Initialize-DeploymentHistory -Path $deploymentHistoryPath
}
catch {
    Stop-Deployment "No se pudo preparar Deployments.md; no se iniciara el despliegue: $($_.Exception.Message)"
}

Invoke-DeploymentWithHistory `
    -Path $deploymentHistoryPath `
    -Environment 'PRODUCCION' `
    -Action {
$testConfirmation = (Read-Host -Prompt 'Deseas ejecutar los tests unitarios antes del despliegue? (S/N)').Trim().ToUpperInvariant()
if ($testConfirmation -eq 'S') {
    if (-not (Test-Path -LiteralPath $unitTestProjectPath -PathType Leaf)) {
        Stop-Deployment "El proyecto de tests unitarios no existe: $unitTestProjectPath"
    }

    $vstestPath = $vstestCandidates |
        Where-Object { Test-Path -LiteralPath $_ -PathType Leaf } |
        Select-Object -First 1

    if (-not $vstestPath) {
        Stop-Deployment 'No se encontro vstest.console.exe en las instalaciones conocidas de Visual Studio.'
    }

    $testBuildArguments = @(
        $unitTestProjectPath,
        '/t:Rebuild',
        '/p:Configuration=Debug',
        '/p:Platform=AnyCPU',
        "/p:ContaPreArtifactsRoot=$artifactsPath",
        '/verbosity:minimal'
    )

    Write-Host 'Reconstruyendo el proyecto de tests unitarios...'
    try {
        & $msbuildPath @testBuildArguments
        $testBuildExitCode = $LASTEXITCODE
    }
    catch {
        Stop-Deployment "Error al reconstruir los tests unitarios: $($_.Exception.Message)"
    }

    if ($testBuildExitCode -ne 0) {
        Stop-Deployment "La reconstruccion de los tests unitarios fallo con el codigo $testBuildExitCode."
    }

    if (-not (Test-Path -LiteralPath $unitTestAssemblyPath -PathType Leaf)) {
        Stop-Deployment "La reconstruccion termino, pero no se encontro el ensamblado de tests: $unitTestAssemblyPath"
    }

    Write-Host 'Ejecutando los tests unitarios...'
    try {
        & $vstestPath $unitTestAssemblyPath
        $testExitCode = $LASTEXITCODE
    }
    catch {
        Stop-Deployment "Error al ejecutar los tests unitarios: $($_.Exception.Message)"
    }

    if ($testExitCode -ne 0) {
        Stop-Deployment "Los tests unitarios fallaron con el codigo $testExitCode. No se continuara con el despliegue."
    }

    Write-Host 'Tests unitarios completados correctamente.'
}
else {
    Write-Host 'Tests unitarios omitidos por el usuario.'
}

$backupConfirmation = (Read-Host -Prompt "Deseas crear un backup de $destinationPath en $backupDirectory? (S/N)").Trim().ToUpperInvariant()
if ($backupConfirmation -eq 'S') {
    New-ProductionBackup `
        -SourcePath $destinationPath `
        -BackupDirectory $backupDirectory `
        -BackupZipPath $backupZipPath
}
else {
    Write-Host 'Backup omitido por el usuario. Se continuara con el despliegue.'
}

# Publish directly to the production IIS site after the optional backup step.
$msbuildArguments = @(
    $solutionPath,
    '/t:Rebuild',
    '/p:Configuration=Release',
    '/p:Platform=Any CPU',
    "/p:ContaPreArtifactsRoot=$artifactsPath",
    '/p:DeployOnBuild=true',
    '/p:PublishProfile=PRODUCCION',
    "/p:PublishUrl=$destinationPath",
    '/p:WebPublishMethod=FileSystem',
    '/p:DeleteExistingFiles=True',
    '/p:ExcludeApp_Data=True',
    '/p:LaunchSiteAfterPublish=False',
    '/verbosity:minimal'
)

$utf8WithoutBom = [System.Text.UTF8Encoding]::new($false)
$deploymentDate = (Get-Date).ToString('yyyy-MM-dd', [System.Globalization.CultureInfo]::InvariantCulture)
$nextVersionJson = ConvertTo-Json -InputObject ([ordered]@{ version = $nextVersion; releaseDate = $deploymentDate }) -Depth 2

Write-Host "Publicando la version $nextVersion con fecha $deploymentDate en produccion..."
try {
    & $msbuildPath @msbuildArguments
    $msbuildExitCode = $LASTEXITCODE
}
catch {
    Stop-Deployment "Error al ejecutar MSBuild: $($_.Exception.Message)"
}

if ($msbuildExitCode -ne 0) {
    Stop-Deployment "La compilacion/publicacion fallo con el codigo $msbuildExitCode."
}

$destinationWebConfig = Join-Path $destinationPath 'Web.config'
if (-not (Test-Path -LiteralPath $destinationWebConfig -PathType Leaf)) {
    Stop-Deployment "La publicacion termino, pero no se encontro Web.config en $destinationPath."
}

if (-not (Test-Path -LiteralPath $destinationVersionPath -PathType Leaf)) {
    Stop-Deployment "La publicacion termino, pero no se encontro JSON.json en $destinationPath."
}

try {
    [System.IO.File]::WriteAllText($destinationVersionPath, $nextVersionJson, $utf8WithoutBom)
}
catch {
    Stop-Deployment "La publicacion termino, pero no se pudo actualizar JSON.json en el destino: $($_.Exception.Message)"
}

try {
    $publishedVersionData = ConvertFrom-Json -InputObject ([System.IO.File]::ReadAllText($destinationVersionPath)) -ErrorAction Stop
}
catch {
    Stop-Deployment "No se pudo verificar JSON.json en el destino: $($_.Exception.Message)"
}

if ($publishedVersionData.version -isnot [string] -or $publishedVersionData.version -cne $nextVersion -or
    $publishedVersionData.releaseDate -isnot [string] -or $publishedVersionData.releaseDate -cne $deploymentDate) {
    Stop-Deployment "La version o la fecha publicada no coinciden con los valores esperados ($nextVersion, $deploymentDate)."
}

Write-Host 'Publicando y verificando Support\Utils...'
$publishedUtilsFileCount = Publish-VerifiedSupportDirectory `
    -SourcePath $supportUtilsPath `
    -DestinationPath $supportUtilsDestinationPath
Write-Host "Support\Utils publicado y verificado ($publishedUtilsFileCount archivos)."

Write-Host 'Publicando y verificando scripts seleccionados de Support\Deploy...'
foreach ($supportDeployFileName in $supportDeployFileNames) {
    $supportDeployFilePath = Join-Path $supportDeployPath $supportDeployFileName
    $supportDeployDestinationFilePath = Join-Path $supportDeployDestinationPath $supportDeployFileName
    Publish-VerifiedSupportFile `
        -SourcePath $supportDeployFilePath `
        -DestinationPath $supportDeployDestinationFilePath
    Write-Host "Publicado y verificado: $supportDeployDestinationFilePath"
}

    }

Write-Host 'Despliegue de produccion completado correctamente.'
