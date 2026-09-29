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
$versionJsonPath = Join-Path $presentationPath 'JSON.json'
$unitTestProjectPath = Join-Path $projectRoot 'Dimatica.ContaPre.PresentationUnitTest\Dimatica.ContaPre.PresentationUnitTest.csproj'
$unitTestAssemblyPath = Join-Path $projectRoot 'Dimatica.ContaPre.PresentationUnitTest\bin\Debug\Dimatica.ContaPre.PresentationUnitTest.dll'
$publishProfilePath = Join-Path $presentationPath 'Properties\PublishProfiles\PRODUCCION.pubxml'

$destinationPath = '\\suimpappmad041\C$\inetpub\wwwroot\CONTAPRE'
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

if (-not (Test-Path -LiteralPath $versionJsonPath -PathType Leaf)) {
    Stop-Deployment "El fichero de version no existe: $versionJsonPath"
}

try {
    $previousVersionJson = [System.IO.File]::ReadAllText($versionJsonPath)
    $versionData = ConvertFrom-Json -InputObject $previousVersionJson -ErrorAction Stop
}
catch {
    Stop-Deployment "El fichero de version no contiene un JSON valido: $($_.Exception.Message)"
}

if ($versionData -isnot [PSCustomObject] -or $versionData.version -isnot [string] -or $versionData.version -notmatch '^\d+$') {
    Stop-Deployment 'La propiedad version de JSON.json debe ser un contador numerico expresado como texto.'
}

if ($versionData.releaseDate -isnot [string]) {
    Stop-Deployment 'La propiedad releaseDate de JSON.json debe ser una fecha con formato yyyy-MM-dd.'
}

try {
    [System.DateTime]::ParseExact(
        $versionData.releaseDate,
        'yyyy-MM-dd',
        [System.Globalization.CultureInfo]::InvariantCulture,
        [System.Globalization.DateTimeStyles]::None
    ) | Out-Null
}
catch {
    Stop-Deployment 'La propiedad releaseDate de JSON.json no contiene una fecha valida con formato yyyy-MM-dd.'
}

try {
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
    Stop-Deployment "No se pudo calcular la siguiente version: $($_.Exception.Message)"
}

if (-not $msbuildPath) {
    Stop-Deployment 'No se encontro MSBuild.exe en las instalaciones conocidas de Visual Studio.'
}

if (-not (Test-Path -LiteralPath $destinationPath -PathType Container)) {
    Stop-Deployment "El destino remoto no existe o no es accesible: $destinationPath"
}

Write-Host "Solucion: $solutionPath"
Write-Host "Perfil: $publishProfilePath"
Write-Host "Destino directo: $destinationPath"
Write-Host "MSBuild: $msbuildPath"

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
    '/p:DeployOnBuild=true',
    '/p:PublishProfile=PRODUCCION',
    "/p:PublishUrl=$destinationPath",
    '/p:WebPublishMethod=FileSystem',
    '/p:DeleteExistingFiles=True',
    '/p:ExcludeApp_Data=True',
    '/p:LaunchSiteAfterPublish=False',
    '/verbosity:minimal'
)

$versionWasPrepared = $false
$deploymentVerified = $false
$utf8WithoutBom = [System.Text.UTF8Encoding]::new($false)
$deploymentDate = (Get-Date).ToString('yyyy-MM-dd', [System.Globalization.CultureInfo]::InvariantCulture)
$nextVersionJson = ConvertTo-Json -InputObject ([ordered]@{ version = $nextVersion; releaseDate = $deploymentDate }) -Depth 2

try {
    $versionWasPrepared = $true
    [System.IO.File]::WriteAllText($versionJsonPath, $nextVersionJson, $utf8WithoutBom)

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

    $destinationVersionPath = Join-Path $destinationPath 'JSON.json'
    if (-not (Test-Path -LiteralPath $destinationVersionPath -PathType Leaf)) {
        Stop-Deployment "La publicacion termino, pero no se encontro JSON.json en $destinationPath."
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

    $deploymentVerified = $true
}
finally {
    if ($versionWasPrepared -and -not $deploymentVerified) {
        try {
            [System.IO.File]::WriteAllText($versionJsonPath, $previousVersionJson, $utf8WithoutBom)
            Write-Host 'Se restauro el fichero local a la version anterior tras el fallo del despliegue.'
        }
        catch {
            Stop-Deployment "El despliegue fallo y no se pudo restaurar la version local: $($_.Exception.Message)"
        }
    }
}

Write-Host 'Despliegue de produccion completado correctamente.'
