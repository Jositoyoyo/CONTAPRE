<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="CustomReportViewer.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.ReportViewer.CustomReportViewer" %>

<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <!-- Page Content -->
    <div id="section_reports" class="container">

        <h3 id="titleHeader" runat="server">Reportes</h3>

        <div id="page_customReportViewer" class="box-block">

            <%--VISOR DE INFORMES--%>
            <div class="reportView">
                <rsweb:ReportViewer ID="ReportViewer"
                    runat="server"
                    Height="100%"
                    Width="100%"
                    ProcessingMode="Remote">
                    <rsweb:ServerReport ReportPath="" ReportServerUrl="" />
                </rsweb:ReportViewer>
            </div>
        </div>
    </div>
</asp:Content>
