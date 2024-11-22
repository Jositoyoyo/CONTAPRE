<%@ Page 
    Title="Pruebas"
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="RadWindow.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Development.RadWindow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>

        <telerik:RadButton ButtonType="LinkButton" ID="btnOpenWindow"
            runat="server"
            RenderMode="Native"
            Text="Abrir ventana"
            AutoPostBack="False"
            OnClientClicked="openRadWindow">
        </telerik:RadButton>

        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
            <Windows>
                <telerik:RadWindow ID="RadWindow1" runat="server" Title="Sample RadWindow" Width="400px" Height="300px" Modal="true" VisibleOnPageLoad="false">
                    <ContentTemplate>
                      
                        <p>sssssssssssssssssss</p>

                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
    </div>

    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            function openRadWindow() {
                var oWnd = $find("<%= RadWindow1.ClientID %>");
                oWnd.show();
            }

            function reloadCommand() {
                var oWnd = $find("<%= RadWindow1.ClientID %>");
                oWnd.get_windowManager().get_activeWindow().set_command("Reload");
                return false; // Prevent postback
            }

            function OnClientCommandHandler(sender, args) {
                var command = args.get_commandName(); // Get the initiated command
                if (command === "Reload") {
                    args.set_cancel(true); // Prevent reloading
                    alert("Reload command was canceled.");
                }
            }
        </script>
    </telerik:RadScriptBlock>

</asp:Content>
