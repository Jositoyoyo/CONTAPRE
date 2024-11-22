# Configuración de rutas
$sourcePath = "C:\CONTAPRE\publicar"
$destinationPath = "\\suimpappmad041\CONTAPRE\publicar"

# Solicitar confirmación antes de proceder
$confirmation = Read-Host -Prompt "Estás a punto de copiar archivos a la máquina remota. ¿Deseas continuar? (S para continuar / N para cancelar)"
if ($confirmation -ne "S") 
{
    Write-Host "Copia cancelada por el usuario."
    exit
}

# Eliminar archivos en el destino, excepto Web.config
Write-Host "Eliminando archivos en $destinationPath excepto Web.config..."
try {
    # Eliminar archivos y carpetas, excluyendo Web.config
    Get-ChildItem -Path "$destinationPath" -Recurse | Where-Object {
        $_.Name -ne "Web.config"
    } | Remove-Item -Recurse -Force -ErrorAction SilentlyContinue

    Write-Host "Archivos antiguos eliminados, excepto Web.config."
}
catch {
    Write-Host "Error al eliminar archivos en el destino: $_"
}

# Copiar archivos desde el origen al destino, excluyendo Web.config
Write-Host "Copiando archivos de $sourcePath a $destinationPath..."
try {
    Get-ChildItem -Path $sourcePath -Recurse | Where-Object {
        $_.Name -ne "Web.config" 
    } | ForEach-Object {
        $destination = $_.FullName.Replace($sourcePath, $destinationPath)
        if ($_.PSIsContainer) {
            New-Item -ItemType Directory -Path $destination -Force | Out-Null
        } else {
            Copy-Item -Path $_.FullName -Destination $destination -Force -ErrorAction SilentlyContinue
        }
    }
    Write-Host "Archivos copiados exitosamente a $destinationPath."
}
catch {
    Write-Host "Error al copiar archivos: $_"
}

Write-Host "Proceso completado. Pulsa Enter para continuar..."
Read-Host
exit
