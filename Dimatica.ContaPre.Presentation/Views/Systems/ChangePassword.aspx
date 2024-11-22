<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Systems.ChangePassword" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <!-- Page Content -->
    <div class="container" id="section_systems">

        <h3>Cambiar contraseña</h3>

        <!--BUTTONS-->
        <%--     <div class="top-buttons">

            <div class="a-buttons">
                <div class="back">
                    <span class="icon"></span>
                    <a onclick="backAction()" role="button">Volver</a>
                </div>
            </div>
        </div>--%>

        <div id="page_changePassword" class="box-block">

            <telerik:RadAjaxPanel ID="rapChangePassword" runat="server" LoadingPanelID="ralPrincipal">

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

                <div class="Pass">

                    <div class="form-group" style="display: flex; flex-direction: row">
                        <span style="width: auto;" id="LabelUsuario">Usuario:</span>
                        <asp:Label CssClass="w-auto" ID="lblUserName" runat="server"></asp:Label>
                    </div>

                    <div class="form-group">
                        <span id="LabelCurrentPass">Contraseña actual:</span>
                        <telerik:RadTextBox ID="txtCurrentPass"
                            runat="server"
                            Width="100%"
                            TextMode="Password"
                            MaxLength="28"
                            placeholder="Contraseña actual">
                        </telerik:RadTextBox>
                        <asp:RequiredFieldValidator ID="rfvCurrentPass"
                            runat="server"
                            ToolTip="Introduzca la contraseña actual"
                            ErrorMessage=" * "
                            Display="Dynamic"
                            ControlToValidate="txtCurrentPass"
                            ValidationGroup="ChangePassGroup"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group">
                        <span id="LabelNewPass">Nueva contraseña:</span>
                        <telerik:RadTextBox ID="txtNewPass"
                            runat="server"
                            Width="100%"
                            TextMode="Password"
                            MaxLength="28"
                            placeholder="Nueva contraseña">
                        </telerik:RadTextBox>
                        <asp:RequiredFieldValidator ID="rfvNewPass"
                            ControlToValidate="txtNewPass"
                            ToolTip="Introduzca la contraseña nueva"
                            ErrorMessage=" * "
                            Display="Dynamic"
                            ValidationGroup="ChangePassGroup"
                            ForeColor="Red"
                            runat="server">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group">
                        <span id="LabelConfirmNewPass">Repetir contraseña:</span>
                        <telerik:RadTextBox ID="txtConfirmNewPass"
                            runat="server"
                            Width="100%"
                            TextMode="Password"
                            MaxLength="28"
                            placeholder="Repetir contraseña">
                        </telerik:RadTextBox>
                        <asp:RequiredFieldValidator ID="rfvConfirmNewPass"
                            ControlToValidate="txtConfirmNewPass"
                            ToolTip="Introduzca nuevamente la contraseña nueva"
                            ErrorMessage=" * "
                            Display="Dynamic"
                            ValidationGroup="ChangePassGroup"
                            ForeColor="Red"
                            runat="server">
                        </asp:RequiredFieldValidator>
                        <p id="passwordHelp" class="form-text text-muted">
                            La contraseña debe contener al menos 8 caracteres, incluyendo una letra mayúscula y un número. No se permiten caracteres especiales ni espacios.
                        </p>


                    </div>

                    <%--BUTTONS--%>
                    <div class="form-group buttons" style="flex-direction: row; justify-content: flex-end">
                        <telerik:RadButton ButtonType="LinkButton" ID="btnChangePassword"
                            runat="server"
                            Text="Guardar"
                            ValidationGroup="ChangePassGroup"
                            AutoPostBack="true"
                            OnClick="btnChangePassword_Click"
                            CssClass="mr-10">
                        </telerik:RadButton>
                        <telerik:RadButton ButtonType="LinkButton" ID="btnCancel"
                            runat="server"
                            Text="Cancelar"
                            AutoPostBack="False"
                            OnClientClicked="OnCancelClientClicked">
                        </telerik:RadButton>
                    </div>
                </div>
            </telerik:RadAjaxPanel>
        </div>
    </div>

    <telerik:RadScriptBlock runat="server">
        <script>

            function OnCancelClientClicked(sender, eventArgs) {
                window.history.back();
            }

            var modalDiv = null;

            function showModalDiv(sender, args)
            {
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
</asp:Content>
