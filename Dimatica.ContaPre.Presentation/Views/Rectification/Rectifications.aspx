<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="Rectifications.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Rectification.Rectifications" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <%--Modales--%>
    <telerik:RadWindowManager ID="rwmRectifications" runat="server">
        <Windows>

            <%--imprimir rectificaciones--%>
            <telerik:RadWindow ID="rwPrintRectifications"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Imprimir Rectificaciones"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="330"
                Height="200"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientRectificationsCloseHandler">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <!-- Page Content -->
    <div id="section_rectification" class="container">

        <h3>Asociar Rectificaciones</h3>

        <div id="page_rectifications" class="box-block">

            <telerik:RadAjaxPanel ID="rapRectifications" runat="server"
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

                <%--BUTTONS--%>
                <div class="top-buttons">
                    <div class="a-buttons">
                        <div class="report">
                            <span class="icon"></span>
                            <asp:LinkButton ID="btnReport"
                                runat="server"
                                OnClick="btnReport_OnClick"
                                Text="Imprimir"></asp:LinkButton>
                        </div>
                    </div>
                </div>

                <div class="divided-screen">

                    <%--RECTIFICACIONES NEGATIVAS--%>
                    <div class="divided-screen-50">

                        <strong>Rectificaciones Negativas (R-)</strong>

                        <div class="top-buttons top-buttons-left">

                            <div class="form-group-fake">
                                <asp:Label ID="LabelTypeNegative"
                                    runat="server"
                                    Text="Tipo: "></asp:Label>
                                <telerik:RadComboBox ID="RcTypesNegative"
                                    runat="server"
                                    Width="100%"
                                    OnSelectedIndexChanged="RcTypesNegative_OnSelectedIndexChanged"
                                    AutoPostBack="True">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="< Seleccione >" Value="" />
                                        <telerik:RadComboBoxItem runat="server" Text="Ingresos" Value="I" />
                                        <telerik:RadComboBoxItem runat="server" Text="Extrapresupuestarias" Value="E" />
                                    </Items>
                                </telerik:RadComboBox>
                            </div>

                            <div class="form-group-fake">
                                <asp:Label ID="LabelYearNegative"
                                    runat="server"
                                    Text="Año: "></asp:Label>
                                <telerik:RadMonthYearPicker ID="RmyDatesNegative"
                                    runat="server"
                                    Width="100px"
                                    AutoPostBack="True"
                                    EnableTyping="False"
                                    Culture="es-ES"
                                    DateInput-Culture-="es-ES"
                                    MonthCellsStyle-CssClass="monthCellClass"
                                    OnSelectedDateChanged="RmyDatesNegative_OnSelectedDateChanged">
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

                        <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgRectificationsNegatives"
                            runat="server"
                            AllowSorting="true"
                            Culture="es-ES"
                            GroupPanelPosition="Top"
                            OnNeedDataSource="RgRectificationsNegatives_OnNeedDataSource"
                            AllowMultiRowSelection="True"
                            CssClass="rectification-table">

                            <GroupingSettings CaseSensitive="false" />

                            <MasterTableView AutoGenerateColumns="false"
                                AllowFilteringByColumn="true"
                                DataKeyNames="Code, Origin"
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
                                    <telerik:GridBoundColumn UniqueName="ConceptNegative"
                                        DataField="Concept"
                                        HeaderText="Concepto"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle Width="75px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="DescriptionNegative"
                                        DataField="Description"
                                        HeaderText="Descripción"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle Width="40%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ProviderNameNegative"
                                        DataField="ProviderName"
                                        HeaderText="Tercero"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle Width="40%" />
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn UniqueName="AmountNegative"
                                        DataField="AmountLabel"
                                        HeaderText="Importe"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false"
                                        ItemStyle-CssClass="right">
                                        <HeaderStyle Width="75px" />
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridNumericColumn UniqueName="AmountNegative"
                                        DataField="Amount"
                                        HeaderText="Importe"
                                        DataFormatString="{0:N}"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false"
                                        ItemStyle-CssClass="right"
                                        FilterControlWidth="100%">
                                        <HeaderStyle Width="75px" />
                                    </telerik:GridNumericColumn>
                                    <telerik:GridClientSelectColumn UniqueName="SelectColumnPositive">
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

                    <%--RECTIFICACIONES POSITIVAS--%>
                    <div class="divided-screen-50">

                        <strong>Rectificaciones Positivas (R+)</strong>

                        <div class="top-buttons top-buttons-left">

                            <div class="form-group-fake">
                                <asp:Label ID="LabelTypePositive"
                                    runat="server"
                                    Text="Tipo: "></asp:Label>
                                <telerik:RadComboBox ID="RcTypesPositive"
                                    runat="server"
                                    Width="100%"
                                    OnSelectedIndexChanged="RcTypesPositive_OnSelectedIndexChanged"
                                    AutoPostBack="True">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="< Seleccione >" Value="" />
                                        <telerik:RadComboBoxItem runat="server" Text="Ingresos" Value="I" />
                                        <telerik:RadComboBoxItem runat="server" Text="Extrapresupuestarias" Value="E" />
                                    </Items>
                                </telerik:RadComboBox>
                            </div>

                            <div class="form-group-fake">
                                <asp:Label ID="LabelYearPositive"
                                    runat="server"
                                    Text="Año: "></asp:Label>
                                <telerik:RadMonthYearPicker ID="RmyDatesPositive"
                                    runat="server"
                                    Width="100px"
                                    AutoPostBack="True"
                                    EnableTyping="False"
                                    Culture="es-ES"
                                    DateInput-Culture-="es-ES"
                                    MonthCellsStyle-CssClass="monthCellClass"
                                    OnSelectedDateChanged="RmyDatesPositive_OnSelectedDateChanged">
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

                        <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgRectificationsPositives"
                            runat="server"
                            AllowSorting="true"
                            Culture="es-ES"
                            GroupPanelPosition="Top"
                            OnNeedDataSource="RgRectificationsPositives_OnNeedDataSource"
                            AllowMultiRowSelection="True"
                            CssClass="rectification-table">

                            <GroupingSettings CaseSensitive="false" />

                            <MasterTableView AutoGenerateColumns="false"
                                AllowFilteringByColumn="true"
                                DataKeyNames="Code, Origin"
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
                                    <telerik:GridBoundColumn UniqueName="ConceptPositive"
                                        DataField="Concept"
                                        HeaderText="Concepto"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle Width="75px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="DescriptionPositive"
                                        DataField="Description"
                                        HeaderText="Descripción"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle Width="40%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ProviderNamePositive"
                                        DataField="ProviderName"
                                        HeaderText="Tercero"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <HeaderStyle Width="40%" />
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn UniqueName="AmountPositive"
                                        DataField="AmountLabel"
                                        HeaderText="Importe"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false"
                                        ItemStyle-CssClass="right">
                                        <HeaderStyle Width="75px" />
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridNumericColumn UniqueName="AmountPositive"
                                        DataField="Amount"
                                        HeaderText="Importe"
                                        DataFormatString="{0:N}"
                                        AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false"
                                        ItemStyle-CssClass="right"
                                        FilterControlWidth="100%">
                                        <HeaderStyle Width="75px" />
                                    </telerik:GridNumericColumn>
                                    <telerik:GridClientSelectColumn UniqueName="SelectColumnPositive">
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

            function GetGridServerElement(serverId, tagName) {
                if (!tagName) {
                    tagName = "*";
                }

                var grid = $get("<%=this.RgRectificationsNegatives.ClientID %>");
                var elements = grid.getElementsByTagName(tagName);
                for (var i = 0; i < elements.length; i++) {
                    var element = elements[i];
                    if (element.id.indexOf(serverId) >= 0) {
                        return element;
                    }
                }
            }

            function showBtnReport() {
                debugger 
                $(".report").show();     
            }

            function showSelectedDate(iCodes, eCodes) {
                var url = "SelectDate.aspx?iCodes=" + iCodes + "&eCodes=" + eCodes;
                var manager = $find("<%= this.rwmRectifications.ClientID %>");
                var oWnd = manager.open(url, "rwPrintRectifications");
            }

            function OnClientRectificationsCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    var date = data.date;
                    var iCodes = data.iCodes;
                    var eCodes = data.eCodes;

                    var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=rectificationsList&date=' + date + '&iCodes=' + iCodes + '&eCodes=' + eCodes;

                    window.open(url, "Reporte");
                }
            }
        </script>
    </telerik:RadScriptBlock>

</asp:Content>
