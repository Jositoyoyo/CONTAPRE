<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="ManageTonnageSheet.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Treasury.ManageTonnageSheet" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <%--Modales--%>
    <telerik:RadWindowManager ID="rwmManageTonnageSheet" runat="server">
    </telerik:RadWindowManager>

    <!-- Page Content -->
    <div id="section_treasury" class="container">

        <h3>Modificar Hoja de Arqueo</h3>

        <div id="page_manageTonnageSheet" class="box-block">

            <telerik:RadAjaxPanel ID="rapManageTonnageSheet" runat="server"
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

                <div class="manage">

                    <div class="form-group form-group-fake">
                        <%--Año--%>
                        <div class="field-container">
                            <span>Año:</span>
                            <strong id="TxtYear" runat="server"></strong>
                        </div>

                        <%--N⁰ Hoja--%>
                        <div class="field-container">
                            <span>N⁰ Hoja:</span>
                            <strong id="TxtSheetNumber" runat="server"></strong>
                        </div>

                        <%--Fecha Hoja--%>
                        <div class="field-container">
                            <span>Fecha Hoja:</span>
                            <strong id="TxtSheetDate" runat="server"></strong>
                        </div>

                        <%--Fecha SICAI--%>
                        <div class="field-container">
                            <span>Fecha SICAI:</span>
                            <strong id="TxtSicaiDate" runat="server"></strong>
                        </div>

                        <%--N⁰ Hoja--%>
                        <div class="field-container">
                            <span>N⁰ SICAI:</span>
                            <strong id="TxtSicaiNumber" runat="server"></strong>
                        </div>
                    </div>

                    <%--BUTTONS--%>
                    <div class="form-group buttons">
                        <div class="delete">
                            <span class="icon"></span>
                            <telerik:RadButton ButtonType="LinkButton" ID="btnDelete"
                                runat="server"
                                RenderMode="Native"
                                Text="Eliminar"
                                AutoPostBack="True"
                                OnClick="btnDelete_OnClick">
                            </telerik:RadButton>
                        </div>

                        <div class="report">
                            <span class="icon"></span>
                            <telerik:RadButton ButtonType="LinkButton" ID="btnReport"
                                runat="server"
                                RenderMode="Native"
                                Text="Imprimir"
                                AutoPostBack="True"
                                OnClick="btnReport_OnClick">
                            </telerik:RadButton>
                        </div>

                        <div class="back">
                            <span class="icon"></span>
                            <telerik:RadButton ButtonType="LinkButton" ID="btnBack"
                                runat="server"
                                RenderMode="Native"
                                Text="Volver"
                                AutoPostBack="True"
                                OnClick="btnBack_OnClick">
                            </telerik:RadButton>
                        </div>
                    </div>
                </div>

                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True"
                    ID="RgTonnageSheetDetails"
                    runat="server"
                    AllowSorting="true"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgTonnageSheetDetails_OnNeedDataSource"
                    OnDeleteCommand="RgTonnageSheetDetails_OnDeleteCommand"
                    ClientSettings-ClientEvents-OnCommand="OnCommand"
                    CssClass="manageTonnageSheet-table">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="false"
                        AllowFilteringByColumn="true"
                        DataKeyNames="DET_CODIGO"
                        ClientDataKeyNames="AmountLabel, DateLabel"
                        CommandItemDisplay="Top"
                        AllowPaging="false"
                        PagerStyle-AlwaysVisible="false"
                        NoMasterRecordsText="No Hay datos a Mostrar."
                        TableLayout="Fixed"
                        EditMode="PopUp"
                        CssClass="popup-table big-table">

                        <CommandItemSettings ShowRefreshButton="False"
                            ShowExportToExcelButton="False"
                            ShowExportToPdfButton="False"
                            ShowAddNewRecordButton="False"
                            AddNewRecordText="Nuevo" />

                        <PagerStyle Mode="NextPrevAndNumeric"
                            PageSizeLabelText="Elementos por pagina: "
                            PagerTextFormat="Navigate pages {4} Página {0} de {1}, elementos {2} a {3} de {5}" />

                        <Columns>
                            <telerik:GridBoundColumn UniqueName="DET_FECHA_APUNTE"
                                DataField="DET_FECHA_APUNTE"
                                HeaderText="Fecha Apunte"
                                AllowFiltering="true"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="GreaterThanOrEqualTo"
                                ShowFilterIcon="false"
                                DataFormatString="{0:dd/MM/yyyy}"
                                FilterControlWidth="90%">
                                <FilterTemplate>
                                    <telerik:RadDatePicker ID="FilteredDatePickerSheet"
                                        RenderMode="Lightweight"
                                        runat="server"
                                        Width="100%"
                                        ClientEvents-OnDateSelected="DateSelectedSheet"
                                        DbSelectedDate='<%#this.SetFilteredDate(Container, "DET_FECHA_APUNTE") %>'
                                        Culture="es-ES" />
                                    <telerik:RadScriptBlock ID="RadScriptBlockSheet"
                                        runat="server">
                                        <script id="scriptSheet"
                                            type="text/javascript">
                                            function DateSelectedSheet(sender, args) {
                                                var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                var date = FormatSelectedDateSheet(sender);
                                                tableView.filter("DET_FECHA_APUNTE", date, "GreaterThanOrEqualTo");
                                            }
                                            function FormatSelectedDateSheet(picker) {
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
                            <%--<telerik:GridBoundColumn UniqueName="LIN_NUMERO"
                                DataField="LIN_NUMERO"
                                HeaderText="Línea Tesorería"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn UniqueName="LIN_NUMERO"
                                DataField="LIN_NUMERO"
                                HeaderText="Línea Tesorería"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                DataType="System.Int32">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn UniqueName="DET_NUMERO_EXPEDIENTE"
                                DataField="DET_NUMERO_EXPEDIENTE"
                                HeaderText="N⁰ Documento"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn UniqueName="DET_NUMERO_EXPEDIENTE"
                                DataField="DET_NUMERO_EXPEDIENTE"
                                HeaderText="N⁰ Documento"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                DataType="System.Int32">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn UniqueName="AmountLabel"
                                DataField="AmountLabel"
                                HeaderText="Importe"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right">
                                <HeaderStyle Width="40%" />
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
                                <HeaderStyle Width="40%" />
                            </telerik:GridNumericColumn>
                            <telerik:GridButtonColumn UniqueName="DeleteColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Eliminar Apunte"
                                CommandName="DeleteTonnageSheetDetail"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-trash-alt"
                                Text=" "
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>
                            <%--<telerik:GridButtonColumn UniqueName="DeleteColumn"
                                ButtonType="LinkButton"
                                HeaderTooltip="Eliminar Apunte"
                                CommandName="Delete"
                                HeaderStyle-Width="40px"
                                ItemStyle-Width="40px"
                                ItemStyle-CssClass="fas fa-trash-alt"
                                Text=" "
                                ConfirmDialogType="RadWindow"
                                ConfirmTitle="ATENCIÓN"
                                ConfirmText="¿ Está seguro que desea eliminar este Apunte de Hoja de Arqueo ?"
                                ConfirmDialogHeight="100px"
                                HeaderStyle-HorizontalAlign="Center">
                            </telerik:GridButtonColumn>--%>
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

            function confirmDeleteCallBackFn(arg) {
                if (arg == null || arg == false) {
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: "ManageTonnageSheet.aspx/DeleteTonnageSheet",
                    data: null,
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    success: function (result) {
                        switch (result.d) {
                            case 0:
                                radalert("Ha ocurrido un error eliminando la Hoja de Arqueo en cuestión.", 330, 140, "Imposible eliminar hoja de arqueo", null, null);
                                break;
                            case 1:
                                var url = window.location.origin + '\\Views\\Treasury\\SeeTonnageSheets.aspx';
                                window.location.href = url;
                                break;
                            case 2:
                                radalert("No se puede eliminar la Hoja de Arqueo en cuestión debido a que no se ha encontrado en la BD.", 330, 140, "Imposible eliminar hoja de arqueo", null, null);
                                break;
                        }
                    }, error: function (xhr, ajaxOptions, thrownError) {
                        radalert("No se puede eliminar la Hoja de Arqueo en cuestión debido a un error inesperado.", 330, 140, "Imposible eliminar hoja de arqueo", null, null);
                    }
                });
            }

            function btnReportOnClientClicked(sender, eventArgs) {
            }

            var itemIndex;
            function OnCommand(sender, eventArgs) {
                if (eventArgs.get_commandName() == "DeleteTonnageSheetDetail") {
                    var grid = sender;
                    var masterTable = grid.get_masterTableView();
                    itemIndex = eventArgs.get_commandArgument();
                    var row = masterTable.get_dataItems()[itemIndex];

                    var amount = row.getDataKeyValue("AmountLabel");
                    var date = row.getDataKeyValue("DateLabel");

                    radconfirm("¿ Desea eliminar realmente el apunte por " + amount + " €, con fecha " + date + " ?",
                        confirmDeleteGridCallBackFn,
                        500,
                        220,
                        null,
                        "Confirmación",
                        null);
                }
            }

            function confirmDeleteGridCallBackFn(arg) {
                if (arg == null || arg == false) {
                    return;
                }

                if (itemIndex) {
                    var masterTable = $find("<%= this.RgTonnageSheetDetails.ClientID %>").get_masterTableView();
                    masterTable.fireCommand("Delete", itemIndex);
                }
            }

            function printTonnageSheet(tonnageSheetCode, sheetNumber, is50, date) {
                var urlSpend = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=tonnageSheetDetail&tonnageSheetCode= ' + tonnageSheetCode + '&sheetNumber=' + sheetNumber + '&is50=' + is50
                    + '&date=' + date;
                window.open(urlSpend, "Imprimir Hoja de Arqueo");
            }

        </script>
    </telerik:RadScriptBlock>
</asp:Content>
