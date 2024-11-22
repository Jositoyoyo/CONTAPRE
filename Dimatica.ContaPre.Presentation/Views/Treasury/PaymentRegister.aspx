<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="PaymentRegister.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Treasury.PaymentRegister" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <!-- Page Content -->
    <div id="section_treasury" class="container">

        <h3>Alta Registro Pagos</h3>

        <%--BUTTONS--%>
        <div class="top-buttons">
            <div class="a-buttons">
                <div class="save">
                    <span class="icon"></span>
                    <asp:LinkButton ID="btnSave"
                        runat="server"
                        Text="Grabar"
                        OnClick="btnSave_OnClick">
                    </asp:LinkButton>
                </div>
                <div class="find">
                    <span class="icon"></span>
                    <asp:LinkButton ID="btnFind"
                        runat="server"
                        OnClick="btnFind_Click"
                        Text="Buscar">
                    </asp:LinkButton>
                </div>
                <div class="delete">
                    <span class="icon"></span>
                    <asp:LinkButton ID="btnClean"
                        runat="server"
                        OnClick="btnClean_Click"
                        Text="Limpiar">
                    </asp:LinkButton>
                </div>
            </div>
        </div>

        <div id="page_paymentRegister" class="box-block">
            <telerik:RadAjaxPanel ID="rapPaymentRegister"
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
                                            <span>Año:</span>
                                            <telerik:RadMonthYearPicker ID="RmyYear"
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
                                            <span>Tipo: </span>
                                            <telerik:RadComboBox ID="RcPayType"
                                                runat="server"
                                                DataValueField="ORI_CODIGO_AUX"
                                                DataTextField="ORI_DESCRIPCION">
                                            </telerik:RadComboBox>
                                        </div>

                                        <div class="field-container">
                                            <span>Tercero (I): </span>
                                            <telerik:RadComboBox ID="RcProvidersIncomes"
                                                runat="server"
                                                Width="250px"
                                                AutoPostBack="False"
                                                DataTextField="PROV_NOMBRE"
                                                DataValueField="PROV_CODIGO">
                                            </telerik:RadComboBox>
                                        </div>

                                        <div class="field-container">
                                            <span>Interesado (G): </span>
                                            <telerik:RadComboBox ID="RcProvidersSpends"
                                                runat="server"
                                                Width="250px"
                                                AutoPostBack="False"
                                                DataTextField="PROV_NOMBRE"
                                                DataValueField="PROV_CODIGO">
                                            </telerik:RadComboBox>
                                        </div>

                                        <div class="field-container">
                                            <span>Fecha entre: </span>
                                            <telerik:RadDatePicker ID="RdpSinceDate"
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
                                            <telerik:RadDatePicker ID="RdpUntilDate"
                                                RenderMode="Lightweight"
                                                runat="server"
                                                Width="150px"
                                                AutoPostBack="False"
                                                Culture="es-ES"
                                                EnableTyping="True"
                                                MaxDate="12/31/9999"
                                                MinDate="01/01/1800" />
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>

                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgTonnageSheet"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgTonnageSheet_NeedDataSource"
                    OnPreRender="RgTonnageSheet_PreRender"
                    OnInsertCommand="RgTonnageSheet_InsertCommand"
                    OnUpdateCommand="RgTonnageSheet_UpdateCommand"
                    OnDeleteCommand="RgTonnageSheet_DeleteCommand"
                    OnItemDataBound="RgTonnageSheet_ItemDataBound"
                    AllowMultiRowSelection="True"
                    CssClass="paymentRegister-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="EXP_CODIGO, PROCEDENCIA, CODIGO_DOCUMENTO, CODIGO_EXP_EXTRAP, DOCUMENTO_APLICACION, PROV_CODIGO, PROV_NOMBRE, DOC_NUMERO_CHEQUE, LIQUIDO, NUMERO_EXPEDIENTE, ORIGEN, DOC_ENLAZADO_TESORERIA, DOC_FECHA_PROPUESTA"
                        CommandItemDisplay="Top"
                        AllowPaging="true"
                        PagerStyle-AlwaysVisible="true"
                        PageSize="100"
                        NoMasterRecordsText="No Hay datos a Mostrar."
                        TableLayout="Fixed">

                        <CommandItemSettings AddNewRecordText="Nuevo"
                            ShowAddNewRecordButton="False"
                            ShowRefreshButton="False"
                            ShowExportToExcelButton="False"
                            ShowExportToPdfButton="False" />

                        <PagerStyle Mode="NextPrevAndNumeric"
                            PageSizeLabelText="Elementos por pagina: "
                            PagerTextFormat="Navigate pages {4} Página {0} de {1}, elementos {2} a {3} de {5}" />

                        <Columns>
                            <telerik:GridBoundColumn UniqueName="PROCEDENCIA"
                                DataField="PROCEDENCIA"
                                HeaderText="Proc."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="50px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="DOC_FECHA_PROPUESTA"
                                DataField="DOC_FECHA_PROPUESTA"
                                HeaderText="F. Propuesta"
                                AllowFiltering="true"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                DataFormatString="{0:dd/MM/yyyy}"
                                FilterControlWidth="90%">
                                <FilterTemplate>
                                    <telerik:RadDatePicker ID="FilteredDatePickerOperation"
                                        RenderMode="Lightweight"
                                        runat="server"
                                        Width="100%"
                                        ClientEvents-OnDateSelected="DateSelectedOperation"
                                        DbSelectedDate='<%#this.SetFilteredDate(Container,"DOC_FECHA_PROPUESTA") %>'
                                        Culture="es-ES" />
                                    <telerik:RadScriptBlock ID="RadScriptBlockOperation"
                                        runat="server">
                                        <script id="scriptOperation"
                                            type="text/javascript">
                                            function DateSelectedOperation(sender, args) {
                                                var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                var date = FormatSelectedDateOperation(sender);
                                                tableView.filter("DOC_FECHA_PROPUESTA", date, "EqualTo");
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
                            <telerik:GridBoundColumn UniqueName="NUMERO_EXPEDIENTE"
                                DataField="NUMERO_EXPEDIENTE"
                                HeaderText="N⁰ Exp."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="GreaterThanOrEqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="50px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="DOCUMENTO_APLICACION"
                                DataField="DOCUMENTO_APLICACION"
                                HeaderText="Aplic./Doc."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PROV_NOMBRE"
                                DataField="PROV_NOMBRE"
                                HeaderText="Tercero/Interesado"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn UniqueName="AmountLabel"
                                DataField="AmountLabel"
                                HeaderText="Importe"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right">
                                <HeaderStyle Width="100" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridNumericColumn UniqueName="LIQUIDO"
                                DataField="LIQUIDO"
                                HeaderText="Importe"
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
                                HeaderText="N⁰ Cheque/Talón"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridClientSelectColumn UniqueName="SelectColumn"
                                HeaderText="Alta">
                                <HeaderStyle Width="40px" />
                            </telerik:GridClientSelectColumn>
                        </Columns>
                    </MasterTableView>

                    <ClientSettings>
                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                        <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True" />
                    </ClientSettings>
                </telerik:RadGrid>

                <div class="manage">
                    <div class="form-group form-group-fake">
                        <div style="display: flex">
                            <span>Fecha:</span>
                            <telerik:RadDatePicker ID="RdDate"
                                RenderMode="Lightweight"
                                runat="server"
                                Width="150px"
                                EnableTyping="False"
                                AutoPostBack="False"
                                Culture="es-ES"
                                MaxDate="12/31/9999"
                                MinDate="01/01/1800" />
                        </div>

                        <div style="display: flex">
                            <span>Agrupar:</span>
                            <telerik:RadCheckBox ID="RcbGroup"
                                runat="server"
                                Checked="False"
                                Text=""
                                AutoPostBack="false">
                            </telerik:RadCheckBox>
                        </div>

                        <div class="field-container buttons">
                            <telerik:RadButton ButtonType="LinkButton" ID="btnReport"
                                runat="server"
                                RenderMode="Native"
                                Text="Generar A. Tesorería"
                                AutoPostBack="True"
                                OnClick="btnReport_OnClick">
                            </telerik:RadButton>
                        </div>
                    </div>
                </div>
            </telerik:RadAjaxPanel>

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

                </script>
            </telerik:RadScriptBlock>

        </div>
    </div>
</asp:Content>
