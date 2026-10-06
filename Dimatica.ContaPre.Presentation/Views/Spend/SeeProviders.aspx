<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="SeeProviders.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Spend.SeeProviders" %>

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
        <telerik:RadScriptManager ID="rsmSeeProviders"
            runat="server"
            EnablePageMethods="True" />
        <telerik:RadSkinManager ID="rskmSeeProviders"
            runat="server"
            Skin="Metro"
            ShowChooser="false" />
        <telerik:RadWindowManager ID="rwmSeeProviders" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ralSeeProviders"
            runat="server"
            Skin="Material"
            Transparency="0"
            Modal="True">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="rapSeeProviders"
            runat="server"
            LoadingPanelID="ralSeeProviders">

            <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgProviders"
                runat="server"
                Culture="es-ES"
                GroupPanelPosition="Top"
                OnNeedDataSource="RgProviders_OnNeedDataSource"
                Height="350px">

                <GroupingSettings CaseSensitive="false" />

                <MasterTableView AutoGenerateColumns="False"
                    AllowFilteringByColumn="False"
                    DataKeyNames="PROV_CODIGO"
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
                        <telerik:GridBoundColumn UniqueName="PROV_NOMBRE"
                            DataField="PROV_NOMBRE"
                            HeaderText="Proveedor"
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
        </script>
    </telerik:RadScriptBlock>
</body>
</html>
