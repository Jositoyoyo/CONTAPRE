<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="ManageFile.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Income.ManageFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
            <telerik:RadAjaxLoadingPanel
                ID="RadAjaxLoadingPanel1"
                runat="server"
                Skin="Material"
                Transparency="0"
                Modal="True">
                <asp:Label ID="Label2" runat="server" ForeColor="Red">Loading...</asp:Label>
            </telerik:RadAjaxLoadingPanel>

    <%--Modales--%>
    <telerik:RadWindowManager ID="rwmManageFile" runat="server">
        <Windows>

            <telerik:RadWindow ID="rwUpdateApplications"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Aplicaciones"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="800"
                Height="600"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientUpdateApplicationsCloseHandler">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
            <telerik:RadWindow
                ID="NewProviderWindow"
                runat="server"
                Modal="True"
                RenderMode="Lightweight"
                NavigateUrl="~/Views/Shared/Components/Partials/Providers/NewProvider.aspx"
                Title="Nuevo Proveedor"
                InitialBehaviors="Maximize"
                CenterIfModal="True"
                EnableShadow="True"
                VisibleStatusbar="False"
                Behaviors="Close, Move, Resize"
                OnClientClose="refreshComboBox"
                OnClientBeforeShow="showSpinnerInWindow"
                OnClientPageLoad="hideSpinnerInWindow">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <!-- Page Content -->
    <div id="section_income" class="container" style="height: calc(100vh - 56px) !important">

        <h3 id="titleHeader" runat="server">Nuevo Expediente</h3>

        <%-- Manage --%>
        <div class="manage">
            <div class="form-group form-group-fake">

                <%--Año--%>
                <div class="field-container">
                    <span>Año:</span>
                    <telerik:RadMonthYearPicker ID="RmyYear"
                        runat="server"
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
                    <asp:RequiredFieldValidator ID="rfvYear"
                        runat="server"
                        Display="Dynamic"
                        ValidationGroup="FileGroup"
                        ControlToValidate="rmyYear"
                        ErrorMessage=" * "
                        ToolTip="Introduzca el Año del Expediente."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </div>

                <%--Proveedor--%>
                <div class="field-container">
                    <span>Proveedor:</span>
                    <telerik:RadComboBox ID="RcProviders"
                        runat="server"
                        Width="300px"
                        AutoPostBack="False"
                        DataTextField="PROV_NOMBRE"
                        DataValueField="PROV_CODIGO">
                    </telerik:RadComboBox>

                    <!-- boton para añadir un nuevo proveedor -->
               
                    <telerik:RadLinkButton 
                        runat="server" 
                        Text="Nuevo Proveedor" 
                         RenderMode="Native"
                         AutoPostBack="True"
                        OnClientClicked="openNewProviderWindow" 
                        style="margin-top: 1px; border: none;"></telerik:RadLinkButton>

                </div>

                <%--Procedencia--%>
                <div class="field-container">
                    <span>Procedencia:</span>
                    <telerik:RadComboBox ID="RcProvenances"
                        runat="server"
                        Width="175px"
                        AutoPostBack="False"
                        DataTextField="PROC_DESCRIPCION"
                        DataValueField="PROC_CODIGO">
                    </telerik:RadComboBox>
                </div>

                <%--Area--%>
                <div class="field-container">
                    <span>Área Origen:</span>
                    <telerik:RadComboBox ID="RcAreas"
                        runat="server"
                        Width="175px"
                        AutoPostBack="False"
                        DataTextField="DisplayLabelArea"
                        DataValueField="CEN_CODIGO">
                    </telerik:RadComboBox>
                </div>

                <%--Descripcion--%>
                <div class="field-container">
                    <span>Descripción:</span>
                    <telerik:RadTextBox ID="RtbDescription"
                        runat="server"
                        Width="100%"
                        Text=""
                        MaxLength="255">
                    </telerik:RadTextBox>
                </div>
            </div>

            <div class="form-group">
                <%--Expediente Cuadrado--%>
                <div class="field-container" style="display: flex; flex-direction: row">
                    <span style="margin-right: 5px">Cuadrado:</span>
                    <asp:Label ID="LblSquare"
                        CssClass="text-red"
                        runat="server">
                    </asp:Label>
                </div>

                <%--Suma DR--%>
                <div class="field-container" style="display: flex; flex-direction: row">
                    <span style="margin-right: 5px">Suma DR:</span>
                    <asp:Label ID="LblDrAmount"
                        CssClass="text-red"
                        runat="server">
                    </asp:Label>
                </div>

                <%--Suma MI--%>
                <div class="field-container" style="display: flex; flex-direction: row">
                    <span style="margin-right: 5px">Suma MI:</span>
                    <asp:Label ID="LblMiAmount"
                        CssClass="text-red"
                        runat="server">
                    </asp:Label>
                </div>

                <%--Diferencia--%>
                <div class="field-container" style="display: flex; flex-direction: row">
                    <span style="margin-right: 5px">Diferencia:</span>
                    <asp:Label ID="LblDifference"
                        CssClass="text-red"
                        runat="server">
                    </asp:Label>
                </div>
            </div>


            <%--BUTTONS--%>
            <div class="form-group buttons">

                <telerik:RadButton ButtonType="LinkButton" ID="btnSave"
                    runat="server"
                    RenderMode="Native"
                    Text="Grabar"
                    AutoPostBack="True"
                    OnClick="btnSave_OnClick"
                    ValidationGroup="FileGroup">
                </telerik:RadButton>

                <telerik:RadButton ButtonType="LinkButton" ID="btnDelete"
                    runat="server"
                    RenderMode="Native"
                    Text="Eliminar"
                    AutoPostBack="True"
                    OnClick="btnDelete_OnClick">
                </telerik:RadButton>

                <telerik:RadButton ButtonType="LinkButton" ID="btnBack"
                    runat="server"
                    RenderMode="Native"
                    Text="Volver"
                    AutoPostBack="True"
                    OnClick="btnBack_OnClick">
                </telerik:RadButton>

            </div>
        </div>

        <div id="page_manageFile" class="box-block">
            <telerik:RadAjaxPanel ID="rapManageFile"
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
                <div>
                    <div class="field-document">

                        <div class="new-document">
                            <span>Documentos:</span>
                        </div>

                        <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgDocuments"
                            runat="server"
                            AllowSorting="true"
                            Culture="es-ES"
                            GroupPanelPosition="Top"
                            OnNeedDataSource="RgDocuments_OnNeedDataSource"
                            OnPreRender="RgDocuments_OnPreRender"
                            OnItemDataBound="RgDocuments_OnItemDataBound"
                            OnInsertCommand="RgDocuments_OnInsertCommand"
                            OnUpdateCommand="RgDocuments_OnUpdateCommand"
                            OnDeleteCommand="RgDocuments_OnDeleteCommand"
                            OnItemCommand="RgDocuments_OnItemCommand"
                            CssClass="manageFile-table">

                            <GroupingSettings CaseSensitive="false" />

                            <MasterTableView AutoGenerateColumns="false"
                                AllowFilteringByColumn="true"
                                DataKeyNames="DOC_CODIGO, TIPD_CODIGO, DOC_NUMERO_MOVIMIENTO_I, DOC_FECHA_MOVIMIENTO_I, DOC_FECHA_PROPUESTA, DOC_FECHA_ASIENTO_DIARIO, DOC_DESCRIPCION, CUE_CODIGO, DOC_ENLAZADO_TESORERIA, HOJ_NUMERO, ANO_HOJA, HOJ_NUMERO50, ANO_HOJA50, DOC_NUMERO_CHEQUE, TIPO_DOC"
                                CommandItemDisplay="Top"
                                AllowPaging="false"
                                PagerStyle-AlwaysVisible="false"
                                NoMasterRecordsText="No Hay datos a Mostrar."
                                TableLayout="Fixed"
                                EditMode="PopUp"
                                CssClass="popup-table big-table">

                                <CommandItemSettings ShowRefreshButton="False"
                                    ShowExportToExcelButton="False"
                                    ShowExportToPdfButton="False"
                                    ShowAddNewRecordButton="true"
                                    AddNewRecordText="Nuevo Documento" />

                                <PagerStyle Mode="NextPrevAndNumeric"
                                    PageSizeLabelText="Elementos por pagina: "
                                    PagerTextFormat="Navigate pages {4} Página {0} de {1}, elementos {2} a {3} de {5}" />

                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="TIPO_DOC"
                                        DataField="TIPO_DOC"
                                        HeaderText="Tipo Doc."
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle Width="75px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="DOC_NUMERO_MOVIMIENTO_I"
                                        DataField="DOC_NUMERO_MOVIMIENTO_I"
                                        HeaderText="N⁰ Ingreso"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle Width="50px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="DOC_FECHA_MOVIMIENTO_I"
                                        DataField="DOC_FECHA_MOVIMIENTO_I"
                                        HeaderText="Fecha Movimiento"
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
                                    <telerik:GridBoundColumn UniqueName="DOC_FECHA_PROPUESTA"
                                        DataField="DOC_FECHA_PROPUESTA"
                                        HeaderText="Fecha Propuesta"
                                        AllowFiltering="true"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="GreaterThanOrEqualTo"
                                        ShowFilterIcon="false"
                                        DataFormatString="{0:dd/MM/yyyy}"
                                        FilterControlWidth="90%">
                                        <FilterTemplate>
                                            <telerik:RadDatePicker ID="FilteredDatePickerProposal"
                                                RenderMode="Lightweight"
                                                runat="server"
                                                Width="100%"
                                                ClientEvents-OnDateSelected="DateSelectedProposal"
                                                DbSelectedDate='<%#this.SetFilteredDate(Container,"DOC_FECHA_PROPUESTA") %>'
                                                Culture="es-ES" />
                                            <telerik:RadScriptBlock ID="RadScriptBlockProposal"
                                                runat="server">
                                                <script id="scriptProposal"
                                                    type="text/javascript">
                                                    function DateSelectedProposal(sender, args) {
                                                        var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                        var date = FormatSelectedDateProposal(sender);
                                                        tableView.filter("DOC_FECHA_PROPUESTA", date, "GreaterThanOrEqualTo");
                                                    }
                                                    function FormatSelectedDateProposal(picker) {
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
                                    <telerik:GridBoundColumn UniqueName="DOC_FECHA_ASIENTO_DIARIO"
                                        DataField="DOC_FECHA_ASIENTO_DIARIO"
                                        HeaderText="Fecha Asiento"
                                        AllowFiltering="true"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="GreaterThanOrEqualTo"
                                        ShowFilterIcon="false"
                                        DataFormatString="{0:dd/MM/yyyy}"
                                        FilterControlWidth="90%">
                                        <FilterTemplate>
                                            <telerik:RadDatePicker ID="FilteredDatePickerEffective"
                                                RenderMode="Lightweight"
                                                runat="server"
                                                Width="100%"
                                                ClientEvents-OnDateSelected="DateSelectedEffective"
                                                DbSelectedDate='<%#this.SetFilteredDate(Container,"DOC_FECHA_ASIENTO_DIARIO") %>'
                                                Culture="es-ES" />
                                            <telerik:RadScriptBlock ID="RadScriptBlockEffective"
                                                runat="server">
                                                <script id="scriptEffective"
                                                    type="text/javascript">
                                                    function DateSelectedEffective(sender, args) {
                                                        var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                        var date = FormatSelectedDateEffective(sender);
                                                        tableView.filter("DOC_FECHA_ASIENTO_DIARIO", date, "GreaterThanOrEqualTo");
                                                    }
                                                    function FormatSelectedDateEffective(picker) {
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
                                    <telerik:GridBoundColumn UniqueName="DOC_DESCRIPCION"
                                        DataField="DOC_DESCRIPCION"
                                        HeaderText="Texto"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle Width="40%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ORDINAL_PAGADOR"
                                        DataField="ORDINAL_PAGADOR"
                                        HeaderText="Ordinal Pagador"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle Width="40%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridCheckBoxColumn UniqueName="DOC_ENLAZADO_TESORERIA"
                                        DataField="DOC_ENLAZADO_TESORERIA"
                                        DataType="System.Boolean"
                                        HeaderText="Tes."
                                        StringFalseValue="False"
                                        StringTrueValue="True"
                                        AllowFiltering="false">
                                        <HeaderStyle Width="40px" />
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridBoundColumn UniqueName="HOJ_NUMERO"
                                        DataField="HOJ_NUMERO"
                                        HeaderText="H. Arqueo"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle Width="75px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="HOJ_NUMERO50"
                                        DataField="HOJ_NUMERO50"
                                        HeaderText="H. Arqueo 50"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle Width="75px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ANO_HOJA"
                                        DataField="ANO_HOJA"
                                        HeaderText="Año Hoja"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle Width="60px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ANO_HOJA50"
                                        DataField="ANO_HOJA50"
                                        HeaderText="Año Hoja 50"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle Width="60px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="DOC_NUMERO_CHEQUE"
                                        DataField="DOC_NUMERO_CHEQUE"
                                        HeaderText="N⁰ Cheque"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle Width="75px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="SEN_NUMERO"
                                        DataField="SEN_NUMERO"
                                        HeaderText="N⁰ Señal."
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle Width="75px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridEditCommandColumn UniqueName="EditColumn"
                                        ButtonType="LinkButton"
                                        HeaderTooltip="Modificar Documento"
                                        ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="40px"
                                        ItemStyle-Width="40px"
                                        ItemStyle-CssClass="fas fa-pencil-alt"
                                        ShowFilterIcon="false"
                                        EditText=" ">
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridButtonColumn UniqueName="DeleteColumn"
                                        ButtonType="LinkButton"
                                        HeaderTooltip="Eliminar Documento"
                                        CommandName="DeleteCommand"
                                        HeaderStyle-Width="40px"
                                        ItemStyle-Width="40px"
                                        ItemStyle-CssClass="fas fa-trash-alt"
                                        Text=" "
                                        HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridButtonColumn>
                                    <telerik:GridButtonColumn UniqueName="ApplicationsColumn"
                                        ButtonType="LinkButton"
                                        HeaderTooltip="Gestionar Aplicaciones"
                                        CommandName="UpdateApplications"
                                        HeaderStyle-Width="40px"
                                        ItemStyle-Width="40px"
                                        ItemStyle-CssClass="fas fa-application-alt"
                                        Text=" "
                                        HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridButtonColumn>
                                    <telerik:GridButtonColumn UniqueName="PrintColumn"
                                        ButtonType="LinkButton"
                                        HeaderTooltip="Imprimir"
                                        CommandName="PrintApplications"
                                        HeaderStyle-Width="40px"
                                        ItemStyle-Width="40px"
                                        ItemStyle-CssClass="fas fa-print-alt"
                                        Text=" "
                                        HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridButtonColumn>
                                </Columns>

                                <EditFormSettings EditFormType="Template">
                                    <FormTemplate>

                                        <h3>Documento Contable</h3>

                                        <div class="form-group form-group-fake">

                                            <%--Tipo Documento--%>
                                            <div class="field-container field-20">
                                                <span>Tipo Doc.:</span>
                                                <telerik:RadComboBox ID="radDropDocumentType"
                                                    runat="server"
                                                    Width="100%"
                                                    DataValueField="TIPD_CODIGO"
                                                    DataTextField="DisplayLabel">
                                                </telerik:RadComboBox>
                                            </div>

                                            <%--N⁰ Ingreso--%>
                                            <div class="field-container field-20">
                                                <span>N⁰ Ingreso:</span>
                                                <telerik:RadNumericTextBox ID="txtNumber"
                                                    runat="server"
                                                    RenderMode="Lightweight"
                                                    MinValue="0"
                                                    ShowSpinButtons="False"
                                                    NumberFormat-DecimalDigits="0"
                                                    Width="100%">
                                                </telerik:RadNumericTextBox>
                                            </div>

                                            <%--Fecha Movimiento--%>
                                            <div class="field-container field-20">
                                                <span>Fecha Movimiento:</span>
                                                <telerik:RadDatePicker ID="rdOperation"
                                                    RenderMode="Lightweight"
                                                    runat="server"
                                                    Width="100%"
                                                    AutoPostBack="False"
                                                    Culture="es-ES"
                                                    EnableTyping="True"
                                                    MaxDate="12/31/9999"
                                                    MinDate="01/01/1800" />
                                                <asp:RequiredFieldValidator ID="rfvOperation"
                                                    runat="server"
                                                    Display="Dynamic"
                                                    ValidationGroup="DocumentGroup"
                                                    ControlToValidate="rdOperation"
                                                    ErrorMessage=" * "
                                                    ToolTip="Introduzca la fecha de movimiento del Documento."
                                                    ForeColor="Red">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <%--Fecha Propuesta--%>
                                            <div class="field-container field-20">
                                                <span>Fecha Propuesta:</span>
                                                <telerik:RadDatePicker ID="rdProposal"
                                                    RenderMode="Lightweight"
                                                    runat="server"
                                                    Width="100%"
                                                    AutoPostBack="False"
                                                    Culture="es-ES"
                                                    EnableTyping="True"
                                                    MaxDate="12/31/9999"
                                                    MinDate="01/01/1800" />
                                            </div>

                                            <%--Fecha Asiento--%>
                                            <div class="field-container field-20">
                                                <span>Fecha Asiento:</span>
                                                <telerik:RadDatePicker ID="rdEffective"
                                                    RenderMode="Lightweight"
                                                    runat="server"
                                                    Width="100%"
                                                    AutoPostBack="False"
                                                    Culture="es-ES"
                                                    EnableTyping="True"
                                                    MaxDate="12/31/9999"
                                                    MinDate="01/01/1800" />
                                            </div>
                                        </div>

                                        <div class="form-group form-group-fake">
                                            <%--Descripcion--%>
                                            <div class="field-container field-40">
                                                <span>Descripción:</span>
                                                <telerik:RadTextBox ID="txtDescription"
                                                    Width="100%"
                                                    runat="server"
                                                    MaxLength="255"
                                                    Text="">
                                                </telerik:RadTextBox>
                                            </div>

                                            <%--Ordinal Pagador--%>
                                            <div class="field-container field-30">
                                                <span>Ordinal Pagador:</span>
                                                <telerik:RadComboBox ID="radDropRestrictedAccount"
                                                    runat="server"
                                                    Width="100%"
                                                    DataValueField="CUE_CODIGO"
                                                    DataTextField="DisplayLabel">
                                                </telerik:RadComboBox>
                                            </div>

                                            <%--N⁰ Cheque--%>
                                            <div class="field-container field-20">
                                                <span>N⁰ Cheque:</span>
                                                <telerik:RadTextBox ID="txtCheck"
                                                    Width="100%"
                                                    runat="server"
                                                    MaxLength="20"
                                                    Text="">
                                                </telerik:RadTextBox>
                                            </div>

                                            <%--Enlazado--%>
                                            <div class="field-container field-10">
                                                <span>Enlazado:</span>
                                                <telerik:RadCheckBox ID="checkBinding"
                                                    runat="server"
                                                    Checked="False"
                                                    Text=""
                                                    AutoPostBack="false">
                                                </telerik:RadCheckBox>
                                            </div>
                                        </div>

                                        <div class="form-group form-group-fake">

                                            <%--Año Hoja Arqueo--%>
                                            <div class="field-container field-25">
                                                <span>Año Hoja Arqueo:</span>
                                                <telerik:RadMonthYearPicker ID="rmyTonnageSheetYear"
                                                    runat="server"
                                                    AutoPostBack="True"
                                                    EnableTyping="False"
                                                    Culture="es-ES"
                                                    DateInput-Culture-="es-ES"
                                                    OnSelectedDateChanged="TxtTonnageSheet_SelectedDateChanged"
                                                    MonthCellsStyle-CssClass="monthCellClass">
                                                    <MonthYearNavigationSettings TodayButtonCaption="Actual"
                                                        OkButtonCaption="Aceptar"
                                                        CancelButtonCaption="Cancelar" />
                                                    <DateInput runat="server"
                                                        DateFormat="yyyy"
                                                        DisplayDateFormat="yyyy" />
                                                </telerik:RadMonthYearPicker>

                                            </div>

                                            <%--N⁰ Hoja Arqueo--%>
                                            <div class="field-container field-25">
                                                <span>N⁰ Hoja Arqueo:</span>
                                                <telerik:RadNumericTextBox ID="txtTonnageSheet"
                                                    runat="server"
                                                    RenderMode="Lightweight"
                                                    MinValue="0"
                                                    ShowSpinButtons="False"
                                                    NumberFormat-DecimalDigits="0">
                                                </telerik:RadNumericTextBox>
                                            </div>

                                            <%--Año Hoja Arqueo 50--%>
                                            <div class="field-container field-25">
                                                <span>Año Hoja Arqueo 50:</span>
                                                <telerik:RadMonthYearPicker ID="rmyTonnageSheet50Year"
                                                    runat="server"
                                                    AutoPostBack="True"
                                                    EnableTyping="False"
                                                    Culture="es-ES"
                                                    DateInput-Culture-="es-ES"
                                                    OnSelectedDateChanged="TxtTonnageSheet50_SelectedDateChanged"
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

                                            <%--N⁰ Hoja Arqueo 50--%>
                                            <div class="field-container field-25">
                                                <span>N⁰ Hoja Arqueo 50:</span>
                                                <telerik:RadNumericTextBox ID="txtTonnageSheet50"
                                                    runat="server"
                                                    RenderMode="Lightweight"
                                                    MinValue="0"
                                                    ShowSpinButtons="False"
                                                    NumberFormat-DecimalDigits="0">
                                                </telerik:RadNumericTextBox>
                                            </div>


                                        </div>

                                        <%--BUTTONS--%>
                                        <div class="buttons">

                                            <asp:LinkButton ID="btnIncomeUpdate"
                                                runat="server"
                                                ValidationGroup="DocumentGroup"
                                                CssClass="Button Add"
                                                CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                OnClientClick="return confirmChangesHoja();">
                                                    <%# (Container is GridEditFormInsertItem) ? "Insertar   " : "Actualizar   " %>
                                            </asp:LinkButton>

                                            <asp:LinkButton ID="btnIncomeCancel"
                                                runat="server"
                                                CausesValidation="False"
                                                CommandName="Cancel"
                                                CssClass="Button Cancel">
                                                Cancelar
                                            </asp:LinkButton>

                                        </div>

                                    </FormTemplate>
                                </EditFormSettings>
                            </MasterTableView>

                            <ClientSettings>
                                <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                            </ClientSettings>

                        </telerik:RadGrid>
                    </div>
                </div>
            </telerik:RadAjaxPanel>

        </div>
    </div>

    <telerik:RadScriptBlock runat="server">

        <script>

            var modalDiv = null;

            function confirmChangesHoja()
            {
                var currentYear = document.getElementById("hiddenCurrentYear").value;
                var updatedYear = document.getElementById("hiddenUpdatedYear").value;
                var currentSheetNumber = document.getElementById("hiddenCurrentSheetNumber").value;
                var updatedSheetNumber = document.getElementById("hiddenUpdatedSheetNumber").value;

                if (currentYear !== updatedYear || currentSheetNumber !== updatedSheetNumber)
                {
                    return confirm("Se han cambiado los datos de la hoja. ¿Desea continuar?");
                }
                return true;
            }

            function showLoading(app, args) {
                var loadingPanel = $find('<%= RadAjaxLoadingPanel1.ClientID %>');
                loadingPanel.show('<%= RadAjaxLoadingPanel1.ClientID %>');
            }

            function hideLoading(app, args) {
                var loadingPanel = $find('<%= RadAjaxLoadingPanel1.ClientID %>');
                loadingPanel.hide('<%= RadAjaxLoadingPanel1.ClientID %>');
            }

            function refreshComboBox()
            {
                showLoading();
                window.setTimeout(function () {
                    window.location.reload();
                }, 1500)   
            }

            function openNewProviderWindow()
            {
                var window = $find("<%= NewProviderWindow.ClientID %>");
                window.show();
                window.center();
            }

            function showSpinnerInWindow()
            {

                // Obtener la instancia de la ventana
                var window = $find("<%= NewProviderWindow.ClientID %>");

                if (window) {
                    // Crear un spinner dentro de la ventana
                    var contentElement = window.get_contentElement();
                    var spinner = document.createElement("div");
                    spinner.id = "spinner";
                    spinner.style.position = "absolute";
                    spinner.style.top = "50%";
                    spinner.style.left = "50%";
                    spinner.style.transform = "translate(-50%, -50%)";
                    spinner.style.zIndex = "9999";
                    spinner.innerHTML = '<p>Cargando el contenido, espere por favor...</p>';
                    contentElement.appendChild(spinner);
                }
            }

            function hideSpinnerInWindow()
            {
                // Ocultar el spinner una vez que el contenido esté cargado
                var spinner = document.getElementById("spinner");
                if (spinner) {
                    spinner.remove();
                }
            }

            function showModalDiv(sender, args)
            {
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

            function hideModalDiv()
            {
                modalDiv.style.display = "none";
            }

            function hideGrids()
            {
                $('#<%=this.btnDelete.ClientID %>').hide();
                $('#page_manageFile').hide();
            }

            function moveNewButtons()
            {
                var $this = $("a[title*='Nuevo']");
                $this.each(function () {
                    var $destino = $(this).closest('.field-document').children(".new-document");
                    !$destino.has("a").length && $(this).appendTo($destino).show();
                });
            }

            var itemIndex;
            function deleteIncomeRecord(index, documentType) {
                itemIndex = index;
                radconfirm("¿ Desea realmente eliminar el documento contable de Ingresos " + documentType + " ?",
                    confirmDeleteIncomeRecordCallBackFn,
                    500,
                    220,
                    null,
                    "Confirmación",
                    null);
            }

            function confirmDeleteIncomeRecordCallBackFn(arg) {
                if (arg == null || arg == false) {
                    return;
                }

                if (itemIndex) {
                    var masterTable = $find("<%= this.RgDocuments.ClientID %>").get_masterTableView();
                    masterTable.fireCommand("Delete", itemIndex);
                }
            }

            function updateApplications(id, year, fileId, documentTitle) {
                var url = "UpdateApplications.aspx?id=" + id + "&year=" + year + "&fileId=" + fileId + "&documentTitle=" + documentTitle;
                var manager = $find("<%= this.rwmManageFile.ClientID %>");
                var oWnd = manager.open(url, "rwUpdateApplications");
            }

            function printApplications(documentId, report) {
                var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=' + report + '&documentId=' + documentId;

                window.open(url, "Reporte");
            }

            function OnClientUpdateApplicationsCloseHandler(sender, args) {
                location.reload(true);
            }


            function confirmDeleteCallBackFn(arg) {
                if (arg == null || arg == false) {
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: "ManageFile.aspx/DeleteAccountingRecord",
                    data: null,
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    success: function (result) {
                        switch (result.d) {
                            case 0:
                                radalert("Ha ocurrido un error eliminando el Expediente Contable en cuestión.", 330, 140, "Imposible eliminar expediente contable", null, null);
                                break;
                            case 1:
                                var url = window.location.origin + '\\Views\\Income\\Files.aspx';
                                window.location.href = url;
                                break;
                            case 2:
                                radalert("No se puede eliminar el Expediente Contable en cuestión debido a que no se ha encontrado en la BD.", 330, 140, "Imposible eliminar expediente contable", null, null);
                                break;
                            case 3:
                                radalert("No se puede eliminar el Expediente Contable en cuestión debido a que no se ha creado aún en la BD.", 330, 140, "Imposible eliminar expediente contable", null, null);
                                break;
                        }
                    }, error: function (xhr, ajaxOptions, thrownError) {
                        radalert("No se puede eliminar el Expediente Contable en cuestión debido a un error inesperado.", 330, 140, "Imposible eliminar expediente contable", null, null);
                    }
                });

                //PageMethods.DeleteAccountingRecord(OnDeleteAccountingRecordSuccess);
            }

            var radLoadingPanel = null;
            var currentUpdatedControl = null;


            $(document)
                .ajaxStart(function () {
                    radLoadingPanel = $find("ralPrincipal");
                    currentUpdatedControl = "section_income";
                    radLoadingPanel.show(currentUpdatedControl);
                })
                .ajaxStop(function () {
                    if (radLoadingPanel != null) {
                        radLoadingPanel.hide(currentUpdatedControl);
                    }
                    radLoadingPanel = null;
                    currentUpdatedControl = null;
                });


        </script>
    </telerik:RadScriptBlock>

</asp:Content>
