<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="RightsRecognitionByConventionFilter.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.List.RightsRecognitionByConventionFilter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="~/Content/Contapre.css" />
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
</head>

<body onload="AdjustRadWidow(170);">
    <form id="form1" runat="server" class="container filter-modal">
        <telerik:RadScriptManager ID="rsmRightsRecognition"
            runat="server"
            EnablePageMethods="True" />
        <telerik:RadSkinManager ID="rskmRightsRecognition"
            runat="server"
            Skin="Metro"
            ShowChooser="false" />
        <telerik:RadWindowManager ID="rwmRightsRecognition" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ralRightsRecognition"
            runat="server"
            Skin="Material"
            Transparency="0"
            Modal="True">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="rapRightsRecognition"
            runat="server"
            LoadingPanelID="ralRightsRecognition">

            <div class="form-group form-group-fake">

                <div class="field-container">
                    <span>Fecha Desde:</span>
                    <telerik:RadDatePicker ID="RdSince"
                        RenderMode="Lightweight"
                        runat="server"
                        Width="100%"
                        AutoPostBack="False"
                        Culture="es-ES"
                        EnableTyping="True"
                        MaxDate="12/31/9999"
                        MinDate="01/01/1800"
                        showPopupOnInit="True">
                        <ClientEvents OnPopupOpening="AdjustRadWidowMax" OnPopupClosing="AdjustRadWidowMin" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="rfvSince"
                        runat="server"
                        Display="Dynamic"
                        ValidationGroup="RightsRecognitionGroup"
                        ControlToValidate="RdSince"
                        ErrorMessage=" * "
                        ToolTip="Introduzca la fecha desde."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CvDates"
                        runat="server"
                        Display="Dynamic"
                        EnableClientScript="true"
                        ClientValidationFunction="validateDates"
                        ErrorMessage=" * "
                        ToolTip="La fecha hasta no puede ser menor que desde."
                        ValidationGroup="RightsRecognitionGroup"
                        ForeColor="Red">
                    </asp:CustomValidator>
                </div>

                <div class="field-container">
                    <span>Hasta:</span>
                    <telerik:RadDatePicker ID="RdUntil"
                        RenderMode="Lightweight"
                        runat="server"
                        Width="100%"
                        AutoPostBack="False"
                        Culture="es-ES"
                        EnableTyping="True"
                        MaxDate="12/31/9999"
                        MinDate="01/01/1800">
                        <ClientEvents OnPopupOpening="AdjustRadWidowMax" OnPopupClosing="AdjustRadWidowMin" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="rfvUntil"
                        runat="server"
                        Display="Dynamic"
                        ValidationGroup="RightsRecognitionGroup"
                        ControlToValidate="RdUntil"
                        ErrorMessage=" * "
                        ToolTip="Introduzca la fecha hasta."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </div>

            </div>

            <div class="form-group buttons">
                <telerik:RadButton ButtonType="LinkButton" ID="btnConfirm"
                    runat="server"
                    RenderMode="Native"
                    OnClientClicked="OnConfirmClientClicked"
                    Text="Imprimir"
                    AutoPostBack="False"
                    ValidationGroup="RightsRecognitionGroup">
                </telerik:RadButton>
                <telerik:RadButton ButtonType="LinkButton" ID="btnCancel"
                    runat="server"
                    RenderMode="Native"
                    OnClientClicked="OnCloseClientClicked"
                    Text="Cancelar"
                    AutoPostBack="False">
                </telerik:RadButton>
            </div>

        </telerik:RadAjaxPanel>
    </form>

    <telerik:RadScriptBlock runat="server">
        <script>

            function AdjustRadWidowMax() {
                AdjustRadWidow(310);
            }

            function AdjustRadWidowMin() {
                AdjustRadWidow(170);
            }

            function AdjustRadWidow(size) {
                var oWindow = GetRadWindow();
                setTimeout(function () {
                    //oWindow.autoSize(true);
                    oWindow.set_height(size);
                }, 320);
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) {
                    oWindow = window.radWindow;
                }
                else if (window.frameElement.radWindow) {
                    oWindow = window.frameElement.radWindow;
                }

                return oWindow;
            }

            function CloseWindows(response) {
                var wnd = GetRadWindow();
                if (wnd) {
                    wnd.close(response);
                }
            }

            function FormatText(date) {
                if (date == null) {
                    return null;
                }

                var dd = date.getDate();
                var mm = date.getMonth() + 1;

                var yyyy = date.getFullYear();
                if (dd < 10) {
                    dd = '0' + dd;
                }
                if (mm < 10) {
                    mm = '0' + mm;
                }

                var result = yyyy + '-' + mm + '-' + dd;
                return result;
            }

            function OnConfirmClientClicked(sender, eventArgs) {
                var validated = Page_ClientValidate('RightsRecognitionGroup');
                if (!validated) {
                    return;
                }

                var dateSinceComp = $find("<%= this.RdSince.ClientID %>");
                var dateSinceValue = dateSinceComp.get_selectedDate();

                var dateUntilComp = $find("<%= this.RdUntil.ClientID %>");
                var dateUntilValue = dateUntilComp.get_selectedDate();

                var oArg = new Object();
                oArg.since = FormatText(dateSinceValue);
                oArg.until = FormatText(dateUntilValue);

                CloseWindows(oArg);
            }

            function OnCloseClientClicked(sender, eventArgs) {
                CloseWindows(null);
            }

            function validateDates(sender, args) {
                var sinceComp = $find("<%= this.RdSince.ClientID %>");
                var sinceValue = sinceComp.get_selectedDate();

                var untilComp = $find("<%= this.RdUntil.ClientID %>");
                var untilValue = untilComp.get_selectedDate();

                if (sinceValue != null && untilValue != null) {
                    if (sinceValue >= untilValue) {
                        args.IsValid = false;
                        return false;
                    }
                }

                if (sinceValue == null || untilValue == null) {
                    args.IsValid = false;
                    return false;
                }

                return true;
            }
        </script>
    </telerik:RadScriptBlock>
</body>
</html>
