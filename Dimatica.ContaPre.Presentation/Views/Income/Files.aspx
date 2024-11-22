<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="Files.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Income.Files" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <%--Modales--%>
    <telerik:RadWindowManager ID="rwmFiles" runat="server">
        <Windows>

            <%--estado provisional--%>
            <telerik:RadWindow ID="rwFilesOrder"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Listado de Aplicaciones e Importe"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="320"
                Height="210"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientFilesOrderCloseHandler">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <!-- Page Content -->
    <div id="section_income" class="container">

        <h3>Expedientes de Ingresos</h3>

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
                <div class="new">
                    <span class="icon"></span>
                    <a href="ManageFile.aspx" role="button">Nuevo</a>
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

        <div id="page_files" class="box-block">

            <telerik:RadAjaxPanel ID="rapFiles" runat="server"
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
                                            <telerik:RadComboBox ID="RcYears"
                                                Width="130px"
                                                runat="server"
                                                AutoPostBack="True"
                                                OnSelectedIndexChanged="RcYears_OnSelectedIndexChanged"
                                                DataTextField="Value"
                                                DataValueField="Value">
                                            </telerik:RadComboBox>
                                        </div>
                                        <div class="field-container">
                                            <span>Descripción:</span>
                                            <telerik:RadTextBox ID="RtbDescription"
                                                Width="200px"
                                                runat="server"
                                                MaxLength="60"
                                                Text="">
                                            </telerik:RadTextBox>
                                        </div>
                                        <div class="field-container">
                                            <span>Terminado:</span>
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
                                            <span>Aplic.</span>
                                            <telerik:RadComboBox ID="RcApplications"
                                                runat="server"
                                                Width="250px"
                                                AutoPostBack="False"
                                                DataTextField="Description"
                                                DataValueField="CacsCode">
                                            </telerik:RadComboBox>
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
                                            <span>Ejercicio</span>
                                            <telerik:RadNumericTextBox ID="RntOperationYear"
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
                                            <span>Proveedor:</span>
                                            <telerik:RadComboBox ID="RcProviders"
                                                runat="server"
                                                Width="250px"
                                                AutoPostBack="False"
                                                DataTextField="PROV_NOMBRE"
                                                DataValueField="PROV_CODIGO">
                                            </telerik:RadComboBox>
                                        </div>
                                        <div class="field-container">
                                            <span style="display: block">Tipo:</span>
                                            <telerik:RadComboBox ID="RcDocumentTypes"
                                                runat="server"
                                                Width="150px"
                                                AutoPostBack="False"
                                                DataTextField="TIPD_NOMBRE_CORTO"
                                                DataValueField="TIPD_DESCRIPCION">
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
                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgFiles"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgFiles_OnNeedDataSource"
                    OnPreRender="RgFiles_OnPreRender"
                    OnItemCommand="RgFiles_OnItemCommand"
                    OnItemDataBound="RgFiles_OnItemDataBound"
                    CssClass="incomeRecords-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="EXP_CODIGO"
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
                                DataField="EXP_ANO_PRESUPUESTO"
                                HeaderText="Año Pto."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="FileNumber"
                                DataField="EXP_NUMERO"
                                HeaderText="N⁰"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="50px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="FileDescription"
                                DataField="EXP_DESCRIPCION"
                                HeaderText="Descripción"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ProviderName"
                                DataField="PROV_NOMBRE"
                                HeaderText="Proveedor"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Application"
                                DataField="CACS_NUMERO"
                                HeaderText="Aplic."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="DocType"
                                DataField="TIPO_DOC"
                                HeaderText="T. Doc."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn UniqueName="Amount"
                                DataField="AmountLabel"
                                HeaderText="Importe"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridNumericColumn UniqueName="Amount"
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
                            <telerik:GridBoundColumn UniqueName="Bound"
                                DataField="DOC_ENLAZADO_TESORERIA_LABEL"
                                HeaderText="Enlaz."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PaperTonnage"
                                DataField="HOJ_NUMERO"
                                HeaderText="H. Arq."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="OperationYear"
                                DataField="EJERCICIO"
                                HeaderText="Ejer."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="75px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridImageColumn DataType="System.String"
                                DataImageUrlFields="SquareImage"
                                DataImageUrlFormatString="/Content/Images/{0}.png"
                                HeaderText="Cuad."
                                ImageAlign="Baseline"
                                ImageHeight="20px"
                                ImageWidth="20px"
                                HeaderStyle-ForeColor="White"
                                HeaderStyle-Width="40px"
                                AllowFiltering="false">
                            </telerik:GridImageColumn>
                            <telerik:GridButtonColumn UniqueName="EditColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Editar expediente"
                                CommandName="UpdateAccountingRecord"
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
                <div class="monto-total-incomeRecords">
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
                var url = "SelectFilesOrder.aspx";
                var manager = $find("<%= this.rwmFiles.ClientID %>");
                var oWnd = manager.open(url, "rwFilesOrder");
            }

            function OnClientFilesOrderCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    var title = data.title;
                    var order = data.order;

                    var yearComp = $find("<%= this.RcYears.ClientID %>");
                    var yearValue = yearComp.get_value();
                    if (yearValue === "< Seleccione >") {
                        yearValue = "-1";
                        //radalert("No se pueden ver los Expedientes de Ingresos debido a que el año esta vacío.", 330, 140, "Imposible ver Expedientes", null, null);
                        //return;
                    }

                    var descriptionComp = $find("<%= this.RtbDescription.ClientID %>");
                    var descriptionValue = descriptionComp.get_value();

                    var squareComp = $find("<%= this.RcSquare.ClientID %>");
                    var squareValue = squareComp.get_value();

                    var providerComp = $find("<%= this.RcProviders.ClientID %>");
                    var providerValue = providerComp.get_value();

                    var exerciseComp = $find("<%= this.RntOperationYear.ClientID %>");
                    var exerciseValue = exerciseComp.get_value();

                    var typeCodeComp = $find("<%= this.RcDocumentTypes.ClientID %>");
                    var typeCodeValue = typeCodeComp.get_value();

                    var numberComp = $find("<%= this.RntNumber.ClientID %>");
                    var numberValue = numberComp.get_value();

                    var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=incomeFiles&title=' + title + '&order=' + order;

                    if (yearValue !== "-1") {
                        url = url + '&budgetYear=' + yearValue;
                    }

                    if (descriptionValue !== "") {
                        url = url + '&description=' + descriptionValue;
                    }

                    if (squareValue !== "") {
                        url = url + '&square=' + squareValue;
                    }

                    if (providerValue !== "-1") {
                        url = url + '&provider=' + providerValue;
                    }

                    if (exerciseValue !== "") {
                        url = url + '&exerciseYear=' + exerciseValue;
                    }

                    if (typeCodeValue !== "-1") {
                        url = url + '&typeCode=' + typeCodeValue;
                    }

                    if (numberValue !== "") {
                        url = url + '&docNumber=' + numberValue;
                    }

                    window.open(url, "Reporte");
                }
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
