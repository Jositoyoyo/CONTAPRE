# OpenIISAndFolder.ps1
# Este script abre el Administrador de IIS y la carpeta de publicación de la aplicación

# Configuración de ruta de la carpeta de publicación
$publishFolderPath = "C:\inetpub\wwwroot\CONTAPRE"


# Confirmación2 para proceder con el despliegue
$confirmation2 = Read-Host -Prompt "Se va abrir  el Administrador de IIS ¿Deseas continuar? (S para continuar / N para cancelar)"
if ($confirmation2 -ne "S") {
    Write-Host "Accion cancelada por el usuario."
    exit
}

# Abrir la consola de administración de IIS
Write-Host "Abriendo el Administrador de IIS..."
Start-Process -FilePath "inetmgr"

# Pausa breve para asegurar que IIS se abra primero
Start-Sleep -Seconds 2

# Abrir la carpeta de publicación en el Explorador de archivos
Write-Host "Abriendo la carpeta de publicación de la aplicación en el Explorador de archivos..."
Invoke-Item -Path $publishFolderPath

Write-Host "Proceso completado. Pulsa Enter para continuar..."
Read-Host
exit