<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="IncomesList.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.List.IncomesList" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <%--Modales--%>
    <telerik:RadWindowManager ID="rwmIncomesList" runat="server">
        <Windows>

            <%--estado provisional--%>
            <telerik:RadWindow ID="rwProvisionalStatus"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Estado Provisional"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="320"
                Height="200"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientProvisionalCloseHandler">
            </telerik:RadWindow>

            <%--situacion concepto--%>
            <telerik:RadWindow ID="rwSituationConcept"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Situación Concepto"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="380"
                Height="230"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientSituationConceptCloseHandler">
            </telerik:RadWindow>

            <%--situacion concepto dr-mi--%>
            <telerik:RadWindow ID="rwSituationConceptDrMi"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Situación Concepto DR-MI"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="400"
                Height="230"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientSituationConceptDrMiCloseHandler">
            </telerik:RadWindow>

            <%--ingresos por sede--%>
            <telerik:RadWindow ID="rwIncomesByPlace"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Ingresos por Sede"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="320"
                Height="230"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientIncomesByPlaceCloseHandler">
            </telerik:RadWindow>

            <%--derechos reconocidos--%>
            <telerik:RadWindow ID="rwRightsRecognized"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Derechos Reconocidos"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="320"
                Height="170"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientRightsRecognizedCloseHandler">
            </telerik:RadWindow>

            <%--reconocieminto de derechos--%>
            <telerik:RadWindow ID="rwRightsRecognitionByConvention"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Reconocimento de derechos"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="330"
                Height="170"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientRightsRecognitionByConventionCloseHandler">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <div id="section_list" class="container">

        <h3>Listados Ingresos</h3>

        <%--BUTTONS--%>
        <div class="top-buttons">
            <div class="a-buttons">
                <div class="back">
                    <span class="icon"></span>
                    <a onclick="backAction()" role="button">Volver</a>
                </div>
            </div>
        </div>

        <div id="page_incomes" class="box-block">
            <telerik:RadAjaxPanel ID="rapIncomesList" runat="server"
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

                <div class="menu-terciario" style="height: calc(100vh - 160px) !important;">

                    <telerik:RadButton ButtonType="LinkButton" ID="btnProvisional"
                        runat="server"
                        RenderMode="Native"
                        Text="Estado Provisional del Ejercicio"
                        AutoPostBack="False"
                        OnClientClicked="provisionalStatus"
                        CssClass="card filter-page">
                    </telerik:RadButton>

                    <telerik:RadButton ButtonType="LinkButton" ID="btnSituationConcept"
                        runat="server"
                        RenderMode="Native"
                        Text="Situación Concepto"
                        AutoPostBack="False"
                        OnClientClicked="situationConcept"
                        CssClass="card filter-page">
                    </telerik:RadButton>

                    <telerik:RadButton ButtonType="LinkButton" ID="btnSituationConceptDrMi"
                        runat="server"
                        RenderMode="Native"
                        Text="Situación Concepto DR-MI"
                        AutoPostBack="False"
                        OnClientClicked="situationConceptDrMi"
                        CssClass="card filter-page">
                    </telerik:RadButton>

                    <telerik:RadButton ButtonType="LinkButton" ID="btnIncomesByPlace"
                        runat="server"
                        RenderMode="Native"
                        Text="Ingresos por Sede"
                        AutoPostBack="False"
                        OnClientClicked="incomesByPlace"
                        CssClass="card filter-page">
                    </telerik:RadButton>

                    <telerik:RadButton ButtonType="LinkButton" ID="btnRightsRecognized"
                        runat="server"
                        RenderMode="Native"
                        Text="Derechos Reconocidos pendientes de ingresar"
                        AutoPostBack="False"
                        OnClientClicked="rightsRecognized"
                        CssClass="card filter-page">
                    </telerik:RadButton>

                    <telerik:RadButton ButtonType="LinkButton" ID="btnRightsRecognitionByConvention"
                        runat="server"
                        RenderMode="Native"
                        Text="Reconocimiento de derechos por convenios"
                        AutoPostBack="False"
                        OnClientClicked="rightsRecognitionByConvention"
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

            function provisionalStatus(sender, eventArgs) {
                var url = "ProvisionalStatusFilter.aspx?type=I";
                var manager = $find("<%= this.rwmIncomesList.ClientID %>");
                var oWnd = manager.open(url, "rwProvisionalStatus");
            }

            function situationConcept(sender, eventArgs) {
                var url = "SituationConceptFilter.aspx";
                var manager = $find("<%= this.rwmIncomesList.ClientID %>");
                var oWnd = manager.open(url, "rwSituationConcept");
            }

            function situationConceptDrMi(sender, eventArgs) {
                var url = "SituationConceptFilter.aspx";
                var manager = $find("<%= this.rwmIncomesList.ClientID %>");
                var oWnd = manager.open(url, "rwSituationConceptDrMi");
            }

            function incomesByPlace(sender, eventArgs) {
                var url = "IncomesByPlaceFilter.aspx";
                var manager = $find("<%= this.rwmIncomesList.ClientID %>");
                var oWnd = manager.open(url, "rwIncomesByPlace");
            }

            function rightsRecognized(sender, eventArgs) {
                var url = "RightsRecognizedFilter.aspx";
                var manager = $find("<%= this.rwmIncomesList.ClientID %>");
                var oWnd = manager.open(url, "rwRightsRecognized");
            }

            function rightsRecognitionByConvention(sender, eventArgs) {
                var url = "RightsRecognitionByConventionFilter.aspx";
                var manager = $find("<%= this.rwmIncomesList.ClientID %>");
                var oWnd = manager.open(url, "rwRightsRecognitionByConvention");
            }


            function OnClientProvisionalCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    var year = data.year;
                    var effectiveDate = data.effectiveDate;
                    var withPending = data.withOutPending;

                    var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=listIncomeProvisionalStatus&year=' + year + '&effectiveDate=' + effectiveDate + '&withOutPending=' + withPending;

                    window.open(url, "Reporte");
                }
            }

            function OnClientSituationConceptCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    var year = data.year;
                    var application = data.application;
                    var applicationLabel = data.applicationLabel;
                    var since = data.since;
                    var until = data.until;

                    var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=listIncomeByConcept&year=' + year + '&application=' + application + '&applicationLabel=' + applicationLabel;

                    if (since != null) {
                        url = url + '&since=' + since;
                    }

                    if (until != null) {
                        url = url + '&until=' + until;
                    }

                    window.open(url, "Reporte");
                }
            }

            function OnClientSituationConceptDrMiCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    var year = data.year;
                    var application = data.application;
                    var since = data.since;
                    var until = data.until;

                    var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=listIncomeByConceptDrMi&year=' + year + '&application=' + application;

                    if (since != null) {
                        url = url + '&since=' + since;
                    }

                    if (until != null) {
                        url = url + '&until=' + until;
                    }

                    window.open(url, "Reporte");
                }
            }

            function OnClientIncomesByPlaceCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    var year = data.year;
                    var place = data.place;
                    var since = data.since;
                    var until = data.until;

                    var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=listIncomeByPlace&year=' + year + '&since=' + since + '&until=' + until;

                    if (place !== "-1") {
                        url = url + '&place=' + place;
                    }

                    window.open(url, "Reporte");
                }
            }

            function OnClientRightsRecognizedCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    var operationDate = data.operationDate;

                    var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=listIncomeRightsRecognized&date=' + operationDate;

                    window.open(url, "Reporte");
                }
            }

            function OnClientRightsRecognitionByConventionCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    var since = data.since;
                    var until = data.until;

                    var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=listIncomeDrAgreement&since=' + since + '&until=' + until;

                    window.open(url, "Reporte");
                }
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
