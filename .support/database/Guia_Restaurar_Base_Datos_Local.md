# Guía: restaurar un backup de CONTAPRE en SQL Server local

**Fecha de elaboración:** 14/10/2026

Esta guía recoge el procedimiento seguido para restaurar un archivo `.bak` en la instancia SQL Server local levantada con Docker Compose.

## Configuración local

- Servidor: `127.0.0.1`
- Puerto: `14333`
- Contenedor: `contapre-sqlserver-1`
- Base de datos de destino: `CONTAPRE`
- Los archivos del directorio `.support/database` del equipo se montan dentro del contenedor como `/var/backups` en solo lectura. En el asistente de restauración se debe usar la ruta del contenedor, por ejemplo `/var/backups/CONTAPRE-Full-<fecha>.bak`.
- El volumen `contapre-sqlserver-data` conserva los datos de SQL Server aunque se reinicie el contenedor.

La configuración de Compose utiliza variables definidas localmente en `.env`. No copiar sus valores a esta guía ni compartirlos en capturas o mensajes.

## Procedimiento

### 1. Iniciar y esperar a que SQL Server esté listo

Desde la raíz del repositorio, comprobar o iniciar el servicio:

```powershell
docker compose up -d sqlserver
```

Seguir el arranque del motor:

```powershell
docker logs --follow contapre-sqlserver-1
```

En un primer arranque tras una actualización de SQL Server pueden aparecer líneas como `Creating view [dbo].[sysutility_ucp_...]`. Son objetos internos del motor y no indican que se estén instalando las tablas funcionales de CONTAPRE. Esperar hasta que el log muestre:

```text
SQL Server is now ready for client connections
```

No iniciar la restauración mientras SQL Server siga ejecutando esos scripts. Si el log termina con errores de actualización o no llega al mensaje de disponibilidad, revisar el `ERRORLOG` antes de continuar.

### 2. Conectar con permisos de administrador

En el cliente SQL, crear o editar la conexión con estos valores:

- Servidor: `127.0.0.1`
- Puerto: `14333`
- Tipo de autenticación: `SQL Login`
- Usuario: `sa`
- Contraseña: el valor local de `MSSQL_SA_PASSWORD` en `.env`
- Base inicial, si el cliente la solicita: `master`

Usar `sa` para validar y restaurar el backup. El login `contapre_app` está destinado a la aplicación y puede carecer de permisos para operaciones de restauración.

El error `Msg 262, CREATE DATABASE permission denied in database 'master'`, seguido de `Msg 3013`, significa que el login usado no tiene los permisos requeridos. En ese caso, no se ha completado la consulta ni la restauración; conectar con `sa` y volver a intentarlo. No es necesario conceder permisos elevados permanentes a `contapre_app`.

### 3. Comprobar el archivo de backup

Antes de restaurar, se puede comprobar que SQL Server lee el encabezado del backup. Sustituir `<nombre-del-backup>` por el nombre real del archivo:

```sql
RESTORE HEADERONLY
FROM DISK = N'/var/backups/<nombre-del-backup>.bak';
```

La consulta debe devolver información del conjunto de backup. Esta comprobación no restaura ni modifica la base de datos. Ejecutarla con el login administrador; de lo contrario, puede fallar por permisos.

Si SQL Server indica que no encuentra el archivo, confirmar que el `.bak` está dentro de `.support/database` en el equipo y que la ruta consultada empieza por `/var/backups/`.

### 4. Restaurar desde el asistente

1. Abrir la opción de restauración de base de datos.
2. En **Backup Location**, seleccionar **Backup File**.
3. Buscar el archivo del backup en `/var/backups` y seleccionarlo.
4. Elegir `CONTAPRE` como base de destino.
5. Esperar a que termine **Loading restore plan...** y revisar el origen, el destino y las opciones del plan.
6. Iniciar la restauración y esperar el mensaje de finalización correcta.

Si el asistente se queda en **Loading restore plan...**, todavía no ha comenzado a restaurar. Confirmar que SQL Server ya está listo, que la conexión usa `sa` y que el archivo se lee con `RESTORE HEADERONLY`.

Si `CONTAPRE` ya existe, revisar las opciones de reemplazo antes de confirmar. Restaurar sobre una base existente puede sustituir sus datos; hacerlo solo cuando se quiera reemplazar la copia local y no se necesite conservar su contenido actual.

### 5. Verificar la restauración

Con una conexión administradora, comprobar que la base existe y está en línea:

```sql
SELECT name, state_desc
FROM sys.databases
WHERE name = N'CONTAPRE';
```

Después, la aplicación se conecta con `contapre_app` según la configuración local. Reservar el login administrador para tareas de mantenimiento como esta restauración.

## Duración orientativa

Un backup de unos 45 MB suele restaurarse en segundos o pocos minutos en un equipo local, aunque depende del disco, del tamaño de la base restaurada y del trabajo de inicio del motor. Mientras SQL Server muestra actividad de actualización, el asistente puede seguir cargando el plan sin haber iniciado la restauración.

## Seguridad

- No incluir contraseñas de `.env` en documentación, capturas ni mensajes.
- Si una contraseña quedó visible en una captura compartida, cambiarla y eliminarla del perfil guardado del cliente SQL.
- No borrar el volumen `contapre-sqlserver-data` como método para resolver una restauración: contiene los datos persistentes de la instancia.
