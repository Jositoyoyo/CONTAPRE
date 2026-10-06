<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="SpendsByConceptFilter.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.List.SpendsByConceptFilter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="/Public/Css/Contapre.css" />
</head>

<body onload="AdjustRadWidow(230);">
    <form id="form1" runat="server" class="container filter-modal">
        <telerik:RadScriptManager ID="rsmSpendsByConcept"
            runat="server"
            EnablePageMethods="True" />
        <telerik:RadSkinManager ID="rskmSpendsByConcept"
            runat="server"
            Skin="Metro"
            ShowChooser="false" />
        <telerik:RadWindowManager ID="rwmSpendsByConcept" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ralSpendsByConcept"
            runat="server"
            Skin="Material"
            Transparency="0"
            Modal="True">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="rapSpendsByConcept"
            runat="server"
            LoadingPanelID="ralSpendsByConcept">

            <div class="form-group form-group-fake">

                <div class="field-container">
                    <span>Año Presupuesto:</span>
                    <telerik:RadComboBox ID="RcYears"
                        runat="server"
                        Width="130px"
                        AutoPostBack="True"
                        OnSelectedIndexChanged="RcYears_OnSelectedIndexChanged"
                        DataTextField="Value"
                        DataValueField="Value">
                    </telerik:RadComboBox>
                </div>

                <div class="field-container">
                    <span>Aplicación Gastos:</span>
                    <telerik:RadComboBox ID="RcApplications"
                        runat="server"
                        Width="178px"
                        AutoPostBack="False"
                        DataTextField="Description"
                        DataValueField="CacsCode">
                    </telerik:RadComboBox>
                </div>

            </div>

            <div class="form-group form-group-fake">

                <div class="field-container">
                    <span>Fecha Asiento Desde:</span>
                    <telerik:RadDatePicker ID="RdEffectiveSince"
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
                    <asp:CustomValidator ID="CvDates"
                        runat="server"
                        Display="Dynamic"
                        EnableClientScript="true"
                        ClientValidationFunction="validateDates"
                        ErrorMessage=" * "
                        ToolTip="La fecha hasta no puede ser menor que desde."
                        ValidationGroup="SpendsGroup"
                        ForeColor="Red">
                    </asp:CustomValidator>
                </div>

                <div class="field-container">
                    <span>Hasta:</span>
                    <telerik:RadDatePicker ID="RdEffectiveUntil"
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
                </div>

            </div>

            <div class="form-group buttons">
                <telerik:RadButton ButtonType="LinkButton" ID="btnConfirm"
                    runat="server"
                    RenderMode="Native"
                    OnClientClicked="OnConfirmClientClicked"
                    Text="Imprimir"
                    AutoPostBack="False"
                    ValidationGroup="SpendsGroup">
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
                AdjustRadWidow(370);
            }

            function AdjustRadWidowMin() {
                AdjustRadWidow(230);
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
                var validated = Page_ClientValidate('SpendsGroup');
                if (!validated) {
                    return;
                }

                var yearComp = $find("<%= this.RcYears.ClientID %>");
                var yearValue = yearComp.get_selectedItem().get_value();

                var applicationComp = $find("<%= this.RcApplications.ClientID %>");
                var applicationValue = applicationComp.get_selectedItem().get_value();

                var dateSinceComp = $find("<%= this.RdEffectiveSince.ClientID %>");
                var dateSinceValue = dateSinceComp.get_selectedDate();

                var dateUntilComp = $find("<%= this.RdEffectiveUntil.ClientID %>");
                var dateUntilValue = dateUntilComp.get_selectedDate();

                var oArg = new Object();
                oArg.year = yearValue;
                oArg.application = applicationValue;
                oArg.since = FormatText(dateSinceValue);
                oArg.until = FormatText(dateUntilValue);

                CloseWindows(oArg);
            }

            function OnCloseClientClicked(sender, eventArgs) {
                CloseWindows(null);
            }

            function validateDates(sender, args) {
                var sinceComp = $find("<%= this.RdEffectiveSince.ClientID %>");
                var sinceValue = sinceComp.get_selectedDate();

                var untilComp = $find("<%= this.RdEffectiveUntil.ClientID %>");
                var untilValue = untilComp.get_selectedDate();

                if (sinceValue != null && untilValue != null) {
                    if (sinceValue >= untilValue) {
                        args.IsValid = false;
                        return false;
                    }
                }

                if (sinceValue == null && untilValue == null) {
                    return true;
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
