<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="BoundDocuments.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Treasury.BoundDocuments" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="~/Content/Contapre.css" />

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>

    <script src="../../Scripts/jquery-3.4.1.js"></script>
    <title></title>
    <style>
        .buttons {
            justify-content: flex-end;
            display: flex;
        }

        form {
            width: 100%;
            height: 100%;
        }

        .manage {
            padding: 15px !important;
        }

        .RadCalendarMonthView td[id*='.'] {
            display: none;
        }
    </style>
</head>
<body class="container">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="rsmBoundDocuments"
            runat="server"
            EnablePageMethods="True" />
        <telerik:RadSkinManager ID="rskmBoundDocuments"
            runat="server"
            Skin="Metro"
            ShowChooser="false" />
        <telerik:RadWindowManager ID="rwmBoundDocuments" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ralBoundDocuments"
            runat="server"
            Skin="Material"
            Transparency="0"
            Modal="True">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="rapBoundDocuments"
            runat="server"
            LoadingPanelID="ralBoundDocuments">

            <div class="manage">
                <div class="form-group form-group-fake">
                    <%--Presupuesto--%>
                    <div class="field-container field-15">
                        <span>Presupuesto:</span>
                        <telerik:RadMonthYearPicker ID="RmyYear"
                            runat="server"
                            AutoPostBack="False"
                            EnableTyping="False"
                            Culture="es-ES"
                            DateInput-Culture-="es-ES"
                            MonthCellsStyle-CssClass="monthCellClass"
                            Width="100%">
                            <MonthYearNavigationSettings TodayButtonCaption="Actual"
                                OkButtonCaption="Aceptar"
                                CancelButtonCaption="Cancelar" />
                            <DateInput runat="server"
                                DateFormat="yyyy"
                                DisplayDateFormat="yyyy">
                            </DateInput>
                        </telerik:RadMonthYearPicker>
                    </div>

                    <%--Líquido--%>
                    <div class="field-container field-15">
                        <span>Líquido:</span>
                        <telerik:RadNumericTextBox ID="RntTreasuryAmount"
                            runat="server"
                            RenderMode="Lightweight"
                            MinValue="0"
                            Width="100%"
                            ShowSpinButtons="False"
                            NumberFormat-DecimalDigits="2">
                        </telerik:RadNumericTextBox>
                    </div>

                    <%--Proveedor--%>
                    <div class="field-container field-35">
                        <span>Proveedor:</span>
                        <telerik:RadComboBox ID="RcProviders"
                            runat="server"
                            Width="100%"
                            AutoPostBack="False"
                            DataTextField="PROV_NOMBRE"
                            DataValueField="PROV_CODIGO">
                        </telerik:RadComboBox>
                    </div>

                    <%--N⁰ Talón o transferencia--%>
                    <div class="field-container field-15">
                        <span>N⁰ Talón o Trans.:</span>
                        <telerik:RadTextBox ID="RtbCheckNumber"
                            runat="server"
                            Width="100%"
                            Text=""
                            MaxLength="30">
                        </telerik:RadTextBox>
                    </div>

                    <%--Tipo de Movimiento--%>
                    <div class="field-container field-15">
                        <span>Tipo Mov. Banc.:</span>
                        <telerik:RadComboBox ID="RcOriginCode"
                            runat="server"
                            Width="100%">
                            <Items>
                                <telerik:RadComboBoxItem Text="Ingresos"
                                                         Value="2" />
                                <telerik:RadComboBoxItem Text="Gastos"
                                    Value="1" />
                            </Items>
                        </telerik:RadComboBox>
                    </div>
                </div>
            </div>

            <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgDocuments"
                runat="server"
                Culture="es-ES"
                GroupPanelPosition="Top"
                OnNeedDataSource="RgDocuments_OnNeedDataSource"
                Height="370px">

                <GroupingSettings CaseSensitive="false" />

                <MasterTableView AutoGenerateColumns="False"
                    AllowFilteringByColumn="False"
                    DataKeyNames="DOC_CODIGO, ORIGEN, LIQUIDO, ANO_PRESUPUESTO, DOC_NUMERO_CHEQUE, DOCUMENTO_APLICACION, NUMERO_EXPEDIENTE, PROV_NOMBRE"
                    CommandItemDisplay="Top"
                    AllowSorting="False"
                    AllowPaging="False"
                    PagerStyle-AlwaysVisible="False"
                    NoMasterRecordsText="No Hay datos a Mostrar.">

                    <CommandItemSettings ShowExportToExcelButton="False"
                        ShowAddNewRecordButton="False"
                        ShowRefreshButton="False"
                        ShowExportToPdfButton="False" />

                    <Columns>
                        <telerik:GridBoundColumn UniqueName="ANO_PRESUPUESTO"
                            DataField="ANO_PRESUPUESTO"
                            HeaderText="Año"
                            AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains"
                            ShowFilterIcon="false">
                            <HeaderStyle Width="50px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="NUMERO_EXPEDIENTE"
                            DataField="NUMERO_EXPEDIENTE"
                            HeaderText="Exp."
                            AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains"
                            ShowFilterIcon="false">
                            <HeaderStyle Width="50px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="PROV_NOMBRE"
                            DataField="PROV_NOMBRE"
                            HeaderText="Proveedor"
                            AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains"
                            ShowFilterIcon="false">
                            <HeaderStyle Width="40%" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="DOCUMENTO_APLICACION"
                            DataField="DOCUMENTO_APLICACION"
                            HeaderText="Docu./Aplic."
                            AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains"
                            ShowFilterIcon="false">
                            <HeaderStyle Width="100px" />
                        </telerik:GridBoundColumn>
                       <%--<telerik:GridBoundColumn UniqueName="AmountLabel"
                            DataField="AmountLabel"
                            HeaderText="Líquido"
                            AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains"
                            ShowFilterIcon="false"
                            ItemStyle-CssClass="right">
                            <HeaderStyle Width="100px" />
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridNumericColumn UniqueName="AmountLabel"
                            DataField="LIQUIDO"
                            HeaderText="Líquido"
                            DataFormatString="{0:N}"
                            AutoPostBackOnFilter="true"
                            CurrentFilterFunction="EqualTo"
                            ShowFilterIcon="false"
                            ItemStyle-CssClass="right"
                            FilterControlWidth="100%">
                            <HeaderStyle Width="100px" />
                        </telerik:GridNumericColumn>
                        <telerik:GridBoundColumn UniqueName="DOC_NUMERO_CHEQUE"
                            DataField="DOC_NUMERO_CHEQUE"
                            HeaderText="N⁰ Talón"
                            AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains"
                            ShowFilterIcon="false">
                            <HeaderStyle Width="70px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="Bound"
                            HeaderText="Enl.">
                            <ItemTemplate>
                                <telerik:RadCheckBox ID="checkBound"
                                    runat="server"
                                    Checked="False"
                                    Text=""
                                    AutoPostBack="false">
                                </telerik:RadCheckBox>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="Finish"
                            HeaderText="Term.">
                            <ItemTemplate>
                                <telerik:RadCheckBox ID="checkFinish"
                                    runat="server"
                                    Checked="False"
                                    Text=""
                                    AutoPostBack="false">
                                </telerik:RadCheckBox>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn UniqueName="ORIGEN"
                            DataField="ORIGEN"
                            HeaderText="Origen"
                            AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains"
                            ShowFilterIcon="false">
                            <HeaderStyle Width="70px" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>

                <ClientSettings>
                    <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                    <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                </ClientSettings>
            </telerik:RadGrid>

            <div class="monto-total">
                <strong>Total Tesorería:</strong>
                <telerik:RadTextBox ID="txtTotal"
                    Width="100px"
                    runat="server"
                    MaxLength="80"
                    Text="0,00"
                    Enabled="False">
                </telerik:RadTextBox>
                <strong>Total documentos contables:</strong>
                <telerik:RadTextBox ID="txtTotalDocuments"
                    Width="100px"
                    runat="server"
                    MaxLength="80"
                    Text="0,00"
                    Enabled="False">
                </telerik:RadTextBox>
            </div>

            <div class="form-group buttons">
                <telerik:RadButton ButtonType="LinkButton" ID="btnFind"
                    runat="server"
                    RenderMode="Native"
                    Text="Buscar Documentos"
                    OnClick="btnFind_OnClick"
                    AutoPostBack="True">
                </telerik:RadButton>
                <telerik:RadButton ButtonType="LinkButton" ID="btnSave"
                    runat="server"
                    RenderMode="Native"
                    Text="Grabar"
                    OnClick="btnSave_OnClick"
                    AutoPostBack="True">
                </telerik:RadButton>
                <telerik:RadButton ButtonType="LinkButton" ID="btnClose"
                    runat="server"
                    RenderMode="Native"
                    OnClientClicked="OnCloseClientClicked"
                    Text="Cerrar Ventana"
                    AutoPostBack="False">
                </telerik:RadButton>
            </div>
        </telerik:RadAjaxPanel>
    </form>

    <telerik:RadScriptBlock runat="server">
        <script>
            function OnCloseClientClicked(sender, eventArgs) {
                CloseWindows(null);
            }

            function CloseWindows(response) {
                var wnd = GetRadWindow();
                if (wnd) {
                    wnd.close(response);
                }
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

            function updateStatus(response) {
                switch (response) {
                    case 0:
                        CloseWindows(null);
                        break;
                    case 1:
                        radalert("No se ha asociado el Apunte de Tesorería en cuestión porque no se ha seleccionado ningún documento.", 330, 140, "Imposible enlazar apunte de tesorería", null, null);
                        break;
                }
            }
        </script>
    </telerik:RadScriptBlock>
</body>
</html>
