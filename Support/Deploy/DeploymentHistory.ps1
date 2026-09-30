function Initialize-DeploymentHistory {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Path
    )

    $encoding = [System.Text.UTF8Encoding]::new($false)
    if (-not [System.IO.File]::Exists($Path) -or (Get-Item -LiteralPath $Path).Length -eq 0) {
        [System.IO.File]::WriteAllText($Path, "# Historial de despliegues`r`n", $encoding)
    }

    $stream = $null
    try {
        $stream = [System.IO.File]::Open(
            $Path,
            [System.IO.FileMode]::Open,
            [System.IO.FileAccess]::Write,
            [System.IO.FileShare]::ReadWrite
        )
    }
    finally {
        if ($stream) {
            $stream.Dispose()
        }
    }
}

function Add-DeploymentHistoryEntry {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Path,

        [Parameter(Mandatory = $true)]
        [ValidateSet('DESARROLLO', 'PRODUCCION')]
        [string]$Environment,

        [Parameter(Mandatory = $true)]
        [ValidateSet('EXITO', 'FALLO')]
        [string]$Result,

        [Parameter(Mandatory = $true)]
        [string]$Detail
    )

    $safeDetail = [System.Text.RegularExpressions.Regex]::Replace($Detail, '\s+', ' ').Trim()
    $safeDetail = [System.Text.RegularExpressions.Regex]::Replace(
        $safeDetail,
        '(?i)\b(password|pwd|user\s*id|uid|access[_ -]?token|token|secret)\s*=\s*[^;,\s]+',
        '$1=***'
    )

    if ($safeDetail.Length -gt 500) {
        $safeDetail = $safeDetail.Substring(0, 497) + '...'
    }

    $safeDetail = [System.Net.WebUtility]::HtmlEncode($safeDetail)
    $timestamp = Get-Date -Format 'yyyy-MM-dd HH:mm:ss'
    $displayResult = if ($Result -eq 'EXITO') { ([string][char]0x00C9) + 'XITO' } else { $Result }
    $entry = @(
        "## $timestamp",
        '',
        "- **Entorno:** $Environment",
        "- **Resultado:** $displayResult",
        '- **Detalle:**',
        '',
        "  <pre>$safeDetail</pre>",
        ''
    ) -join "`r`n"

    $existingContent = [System.IO.File]::ReadAllText($Path)
    if ($existingContent.Length -gt 0 -and -not $existingContent.EndsWith("`n`n")) {
        if ($existingContent.EndsWith("`n")) {
            $entry = "`r`n$entry"
        }
        else {
            $entry = "`r`n`r`n$entry"
        }
    }

    [System.IO.File]::AppendAllText($Path, $entry, [System.Text.UTF8Encoding]::new($false))
}

function Invoke-DeploymentWithHistory {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Path,

        [Parameter(Mandatory = $true)]
        [ValidateSet('DESARROLLO', 'PRODUCCION')]
        [string]$Environment,

        [Parameter(Mandatory = $true)]
        [scriptblock]$Action
    )

    try {
        & $Action
    }
    catch {
        $deploymentError = $_
        $failureDetail = [string]$deploymentError.Exception.Message
        if ([string]::IsNullOrWhiteSpace($failureDetail)) {
            $failureDetail = 'El despliegue termino con un error.'
        }

        try {
            Add-DeploymentHistoryEntry `
                -Path $Path `
                -Environment $Environment `
                -Result 'FALLO' `
                -Detail $failureDetail
        }
        catch {
            Write-Warning "No se pudo registrar el fallo en Deployments.md: $($_.Exception.Message)"
        }

        throw $deploymentError
    }

    try {
        Add-DeploymentHistoryEntry `
            -Path $Path `
            -Environment $Environment `
            -Result 'EXITO' `
            -Detail 'Despliegue completado correctamente.'
    }
    catch {
        throw "El despliegue de $Environment finalizo correctamente, pero no se pudo registrar en Deployments.md: $($_.Exception.Message)"
    }
}
