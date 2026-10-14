# Pruebas HTTP funcionales con MSTest

El proyecto `Dimatica.ContaPre.HttpTest` comprueba la respuesta HTTP de la aplicación Web Forms usando `HttpClient`. No automatiza un navegador ni ejecuta JavaScript.

## Preparación y ejecución

La aplicación debe estar levantada antes de ejecutar estas pruebas. Para usar la instancia local, inicia ContaPre con `.support/utils/StartLocal.ps1` y, en otra consola, compila y ejecuta el proyecto:

```powershell
& '...\MSBuild.exe' `
    'Dimatica.ContaPre.Tests\Dimatica.ContaPre.HttpTest\Dimatica.ContaPre.HttpTest.csproj' `
    /t:Build /p:Configuration=Debug /p:Platform=AnyCPU

& '...\vstest.console.exe' `
    'artifacts\bin\Dimatica.ContaPre.HttpTest\Debug\Dimatica.ContaPre.HttpTest.dll'
```

Por defecto, las pruebas usan `http://localhost:54234`. Para apuntar a otra instancia, establece `CONTAPRE_BASE_URL` antes de ejecutarlas:

```powershell
$env:CONTAPRE_BASE_URL = 'http://localhost:54234'
```

La cobertura HTTP solicita las páginas públicas de inicio de sesión y recuperación de contraseña para comprobar el estado HTTP, el contenido funcional y los enlaces de navegación. También solicita `GET /Default.aspx` y verifica que la aplicación redirija a la página de inicio de sesión. El cliente no sigue automáticamente las redirecciones para poder comprobar el estado HTTP 302 y su destino.

Las pruebas solo realizan solicitudes GET: no envían credenciales, no ejecutan la recuperación de contraseña ni modifican datos. No prueban JavaScript ni la interacción visual en un navegador. No las ejecutes contra producción. Si no se puede conectar, la prueba falla con una indicación para iniciar la aplicación o configurar la URL base.
