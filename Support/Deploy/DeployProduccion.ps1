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

$msbuildPath = $msbuildCandidates |
    Where-Object { Test-Path -LiteralPath $_ -PathType Leaf } |
    Select-Object -First 1

if (-not (Test-Path -LiteralPath $solutionPath -PathType Leaf)) {
    Stop-Deployment "La solucion no existe: $solutionPath"
}

if (-not (Test-Path -LiteralPath $publishProfilePath -PathType Leaf)) {
    Stop-Deployment "El perfil de publicacion no existe: $publishProfilePath"
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

Write-Host 'Compilando y publicando en produccion...'
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

Write-Host 'Despliegue de produccion completado correctamente.'
