<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="SeePurchaseBills.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Spend.SeePurchaseBills" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="/Public/Css/Contapre.css" />

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>

    <script src="/Public/Javascript/jquery-3.7.1.js"></script>
    <title></title>
    <style>
        .buttons {
            justify-content: flex-end;
            display: flex;
        }

        form {
            width: 100%;
            height: 100%;
        }

        .manage {
            padding: 15px !important;
        }

        .p-10 {
            padding: 10px !important;
        }

        .ml-5 {
            margin-left: 5px !important;
        }

        .field-container {
            flex-direction: row !important;
        }
    </style>
</head>
<body class="container">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="rsmSeePurchaseBills"
            runat="server"
            EnablePageMethods="True" />
        <telerik:RadSkinManager ID="rskmSeePurchaseBills"
            runat="server"
            Skin="Metro"
            ShowChooser="false" />
        <telerik:RadWindowManager ID="rwmSeePurchaseBills" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ralSeePurchaseBills"
            runat="server"
            Skin="Material"
            Transparency="0"
            Modal="True">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="rapSeePurchaseBills"
            runat="server"
            LoadingPanelID="ralSeePurchaseBills">
            <div class="manage">
                <div class="form-group form-group-fake">
                    <div class="field-container field-25">
                        <strong id="TxtDescription" runat="server"></strong>
                    </div>
                    <div class="field-container field-25">
                        <span>Expediente Contable:</span>
                        <strong id="TxtAccountingRecord" runat="server"></strong>
                    </div>
                    <div class="field-container field-25">
                        <strong>Total Documento:</strong>
                        <strong id="TxtTotalDocument" runat="server"></strong>
                    </div>
                    <div class="field-container field-25">
                        <strong>Total Facturas:</strong>
                        <strong id="TxtTotalBills" runat="server"></strong>
                    </div>
                </div>
            </div>

            <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgPurchaseBills"
                runat="server"
                Culture="es-ES"
                GroupPanelPosition="Top"
                OnNeedDataSource="RgPurchaseBills_OnNeedDataSource"
                OnDeleteCommand="RgPurchaseBills_OnDeleteCommand"
                OnItemCommand="RgPurchaseBills_OnItemCommand"
                OnItemDataBound="RgPurchaseBills_OnItemDataBound"
                OnDetailTableDataBind="RgPurchaseBills_OnDetailTableDataBind"
                Height="420px">

                <GroupingSettings CaseSensitive="false" />

                <MasterTableView AutoGenerateColumns="False"
                    AllowFilteringByColumn="False"
                    DataKeyNames="DOC_CODIGO, PROV_COD_PROVEEDOR, FA_FIRMA_RO"
                    CommandItemDisplay="Top"
                    AllowSorting="False"
                    AllowPaging="False"
                    PagerStyle-AlwaysVisible="False"
                    NoMasterRecordsText="No Hay datos a Mostrar."
                    Name="ParentGrid">

                    <CommandItemSettings ShowExportToExcelButton="False"
                        ShowAddNewRecordButton="False"
                        AddNewRecordText="Nuevo"
                        ShowRefreshButton="false"
                        ShowExportToPdfButton="false" />

                    <Columns>
                        <telerik:GridBoundColumn UniqueName="FA_FIRMA_RO"
                            DataField="FA_FIRMA_RO"
                            HeaderText="Fecha RO"
                            AllowFiltering="False"
                            AutoPostBackOnFilter="False"
                            CurrentFilterFunction="GreaterThanOrEqualTo"
                            ShowFilterIcon="false"
                            DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle Width="90px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="PROV_NOMBRE"
                            DataField="PROV_NOMBRE"
                            HeaderText="Proveedor"
                            AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains"
                            ShowFilterIcon="false">
                            <HeaderStyle Width="40%" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="FA_IMPORTE_INTEGRO_LABEL"
                            DataField="FA_IMPORTE_INTEGRO_LABEL"
                            HeaderText="Importe"
                            AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains"
                            ShowFilterIcon="false"
                            ItemStyle-CssClass="right">
                            <HeaderStyle Width="90px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridButtonColumn UniqueName="DeleteColumn"
                            ButtonType="LinkButton"
                            HeaderTooltip="Eliminar RO"
                            CommandName="Delete"
                            HeaderStyle-Width="40px"
                            ItemStyle-Width="40px"
                            ItemStyle-CssClass="fas fa-trash-alt"
                            Text=" " ConfirmDialogType="RadWindow"
                            ConfirmTitle="ATENCIÓN"
                            ConfirmText="¿ Está seguro que desea eliminar esta RO ?"
                            ConfirmDialogHeight="100px"
                            HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridButtonColumn>
                        <telerik:GridButtonColumn UniqueName="EditColumn"
                            ButtonType="LinkButton"
                            HeaderTooltip=""
                            CommandName="UpdatePurchaseBill"
                            HeaderStyle-Width="40px"
                            ItemStyle-Width="40px"
                            Text=" "
                            HeaderStyle-HorizontalAlign="Center">
                        </telerik:GridButtonColumn>
                    </Columns>

                    <%--VER--%>
                    <DetailTables>
                        <telerik:GridTableView AutoGenerateColumns="false"
                            CommandItemDisplay="Top"
                            DataKeyNames="FA_CODIGO, DOC_CODIGO"
                            AllowFilteringByColumn="False"
                            AllowSorting="False"
                            AllowPaging="False"
                            PagerStyle-AlwaysVisible="False"
                            NoMasterRecordsText="No Hay datos a Mostrar."
                            Name="BillsGrid">

                            <CommandItemSettings ShowRefreshButton="false"
                                ShowExportToExcelButton="false"
                                ShowAddNewRecordButton="false" />

                            <Columns>
                                <telerik:GridBoundColumn UniqueName="APP_PRESUP"
                                    DataField="APP_PRESUP"
                                    HeaderText="Apl. Pres."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="80px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="FA_NUM_FACTURA"
                                    DataField="FA_NUM_FACTURA"
                                    HeaderText="Nro Factura"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <HeaderStyle Width="100px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="FA_FECHA_FACTURA"
                                    DataField="FA_FECHA_FACTURA"
                                    HeaderText="F. Factura"
                                    AllowFiltering="False"
                                    AutoPostBackOnFilter="False"
                                    CurrentFilterFunction="GreaterThanOrEqualTo"
                                    ShowFilterIcon="false"
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <HeaderStyle Width="90px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="FA_IMPORTE_INTEGRO_LABEL"
                                    DataField="FA_IMPORTE_INTEGRO_LABEL"
                                    HeaderText="Importe"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="FA_BASE_IMPONIBLE_LABEL"
                                    DataField="FA_BASE_IMPONIBLE_LABEL"
                                    HeaderText="Base Imponible"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="40%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="FA_IMPORTE_IVA_LABEL"
                                    DataField="FA_IMPORTE_IVA_LABEL"
                                    HeaderText="I.V.A."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="60px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="FA_IMPORTE_RETENCION_LABEL"
                                    DataField="FA_IMPORTE_RETENCION_LABEL"
                                    HeaderText="Retención"
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="90px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="FA_IMPORTE_BOE_LABEL"
                                    DataField="FA_IMPORTE_BOE_LABEL"
                                    HeaderText="Imp B.O.E."
                                    AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false"
                                    ItemStyle-CssClass="right">
                                    <HeaderStyle Width="90px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn UniqueName="DeleteColumn"
                                    ButtonType="LinkButton"
                                    HeaderTooltip="Eliminar Factura"
                                    CommandName="Delete"
                                    HeaderStyle-Width="40px"
                                    ItemStyle-Width="40px"
                                    ItemStyle-CssClass="fas fa-trash-alt"
                                    Text=" " ConfirmDialogType="RadWindow"
                                    ConfirmTitle="ATENCIÓN"
                                    ConfirmText="¿ Está seguro que desea eliminar esta Factura ?"
                                    ConfirmDialogHeight="100px"
                                    HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridButtonColumn>
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </MasterTableView>

                <ClientSettings>
                    <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                    <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                </ClientSettings>
            </telerik:RadGrid>

            <div class="form-group buttons" style="position: relative; bottom: 55px">
                <telerik:RadButton ButtonType="LinkButton" ID="btnAccept"
                    runat="server"
                    RenderMode="Native"
                    OnClientClicked="OnCloseClientClicked"
                    Text="Aceptar"
                    AutoPostBack="False">
                </telerik:RadButton>
            </div>
        </telerik:RadAjaxPanel>
    </form>
    <telerik:RadScriptBlock runat="server">
        <script>
            function OnCloseClientClicked(sender, eventArgs) {
                CloseWindows(null);
            }

            function CloseWindows(response) {
                var wnd = GetRadWindow();
                if (wnd) {
                    wnd.close(response);
                }
            }

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) {
                    oWindow = window.radWindow;
                }
                else if (window.frameElement.radWindow) {
                    oWindow = window.frameElement.radWindow;
                }

                return oWindow;
            }

            function showError(response) {
                switch (response) {
                    case 0:
                        radalert("No se puede eliminar la factura en cuestión debido a que ya ha sido vinculada a un documento contable.", 330, 140, "Imposible eliminar factura", null, null);
                        break;
                    case 1:
                        radalert("Ha ocurrido un error eliminando la factura en cuestión.", 330, 140, "Imposible eliminar factura", null, null);
                        break;
                    case 2:
                        radalert("No se puede eliminar la factura en cuestión debido a que no se ha encontrado en la BD.", 330, 140, "Imposible eliminar factura", null, null);
                        break;
                    case 3:
                        radalert("No se puede eliminar la RO en cuestión debido a que ya ha sido vinculada a un documento contable.", 330, 140, "Imposible eliminar factura", null, null);
                        break;
                    case 4:
                        radalert("No se puede eliminar la RO en cuestión debido a que no tiene facturas asociadas.", 330, 140, "Imposible eliminar factura", null, null);
                        break;
                    case 5:
                        radalert("No se puede eliminar la RO en cuestión debido a que no se pudieron eliminar todas las facturas asociadas.", 330, 140, "Imposible eliminar factura", null, null);
                        break;
                    case 6:
                        radalert("No se puede eliminar la RO en cuestión debido a que alguna factura asociada está viculada a un documento contable.", 330, 140, "Imposible eliminar factura", null, null);
                        break;
                    case 7:
                        radalert("No se puede agregar la RO debido a que alguna factura asociada no ha sido encontrada en la BD.", 330, 140, "Imposible agregar factura", null, null);
                        break;
                    case 8:
                        radalert("El importe introducido supera el importe total del RC. Consulte el estado del expediente y verifique los importes antes de continuar.", 330, 140, "Fase RC superada", null, null);
                        break;
                    case 9:
                        radalert("El importe introducido supera el importe total del AD. Consulte el estado del expediente y verifique los importes antes de continuar.", 330, 140, "Fase RC superada", null, null);
                        break;
                    case 10:
                        radalert("No se puede agregar la RO debido a que alguna información del documento contable no ha sido encontrada.", 330, 140, "Imposible agregar factura", null, null);
                        break;
                }
            }

            function showBigError(cacsCode, articleNumber, articleName, year, articleBudget, articleCredit, articleDiff, hasChapter, chapterNumber, chapterName, chapterBudget, chapterCredit, chapterDiff) {
                var text = "El crédito retenido supera al presupuestado.</br>A nivel de art1culo: " + articleNumber + " - " + articleName + "(" + year + ")</br>Presupuesto Definitivo: " + articleBudget + "</br>Crédito Retenido: " + articleCredit + "</br>Diferencia: " + articleDiff;

                var size = 220;
                if (hasChapter == 1) {
                    size = 300;
                    text = text + "</br></br>A nivel de capitulo: " + chapterNumber + " - " + chapterName + "(" + year + ")</br>Presupuesto Definitivo: " + chapterBudget + "</br>Crédito Retenido: " + chapterCredit + "</br>Diferencia: " + chapterDiff;
                }

                radalert(text, 340, size, "Error", null, null);
            }
        </script>
    </telerik:RadScriptBlock>
</body>
</html>
