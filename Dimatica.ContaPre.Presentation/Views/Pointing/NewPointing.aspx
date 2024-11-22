<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="NewPointing.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Pointing.NewPointing" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <!-- Page Content -->
    <div id="section_pointing" class="container">

        <h3>Nuevo Señalamiento</h3>

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
                <div class="report">
                    <span class="icon"></span>
                    <asp:LinkButton ID="btnReport"
                        runat="server"
                        OnClientClick="btnReportOnClientClick();return false;"
                        Text="Listado">
                    </asp:LinkButton>
                </div>
            </div>
        </div>

        <div id="page_newPointing" class="box-block">
            <telerik:RadAjaxPanel ID="rapNewPointing"
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
                                            <span>Año Presupuesto:</span>
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
                                            <span>Tipo Documento:</span>
                                            <telerik:RadComboBox ID="RcbType"
                                                runat="server"
                                                Width="130px">
                                                <Items>
                                                    <telerik:RadComboBoxItem runat="server" Text="< Seleccione >" Value="-1" />
                                                    <telerik:RadComboBoxItem runat="server" Text="OP" Value="OP" />
                                                    <telerik:RadComboBoxItem runat="server" Text="ADOP" Value="ADOP" />
                                                    <telerik:RadComboBoxItem runat="server" Text="PMP" Value="PMP" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>

                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgDocuments"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgDocuments_NeedDataSource"
                    OnPreRender="RgDocuments_PreRender"
                    AllowMultiRowSelection="False"
                    CssClass="newPointing-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="DOC_CODIGO_AUX, DOC_FECHA_ASIENTO_DIARIO, EXP_EXTRAP_CODIGO, NUMERO_DOCUMENTO, TIPO_DOCUMENTO, CONCEPTO, PROV_NOMBRE, LIQUIDO, DOC_NUMERO_CHEQUE, doc_reparado, tiene_irpf, Select, Repair"
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
                            <telerik:GridBoundColumn UniqueName="NUMERO_DOCUMENTO"
                                DataField="NUMERO_DOCUMENTO"
                                HeaderText="N⁰ Exp."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="GreaterThanOrEqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="50px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TIPO_DOCUMENTO"
                                DataField="TIPO_DOCUMENTO"
                                HeaderText="Tipo Doc."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="50px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CONCEPTO"
                                DataField="CONCEPTO"
                                HeaderText="Concepto"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PROV_NOMBRE"
                                DataField="PROV_NOMBRE"
                                HeaderText="Perceptor"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn UniqueName="AmountLabel"
                                DataField="AmountLabel"
                                HeaderText="Líquido"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridNumericColumn UniqueName="AmountLabel"
                                DataField="LIQUIDO"
                                HeaderText="Líquido"
                                DataFormatString="{0:N}"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right"
                                FilterControlWidth="100%">
                                <HeaderStyle Width="150px" />
                            </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn UniqueName="DOC_NUMERO_CHEQUE"
                                DataField="DOC_NUMERO_CHEQUE"
                                HeaderText="N⁰ Cheque"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="Repair"
                                HeaderText="Reparado"
                                AllowFiltering="False"
                                ShowFilterIcon="False">
                                <ItemTemplate>
                                    <telerik:RadCheckBox ID="checkRepair"
                                        runat="server"
                                        Checked='<%#this.Eval("Repair") %>'
                                        Text=""
                                        AutoPostBack="false">
                                    </telerik:RadCheckBox>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="Select"
                                HeaderText="Añadir"
                                AllowFiltering="False"
                                ShowFilterIcon="False">
                                <ItemTemplate>
                                    <telerik:RadCheckBox ID="checkSelect"
                                        runat="server"
                                        Checked='<%#this.Eval("Select") %>'
                                        Text=""
                                        AutoPostBack="false">
                                    </telerik:RadCheckBox>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                            </telerik:GridTemplateColumn>
                            <%--<telerik:GridClientSelectColumn UniqueName="SelectColumn"
                                HeaderText="Alta">
                                <HeaderStyle Width="40px" />
                            </telerik:GridClientSelectColumn>--%>
                        </Columns>
                    </MasterTableView>

                    <ClientSettings>
                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                        <%--<Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True" />--%>
                    </ClientSettings>
                </telerik:RadGrid>

                <%--MANAGE--%>
                <div class="manage">

                    <div class="form-group form-group-horizontal form-group-fake">

                        <div class="field-container">
                            <span>N⁰ Señalamiento:</span>
                            <telerik:RadTextBox ID="RtbPointingNumber"
                                Width="50px"
                                runat="server"
                                MaxLength="10"
                                Text=""
                                Enabled="False">
                            </telerik:RadTextBox>
                        </div>

                        <div class="field-container">
                            <span>Fecha:</span>
                            <telerik:RadTextBox ID="RtbDate"
                                Width="100px"
                                runat="server"
                                MaxLength="10"
                                Text=""
                                Enabled="False">
                            </telerik:RadTextBox>
                        </div>

                        <div class="field-container">
                            <span>TOTAL:</span>
                            <telerik:RadTextBox ID="RtbAmount"
                                Width="120px"
                                runat="server"
                                MaxLength="80"
                                Text="0,00"
                                Enabled="False">
                            </telerik:RadTextBox>
                        </div>

                        <div class="field-container buttons">
                            <telerik:RadButton ButtonType="LinkButton" ID="btnGenerate"
                                runat="server"
                                RenderMode="Native"
                                Text="Generar Señalamiento"
                                AutoPostBack="True"
                                OnClick="btnGenerate_OnClick">
                            </telerik:RadButton>
                            <telerik:RadButton ButtonType="LinkButton" ID="btnTotal"
                                runat="server"
                                RenderMode="Native"
                                Text="Calcular Totales"
                                AutoPostBack="True"
                                OnClick="btnTotal_OnClick">
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

                    function seeTotal(total) {
                        radalert("La suma total de los documentos seleccionados es: " + total, 330, 140, "Total", null, null);
                    }

                    function btnReportOnClientClick(sender, eventArgs) {
                        var yearComp = $find("<%= this.RmyYear.ClientID %>");
                        var yearValue = yearComp.get_selectedDate().getFullYear();

                        var typeComp = $find("<%= this.RcbType.ClientID %>");
                        var typeValue = typeComp.get_selectedItem().get_value();

                        var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=pointingPendingDocs&year=' + yearValue;

                        if (typeValue != "-1") {
                            url = url + '&type=' + typeValue;
                        }

                        window.open(url, "Reporte");
                    }

                </script>
            </telerik:RadScriptBlock>
        </div>
    </div>
</asp:Content>
