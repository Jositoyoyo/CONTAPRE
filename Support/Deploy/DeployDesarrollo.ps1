# Set-ExecutionPolicy RemoteSigned -Scope Process
# .\DeployDesarrollo.ps1

$ErrorActionPreference = 'Stop'

function Stop-Deployment {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Message
    )

    throw $Message
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
$publishProfilePath = Join-Path $presentationPath 'Properties\PublishProfiles\DESARROLLO.pubxml'
$clearLogsPath = Join-Path $scriptDirectory 'ClearLogs.ps1'

# Direct destination. No staging directory or backup is used.
$destinationPath = '\\suimpappmad021\C$\inetpub\wwwroot\CONTAPRE'
$logsPath = Join-Path $destinationPath 'Logs'
$clearLogsDestinationPath = Join-Path $destinationPath 'ClearLogs.ps1'

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

if (-not (Test-Path -LiteralPath $clearLogsPath -PathType Leaf)) {
    Stop-Deployment "No se encontro el unico archivo de Support\\Deploy que se publicara: $clearLogsPath"
}

if (-not $msbuildPath) {
    Stop-Deployment 'No se encontro MSBuild.exe en las instalaciones conocidas de Visual Studio.'
}

if (-not (Test-Path -LiteralPath $destinationPath -PathType Container)) {
    Stop-Deployment "El destino remoto no existe o no es accesible: $destinationPath"
}

Write-Host "Solucion: $solutionPath"
Write-Host "Destino directo: $destinationPath"
Write-Host "MSBuild: $msbuildPath"
Write-Host 'Publicacion de Support\\Deploy: ClearLogs.ps1 solamente'

$confirmation = (Read-Host -Prompt "Se limpiaran los logs de $logsPath y se desplegara la aplicacion. Continuar? (S/N)").Trim().ToUpperInvariant()
if ($confirmation -ne 'S') {
    Write-Host 'Despliegue cancelado por el usuario. No se realizaron cambios remotos.'
    exit 0
}

if (Test-Path -LiteralPath $logsPath -PathType Container) {
    Write-Host "Eliminando archivos de log en $logsPath..."
    Get-ChildItem -LiteralPath $logsPath -File -Recurse -Force |
        Remove-Item -Force -ErrorAction Stop
}
else {
    Write-Host "La carpeta de logs no existe; se continua con el despliegue: $logsPath"
}

# Publish the application directly to IIS. Existing files are preserved because no backup is requested.
$msbuildArguments = @(
    $solutionPath,
    '/t:Rebuild',
    '/p:Configuration=Development',
    '/p:Platform=Any CPU',
    '/p:DeployOnBuild=true',
    '/p:PublishProfile=DESARROLLO',
    "/p:PublishUrl=$destinationPath",
    '/p:WebPublishMethod=FileSystem',
    '/p:DeleteExistingFiles=False',
    '/p:ExcludeApp_Data=True',
    '/p:LaunchSiteAfterPublish=False',
    '/verbosity:minimal'
)

Write-Host 'Compilando y publicando directamente en el destino remoto...'
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

# Support\\Deploy is handled separately because it is outside the web project.
Write-Host 'Publicando unicamente ClearLogs.ps1 desde Support\\Deploy...'
Copy-Item -LiteralPath $clearLogsPath -Destination $clearLogsDestinationPath -Force

$sourceHash = (Get-FileHash -LiteralPath $clearLogsPath -Algorithm SHA256).Hash
$destinationHash = (Get-FileHash -LiteralPath $clearLogsDestinationPath -Algorithm SHA256).Hash

if ($sourceHash -ne $destinationHash) {
    Stop-Deployment "La verificacion de ClearLogs.ps1 fallo: $clearLogsDestinationPath"
}

Write-Host 'Proceso completado correctamente.'
Write-Host "ClearLogs.ps1 publicado y verificado en $clearLogsDestinationPath"
Write-Host "SHA256: $destinationHash"
