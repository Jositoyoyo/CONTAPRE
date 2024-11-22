<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="ExtrabudgetaryList.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.List.ExtrabudgetaryList" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <%--Modales--%>
    <telerik:RadWindowManager ID="rwmExtrabudgetaryList" runat="server">
        <Windows>

            <%--bede y haber--%>
            <telerik:RadWindow ID="rwDebitAndCredit"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Debe y Haber"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="330"
                Height="230"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientDebitAndCreditCloseHandler">
            </telerik:RadWindow>
            
        </Windows>
    </telerik:RadWindowManager>

    <div id="section_list" class="container">

        <h3>Listados Extrapresupuestarias</h3>

        <%--BUTTONS--%>
        <div class="top-buttons">
            <div class="a-buttons">
                <div class="back">
                    <span class="icon"></span>
                    <a onclick="backAction()" role="button">Volver</a>
                </div>
            </div>
        </div>

        <div id="page_extrabudgetary" class="box-block">
            <telerik:RadAjaxPanel ID="rapExtrabudgetaryList" runat="server"
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
                    <telerik:RadButton ButtonType="LinkButton" ID="btnDebitAndCredit"
                        runat="server"
                        RenderMode="Native"
                        Text="Debe y Haber Extrapresupuestarias"
                        AutoPostBack="False"
                        OnClientClicked="debitAndCredit"
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

            function debitAndCredit(sender, eventArgs) {
                var url = "DebitAndCreditFilter.aspx";
                var manager = $find("<%= this.rwmExtrabudgetaryList.ClientID %>");
                var oWnd = manager.open(url, "rwDebitAndCredit");
            }


            function OnClientDebitAndCreditCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    var application = data.application;
                    var since = data.since;
                    var until = data.until;

                    var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=listExtraBudgetary&application=' + application + '&since=' + since + '&until=' + until;

                    window.open(url, "Reporte");
                }
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
