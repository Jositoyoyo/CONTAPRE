# Pruebas de integración con Playwright

Las pruebas de interfaz se ejecutan con Playwright Test y Chromium, de forma independiente del proyecto Web Forms y de las pruebas unitarias MSTest.

## Requisitos

- Node.js 20 o posterior y npm.
- Visual Studio/MSBuild e IIS Express, necesarios para `Support/Utils/StartLocal.ps1`.
- Dependencias NuGet de la solución restauradas.

## Preparación y ejecución

Desde la carpeta `Dimatica.ContaPre.Tests` del repositorio:

```powershell
npm ci
npx playwright install chromium
npm run test:integration
```

Playwright inicia la aplicación mediante `StartLocal.ps1`, que reconstruye la solución en `Debug` y sirve el sitio en `http://localhost:54234/`. Si la aplicación ya está disponible, se reutiliza fuera de CI. Para probar otra instancia, arráncala por separado y establece su URL base:

```powershell
$env:PLAYWRIGHT_BASE_URL = 'http://localhost:54234'
npm run test:integration
```

La prueba inicial recorre las pantallas de inicio de sesión y recuperación de contraseña y vuelve al inicio de sesión. No rellena ni envía credenciales, no actualiza datos y no dispara el envío de correo.

Los informes HTML y resultados de ejecución se guardan localmente en `Dimatica.ContaPre.Tests/playwright-report/` y `Dimatica.ContaPre.Tests/test-results/`; ambos directorios están excluidos de Git.
