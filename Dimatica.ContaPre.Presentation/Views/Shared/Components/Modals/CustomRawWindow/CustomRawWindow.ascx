<%@ Control
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="CustomRawWindow.ascx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Shared.Components.Modals.CustomRawWindow.CustomRawWindow" %>

<link href="<%= ResolveUrl("~/Views/Shared/Components/Modals/CustomRawWindow/CustomModalWrapper.css") %>" rel="stylesheet" type="text/css" />

<div id="customRadModalOverlay" class="custom-window-overlay" style="display: none;"></div>

<div id="customModalWrapper" class="RadWindow" style="display: none;">
    <div class="modal-header">
        <span id="customModalTitle">ATENCIÓN</span>
        <span id="customRadModalcloseButton" class="close-button" onclick="closeCustomModal(false);" title="Close">×</span>
    </div>
    <div class="modal-content">
        <div id="customModalMessage" class="modal-message">
            ¿Está seguro que desea cambiar el estado de este Usuario?
        </div>
        <div class="modal-actions">
            <a class="modal-button ok-button" onclick="closeCustomModal(true);" href="javascript:void(0);">
                <span id="customModalOkText">OK</span>
            </a>
            <a class="modal-button cancel-button" onclick="closeCustomModal(false);" href="javascript:void(0);">
                <span id="customModalCancelText">Cancel</span>
            </a>
        </div>
    </div>
</div>

<script>

    let customModalCallback = null;


    function openCustomModal(message = "¿Está seguro?", width = 400, height = 150, title = 'ATENCIÓN', callback = null, okText = "OK", cancelText = "Cancel")
    {
        document.getElementById("customModalMessage").innerText = message;
        document.getElementById("customRadModalOverlay").style.display = "block";
        document.getElementById("customModalWrapper").style.display = "block";
        document.getElementById("customRadModalcloseButton").style.display = "initial";

        document.getElementById("customModalTitle").innerText      = title;
        document.getElementById("customModalMessage").innerHTML    = message;
        document.getElementById("customModalOkText").innerText     = okText;
        document.getElementById("customModalCancelText").innerText = cancelText;

        const wrapper = document.getElementById("customModalWrapper");
        wrapper.style.width   = width + "px";
        wrapper.style.height  = height + "px";
        wrapper.style.display = "block";
        customModalCallback   = callback;
    }

    function openCustomModal2(options)
    {
        // Configuración por defecto
        const defaultOptions = {
            title: "ATENCIÓN",
            message: "¿Está seguro?",
            width: 400,
            height: 150,
            okText: "OK",
            cancelText: "Cancel",
            showCustomRadModalcloseButton: true,
            callback: null
        };

        // Fusionar opciones por defecto con las opciones proporcionadas
        const config = { ...defaultOptions, ...options };

        // Aplicar las configuraciones
        document.getElementById("customRadModalOverlay").style.display = "block";
        document.getElementById("customModalTitle").innerText = config.title;
        document.getElementById("customModalMessage").innerHTML = config.message;
        document.getElementById("customModalOkText").innerText = config.okText;
        document.getElementById("customModalCancelText").innerText = config.cancelText;

        config.showCustomRadModalcloseButton === true
            ? document.getElementById("customRadModalcloseButton").style.display = "initial"
            : document.getElementById("customRadModalcloseButton").style.display = "none";

        // Cambiar tamaño de la ventana
        const wrapper = document.getElementById("customModalWrapper");
        wrapper.style.width = config.width + "px";
        wrapper.style.height = config.height + "px";

        // Mostrar el modal y asignar el callback
        wrapper.style.display = "block";
        customModalCallback = config.callback;
    }


    function closeCustomModal(isConfirmed)
    {
        document.getElementById("customRadModalOverlay").style.display = "none";
        document.getElementById("customModalWrapper").style.display = "none";
        if (typeof customModalCallback === "function") {
            customModalCallback(isConfirmed);
        }
        customModalCallback = null;
    }

</script>