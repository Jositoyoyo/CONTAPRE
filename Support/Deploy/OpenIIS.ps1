# .\OpenIIS.ps1
# Este script abre el Administrador de IIS

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

exit