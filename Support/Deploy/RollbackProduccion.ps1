# Set-ExecutionPolicy RemoteSigned -Scope Process
# .\RollbackProduccion.ps1

$ErrorActionPreference = 'Stop'

function Stop-Rollback {
    param(
        [Parameter(Mandatory = $true)]
        [string]$Message
    )

    throw $Message
}

function Assert-SafeArchiveEntry {
    param(
        [Parameter(Mandatory = $true)]
        [string]$EntryName
    )

    $normalizedEntryName = $EntryName.Replace('/', '\')

    if ([string]::IsNullOrWhiteSpace($normalizedEntryName) -or
        $normalizedEntryName.StartsWith('\') -or
        $normalizedEntryName -match '^[A-Za-z]:') {
        Stop-Rollback "La entrada del ZIP no es una ruta relativa valida: $EntryName"
    }

    $segments = $normalizedEntryName.Split('\')
    if ($segments | Where-Object { $_ -eq '..' }) {
        Stop-Rollback "La entrada del ZIP contiene una ruta no segura: $EntryName"
    }
}

function Get-RelativePath {
    param(
        [Parameter(Mandatory = $true)]
        [string]$RootPath,

        [Parameter(Mandatory = $true)]
        [string]$FilePath
    )

    $root = $RootPath.TrimEnd('\')
    return $FilePath.Substring($root.Length).TrimStart('\').Replace('\', '/')
}

function Assert-RestoredStructure {
    param(
        [Parameter(Mandatory = $true)]
        [string]$SourcePath,

        [Parameter(Mandatory = $true)]
        [string]$DestinationPath
    )

    $sourceFiles = @(Get-ChildItem -LiteralPath $SourcePath -File -Recurse -Force)
    $destinationFiles = @(Get-ChildItem -LiteralPath $DestinationPath -File -Recurse -Force)

    $sourceByRelativePath = @{}
    foreach ($sourceFile in $sourceFiles) {
        $relativePath = Get-RelativePath -RootPath $SourcePath -FilePath $sourceFile.FullName
        $sourceByRelativePath[$relativePath.ToLowerInvariant()] = $sourceFile
    }

    $destinationByRelativePath = @{}
    foreach ($destinationFile in $destinationFiles) {
        $relativePath = Get-RelativePath -RootPath $DestinationPath -FilePath $destinationFile.FullName
        $destinationByRelativePath[$relativePath.ToLowerInvariant()] = $destinationFile
    }

    if ($sourceByRelativePath.Count -ne $destinationByRelativePath.Count) {
        Stop-Rollback "La verificacion fallo: el numero de archivos no coincide ($($sourceByRelativePath.Count) frente a $($destinationByRelativePath.Count))."
    }

    foreach ($relativePath in $sourceByRelativePath.Keys) {
        if (-not $destinationByRelativePath.ContainsKey($relativePath)) {
            Stop-Rollback "La verificacion fallo: falta el archivo $relativePath en el destino."
        }

        if ($sourceByRelativePath[$relativePath].Length -ne $destinationByRelativePath[$relativePath].Length) {
            Stop-Rollback "La verificacion fallo: el tamaño de $relativePath no coincide en el destino."
        }
    }

    $destinationWebConfig = Join-Path $DestinationPath 'Web.config'
    if (-not (Test-Path -LiteralPath $destinationWebConfig -PathType Leaf)) {
        Stop-Rollback "La restauracion termino, pero no se encontro Web.config en $DestinationPath."
    }
}

try {
    Add-Type -AssemblyName System.IO.Compression.FileSystem

    $backupDirectory = '\\suimpappmad041\CONTAPRE\backups'
    $destinationPath = '\\suimpappmad041\C$\inetpub\wwwroot\CONTAPRE'
    $backupName = (Read-Host -Prompt 'Introduce el nombre exacto del backup ZIP (ejemplo: backup_280920261118.zip)').Trim()

    if ([string]::IsNullOrWhiteSpace($backupName)) {
        Stop-Rollback 'No se ha indicado ningun backup.'
    }

    if ([System.IO.Path]::GetFileName($backupName) -cne $backupName -or
        [System.IO.Path]::GetExtension($backupName) -ine '.zip') {
        Stop-Rollback 'El backup debe ser un nombre de archivo simple con extension .zip.'
    }

    if (-not (Test-Path -LiteralPath $backupDirectory -PathType Container)) {
        Stop-Rollback "La carpeta de backups no existe o no es accesible: $backupDirectory"
    }

    $backupPath = Join-Path $backupDirectory $backupName
    if (-not (Test-Path -LiteralPath $backupPath -PathType Leaf)) {
        Stop-Rollback "No se encontro el backup indicado: $backupPath"
    }

    if (-not (Test-Path -LiteralPath $destinationPath -PathType Container)) {
        Stop-Rollback "El destino de produccion no existe o no es accesible: $destinationPath"
    }

    $archive = $null
    $archiveEntryNames = @()
    try {
        $archive = [System.IO.Compression.ZipFile]::OpenRead($backupPath)
        $archiveEntries = @($archive.Entries)

        if ($archiveEntries.Count -eq 0) {
            Stop-Rollback "El backup esta vacio: $backupPath"
        }

        $entryNames = @{}
        foreach ($entry in $archiveEntries) {
            Assert-SafeArchiveEntry -EntryName $entry.FullName

            $normalizedEntryName = $entry.FullName.Replace('/', '\').TrimEnd('\').ToLowerInvariant()
            if ($normalizedEntryName -and $entryNames.ContainsKey($normalizedEntryName)) {
                Stop-Rollback "El backup contiene entradas duplicadas: $($entry.FullName)"
            }

            if ($normalizedEntryName) {
                $entryNames[$normalizedEntryName] = $true
                $archiveEntryNames += $normalizedEntryName
            }
        }

        if (-not ($archiveEntryNames -contains 'web.config')) {
            Stop-Rollback 'El backup no contiene Web.config en la raiz del sitio.'
        }
    }
    finally {
        if ($archive) {
            $archive.Dispose()
        }
    }

    Write-Host "Backup seleccionado: $backupPath"
    Write-Host "Destino de produccion: $destinationPath"
    $confirmation = (Read-Host -Prompt 'Se reemplazara exactamente el contenido del destino. Continuar? (S/N)').Trim().ToUpperInvariant()
    if ($confirmation -ne 'S') {
        Write-Host 'Rollback cancelado por el usuario. No se modifico produccion.'
        exit 0
    }

    $temporaryPath = Join-Path ([System.IO.Path]::GetTempPath()) ("CONTAPRE-Rollback-" + [Guid]::NewGuid().ToString('N'))
    try {
        New-Item -ItemType Directory -Path $temporaryPath -Force | Out-Null
        Write-Host "Extrayendo el backup en una carpeta temporal local..."
        [System.IO.Compression.ZipFile]::ExtractToDirectory($backupPath, $temporaryPath)

        $temporaryWebConfig = Join-Path $temporaryPath 'Web.config'
        if (-not (Test-Path -LiteralPath $temporaryWebConfig -PathType Leaf)) {
            Stop-Rollback 'La extraccion termino, pero no se encontro Web.config en el contenido temporal.'
        }

        $robocopyPath = Join-Path $env:SystemRoot 'System32\robocopy.exe'
        if (-not (Test-Path -LiteralPath $robocopyPath -PathType Leaf)) {
            Stop-Rollback "No se encontro robocopy.exe: $robocopyPath"
        }

        Write-Host 'Reemplazando exactamente el contenido del sitio de produccion...'
        & $robocopyPath $temporaryPath $destinationPath '/MIR' '/COPY:DAT' '/DCOPY:DAT' '/R:2' '/W:2'
        $robocopyExitCode = $LASTEXITCODE

        if ($robocopyExitCode -gt 7) {
            Stop-Rollback "La sincronizacion del rollback fallo con el codigo $robocopyExitCode."
        }

        Assert-RestoredStructure -SourcePath $temporaryPath -DestinationPath $destinationPath
        Write-Host "Rollback completado correctamente con $backupName."
    }
    finally {
        if (Test-Path -LiteralPath $temporaryPath) {
            Remove-Item -LiteralPath $temporaryPath -Recurse -Force -ErrorAction Stop
        }
    }
}
catch {
    Write-Error "Rollback de produccion fallido: $($_.Exception.Message)"
    exit 1
}
