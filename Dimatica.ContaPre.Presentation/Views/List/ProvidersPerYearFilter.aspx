<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="ProvidersPerYearFilter.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.List.ProvidersPerYearFilter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="/Public/Css/Contapre.css" />
</head>

<body>
    <form id="form1" runat="server" class="container filter-modal">
        <telerik:RadScriptManager ID="rsmComplianceGrade"
            runat="server"
            EnablePageMethods="True" />
        <telerik:RadSkinManager ID="rskmComplianceGrade"
            runat="server"
            Skin="Metro"
            ShowChooser="false" />
        <telerik:RadWindowManager ID="rwmComplianceGrade" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ralComplianceGrade"
            runat="server"
            Skin="Material"
            Transparency="0"
            Modal="True">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="rapComplianceGrade"
            runat="server"
            LoadingPanelID="ralComplianceGrade">

            <div class="form-group form-group-fake">

                <div class="field-container">
                    <span>Año:</span>
                    <telerik:RadComboBox ID="RcYears"
                        runat="server"
                        Width="80px"
                        AutoPostBack="False"
                        DataTextField="Value"
                        DataValueField="Value">
                    </telerik:RadComboBox>
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

            function OnConfirmClientClicked(sender, eventArgs) {
                var yearComp = $find("<%= this.RcYears.ClientID %>");
                var yearValue = yearComp.get_selectedItem().get_value();

                var oArg = new Object();
                oArg.year = yearValue;

                CloseWindows(oArg);
            }

            function OnCloseClientClicked(sender, eventArgs) {
                CloseWindows(null);
            }
        </script>
    </telerik:RadScriptBlock>
</body>
</html>
