<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="SeeStatusRecord.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Spend.SeeStatusRecord" %>

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
    </style>
</head>
<body class="container">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="rsmSeeStatusRecord"
            runat="server"
            EnablePageMethods="True" />
        <telerik:RadSkinManager ID="rskmSeeStatusRecord"
            runat="server"
            Skin="Metro"
            ShowChooser="false" />
        <telerik:RadWindowManager ID="rwmSeeStatusRecord" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ralSeeStatusRecord"
            runat="server"
            Skin="Material"
            Transparency="0"
            Modal="True">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="rapSeeStatusRecord"
            runat="server"
            LoadingPanelID="ralSeeStatusRecord">

            <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgResume"
                runat="server"
                Culture="es-ES"
                GroupPanelPosition="Top"
                OnNeedDataSource="RgResume_OnNeedDataSource"
                OnItemDataBound="RgResume_OnItemDataBound"
                Height="270px">

                <GroupingSettings CaseSensitive="false" />

                <MasterTableView AutoGenerateColumns="False"
                    AllowFilteringByColumn="False"
                    CommandItemDisplay="Top"
                    AllowSorting="False"
                    AllowPaging="False"
                    PagerStyle-AlwaysVisible="False"
                    NoMasterRecordsText="No Hay datos a Mostrar.">

                    <CommandItemSettings ShowExportToExcelButton="False"
                        ShowAddNewRecordButton="False"
                        AddNewRecordText="Nuevo"
                        ShowRefreshButton="false"
                        ShowExportToPdfButton="false" />

                    <Columns>
                        <telerik:GridBoundColumn UniqueName="Column"
                            DataField="Column"
                            HeaderText=""
                            AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains"
                            ShowFilterIcon="false">
                            <HeaderStyle Width="40%" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="AccumulatedLabel"
                            DataField="AccumulatedLabel"
                            HeaderText="Acumulado"
                            AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains"
                            ShowFilterIcon="false"
                            ItemStyle-CssClass="right">
                            <HeaderStyle Width="40%" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="PendingLabel"
                            DataField="PendingLabel"
                            HeaderText="Pendiente"
                            AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains"
                            ShowFilterIcon="false"
                            ItemStyle-CssClass="right">
                            <HeaderStyle Width="40%" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>

                <ClientSettings>
                    <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                    <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                </ClientSettings>
            </telerik:RadGrid>

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
