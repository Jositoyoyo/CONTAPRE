<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="SpendRecords.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Spend.SpendRecords" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <!-- Page Content -->
    <div id="section_spend" class="container">

        <h3>Expedientes Contables de Gastos</h3>

        <%--BUTTONS--%>
        <div class="top-buttons">
            <div class="a-buttons">
                <div class="find">
                    <span class="icon"></span>
                    <asp:LinkButton ID="btnFind"
                        runat="server"
                        OnClick="btnFind_OnClick"
                        Text="Buscar"
                        ValidationGroup="RecordGroup"></asp:LinkButton>
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

        <div id="page_spendRecords" class="box-block">

            <telerik:RadAjaxPanel ID="rapSpendRecords" runat="server"
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
                                            <telerik:RadComboBox ID="RcBudgetYears"
                                                runat="server"
                                                Width="130px"
                                                AutoPostBack="False"
                                                DataTextField="Value"
                                                DataValueField="Value">
                                            </telerik:RadComboBox>
                                        </div>

                                        <div class="field-container">
                                            <span>N⁰ Exped. Contable.:</span>
                                            <telerik:RadNumericTextBox ID="RntNumber"
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
                                            <span>Procedencia Gasto:</span>
                                            <telerik:RadComboBox ID="RcProvenances"
                                                runat="server"
                                                Width="250px"
                                                AutoPostBack="False"
                                                DataTextField="PROC_DESCRIPCION"
                                                DataValueField="PROC_CODIGO">
                                            </telerik:RadComboBox>
                                        </div>

                                        <div class="field-container">
                                            <span>Plurianual:</span>
                                            <telerik:RadComboBox ID="RcMultiYear"
                                                runat="server"
                                                AutoPostBack="False">
                                                <Items>
                                                    <telerik:RadComboBoxItem runat="server" Text="< Seleccione >" Value="" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Si" Value="1" />
                                                    <telerik:RadComboBoxItem runat="server" Text="No" Value="0" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </div>

                                        <div class="field-container">
                                            <span>Proveedor:</span>
                                            <telerik:RadComboBox ID="RcProviders"
                                                runat="server"
                                                DataValueField="PROV_CODIGO"
                                                DataTextField="PROV_NOMBRE">
                                            </telerik:RadComboBox>
                                        </div>

                                        <div class="field-container" style="display: block">
                                            <span style="display: block">NIF:</span>
                                            <telerik:RadTextBox ID="RtbNifFirst"
                                                runat="server"
                                                Width="50px"
                                                MaxLength="1"
                                                Text="">
                                            </telerik:RadTextBox>
                                            <asp:RegularExpressionValidator ID="revNifFirst"
                                                runat="server"
                                                Display="Dynamic"
                                                ValidationGroup="RecordGroup"
                                                ControlToValidate="RtbNifFirst"
                                                ErrorMessage=" * "
                                                ToolTip="Formato inválido."
                                                ForeColor="Red"
                                                ValidationExpression="[a-zA-Z]">
                                            </asp:RegularExpressionValidator>
                                            <telerik:RadTextBox ID="RtbNifNumber"
                                                runat="server"
                                                Width="100px"
                                                MaxLength="8"
                                                Text="">
                                            </telerik:RadTextBox>
                                            <asp:RegularExpressionValidator ID="revNifNumber"
                                                runat="server"
                                                Display="Dynamic"
                                                ValidationGroup="RecordGroup"
                                                ControlToValidate="RtbNifNumber"
                                                ErrorMessage=" * "
                                                ToolTip="Formato inválido."
                                                ForeColor="Red"
                                                ValidationExpression="\d{7,8}">
                                            </asp:RegularExpressionValidator>
                                            <telerik:RadTextBox ID="RtbNifLast"
                                                runat="server"
                                                Width="50px"
                                                MaxLength="1"
                                                Text="">
                                            </telerik:RadTextBox>
                                            <asp:RegularExpressionValidator ID="revNifLast"
                                                runat="server"
                                                Display="Dynamic"
                                                ValidationGroup="RecordGroup"
                                                ControlToValidate="RtbNifLast"
                                                ErrorMessage=" * "
                                                ToolTip="Formato inválido."
                                                ForeColor="Red"
                                                ValidationExpression="[a-zA-Z]">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>


                                
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>
                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgRecords"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgRecords_OnNeedDataSource"
                    OnPreRender="RgRecords_OnPreRender"
                    OnItemCommand="RgRecords_OnItemCommand"
                    CssClass="incomeDrRecords-table">

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
                                <HeaderStyle Width="75px" />
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
                                HeaderText="Plu."
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

            function btnReportOnClientClick(sender, eventArgs) {
                var budgetYearsComp = $find("<%= this.RcBudgetYears.ClientID %>");
                var budgetYearsValue = budgetYearsComp.get_selectedItem().get_value();

                var numberComp = $find("<%= this.RntNumber.ClientID %>");
                var numberValue = numberComp.get_value();

                var provenancesComp = $find("<%= this.RcProvenances.ClientID %>");
                var provenancesValue = provenancesComp.get_selectedItem().get_value();

                var providersComp = $find("<%= this.RcProviders.ClientID %>");
                var providersValue = providersComp.get_selectedItem().get_value();

                var nifFirstComp = $find("<%= this.RtbNifFirst.ClientID %>");
                var nifFirstValue = nifFirstComp.get_value();

                var nifNumberComp = $find("<%= this.RtbNifNumber.ClientID %>");
                var nifNumberValue = nifNumberComp.get_value();

                var nifLastComp = $find("<%= this.RtbNifLast.ClientID %>");
                var nifLastValue = nifLastComp.get_value();

                var multiYearComp = $find("<%= this.RcMultiYear.ClientID %>");
                var multiYearValue = multiYearComp.get_selectedItem().get_value();

                var providerNif = nifFirstValue + "-" + nifNumberValue + "-" + nifLastValue;

                var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=spendsRecordsList';

                if (budgetYearsValue != "< Seleccione >") {
                    url = url + '&budgetYears=' + budgetYearsValue;
                }

                if (numberValue != "") {
                    url = url + '&number=' + numberValue;
                }

                if (provenancesValue != "-1") {
                    url = url + '&provenances=' + provenancesValue;
                }

                if (providersValue != "-1") {
                    url = url + '&providers=' + providersValue;
                }

                if (providerNif != "--") {
                    url = url + '&providerNif=' + providerNif;
                }

                if (multiYearValue != "") {
                    url = url + '&multiYear=' + multiYearValue;
                }

                window.open(url, "Reporte");
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
