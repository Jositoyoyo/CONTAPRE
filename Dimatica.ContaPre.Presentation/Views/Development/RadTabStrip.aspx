<%@ Page 
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true" 
    CodeBehind="RadTabStrip.aspx.cs" 
    Inherits="Dimatica.ContaPre.Presentation.Views.Development.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head"  runat="server"> </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->
    <div id="section_home" class="container">

        <h3>Enviar correo de pruebas</h3>
        
        <div>
            <!-- RadTabStrip de ejemplo -->
            <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1" SelectedIndex="0">
                <Tabs>
                    <telerik:RadTab Text="Tab 1" PageViewID="PageView1" />
                    <telerik:RadTab Text="Tab 2" PageViewID="PageView2" />
                    <telerik:RadTab Text="Tab 3" PageViewID="PageView3" />
                </Tabs>
            </telerik:RadTabStrip>

            <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                <telerik:RadPageView ID="PageView1" runat="server">
                    <h2>Contenido de la Pestaña 1</h2>
                    <p>Este es el contenido de la primera pestaña.</p>
                </telerik:RadPageView>
                <telerik:RadPageView ID="PageView2" runat="server">
                    <h2>Contenido de la Pestaña 2</h2>
                    <p>Este es el contenido de la segunda pestaña.</p>
                </telerik:RadPageView>
                <telerik:RadPageView ID="PageView3" runat="server">
                    <h2>Contenido de la Pestaña 3</h2>
                    <p>Este es el contenido de la tercera pestaña.</p>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>

    </div>
</asp:Content>
