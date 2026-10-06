<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="SeeStatusApplication.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.ExtraBudgetary.SeeStatusApplication" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="/Public/Css/Contapre.css" />

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>

    <script src="/Public/javascript/jquery-3.7.1.js"></script>
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
        <telerik:RadScriptManager ID="rsmSeeStatusApplication"
            runat="server"
            EnablePageMethods="True" />
        <telerik:RadSkinManager ID="rskmSeeStatusApplication"
            runat="server"
            Skin="Metro"
            ShowChooser="false" />
        <telerik:RadWindowManager ID="rwmSeeStatusApplication" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ralSeeStatusApplication"
            runat="server"
            Skin="Material"
            Transparency="0"
            Modal="True">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="rapSeeStatusApplication"
            runat="server"
            LoadingPanelID="ralSeeStatusApplication">
            <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgResume"
                runat="server"
                Culture="es-ES"
                GroupPanelPosition="Top"
                OnNeedDataSource="RgResume_OnNeedDataSource"
                OnItemDataBound="RgResume_OnItemDataBound"
                OnPreRender="RgResume_OnPreRender"
                Height="130px">

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
                            <HeaderStyle Width="50px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="AccumulatedLabel"
                            DataField="AccumulatedLabel"
                            HeaderText="TODAS"
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
