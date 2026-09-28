# Version -1.0.0
# Set-ExecutionPolicy RemoteSigned -Scope Process
# .\ClearLogs.ps1

# Configuración de la ruta de los logs
$logPath = "\\suimpappmad021\C$\inetpub\wwwroot\CONTAPRE\Logs"

# Confirmación para proceder con la limpieza de logs
$confirmation = Read-Host -Prompt "Estás a punto de eliminar los archivos de log en el directorio $logPath. ¿Deseas continuar? (S para continuar / N para cancelar)"
if ($confirmation -ne "S") 
{
    Write-Host "Limpieza de logs cancelada por el usuario."
    exit
}

# Limpiar archivos de log en la carpeta especificada
Write-Host "Eliminando archivos de log en $logPath..."
Get-ChildItem -Path $logPath -Recurse | ForEach-Object {
    Remove-Item -Path $_.FullName -Force -ErrorAction SilentlyContinue
}

# Abrir la carpeta de publicación en el Explorador de archivos
Write-Host "Abriendo la carpeta de Logs de la aplicación en el Explorador de archivos..."
Invoke-Item -Path $logPath

Write-Host "Proceso completado. Pulsa Enter para continuar..."
Read-Host
exit
