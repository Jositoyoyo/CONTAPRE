<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="IncomesByPlaceFilter.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.List.IncomesByPlaceFilter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="~/Content/Contapre.css" />
</head>
<body class="container">
    <form id="form1" runat="server" class="container filter-modal">
        <telerik:RadScriptManager ID="rsmIncomesByPlace"
            runat="server"
            EnablePageMethods="True" />
        <telerik:RadSkinManager ID="rskmIncomesByPlace"
            runat="server"
            Skin="Metro"
            ShowChooser="false" />
        <telerik:RadWindowManager ID="rwmIncomesByPlace" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ralIncomesByPlace"
            runat="server"
            Skin="Material"
            Transparency="0"
            Modal="True">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="rapIncomesByPlace"
            runat="server"
            LoadingPanelID="ralIncomesByPlace">

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

                <div class="field-container">
                    <span>Sede:</span>
                    <telerik:RadComboBox ID="RcPlaces"
                        runat="server"
                        Width="140px"
                        AutoPostBack="False"
                        DataTextField="DisplayLabel"
                        DataValueField="CEN_CODIGO">
                    </telerik:RadComboBox>
                </div>
            
            </div>

            <div class="form-group form-group-fake">

                <div class="field-container">
                    <span>Meses entre:</span>
                    <telerik:RadComboBox ID="RcMonthsSince"
                        runat="server"
                        Width="100px"
                        AutoPostBack="False">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Enero" Value="1" />
                            <telerik:RadComboBoxItem runat="server" Text="Febrero" Value="2" />
                            <telerik:RadComboBoxItem runat="server" Text="Marzo" Value="3" />
                            <telerik:RadComboBoxItem runat="server" Text="Abril" Value="4" />
                            <telerik:RadComboBoxItem runat="server" Text="Mayo" Value="5" />
                            <telerik:RadComboBoxItem runat="server" Text="Junio" Value="6" />
                            <telerik:RadComboBoxItem runat="server" Text="Julio" Value="7" />
                            <telerik:RadComboBoxItem runat="server" Text="Agosto" Value="8" />
                            <telerik:RadComboBoxItem runat="server" Text="Septiembre" Value="9" />
                            <telerik:RadComboBoxItem runat="server" Text="Octubre" Value="10" />
                            <telerik:RadComboBoxItem runat="server" Text="Noviembre" Value="11" />
                            <telerik:RadComboBoxItem runat="server" Text="Diciembre" Value="12" />
                        </Items>
                    </telerik:RadComboBox>
                    <asp:CustomValidator ID="CvMonths"
                        runat="server"
                        Display="Dynamic"
                        EnableClientScript="true"
                        ClientValidationFunction="validateMonths"
                        ErrorMessage=" * "
                        ToolTip="El mes entre no puede ser superior al mes y."
                        ValidationGroup="IncomesGroup"
                        ForeColor="Red">
                    </asp:CustomValidator>
                </div>

                <div class="field-container">
                    <span>y:</span>
                    <telerik:RadComboBox ID="RcMonthsUntil"
                        runat="server"
                        Width="100px"
                        AutoPostBack="False">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Enero" Value="1" />
                            <telerik:RadComboBoxItem runat="server" Text="Febrero" Value="2" />
                            <telerik:RadComboBoxItem runat="server" Text="Marzo" Value="3" />
                            <telerik:RadComboBoxItem runat="server" Text="Abril" Value="4" />
                            <telerik:RadComboBoxItem runat="server" Text="Mayo" Value="5" />
                            <telerik:RadComboBoxItem runat="server" Text="Junio" Value="6" />
                            <telerik:RadComboBoxItem runat="server" Text="Julio" Value="7" />
                            <telerik:RadComboBoxItem runat="server" Text="Agosto" Value="8" />
                            <telerik:RadComboBoxItem runat="server" Text="Septiembre" Value="9" />
                            <telerik:RadComboBoxItem runat="server" Text="Octubre" Value="10" />
                            <telerik:RadComboBoxItem runat="server" Text="Noviembre" Value="11" />
                            <telerik:RadComboBoxItem runat="server" Text="Diciembre" Value="12" Selected="True" />
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
                    ValidationGroup="IncomesGroup">
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
                var validated = Page_ClientValidate('IncomesGroup');
                if (!validated) {
                    return;
                }

                var yearComp = $find("<%= this.RcYears.ClientID %>");
                var yearValue = yearComp.get_selectedItem().get_value();

                var placeComp = $find("<%= this.RcPlaces.ClientID %>");
                var placeValue = placeComp.get_selectedItem().get_value();

                var sinceComp = $find("<%= this.RcMonthsSince.ClientID %>");
                var sinceValue = sinceComp.get_selectedItem().get_value();

                var untilComp = $find("<%= this.RcMonthsUntil.ClientID %>");
                var untilValue = untilComp.get_selectedItem().get_value();

                var oArg = new Object();
                oArg.year = yearValue;
                oArg.place = placeValue;
                oArg.since = sinceValue;
                oArg.until = untilValue;

                CloseWindows(oArg);
            }

            function OnCloseClientClicked(sender, eventArgs) {
                CloseWindows(null);
            }

            function validateMonths(sender, args) {
                var sinceComp = $find("<%= this.RcMonthsSince.ClientID %>");
                var sinceValue = sinceComp.get_selectedItem().get_value();

                var untilComp = $find("<%= this.RcMonthsUntil.ClientID %>");
                var untilValue = untilComp.get_selectedItem().get_value();

                if (sinceValue >= untilValue) {
                    args.IsValid = false;
                    return false;
                }

                return true;
            }
        </script>
    </telerik:RadScriptBlock>
</body>
</html>
