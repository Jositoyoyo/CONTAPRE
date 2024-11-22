<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="GroupDr.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Income.GroupDr" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <!-- Page Content -->
    <div id="section_income" class="container">

        <h3>Agrupar Dr</h3>

        <%--BUTTONS--%>
        <div class="top-buttons top-buttons-combobox" style="justify-content: flex-end;">
            <div class="a-comboboxes" style="display:none;">
                <span>N⁰ Agrupación:</span>
                <telerik:RadNumericTextBox ID="TxtNewGroup"
                    runat="server"
                    Width="80px"
                    RenderMode="Lightweight"
                    MinValue="0"
                    MaxValue="999"
                    MaxLength="3"
                    ShowSpinButtons="False"                    
                    NumberFormat-DecimalDigits="0"
                    style="display:none;">
                </telerik:RadNumericTextBox>
            </div>
            <div class="a-buttons">
                <div class="find">
                    <span class="icon"></span>
                    <asp:LinkButton ID="btnFind"
                        runat="server"
                        OnClick="btnFind_OnClick"
                        Text="Buscar"></asp:LinkButton>
                </div>
                <div class="new" style="width: 160px">
                    <span class="icon"></span>
                    <asp:LinkButton ID="btnNew"
                        runat="server"
                        OnClick="btnNew_OnClick"
                        Text="Agrupar DR's"></asp:LinkButton>
                </div>
                <div class="report" style="width: 180px">
                    <span class="icon"></span>
                    <a onclick="reportIncomeDr('incomeDr')" role="button">Resumen Contable</a>
                </div>                
                <div class="report">
                    <span class="icon"></span>
                    <a onclick="reportIncomeDr('incomeAnnexedDr')" role="button">Anexo</a>
                </div>
            </div>
        </div>

        <div id="page_groupDr" class="box-block">
            <telerik:RadAjaxPanel ID="rapGroupDr"
                runat="server"
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
                                    <div class="form-group form-group form-group-fake">
                                        <div class="field-container">
                                            <span>Año Presup.:</span>
                                            <telerik:RadMonthYearPicker ID="RmyBudgetYear"
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
                                            <span>Año Ejer.:</span>
                                            <telerik:RadMonthYearPicker ID="RmyExerciseYear"
                                                runat="server"
                                                Width="100px"
                                                AutoPostBack="False"
                                                EnableTyping="False"
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
                                            <span>Documento:</span>
                                            <telerik:RadComboBox ID="RcDocuments"
                                                runat="server"
                                                Width="250px"
                                                AutoPostBack="False"
                                                DataTextField="DisplayLabel"
                                                DataValueField="TIPD_CODIGO">
                                            </telerik:RadComboBox>
                                        </div>
                                        <div class="field-container">
                                            <span>Consultar Agrupación existente N⁰:</span>
                                            <telerik:RadNumericTextBox ID="TxtGroup"
                                                runat="server"
                                                Width="80px"
                                                RenderMode="Lightweight"
                                                MinValue="0"
                                                MaxValue="999"
                                                MaxLength="3"
                                                ShowSpinButtons="False"
                                                NumberFormat-DecimalDigits="0">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>
                <div id="withOutGroup">
                    <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgDocuments"
                        runat="server"
                        AllowSorting="true"
                        Culture="es-ES"
                        GroupPanelPosition="Top"
                        OnNeedDataSource="RgDocuments_OnNeedDataSource"
                        OnPreRender="RgDocuments_OnPreRender"
                        AllowMultiRowSelection="True"
                        CssClass="incomeDrRecords-table">

                        <GroupingSettings CaseSensitive="false" />

                        <MasterTableView AutoGenerateColumns="false"
                            AllowFilteringByColumn="true"
                            DataKeyNames="DOC_CODIGO, DOC_AGRUPADO_DR_I"
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
                                <telerik:GridBoundColumn UniqueName="TIPD_NOMBRE_CORTO"
                                    DataField="TIPD_NOMBRE_CORTO"
                                    HeaderText="Doc."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="75px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PROV_NOMBRE"
                                    DataField="PROV_NOMBRE"
                                    HeaderText="Tercero"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="EXP_DESCRIPCION"
                                    DataField="EXP_DESCRIPCION"
                                    HeaderText="Descripción"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CACS_NUMERO"
                                    DataField="CACS_NUMERO"
                                    HeaderText="Aplicación"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="75px" />
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn UniqueName="AmountDocLabel"
                                    DataField="AmountDocLabel"
                                    HeaderText="Importe"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridNumericColumn UniqueName="AmountDocLabel"
                                    DataField="DOCA_IMPORTE"
                                    HeaderText="Importe"
                                    DataFormatString="{0:N}"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right"
                                    FilterControlWidth="100%">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridNumericColumn>
                                <telerik:GridBoundColumn UniqueName="DOC_FECHA_MOVIMIENTO_I"
                                    DataField="DOC_FECHA_MOVIMIENTO_I"
                                    HeaderText="Fecha Mov."
                                    AllowFiltering="true"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="GreaterThanOrEqualTo"
                                    ShowFilterIcon="false"
                                    DataFormatString="{0:dd/MM/yyyy}"
                                    FilterControlWidth="90%">
                                    <FilterTemplate>
                                        <telerik:RadDatePicker ID="FilteredDatePickerOperation"
                                            RenderMode="Lightweight"
                                            runat="server"
                                            Width="100%"
                                            ClientEvents-OnDateSelected="DateSelectedOperation"
                                            DbSelectedDate='<%#this.SetFilteredDate(Container,"DOC_FECHA_MOVIMIENTO_I") %>'
                                            Culture="es-ES" />
                                        <telerik:RadScriptBlock ID="RadScriptBlockOperation"
                                            runat="server">
                                            <script id="scriptOperation"
                                                type="text/javascript">
                                                function DateSelectedOperation(sender, args) {
                                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                    var date = FormatSelectedDateOperation(sender);
                                                    tableView.filter("DOC_FECHA_MOVIMIENTO_I", date, "GreaterThanOrEqualTo");
                                                }
                                                function FormatSelectedDateOperation(picker) {
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
                                <telerik:GridClientSelectColumn UniqueName="SelectColumn">
                                    <HeaderStyle Width="35px" />
                                </telerik:GridClientSelectColumn>
                            </Columns>
                        </MasterTableView>

                        <ClientSettings>
                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                            <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                            <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
                <div id="withGroup">
                    <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgDocumentsGroup"
                        runat="server"
                        AllowSorting="true"
                        Culture="es-ES"
                        GroupPanelPosition="Top"
                        OnNeedDataSource="RgDocumentsGroup_OnNeedDataSource"
                        CssClass="incomeDrRecords-table">

                        <GroupingSettings CaseSensitive="false" />

                        <MasterTableView AutoGenerateColumns="false"
                            AllowFilteringByColumn="true"
                            DataKeyNames="DOC_CODIGO, DOC_AGRUPADO_DR_I"
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
                                <telerik:GridBoundColumn UniqueName="NumberGroup"
                                    DataField="DOC_NUMERO_MOVIMIENTO_I"
                                    HeaderText="Agr."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="TIPD_NOMBRE_CORTOGroup"
                                    DataField="TIPD_NOMBRE_CORTO"
                                    HeaderText="Doc."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="75px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PROV_NOMBREGroup"
                                    DataField="PROV_NOMBRE"
                                    HeaderText="Tercero"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="EXP_DESCRIPCIONGroup"
                                    DataField="EXP_DESCRIPCION"
                                    HeaderText="Descripción"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CACS_NUMEROGroup"
                                    DataField="CACS_NUMERO"
                                    HeaderText="Aplicación"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="75px" />
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn UniqueName="AmountDocLabelGroup"
                                    DataField="AmountDocLabel"
                                    HeaderText="Importe"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridNumericColumn UniqueName="AmountDocLabelGroup"
                                    DataField="DOCA_IMPORTE"
                                    HeaderText="Importe"
                                    DataFormatString="{0:N}"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right"
                                    FilterControlWidth="100%">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridNumericColumn>
                                <telerik:GridBoundColumn UniqueName="DOC_FECHA_MOVIMIENTO_IGroup"
                                    DataField="DOC_FECHA_MOVIMIENTO_I"
                                    HeaderText="Fecha Mov."
                                    AllowFiltering="true"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="GreaterThanOrEqualTo"
                                    ShowFilterIcon="false"
                                    DataFormatString="{0:dd/MM/yyyy}"
                                    FilterControlWidth="90%">
                                    <FilterTemplate>
                                        <telerik:RadDatePicker ID="FilteredDatePickerOperationGroup"
                                            RenderMode="Lightweight"
                                            runat="server"
                                            Width="100%"
                                            ClientEvents-OnDateSelected="DateSelectedOperation"
                                            DbSelectedDate='<%#this.SetFilteredDate(Container,"DOC_FECHA_MOVIMIENTO_IGroup") %>'
                                            Culture="es-ES" />
                                        <telerik:RadScriptBlock ID="RadScriptBlockOperation"
                                            runat="server">
                                            <script id="scriptOperation"
                                                type="text/javascript">
                                                function DateSelectedOperation(sender, args) {
                                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                    var date = FormatSelectedDateOperation(sender);
                                                    tableView.filter("DOC_FECHA_MOVIMIENTO_IGroup", date, "GreaterThanOrEqualTo");
                                                }
                                                function FormatSelectedDateOperation(picker) {
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
                            </Columns>
                        </MasterTableView>

                        <ClientSettings>
                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                            <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
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

            function hideShowGrids(status) {
                if (status == "" || status == "New") {
                    $('#withGroup').hide();
                    $('#withOutGroup').show();
                } else {
                    $('#withOutGroup').hide();
                    $('#withGroup').show();
                }
            }

            function reportIncomeDr(report) {
                var budgetYearComp = $find("<%= this.RmyBudgetYear.ClientID %>");
                var budgetYearDate = budgetYearComp.get_selectedDate();
                var budgetYearValue = null;
                if (budgetYearDate != null) {

                    budgetYearValue = budgetYearDate.getFullYear();
                }

                var exerciseYearComp = $find("<%= this.RmyExerciseYear.ClientID %>");
                var exerciseYearValue = exerciseYearComp.get_selectedDate().getFullYear();

                var documentComp = $find("<%= this.RcDocuments.ClientID %>");
                var documentValue = documentComp.get_value();

                var groupComp = $find("<%= this.TxtGroup.ClientID %>");
                var groupValue = groupComp.get_value();

                if (groupValue === "") {
                    radalert("No se puede ver el Anexo DR debido a que el N⁰ de agrupación esta vacío.", 330, 140, "Imposible ver Anexo", null, null);
                    return;
                }

                var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=' + report + '&exerciseYear=' + exerciseYearValue + '&groupNumber=' + groupValue;

                if (budgetYearValue != null) {
                    url = url + '&budgetYear=' + budgetYearValue;
                }

                if (documentValue !== "-1") {
                    url = url + '&documentCode=' + documentValue;
                }

                window.open(url, "Reporte");
            }

        </script>
    </telerik:RadScriptBlock>
</asp:Content>
