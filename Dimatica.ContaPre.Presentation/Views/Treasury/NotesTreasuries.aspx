<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="NotesTreasuries.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Treasury.NotesTreasuries" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <%--Modales--%>
    <telerik:RadWindowManager ID="rwmNotesTreasuries" runat="server">
        <Windows>

            <%--estado provisional--%>
            <telerik:RadWindow ID="rwTreasuriesOrder"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Listado de Apuntes de Tesorería"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="320"
                Height="210"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientTreasuriesOrderCloseHandler">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <!-- Page Content -->
    <div id="section_treasury" class="container">

        <h3>Apuntes de Tesorería</h3>

        <div class="top-buttons">
            <div class="a-buttons">
                <div class="find">
                    <span class="icon"></span>
                    <asp:LinkButton ID="btnFind"
                        runat="server"
                        Text="Buscar"
                        OnClick="btnFind_Click">
                    </asp:LinkButton>
                </div>
                <div class="new">
                    <span class="icon"></span>
                    <a href="ManageTreasury.aspx" role="button">Nuevo</a>
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

        <div id="page_notesTreasuries" class="box-block">

            <telerik:RadAjaxPanel ID="rapUsers" runat="server"
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
                    Style="z-index: 100000">
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
                                            <span>Año:</span>
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
                                            <span>Tipo Movimiento: </span>
                                            <telerik:RadComboBox ID="RcOriginCode"
                                                runat="server"
                                                DataValueField="ORI_CODIGO_AUX"
                                                DataTextField="ORI_DESCRIPCION">
                                            </telerik:RadComboBox>
                                        </div>
                                        <div class="field-container">
                                            <span>Importe: </span>
                                            <telerik:RadNumericTextBox ID="RntTreasuryAmount"
                                                runat="server"
                                                RenderMode="Lightweight"
                                                Width="75px"
                                                MinValue="0"
                                                ShowSpinButtons="False"
                                                NumberFormat-DecimalDigits="2">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="field-container">
                                            <span>F. Banco entre: </span>
                                            <telerik:RadDatePicker ID="RdpSinceBankDate"
                                                RenderMode="Lightweight"
                                                runat="server"
                                                Width="150px"
                                                AutoPostBack="False"
                                                Culture="es-ES"
                                                EnableTyping="True"
                                                MaxDate="12/31/9999"
                                                MinDate="01/01/1800" />
                                        </div>
                                        <div class="field-container">
                                            <span>y:</span>
                                            <telerik:RadDatePicker ID="RdpUntilBankDate"
                                                RenderMode="Lightweight"
                                                runat="server"
                                                Width="150px"
                                                AutoPostBack="False"
                                                Culture="es-ES"
                                                EnableTyping="True"
                                                MaxDate="12/31/9999"
                                                MinDate="01/01/1800" />
                                        </div>
                                        <div class="field-container">
                                            <span>Talón/Transferencia: </span>
                                            <telerik:RadComboBox ID="RcPayFormCode"
                                                runat="server"
                                                DataValueField="FOR_CODIGO_AUX"
                                                DataTextField="DisplayLabel">
                                            </telerik:RadComboBox>
                                        </div>
                                        <div class="field-container">
                                            <span>N⁰ Talón o transferencia: </span>
                                            <telerik:RadTextBox ID="RtbCheckNumber"
                                                runat="server"
                                                Width="100px"
                                                Text=""
                                                MaxLength="30">
                                            </telerik:RadTextBox>
                                        </div>
                                        <div class="field-container">
                                            <span>Signo:</span>
                                            <telerik:RadComboBox ID="RcTreasuryHave"
                                                runat="server">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="< Seleccione >"
                                                        Value="-1" />
                                                    <telerik:RadComboBoxItem Text="+"
                                                        Value="0" />
                                                    <telerik:RadComboBoxItem Text="-"
                                                        Value="1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </div>
                                        <div class="field-container">
                                            <span>Descripción:</span>
                                            <telerik:RadTextBox ID="txtDescription"
                                                runat="server">
                                            </telerik:RadTextBox>
                                        </div>
                                        <div class="field-container">
                                            <span>F. Registro entre:</span>
                                            <telerik:RadDatePicker ID="RdpSinceEntryDate"
                                                RenderMode="Lightweight"
                                                runat="server"
                                                Width="150px"
                                                AutoPostBack="False"
                                                Culture="es-ES"
                                                EnableTyping="True"
                                                MaxDate="12/31/9999"
                                                MinDate="01/01/1800" />
                                        </div>
                                        <div class="field-container">
                                            <span>y:</span>
                                            <telerik:RadDatePicker ID="RdpUntilEntryDate"
                                                RenderMode="Lightweight"
                                                runat="server"
                                                Width="150px"
                                                AutoPostBack="False"
                                                Culture="es-ES"
                                                EnableTyping="True"
                                                MaxDate="12/31/9999"
                                                MinDate="01/01/1800" />
                                        </div>
                                        <div class="field-container">
                                            <span>Tipo Registro: </span>
                                            <telerik:RadComboBox ID="RcRegisterTypeCode"
                                                runat="server"
                                                DataValueField="TIPR_CODIGO"
                                                DataTextField="DisplayLabel">
                                            </telerik:RadComboBox>
                                        </div>
                                        <div class="field-container">
                                            <span>Anulado: </span>
                                            <telerik:RadComboBox ID="RcIsCanceled"
                                                runat="server"
                                                Width="130px"
                                                AutoPostBack="False">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="< Seleccione >"
                                                        Value="-1" />
                                                    <telerik:RadComboBoxItem Text="Si"
                                                        Value="1" />
                                                    <telerik:RadComboBoxItem Text="No"
                                                        Value="0" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </div>
                                        <div class="field-container">
                                            <span>Enlazado:</span>
                                            <telerik:RadComboBox ID="RcIsBound"
                                                runat="server"
                                                Width="130px"
                                                AutoPostBack="False">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="< Seleccione >"
                                                        Value="-1" />
                                                    <telerik:RadComboBoxItem Text="Si"
                                                        Value="1" />
                                                    <telerik:RadComboBoxItem Text="No"
                                                        Value="0" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </div>
                                        <div class="field-container">
                                            <span>T. Pendiente de cobro a fecha: </span>
                                            <telerik:RadDatePicker ID="RdpPendingDate"
                                                RenderMode="Lightweight"
                                                runat="server"
                                                Width="150px"
                                                AutoPostBack="False"
                                                Culture="es-ES"
                                                EnableTyping="True"
                                                MaxDate="12/31/9999"
                                                MinDate="01/01/1800" />
                                        </div>
                                        <div class="field-container">
                                            <span>Año doc. desde:</span>
                                            <telerik:RadMonthYearPicker ID="RmyDocSince"
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
                                            <span>Año doc. Hasta:</span>
                                            <telerik:RadMonthYearPicker ID="RmyDocUntil"
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
                                    </div>
                                </div>
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>

                <%-- tabla apuntes de tesoreria --%>
                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgTonnageSheet"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgTonnageSheet_NeedDataSource"
                    OnInsertCommand="RgTonnageSheet_InsertCommand"
                    OnUpdateCommand="RgTonnageSheet_UpdateCommand"
                    OnDeleteCommand="RgTonnageSheet_DeleteCommand"
                    OnItemDataBound="RgTonnageSheet_ItemDataBound"
                    OnPreRender="RgTonnageSheet_PreRender"
                    OnItemCommand="RgTonnageSheet_OnItemCommand"
                    CssClass="notesTreasuries-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="TES_CODIGO"
                        CommandItemDisplay="Top"
                        AllowPaging="true"
                        PagerStyle-AlwaysVisible="true"
                        PageSize="100"
                        NoMasterRecordsText="No Hay datos a Mostrar"
                        TableLayout="Fixed"
                        EditMode="PopUp"
                        CssClass="popup-table normal-table">

                        <CommandItemSettings AddNewRecordText="Nuevo"
                            ShowAddNewRecordButton="false"
                            ShowRefreshButton="True"
                            ShowExportToExcelButton="True"
                            ShowExportToPdfButton="False" />

                        <PagerStyle Mode="NextPrevAndNumeric"
                            PageSizeLabelText="Elementos por pagina: "
                            PagerTextFormat="Navigate pages {4} Página {0} de {1}, elementos {2} a {3} de {5}" />

                        <Columns>
                            <telerik:GridBoundColumn UniqueName="TES_ANO_PRESUPUESTO"
                                DataField="TES_ANO_PRESUPUESTO"
                                HeaderText="Año"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="80px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TES_FECHA_BANCO"
                                DataField="TES_FECHA_BANCO"
                                HeaderText="F. Banco"
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
                                        DbSelectedDate='<%#this.SetFilteredDate(Container,"TES_FECHA_BANCO") %>'
                                        Culture="es-ES" />
                                    <telerik:RadScriptBlock ID="RadScriptBlockOperation"
                                        runat="server">
                                        <script id="scriptOperation"
                                            type="text/javascript">
                                            function DateSelectedOperation(sender, args) {
                                                var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                var date = FormatSelectedDateOperation(sender);
                                                tableView.filter("TES_FECHA_BANCO", date, "GreaterThanOrEqualTo");
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
                            <telerik:GridBoundColumn UniqueName="TES_FECHA_APUNTE"
                                DataField="TES_FECHA_APUNTE"
                                HeaderText="F. Registro"
                                AllowFiltering="true"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="GreaterThanOrEqualTo"
                                ShowFilterIcon="false"
                                DataFormatString="{0:dd/MM/yyyy}"
                                FilterControlWidth="90%">
                                <FilterTemplate>
                                    <telerik:RadDatePicker ID="FilteredDatePickerOperation2"
                                        RenderMode="Lightweight"
                                        runat="server"
                                        Width="100%"
                                        ClientEvents-OnDateSelected="DateSelectedOperation2"
                                        DbSelectedDate='<%#this.SetFilteredDate(Container,"TES_FECHA_APUNTE") %>'
                                        Culture="es-ES" />
                                    <telerik:RadScriptBlock ID="RadScriptBlockOperation2"
                                        runat="server">
                                        <script id="scriptOperation"
                                            type="text/javascript">
                                            function DateSelectedOperation2(sender, args) {
                                                var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                var date = FormatSelectedDateOperation2(sender);
                                                tableView.filter("TES_FECHA_APUNTE", date, "GreaterThanOrEqualTo");
                                            }
                                            function FormatSelectedDateOperation2(picker) {
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
                            <telerik:GridBoundColumn UniqueName="TES_APLICACION"
                                DataField="TES_APLICACION"
                                HeaderText="Aplicación."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn UniqueName="AmountLabel"
                                DataField="AmountLabel"
                                HeaderText="Importe"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="GreaterThanOrEqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right">
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridNumericColumn UniqueName="TES_TOTAL_IMPORTE_LIQUIDO"
                                DataField="TES_TOTAL_IMPORTE_LIQUIDO"
                                HeaderText="Importe"
                                DataFormatString="{0:N}"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right"
                                FilterControlWidth="100%">
                                <HeaderStyle Width="100px" />
                            </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn UniqueName="SingLabel"
                                DataField="SingLabel"
                                HeaderText="Signo"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="50px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ORI_DESCRIPCION"
                                DataField="ORI_DESCRIPCION"
                                HeaderText="Origen"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TES_NUMERO_CHEQUE"
                                DataField="TES_NUMERO_CHEQUE"
                                HeaderText="N⁰ Talón"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TES_DESCRIPCION"
                                DataField="TES_DESCRIPCION"
                                HeaderText="Descripción"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn UniqueName="EditColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Editar apunte"
                                CommandName="UpdateTreasury"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-pencil-alt"
                                Text=" "
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>
                        </Columns>

                        <EditFormSettings EditFormType="Template">
                            <FormTemplate>
                            </FormTemplate>
                        </EditFormSettings>

                    </MasterTableView>

                    <ClientSettings>
                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                    </ClientSettings>
                </telerik:RadGrid>
                <div class="monto-total-notesTreasuries">
                    <strong>Total:</strong>
                    <telerik:RadTextBox ID="txtTotal"
                        Width="120px"
                        runat="server"
                        MaxLength="80"
                        Text="0,00"
                        Enabled="False">
                    </telerik:RadTextBox>
                </div>
            </telerik:RadAjaxPanel>
        </div>
    </div>
    <telerik:RadScriptBlock runat="server">
        <script>

            function btnReportOnClientClick(sender, eventArgs) {
                var url = "SelectTreasuriesOrder.aspx";
                var manager = $find("<%= this.rwmNotesTreasuries.ClientID %>");
                var oWnd = manager.open(url, "rwTreasuriesOrder");
            }

            function OnClientTreasuriesOrderCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    var title = data.title;
                    var order = data.order;

                    debugger 
                    var exerciseYearComp = $find("<%= this.RmyExerciseYear.ClientID %>");
                    var exerciseYearDate = exerciseYearComp.get_selectedDate();
                    var exerciseYearValue = null;
                    if (exerciseYearDate != null) {

                        exerciseYearValue = exerciseYearDate.getFullYear();
                    }

                    var originCodeComp = $find("<%= this.RcOriginCode.ClientID %>");
                    var originCodeValue = originCodeComp.get_selectedItem().get_value();

                    var amountComp = $find("<%= this.RntTreasuryAmount.ClientID %>");
                    var amountValue = amountComp.get_value();

                    var sinceBankDateComp = $find("<%= this.RdpSinceBankDate.ClientID %>");
                    var sinceBankDateValue = FormatText(sinceBankDateComp.get_selectedDate());

                    var untilBankDateComp = $find("<%= this.RdpUntilBankDate.ClientID %>");
                    var untilBankDateValue = FormatText(untilBankDateComp.get_selectedDate());

                    var payFormCodeComp = $find("<%= this.RcPayFormCode.ClientID %>");
                    var payFormCodeValue = payFormCodeComp.get_selectedItem().get_value();

                    var checkNumberComp = $find("<%= this.RtbCheckNumber.ClientID %>");
                    var checkNumberValue = checkNumberComp.get_value();

                    var treasuryHaveComp = $find("<%= this.RcTreasuryHave.ClientID %>");
                    var treasuryHaveValue = treasuryHaveComp.get_selectedItem().get_value();

                    var descriptionComp = $find("<%= this.txtDescription.ClientID %>");
                    var descriptionValue = descriptionComp.get_value();

                    var sinceDateEntryComp = $find("<%= this.RdpSinceEntryDate.ClientID %>");
                    var sinceDateEntryValue = FormatText(sinceDateEntryComp.get_selectedDate());

                    var untilDateEntryComp = $find("<%= this.RdpUntilEntryDate.ClientID %>");
                    var untilDateEntryValue = FormatText(untilDateEntryComp.get_selectedDate());

                    var registerTypeCodeComp = $find("<%= this.RcRegisterTypeCode.ClientID %>");
                    var registerTypeCodeValue = registerTypeCodeComp.get_selectedItem().get_value();

                    var isCanceledComp = $find("<%= this.RcIsCanceled.ClientID %>");
                    var isCanceledValue = isCanceledComp.get_selectedItem().get_value();

                    var isBoundComp = $find("<%= this.RcIsBound.ClientID %>");
                    var isBoundValue = isBoundComp.get_selectedItem().get_value();

                    var pendingDateComp = $find("<%= this.RdpPendingDate.ClientID %>");
                    var pendingDateValue = FormatText(pendingDateComp.get_selectedDate());

                    var sinceDocYearComp = $find("<%= this.RmyDocSince.ClientID %>");
                    var sinceDocYearDate = sinceDocYearComp.get_selectedDate();
                    var sinceDocYearValue = null;
                    if (sinceDocYearDate != null) {

                        sinceDocYearValue = sinceDocYearDate.getFullYear();
                    }

                    var untilDocYearComp = $find("<%= this.RmyDocUntil.ClientID %>");
                    var untilDocYearDate = untilDocYearComp.get_selectedDate();
                    var untilDocYearValue = null;
                    if (untilDocYearDate != null) {

                        untilDocYearValue = untilDocYearDate.getFullYear();
                    }

                    var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=treasuryNotesTreasuries&title=' + title + '&order=' + order;

                    if (exerciseYearValue !== null) {
                        url = url + '&exerciseYear=' + exerciseYearValue;
                    }

                    if (originCodeValue !== "-1") {
                        url = url + '&originCode=' + originCodeValue;
                    }

                    if (amountValue !== "") {
                        url = url + '&amount=' + amountValue;
                    }

                    if (sinceBankDateValue !== null) {
                        url = url + '&sinceBankDate=' + sinceBankDateValue;
                    }

                    if (untilBankDateValue !== null) {
                        url = url + '&untilBankDate=' + untilBankDateValue;
                    }

                    if (payFormCodeValue !== "-1") {
                        url = url + '&payFormCode=' + payFormCodeValue;
                    }

                    if (checkNumberValue !== "") {
                        url = url + '&checkNumber=' + checkNumberValue;
                    }

                    if (treasuryHaveValue !== "-1") {
                        url = url + '&treasuryHave=' + treasuryHaveValue;
                    }

                    if (descriptionValue !== "") {
                        url = url + '&description=' + descriptionValue;
                    }

                    if (sinceDateEntryValue !== null) {
                        url = url + '&sinceDateEntry=' + sinceDateEntryValue;
                    }

                    if (untilDateEntryValue !== null) {
                        url = url + '&untilDateEntry=' + untilDateEntryValue;
                    }

                    if (registerTypeCodeValue !== "-1") {
                        url = url + '&registerTypeCode=' + registerTypeCodeValue;
                    }

                    if (isCanceledValue !== "-1") {
                        url = url + '&isCanceled=' + isCanceledValue;
                    }

                    if (isBoundValue !== "-1") {
                        url = url + '&isBound=' + isBoundValue;
                    }

                    if (pendingDateValue !== null) {
                        url = url + '&pendingDate=' + pendingDateValue;
                    }

                    if (sinceDocYearValue !== null) {
                        url = url + '&sinceDocYear=' + sinceDocYearValue;
                    }

                    if (untilDocYearValue !== null) {
                        url = url + '&untilDocYear=' + untilDocYearValue;
                    }

                    window.open(url, "Reporte");
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

        </script>
    </telerik:RadScriptBlock>
</asp:Content>
