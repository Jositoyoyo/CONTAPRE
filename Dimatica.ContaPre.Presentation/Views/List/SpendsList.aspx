<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="SpendsList.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.List.SpendsList" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <%--Modales--%>
    <telerik:RadWindowManager ID="rwmSpendsList" runat="server">
        <Windows>

            <%--estado provisional--%>
            <telerik:RadWindow ID="rwProvisionalStatus"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Estado Provisional"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="330"
                Height="200"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientProvisionalCloseHandler">
            </telerik:RadWindow>   
            
            <%--gastos por concepto--%>
            <telerik:RadWindow ID="rwSpendsByConcept"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Gastos por Concepto"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="380"
                Height="230"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientSpendsByConceptCloseHandler">
            </telerik:RadWindow>     
            
            <%--pagos por sede--%>
            <telerik:RadWindow ID="rwPaymentsByPlace"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Pagos por Sede"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="320"
                Height="280"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientPaymentsByPlaceCloseHandler">
            </telerik:RadWindow>  
            
            <%--grado de cumplimiento--%>
            <telerik:RadWindow ID="rwComplianceGrade"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Grado de Cumplimiento"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="430"
                Height="170"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientComplianceGradeCloseHandler">
            </telerik:RadWindow> 
            
            <%--proveedores por año--%>
            <telerik:RadWindow ID="rwProvidersPerYear"
                runat="server"
                OffsetElementID="main"
                RenderMode="Lightweight"
                Title="Proveedores por Año"
                Behaviors="Close"
                VisibleStatusbar="False"
                Width="320"
                Height="170"
                CenterIfModal="True"
                EnableShadow="True"
                Modal="True"
                OnClientClose="OnClientProvidersPerYearCloseHandler">
            </telerik:RadWindow>

        </Windows>
    </telerik:RadWindowManager>

    <div id="section_list" class="container">

        <h3>Listados Gastos</h3>
        <%--BUTTONS--%>
        <div class="top-buttons">
            <div class="a-buttons">
                <div class="back">
                    <span class="icon"></span>
                    <a onclick="backAction()" role="button">Volver</a>
                </div>
            </div>
        </div>

        <div id="page_spends" class="box-block">
            <telerik:RadAjaxPanel ID="rapSpendsList" runat="server"
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

                    <telerik:RadButton ButtonType="LinkButton" ID="btnProvisional"
                        runat="server"
                        RenderMode="Native"
                        Text="Estado Provisional del Ejercicio"
                        AutoPostBack="False"
                        OnClientClicked="provisionalStatus"
                        CssClass="card filter-page">
                    </telerik:RadButton>

                    <telerik:RadButton ButtonType="LinkButton" ID="btnSpendsByConcept"
                        runat="server"
                        RenderMode="Native"
                        Text="Gastos por Concepto"
                        AutoPostBack="False"
                        OnClientClicked="spendsByConcept"
                        CssClass="card filter-page">
                    </telerik:RadButton>

                    <telerik:RadButton ButtonType="LinkButton" ID="btnPayments"
                        runat="server"
                        RenderMode="Native"
                        Text="Pagos por Sede"
                        AutoPostBack="False"
                        OnClientClicked="paymentsByPlace"
                        CssClass="card filter-page">
                    </telerik:RadButton>

                    <telerik:RadButton ButtonType="LinkButton" ID="btnCompliance"
                        runat="server"
                        RenderMode="Native"
                        Text="Grado de Cumplimiento del presupuesto No Vinculante"
                        AutoPostBack="False"
                        OnClientClicked="complianceGrade"
                        CssClass="card filter-page">
                    </telerik:RadButton>

                    <telerik:RadButton ButtonType="LinkButton" ID="btnProviders"
                        runat="server"
                        RenderMode="Native"
                        Text="Listado de Proveedores por año"
                        AutoPostBack="False"
                        OnClientClicked="providersPerYear"
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
                var url = "ProvisionalStatusFilter.aspx?type=G";
                var manager = $find("<%= this.rwmSpendsList.ClientID %>");
                var oWnd = manager.open(url, "rwProvisionalStatus");
            }

            function spendsByConcept(sender, eventArgs) {
                var url = "SpendsByConceptFilter.aspx";
                var manager = $find("<%= this.rwmSpendsList.ClientID %>");
                var oWnd = manager.open(url, "rwSpendsByConcept");
            }

            function paymentsByPlace(sender, eventArgs) {
                var url = "PaymentsByPlaceFilter.aspx";
                var manager = $find("<%= this.rwmSpendsList.ClientID %>");
                var oWnd = manager.open(url, "rwPaymentsByPlace");
            }

            function complianceGrade(sender, eventArgs) {
                var url = "ComplianceGradeFilter.aspx";
                var manager = $find("<%= this.rwmSpendsList.ClientID %>");
                var oWnd = manager.open(url, "rwComplianceGrade");
            }  

            function providersPerYear(sender, eventArgs) {
                var url = "ProvidersPerYearFilter.aspx";
                var manager = $find("<%= this.rwmSpendsList.ClientID %>");
                var oWnd = manager.open(url, "rwProvidersPerYear");
            }


            function OnClientProvisionalCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    var year = data.year;
                    var effectiveDate = data.effectiveDate;
                    var withPending = data.withOutPending;

                    var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=listSpendProvisionalStatus&year=' + year + '&effectiveDate=' + effectiveDate + '&withOutPending=' + withPending;

                    window.open(url, "Reporte");
                }
            }

            function OnClientSpendsByConceptCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    var year = data.year;
                    var application = data.application;
                    var since = data.since;
                    var until = data.until;

                    var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=listSpendsByConcept&year=' + year + '&application=' + application;

                    if (since != null) {
                        url = url + '&since=' + since;
                    }

                    if (until != null) {
                        url = url + '&until=' + until;
                    }

                    window.open(url, "Reporte");
                }
            }

            function OnClientPaymentsByPlaceCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    var year = data.year;
                    var place = data.place;
                    var since = data.since;
                    var until = data.until;
                    var application = data.application; 

                    var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=listSpendsByPlace&year=' + year + '&since=' + since + '&until=' + until;

                    if (place !== "-1") {
                        url = url + '&place=' + place;
                    }

                    if (application !== "-1") {
                        url = url + '&application=' + application;
                    }

                    window.open(url, "Reporte");
                }
            }

            function OnClientComplianceGradeCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    var year = data.year;
                    var since = data.since;
                    var until = data.until;

                    var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=listSpendsComplianceGrade&year=' + year + '&since=' + since + '&until=' + until;

                    window.open(url, "Reporte");
                }
            }

            function OnClientProvidersPerYearCloseHandler(sender, args) {
                var data = args.get_argument();
                if (data != null) {
                    var year = data.year;
                    
                    var url = window.location.origin + '\\Views\\ReportViewer\\CustomReportViewer.aspx?report=listSpendsProvidersByYear&year=' + year;

                    window.open(url, "Reporte");
                }
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
