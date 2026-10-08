**Tareas Pendientes**

Crear pruebas :

- Unitarias: no acceden a bases de datos ni a IIS. Ya existen en Dimatica.ContaPre.UnitTest; por ejemplo, RecordingDataContext permite comprobar servicios sin conectarse a SQL Server u Oracle. Eso no es una integración real con la base de datos.
- Integración entre capas: ejecutan lógica de negocio y acceso a datos contra una base de pruebas aislada, con datos sembrados por cada prueba y limpieza al terminar.
- Integración web: comprueban la aplicación Web Forms completa a través de IIS Express y el navegador. El script StartLocal.ps1 arranca la aplicación local, aunque no automatiza esas comprobaciones.

Completar tareas :

Terminar :
- La documentacion del proyecto.


Errores :

- http://suimpappmad021.uimp.age/Views/Maintenance/Providers.aspx
- http://suimpappmad021.uimp.age/Views/Maintenance/Providers.aspx


Revisar :
- Si se puede mover el proyecto a D:
- Revisar si hay ficheros de compilacion de despliegue mezclados con ficheros del proyecto