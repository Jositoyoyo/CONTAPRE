<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="Home.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Home.Home" %>

<asp:Content ID="Content1" 
             ContentPlaceHolderID="head"
             runat="server">
</asp:Content>
<asp:Content ID="Content2" 
             ContentPlaceHolderID="ContentPlaceHolder1"
             runat="server">
    <!-- Page Content -->
    <div id="section_home" class="container">

        <h3>Home</h3>
        <div id="page_home" class="home">
            <h4>Bienvenido a la aplicación de contabilidad presupuestaria</h4>
            <div class="home__img">
                <img src="/Public/Images/UIMP_Conf_Pricipal_RGB.png" alt="Logo UIMP">
            </div>
        </div>
    </div>
</asp:Content>
