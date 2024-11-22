<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="Sings.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Pointing.Sings" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <!-- Page Content -->
    <div id="section_pointing" class="container">

        <h3>Listado de Señalamientos</h3>

        <%--BUTTONS--%>
        <div class="top-buttons">
            <div class="a-buttons">
                <div class="new">
                    <span class="icon"></span>
                    <a href="NewPointing.aspx" role="button">Nuevo</a>
                </div>
                <div class="find">
                    <span class="icon"></span>
                    <asp:LinkButton ID="btnFind"
                        runat="server"
                        OnClick="btnFind_Click"
                        Text="Buscar">
                    </asp:LinkButton>
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

        <div id="page_sings" class="box-block">
            <telerik:RadAjaxPanel ID="rapSings"
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
                                            <span>N⁰ Señalamiento:</span>
                                            <telerik:RadNumericTextBox ID="RntPointingNumber"
                                                runat="server"
                                                Width="60px"
                                                RenderMode="Lightweight"
                                                MinValue="0"
                                                ShowSpinButtons="False"
                                                NumberFormat-DecimalDigits="0">
                                            </telerik:RadNumericTextBox>
                                        </div>

                                        <div class="field-container">
                                            <span>Fecha: </span>
                                            <telerik:RadDatePicker ID="RdpDate"
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
                                            <span>Importe: </span>
                                            <telerik:RadNumericTextBox ID="RntAmount"
                                                runat="server"
                                                RenderMode="Lightweight"
                                                Width="75px"
                                                MinValue="0"
                                                ShowSpinButtons="False"
                                                NumberFormat-DecimalDigits="2">
                                            </telerik:RadNumericTextBox>
                                        </div>

                                        <div class="field-container">
                                            <span>N⁰ Expediente:</span>
                                            <telerik:RadNumericTextBox ID="RntFileNumber"
                                                runat="server"
                                                Width="60px"
                                                RenderMode="Lightweight"
                                                MinValue="0"
                                                ShowSpinButtons="False"
                                                NumberFormat-DecimalDigits="0">
                                            </telerik:RadNumericTextBox>
                                        </div>

                                        <div class="field-container">
                                            <span>Tipo: </span>
                                            <telerik:RadComboBox ID="RcType"
                                                runat="server"
                                                Width="130px"
                                                AutoPostBack="False">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="< Seleccione >"
                                                        Value="-1" />
                                                    <telerik:RadComboBoxItem Text="OP"
                                                        Value="OP" />
                                                    <telerik:RadComboBoxItem Text="ADOP"
                                                        Value="ADOP" />
                                                    <telerik:RadComboBoxItem Text="PMP"
                                                        Value="PMP" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>

                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgSings"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgSings_NeedDataSource"
                    OnItemCommand="RgSings_OnItemCommand"
                    OnPreRender="RgSings_PreRender"
                    CssClass="seeTonnageSheet-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="SEN_CODIGO"
                        CommandItemDisplay="Top"
                        AllowPaging="true"
                        PagerStyle-AlwaysVisible="true"
                        PageSize="100"
                        NoMasterRecordsText="No Hay datos a Mostrar."
                        TableLayout="Fixed"
                        EditMode="PopUp"
                        CssClass="popup-table normal-table">

                        <CommandItemSettings AddNewRecordText="Nuevo"
                            ShowAddNewRecordButton="false"
                            ShowRefreshButton="True"
                            ShowExportToExcelButton="True"
                            ShowExportToPdfButton="False" />

                        <PagerStyle Mode="NextPrevAndNumeric"
                            PageSizeLabelText="Elementos por pagina: "
                            PagerTextFormat="Navigate pages {4} Página {0} de {1}, elementos {2} a {3} de {5}" />

                        <Columns>
                            <telerik:GridBoundColumn UniqueName="SEN_NUMERO"
                                DataField="SEN_NUMERO"
                                HeaderText="N⁰ Señal."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="SEN_ANO"
                                DataField="SEN_ANO"
                                HeaderText="Año Presup."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="SEN_FECHA"
                                DataField="SEN_FECHA"
                                HeaderText="Fecha"
                                AllowFiltering="true"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                DataFormatString="{0:dd/MM/yyyy}"
                                FilterControlWidth="90%">
                                <FilterTemplate>
                                    <telerik:RadDatePicker ID="FilteredDatePickerOperation"
                                        RenderMode="Lightweight"
                                        runat="server"
                                        Width="100%"
                                        ClientEvents-OnDateSelected="DateSelectedOperation"
                                        DbSelectedDate='<%#this.SetFilteredDate(Container,"SEN_FECHA") %>'
                                        Culture="es-ES" />
                                    <telerik:RadScriptBlock ID="RadScriptBlockOperation"
                                        runat="server">
                                        <script id="scriptOperation"
                                            type="text/javascript">
                                            function DateSelectedOperation(sender, args) {
                                                var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                var date = FormatSelectedDateOperation(sender);
                                                tableView.filter("SEN_FECHA", date, "EqualTo");
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
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="NUMERO_DOCUMENTOS"
                                DataField="NUMERO_DOCUMENTOS"
                                HeaderText="N⁰ Docs."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn UniqueName="AmountLabel"
                                DataField="AmountLabel"
                                HeaderText="Importe Líquido Total"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="GreaterThanOrEqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridNumericColumn UniqueName="AmountLabel"
                                DataField="SEN_TOTAL_LIQUIDO"
                                HeaderText="Importe Líquido Total"
                                DataFormatString="{0:N}"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right"
                                FilterControlWidth="100%">
                                <HeaderStyle Width="40%" />
                            </telerik:GridNumericColumn>
                            <telerik:GridButtonColumn UniqueName="EditColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Editar"
                                CommandName="UpdatePointing"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-pencil-alt"
                                Text=" "
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>
                        </Columns>

                        <EditFormSettings EditFormType="Template">
                            <FormTemplate>
                            </FormTemplate>
                        </EditFormSettings>

                    </MasterTableView>

                    <ClientSettings>
                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                    </ClientSettings>
                </telerik:RadGrid>
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

                    function btnReportOnClientClick(sender, eventArgs) {

                        var yearComp = $find("<%= this.RmyYear.ClientID %>");
                        var yearValue = yearComp.get_selectedDate().getFullYear();

                        var pointingNumberComp = $find("<%= this.RntPointingNumber.ClientID %>");
                        var pointingNumberValue = pointingNumberComp.get_value();

                        var dateComp = $find("<%= this.RdpDate.ClientID %>");
                        var dateValue = dateComp.get_selectedDate();

                        var amountComp = $find("<%= this.RntAmount.ClientID %>");
                        var amountValue = amountComp.get_value();

                        var fileNumberComp = $find("<%= this.RntFileNumber.ClientID %>");
                        var fileNumberValue = fileNumberComp.get_value();

                        var typeComp = $find("<%= this.RcType.ClientID %>");
                        var typeValue = typeComp.get_selectedItem().get_value();

                        var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=pointingList&year=' + yearValue;

                        if (pointingNumberValue != "") {
                            url = url + '&pointingNumber=' + pointingNumberValue;
                        }

                        if (dateValue != null) {
                            url = url + '&date=' + dateValue;
                        }

                        if (amountValue != "") {
                            url = url + '&amount=' + amountValue;

                        }

                        if (fileNumberValue != "") {
                            url = url + '&fileNumber=' + fileNumberValue;
                        }

                        if (typeValue != "-1") {
                            url = url + '&type=' + typeValue;
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
        </div>
    </div>
</asp:Content>
