<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="Login.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Account.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contabilidad Presupuestaria</title>
    <link rel="stylesheet" href="/Public/Css/Contapre.css" />
    <link rel="stylesheet" href="/Public/Css/fontawesome-all.css" />

    <link href="/Public/Images/UIMP_Conf_Pricipal_RGB.png" rel="shortcut icon" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>

    <script src="/Public/javascript/jquery-3.7.1.js"></script>
</head>
<body>
    <div class="Login">

        <div class="header-login">
            <img src="/Public/Images/UIMP_Conf_Pricipal_RGB.png" alt="Contabilidad Presupuestaria" />
        </div>

        <form id="form1" runat="server" class="login-form" defaultbutton="BtnLogin">

            <telerik:RadAjaxPanel ID="RApLogin"
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
                <%--      <Scripts>
                    <asp:ScriptReference Path="~/Public/javascript/jquery-3.7.1.min.js" />
                    <asp:ScriptReference Path="~/Public/javascript/bootstrap.js" />
                    <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Public/javascript/WebForms/WebForms.js" />
                    <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Public/javascript/WebForms/WebUIValidation.js" />
                    <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Public/javascript/WebForms/MenuStandards.js" />
                    <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Public/javascript/WebForms/GridView.js" />
                    <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Public/javascript/WebForms/DetailsView.js" />
                    <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Public/javascript/WebForms/TreeView.js" />
                    <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Public/javascript/WebForms/WebParts.js" />
                    <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Public/javascript/WebForms/Focus.js" />
                </Scripts>--%>
            </telerik:RadScriptManager>

            <div class="form-group">

                <asp:Label ID="LabelEmail"
                    runat="server"
                    Text="Usuario: "></asp:Label>

                <telerik:RadTextBox ID="RTbMail"
                    runat="server"
                    Width="100%"
                    MaxLength="50"
                    placeholder="Usuario">
                </telerik:RadTextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidatorMail"
                    runat="server"
                    ControlToValidate="RTbMail"
                    Text=" * "
                    ToolTip="El campo E-Mail es obligatorio."
                    Display="Dynamic"
                    ForeColor="Red"
                    ValidationGroup="LoginGroup">
                </asp:RequiredFieldValidator>

            </div>

            <div class="form-group">

                <asp:Label ID="LabelPassword"
                    runat="server"
                    Text="Password: "></asp:Label>

                <telerik:RadTextBox ID="RTbPassword"
                    runat="server"
                    Width="100%"
                    MaxLength="50"
                    TextMode="Password"
                    placeholder="password">
                </telerik:RadTextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword"
                    runat="server"
                    ControlToValidate="RTbPassword"
                    Text=" * "
                    ToolTip="El campo Contraseña es obligatorio."
                    Display="Dynamic"
                    ForeColor="Red"
                    ValidationGroup="LoginGroup">
                </asp:RequiredFieldValidator>

                <p id="ErrorLogin"
                    runat="server"
                    class="text-danger small">
                </p>

            </div>

            <%--BUTTONS--%>
            <div class="form-group buttons">

                <asp:Button ID="BtnLogin"
                    runat="server"
                    Text="Entrar"
                    ValidationGroup="LoginGroup"
                    OnClick="BtnLogin_OnClick"
                    TabIndex="3"
                    OnClientClick="hideErrorMessage();"></asp:Button>

            </div>

              <div class="forgot-pass">
                  <a href="ForgotPassword.aspx">¿Olvidaste tu contraseña?</a>
                  <br />
                <span id="version" runat="server"></span><span> | </span><span id="environment" runat="server"></span>
            </div>
        </form>
    </div>

    <telerik:RadScriptBlock runat="server">
        <script>
            var modalDiv = null;

            function showModalDiv(sender, args) 
            {
                if (!modalDiv)
                {
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

            function hideModalDiv()
            {
                modalDiv.style.display = "none";
            }

            function hideErrorMessage()
            {
                document.getElementById('<%= ErrorLogin.ClientID %>').style.display = 'none';
            }

        </script>
    </telerik:RadScriptBlock>
</body>
</html>