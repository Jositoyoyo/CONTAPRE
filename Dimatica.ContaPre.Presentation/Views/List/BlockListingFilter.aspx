<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="BlockListingFilter.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.List.BlockListingFilter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="/Public/Css/Contapre.css" />
</head>

<body onload="AdjustRadWidow(170);">
    <form id="form1" runat="server" class="container filter-modal">
        <telerik:RadScriptManager ID="rsmBlockListing"
            runat="server"
            EnablePageMethods="True" />
        <telerik:RadSkinManager ID="rskmBlockListing"
            runat="server"
            Skin="Metro"
            ShowChooser="false" />
        <telerik:RadWindowManager ID="rwmBlockListing" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ralBlockListing"
            runat="server"
            Skin="Material"
            Transparency="0"
            Modal="True">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="rapBlockListing"
            runat="server"
            LoadingPanelID="ralBlockListing">

            <div class="form-group form-group-fake">

                <div class="field-container">
                    <span>Saldo Banco:</span>
                    <telerik:RadNumericTextBox ID="RntAmount"
                        runat="server"
                        RenderMode="Lightweight"
                        Width="100px"
                        Value="0"
                        EmptyMessage=""
                        ShowSpinButtons="False"
                        AutoPostBack="False"
                        Culture="es-ES">
                    </telerik:RadNumericTextBox>
                </div>

                <div class="field-container">
                    <span>Fecha Registro:</span>
                    <telerik:RadDatePicker ID="RdRegister"
                        RenderMode="Lightweight"
                        runat="server"
                        Width="140px"
                        AutoPostBack="False"
                        Culture="es-ES"
                        EnableTyping="True"
                        MaxDate="12/31/9999"
                        MinDate="01/01/1800">
                        <ClientEvents OnPopupOpening="AdjustRadWidowMax" OnPopupClosing="AdjustRadWidowMin" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="rfvRegister"
                        runat="server"
                        Display="Dynamic"
                        ValidationGroup="BlockListingGroup"
                        ControlToValidate="RdRegister"
                        ErrorMessage=" * "
                        ToolTip="Introduzca la fecha de registro."
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
                    ValidationGroup="BlockListingGroup">
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
                var validated = Page_ClientValidate('BlockListingGroup');
                if (!validated) {
                    return;
                }

                var amountComp = $find("<%= this.RntAmount.ClientID %>");
                var amountValue = amountComp.get_value();

                var registerComp = $find("<%= this.RdRegister.ClientID %>");
                var registerValue = registerComp.get_selectedDate();

                var oArg = new Object();
                oArg.amount = amountValue;
                oArg.register = FormatText(registerValue);

                CloseWindows(oArg);
            }

            function OnCloseClientClicked(sender, eventArgs) {
                CloseWindows(null);
            }
        </script>
    </telerik:RadScriptBlock>
</body>
</html>
