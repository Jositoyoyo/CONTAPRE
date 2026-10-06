<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="CheckApplicationAmount.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Spend.CheckApplicationAmount" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <!-- Page Content -->
    <div id="section_spend" class="container">

        <h3>Consultar Aplicación Importe</h3>

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

        <div id="page_checkApplicationAmount" class="box-block">

            <telerik:RadAjaxPanel ID="rapCheckApplicationAmount" runat="server"
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
                                                AutoPostBack="True"
                                                OnSelectedIndexChanged="RcYears_OnSelectedIndexChanged"
                                                DataTextField="Value"
                                                DataValueField="Value">
                                            </telerik:RadComboBox>
                                        </div>

                                        <div class="field-container">
                                            <span>Aplicación</span>
                                            <telerik:RadComboBox ID="RcApplications"
                                                runat="server"
                                                Width="250px"
                                                AutoPostBack="False"
                                                DataTextField="Description"
                                                DataValueField="CacsCode">
                                            </telerik:RadComboBox>
                                        </div>

                                        <div class="field-container">
                                            <span>Importe entre:</span>
                                            <telerik:RadNumericTextBox ID="RntSince"
                                                runat="server"
                                                RenderMode="Lightweight"
                                                Width="150px"
                                                MinValue="0"
                                                ShowSpinButtons="False"
                                                Culture="es-ES">
                                            </telerik:RadNumericTextBox>
                                        </div>

                                        <div class="field-container">
                                            <span>y:</span>
                                            <telerik:RadNumericTextBox ID="RntUntil"
                                                runat="server"
                                                RenderMode="Lightweight"
                                                Width="150px"
                                                MinValue="0"
                                                ShowSpinButtons="False"
                                                Culture="es-ES">
                                            </telerik:RadNumericTextBox>
                                        </div>

                                        <div class="field-container">
                                            <span>Enlaz.</span>
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
                                            <span>Cuad.</span>
                                            <telerik:RadComboBox ID="RcSquare"
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
                                            <span>Proveedor:</span>
                                            <telerik:RadComboBox ID="RcProviders"
                                                runat="server"
                                                Width="400px"
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

                                        <div class="field-container">
                                            <span style="display: block">Tipo:</span>
                                            <telerik:RadComboBox ID="RcDocumentTypes"
                                                runat="server"
                                                Width="150px"
                                                AutoPostBack="False"
                                                DataTextField="DisplayLabel"
                                                DataValueField="TIPD_CODIGO">
                                            </telerik:RadComboBox>
                                        </div>

                                        <div class="field-container">
                                            <span>N⁰:</span>
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
                    CssClass="spendCheckRecords-table">

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
                            <telerik:GridBoundColumn UniqueName="EXP_ANO_PRESUPUESTO"
                                DataField="EXP_ANO_PRESUPUESTO"
                                HeaderText="Año Pto."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="EXP_NUM_EXP_CONTABLE_ANUAL"
                                DataField="EXP_NUM_EXP_CONTABLE_ANUAL"
                                HeaderText="N⁰"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                DataType="System.Int32">
                                <HeaderStyle Width="50px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CACS_NUMERO"
                                DataField="CACS_NUMERO"
                                HeaderText="Aplic."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TIPO_DOC"
                                DataField="TIPO_DOC"
                                HeaderText="Doc."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <%--columna dinero--%>
                            <telerik:GridNumericColumn UniqueName="IMPORTE"
                                DataField="IMPORTE"
                                HeaderText="Importe"
                                DataFormatString="{0:N}"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right"
                                FilterControlWidth="100%">
                                <HeaderStyle Width="150px" />
                            </telerik:GridNumericColumn>
                            <%--  <telerik:GridBoundColumn UniqueName="AmountLabel"
                                DataField="AmountLabel"
                                HeaderText="Importe"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn UniqueName="DOC_ENLAZADO_TESORERIA_LABEL"
                                DataField="DOC_ENLAZADO_TESORERIA_LABEL"
                                HeaderText="Teso."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="50px" />
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
                                HeaderText="Interesado"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
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
                <div class="monto-total-spendRecords">
                    <strong>Total:</strong>
                    <telerik:RadTextBox ID="txtTotal"
                        Width="120px"
                        runat="server"
                        MaxLength="80"
                        Text="0,00"
                        Enabled="False">
                    </telerik:RadTextBox>
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

            function btnReportOnClientClick(sender, eventArgs) {
                var yearComp = $find("<%= this.RcBudgetYears.ClientID %>");
                var yearValue = yearComp.get_selectedItem().get_value();

                var applicationComp = $find("<%= this.RcApplications.ClientID %>");
                var applicationValue = applicationComp.get_selectedItem().get_value();

                var sinceAmountComp = $find("<%= this.RntSince.ClientID %>");
                var sinceAmountValue = sinceAmountComp.get_value();

                var untilAmountComp = $find("<%= this.RntUntil.ClientID %>");
                var untilAmountValue = untilAmountComp.get_value();

                var boundComp = $find("<%= this.RcBound.ClientID %>");
                var boundValue = boundComp.get_selectedItem().get_value();

                var squareComp = $find("<%= this.RcSquare.ClientID %>");
                var squareValue = squareComp.get_selectedItem().get_value();

                var providerComp = $find("<%= this.RcProviders.ClientID %>");
                var providerValue = providerComp.get_selectedItem().get_value();

                var nifFirstComp = $find("<%= this.RtbNifFirst.ClientID %>");
                var nifFirstValue = nifFirstComp.get_value();
                var nifNumberComp = $find("<%= this.RtbNifNumber.ClientID %>");
                var nifNumberValue = nifNumberComp.get_value();
                var nifLastComp = $find("<%= this.RtbNifLast.ClientID %>");
                var nifLastValue = nifLastComp.get_value();
                var providerNifValue = nifFirstValue + "-" + nifNumberValue + "-" + nifLastValue;

                var docTypeComp = $find("<%= this.RcDocumentTypes.ClientID %>");
                var docTypeValue = docTypeComp.get_selectedItem().get_value();

                var docNumberComp = $find("<%= this.RntNumber.ClientID %>");
                var docNumberValue = docNumberComp.get_value();

                if (yearValue === "< Seleccione >" && applicationValue === "-1" && sinceAmountValue === "" && untilAmountValue === "" && boundValue === "" && squareValue === "" && providerValue === "-1" && providerNifValue === "--" && docTypeValue === "-1" && docNumberValue === "") {
                    return;
                }
                
                var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=spendApplicationAmount';

                if (yearValue !== "< Seleccione >") {
                    url = url + '&year=' + yearValue;
                }

                if (applicationValue !== "-1") {
                    url = url + '&application=' + applicationValue;
                }

                if (sinceAmountValue !== "") {
                    url = url + '&sinceAmount=' + sinceAmountValue;
                }

                if (untilAmountValue !== "") {
                    url = url + '&untilAmount=' + untilAmountValue;
                }

                if (boundValue !== "") {
                    url = url + '&bound=' + boundValue;
                }

                if (squareValue !== "") {
                    url = url + '&square=' + squareValue;
                }

                if (providerValue !== "-1") {
                    url = url + '&provider=' + providerValue;
                }

                if (providerNifValue !== "--") {
                    url = url + '&providerNif=' + providerNifValue;
                }

                if (docTypeValue !== "-1") {
                    url = url + '&docType=' + docTypeValue;
                }

                if (docNumberValue !== "") {
                    url = url + '&docNumber=' + docNumberValue;
                }

                window.open(url, "Reporte");
            }

            function FormatText(date) {
                if (date == null) {
                    return null;
                }

                var dd = date.getDate();
                var mm = date.getMonth() + 1;

                var yyyy = date.getFullYear();
                if (dd < 10) {
                    dd = '0' + dd;
                }
                if (mm < 10) {
                    mm = '0' + mm;
                }

                var result = yyyy + '-' + mm + '-' + dd;
                return result;
            }

        </script>
    </telerik:RadScriptBlock>
</asp:Content>
