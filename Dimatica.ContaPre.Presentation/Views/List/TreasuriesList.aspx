<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="TreasuriesList.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.List.TreasuriesList" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <%--Modales--%>
    <telerik:RadWindowManager ID="rwmTreasuriesList" runat="server">
        <Windows>

            <%--libro contabilidad--%>
            <telerik:RadWindow ID="rwAccountingBook"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Libro Contabilidad"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="340"
                Height="170"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientAccountingBookCloseHandler">
            </telerik:RadWindow>

            <%--extracto banco--%>
            <telerik:RadWindow ID="rwBankStatement"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Extracto Banco"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="340"
                Height="170"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientBankStatementCloseHandler">
            </telerik:RadWindow>

            <%--listado de cuadre--%>
            <telerik:RadWindow ID="rwBlockListing"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Listado de Cuadre"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="340"
                Height="170"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientBlockListingCloseHandler">
            </telerik:RadWindow>

            <%--registro de pagos--%>
            <telerik:RadWindow ID="rwPaymentRecord"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Registro de Pagos"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="340"
                Height="170"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientPaymentRecordCloseHandler">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <div id="section_list" class="container">

        <h3>Listados Tesorería</h3>

        <%--BUTTONS--%>
        <div class="top-buttons">
            <div class="a-buttons">
                <div class="back">
                    <span class="icon"></span>
                    <a onclick="backAction()" role="button">Volver</a>
                </div>
            </div>
        </div>

        <div id="page_treasuries" class="box-block">
            <telerik:RadAjaxPanel ID="rapTreasuriesList" runat="server"
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

                <div class="menu-terciario"  style="height: calc(100vh - 160px) !important;">

                    <telerik:RadButton ButtonType="LinkButton" ID="btnAccountingBook"
                        runat="server"
                        RenderMode="Native"
                        Text="Libro Contabilidad"
                        AutoPostBack="False"
                        OnClientClicked="accountingBook"
                        CssClass="card filter-page">
                    </telerik:RadButton>   
                    
                    <telerik:RadButton ButtonType="LinkButton" ID="btnBankStatement"
                        runat="server"
                        RenderMode="Native"
                        Text="Extracto Banco"
                        AutoPostBack="False"
                        OnClientClicked="bankStatement"
                        CssClass="card filter-page">
                    </telerik:RadButton>

                    <telerik:RadButton ButtonType="LinkButton" ID="btnBlockListing"
                        runat="server"
                        RenderMode="Native"
                        Text="Listado de Cuadre"
                        AutoPostBack="False"
                        OnClientClicked="blockListing"
                        CssClass="card filter-page">
                    </telerik:RadButton>

                    <telerik:RadButton ButtonType="LinkButton" ID="btnPaymentRecord"
                        runat="server"
                        RenderMode="Native"
                        Text="Registro de Pagos de Contabilidad"
                        AutoPostBack="False"
                        OnClientClicked="paymentRecord"
                        CssClass="card filter-page">
                    </telerik:RadButton>
                </div>

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

            function accountingBook(sender, eventArgs) {
                var url = "AccountingBookFilter.aspx";
                var manager = $find("<%= this.rwmTreasuriesList.ClientID %>");
                var oWnd = manager.open(url, "rwAccountingBook");
            }

            function bankStatement(sender, eventArgs) {
                var url = "BankStatementFilter.aspx";
                var manager = $find("<%= this.rwmTreasuriesList.ClientID %>");
                var oWnd = manager.open(url, "rwBankStatement");
            }

            function blockListing(sender, eventArgs) {
                var url = "BlockListingFilter.aspx";
                var manager = $find("<%= this.rwmTreasuriesList.ClientID %>");
                var oWnd = manager.open(url, "rwBlockListing");
            }

            function paymentRecord(sender, eventArgs) {
                var url = "PaymentRecordFilter.aspx";
                var manager = $find("<%= this.rwmTreasuriesList.ClientID %>");
                var oWnd = manager.open(url, "rwPaymentRecord");
            }

            function OnClientAccountingBookCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    var since = data.since;
                    var until = data.until;

                    var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=listTreasuryAccountingBook&since=' + since + '&until=' + until;

                    window.open(url, "Reporte");
                }
            }

            function OnClientBankStatementCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    var since = data.since;
                    var until = data.until;

                    var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=listTreasuryBankStatement&since=' + since + '&until=' + until;

                    window.open(url, "Reporte");
                }
            }

            function OnClientBlockListingCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    var amount = data.amount;
                    var register = data.register; 

                    var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=listTreasuryBlockListing&amount=' + amount + '&register=' + register;

                    window.open(url, "Reporte");
                }
            }

            function OnClientPaymentRecordCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    var since = data.since;
                    var until = data.until;

                    var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=listTreasuryPaymentRecord&since=' + since + '&until=' + until;

                    window.open(url, "Reporte");
                }
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
