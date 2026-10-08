# Version -1.0.0
# Set-ExecutionPolicy RemoteSigned -Scope Process
# .\CheckPlattorm.ps1

$script:checkFailures = @()

function Write-PlatformCheck {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Name,

        [Parameter(Mandatory = $true)]
        [bool]$Passed,

        [Parameter(Mandatory = $true)]
        [string]$Details
    )

    if ($Passed) {
        Write-Host "[OK] $Name - $Details" -ForegroundColor Green
    }
    else {
        Write-Host "[ERROR] $Name - $Details" -ForegroundColor Red
        $script:checkFailures += $Name
    }
}

# Comprobar Windows sin depender de $IsWindows, que no esta disponible en Windows PowerShell 5.1.
$isWindowsPlatform = [System.Environment]::OSVersion.Platform -eq [System.PlatformID]::Win32NT
if (-not $isWindowsPlatform) {
    Write-PlatformCheck -Name 'Windows' -Passed $false -Details 'Este script solo se puede ejecutar en Windows.'
    exit 1
}
Write-PlatformCheck -Name 'Windows' -Passed $true -Details ([System.Environment]::OSVersion.VersionString)

# Comprobar el runtime .NET Framework 4.8 mediante el valor Release documentado por Microsoft.
$dotNetRelease = $null
$dotNetRegistryPath = 'HKLM:\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full'
try {
    $dotNetRegistration = Get-ItemProperty -LiteralPath $dotNetRegistryPath -Name Release -ErrorAction Stop
    $dotNetRelease = [long]$dotNetRegistration.Release
}
catch {
    $dotNetRelease = $null
}

$dotNetRuntimeInstalled = $null -ne $dotNetRelease -and $dotNetRelease -ge 528040
$dotNetRuntimeDetails = if ($dotNetRuntimeInstalled) {
    ".NET Framework 4.8 o posterior detectado (Release $dotNetRelease)."
}
else {
    '.NET Framework 4.8 o posterior no esta instalado.'
}
Write-PlatformCheck -Name '.NET Framework runtime' -Passed $dotNetRuntimeInstalled -Details $dotNetRuntimeDetails

# Comprobar el targeting pack .NET Framework 4.8 necesario para compilar esta solucion legacy.
$programFilesX86 = ${env:ProgramFiles(x86)}
if ([string]::IsNullOrWhiteSpace($programFilesX86)) {
    $programFilesX86 = $env:ProgramFiles
}
$targetingPackPath = Join-Path $programFilesX86 'Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8'
$targetingPackMarker = Join-Path $targetingPackPath 'RedistList\FrameworkList.xml'
$targetingPackInstalled = Test-Path -LiteralPath $targetingPackMarker -PathType Leaf
$targetingPackDetails = if ($targetingPackInstalled) {
    "Targeting pack encontrado en $targetingPackPath."
}
else {
    "No se encontro el targeting pack .NET Framework 4.8 en $targetingPackPath."
}
Write-PlatformCheck -Name '.NET Framework 4.8 Developer Pack' -Passed $targetingPackInstalled -Details $targetingPackDetails

# Comprobar si tiene una configuracion global valida y legible.
$iisConfigurationPath = Join-Path $env:windir 'System32\inetsrv\config\applicationHost.config'
if ([System.Environment]::Is64BitOperatingSystem -and -not [System.Environment]::Is64BitProcess -and -not (Test-Path -LiteralPath $iisConfigurationPath)) {
    $iisConfigurationPath = Join-Path $env:windir 'Sysnative\inetsrv\config\applicationHost.config'
}

$iisConfigurationValid = $false
try {
    if (Test-Path -LiteralPath $iisConfigurationPath -PathType Leaf) {
        $iisConfiguration = New-Object System.Xml.XmlDocument
        $iisConfiguration.XmlResolver = $null
        $iisConfiguration.Load($iisConfigurationPath)
        $iisConfigurationValid = $null -ne $iisConfiguration.SelectSingleNode('/configuration/system.applicationHost')
    }
}
catch {
    $iisConfigurationValid = $false
}

$iisConfigurationDetails = if ($iisConfigurationValid) {
    'La configuracion global de IIS existe y es XML valido.'
}
else {
    "No se encontro una configuracion global de IIS valida en $iisConfigurationPath."
}
Write-PlatformCheck -Name 'Configuracion de IIS' -Passed $iisConfigurationValid -Details $iisConfigurationDetails

if ($iisService -and $iisService.Status -ne 'Running') {
    Write-Warning 'El servicio W3SVC esta detenido. Este verificador no inicia ni modifica servicios.'
}

if ($script:checkFailures.Count -gt 0) {
    Write-Host "Validacion incompleta. Requisitos pendientes: $($script:checkFailures -join ', ')." -ForegroundColor Red
    exit 1
}

Write-Host 'Validacion completada correctamente.' -ForegroundColor Green
exit 0
