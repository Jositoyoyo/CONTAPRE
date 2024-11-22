<%@ Page Title="Pruebas de envio de correo"
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="SendEmail.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Develoment.SendEmail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head"  runat="server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->
    <div id="section_home" class="container">

        <h3>Enviar correo de pruebas</h3>
        <div id="page_home" class="home">
            <div>
                <label for="txtEmail">Correo Electrónico:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" type="email"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Por favor, introduce tu correo electrónico." Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Por favor, introduce un correo electrónico válido."
                    ValidationExpression="\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b"
                    Display="Dynamic" CssClass="text-danger"></asp:RegularExpressionValidator>
            </div>
            <div>
                <asp:Button ID="btnSendEmailLabel" Text="Actualizar etiqueta" runat="server" OnClick="btnSendEmail_Click" />
                <asp:Label ID="lblAlertMessage" runat="server" Text=""></asp:Label>

        </div>
        </div>

    </div>
</asp:Content>