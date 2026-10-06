<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="CreditModifications.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Budget.CreditModifications" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>

<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <!-- Page Content -->
    <div id="section_budget" class="container">

        <h3>Modificaciones de Crédito</h3>

        <%--BUTTONS--%>
        <div class="top-buttons top-buttons-combobox">
            <div class="a-comboboxes">
                <asp:Label ID="LabelYear"
                    runat="server"
                    Text="Año: "></asp:Label>
                <telerik:RadComboBox ID="RcYears"
                    runat="server"
                    AutoPostBack="False"
                    DataTextField="Value"
                    DataValueField="Value">
                </telerik:RadComboBox>
                <asp:Label ID="LabelSince"
                    runat="server"
                    Text="N⁰ orden entre: "></asp:Label>
                <telerik:RadNumericTextBox ID="txtSince"
                    runat="server"
                    RenderMode="Lightweight"
                    MinValue="0"
                    ShowSpinButtons="False"
                    NumberFormat-DecimalDigits="0">
                </telerik:RadNumericTextBox>
                <asp:Label ID="LabelUntil"
                    runat="server"
                    Text="y"
                    CssClass="span-and"></asp:Label>
                <telerik:RadNumericTextBox ID="txtUntil"
                    runat="server"
                    RenderMode="Lightweight"
                    MinValue="0"
                    ShowSpinButtons="False"
                    NumberFormat-DecimalDigits="0">
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
                <div class="new">
                    <span class="icon"></span>
                    <a href="ManageCreditModification.aspx" role="button">Nueva</a>
                </div>
            </div>
        </div>

        <div id="page_creditModifications" class="box-block">

            <telerik:RadAjaxPanel ID="rapCreditModifications" runat="server"
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
                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgCreditModifications"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgCreditModifications_OnNeedDataSource"
                    OnPreRender="RgCreditModifications_OnPreRender"
                    OnItemCommand="RgCreditModifications_OnItemCommand"
                    CssClass="creditModifications-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="MOD_CODIGO"
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
                            <telerik:GridBoundColumn UniqueName="Year"
                                DataField="MOD_ANO_PRESUPUESTO"
                                HeaderText="Año"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Order"
                                DataField="MOD_NUMERO_ORDEN"
                                HeaderText="Número"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="DescriptionLabelIncome"
                                DataField="DescriptionLabelIncome"
                                HeaderText="Tipo de Modif. de Crédito de Ingreso"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="DescriptionLabelSpend"
                                DataField="DescriptionLabelSpend"
                                HeaderText="Tipo de Modif. de Crédito de Gasto"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridImageColumn DataType="System.String"
                                DataImageUrlFields="ExecuteImage"
                                DataImageUrlFormatString="/Public/Images/{0}.png"
                                ImageAlign="Baseline"
                                ImageHeight="20px"
                                ImageWidth="20px"
                                HeaderStyle-ForeColor="White"
                                HeaderStyle-Width="40px"
                                AllowFiltering="false">
                            </telerik:GridImageColumn>
                            <telerik:GridButtonColumn UniqueName="EditColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Editar modificación de crédito"
                                CommandName="UpdateCreditModification"
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

                var grid = $get("<%=this.RgCreditModifications.ClientID %>");
                var elements = grid.getElementsByTagName(tagName);
                for (var i = 0; i < elements.length; i++) {
                    var element = elements[i];
                    if (element.id.indexOf(serverId) >= 0) {
                        return element;
                    }
                }
            }

        </script>
    </telerik:RadScriptBlock>
</asp:Content>
