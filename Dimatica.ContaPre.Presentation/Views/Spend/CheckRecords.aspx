<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="CheckRecords.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Spend.CheckRecords" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <!-- Page Content -->
    <div id="section_spend" class="container">

        <h3>Comprobar Expedientes</h3>

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
            </div>
        </div>

        <div id="page_checkRecords" class="box-block">

            <telerik:RadAjaxPanel ID="rapCheckRecords" runat="server"
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

                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgRecords"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgRecords_OnNeedDataSource"
                    OnPreRender="RgRecords_OnPreRender"
                    OnItemCommand="RgRecords_OnItemCommand"
                    CssClass="checkRecords-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="EXP_CODIGO"
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
                            <telerik:GridBoundColumn UniqueName="EXP_NUM_EXP_CONTABLE_ANUAL"
                                DataField="EXP_NUM_EXP_CONTABLE_ANUAL"
                                HeaderText="N⁰"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="50px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="EXP_ANO_PRESUPUESTO"
                                DataField="EXP_ANO_PRESUPUESTO"
                                HeaderText="Año Pto."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="EA_DESCRIPCION"
                                DataField="EA_DESCRIPCION"
                                HeaderText="Descripción"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PROV_NOMBRE"
                                DataField="PROV_NOMBRE"
                                HeaderText="Proveedor"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PROC_DESCRIPCION"
                                DataField="PROC_DESCRIPCION"
                                HeaderText="Procedencia"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridImageColumn DataType="System.String"
                                DataImageUrlFields="MultiYearImage"
                                DataImageUrlFormatString="/Public/Images/{0}.png"
                                HeaderText="Plur."
                                ImageAlign="Baseline"
                                ImageHeight="20px"
                                ImageWidth="20px"
                                HeaderStyle-ForeColor="White"
                                HeaderStyle-Width="45px"
                                AllowFiltering="false">
                            </telerik:GridImageColumn>
                            <telerik:GridImageColumn DataType="System.String"
                                DataImageUrlFields="SquareImage"
                                DataImageUrlFormatString="/Public/Images/{0}.png"
                                HeaderText="Cuad."
                                ImageAlign="Baseline"
                                ImageHeight="20px"
                                ImageWidth="20px"
                                HeaderStyle-ForeColor="White"
                                HeaderStyle-Width="45px"
                                AllowFiltering="false">
                            </telerik:GridImageColumn>
                            <telerik:GridButtonColumn UniqueName="EditColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Editar expediente gasto"
                                CommandName="UpdateSpendRecord"
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

        </script>
    </telerik:RadScriptBlock>
</asp:Content>
