<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="StatementAccounts.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Income.StatementAccounts" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <!-- Page Content -->
    <div id="section_income" class="container">

        <h3>Estado de Cuentas Restringidas</h3>

        <%--BUTTONS--%>
        <div class="top-buttons">
            <div class="a-buttons">
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
                                    Text="Imprimir"></asp:LinkButton>
                </div>
            </div>
        </div>

        <div id="page_statementAccounts" class="box-block">
            <telerik:RadAjaxPanel ID="rapStatementAccounts"
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
                                            <span>Ordinal Pagador:</span>
                                            <telerik:RadComboBox ID="RcAccounts"
                                                runat="server"
                                                Width="250px"
                                                AutoPostBack="False"
                                                DataTextField="DisplayDescriptionLabel"
                                                DataValueField="CUE_CODIGO">
                                            </telerik:RadComboBox>
                                        </div>
                                        <div class="field-container">
                                            <span>Fecha apunte entre:</span>
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
                                    </div>
                                </div>
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>

                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgAccounts"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgAccounts_OnNeedDataSource"
                    OnPreRender="RgAccounts_OnPreRender"
                    CssClass="incomeDrRecords-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="DET_CODIGO"
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
                            <telerik:GridBoundColumn UniqueName="DET_FECHA_APUNTE"
                                DataField="DET_FECHA_APUNTE"
                                HeaderText="Fecha Apunte"
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
                                        DbSelectedDate='<%#this.SetFilteredDate(Container,"DET_FECHA_APUNTE") %>'
                                        Culture="es-ES" />
                                    <telerik:RadScriptBlock ID="RadScriptBlockOperation"
                                        runat="server">
                                        <script id="scriptOperation"
                                            type="text/javascript">
                                            function DateSelectedOperation(sender, args) {
                                                var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                var date = FormatSelectedDateOperation(sender);
                                                tableView.filter("DET_FECHA_APUNTE", date, "GreaterThanOrEqualTo");
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
                            <telerik:GridBoundColumn UniqueName="DET_NUMERO_EXPEDIENTE"
                                DataField="DET_NUMERO_EXPEDIENTE"
                                HeaderText="N⁰ Doc."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="DOC_DESCRIPCION"
                                DataField="DOC_DESCRIPCION"
                                HeaderText="Descripción"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn UniqueName="AmountLabel"
                                DataField="AmountLabel"
                                HeaderText="Entrada"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridNumericColumn UniqueName="AmountLabel"
                                DataField="AmountLabel"
                                HeaderText="Entrada"
                                DataFormatString="{0:N}"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right"
                                FilterControlWidth="100%">
                                <HeaderStyle Width="150px" />
                            </telerik:GridNumericColumn>
                            <%--<telerik:GridBoundColumn UniqueName="Amount50Label"
                                DataField="Amount50Label"
                                HeaderText="Salida"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridNumericColumn UniqueName="Amount50Label"
                                DataField="Amount50Label"
                                HeaderText="Salida"
                                DataFormatString="{0:N}"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right"
                                FilterControlWidth="100%">
                                <HeaderStyle Width="150px" />
                            </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn UniqueName="HOJ_NUMERO"
                                DataField="HOJ_NUMERO"
                                HeaderText="H. Arqueo"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="HOJ_FECHA"
                                DataField="HOJ_FECHA"
                                HeaderText="Fecha H.A."
                                AllowFiltering="true"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="GreaterThanOrEqualTo"
                                ShowFilterIcon="false"
                                DataFormatString="{0:dd/MM/yyyy}"
                                FilterControlWidth="90%">
                                <FilterTemplate>
                                    <telerik:RadDatePicker ID="FilteredDatePickerSheet"
                                        RenderMode="Lightweight"
                                        runat="server"
                                        Width="100%"
                                        ClientEvents-OnDateSelected="DateSelectedSheet"
                                        DbSelectedDate='<%#this.SetFilteredDate(Container,"HOJ_FECHA") %>'
                                        Culture="es-ES" />
                                    <telerik:RadScriptBlock ID="RadScriptBlockSheet"
                                        runat="server">
                                        <script id="scriptSheet"
                                            type="text/javascript">
                                            function DateSelectedSheet(sender, args) {
                                                var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                var date = FormatSelectedDateSheet(sender);
                                                tableView.filter("HOJ_FECHA", date, "GreaterThanOrEqualTo");
                                            }
                                            function FormatSelectedDateSheet(picker) {
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

            function btnReportOnClientClick(sender, eventArgs) {
                var exerciseYearComp = $find("<%= this.RmyExerciseYear.ClientID %>");
                var exerciseYearValue = exerciseYearComp.get_selectedDate().getFullYear();

                var budgetYearComp = $find("<%= this.RmyBudgetYear.ClientID %>");
                var budgetYearDate = budgetYearComp.get_selectedDate();
                var budgetYearValue = null;
                if (budgetYearDate != null) {
                    budgetYearValue = budgetYearDate.getFullYear();
                }

                var accountsComp = $find("<%= this.RcAccounts.ClientID %>");
                var accountsValue = accountsComp.get_selectedItem().get_value();
                var accountsLabelValue = accountsComp.get_selectedItem().get_text();

                var sinceComp = $find("<%= this.RdSince.ClientID %>");
                var sinceValue = sinceComp.get_selectedDate();

                var untilComp = $find("<%= this.RdUntil.ClientID %>");
                var untilValue = untilComp.get_selectedDate();

                var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=incomeStatementAccounts&exerciseYear=' + exerciseYearValue + '&accounts=' + accountsValue + '&accountsLabel=' + accountsLabelValue;

                if (budgetYearValue != null) {
                    url = url + '&budgetYear=' + budgetYearValue;
                }

                if (sinceValue != null) {
                    url = url + '&since=' + sinceValue;
                }

                if (untilValue != null) {
                    url = url + '&until=' + untilValue;
                }

                window.open(url, "Reporte");
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
