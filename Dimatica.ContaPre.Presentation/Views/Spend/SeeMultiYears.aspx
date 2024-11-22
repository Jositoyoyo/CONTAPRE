<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="SeeMultiYears.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Spend.SeeMultiYears" %>

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
        <telerik:RadScriptManager ID="rsmSeeMultiYears"
            runat="server"
            EnablePageMethods="True" />
        <telerik:RadSkinManager ID="rskmSeeMultiYears"
            runat="server"
            Skin="Metro"
            ShowChooser="false" />
        <telerik:RadWindowManager ID="rwmSeeMultiYears" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="ralSeeMultiYears"
            runat="server"
            Skin="Material"
            Transparency="0"
            Modal="True">
        </telerik:RadAjaxLoadingPanel>

        <telerik:RadAjaxPanel ID="rapSeeMultiYears"
            runat="server"
            LoadingPanelID="ralSeeMultiYears">

            <telerik:RadGrid ClientSettings-EnableRowHoverStyle="True" ID="RgDocuments"
                runat="server"
                Culture="es-ES"
                GroupPanelPosition="Top"
                OnNeedDataSource="RgDocuments_OnNeedDataSource"
                Height="350px">

                <GroupingSettings CaseSensitive="false" />

                <MasterTableView AutoGenerateColumns="False"
                    AllowFilteringByColumn="False"
                    DataKeyNames="EA_NUMERO"
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
                        <telerik:GridBoundColumn UniqueName="EA_NUMERO"
                            DataField="EA_NUMERO"
                            HeaderText="N⁰ Exp. Admin."
                            AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains"
                            ShowFilterIcon="false">
                            <HeaderStyle Width="40%" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="EXP_ANO_PRESUPUESTO"
                            DataField="EXP_ANO_PRESUPUESTO"
                            HeaderText="Año Exp. Contable"
                            AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains"
                            ShowFilterIcon="false">
                            <HeaderStyle Width="40%" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="EXP_NUM_EXP_CONTABLE_ANUAL"
                            DataField="EXP_NUM_EXP_CONTABLE_ANUAL"
                            HeaderText="N⁰ Exp. Contable"
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
