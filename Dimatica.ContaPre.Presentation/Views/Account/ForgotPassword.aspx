<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="ForgotPassword.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Account.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contabilidad Presupuestaria - recuperar password</title>
    <link rel="stylesheet" href="~/Content/Contapre.css" />
    <link rel="stylesheet" href="~/Content/fontawesome-all.css" />

    <link href="~/content/Images/UIMP_Conf_Pricipal_RGB.png" rel="shortcut icon" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>

    <script src="../../Scripts/jquery-3.4.1.js"></script>
</head>
<body>

    <div class="Login">

        <div class="header-login">
             <img src="../../Content/Images/UIMP_Conf_Pricipal_RGB.png" alt="Contabilidad Presupuestaria" />
        </div>

        <form role="form" id="formForgotPassword" class="login-form" runat="server">

            <telerik:RadAjaxPanel ID="RApForgotPassword"
                runat="server">
                <telerik:RadNotification ID="RadNotification"
                    runat="server"
                    RenderMode="Lightweight"
                    VisibleOnPageLoad="False"
                    Position="TopRight"
                    Width="300"
                    Height="150"
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
            </telerik:RadAjaxPanel>
            <telerik:RadScriptManager ID="RadScriptManager"
                runat="server">
            </telerik:RadScriptManager>

            <p id="lblMsg" runat="server" class="text-help">
                Por favor, introduce tu nombre de usuario para recibir en tu correo las instrucciones de acceso.
            </p>

            <div class="form-group">

                <asp:Label ID="LabelUser" runat="server" Text="Usuario"></asp:Label>

                <telerik:RadTextBox ID="RTbUser"
                    runat="server"
                    Width="100%"
                    MaxLength="50"
                    placeholder="Usuario">
                </telerik:RadTextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidatorUser"
                    runat="server"
                    ControlToValidate="RTbUser"
                    Text=" * "
                    ToolTip="El campo usuario es obligatorio."
                    Display="Dynamic"
                    ForeColor="Red"
                    ValidationGroup="ForgotPasswordGroup">
                </asp:RequiredFieldValidator>

                <p id="lblError"
                    runat="server"
                    class="text-danger small">
                </p>

            </div>

            <div class="form-group buttons">
                <asp:Button ID="btnForgotPassword" runat="server" Text="Enviar" class="btn btn-lg btn-success btn-block" OnClick="btnForgotPassword_Click" />
            </div>
            <div class="forgot-pass">
                <a href="Login.aspx">Volver</a>
            </div>
        </form>
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

        </script>
    </telerik:RadScriptBlock>
</body>
</html>
