<%@ Control 
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="CustomRadAlert.ascx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Shared.Components.Alerts.CustomRadAlert.CustomRadAlert" %>

<link href="<%= ResolveUrl("~/Views/Shared/Components/Alerts/CustomRadAlert/CustomRawAlert.css") %>" rel="stylesheet" type="text/css" />

<div id="customRadAlertOverlay" class="custom-alert-overlay" style="display: none;"></div>

<!-- Ventana alert personalizada -->
<div id="customRadAlertWrapper" class="RadWindow RadWindow_Metro rwNormalWindow rwTransparentWindow">

    <div class="rwTitleRow" id="customRadAlertTitleBar">
        <span id="customRadAlertTitle">Client RadAlert</span>
        <span id="customRadAlertCloseButton" style="float: right; cursor: pointer;" onclick="closeCustomRadAlert()">×</span>
    </div>

    <div class="rwContentRow">
        <div class="rwDialogPopup radalert">
            <div class="rwDialogText" id="customRadAlertMessage"></div>
            <div class="rwDialogButtons">
                <a id="customRadAlertButton" onclick="closeCustomRadAlert()" class="rwPopupButton" href="javascript:void(0);">OK</a>
            </div>
        </div>
    </div>

</div>

<script>

    let customRadAlertCallback = null;

    function openCustomRadAlert(message = "", width = 400, height = 250, title = 'Alert', callback = null, buttonText = 'OK')
    {
        document.getElementById("customRadAlertOverlay").style.display = "block";
        document.getElementById("customRadAlertTitle").innerText       = title;
        document.getElementById("customRadAlertMessage").innerHTML = message;
        document.getElementById("customRadAlertCloseButton").style.display = "initial";


        if (buttonText === '')
        {
            document.getElementById("customRadAlertButton").innerText     = "";
            document.getElementById("customRadAlertButton").style.display = "none";
        }
        else
        {
            document.getElementById("customRadAlertButton").innerText     = buttonText;
            document.getElementById("customRadAlertButton").style.display = "initial";
        }

        const wrapper          = document.getElementById("customRadAlertWrapper");
        wrapper.style.width    = width + "px";
        wrapper.style.height   = height + "px";
        wrapper.style.display  = "block";
        customRadAlertCallback = callback;

    }

    function openCustomRadAlert2(options)
    {

        // Configuración por defecto
        const defaultOptions = {
            title: "Alert",
            message: "",
            width: 400,
            height: 250,
            buttonText: "OK",
            showCustomRadAlertCloseButton: true,
            callback: null
        };

        // Fusionar opciones por defecto con las opciones proporcionadas
        const config = { ...defaultOptions, ...options };

        // Aplicar las configuraciones
        const alertWrapper        = document.getElementById("customRadAlertWrapper");
        alertWrapper.style.width  = config.width + "px";
        alertWrapper.style.height = config.height + "px";

        document.getElementById("customRadAlertOverlay").style.display = "block";
        document.getElementById("customRadAlertTitle").innerText       = config.title;
        document.getElementById("customRadAlertMessage").innerHTML     = config.message;

        if (config.buttonText === '')
        {
            document.getElementById("customRadAlertButton").innerText     = "";
            document.getElementById("customRadAlertButton").style.display = "none";
        }
        else
        {
            document.getElementById("customRadAlertButton").innerText     = config.buttonText;
            document.getElementById("customRadAlertButton").style.display = "initial";
        }

        config.showCustomRadAlertCloseButton === true
            ? document.getElementById("customRadAlertCloseButton").style.display = "initial"
            : document.getElementById("customRadAlertCloseButton").style.display = "none";
                        
        // Mostrar el modal y asignar el callback
        alertWrapper.style.display = "block";
        customRadAlertCallback = config.callback;
    }


    function closeCustomRadAlert()
    {
        document.getElementById("customRadAlertOverlay").style.display = "none";
        document.getElementById("customRadAlertWrapper").style.display = "none";

        if (typeof customRadAlertCallback === "function")
        {
            customRadAlertCallback();
        }

        customRadAlertCallback = null;
    }

</script>
