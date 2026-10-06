<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="SelectDate.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Rectification.SelectDate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="/Public/Css/Contapre.css" />
</head>

<body>

    <form id="form1" runat="server" class="container filter-modal">
        <telerik:RadScriptManager ID="rsmRectifications"
            runat="server"
            EnablePageMethods="True" />
        <telerik:RadSkinManager ID="rskmRectifications"
            runat="server"
            Skin="Metro"
            ShowChooser="false" />
        <telerik:RadWindowManager ID="rwmRectifications" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ralRectifications"
            runat="server"
            Skin="Material"
            Transparency="0"
            Modal="True">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="rapRectifications"
            runat="server"
            LoadingPanelID="ralRectifications">

            <div class="form-group form-group-fake">
                
                <div class="field-container">
                    <span>Fecha:</span>
                    <telerik:RadDatePicker ID="RdDate"
                        RenderMode="Lightweight"
                        runat="server"
                        Width="100%"
                        AutoPostBack="False"
                        Culture="es-ES"
                        EnableTyping="False"
                        MaxDate="12/31/9999"
                        MinDate="01/01/1800" />
                </div>
            </div>

            <div class="form-group buttons">
                <telerik:RadButton ButtonType="LinkButton" ID="btnConfirm"
                    runat="server"
                    RenderMode="Native"
                    OnClientClicked="OnConfirmClientClicked"
                    Text="Imprimir"
                    AutoPostBack="False">
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
                var dateComp = $find("<%= this.RdDate.ClientID %>");
                var dateValue = dateComp.get_selectedDate();

                var iCodes = '<%= this.Session["_iCodes"] %>';
                var eCodes = '<%= this.Session["_eCodes"] %>';

                var oArg = new Object();
                oArg.date = FormatText(dateValue);
                oArg.iCodes = iCodes;
                oArg.eCodes = eCodes;

                CloseWindows(oArg);
            }

            function OnCloseClientClicked(sender, eventArgs) {
                CloseWindows(null);
            }
        </script>
    </telerik:RadScriptBlock>
</body>
</html>
