<%@ Page 
    Title="Pruebas"
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="HomeTest.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Development.HomeTest" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <!-- Page Content -->
    <div id="section_home" class="container">

        <h3>Enviar correo de pruebas</h3>
        <div id="page_home" class="home">
            
             <asp:Button ID="btnShowSpan" Text="Actualizar etiqueta" runat="server" OnClick="btnUpdateLabel_Click" />
             <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            <asp:HyperLink ID="linkToOtherPage" NavigateUrl="../Usuarios/Usuarios.aspx" Text="Ir a otra página" runat="server"></asp:HyperLink>
             <asp:HyperLink ID="linkToOtherPage2" NavigateUrl="../Test/RadWindow.aspx" Text="Ir a otra página" runat="server"></asp:HyperLink>
        </div>

    </div>
</asp:Content>
