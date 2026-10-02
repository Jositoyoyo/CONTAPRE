# Set-ExecutionPolicy RemoteSigned -Scope Process
# .\StartLocal.ps1

$ErrorActionPreference = 'Stop'

function Stop-LocalApplication {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Message
    )

    Write-Error $Message
    exit 1
}

# Locate the solution root from the script directory.
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
    Stop-LocalApplication 'No se encontro Dimatica.ContaPre.sln en la carpeta del script ni en sus directorios padre.'
}

$solutionPath = Join-Path $projectRoot 'Dimatica.ContaPre.sln'
$presentationPath = Join-Path $projectRoot 'Dimatica.ContaPre.Presentation'
$artifactsPath = Join-Path $projectRoot 'artifacts'
$siteOutputPath = Join-Path $artifactsPath 'iis\Debug'
$applicationHostConfig = Join-Path $projectRoot '.vs\Dimatica.ContaPre\config\applicationhost.config'
$localUrl = 'http://localhost:54234/'
$localPort = 54234
$siteName = 'Dimatica.ContaPre.Presentation'

$msbuildCandidates = @(
    'C:\Program Files\Microsoft Visual Studio\18\Insiders\MSBuild\Current\Bin\MSBuild.exe',
    'C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe',
    'C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe',
    'C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe',
    'C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\MSBuild.exe'
)

$iisExpressCandidates = @(
    'C:\Program Files\IIS Express\iisexpress.exe',
    'C:\Program Files (x86)\IIS Express\iisexpress.exe'
)

$msbuildPath = $msbuildCandidates |
    Where-Object { Test-Path -LiteralPath $_ -PathType Leaf } |
    Select-Object -First 1

$iisExpressPath = $iisExpressCandidates |
    Where-Object { Test-Path -LiteralPath $_ -PathType Leaf } |
    Select-Object -First 1

if (-not (Test-Path -LiteralPath $solutionPath -PathType Leaf)) {
    Stop-LocalApplication "La solucion no existe: $solutionPath"
}

if (-not (Test-Path -LiteralPath $presentationPath -PathType Container)) {
    Stop-LocalApplication "El proyecto de Presentation no existe: $presentationPath"
}

if (-not (Test-Path -LiteralPath $applicationHostConfig -PathType Leaf)) {
    Stop-LocalApplication "No se encontro la configuracion de IIS Express: $applicationHostConfig"
}

if (-not $msbuildPath) {
    Stop-LocalApplication 'No se encontro MSBuild.exe en las instalaciones conocidas de Visual Studio.'
}

if (-not $iisExpressPath) {
    Stop-LocalApplication 'No se encontro iisexpress.exe en las rutas conocidas de IIS Express.'
}

try {
    $listeningEndpoints = @(
        [System.Net.NetworkInformation.IPGlobalProperties]::GetIPGlobalProperties().GetActiveTcpListeners() |
            Where-Object { $_.Port -eq $localPort }
    )
}
catch {
    Stop-LocalApplication "No se pudo comprobar el puerto local $localPort`: $($_.Exception.Message)"
}

if ($listeningEndpoints.Count -gt 0) {
    Stop-LocalApplication "El puerto $localPort ya esta en uso. No se detendra el proceso existente."
}

Write-Host "Solucion: $solutionPath"
Write-Host "Proyecto web: $presentationPath"
Write-Host "MSBuild: $msbuildPath"
Write-Host "IIS Express: $iisExpressPath"
Write-Host "Configuracion IIS Express: $applicationHostConfig"
Write-Host "URL local: $localUrl"
Write-Host 'Configuracion de compilacion: Debug'

$msbuildArguments = @(
    $solutionPath,
    '/t:Rebuild',
    '/p:Configuration=Debug',
    '/p:Platform=Any CPU',
    "/p:ContaPreArtifactsRoot=$artifactsPath",
    '/verbosity:minimal'
)

Write-Host 'Compilando la solucion en configuracion Debug...'
try {
    & $msbuildPath @msbuildArguments
    $msbuildExitCode = $LASTEXITCODE
}
catch {
    Stop-LocalApplication "Error al ejecutar MSBuild: $($_.Exception.Message)"
}

if ($msbuildExitCode -ne 0) {
    Stop-LocalApplication "La compilacion fallo con el codigo $msbuildExitCode. No se iniciara IIS Express."
}

$siteAssemblyPath = Join-Path $siteOutputPath 'bin\Dimatica.ContaPre.Presentation.dll'
if (-not (Test-Path -LiteralPath $siteAssemblyPath -PathType Leaf)) {
    Stop-LocalApplication "La compilacion termino, pero no se encontro el ensamblado web preparado: $siteAssemblyPath"
}

try {
    [xml]$applicationHost = Get-Content -LiteralPath $applicationHostConfig
    $siteVirtualDirectory = $applicationHost.configuration.'system.applicationHost'.sites.site |
        Where-Object { $_.name -eq $siteName } |
        Select-Object -First 1

    if (-not $siteVirtualDirectory) {
        Stop-LocalApplication "No se encontro el sitio '$siteName' en applicationhost.config."
    }

    $siteVirtualDirectory.application.virtualDirectory.physicalPath = $siteOutputPath
    $applicationHost.Save($applicationHostConfig)
}
catch {
    Stop-LocalApplication "No se pudo configurar IIS Express para servir desde artifacts: $($_.Exception.Message)"
}

Write-Host "Iniciando IIS Express para $siteName en $localUrl..."
Write-Host 'Pulsa Ctrl+C para detener la aplicacion.'

$iisExpressStartupFailed = $false

try {
    & $iisExpressPath "/config:$applicationHostConfig" "/site:$siteName" 2>&1 |
        ForEach-Object {
            $outputLine = $_.ToString()
            Write-Host $outputLine

            if ($outputLine -match '(?i)Unable to start iisexpress|Access denied|Acceso denegado') {
                $iisExpressStartupFailed = $true
            }
        }

    $iisExpressExitCode = $LASTEXITCODE
}
catch {
    Stop-LocalApplication "Error al iniciar IIS Express: $($_.Exception.Message)"
}

if ($iisExpressStartupFailed) {
    Stop-LocalApplication 'IIS Express no pudo iniciar la aplicacion.'
}

if ($iisExpressExitCode -ne 0) {
    Stop-LocalApplication "IIS Express finalizo con el codigo $iisExpressExitCode."
}
