<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="ManageSpendRecord.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Spend.ManageSpendRecord" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <%--Modales--%>
    <telerik:RadWindowManager ID="rwmManageSpendRecord" runat="server">
        <Windows>

            <%--aplicaciones de documentos--%>
            <telerik:RadWindow ID="rwUpdateApplications"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Aplicaciones"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="500"
                Height="600"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientUpdateApplicationsCloseHandler">
            </telerik:RadWindow>

            <%--descuentos de ingresos--%>
            <telerik:RadWindow ID="rwUpdateIncomeDiscounts"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Descuentos Ingresos"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="500"
                Height="600"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientUpdateIncomeDiscountsCloseHandler">
            </telerik:RadWindow>

            <%--descuentos extrapresupuestarios--%>
            <telerik:RadWindow ID="rwUpdateExtraBudgetaryDiscounts"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Descuentos Extrapresupuestarios"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="600"
                Height="600"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientUpdateExtraBudgetaryDiscountsCloseHandler">
            </telerik:RadWindow>

            <%--estado expediente--%>
            <telerik:RadWindow ID="rwSeeStatusRecord"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Estado Expediente"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="400"
                Height="375"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientSeeStatusRecordCloseHandler">
            </telerik:RadWindow>

            <%--proveedores expediente--%>
            <telerik:RadWindow ID="rwSeeProviders"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Proveedores del Expediente Contable"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="400"
                Height="400"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientSeeProvidersCloseHandler">
            </telerik:RadWindow>

            <%--proveedores plurianuales--%>
            <telerik:RadWindow ID="rwSeeMultiYears"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Ver Plurianuales"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="400"
                Height="400"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientSeeMultiYearsCloseHandler">
            </telerik:RadWindow>

            <%--apuntes de tesoreria--%>
            <telerik:RadWindow ID="rwSeeNotesTreasuries"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Apuntes de Tesorería"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="700"
                Height="600"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientSeeNotesTreasuriesCloseHandler">
            </telerik:RadWindow>

            <%--facturas de compras--%>
            <telerik:RadWindow ID="rwSeePurchaseBills"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Facturas Compras"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="800"
                Height="550"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientSeePurchaseBillsCloseHandler">
            </telerik:RadWindow>

            <%--Certificado--%>
            <telerik:RadWindow ID="rwCertificateFilter"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Certificado"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="400"
                Height="230"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientCertificateFilterCloseHandler">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <!-- Page Content -->
    <div id="section_spend" class="container" style="height: calc(100vh - 60px) !important">

        <h3 id="titleHeader" runat="server">Consulta/Modif. Expediente</h3>

        <div id="page_manageSpendRecord" class="box-block">

            <telerik:RadAjaxPanel ID="rapManageSpendRecord" runat="server"
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


                <%--MANAGE--%>
                <div class="manage">

                    <div class="form-group form-group-fake">

                        <%--Año Ejercicio--%>
                        <div class="field-container">
                            <span>Año Ejer.:</span>
                            <strong id="TxtExerciseYear" runat="server"></strong>
                        </div>

                        <%--N⁰ Exp Admin--%>
                        <div class="field-container">
                            <span>N⁰ Exp. Admin.:</span>
                            <strong id="TxtRecordNumber" runat="server"></strong>
                        </div>

                        <%--Procedencia--%>
                        <div class="field-container">
                            <span>Procedencia Gasto:</span>
                            <strong id="TxtProvenance" runat="server"></strong>
                        </div>

                        <%--Año Presupuesto--%>
                        <div class="field-container">
                            <span>Año Presup.:</span>
                            <strong id="TxtBudgetYear" runat="server"></strong>
                        </div>

                        <%--N⁰ Exp Cont--%>
                        <div class="field-container">
                            <span>N⁰ Exp. Cont.:</span>
                            <strong id="TxtAccountingNumber" runat="server"></strong>
                        </div>

                        <%--Descripcion--%>
                        <div class="field-container">
                            <span>Descripcion:</span>
                            <strong id="TxtDescription" runat="server"></strong>
                        </div>
                    </div>

                    <div class="form-group form-group-fake">

                        <%--Area Orig Pago--%>
                        <div class="field-container">
                            <span>Área Orig. Pago:</span>
                            <telerik:RadComboBox ID="RcAreas"
                                runat="server"
                                Width="175px"
                                AutoPostBack="False"
                                DataTextField="DisplayLabelArea"
                                DataValueField="CEN_CODIGO">
                            </telerik:RadComboBox>
                        </div>

                        <%--Proveedor--%>
                        <div class="field-container">
                            <span>Proveedor:</span>
                            <telerik:RadComboBox ID="RcProviders"
                                runat="server"
                                Width="300px"
                                AutoPostBack="True"
                                DataTextField="PROV_NOMBRE"
                                DataValueField="PROV_CODIGO"
                                OnSelectedIndexChanged="RcProviders_OnSelectedIndexChanged">
                            </telerik:RadComboBox>
                        </div>

                        <%--Ordinal Pagador--%>
                        <div class="field-container">
                            <span>Ordinal Pagador:</span>
                            <telerik:RadComboBox ID="RcRestrictedAccount"
                                runat="server"
                                Width="175px"
                                DataValueField="CUE_CODIGO"
                                DataTextField="DisplayLabel">
                            </telerik:RadComboBox>
                        </div>

                        <%--Clas Org--%>
                        <%--<div class="field-container">
                    <span>Clas. Org.:</span>
                    <telerik:RadTextBox ID="TxtOrgClassification"
                        runat="server"
                        Width="100px"
                        MaxLength="5"
                        Text="">
                    </telerik:RadTextBox>
                </div>--%>

                        <%--Clas Func--%>
                        <div class="field-container">
                            <span>Clas. Func.:</span>
                            <telerik:RadComboBox ID="RcPrograms"
                                runat="server"
                                AutoPostBack="false"
                                DataTextField="PRO_NUMERO"
                                DataValueField="PRO_CODIGO_AUX"
                                Width="130px">
                            </telerik:RadComboBox>
                        </div>

                        <%--Cuad--%>
                        <div class="field-container">
                            <span>Cuad.:</span>
                            <telerik:RadCheckBox ID="RcbSquare"
                                runat="server"
                                Checked="False"
                                Text=""
                                AutoPostBack="false">
                            </telerik:RadCheckBox>
                        </div>

                        <%--Plurianual--%>
                        <div class="field-container">
                            <span>Plurianual:</span>
                            <telerik:RadCheckBox ID="RcbMultiYear"
                                runat="server"
                                Checked="False"
                                Text=""
                                AutoPostBack="false">
                            </telerik:RadCheckBox>
                        </div>
                    </div>

                    <%--BUTTONS--%>
                    <div class="form-group buttons">
                        <div class="save">
                            <span class="icon"></span>
                            <telerik:RadButton ButtonType="LinkButton" ID="btnSave"
                                runat="server"
                                RenderMode="Native"
                                Text="Grabar"
                                AutoPostBack="True"
                                OnClick="btnSave_OnClick">
                            </telerik:RadButton>
                        </div>

                        <div class="delete">
                            <span class="icon"></span>
                            <telerik:RadButton ButtonType="LinkButton" ID="btnDelete"
                                runat="server"
                                RenderMode="Native"
                                Text="Eliminar"
                                AutoPostBack="False"
                                OnClientClicked="btnDeleteOnClientClicked">
                            </telerik:RadButton>
                        </div>

                        <div class="save">
                            <span class="icon"></span>
                            <telerik:RadButton ButtonType="LinkButton" ID="btnSee"
                                runat="server"
                                RenderMode="Native"
                                Text="Ver"
                                AutoPostBack="True"
                                OnClick="btnSee_OnClick">
                            </telerik:RadButton>
                        </div>

                        <div class="save">
                            <span class="icon"></span>
                            <telerik:RadButton ButtonType="LinkButton" ID="btnProviders"
                                runat="server"
                                RenderMode="Native"
                                Text="Prov. del Exp."
                                AutoPostBack="True"
                                OnClick="btnProviders_OnClick">
                            </telerik:RadButton>
                        </div>

                        <div class="back">
                            <span class="icon"></span>
                            <telerik:RadButton ButtonType="LinkButton" ID="btnBack"
                                runat="server"
                                RenderMode="Native"
                                Text="Volver"
                                AutoPostBack="False"
                                OnClientClicked="backAction">
                            </telerik:RadButton>
                        </div>
                    </div>
                </div>

                <div class="field-document">
                    <div class="new-document">
                        <asp:LinkButton ID="btnIncomeDiscounts"
                            runat="server"
                            Text="Descuentos Ingresos"
                            OnClick="btnIncomeDiscounts_OnClick">
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnExtraBudgetaryDiscounts"
                            runat="server"
                            Text="Descuentos Extrap."
                            OnClick="btnExtraBudgetaryDiscounts_OnClick">
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnSeeStatusRecord"
                            runat="server"
                            Text="Estado Expediente"
                            OnClick="btnSeeStatusRecord_OnClick">
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnSeeNotesTreasuries"
                            runat="server"
                            Text="Ver A. Tesorería"
                            OnClick="btnSeeNotesTreasuries_OnClick">
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnSeePurchaseBills"
                            runat="server"
                            Text="Facturas de Compras"
                            OnClick="btnPurchaseBills_OnClick">
                        </asp:LinkButton>
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
                        CssClass="manageSpendRecords-table">

                        <GroupingSettings CaseSensitive="false" />

                        <MasterTableView AutoGenerateColumns="false"
                            AllowFilteringByColumn="true"
                            DataKeyNames="DOC_CODIGO, TIPD_CODIGO, DOC_FECHA_PROPUESTA, DOC_FECHA_ASIENTO_DIARIO, DOC_DESCRIPCION, CUE_CODIGO, DOC_ENLAZADO_TESORERIA, HOJ_NUMERO, ANO_HOJA, HOJ_NUMERO50, ANO_HOJA50, DOC_NUMERO_CHEQUE, TIPP_CODIGO, FOR_CODIGO, SEN_NUMERO, TES_CODIGO, SEN_CODIGO, TIPO_DOC"
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
                                AddNewRecordText="Nuevo" />

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
                                <telerik:GridBoundColumn UniqueName="DOC_DESCRIPCION"
                                    DataField="DOC_DESCRIPCION"
                                    HeaderText="Texto"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="DOC_FECHA_PROPUESTA"
                                    DataField="DOC_FECHA_PROPUESTA"
                                    HeaderText="F. Propuesta"
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
                                    HeaderText="F. Asiento"
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
                                <telerik:GridBoundColumn UniqueName="DOC_NUMERO_CHEQUE"
                                    DataField="DOC_NUMERO_CHEQUE"
                                    HeaderText="N⁰ Cheque"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="75px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="DOC_FACTURA"
                                    DataField="DOC_FACTURA"
                                    HeaderText="Factura/s"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="100px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="SEN_NUMERO"
                                    DataField="SEN_NUMERO"
                                    HeaderText="N⁰ Señ."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="75px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ORDINAL_PAGADOR"
                                    DataField="ORDINAL_PAGADOR"
                                    HeaderText="Ordinal Pagador"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="FOR_DESCRIPCION"
                                    DataField="FOR_DESCRIPCION"
                                    HeaderText="Forma Pago"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="100px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="TIPP_DESCRIPCION"
                                    DataField="TIPP_DESCRIPCION"
                                    HeaderText="Tipo Pago"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="100px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn UniqueName="DOC_ENLAZADO_TESORERIA"
                                    DataField="DOC_ENLAZADO_TESORERIA"
                                    DataType="System.Boolean"
                                    HeaderText="Tes."
                                    StringFalseValue="False"
                                    StringTrueValue="True"
                                    AllowFiltering="false">
                                    <HeaderStyle Width="30px" />
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridBoundColumn UniqueName="HOJ_NUMERO"
                                    DataField="HOJ_NUMERO"
                                    HeaderText="H. Arq."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="75px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="HOJ_NUMERO50"
                                    DataField="HOJ_NUMERO50"
                                    HeaderText="H. Arq. 50"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="75px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ANO_HOJA"
                                    DataField="ANO_HOJA"
                                    HeaderText="Año H."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="70px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ANO_HOJA50"
                                    DataField="ANO_HOJA50"
                                    HeaderText="Año H. 50"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="70px" />
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
                                <telerik:GridButtonColumn UniqueName="PrintCertificateColumn"
                                    ButtonType="LinkButton"
                                    HeaderTooltip="Certificado"
                                    CommandName="PrintCertificate"
                                    HeaderStyle-Width="40px"
                                    ItemStyle-Width="40px"
                                    ItemStyle-CssClass="fas fa-certificate-alt"
                                    Text=" "
                                    HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridButtonColumn>
                                <telerik:GridClientSelectColumn UniqueName="SelectColumn">
                                    <HeaderStyle Width="30px" />
                                </telerik:GridClientSelectColumn>
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

                                        <%--Tipo Pago--%>
                                        <div class="field-container field-20">
                                            <span>Tipo Pago:</span>
                                            <telerik:RadComboBox ID="radDropPayType"
                                                runat="server"
                                                Width="100%"
                                                DataValueField="TIPP_CODIGO_AUX"
                                                DataTextField="DisplayLabel">
                                            </telerik:RadComboBox>
                                        </div>

                                        <%--Forma Pago--%>
                                        <div class="field-container field-15">
                                            <span>Forma Pago:</span>
                                            <telerik:RadComboBox ID="radDropPayForm"
                                                runat="server"
                                                Width="100%"
                                                DataValueField="FOR_CODIGO_AUX"
                                                DataTextField="DisplayLabel">
                                            </telerik:RadComboBox>
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

                                        <%--Año Hoja Arqueo--%>
                                        <div class="field-container field-15">
                                            <span>Año H. A.:</span>
                                            <telerik:RadMonthYearPicker ID="rmyTonnageSheetYear"
                                                runat="server"
                                                Width="80px"
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

                                        <%--Año Hoja Arqueo 50--%>
                                        <div class="field-container field-15">
                                            <span>Año H. A. 50:</span>
                                            <telerik:RadMonthYearPicker ID="rmyTonnageSheet50Year"
                                                runat="server"
                                                Width="80px"
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

                                        <%--Factura/s--%>
                                        <div class="field-container field-20">
                                            <span>Factura/s:</span>
                                            <telerik:RadTextBox ID="txtSing"
                                                Width="100%"
                                                runat="server"
                                                MaxLength="75"
                                                Text="">
                                            </telerik:RadTextBox>
                                        </div>
                                    </div>

                                    <%--BUTTONS--%>
                                    <div class="buttons">

                                        <asp:LinkButton ID="btnIncomeUpdate"
                                            runat="server"
                                            ValidationGroup="IncomeApplicationGroup"
                                            CssClass="Button Add"
                                            CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
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
                            <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True" />
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

            function hideGrids() {
                $('#page_manageCreditModification').hide();
            }

            function moveNewButtons() {
                var $this = $("a[title*='Nuevo']");
                $this.each(function () {
                    var $destino = $(this).closest('.field-document').children(".new-document");
                    //$(this).appendTo($destino).show();
                    $($destino).prepend(this).show();
                    $($destino).prepend("<span> Documentos:</span>");
                    //$destino.has("a").length && $(this).appendTo($destino).show();
                });
            }

            var itemIndex;
            function deleteSpendRecord(index, documentType) {
                itemIndex = index;
                radconfirm("¿ Desea realmente eliminar el Documento Contable " + documentType + " ?<br/><br/>Se eliminarán todos los datos asociados a dicho documento:<br/>- Aplicaciones asociadas y sus importes.",
                    confirmDeleteSpendRecordCallBackFn,
                    500,
                    220,
                    null,
                    "Confirmación",
                    null);
            }

            function confirmDeleteSpendRecordCallBackFn(arg) {
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
                var manager = $find("<%= this.rwmManageSpendRecord.ClientID %>");
                var oWnd = manager.open(url, "rwUpdateApplications");
            }

            function OnClientUpdateApplicationsCloseHandler(sender, args) {
                location.reload(true);
            }

            function updateIncomeDiscounts(documentId, year, fileId, proposal) {
                var url = "UpdateIncomeDiscounts.aspx?documentId=" + documentId + "&year=" + year + "&fileId=" + fileId + "&proposal=" + proposal;
                var manager = $find("<%= this.rwmManageSpendRecord.ClientID %>");
                var oWnd = manager.open(url, "rwUpdateIncomeDiscounts");
            }

            function OnClientUpdateIncomeDiscountsCloseHandler(sender, args) {
                location.reload(true);
            }

            function updateExtraBudgetaryDiscounts(documentId, fileId, year, annualFileId) {
                var url = "UpdateExtraBudgetaryDiscounts.aspx?documentId=" + documentId + "&fileId=" + fileId + "&year=" + year + "&annualFileId=" + annualFileId;
                var manager = $find("<%= this.rwmManageSpendRecord.ClientID %>");
                var oWnd = manager.open(url, "rwUpdateExtraBudgetaryDiscounts");
            }

            function OnClientUpdateExtraBudgetaryDiscountsCloseHandler(sender, args) {
                location.reload(true);
            }

            function seeStatusRecord(fileId) {
                var url = "SeeStatusRecord.aspx?fileId=" + fileId;
                var manager = $find("<%= this.rwmManageSpendRecord.ClientID %>");
                var oWnd = manager.open(url, "rwSeeStatusRecord");
            }

            function OnClientSeeStatusRecordCloseHandler(sender, args) {
                location.reload(true);
            }

            function seeProviders(fileId) {
                var url = "SeeProviders.aspx?fileId=" + fileId;
                var manager = $find("<%= this.rwmManageSpendRecord.ClientID %>");
                var oWnd = manager.open(url, "rwSeeProviders");
            }

            function OnClientSeeProvidersCloseHandler(sender, args) {
                location.reload(true);
            }

            function seeMultiYears(fileId) {
                var url = "SeeMultiYears.aspx?fileId=" + fileId;
                var manager = $find("<%= this.rwmManageSpendRecord.ClientID %>");
                var oWnd = manager.open(url, "rwSeeMultiYears");
            }

            function OnClientSeeMultiYearsCloseHandler(sender, args) {
                location.reload(true);
            }

            function seeNotesTreasuries(documentId, treasuryId) {
                var url = "SeeNotesTreasuries.aspx?documentId=" + documentId + "&treasuryId=" + treasuryId;
                var manager = $find("<%= this.rwmManageSpendRecord.ClientID %>");
                var oWnd = manager.open(url, "rwSeeNotesTreasuries");
            }

            function OnClientSeeNotesTreasuriesCloseHandler(sender, args) {
                location.reload(true);
            }

            function seePurchaseBills(documentId, fileId, administrativeId, description, budgetYear, yearNumber) {
                var url = "SeePurchaseBills.aspx?documentId=" + documentId + "&fileId=" + fileId + "&administrativeId=" + administrativeId + "&description=" + description + "&budgetYear=" + budgetYear + "&yearNumber=" + yearNumber;
                var manager = $find("<%= this.rwmManageSpendRecord.ClientID %>");
                var oWnd = manager.open(url, "rwSeePurchaseBills");
            }

            function OnClientSeePurchaseBillsCloseHandler(sender, args) {
                location.reload(true);
            }

            function showCertificateReport(documentId) {
                var url = "CertificateFilter.aspx?documentId=" + documentId;
                var manager = $find("<%= this.rwmManageSpendRecord.ClientID %>");
                var oWnd = manager.open(url, "rwCertificateFilter");
            }

            function OnClientCertificateFilterCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    var url1 = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=spendCertificate&documentId=' + data.documentId + '&name=' + data.name + '&ocupation=' + data.ocupation;

                    window.open(url1, "Reporte");
                }
            }

            function btnDeleteOnClientClicked(sender, eventArgs) {
                var grid = $find("<%= this.RgDocuments.ClientID %>");
                var count = grid.get_masterTableView().get_dataItems().length;
                if (count > 0) {
                    radalert("No se puede eliminar el expediente, primero elimine los documentos contables.", 330, 140, "Imposible eliminar expediente contable", null, null);
                    return;
                }

                radconfirm("¿ Está seguro que desea eliminar el expediente de gasto en cuestión ?",
                    confirmDeleteCallBackFn,
                    330,
                    140,
                    null,
                    "Confirmación",
                    null);
            }

            function confirmDeleteCallBackFn(arg) {
                if (arg == null || arg == false) {
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: "ManageSpendRecord.aspx/DeleteSpendRecord",
                    data: null,
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    success: function (result) {
                        switch (result.d) {
                            case 0:
                                radalert("Ha ocurrido un error eliminando el Expediente Contable en cuestión.", 330, 140, "Imposible eliminar expediente contable", null, null);
                                break;
                            case 1:
                                var url = window.location.origin + '\\Views\\Spend\\SpendRecords.aspx';
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
            }

            function showReport(page1, page2, documentId) {
                var url1 = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=' + page1 + '&documentId=' + documentId;

                window.open(url1, "Reporte Gastos");

                if (page2 !== "") {
                    var url2 = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=' + page2 + '&documentId=' + documentId;

                    window.open(url2, "Reporte Gastos 2");
                }
            }
            
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
