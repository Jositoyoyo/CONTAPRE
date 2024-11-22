# Set-ExecutionPolicy RemoteSigned -Scope Process
# .\deploy.ps1

# Configuración de rutas
$sourcePath         = "C:\CONTAPRE\publicar"
$destinationPath    = "C:\inetpub\wwwroot\CONTAPRE"
$externalBackupPath = "C:\CONTAPRE\backups"
$timestamp          = Get-Date -Format 'yyyyMMdd_HHmmss'
$backupPath         = "$externalBackupPath\Backup_$timestamp"

# Confirmación para proceder con el despliegue
$confirmation = Read-Host -Prompt "Estás a punto de implementar cambios en el directorio de desarrollo. ¿Deseas continuar? (S para continuar / N para cancelar)"
if ($confirmation -ne "S") 
{
    Write-Host "Despliegue cancelado por el usuario."
    exit
}

# Abrir la ventana de administración de IIS
Write-Host "Para asegurar el proceso de despliegue, por favor detenga el servidor..."
Start-Process -FilePath "inetmgr"

Write-Host "Abriendo carpeta de la aplicación--> $destinationPath..."
# Pausa de 3 segundos y abrir directorio de destino en el explorador de archivos
Invoke-Item -Path $destinationPath
Start-Sleep -Seconds 3

# Confirmación2 para proceder con el despliegue
$confirmation2 = Read-Host -Prompt "¿Deseas continuar? (S para continuar / N para cancelar)"
if ($confirmation2 -ne "S") 
{
    Write-Host "Despliegue cancelado por el usuario."
    exit
}

# Crear copia de seguridad en una ruta externa, incluyendo la carpeta DLLs
Write-Host "Creando copia de seguridad en $backupPath..."
New-Item -ItemType Directory -Path $backupPath -Force | Out-Null
Start-Process -NoNewWindow -Wait -FilePath "robocopy" -ArgumentList "$destinationPath", "$backupPath", "/MIR"

# Confirmación antes de eliminar el contenido actual en el destino
$deleteConfirmation = Read-Host -Prompt "Vas a eliminar el contenido actual de $destinationPath, excepto 'aspnet_client' y 'Logs'. ¿Deseas continuar? (S para continuar / N para cancelar)"
if ($deleteConfirmation -eq "S") 
{

    # Borrar carpetas y archivos en el destino excepto 'aspnet_client' y 'Logs'
    Write-Host "Eliminando contenido actual de $destinationPath, excepto 'aspnet_client' y 'Logs'..."
    Get-ChildItem -Path $destinationPath | Where-Object {
        $_.Name -notin 'aspnet_client', 'Logs'
    } | Remove-Item -Recurse -Force -ErrorAction SilentlyContinue
    
} else 
{
    Write-Host "Eliminación cancelada por el usuario."
}

# Copiar archivos y carpetas desde el origen al destino excluyendo la carpeta 'PowerShell'
Write-Host "Copiando archivos de $sourcePath a $destinationPath ..."
Get-ChildItem -Path $sourcePath -Recurse | Where-Object 
{
    $_.FullName -notmatch '\\PowerShell\\' 
} | ForEach-Object {
    $destination = $_.FullName.Replace($sourcePath, $destinationPath)
    if ($_.PSIsContainer) {
        New-Item -ItemType Directory -Path $destination -Force | Out-Null
    } else {
        Copy-Item -Path $_.FullName -Destination $destination -Force -ErrorAction SilentlyContinue
    }
}

# Confirmación para comprimir y eliminar el respaldo temporal
$backupDeleteConfirmation = Read-Host -Prompt "La copia de seguridad ha sido creada en $backupPath. ¿Deseas convertir esta copia de seguridad en un archivo zip y eliminar la carpeta original? (S / N)"

if ($backupDeleteConfirmation -eq "S") 
{
    Write-Host "Creando archivo zip de la copia de seguridad..."
    $zipPath = "$backupPath.zip"
    Add-Type -AssemblyName System.IO.Compression.FileSystem
    [System.IO.Compression.ZipFile]::CreateFromDirectory($backupPath, $zipPath)
    Remove-Item -Path $backupPath -Recurse -Force
    Write-Host "Copia de seguridad comprimida en $zipPath y carpeta temporal eliminada."
} else {
    Write-Host "Copia de seguridad temporal conservada en $backupPath."
}

Write-Host "Abriendo carpeta de backup..."
Invoke-Item -Path $externalBackupPath
Write-Host "Proceso completado. Escribe cualquier letra y pulsa 'Enter' para continuar..."
Read-Host
exit