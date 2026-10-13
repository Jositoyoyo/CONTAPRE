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

La cobertura inicial solicita `GET /Views/Account/Login.aspx` y comprueba que la respuesta sea HTTP 200 y que el HTML incluya «Contabilidad Presupuestaria». No envía credenciales ni modifica datos. Si no se puede conectar, la prueba falla con una indicación para iniciar la aplicación o configurar la URL base.
