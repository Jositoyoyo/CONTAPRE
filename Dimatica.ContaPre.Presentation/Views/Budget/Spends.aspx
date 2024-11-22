<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="Spends.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Budget.Spends" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <%--Modales--%>
    <telerik:RadWindowManager ID="rwmSpends" runat="server">
        <Windows>

            <%--nuevo presupuesto--%>
            <telerik:RadWindow ID="rwNewSpends"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Nuevo Presupuesto"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="900"
                Height="700"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientNewSpendsCloseHandler"
                CssClass="tabla-presupuestos">
            </telerik:RadWindow>

            <%--editar presupuesto--%>
            <telerik:RadWindow ID="rwUpdateSpends"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Editar Presupuesto"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="900"
                Height="700"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientUpdateSpendsCloseHandler"
                CssClass="tabla-presupuestos">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <!-- Page Content -->
    <div id="section_budget" class="container">

        <h3>Gastos</h3>

        <%--BUTTONS--%>
        <div class="top-buttons top-buttons-combobox">
            <div class="a-comboboxes">
                <asp:Label ID="LabelType"
                    runat="server"
                    Text="Año: "></asp:Label>
                <telerik:RadComboBox ID="RcYears"
                    runat="server"
                    Width="80px"
                    OnSelectedIndexChanged="RcYears_OnSelectedIndexChanged"
                    AutoPostBack="True"
                    DataTextField="Value"
                    DataValueField="Value">
                </telerik:RadComboBox>
                <span id="LabelClose"
                    style="color: red">Presupuesto CERRADO</span>
            </div>
            <div class="a-buttons">
                <div class="new">
                    <span class="icon"></span>
                    <a onclick="newBudget()" role="button">Nuevo</a>
                </div>
                <div class="report" style="width: 150px">
                    <span class="icon"></span>
                    <a onclick="reportBudget()" role="button">Grado Cumpl.</a>
                </div>
                <div class="edit">
                    <span class="icon"></span>
                    <a onclick="editBudget()" role="button">Editar</a>
                </div>
                <div class="delete">
                    <span class="icon"></span>
                    <a onclick="deleteBudget()" role="button">Eliminar</a>
                </div>
                <div class="close">
                    <span class="icon"></span>
                    <a onclick="closeBudget()" role="button">Cerrar</a>
                </div>
                <%--<div class="back">
                    <span class="icon"></span>
                    <a onclick="backAction()" role="button">Volver</a>
                </div>--%>
            </div>
        </div>

        <div id="page_spends" class="box-block">

            <telerik:RadAjaxPanel ID="rapSpends" runat="server"
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
                <div id="progressBudget">
                    <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgSpends"
                        runat="server"
                        AllowSorting="true"
                        Culture="es-ES"
                        GroupPanelPosition="Top"
                        OnNeedDataSource="RgSpends_OnNeedDataSource"
                        OnItemDataBound="RgSpends_OnItemDataBound"
                        OnPreRender="RgSpends_OnPreRender"
                        CssClass="budget-table">

                        <GroupingSettings CaseSensitive="false" />

                        <MasterTableView AutoGenerateColumns="false"
                            AllowFilteringByColumn="true"
                            DataKeyNames="BudgetId, Level"
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
                                <telerik:GridBoundColumn UniqueName="Program"
                                    DataField="ProgramLabel"
                                    HeaderText="Prog."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="75px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Chapter"
                                    DataField="Chapter"
                                    HeaderText="Cap."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="75px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Article"
                                    DataField="Article"
                                    HeaderText="Art."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="75px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Concept"
                                    DataField="Concept"
                                    HeaderText="Con."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="75px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="SubConcept"
                                    DataField="SubConcept"
                                    HeaderText="Sub."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="75px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Description"
                                    DataField="Description"
                                    HeaderText="Descripción"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn UniqueName="AmountSub"
                                    DataField="FinalAmountSub"
                                    HeaderText="Por Subcon."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridNumericColumn UniqueName="AmountSub"
                                    DataField="FinalAmountSub"
                                    HeaderText="Por Subcon."
                                    DataFormatString="{0:N}"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right"
                                    FilterControlWidth="100%">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridNumericColumn>
                                <%--<telerik:GridBoundColumn UniqueName="AmountCon"
                                    DataField="FinalAmountCon"
                                    HeaderText="Por Conc."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridNumericColumn UniqueName="AmountCon"
                                    DataField="FinalAmountCon"
                                    HeaderText="Por Conc."
                                    DataFormatString="{0:N}"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right"
                                    FilterControlWidth="100%">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridNumericColumn>
                                <%--<telerik:GridBoundColumn UniqueName="AmountChaArt"
                                    DataField="FinalAmountChaArt"
                                    HeaderText="Por Cap. y Art."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridNumericColumn UniqueName="AmountChaArt"
                                    DataField="FinalAmountChaArt"
                                    HeaderText="Por Cap. y Art."
                                    DataFormatString="{0:N}"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right"
                                    FilterControlWidth="100%">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridNumericColumn>
                                <telerik:GridCheckBoxColumn UniqueName="NotBinding"
                                    DataField="NotBinding"
                                    DataType="System.Boolean"
                                    HeaderText="No Vinc."
                                    StringFalseValue="False"
                                    StringTrueValue="True"
                                    AllowFiltering="false">
                                    <HeaderStyle Width="80px" />
                                </telerik:GridCheckBoxColumn>
                            </Columns>
                        </MasterTableView>

                        <ClientSettings>
                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                            <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                        </ClientSettings>
                    </telerik:RadGrid>


                    <div class="monto-total">
                        <strong>Total:</strong>
                        <telerik:RadTextBox ID="txtTotal"
                            Width="120px"
                            runat="server"
                            MaxLength="80"
                            Text="0,00"
                            Enabled="False">
                        </telerik:RadTextBox>
                    </div>
                </div>

                <%--Ingresos cerrados--%>
                <div id="closeBudget">
                    <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgSpendsClose"
                        runat="server"
                        AllowSorting="true"
                        Culture="es-ES"
                        GroupPanelPosition="Top"
                        OnNeedDataSource="RgSpendsClose_OnNeedDataSource"
                        OnItemDataBound="RgSpendsClose_OnItemDataBound"
                        OnPreRender="RgSpendsClose_OnPreRender"
                        CssClass="close-budget-table">

                        <GroupingSettings CaseSensitive="false" />

                        <MasterTableView AutoGenerateColumns="false"
                            AllowFilteringByColumn="true"
                            DataKeyNames="BudgetId, Description, Level"
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

                            <ColumnGroups>
                                <telerik:GridColumnGroup Name="InitialAmount"
                                    HeaderText="Inicial"
                                    HeaderStyle-HorizontalAlign="Center" />
                                <telerik:GridColumnGroup Name="UpdateAmount"
                                    HeaderText="Modificación"
                                    HeaderStyle-HorizontalAlign="Center" />
                                <telerik:GridColumnGroup Name="FinalAmount"
                                    HeaderText="Definitivo"
                                    HeaderStyle-HorizontalAlign="Center" />
                            </ColumnGroups>

                            <Columns>
                                <telerik:GridBoundColumn UniqueName="Program"
                                    DataField="ProgramLabel"
                                    HeaderText="Prog."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="75px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ApplicationLabel"
                                    DataField="ApplicationLabel"
                                    HeaderText="Aplic."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="75px" />
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridEditCommandColumn UniqueName="DescriptionColumn"
                                    ButtonType="LinkButton"
                                    HeaderTooltip="Descripción"
                                    ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="40px"
                                    ItemStyle-Width="40px"
                                    ItemStyle-CssClass="fas fa-points"
                                    ShowFilterIcon="false"
                                    EditText=" ">
                                </telerik:GridEditCommandColumn>--%>
                                <telerik:GridImageColumn UniqueName="DescriptionColumn"
                                    DataType="System.String"
                                    DataImageUrlFields="PointsImage"
                                    DataImageUrlFormatString="/Content/Images/points.png"
                                    HeaderText=""
                                    ImageAlign="Baseline"
                                    ImageHeight="20px"
                                    ImageWidth="20px"
                                    HeaderStyle-ForeColor="White"
                                    HeaderStyle-Width="40px"
                                    AllowFiltering="false">
                                </telerik:GridImageColumn>
                                <%--<telerik:GridBoundColumn UniqueName="AmountSub"
                                    DataField="AmountSub"
                                    HeaderText="Subcon."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ColumnGroupName="InitialAmount"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridNumericColumn UniqueName="AmountSub"
                                    DataField="AmountSub"
                                    HeaderText="Subcon."
                                    DataFormatString="{0:N}"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right"
                                    FilterControlWidth="100%">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridNumericColumn>
                                <%--<telerik:GridBoundColumn UniqueName="AmountCon"
                                    DataField="AmountCon"
                                    HeaderText="Conc."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ColumnGroupName="InitialAmount"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridNumericColumn UniqueName="AmountCon"
                                    DataField="AmountCon"
                                    HeaderText="Conc."
                                    DataFormatString="{0:N}"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right"
                                    FilterControlWidth="100%">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridNumericColumn>
                                <%--<telerik:GridBoundColumn UniqueName="AmountChaArt"
                                    DataField="AmountChaArt"
                                    HeaderText="Cap./Art."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ColumnGroupName="InitialAmount"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridNumericColumn UniqueName="AmountChaArt"
                                    DataField="AmountChaArt"
                                    HeaderText="Cap./Art."
                                    DataFormatString="{0:N}"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right"
                                    FilterControlWidth="100%">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridNumericColumn>
                                <%--<telerik:GridBoundColumn UniqueName="UpdateAmountSub"
                                    DataField="UpdateAmountSub"
                                    HeaderText="Subcon."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ColumnGroupName="UpdateAmount"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridNumericColumn UniqueName="UpdateAmountSub"
                                    DataField="UpdateAmountSub"
                                    HeaderText="Subcon."
                                    DataFormatString="{0:N}"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right"
                                    FilterControlWidth="100%">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridNumericColumn>
                                <%--<telerik:GridBoundColumn UniqueName="UpdateAmountCon"
                                    DataField="UpdateAmountCon"
                                    HeaderText="Conc."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ColumnGroupName="UpdateAmount"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridNumericColumn UniqueName="UpdateAmountCon"
                                    DataField="UpdateAmountCon"
                                    HeaderText="Conc."
                                    DataFormatString="{0:N}"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right"
                                    FilterControlWidth="100%">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridNumericColumn>
                                <%--<telerik:GridBoundColumn UniqueName="UpdateAmountChaArt"
                                    DataField="UpdateAmountChaArt"
                                    HeaderText="Cap./Art."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ColumnGroupName="UpdateAmount"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridNumericColumn UniqueName="UpdateAmountChaArt"
                                    DataField="UpdateAmountChaArt"
                                    HeaderText="Cap./Art."
                                    DataFormatString="{0:N}"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right"
                                    FilterControlWidth="100%">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridNumericColumn>
                                <%--<telerik:GridBoundColumn UniqueName="FinalAmountSub"
                                    DataField="FinalAmountSub"
                                    HeaderText="Subcon."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ColumnGroupName="FinalAmount"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridNumericColumn UniqueName="FinalAmountSub"
                                    DataField="FinalAmountSub"
                                    HeaderText="Subcon."
                                    DataFormatString="{0:N}"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right"
                                    FilterControlWidth="100%">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridNumericColumn>
                                <%--<telerik:GridBoundColumn UniqueName="FinalAmountCon"
                                    DataField="FinalAmountCon"
                                    HeaderText="Conc."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ColumnGroupName="FinalAmount"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridNumericColumn UniqueName="FinalAmountCon"
                                    DataField="FinalAmountCon"
                                    HeaderText="Conc."
                                    DataFormatString="{0:N}"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right"
                                    FilterControlWidth="100%">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridNumericColumn>
                                <%--<telerik:GridBoundColumn UniqueName="FinalAmountChaArt"
                                    DataField="FinalAmountChaArt"
                                    HeaderText="Cap./Art."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ColumnGroupName="FinalAmount"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridNumericColumn UniqueName="FinalAmountChaArt"
                                    DataField="FinalAmountChaArt"
                                    HeaderText="Cap./Art."
                                    DataFormatString="{0:N}"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right"
                                    FilterControlWidth="100%">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridNumericColumn>
                                <telerik:GridCheckBoxColumn UniqueName="NotBinding"
                                    DataField="NotBinding"
                                    DataType="System.Boolean"
                                    HeaderText="No Vinc."
                                    StringFalseValue="False"
                                    StringTrueValue="True"
                                    AllowFiltering="false">
                                    <HeaderStyle Width="80px" />
                                </telerik:GridCheckBoxColumn>
                            </Columns>
                        </MasterTableView>

                        <ClientSettings>
                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                            <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                        </ClientSettings>
                    </telerik:RadGrid>

                    <div class="monto-total">
                        <strong>Total:</strong>
                        <telerik:RadTextBox ID="txtCurrentTotal"
                            Width="120px"
                            runat="server"
                            MaxLength="80"
                            Text="0,00"
                            Enabled="False">
                        </telerik:RadTextBox>
                        <telerik:RadTextBox ID="txtUpdateTotal"
                            Width="120px"
                            runat="server"
                            MaxLength="80"
                            Text="0,00"
                            Enabled="False">
                        </telerik:RadTextBox>
                        <telerik:RadTextBox ID="txtFinalTotal"
                            Width="120px"
                            runat="server"
                            MaxLength="80"
                            Text="0,00"
                            Enabled="False">
                        </telerik:RadTextBox>
                    </div>
                </div>
            </telerik:RadAjaxPanel>
        </div>
    </div>

    <telerik:RadScriptBlock runat="server">
        <script>

<%--            $(document).ready(function () {
                debugger;
                var origin = '<%= this.Session["_budgetOrigin"] %>';
                if (origin != "") {
                    '<% this.Session["_budgetOrigin"] = ""; %>';

                    switch (origin) {
                        case "newSpend":
                            newBudget();
                            break;
                        case "updateSpend":
                            editBudget();
                            break;
                    }
                }
            });--%>

            function hideShowGrids(isClose) {
                if (isClose == 'True') {
                    $('#progressBudget').hide();
                    $('#closeBudget').show();
                    $('#LabelClose').show();
                }
                else {
                    $('#closeBudget').hide();
                    $('#progressBudget').show();
                    $('#LabelClose').hide();
                }
            }

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

                var grid = $get("<%=this.RgSpends.ClientID %>");
                var elements = grid.getElementsByTagName(tagName);
                for (var i = 0; i < elements.length; i++) {
                    var element = elements[i];
                    if (element.id.indexOf(serverId) >= 0) {
                        return element;
                    }
                }
            }

            function newBudget() {
                var url = "NewSpends.aspx";
                var manager = $find("<%= this.rwmSpends.ClientID %>");
                var oWnd = manager.open(url, "rwNewSpends");
            }

            function reportBudget() {
                var yearComp = $find("<%= this.RcYears.ClientID %>");
                var yearValue = yearComp.get_value();

                var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=spendLevelCompliance&year=' + yearValue;
                window.open(url, "Reporte");
            }

            function editBudget() {
                var yearComp = $find("<%= this.RcYears.ClientID %>");
                var yearValue = yearComp.get_value();

                var url = "UpdateSpends.aspx?year=" + yearValue;
                var manager = $find("<%= this.rwmSpends.ClientID %>");
                var oWnd = manager.open(url, "rwUpdateSpends");
            }

            function deleteBudget() {
                radconfirm("¿Está seguro que desea eliminar realmente este Presupuesto de Gasto ?</br></br>AVISO: Sólo puede eliminarse un Presupuesto en caso de que</br>no se haya utilizado en ningún crédito o documento.",
                    confirmDeleteCallBackFn,
                    500,
                    220,
                    null,
                    "Confirmación",
                    null);
            }

            function closeBudget() {
                radconfirm("¿ Está seguro que desea cerrar este Presupuesto de Gasto ?</br></br>Recuerde que una vez cerrado, el presupuesto no podrá ser eliminado</br>y solo podrá ser modificado a través de expedientes de modificación.",
                    confirmCloseCallBackFn,
                    500,
                    220,
                    null,
                    "Confirmación",
                    null);
            }

            function OnClientNewSpendsCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    switch (data) {
                        case 1:
                            location.reload(true);
                            break;
                        case 2:
                            var url = window.location.origin + '\\Views\\Maintenance\\Applications.aspx';
                            window.location.href = url;
                            break;
                        case 3:
                            var url = window.location.origin + '\\Views\\Maintenance\\Programs.aspx';
                            window.location.href = url;
                            break;
                    }
                }
            }

            function OnClientUpdateSpendsCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    switch (data) {
                        case 1:
                            location.reload(true);
                            break;
                        case 2:
                            var yearComp = $find("<%= this.RcYears.ClientID %>");
                            var yearValue = yearComp.get_value();

                            var url = window.location.origin + '\\Views\\Maintenance\\Applications.aspx?year=' + yearValue + "&type=I";
                            window.location.href = url;
                            break;
                        case 3:
                            var url = window.location.origin + '\\Views\\Maintenance\\Programs.aspx';
                            window.location.href = url;
                            break;
                    }
                }
            }

            function confirmDeleteCallBackFn(arg) {
                if (arg == null || arg == false) {
                    return;
                }

                var yearComp = $find("<%= this.RcYears.ClientID %>");
                var yearValue = yearComp.get_value();

                $.ajax({
                    type: "POST",
                    url: "Spends.aspx/DeleteBudget",
                    data: JSON.stringify({
                        yearValue: yearValue
                    }),
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    success: function (result) {
                        switch (result.d) {
                            case 0:
                                radalert("No se puede eliminar el Presupuesto en cuestión debido a que ya ha sido utilizado.", 330, 140, "Imposible eliminar presupuesto", null, null);
                                break;
                            case 1:
                                location.reload(true);
                                break;
                            case 2:
                                radalert("No se puede eliminar el Presupuesto en cuestión debido a que no se ha encontrado en la BD.", 330, 140, "Imposible eliminar presupuesto", null, null);
                                break;
                            case 3:
                                radalert("No se puede eliminar el Presupuesto en cuestión debido a que ya se encuentra cerrado.", 330, 140, "Imposible eliminar presupuesto", null, null);
                                break;
                            case 4:
                                radalert("No se puede eliminar el Presupuesto en cuestión debido a un error inesperado.", 330, 140, "Imposible eliminar presupuesto", null, null);
                                break;
                        }
                    }, error: function (xhr, ajaxOptions, thrownError) {
                        radalert("No se puede eliminar el Presupuesto en cuestión debido a un error inesperado.", 330, 140, "Imposible eliminar presupuesto", null, null);
                    }
                });

                //PageMethods.DeleteBudget(yearValue, OnDeleteBudgetSuccess);
            }

            $(document)
                .ajaxStart(function () {
                    debugger;
                    $('#ralPrincipal').show();
                })
                .ajaxStop(function () {
                    debugger;
                    $('#ralPrincipal').hide();
                });

            function OnDeleteBudgetSuccess(response, userContext, methodName) {
                <%--    var radNotification = $find("<%= this.RadNotification.ClientID %>");
                radNotification.set_text('asdasd');
                radNotification.set_title('test');
                radNotification.show();--%>
                switch (response) {
                    case 0:
                        radalert("No se puede eliminar el Presupuesto en cuestión debido a que ya ha sido utilizado.", 330, 140, "Imposible eliminar presupuesto", null, null);
                        break;
                    case 1:
                        location.reload(true);
                        break;
                    case 2:
                        radalert("No se puede eliminar el Presupuesto en cuestión debido a que no se ha encontrado en la BD.", 330, 140, "Imposible eliminar presupuesto", null, null);
                        break;
                    case 3:
                        radalert("No se puede eliminar el Presupuesto en cuestión debido a que ya se encuentra cerrado.", 330, 140, "Imposible eliminar presupuesto", null, null);
                        break;
                    case 4:
                        radalert("No se puede eliminar el Presupuesto en cuestión debido a un error inesperado.", 330, 140, "Imposible eliminar presupuesto", null, null);
                        break;
                }
            }

            function confirmCloseCallBackFn(arg) {
                if (arg == null || arg == false) {
                    return;
                }

                var yearComp = $find("<%= this.RcYears.ClientID %>");
                var yearValue = yearComp.get_value();

                PageMethods.CloseBudget(yearValue, OnDeleteBudgetSuccess);
            }

            function OnCloseBudgetSuccess(response, userContext, methodName) {
                switch (response) {
                    case 1:
                        location.reload(true);
                        break;
                    case 2:
                        radalert("No se puede cerrar el Presupuesto en cuestión debido a que no se ha encontrado en la BD.", 330, 140, "Imposible cerrar presupuesto", null, null);
                        break;
                    case 3:
                        radalert("No se puede cerrar el Presupuesto en cuestión debido a un error inesperado.", 330, 140, "Imposible cerrar presupuesto", null, null);
                        break;
                }
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
