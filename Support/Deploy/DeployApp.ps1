# Set-ExecutionPolicy RemoteSigned -Scope Process
# .\DeployApp.ps1

$ErrorActionPreference = 'Stop'

function Stop-Deployment {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Message
    )

    Write-Error $Message
    exit 1
}

function Get-YesNoAnswer {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Prompt
    )

    return (Read-Host -Prompt $Prompt).Trim().ToUpperInvariant()
}

function Test-DirectoryAccess {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Path,

        [Parameter(Mandatory = $true)]
        [string]$Description
    )

    try {
        if (-not (Test-Path -LiteralPath $Path -PathType Container)) {
            Stop-Deployment "El directorio $Description no existe o no es accesible: $Path"
        }

        # Fuerza una operación de lectura para detectar permisos insuficientes.
        Get-ChildItem -LiteralPath $Path -Force -ErrorAction Stop | Select-Object -First 1 | Out-Null
    }
    catch {
        Stop-Deployment "No se puede acceder al directorio $Description ($Path): $($_.Exception.Message)"
    }
}

function Invoke-Robocopy {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Source,

        [Parameter(Mandatory = $true)]
        [string]$Destination,

        [Parameter(Mandatory = $true)]
        [string[]]$Options,

        [Parameter(Mandatory = $true)]
        [string]$Operation
    )

    Write-Host "$Operation..."

    try {
        & $robocopyPath $Source $Destination @Options
        $robocopyExitCode = $LASTEXITCODE
    }
    catch {
        Stop-Deployment "Error al ejecutar robocopy durante $Operation`: $($_.Exception.Message)"
    }

    # Robocopy considera satisfactorios los códigos entre 0 y 7.
    if ($robocopyExitCode -ge 8) {
        Stop-Deployment "Robocopy falló durante $Operation con el código $robocopyExitCode."
    }

    Write-Host "$Operation completado. Código robocopy: $robocopyExitCode"
}

# Rutas del proyecto y del despliegue.
$scriptDirectory = $PSScriptRoot
$searchPath      = (Get-Item -LiteralPath $scriptDirectory).FullName
$projectRoot     = $null

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
    Stop-Deployment "No se encontró Dimatica.ContaPre.sln en la carpeta del script ni en sus directorios padre."
}

$solutionPath       = Join-Path $projectRoot 'Dimatica.ContaPre.sln'
$presentationPath   = Join-Path $projectRoot 'Dimatica.ContaPre.Presentation'
$publishProfilePath = Join-Path $presentationPath 'Properties\PublishProfiles\DESARROLLO.pubxml'

$publishPath        = '\\suimpappmad021\CONTAPRE\publicar'
$destinationPath    = '\\suimpappmad021\C$\inetpub\wwwroot\CONTAPRE'
$externalBackupPath = 'C:\CONTAPRE\backups'
$timestamp          = Get-Date -Format 'yyyyMMdd_HHmmss'
$backupPath         = Join-Path $externalBackupPath "Backup_$timestamp"

# Buscar MSBuild en la instalación disponible de Visual Studio.
$msbuildCandidates = @(
    'C:\Program Files\Microsoft Visual Studio\18\Insiders\MSBuild\Current\Bin\MSBuild.exe',
    'C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe',
    'C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe',
    'C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe',
    'C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\MSBuild.exe'
)

$msbuildPath = $msbuildCandidates | Where-Object {
    Test-Path -LiteralPath $_ -PathType Leaf
} | Select-Object -First 1

$robocopyPath = Join-Path $env:SystemRoot 'System32\robocopy.exe'

# Validaciones locales y de configuración.
if (-not (Test-Path -LiteralPath $solutionPath -PathType Leaf)) {
    Stop-Deployment "La solución no existe: $solutionPath"
}

if (-not (Test-Path -LiteralPath $publishProfilePath -PathType Leaf)) {
    Stop-Deployment "El perfil de publicación no existe: $publishProfilePath"
}

if (-not $msbuildPath) {
    Stop-Deployment 'No se encontró MSBuild.exe en las instalaciones conocidas de Visual Studio.'
}

if (-not (Test-Path -LiteralPath $robocopyPath -PathType Leaf)) {
    Stop-Deployment "No se encontró robocopy.exe: $robocopyPath"
}

Test-DirectoryAccess -Path $publishPath -Description 'publicación remota'
Test-DirectoryAccess -Path $destinationPath -Description 'destino remoto de IIS'

if (-not (Test-Path -LiteralPath $externalBackupPath -PathType Container)) {
    try {
        New-Item -ItemType Directory -Path $externalBackupPath -Force | Out-Null
    }
    catch {
        Stop-Deployment "No se pudo crear la carpeta local de backups $externalBackupPath`: $($_.Exception.Message)"
    }
}

Write-Host "Solución: $solutionPath"
Write-Host "Perfil: $publishProfilePath"
Write-Host "Publicación: $publishPath"
Write-Host "Destino IIS: $destinationPath"
Write-Host "MSBuild: $msbuildPath"

$confirmation = Get-YesNoAnswer -Prompt 'Se va a compilar y desplegar la aplicación remota. ¿Deseas continuar? (S/N)'
if ($confirmation -ne 'S') {
    Write-Host 'Despliegue cancelado por el usuario.'
    exit 0
}

# Compilar y publicar directamente en la carpeta compartida.
Write-Host 'Compilando y publicando la aplicación en configuración Development...'
$msbuildArguments = @(
    $solutionPath,
    '/t:Rebuild',
    '/p:Configuration=Development',
    '/p:Platform=Any CPU',
    '/p:DeployOnBuild=true',
    '/p:PublishProfile=DESARROLLO',
    "/p:PublishUrl=$publishPath",
    '/p:WebPublishMethod=FileSystem',
    '/p:DeleteExistingFiles=True',
    '/p:ExcludeApp_Data=True',
    '/verbosity:minimal'
)

try {
    & $msbuildPath @msbuildArguments
    $msbuildExitCode = $LASTEXITCODE
}
catch {
    Stop-Deployment "Error al ejecutar MSBuild`: $($_.Exception.Message)"
}

if ($msbuildExitCode -ne 0) {
    Stop-Deployment "La compilación/publicación falló con el código $msbuildExitCode. No se copiará nada al sitio IIS."
}

$publishedWebConfig = Join-Path $publishPath 'Web.config'
if (-not (Test-Path -LiteralPath $publishedWebConfig -PathType Leaf)) {
    Stop-Deployment "La publicación terminó, pero no se encontró Web.config en $publishPath. No se copiará nada al sitio IIS."
}

Write-Host 'Compilación y publicación completadas correctamente.'

$confirmationBackup = Get-YesNoAnswer -Prompt "¿Deseas realizar una copia de seguridad de $destinationPath en $backupPath? (S/N)"
if ($confirmationBackup -eq 'S') {
    try {
        New-Item -ItemType Directory -Path $backupPath -Force | Out-Null
    }
    catch {
        Stop-Deployment "No se pudo crear la carpeta de backup $backupPath`: $($_.Exception.Message)"
    }

    Invoke-Robocopy `
        -Source $destinationPath `
        -Destination $backupPath `
        -Options @('/E', '/COPY:DAT', '/DCOPY:DAT', '/R:2', '/W:5', '/XJ', '/NP') `
        -Operation 'Crear la copia de seguridad local'
}

$deleteConfirmation = Get-YesNoAnswer -Prompt "Vas a eliminar el contenido actual de $destinationPath, excepto 'aspnet_client' y 'Logs'. ¿Deseas continuar? (S/N)"
if ($deleteConfirmation -eq 'S') {
    Write-Host "Eliminando contenido actual de $destinationPath, excepto 'aspnet_client' y 'Logs'..."

    try {
        Get-ChildItem -LiteralPath $destinationPath -Force -ErrorAction Stop |
            Where-Object { $_.Name -notin @('aspnet_client', 'Logs') } |
            Remove-Item -Recurse -Force -ErrorAction Stop
    }
    catch {
        Stop-Deployment "No se pudo limpiar el destino remoto: $($_.Exception.Message)"
    }
}
else {
    Write-Host 'Limpieza cancelada. Se sobrescribirán los archivos existentes.'
}

Invoke-Robocopy `
    -Source $publishPath `
    -Destination $destinationPath `
    -Options @('/E', '/COPY:DAT', '/DCOPY:DAT', '/R:2', '/W:5', '/XJ', '/NP', '/XD', 'PowerShell') `
    -Operation 'Copiar la publicación al sitio IIS remoto'

$destinationWebConfig = Join-Path $destinationPath 'Web.config'
if (-not (Test-Path -LiteralPath $destinationWebConfig -PathType Leaf)) {
    Stop-Deployment "La copia terminó, pero no se encontró Web.config en $destinationPath."
}

if ($confirmationBackup -eq 'S') {
    $backupDeleteConfirmation = Get-YesNoAnswer -Prompt "La copia de seguridad se creó en $backupPath. ¿Deseas comprimirla y eliminar la carpeta original? (S/N)"

    if ($backupDeleteConfirmation -eq 'S') {
        $zipPath = "$backupPath.zip"

        try {
            Add-Type -AssemblyName System.IO.Compression.FileSystem
            [System.IO.Compression.ZipFile]::CreateFromDirectory($backupPath, $zipPath)
            Remove-Item -LiteralPath $backupPath -Recurse -Force -ErrorAction Stop
            Write-Host "Copia de seguridad comprimida en $zipPath."
        }
        catch {
            Stop-Deployment "No se pudo comprimir la copia de seguridad: $($_.Exception.Message)"
        }
    }
    else {
        Write-Host "Copia de seguridad conservada en $backupPath."
    }
}

Write-Host 'Proceso completado correctamente.'
Read-Host 'Pulsa Enter para finalizar'
exit 0
