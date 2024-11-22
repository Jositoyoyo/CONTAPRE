<%@ Page Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="ManageUser.aspx.cs"
    Inherits="Dimatica.ContaPre.Presentation.Views.Systems.Users.ManageUser" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>
<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <!-- Page Content -->
    <div id="section_treasury" class="container" style="height: calc(100vh - 56px) !important">

        <h3 id="titleHeader" runat="server">Nuevo usuario del sistema</h3>

        <%-- MANAGE --%>
        <div class="manage" id="manage_user">

            <telerik:RadAjaxLoadingPanel
                ID="RadAjaxLoadingPanel1"
                runat="server"
                Skin="Material"
                Transparency="0"
                Modal="True">
                <asp:Label ID="Label2" runat="server" ForeColor="Red">Loading...</asp:Label>
            </telerik:RadAjaxLoadingPanel>

            <telerik:RadAjaxPanel ID="rapManageUser"
                runat="server"
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

                <telerik:RadNotification ID="RadNotification1" runat="server" 
                    RenderMode="Lightweight"
                    Position="Center"
                    Width="330px" 
                    Height="140px" 
                    Text="Notification"
                    ContentIcon="warning" 
                    TitleIcon="warning"
                    Animation="Fade"
                    EnableRoundedCorners="True"
                    EnableShadow="False"
                    ContentScrolling="Auto"
                    Opacity="100"
                    ShowSound="None"
                    AutoCloseDelay="0">
                </telerik:RadNotification>

             
                <div class="form-group form-group-fake">

                    <div class="field-container">

                        <div class="form-group">

                            <%--Nombre--%>
                            <div class="field-container">
                                <span>Nombre:</span>
                                <telerik:RadTextBox ID="txtName"
                                    Width="100%"
                                    runat="server"
                                    MaxLength="20"
                                    Text="">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rfvName"
                                    runat="server"
                                    Display="Dynamic"
                                    ValidationGroup="UserGroup"
                                    ControlToValidate="txtName"
                                    ErrorMessage="Nombre obligatorio"
                                    ToolTip="Introduzca el nombre del Usuario"
                                    ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </div>

                            <%--Apellidos--%>
                            <div class="field-container">
                                <span>Apellidos:</span>
                                <telerik:RadTextBox ID="txtLastName"
                                    Width="100%"
                                    runat="server"
                                    MaxLength="60"
                                    Text="">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rfvLastName"
                                    runat="server"
                                    Display="Dynamic"
                                    ValidationGroup="UserGroup"
                                    ControlToValidate="txtLastName"
                                    ErrorMessage="Apellidos obgligatorio "
                                    ToolTip="Introduzca los apellidos del Usuario"
                                    ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </div>

                        </div>

                        <div class="form-group">

                            <%--Login--%>
                            <div class="field-container">
                                <span>Login:</span>
                                <telerik:RadTextBox ID="txtLogin"
                                    Width="100%"
                                    runat="server"
                                    MaxLength="8"
                                    Text="">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="rfvLogin"
                                    runat="server"
                                    Display="Dynamic"
                                    ValidationGroup="UserGroup"
                                    ControlToValidate="txtLogin"
                                    ErrorMessage="Login obligatorio"
                                    ToolTip="Introduzca el login del Usuario."
                                    ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </div>

                            <%--Email--%>
                            <div class="field-container">

                                <span>Email:</span>
                                
                                    <telerik:RadTextBox ID="txtEmail"
                                        Width="100%"
                                        runat="server"
                                        MaxLength="100"
                                        Placeholder="Introduce tu correo electrónico">
                                    </telerik:RadTextBox>

                                    <asp:RequiredFieldValidator ID="rfvEmail" 
                                        runat="server"
                                        Display="Dynamic"
                                        ValidationGroup="UserGroup"
                                        ControlToValidate="txtLogin"
                                        ErrorMessage="Correo electronico obligatorio"
                                        ToolTip="Introduzca el login del Usuario."
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="revEmail" runat="server"
                                        ControlToValidate="txtEmail"
                                        ErrorMessage="Formato de correo electrónico no válido"
                                        ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
                                        Display="Dynamic"
                                        ForeColor="Red">
                                    </asp:RegularExpressionValidator>

                               

                            </div>
                            <!-- Email -->

                        </div>

                        <div class="form-group">

                            <%--Tipo--%>
                            <div class="field-container field-50">
                                <span>Usuario por defecto de:</span>
                                <telerik:RadComboBox ID="radDropType"
                                    runat="server" Width="100%">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="Gastos" Value="G" />
                                        <telerik:RadComboBoxItem runat="server" Text="Ingresos" Value="I" />
                                    </Items>
                                </telerik:RadComboBox>
                            </div>

                            <%--Nivel--%>
                            <div class="field-container field-50">
                                <span>Nivel:</span>
                                <telerik:RadComboBox ID="radDropLevel"
                                    runat="server" Width="100%">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="10 - Consultar, Imprimir." Value="10" />
                                        <telerik:RadComboBoxItem runat="server" Text="50 - Modificar, Insertar, Eliminar." Value="50" />
                                        <telerik:RadComboBoxItem runat="server" Text="100 - Administración." Value="100" />
                                    </Items>
                                </telerik:RadComboBox>
                            </div>
                        </div>

                    </div>

                </div>

                <%--BUTTONS--%>
                <div class="form-group buttons" id="buttons_group">


                    <telerik:RadButton ButtonType="LinkButton" ID="btnInsert"
                        runat="server"
                        RenderMode="Native"
                        Text="Insertar Usuario"
                        AutoPostBack="True"
                        OnClick="btnInsert_OnClick"
                        ValidationGroup="UserGroup">
                    </telerik:RadButton>

                    <telerik:RadButton ButtonType="LinkButton" ID="btnUpdate"
                        runat="server"
                        RenderMode="Native"
                        Text="Actualizar Usuario"
                        AutoPostBack="True"
                        OnClick="btnUpdate_OnClick"
                        ValidationGroup="UserGroup">
                    </telerik:RadButton>

                    <telerik:RadButton ButtonType="LinkButton" ID="btnDelete"
                        runat="server"
                        RenderMode="Native"
                        Text="Eliminar Usuario"
                        AutoPostBack="False"
                        OnClientClicked="btnDeleteOnClientClicked"
                         CausesValidation="False">
                    </telerik:RadButton>

                    <telerik:RadButton ButtonType="LinkButton" ID="btnStatus"
                        runat="server"
                        RenderMode="Native"
                        Text="Desactivar"
                        AutoPostBack="True"
                        OnClick="btnUpdateStatus_Onclick"
                        CausesValidation="False">
                    </telerik:RadButton>

                    <!-- boton de enviar email. mostrar ventana de confirmacion antes de enviar -->
                    <telerik:RadButton ButtonType="LinkButton" ID="btnSendEmail"
                        runat="server"
                        RenderMode="Native"
                        Text="Enviar Credenciales"
                        AutoPostBack="False"
                        OnClientClicked="btnSendEmailOnClientClicked">
                    </telerik:RadButton>

                    <telerik:RadButton ButtonType="LinkButton" ID="btnSendEmailNewUser"
                        runat="server"
                        RenderMode="Native"
                        Text="Enviar Credenciales"
                        OnClick="btnSendMailNewUser_Onclick"
                        CausesValidation="False">
                    </telerik:RadButton>

                    <!-- boton de reiniciar password. mostrar ventana de confirmacion antes de enviar -->
                    <telerik:RadButton ButtonType="LinkButton" ID="btnResetPassword"
                        runat="server"
                        RenderMode="Native"
                        Text="Reiniciar Password"
                        AutoPostBack="False"
                        OnClientClicked="btnResetPasswordOnClicked"
                        CausesValidation="False">
                    </telerik:RadButton>

                    <telerik:RadButton ButtonType="LinkButton" ID="btnBack"
                        runat="server"
                        RenderMode="Native"
                        Text="Volver"
                        AutoPostBack="True"
                        OnClick="btnBack_OnClick"
                        CausesValidation="False">
                    </telerik:RadButton>



                </div>

            </telerik:RadAjaxPanel>

        </div>

    </div>

    <telerik:RadScriptBlock runat="server">
        <script>

            var modalDiv = null;
            var currentUserId = '<%= ViewState["CurrentUserId"] %>'; // Suponiendo que se guarda en el ViewState

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

            function showLoading(app, args)
            {
                var loadingPanel = $find('<%= RadAjaxLoadingPanel1.ClientID %>');
                loadingPanel.show('<%= RadAjaxLoadingPanel1.ClientID %>');
            }

            function hideLoading(app, args)
            {
                var loadingPanel = $find('<%= RadAjaxLoadingPanel1.ClientID %>');
                loadingPanel.hide('<%= RadAjaxLoadingPanel1.ClientID %>');
            }

            function hideModalDiv(sender, args)
            {
                if (modalDiv)
                {
                    modalDiv.style.display = "none";
                }
            }

            function btnDeleteOnClientClicked(sender, eventArgs)
            {
                if (window.confirm("¿ Desea realmente eliminar este usuario ? Esta acción es irreversible"))
                {
                    showLoading();

                    $.ajax({
                        type: "POST",
                        url: "<%= ResolveUrl("~/Views/Systems/Users/ManageUser.aspx/DeleteUser") %>",
                        data: JSON.stringify({ user_id: currentUserId }),
                        contentType: "application/json; charset=utf-8",
                        async: true,
                        success: function (data) {

                            var result = JSON.parse(data.d);

                            console.log(result);

                            if (result.status === 'error')
                            {
                                hideLoading();
                                showNotification("No se puede eliminar usuario", result.message);
                                return;
                            }

                            if (result.status === 'ok')
                            {
                                hideLoading();
                                $('#buttons_group').hide();
                                showNotification("Éxito al borrar el usuario", "Se ha borrado con éxito el usuario. Redirigiendo...");
                                setTimeout(function(){
                                    window.location.href = window.location.origin + '\\Views\\Systems\\Users\\Users.aspx';
                                }, 3000);
                                return;
                            }

                            hideLoading();
                            showNotification("No se puede eliminar el usuario.", "No se puede eliminar el usuario en cuestión debido a un error inesperado. Exception");


                        }, error: function (xhr, ajaxOptions, thrownError) {
                            hideLoading();
                            showNotification("No se puede eliminar el usuario", "No se puede eliminar el usuario en cuestión debido a un error inesperado. Exception");
                        }
                    });
                }
            }


            function btnSendEmailOnClientClicked(sender, eventArgs)
            {
                if (window.confirm("¿ Desea realmente enviar un email a este usuario con las credenciales ?"))
                {
                    showLoading();

                    $.ajax({
                         type: "POST",
                         url: "<%= ResolveUrl("~/Views/Systems/Users/ManageUser.aspx/SendEmail") %>",
                         data: JSON.stringify({ user_id: currentUserId }),
                         contentType: "application/json; charset=utf-8",
                         async: true,
                         success: function (data) {

                             var result = JSON.parse(data.d);
                             var btnEmail = $find('<%= btnSendEmail.ClientID %>');

                             console.log(result);

                             if (result.status === 'error')
                             {
                                 hideLoading();
                                 showNotification("No se pudo enviar email", result.message);
                                 return;
                             }

                             if (result.status === 'ok')
                             {
                                 btnEmail.text = "Email enviado";
                                // btnEmail.set_enabled(false);
                                 hideLoading();
                                 showNotification("Se ha enviado con exito el email", "El usuario va a recibir al correo sus credenciales de acceso al sistema");
                                 return;
                             }

                             hideLoading();
                             showNotification("No se puede enviar el email.", "No se puede enviar el email en cuestión debido a un error inesperado.Exception");

                         }, error: function (xhr, ajaxOptions, thrownError) {
                             hideLoading();
                             showNotification("No se puede enviar el email.", "No se puede enviar el email en cuestión debido a un error inesperado.Exception");
                         }
                     });

                }
            }

      

            function btnResetPasswordOnClicked(sender, eventArgs)
            {


                if (window.confirm("¿ Desea realmente reiniciar la contraseña de este usuario ?"))
                {
                    showLoading();

                    $.ajax({
                         type: "POST",
                         url: "<%= ResolveUrl("~/Views/Systems/Users/ManageUser.aspx/ResetPassword") %>",
                         data: JSON.stringify({ user_id: currentUserId }),
                         contentType: "application/json; charset=utf-8",
                         async: true,
                         success: function (data) {

                             var result = JSON.parse(data.d);

                             if (result.status === 'error') {
                                 hideLoading();
                                 showNotification("No se puede reiniciar la contraseña", "Por favor contacte con el administrador");
                                 return;
                             }

                             if (result.status === 'ok') {
                                 hideLoading();
                                 showNotification("Exito al reiniciar la contraseña", "El usuario dispone de una nueva contraseña. Se recomienda enviar notificación");
                                 return;
                             }

                             hideLoading();
                             showNotification("No se puede reiniciar la contraseña",  "No se puede reiniciar la contraseña en cuestión debido a un error inesperado.Exception");

                         }, error: function (xhr, ajaxOptions, thrownError) {
                             hideLoading();
                             showNotification("No se puede reiniciar la contraseña", "No se puede reiniciar la contraseña en cuestión debido a un error inesperado.Exception");

                         }
                     });
                }
            }

            function showNotification(title, text)
            {
                var notification = $find("<%= RadNotification1.ClientID %>");
                notification.set_title(title);
                notification.set_text(text);
                notification.show();
            }


        </script>
    </telerik:RadScriptBlock>


</asp:Content>

