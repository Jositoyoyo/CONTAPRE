<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="RightsRecognizedFilter.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.List.RightsRecognizedFilter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="~/Content/Contapre.css" />
</head>

<body onload="AdjustRadWidow(170);">
    <form id="form1" runat="server" class="container filter-modal">
        <telerik:RadScriptManager ID="rsmRightsRecognized"
            runat="server"
            EnablePageMethods="True" />
        <telerik:RadSkinManager ID="rskmRightsRecognized"
            runat="server"
            Skin="Metro"
            ShowChooser="false" />
        <telerik:RadWindowManager ID="rwmRightsRecognized" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ralRightsRecognized"
            runat="server"
            Skin="Material"
            Transparency="0"
            Modal="True">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="rapRightsRecognized"
            runat="server"
            LoadingPanelID="ralRightsRecognized">

            <div class="form-group form-group-fake">

                <div class="field-container">
                    <span>Fecha Movimiento Hasta:</span>
                    <telerik:RadDatePicker ID="RdOperation"
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
                    <asp:RequiredFieldValidator ID="rfvOperation"
                        runat="server"
                        Display="Dynamic"
                        ValidationGroup="ReportGroup"
                        ControlToValidate="RdOperation"
                        ErrorMessage=" * "
                        ToolTip="Introduzca la fecha de movimiento."
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
                    ValidationGroup="ReportGroup">
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
                var dateComp = $find("<%= this.RdOperation.ClientID %>");
                var dateValue = dateComp.get_selectedDate();

                var oArg = new Object();
                oArg.operationDate = FormatText(dateValue);

                CloseWindows(oArg);
            }

            function OnCloseClientClicked(sender, eventArgs) {
                CloseWindows(null);
            }
        </script>
    </telerik:RadScriptBlock>
</body>
</html>
