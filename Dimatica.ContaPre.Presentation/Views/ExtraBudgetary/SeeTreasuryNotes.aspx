<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="SeeTreasuryNotes.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.ExtraBudgetary.SeeTreasuryNotes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="~/Content/Contapre.css" />

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>

    <script src="../../Scripts/jquery-3.4.1.js"></script>
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

        strong {
            margin: 10px;
        }
    </style>
</head>
<body class="container">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="rsmSeeTreasuryNotes"
            runat="server"
            EnablePageMethods="True" />
        <telerik:RadSkinManager ID="rskmSeeTreasuryNotes"
            runat="server"
            Skin="Metro"
            ShowChooser="false" />
        <telerik:RadWindowManager ID="rwmSeeTreasuryNotes" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ralSeeTreasuryNotes"
            runat="server"
            Skin="Material"
            Transparency="0"
            Modal="True">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="rapSeeTreasuryNotes"
            runat="server"
            LoadingPanelID="ralSeeTreasuryNotes">
            <div class="form-group">
                <strong>Apunte de Tesorería</strong>
                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgTreasuryNotes"
                    runat="server"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgTreasuryNotes_OnNeedDataSource"
                    Height="200px">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="False"
                        AllowFilteringByColumn="False"
                        DataKeyNames="TES_CODIGO"
                        CommandItemDisplay="Top"
                        AllowSorting="False"
                        AllowPaging="False"
                        PagerStyle-AlwaysVisible="False"
                        NoMasterRecordsText="No Hay datos a Mostrar.">

                        <CommandItemSettings ShowExportToExcelButton="False"
                            ShowAddNewRecordButton="False"
                            ShowRefreshButton="False"
                            ShowExportToPdfButton="False" />

                        <Columns>
                            <telerik:GridBoundColumn UniqueName="TES_ANO_PRESUPUESTO"
                                DataField="TES_ANO_PRESUPUESTO"
                                HeaderText="Año"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="50px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TES_FECHA_BANCO"
                                DataField="TES_FECHA_BANCO"
                                HeaderText="F. Banco"
                                AllowFiltering="true"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="GreaterThanOrEqualTo"
                                ShowFilterIcon="false"
                                DataFormatString="{0:dd/MM/yyyy}"
                                FilterControlWidth="90%">
                                <HeaderStyle Width="90px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TES_FECHA_APUNTE"
                                DataField="TES_FECHA_APUNTE"
                                HeaderText="F. Apunte"
                                AllowFiltering="true"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="GreaterThanOrEqualTo"
                                ShowFilterIcon="false"
                                DataFormatString="{0:dd/MM/yyyy}"
                                FilterControlWidth="90%">
                                <HeaderStyle Width="90px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TES_APLICACION"
                                DataField="TES_APLICACION"
                                HeaderText="Aplicación"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="90px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridNumericColumn UniqueName="AmountLabel"
                                DataField="TES_TOTAL_IMPORTE_LIQUIDO"
                                HeaderText="Importe Total"
                                DataFormatString="{0:N}"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right"
                                FilterControlWidth="100%">
                                <HeaderStyle Width="100px" />
                            </telerik:GridNumericColumn>
                            <%--<telerik:GridBoundColumn UniqueName="AmountLabel"
                                DataField="AmountLabel"
                                HeaderText="Importe Total"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right">
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn UniqueName="TES_NUMERO_CHEQUE"
                                DataField="TES_NUMERO_CHEQUE"
                                HeaderText="N⁰ Talón/Transf."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="120px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TES_DESCRIPCION"
                                DataField="TES_DESCRIPCION"
                                HeaderText="Descripción"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>

                    <ClientSettings>
                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>

            <div class="form-group">
                <strong>Desglose Apunte</strong>
                <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgDetails"
                    runat="server"
                    Culture="es-ES"
                    GroupPanelPosition="Top"
                    OnNeedDataSource="RgDetails_OnNeedDataSource"
                    Height="200px">

                    <GroupingSettings CaseSensitive="false" />

                    <MasterTableView AutoGenerateColumns="False"
                        AllowFilteringByColumn="False"
                        DataKeyNames="TESD_CODIGO"
                        CommandItemDisplay="Top"
                        AllowSorting="False"
                        AllowPaging="False"
                        PagerStyle-AlwaysVisible="False"
                        NoMasterRecordsText="No Hay datos a Mostrar.">

                        <CommandItemSettings ShowExportToExcelButton="False"
                            ShowAddNewRecordButton="False"
                            ShowRefreshButton="False"
                            ShowExportToPdfButton="False" />

                        <Columns>
                            <telerik:GridBoundColumn UniqueName="TESD_ANO_PRESUPUESTO"
                                DataField="TESD_ANO_PRESUPUESTO"
                                HeaderText="Año"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="50px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TESD_DOCUMENTO"
                                DataField="TESD_DOCUMENTO"
                                HeaderText="Documento"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="90px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TESD_NUMERO_EXPEDIENTE"
                                DataField="TESD_NUMERO_EXPEDIENTE"
                                HeaderText="N⁰ Exp."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="90px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TES_APLICACION"
                                DataField="TES_APLICACION"
                                HeaderText="Aplicación"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="90px" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn UniqueName="AmountDocumentLabel"
                                DataField="AmountDocumentLabel"
                                HeaderText="Importe"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right">
                                <HeaderStyle Width="100px" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridNumericColumn UniqueName="AmountDocumentLabel"
                                DataField="TESD_IMPORTE_LIQUIDO"
                                HeaderText="Importe"
                                DataFormatString="{0:N}"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo"
                                ShowFilterIcon="false"
                                ItemStyle-CssClass="right"
                                FilterControlWidth="100%">
                                <HeaderStyle Width="100px" />
                            </telerik:GridNumericColumn>
                            <telerik:GridBoundColumn UniqueName="TESD_NUMERO_CHEQUE"
                                DataField="TESD_NUMERO_CHEQUE"
                                HeaderText="N⁰ Talón/Transf."
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="120px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TESD_DESCRIPCION"
                                DataField="TESD_DESCRIPCION"
                                HeaderText="Descripción"
                                AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <HeaderStyle Width="40%" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>

                    <ClientSettings>
                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>

            <div class="form-group buttons">
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
        </script>
    </telerik:RadScriptBlock>
</body>
</html>
