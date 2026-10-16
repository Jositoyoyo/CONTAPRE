# Controles de alertas y modales en Presentation

## `CustomRadAlert`

`Dimatica.ContaPre.Presentation\Views\Shared\Components\Alerts\CustomRadAlert\CustomRadAlert.ascx` es un control de usuario Web Forms que presenta una alerta mediante HTML, JavaScript y estilos propios. La hoja de estilos vinculada es `CustomRawAlert.css` (generada desde `CustomRawAlert.scss`).

Para usarlo en una página `.aspx`, se registra y se inserta una instancia:

```aspx
<%@ Register Src="~/Views/Shared/Components/Alerts/CustomRadAlert/CustomRadAlert.ascx" TagPrefix="uc" TagName="CustomRadAlert" %>
<uc:CustomRadAlert ID="CustomRadAlert" runat="server" />
```

El control publica estas funciones JavaScript globales:

- `openCustomRadAlert(message, width, height, title, callback, buttonText)` abre una alerta con argumentos posicionales.
- `openCustomRadAlert2(options)` abre una alerta pasando opciones como objeto. Admite `title`, `message`, `width`, `height`, `buttonText`, `showCustomRadAlertCloseButton` y `callback`.
- `closeCustomRadAlert()` cierra la alerta y ejecuta el callback, si se proporcionó.

El mensaje se introduce mediante `innerHTML`, por lo que los llamadores deben codificar como HTML cualquier texto procedente de entradas o datos no confiables. La página debe incluir una sola instancia del control, ya que este usa identificadores DOM y nombres de función globales.

### Páginas que lo usan actualmente

- `Views\Maintenance\NewProvider.aspx`
- `Views\Development\AlertConfirmPrompt.aspx`
- `Views\Budget\ManageCreditModification.aspx`

Al añadir o retirar usos, actualizar esta lista y la referencia de `AGENTS.md`.

## `CustomRawWindow`

`Dimatica.ContaPre.Presentation\Views\Shared\Components\Modals\CustomRawWindow\CustomRawWindow.ascx` es un control Web Forms para mostrar un modal propio de confirmación. Usa HTML, JavaScript y estilos de `CustomModalWrapper.css` (generados desde `CustomModalWrapper.scss`). La clase CSS `RadWindow` del contenedor es solo una clase visual: el control no crea ni controla un `RadWindow` Telerik.

Se registra e inserta de la misma forma que otros controles de usuario:

```aspx
<%@ Register Src="~/Views/Shared/Components/Modals/CustomRawWindow/CustomRawWindow.ascx" TagPrefix="uc" TagName="CustomRawWindow" %>
<uc:CustomRawWindow ID="customModal" runat="server" />
```

El control publica estas funciones JavaScript globales:

- `openCustomModal(message, width, height, title, callback, okText, cancelText)` abre el modal mediante argumentos posicionales.
- `openCustomModal2(options)` acepta `title`, `message`, `width`, `height`, `okText`, `cancelText`, `showCustomRadModalcloseButton` y `callback`.
- `closeCustomModal(isConfirmed)` cierra el modal y llama al callback con `true` si se confirmó o `false` si se canceló o cerró con la X.

El mensaje se asigna a `innerHTML`; codificar como HTML cualquier texto procedente de entradas o datos no confiables. Mantener una sola instancia de este control por página porque usa identificadores DOM y nombres de función globales.

### Páginas que lo usan actualmente

- `Views\Maintenance\NewProvider.aspx`
- `Views\Development\AlertConfirmPrompt.aspx`
- `Views\Budget\ManageCreditModification.aspx`

## Alertas de Telerik

Las funciones de Telerik `radalert` y `radconfirm` son distintas de los controles propios `CustomRadAlert` y `CustomRawWindow`. Utilizan componentes cliente de Telerik y crean ventanas gestionadas por sus scripts; los controles propios muestran HTML y JavaScript directamente y no crean un objeto cliente `RadWindow`.

Por ejemplo, `Views\Treasury\ManageTreasury.aspx` usa `radalert` y `radconfirm`, pero actualmente no registra `CustomRadAlert` ni `CustomRawWindow`. Al investigar errores de `RadWindow` o `ResizeExtender`, comprobar cuál de estas implementaciones invoca realmente la página afectada.
