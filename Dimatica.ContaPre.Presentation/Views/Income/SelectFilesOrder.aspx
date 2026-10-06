<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="SelectFilesOrder.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Income.SelectFilesOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="/Public/Css/Contapre.css" />

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>

    <script src="/Public/Javascript/jquery-3.7.1.js"></script>
    <title></title>
    <style>
        .buttons {
            justify-content: flex-end;
            display: flex;
        }

        form {
            height: 100%;
        }
    </style>
</head>
<body class="container">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="rsmFilesOrder"
            runat="server"
            EnablePageMethods="True" />
        <telerik:RadSkinManager ID="rskmFilesOrder"
            runat="server"
            Skin="Metro"
            ShowChooser="false" />
        <telerik:RadWindowManager ID="rwmFilesOrder" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ralFilesOrder"
            runat="server"
            Skin="Material"
            Transparency="0"
            Modal="True">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="rapFilesOrder"
            runat="server"
            LoadingPanelID="ralFilesOrder">

            <div class="form-group form-group-fake">
                <div class="field-container">
                    <span>Título:</span>
                    <telerik:RadTextBox ID="RtbTitle"
                        Width="100%"
                        runat="server"
                        MaxLength="60"
                        Text="Expedientes de Ingresos">
                    </telerik:RadTextBox>
                    <asp:RequiredFieldValidator ID="rfvTitle"
                        runat="server"
                        Display="Dynamic"
                        ValidationGroup="FilesOrderGroup"
                        ControlToValidate="RtbTitle"
                        ErrorMessage=" * "
                        ToolTip="Introduzca el título del Listado."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group form-group-fake">
                <div class="field-container">
                    <span>Orden:</span>
                    <telerik:RadComboBox ID="RcOrder"
                        runat="server"
                        Width="100%"
                        AutoPostBack="False">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Año + Número" Value="AnoNumero" />
                            <telerik:RadComboBoxItem runat="server" Text="Descripción + Año + Número" Value="DescripcionAnoNumero" />
                            <telerik:RadComboBoxItem runat="server" Text="Tercero + Año + Número" Value="TerceroAnoNumero" />
                            <telerik:RadComboBoxItem runat="server" Text="Aplicación + Año + Número" Value="AplicacionAnoNumero" />
                            <telerik:RadComboBoxItem runat="server" Text="Importe" Value="Importe" />
                        </Items>
                    </telerik:RadComboBox>
                </div>
            </div>

            <div class="form-group buttons">
                <telerik:RadButton ButtonType="LinkButton" ID="btnConfirm"
                    runat="server"
                    RenderMode="Native"
                    OnClientClicked="OnConfirmClientClicked"
                    Text="Imprimir"
                    AutoPostBack="False"
                    ValidationGroup="FilesOrderGroup">
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
                var validated = Page_ClientValidate('FilesOrderGroup');
                if (!validated) {
                    return;
                }

                var titleComp = $find("<%= this.RtbTitle.ClientID %>");
                var titleValue = titleComp.get_value();

                var orderComp = $find("<%= this.RcOrder.ClientID %>");
                var orderValue = orderComp.get_selectedItem().get_value();

                var oArg = new Object();
                oArg.title = titleValue;
                oArg.order = orderValue;

                CloseWindows(oArg);
            }

            function OnCloseClientClicked(sender, eventArgs) {
                CloseWindows(null);
            }

        </script>
    </telerik:RadScriptBlock>
</body>
</html>
