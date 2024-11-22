<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="ExtraBudgetaries.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.ExtraBudgetary.ExtraBudgetaries" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <%--Modales--%>
    <telerik:RadWindowManager ID="rwmExtraBudgetaries" runat="server">
        <Windows>

            <%--estado expediente--%>
            <telerik:RadWindow ID="rwSeeApplication"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Estado Aplicación"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="400"
                Height="225"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <!-- Page Content -->
    <div id="section_extraBudgetary" class="container">

        <h3>Listado de Expedientes</h3>

        <%--BUTTONS--%>
        <div class="top-buttons">
            <div class="a-buttons">
                <div class="new">
                    <span class="icon"></span>
                    <a href="ManageExtraBudgetary.aspx" role="button">Nuevo</a>
                </div>
                <div class="find">
                    <span class="icon"></span>
                    <asp:LinkButton ID="btnFind"
                        runat="server"
                        OnClick="btnFind_OnClick"
                        Text="Buscar"></asp:LinkButton>
                </div>
                <div class="report">
                    <span class="icon"></span>
                    <asp:LinkButton ID="btnReport"
                        runat="server"
                        OnClientClick="btnReportOnClientClick();return false;"
                        Text="Listado"></asp:LinkButton>
                </div>
                <div class="list">
                    <span class="icon"></span>
                    <%--     <asp:LinkButton ID="btnReport"
                        runat="server"
                        OnClick="btnReport_OnClick"
                        Text="Estado Aplicación"></asp:LinkButton>--%>

                    <a onclick="seeStatusApplication()" role="button">Est. Aplic.</a>
                </div>
            </div>
        </div>

        <div id="page_extraBudgetaries" class="box-block">
            <telerik:RadAjaxPanel ID="rapExtraBudgetaries"
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
                                                AutoPostBack="False"
                                                Width="100px"
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
                                            <span>Tipo:</span>
                                            <telerik:RadComboBox ID="RcExtraBudgetaryTypes"
                                                runat="server"
                                                Width="250px"
                                                AutoPostBack="False"
                                                DataTextField="TIP_EXTRAP_DESCRIPCION"
                                                DataValueField="TIP_EXTRAP_CODIGO_AUX">
                                            </telerik:RadComboBox>
                                        </div>
                                        <div class="field-container">
                                            <span>Aplic. Extrapresup.:</span>
                                            <telerik:RadComboBox ID="RcExtraBudgetaryApplications"
                                                runat="server"
                                                Width="250px"
                                                AutoPostBack="False"
                                                DataTextField="DisplayLabel"
                                                DataValueField="EXTRAPRE_CODIGO">
                                            </telerik:RadComboBox>
                                        </div>
                                        <div class="field-container">
                                            <span>Enlazado:</span>
                                            <telerik:RadComboBox ID="RcBound"
                                                runat="server"
                                                Width="130px"
                                                AutoPostBack="False">
                                                <Items>
                                                    <telerik:RadComboBoxItem runat="server" Text="< Seleccione >" Value="" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Si" Value="1" />
                                                    <telerik:RadComboBoxItem runat="server" Text="No" Value="0" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </div>
                                        <div class="field-container">
                                            <span>Fecha entre:</span>
                                            <telerik:RadDatePicker ID="RdSince"
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
                                            <telerik:RadDatePicker ID="RdUntil"
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
                                            <span>N⁰ Exped. entre:</span>
                                            <telerik:RadNumericTextBox ID="RntSince"
                                                runat="server"
                                                RenderMode="Lightweight"
                                                Width="75px"
                                                MinValue="0"
                                                MaxValue="9999"
                                                MaxLength="4"
                                                ShowSpinButtons="False"
                                                NumberFormat-DecimalDigits="0">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="field-container">
                                            <span>y:</span>
                                            <telerik:RadNumericTextBox ID="RntUntil"
                                                runat="server"
                                                RenderMode="Lightweight"
                                                Width="75px"
                                                MinValue="0"
                                                MaxValue="9999"
                                                MaxLength="4"
                                                ShowSpinButtons="False"
                                                NumberFormat-DecimalDigits="0">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <%--        <div class="field-container">
                                            <span>Ordinal Pagador:</span>
                                            <telerik:RadComboBox ID="RcProviders"
                                                runat="server"
                                                Width="250px"
                                                AutoPostBack="False"
                                                DataTextField="PROV_NOMBRE"
                                                DataValueField="PROV_CODIGO">
                                            </telerik:RadComboBox>
                                        </div>--%>
                                        <%--              <div class="field-container">
                                            <span>Operación:</span>
                                            <telerik:RadComboBox ID="RcOperationType"
                                                runat="server"
                                                Width="130px"
                                                AutoPostBack="False">
                                                <Items>
                                                    <telerik:RadComboBoxItem runat="server" Text="< Seleccione >" Value="" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Gastos" Value="G" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Ingresos" Value="I" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </div>--%>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>

                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgFiles"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgFiles_OnNeedDataSource"
                    OnPreRender="RgFiles_OnPreRender"
                    OnItemCommand="RgFiles_OnItemCommand"
                    OnItemDataBound="RgFiles_OnItemDataBound"
                    CssClass="extraBudgetary-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="EXP_EXTRAP_CODIGO"
                        CommandItemDisplay="Top"
                        AllowPaging="true"
                        PagerStyle-AlwaysVisible="true"
                        PageSize="100"
                        NoMasterRecordsText="No Hay datos a Mostrar."
                        TableLayout="Fixed"
                        EditMode="PopUp"
                        CssClass="popup-table normal-table">

                        <CommandItemSettings ShowRefreshButton="False"
                            ShowExportToExcelButton="False"
                            ShowExportToPdfButton="False"
                            ShowAddNewRecordButton="False" />

                        <PagerStyle Mode="NextPrevAndNumeric"
                            PageSizeLabelText="Elementos por pagina: "
                            PagerTextFormat="Navigate pages {4} Página {0} de {1}, elementos {2} a {3} de {5}" />

                        <Columns>
                            <telerik:GridBoundColumn UniqueName="EXP_EXTRAP_NUMERO"
                                DataField="EXP_EXTRAP_NUMERO"
                                HeaderText="N⁰"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                DataType="System.Int32">
                                <HeaderStyle Width="50px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="EXP_EXTRAP_ANO_PRESUPUESTO"
                                DataField="EXP_EXTRAP_ANO_PRESUPUESTO"
                                HeaderText="Año"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="EXTRAPRE_NUMERO"
                                DataField="EXTRAPRE_NUMERO"
                                HeaderText="Aplic."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                DataType="System.Int32">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="EXP_EXTRAP_FECHA"
                                DataField="EXP_EXTRAP_FECHA"
                                HeaderText="Fecha"
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
                                        DbSelectedDate='<%#this.SetFilteredDate(Container,"EXP_EXTRAP_FECHA") %>'
                                        Culture="es-ES" />
                                    <telerik:RadScriptBlock ID="RadScriptBlockOperation"
                                        runat="server">
                                        <script id="scriptOperation"
                                            type="text/javascript">
                                            function DateSelectedOperation(sender, args) {
                                                var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                var date = FormatSelectedDateOperation(sender);
                                                tableView.filter("EXP_EXTRAP_FECHA", date, "GreaterThanOrEqualTo");
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
                            <telerik:GridNumericColumn UniqueName="AmountLabel"
                                DataField="EXP_EXTRAP_IMPORTE"
                                HeaderText="Importe"
                                DataFormatString="{0:N}"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right"
                                FilterControlWidth="100%">
                                <HeaderStyle Width="150px" />
                            </telerik:GridNumericColumn>
                            <%--<telerik:GridBoundColumn UniqueName="AmountLabel"
                                DataField="AmountLabel"
                                HeaderText="Importe"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn UniqueName="TIPO_DOC"
                                DataField="TIPO_DOC"
                                HeaderText="Tipo"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ORDINAL_PAGADOR"
                                DataField="ORDINAL_PAGADOR"
                                HeaderText="Ord. Pag."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="INTERESADO"
                                DataField="INTERESADO"
                                HeaderText="Interesado"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="EXP_NUM_EXP_CONTABLE_ANUAL"
                                DataField="EXP_NUM_EXP_CONTABLE_ANUAL_LABEL"
                                HeaderText="Proc."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TERCERO"
                                DataField="TERCERO"
                                HeaderText="Tercero"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn UniqueName="EditColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Editar expediente"
                                CommandName="UpdateExtraBudgetary"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-pencil-alt"
                                Text=" "
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>
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

            function GetGridServerElement(serverId, tagName) {
                if (!tagName) {
                    tagName = "*";
                }

                var grid = $get("<%=this.RgFiles.ClientID %>");
                var elements = grid.getElementsByTagName(tagName);
                for (var i = 0; i < elements.length; i++) {
                    var element = elements[i];
                    if (element.id.indexOf(serverId) >= 0) {
                        return element;
                    }
                }
            }

            function btnReportOnClientClick(sender, eventArgs) {
                var yearComp = $find("<%= this.RmyYear.ClientID %>");
                var yearValue = "";
                if (yearComp.get_selectedDate() != null) {
                    yearValue = yearComp.get_selectedDate().getFullYear()
                }

                var extraBudgetaryTypesComp = $find("<%= this.RcExtraBudgetaryTypes.ClientID %>");
                var extraBudgetaryTypesValue = extraBudgetaryTypesComp.get_selectedItem().get_value();

                var extraBudgetaryApplicationsComp = $find("<%= this.RcExtraBudgetaryApplications.ClientID %>");
                var extraBudgetaryApplicationsValue = extraBudgetaryApplicationsComp.get_selectedItem().get_value();

                var boundComp = $find("<%= this.RcBound.ClientID %>");
                var boundValue = boundComp.get_selectedItem().get_value();

                var sinceComp = $find("<%= this.RdSince.ClientID %>");
                var sinceValue = sinceComp.get_selectedDate();

                var untilComp = $find("<%= this.RdUntil.ClientID %>");
                var untilValue = untilComp.get_selectedDate();

                var sinceNumberComp = $find("<%= this.RntSince.ClientID %>");
                var sinceNumberValue = sinceNumberComp.get_value();

                var untilNumberComp = $find("<%= this.RntUntil.ClientID %>");
                var untilNumberValue = untilNumberComp.get_value();

                var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=extraBudgetariesList';

                if (yearValue != "") {
                    url = url + '&exerciseYear=' + yearValue;
                }

                if (extraBudgetaryTypesValue != "-1") {
                    url = url + '&extraBudgetaryTypes=' + extraBudgetaryTypesValue;
                }

                if (extraBudgetaryApplicationsValue != "-1") {
                    url = url + '&extraBudgetaryApplications=' + extraBudgetaryApplicationsValue;
                }

                if (boundValue != "") {
                    url = url + '&isBound=' + boundValue;
                }

                if (sinceValue != null) {
                    url = url + '&since=' + sinceValue;
                }

                if (untilValue != null) {
                    url = url + '&until=' + untilValue;
                }

                if (sinceNumberValue != "") {
                    url = url + '&sinceNumber=' + sinceNumberValue;
                }

                if (untilNumberValue != "") {
                    url = url + '&untilNumber=' + untilNumberValue;
                }

                window.open(url, "Reporte");
            }

            function seeStatusApplication() {
                var yearComp = $find("<%= this.RmyYear.ClientID %>");
                var yearValue = yearComp.get_selectedDate().getFullYear();

                var applicationComp = $find("<%= this.RcExtraBudgetaryApplications.ClientID %>");
                var applicationValue = applicationComp.get_value();
                var applicationText = "TODAS";
                if (applicationValue != "-1") {
                    applicationText = applicationComp.get_text();
                }

                var url = "SeeStatusApplication.aspx?year=" + yearValue + "&application=" + applicationValue + "&applicationText=" + applicationText;
                var manager = $find("<%= this.rwmExtraBudgetaries.ClientID %>");
                var oWnd = manager.open(url, "rwSeeApplication");
            }

        </script>
    </telerik:RadScriptBlock>
</asp:Content>
