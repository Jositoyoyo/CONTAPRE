<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="CertificateFilter.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Spend.CertificateFilter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="~/Content/Contapre.css" />
</head>

<body>
    <form id="form1" runat="server" class="container filter-modal">
        <telerik:RadScriptManager ID="rsmCertificateFilter"
            runat="server"
            EnablePageMethods="True" />
        <telerik:RadSkinManager ID="rskmCertificateFilter"
            runat="server"
            Skin="Metro"
            ShowChooser="false" />
        <telerik:RadWindowManager ID="rwmCertificateFilter" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ralCertificateFilter"
            runat="server"
            Skin="Material"
            Transparency="0"
            Modal="True">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="rapCertificateFilter"
            runat="server"
            LoadingPanelID="ralCertificateFilter">

            <div class="form-group form-group-fake">

                <div class="field-container">
                    <span>Nombre:</span>
                    <telerik:RadTextBox ID="TxtName"
                        Width="370px"
                        runat="server"
                        MaxLength="50"
                        Text="">
                    </telerik:RadTextBox>
                    <asp:RequiredFieldValidator ID="rfvName"
                        runat="server"
                        Display="Dynamic"
                        ValidationGroup="CertificateGroup"
                        ControlToValidate="TxtName"
                        ErrorMessage=" * "
                        ToolTip="Introduzca el Nombre."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="form-group form-group-fake">

                <div class="field-container">
                    <span>Cargo:</span>
                    <telerik:RadTextBox ID="TxtOcupation"
                        Width="370px"
                        runat="server"
                        MaxLength="50"
                        Text="">
                    </telerik:RadTextBox>
                    <asp:RequiredFieldValidator ID="rfvOcupation"
                        runat="server"
                        Display="Dynamic"
                        ValidationGroup="CertificateGroup"
                        ControlToValidate="TxtOcupation"
                        ErrorMessage=" * "
                        ToolTip="Introduzca el Cargo."
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
                    ValidationGroup="CertificateGroup">
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
                var validated = Page_ClientValidate('CertificateGroup');
                if (!validated) {
                    return;
                }

                var nameComp = $find("<%= this.TxtName.ClientID %>");
                var nameValue = nameComp.get_value();

                var ocupationComp = $find("<%= this.TxtOcupation.ClientID %>");
                var ocupationValue = ocupationComp.get_value();

                var oArg = new Object();
                oArg.name = nameValue;
                oArg.ocupation = ocupationValue;
                oArg.documentId = "<%= this.DocumentId %>";

                CloseWindows(oArg);
            }

            function OnCloseClientClicked(sender, eventArgs) {
                CloseWindows(null);
            }

        </script>
    </telerik:RadScriptBlock>
</body>
</html>
