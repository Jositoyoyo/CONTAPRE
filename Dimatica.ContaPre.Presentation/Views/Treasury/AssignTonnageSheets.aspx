<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="AssignTonnageSheets.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Treasury.AssignTonnageSheets" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <!-- Page Content -->
    <div id="section_treasury" class="container">

        <h3>Asignar Hoja Arqueo</h3>

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
                        Text="Buscar"
                        OnClick="btnFind_OnClick">
                    </asp:LinkButton>
                </div>
            </div>
        </div>

        <div id="page_assignTonnageSheets" class="box-block">

            <telerik:RadAjaxPanel ID="rapAssignTonnageSheets"
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
                            Expanded="True">
                            <ContentTemplate>
                                <div class="manage">
                                    <div class="form-group form-group form-group-fake">
                                        <div class="field-container">
                                            <telerik:RadRadioButtonList ID="RrbFifty"
                                                runat="server">
                                                <Items>
                                                    <telerik:ButtonListItem Text="Normal"
                                                        Value="0"
                                                        Selected="true" />
                                                    <telerik:ButtonListItem Text="Línea 50"
                                                        Value="1" />
                                                </Items>
                                            </telerik:RadRadioButtonList>
                                        </div>

                                        <div class="field-container">
                                            <span>Año:</span>
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
                                            <span>N⁰ Hoja Arqueo:</span>
                                            <telerik:RadNumericTextBox ID="RntSheetNumber"
                                                runat="server"
                                                RenderMode="Lightweight"
                                                Width="75px"
                                                MinValue="0"
                                                MaxValue="9999"
                                                MaxLength="4"
                                                ShowSpinButtons="False"
                                                NumberFormat-DecimalDigits="0">
                                            </telerik:RadNumericTextBox>
                                            <asp:RequiredFieldValidator ID="rfvSheetNumber"
                                                runat="server"
                                                Display="Dynamic"
                                                ControlToValidate="RntSheetNumber"
                                                ErrorMessage=" * Introduzca el número de hoja de arqueo."
                                                ToolTip="Introduzca el número de hoja de arqueo."
                                                ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>

                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgTonnageSheet"
                    runat="server"
                    Visible="False"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgTonnageSheet_NeedDataSource"
                    OnPreRender="RgTonnageSheet_OnPreRender"
                    AllowMultiRowSelection="True"
                    CssClass="assignTonnageSheet-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="DET_CODIGO, EXP_EXTRAP_CODIGO, MARCADO, DET_FECHA_APUNTE, DET_IMPORTE, LIN_NUMERO, DET_NUMERO_EXPEDIENTE, EXP_ANO_PRESUPUESTO, CUE_CODIGO"
                        CommandItemDisplay="Top"
                        AllowPaging="true"
                        PagerStyle-AlwaysVisible="true"
                        PageSize="100"
                        NoMasterRecordsText="No Hay datos a Mostrar."
                        TableLayout="Fixed">

                        <CommandItemSettings AddNewRecordText="Nuevo"
                            ShowAddNewRecordButton="false"
                            ShowRefreshButton="True"
                            ShowExportToExcelButton="True"
                            ShowExportToPdfButton="False" />

                        <PagerStyle Mode="NextPrevAndNumeric"
                            PageSizeLabelText="Elementos por pagina: "
                            PagerTextFormat="Navigate pages {4} Página {0} de {1}, elementos {2} a {3} de {5}" />

                        <Columns>
                            <telerik:GridBoundColumn UniqueName="DET_FECHA_APUNTE"
                                DataField="DET_FECHA_APUNTE"
                                HeaderText="Fecha"
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
                            <telerik:GridBoundColumn UniqueName="LIN_NUMERO"
                                DataField="LIN_NUMERO"
                                HeaderText="Línea Tesorería"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="DET_NUMERO_EXPEDIENTE"
                                DataField="DET_NUMERO_EXPEDIENTE"
                                HeaderText="M.I."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>
                      <%--      <telerik:GridBoundColumn UniqueName="DET_IMPORTE_LABEL"
                                DataField="DET_IMPORTE_LABEL"
                                HeaderText="Importe"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="GreaterThanOrEqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridNumericColumn UniqueName="DET_IMPORTE"
                                DataField="DET_IMPORTE"
                                HeaderText="Importe"
                                DataFormatString="{0:N}"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right"
                                FilterControlWidth="100%">
                                <HeaderStyle Width="100px" />
                            </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn UniqueName="CUE_DESCRIPCION"
                                DataField="CUE_DESCRIPCION"
                                HeaderText="Cuenta Restringida"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridClientSelectColumn UniqueName="SelectColumn"
                                HeaderText="Marcado">
                                <HeaderStyle Width="40px" />
                            </telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn UniqueName="EXP_ANO_PRESUPUESTO"
                                DataField="EXP_ANO_PRESUPUESTO"
                                HeaderText="Año Presup."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="150px" />
                            </telerik:GridBoundColumn>
                        </Columns>

                        <EditFormSettings EditFormType="Template">
                            <FormTemplate>
                            </FormTemplate>
                        </EditFormSettings>

                    </MasterTableView>

                    <ClientSettings>
                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                        <Selecting AllowRowSelect="True" UseClientSelectColumnOnly="True" />
                    </ClientSettings>

                </telerik:RadGrid>

                <div class="manage" id="createReport" runat="server" visible="false">
                    <div class="form-group form-group-fake">
                        <div class="field-container">
                            <strong>Fase I:</strong>
                            <span>Marcar los apuntes de Tesorería que quiere asignar y pulse [Grabar].</span>
                        </div>
                        <div class="field-container">
                            <strong>Fase II:</strong>
                            <div style="display: flex">
                                <span>Fecha:</span>
                                <telerik:RadDatePicker ID="RdDate"
                                    RenderMode="Lightweight"
                                    runat="server"
                                    Width="150px"
                                    EnableTyping="False"
                                    AutoPostBack="False"
                                    Culture="es-ES"
                                    MaxDate="12/31/9999"
                                    MinDate="01/01/1800" />
                            </div>
                        </div>

                        <div class="field-container buttons">
                            <telerik:RadButton ButtonType="LinkButton" ID="btnReport"
                                runat="server"
                                RenderMode="Native"
                                Text="Hacer Hojas"
                                AutoPostBack="True"
                                OnClick="btnReport_OnClick">
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

                    function printTonnageSheet(tonnageSheetCode, sheetNumber, is50, date) {
                        var urlSpend = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=tonnageSheetDetail&tonnageSheetCode= ' + tonnageSheetCode + '&sheetNumber=' + sheetNumber + '&is50=' + is50
                            + '&date=' + date;
                        window.open(urlSpend, "Imprimir Hoja de Arqueo");
                    }

                </script>
            </telerik:RadScriptBlock>

        </div>
    </div>
</asp:Content>
