<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="ManagePointing.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Pointing.ManagePointing" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <%--Modales--%>
    <telerik:RadWindowManager ID="rwmManagePointing" runat="server">
    </telerik:RadWindowManager>

    <!-- Page Content -->
    <div id="section_pointing" class="container">

        <h3>Consultar Señalamiento</h3>

        <%--MANAGE--%>
        <div class="manage">

            <div class="form-group">
                <%--N⁰ Señal.--%>
                <div class="field-container">
                    <span>N⁰ Señal.:</span>
                    <strong id="TxtPointingNumber" runat="server"></strong>
                </div>

                <%--Año--%>
                <div class="field-container">
                    <span>Año:</span>
                    <strong id="TxtYear" runat="server"></strong>
                </div>

                <%--Fecha--%>
                <div class="field-container">
                    <span>Fecha:</span>
                    <strong id="TxtDate" runat="server"></strong>
                </div>
            </div>

            <%--BUTTONS--%>
            <div class="form-group buttons">
                <telerik:RadButton ButtonType="LinkButton" ID="btnNew"
                    runat="server"
                    RenderMode="Native"
                    Text="Nuevo Señalamiento"
                    AutoPostBack="True"
                    OnClick="btnNew_OnClick">
                </telerik:RadButton>
                <telerik:RadButton ButtonType="LinkButton" ID="btnDelete"
                    runat="server"
                    RenderMode="Native"
                    Text="Eliminar"
                    AutoPostBack="true"
                    OnClick="btnDelete_OnClick">
                </telerik:RadButton>
                <telerik:RadButton ButtonType="LinkButton" ID="btnBack"
                    runat="server"
                    RenderMode="Native"
                    Text="Volver"
                    AutoPostBack="True"
                    OnClick="btnBack_OnClick">
                </telerik:RadButton>
            </div>

            <div id="page_managePointing" class="box-block">
                <telerik:RadAjaxPanel ID="rapManagePointing"
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

                    <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgDocuments"
                        runat="server"
                        AllowSorting="true"
                        Culture="es-ES"
                        GroupPanelPosition="Top"
                        OnNeedDataSource="RgDocuments_OnNeedDataSource"
                        OnDeleteCommand="RgDocuments_OnDeleteCommand"
                        ClientSettings-ClientEvents-OnCommand="OnCommand"
                        CssClass="managePointing-table">

                        <GroupingSettings CaseSensitive="false" />

                        <MasterTableView AutoGenerateColumns="false"
                            AllowFilteringByColumn="true"
                            DataKeyNames="SEND_CODIGO, DOC_CODIGO"
                            ClientDataKeyNames="NUMERO_DOCUMENTO"
                            CommandItemDisplay="Top"
                            AllowPaging="false"
                            PagerStyle-AlwaysVisible="false"
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
                                <telerik:GridBoundColumn UniqueName="NUMERO_DOCUMENTO"
                                    DataField="NUMERO_DOCUMENTO"
                                    HeaderText="N⁰ Exp."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="75px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CONCEPTO"
                                    DataField="CONCEPTO"
                                    HeaderText="Concepto"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="100px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PERCEPTOR"
                                    DataField="PERCEPTOR"
                                    HeaderText="Perceptor"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="IMPORTE_INTEGRO"
                                    DataField="IMPORTE_INTEGRO_LABEL"
                                    HeaderText="Integro"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="TOTAL_DESCUENTOS"
                                    DataField="TOTAL_DESCUENTOS_LABEL"
                                    HeaderText="Descuentos"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="IMPORTE_LIQUIDO"
                                    DataField="IMPORTE_LIQUIDO_LABEL"
                                    HeaderText="Líquido"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="TOTAL_TRANSFERIDO"
                                    DataField="TOTAL_IMPORTE_LIQUIDO_LABEL"
                                    HeaderText="Total Transf."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NUMERO_CHEQUE"
                                    DataField="NUMERO_CHEQUE"
                                    HeaderText="N⁰ Transf."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="150px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn UniqueName="DeleteColumn"
                                    ButtonType="LinkButton"
                                    HeaderTooltip="Eliminar Apunte"
                                    CommandName="DeleteDocument"
                                    HeaderStyle-Width="40px"
                                    ItemStyle-Width="40px"
                                    ItemStyle-CssClass="fas fa-trash-alt"
                                    Text=" "
                                    HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridButtonColumn>
                                <%--                                <telerik:GridButtonColumn UniqueName="DeleteColumn"
                                    ButtonType="LinkButton"
                                    HeaderTooltip="Eliminar Documento"
                                    CommandName="Delete"
                                    HeaderStyle-Width="40px"
                                    ItemStyle-Width="40px"
                                    ItemStyle-CssClass="fas fa-trash-alt"
                                    Text=" " ConfirmDialogType="RadWindow"
                                    ConfirmTitle="ATENCIÓN"
                                    ConfirmText="¿ Está seguro que desea eliminar este Documento del Señalamiento en cuestión ?"
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

                    <div class="monto-total-managePointing">
                        <div class="fullAmount">
                            <strong>Total Integro:</strong>
                            <telerik:RadTextBox ID="txtFullAmount"
                                Width="120px"
                                runat="server"
                                MaxLength="80"
                                Text="0,00"
                                Enabled="False">
                            </telerik:RadTextBox>
                        </div>

                        <div class="liquidAmount">
                            <strong>Total Líquido:</strong>
                            <telerik:RadTextBox ID="txtLiquidAmount"
                                Width="120px"
                                runat="server"
                                MaxLength="80"
                                Text="0,00"
                                Enabled="False">
                            </telerik:RadTextBox>
                        </div>

                        <telerik:RadCheckBox ID="checkGroup"
                            runat="server"
                            Checked="False"
                            Text="Agrupar Transf."
                            AutoPostBack="false">
                        </telerik:RadCheckBox>

                        <div class="field-container buttons">
                            <telerik:RadButton ButtonType="LinkButton" ID="btnTransfers"
                                runat="server"
                                RenderMode="Native"
                                Text="Transferencias"
                                AutoPostBack="True"
                                OnClick="btnTransfers_OnClick">
                            </telerik:RadButton>
                            <telerik:RadButton ButtonType="LinkButton" ID="btnReport"
                                runat="server"
                                RenderMode="Native"
                                Text="Imprimir"
                                AutoPostBack="True"
                                OnClick="btnReport_OnClick">
                            </telerik:RadButton>
                        </div>
                    </div>
                </telerik:RadAjaxPanel>
            </div>
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

            function hideGrids() {
                $('#<%=this.btnDelete.ClientID %>').hide();
                $('#page_manageFile').hide();
            }

            function confirmDeleteCallBackFn(arg) {
                if (arg == null || arg == false) {
                    return;
                }

                $.ajax({
                    type: "POST",
                    url: "ManagePointing.aspx/DeletePointing",
                    data: null,
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    success: function (result) {
                        switch (result.d) {
                            case 0:
                                radalert("Ha ocurrido un error eliminando el Señalamiento en cuestión.", 330, 140, "Imposible eliminar señalamiento", null, null);
                                break;
                            case 1:
                                var url = window.location.origin + '\\Views\\Pointing\\Sings.aspx';
                                window.location.href = url;
                                break;
                            case 2:
                                radalert("No se puede eliminar el Señalamiento en cuestión debido a que no se ha encontrado en la BD.", 330, 140, "Imposible eliminar señalamiento", null, null);
                                break;
                            case 3:
                                radalert("No se puede eliminar el Señalamiento en cuestión debido a que no se ha creado aún en la BD.", 330, 140, "Imposible eliminar señalamiento", null, null);
                                break;
                        }
                    }, error: function (xhr, ajaxOptions, thrownError) {
                        radalert("No se puede eliminar el Señalamiento en cuestión debido a un error inesperado.", 330, 140, "Imposible eliminar señalamiento", null, null);
                    }
                });
            }

            var radLoadingPanel = null;
            var currentUpdatedControl = null;

            $(document)
                .ajaxStart(function () {
                    radLoadingPanel = $find("ralPrincipal");
                    currentUpdatedControl = "section_pointing";
                    radLoadingPanel.show(currentUpdatedControl);
                })
                .ajaxStop(function () {
                    if (radLoadingPanel != null) {
                        radLoadingPanel.hide(currentUpdatedControl);
                    }
                    radLoadingPanel = null;
                    currentUpdatedControl = null;
                });

            var itemIndex;
            function OnCommand(sender, eventArgs) {
                if (eventArgs.get_commandName() == "DeleteDocument") {
                    var grid = sender;
                    var masterTable = grid.get_masterTableView();
                    itemIndex = eventArgs.get_commandArgument();
                    var row = masterTable.get_dataItems()[itemIndex];

                    var number = row.getDataKeyValue("NUMERO_DOCUMENTO");

                    radconfirm("¿ Desea eliminar realmente el documento n⁰ " + number + " del señalamiento en cuestión ?",
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
                    var masterTable = $find("<%= this.RgDocuments.ClientID %>").get_masterTableView();
                    masterTable.fireCommand("Delete", itemIndex);
                }
            }

            function showReport(pointingId, pointingNumber, pointingYear) {
                debugger 
                var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=pointingCurrentReport&pointingId=' + pointingId;

                if (pointingNumber !== "") {
                    url = url + '&pointingNumber=' + pointingNumber;
                }

                if (pointingYear !== "") {
                    url = url + '&pointingYear=' + pointingYear;
                }

                window.open(url, "Reporte");
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
