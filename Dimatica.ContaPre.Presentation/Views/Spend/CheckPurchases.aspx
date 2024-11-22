<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="CheckPurchases.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Spend.CheckPurchases" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <!-- Page Content -->
    <div id="section_spend" class="container">

        <h3>Facturas Compras</h3>

        <%--BUTTONS--%>
        <div class="top-buttons">
            <div class="a-buttons">
                <div class="find">
                    <span class="icon"></span>
                    <asp:LinkButton ID="btnFind"
                        runat="server"
                        OnClick="btnFind_OnClick"
                        Text="Buscar"
                        ValidationGroup="RecordGroup"></asp:LinkButton>
                </div>
                <div class="report">
                    <span class="icon"></span>
                    <asp:LinkButton ID="btnReport"
                                    runat="server"
                                    OnClientClick="btnReportOnClientClick();return false;"
                                    Text="Listado"></asp:LinkButton>
                </div>
            </div>
        </div>

        <div id="page_spendRecords" class="box-block">

            <telerik:RadAjaxPanel ID="rapSpendRecords" runat="server"
                LoadingPanelID="ralPrincipal">
                <telerik:RadNotification ID="RadNotification"
                    runat="server"
                    RenderMode="Lightweight"
                    VisibleOnPageLoad="False"
                    Position="TopRight"
                    Width="500"
                    Height="100"
                    Animation="Fade"
                    EnableRoundedCorners="True"
                    EnableShadow="False"
                    ContentScrolling="Auto"
                    Opacity="100"
                    ShowSound="None"
                    AutoCloseDelay="0"
                    LoadContentOn="EveryShow"
                    Style="z-index: 100000"
                    OnClientShowing="showModalDiv"
                    OnClientHidden="hideModalDiv">
                </telerik:RadNotification>
                <telerik:RadPanelBar ID="RpbFilter"
                    runat="server"
                    RenderMode="Lightweight"
                    Width="100%"
                    Height="100%">
                    <Items>
                        <telerik:RadPanelItem runat="server"
                            Text="Filtros"
                            Expanded="False">
                            <ContentTemplate>
                                <div class="manage">
                                    <div class="form-group form-group-fake">
                                        <div class="field-container">
                                            <span>Proveedor:</span>
                                            <telerik:RadComboBox ID="RcProviders"
                                                runat="server"
                                                Width="400px"
                                                DataValueField="PROV_CODIGO"
                                                DataTextField="PROV_NOMBRE">
                                            </telerik:RadComboBox>
                                        </div>

                                        <div class="field-container">
                                            <span>Nro Factura:</span>
                                            <telerik:RadTextBox ID="RtbNumber"
                                                runat="server"
                                                Width="150px"
                                                MaxLength="30"
                                                Text="">
                                            </telerik:RadTextBox>
                                        </div>

                                        <div class="field-container">
                                            <span>Importe RO:</span>
                                            <telerik:RadNumericTextBox ID="RntRoAmount"
                                                runat="server"
                                                RenderMode="Lightweight"
                                                Width="150px"
                                                MinValue="0"
                                                ShowSpinButtons="False"
                                                Culture="es-ES">
                                            </telerik:RadNumericTextBox>
                                        </div>

                                        <div class="field-container">
                                            <span>Ejercicio:</span>
                                            <telerik:RadMonthYearPicker ID="RmyExerciseYear"
                                                runat="server"
                                                Width="100px"
                                                AutoPostBack="False"
                                                EnableTyping="True"
                                                Culture="es-ES"
                                                DateInput-Culture-="es-ES"
                                                MonthCellsStyle-CssClass="monthCellClass">
                                                <MonthYearNavigationSettings TodayButtonCaption="Actual"
                                                    OkButtonCaption="Aceptar"
                                                    CancelButtonCaption="Cancelar" />
                                                <DateInput runat="server"
                                                    DateFormat="yyyy"
                                                    DisplayDateFormat="yyyy">
                                                </DateInput>
                                            </telerik:RadMonthYearPicker>
                                        </div>

                                        <div class="field-container">
                                            <span>Fecha Factura:</span>
                                            <telerik:RadDatePicker ID="RdpBillDate"
                                                RenderMode="Lightweight"
                                                runat="server"
                                                Width="120px"
                                                AutoPostBack="False"
                                                Culture="es-ES"
                                                EnableTyping="True"
                                                MaxDate="12/31/9999"
                                                MinDate="01/01/1800" />
                                        </div>

                                        <div class="field-container">
                                            <span>Fecha RO:</span>
                                            <telerik:RadDatePicker ID="RdpRoDate"
                                                RenderMode="Lightweight"
                                                runat="server"
                                                Width="120px"
                                                AutoPostBack="False"
                                                Culture="es-ES"
                                                EnableTyping="True"
                                                MaxDate="12/31/9999"
                                                MinDate="01/01/1800" />
                                        </div>

                                        <div class="field-container">
                                            <span>Exp. Admin.:</span>
                                            <telerik:RadNumericTextBox ID="RntAdministrative"
                                                runat="server"
                                                RenderMode="Lightweight"
                                                Width="150px"
                                                MinValue="0"
                                                ShowSpinButtons="False"
                                                Culture="es-ES"
                                                NumberFormat-DecimalDigits="0">
                                            </telerik:RadNumericTextBox>
                                        </div>

                                        <div class="field-container">
                                            <span>Importe Fra:</span>
                                            <telerik:RadNumericTextBox ID="RntFraAmount"
                                                runat="server"
                                                RenderMode="Lightweight"
                                                Width="150px"
                                                MinValue="0"
                                                ShowSpinButtons="False"
                                                Culture="es-ES">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>
                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgPurchases"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgPurchases_OnNeedDataSource"
                    OnPreRender="RgPurchases_OnPreRender"
                    OnItemCommand="RgPurchases_OnItemCommand"
                    OnDeleteCommand="RgPurchases_OnDeleteCommand"
                    OnItemDataBound="RgPurchases_OnItemDataBound"
                    ClientSettings-ClientEvents-OnCommand="OnCommand"
                    CssClass="checkPurchases-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="FA_CODIGO, BoundLabel, CODFACTURAGEI, EXP_CODIGO, PROV_NOMBRE"
                        ClientDataKeyNames="BoundLabel, FA_NUM_FACTURA, FA_IMPORTE_INTEGRO_LABEL"
                        CommandItemDisplay="Top"
                        AllowPaging="true"
                        PagerStyle-AlwaysVisible="true"
                        PageSize="100"
                        NoMasterRecordsText="No Hay datos a Mostrar."
                        TableLayout="Fixed">

                        <CommandItemSettings ShowRefreshButton="False"
                            ShowExportToExcelButton="False"
                            ShowExportToPdfButton="False"
                            ShowAddNewRecordButton="False" />

                        <PagerStyle Mode="NextPrevAndNumeric"
                            PageSizeLabelText="Elementos por pagina: "
                            PagerTextFormat="Navigate pages {4} Página {0} de {1}, elementos {2} a {3} de {5}" />

                        <Columns>
                            <telerik:GridBoundColumn UniqueName="FA_FIRMA_RO"
                                DataField="FA_FIRMA_RO"
                                HeaderText="Fecha RO"
                                AllowFiltering="true"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="GreaterThanOrEqualTo"
                                ShowFilterIcon="false"
                                DataFormatString="{0:dd/MM/yyyy}"
                                FilterControlWidth="90%">
                                <FilterTemplate>
                                    <telerik:RadDatePicker ID="FilteredDatePickerRoDate"
                                        RenderMode="Lightweight"
                                        runat="server"
                                        Width="100%"
                                        ClientEvents-OnDateSelected="DateSelectedRoDate"
                                        DbSelectedDate='<%#this.SetFilteredDate(Container,"FA_FIRMA_RO") %>'
                                        Culture="es-ES" />
                                    <telerik:RadScriptBlock ID="RadScriptBlockRoDate"
                                        runat="server">
                                        <script id="scriptRoDate"
                                            type="text/javascript">
                                            function DateSelectedRoDate(sender, args) {
                                                var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                var date = FormatSelectedDateRoDate(sender);
                                                tableView.filter("FA_FIRMA_RO", date, "GreaterThanOrEqualTo");
                                            }
                                            function FormatSelectedDateRoDate(picker) {
                                                var date = picker.get_selectedDate();
                                                var dateInput = picker.get_dateInput();
                                                var formattedDate = dateInput.get_dateFormatInfo().FormatDate(date, dateInput.get_displayDateFormat());

                                                return formattedDate;
                                            }
                                        </script>
                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridNumericColumn UniqueName="FA_IMPORTE_RO"
                                DataField="FA_IMPORTE_RO"
                                HeaderText="Importe RO"
                                DataFormatString="{0:N}"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right"
                                FilterControlWidth="100%">
                                <HeaderStyle Width="100px" />
                            </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn UniqueName="PROV_NOMBRE"
                                DataField="PROV_NOMBRE"
                                HeaderText="Proveedor"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="FA_NUM_FACTURA"
                                DataField="FA_NUM_FACTURA"
                                HeaderText="Nro Factura"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="FA_FECHA_FACTURA"
                                DataField="FA_FECHA_FACTURA"
                                HeaderText="Fecha Factura"
                                AllowFiltering="true"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="GreaterThanOrEqualTo"
                                ShowFilterIcon="false"
                                DataFormatString="{0:dd/MM/yyyy}"
                                FilterControlWidth="90%">
                                <FilterTemplate>
                                    <telerik:RadDatePicker ID="FilteredDatePickerBillDate"
                                        RenderMode="Lightweight"
                                        runat="server"
                                        Width="100%"
                                        ClientEvents-OnDateSelected="DateSelectedBillDate"
                                        DbSelectedDate='<%#this.SetFilteredDate(Container,"FA_FECHA_FACTURA") %>'
                                        Culture="es-ES" />
                                    <telerik:RadScriptBlock ID="RadScriptBlockBillDate"
                                        runat="server">
                                        <script id="scriptBillDate"
                                            type="text/javascript">
                                            function DateSelectedBillDate(sender, args) {
                                                var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                var date = FormatSelectedDateBillDate(sender);
                                                tableView.filter("FA_FECHA_FACTURA", date, "GreaterThanOrEqualTo");
                                            }
                                            function FormatSelectedDateBillDate(picker) {
                                                var date = picker.get_selectedDate();
                                                var dateInput = picker.get_dateInput();
                                                var formattedDate = dateInput.get_dateFormatInfo().FormatDate(date, dateInput.get_displayDateFormat());

                                                return formattedDate;
                                            }
                                        </script>
                                    </telerik:RadScriptBlock>
                                </FilterTemplate>
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridNumericColumn UniqueName="FA_IMPORTE_INTEGRO"
                                DataField="FA_IMPORTE_INTEGRO"
                                HeaderText="Importe"
                                DataFormatString="{0:N}"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right"
                                FilterControlWidth="100%">
                                <HeaderStyle Width="100px" />
                            </telerik:GridNumericColumn>
                            <telerik:GridNumericColumn UniqueName="FA_BASE_IMPONIBLE"
                                DataField="FA_BASE_IMPONIBLE"
                                HeaderText="Base Imp."
                                DataFormatString="{0:N}"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right"
                                FilterControlWidth="100%">
                                <HeaderStyle Width="100px" />
                            </telerik:GridNumericColumn>
                            <telerik:GridNumericColumn UniqueName="FA_IMPORTE_IVA"
                                DataField="FA_IMPORTE_IVA"
                                HeaderText="IVA"
                                DataFormatString="{0:N}"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right"
                                FilterControlWidth="100%">
                                <HeaderStyle Width="100px" />
                            </telerik:GridNumericColumn>
                            <telerik:GridNumericColumn UniqueName="FA_IMPORTE_RETENCION"
                                DataField="FA_IMPORTE_RETENCION"
                                HeaderText="Retención"
                                DataFormatString="{0:N}"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right"
                                FilterControlWidth="100%">
                                <HeaderStyle Width="100px" />
                            </telerik:GridNumericColumn>
                            <telerik:GridNumericColumn UniqueName="FA_IMPORTE_BOE"
                                DataField="FA_IMPORTE_BOE"
                                HeaderText="BOE"
                                DataFormatString="{0:N}"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right"
                                FilterControlWidth="100%">
                                <HeaderStyle Width="100px" />
                            </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn UniqueName="BoundLabel"
                                DataField="BoundLabel"
                                HeaderText="Enl."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="50px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="RecordNumber"
                                DataField="RecordNumber"
                                HeaderText="Enx. Contable"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn UniqueName="EditColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Editar"
                                CommandName="UpdateSpendRecord"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-pencil-alt"
                                Text=" "
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>
                            <telerik:GridButtonColumn UniqueName="DeleteColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Eliminar Factura"
                                CommandName="DeleteSpendRecord"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-trash-alt"
                                Text=" "
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>
                            <%-- <telerik:GridButtonColumn UniqueName="DeleteColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Eliminar Factura"
                                CommandName="Delete"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-trash-alt"
                                Text=" " ConfirmDialogType="RadWindow"
                                ConfirmTitle="ATENCIÓN"
                                ConfirmText="¿ Está seguro que desea eliminar esta Factura ?</br></br>AVISO: Si acepta, deberá informar a Compras y Contratos para que</br>vuelvan a trasladarla a Contabilidad cuando sea reparada."
                                ConfirmDialogHeight="100px"
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>--%>
                        </Columns>
                    </MasterTableView>

                    <ClientSettings>
                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                    </ClientSettings>
                </telerik:RadGrid>
            </telerik:RadAjaxPanel>
        </div>
    </div>
    <telerik:RadScriptBlock runat="server">
        <script>

            var modalDiv = null;

            function showModalDiv(sender, args) {
                if (!modalDiv) {
                    modalDiv = document.createElement("div");
                    modalDiv.style.width = "100%";
                    modalDiv.style.height = "100%";
                    modalDiv.style.backgroundColor = "#aaaaaa";
                    modalDiv.style.position = "absolute";
                    modalDiv.style.left = "0px";
                    modalDiv.style.top = "0px";
                    modalDiv.style.filter = "progid:DXImageTransform.Microsoft.Alpha(style=0,opacity=50)";
                    modalDiv.style.opacity = ".5";
                    modalDiv.style.MozOpacity = ".5";
                    modalDiv.setAttribute("unselectable", "on");
                    modalDiv.style.zIndex = (sender.get_zIndex() - 1).toString();
                    document.body.appendChild(modalDiv);
                }
                modalDiv.style.display = "";
            }

            function hideModalDiv() {
                modalDiv.style.display = "none";
            }

            var itemIndex;
            function OnCommand(sender, eventArgs) {
                if (eventArgs.get_commandName() == "DeleteSpendRecord") {
                    var grid = sender;
                    var masterTable = grid.get_masterTableView();
                    itemIndex = eventArgs.get_commandArgument();
                    var row = masterTable.get_dataItems()[itemIndex];
                    var isBound = row.getDataKeyValue("BoundLabel");

                    if (isBound == "Si") {
                        radalert("La factura no se puede eliminar porque está enlazada a un documento contable.", 330, 140, "Imposible eliminar Factura", null, null);
                        return;
                    }

                    var billNumber = row.getDataKeyValue("FA_NUM_FACTURA");
                    var amount = row.getDataKeyValue("FA_IMPORTE_INTEGRO_LABEL");

                    radconfirm("¿ Está seguro que desea eliminar la Factura número " + billNumber + " de importe " + amount + " ?</br></br>AVISO: Si acepta, deberá informar a Compras y Contratos para que vuelvan a trasladarla a Contabilidad cuando sea reparada.",
                        confirmDeleteGridCallBackFn,
                        500,
                        220,
                        null,
                        "Confirmación",
                        null);
                }
            }

            function confirmDeleteGridCallBackFn(arg) {
                if (arg == null || arg == false) {
                    return;
                }

                if (itemIndex) {
                    var masterTable = $find("<%= this.RgPurchases.ClientID %>").get_masterTableView();
                    masterTable.fireCommand("Delete", itemIndex);
                } 
            }

            function btnReportOnClientClick(sender, eventArgs) {
                var providerComp = $find("<%= this.RcProviders.ClientID %>");
                var providerValue = providerComp.get_selectedItem().get_value();

                var billNumberComp = $find("<%= this.RtbNumber.ClientID %>");
                var billNumberValue = billNumberComp.get_value();

                var roAmountComp = $find("<%= this.RntRoAmount.ClientID %>");
                var roAmountValue = roAmountComp.get_value();

                var exerciseYearComp = $find("<%= this.RmyExerciseYear.ClientID %>");
                var exerciseYearDate = exerciseYearComp.get_selectedDate();
                var exerciseYearValue = null;
                if (exerciseYearDate != null) {

                    exerciseYearValue = exerciseYearDate.getFullYear();
                }

                var billDateComp = $find("<%= this.RdpBillDate.ClientID %>");
                var billDateValue = FormatText(billDateComp.get_selectedDate());

                var roDateComp = $find("<%= this.RdpRoDate.ClientID %>");
                var roDateValue = FormatText(roDateComp.get_selectedDate());

                var administrativeIdComp = $find("<%= this.RntAdministrative.ClientID %>");
                var administrativeIdValue = administrativeIdComp.get_value();

                var billAmountComp = $find("<%= this.RntFraAmount.ClientID %>");
                var billAmountValue = billAmountComp.get_value();

                if (providerValue === "-1" && billNumberValue === "" && roAmountValue === "" && exerciseYearValue == null && billDateValue == null && roDateValue == null && administrativeIdValue === "" && billAmountValue === "") {
                    return;
                }

                var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=spendBillPaymentList';

                if (providerValue !== "-1") {
                    url = url + '&provider=' + providerValue;
                }

                if (billNumberValue !== "") {
                    url = url + '&billNumber=' + billNumberValue;
                }

                if (roAmountValue !== "") {
                    url = url + '&roAmount=' + roAmountValue;
                }

                if (exerciseYearValue != null) {
                    url = url + '&exerciseYear=' + exerciseYearValue;
                }

                if (billDateValue != null) {
                    url = url + '&billDate=' + billDateValue;
                }

                if (roDateValue != null) {
                    url = url + '&roDate=' + roDateValue;
                }

                if (administrativeIdValue !== "") {
                    url = url + '&administrativeId=' + administrativeIdValue;
                }

                if (billAmountValue !== "") {
                    url = url + '&billAmount=' + billAmountValue;
                }

                window.open(url, "Reporte");
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
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
