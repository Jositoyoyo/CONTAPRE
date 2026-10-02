namespace Dimatica.ContaPre.Presentation.Views.Systems.Users
{
    #region NameSpaces

    using System;
    using System.Text;
    using System.Web.Script.Serialization;
    using System.Web.Services;
    using System.Configuration;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Helpers;
    using Dimatica.ContaPre.Presentation.Views.Shared;
    using Dimatica.ContaPre.Presentation.DataValidation;
    using Dimatica.ContaPre.Presentation.Helpers.Email;
    using System.Collections.Specialized;

    #endregion

    public partial class ManageUser : BasePage
    {
        #region Static Fields and Constants

        private static IUserService userService = DependencyFactory.GetInstance<IUserService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
               InitializePage();
            }

            // Si el usuario no es administrador, no puede acceder a esta página
            if (LoginUser.USU_NIVEL.ToString() != "100")
            {
                Session["ErrorMessage"] = "No tienes permiso para acceder a esta página";
                this.Response.Redirect("~/Views/Systems/Users/Users.aspx");
            }

        }

        protected void InitializePage()
        {

            var userId = this.Request.QueryString["id"];

            if (userId == null)
            {

                this.titleHeader.InnerText = "Nuevo usuario";
                this.btnUpdate.Visible = false;
                this.btnStatus.Visible = false;
                this.btnDelete.Visible = false;
                this.btnSendEmail.Visible = false;
                this.btnSendEmailNewUser.Visible = false;
                this.btnResetPassword.Visible = false;

            }
            else
            {

                User user = userService.GetUserByCode(Convert.ToInt32(userId));

                if (user == null)
                {
                    Session["ErrorMessage"] = "Usuario no encontrado";
                    this.Response.Redirect("~/Views/Usuarios/Usuarios.aspx");
                }

                ViewState["CurrentUserId"] = user.USU_CODIGO;

                if (user.Obsolete == true)
                {
                    this.btnStatus.Text = "Activar";
                }

                this.titleHeader.InnerText = "Editar usuario";
                this.txtName.Text = user.USU_NOMBRE;
                this.txtLastName.Text = user.USU_APELLIDOS;
                this.txtLogin.Text = user.USU_LOGIN;
                this.txtEmail.Text = user.USU_EMAIL;
                this.radDropType.SelectedValue = user.USU_I_G;
                this.radDropLevel.SelectedValue = user.USU_NIVEL.ToString();
                this.btnInsert.Visible = false;
                this.btnSendEmailNewUser.Visible = false;

                if (user.USU_CODIGO == LoginUser.USU_CODIGO)
                {
                    this.btnStatus.Visible = false;
                    this.btnResetPassword.Visible = false;
                    this.btnDelete.Visible = false;
                    this.btnSendEmail.Visible = false;
                }

                if (user.USU_EMAIL == null)
                {
                    this.btnSendEmail.Visible = false;
                }

            }

        }

        protected void btnInsert_OnClick(object sender, EventArgs e)
        {

            var strBuilder    = new StringBuilder();
            var plainPassword = PlainPasswordGenerator.GeneratePassword();
            var email         = this.txtEmail.Text;

            if (email == null || !EmailValidator.IsValidCorporateEmail(email))
            {
                strBuilder.Append("El email no es valido. Debe ser un email corporativo de la UIMP.");
                this.ShowMessage(this.RadNotification, "Email no valido", strBuilder, MessageType.Warning);
                return;
            }

            try
            {
                User user = new User
                {
                    USU_NOMBRE = this.txtName.Text,
                    USU_APELLIDOS = this.txtLastName.Text,
                    USU_LOGIN = this.txtLogin.Text,
                    USU_EMAIL = email,
                    USU_I_G = this.radDropType.SelectedValue,
                    USU_NIVEL = Convert.ToByte(this.radDropLevel.SelectedValue),
                    USU_PASSWORD = SessionHelper.Encrypt(plainPassword),
                    USU_CODIGO_MODIFICACION = LoginUser.USU_CODIGO
                };

                var insert = userService.InsertUser(user);
                
                switch (insert.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Usuario.");
                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("Ya existe un Usuario con ese Login.");
                        break;
                }

                if (insert.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error insertando el Usuario", strBuilder, MessageType.Warning);
                }

                if(insert.ResponseCode == ResponseCode.Ok)
                {

                    this.txtName.Enabled        = false;
                    this.txtLastName.Enabled    = false;
                    this.txtLogin.Enabled       = false;
                    this.txtEmail.Enabled       = false;
                    this.radDropType.Enabled    = false;
                    this.radDropLevel.Enabled   = false;
                    this.btnInsert.Visible      = false;
                    this.btnSendEmail.Visible   = false;
                    this.btnSendEmailNewUser.Visible = true;

                    strBuilder.Append("El Usuario ha sido insertado correctamente.");
                    this.ShowMessage(this.RadNotification, "Usuario insertado con exito", strBuilder, MessageType.Ok);

                }

            }
            catch (Exception ex)
            {
                LogError(ex, "Error insertando el Usuario.");
                strBuilder.Append("Ha ocurrido un error insertando el Usuario en cuestión.");
                this.ShowMessage(this.RadNotification, "Error insertando el Usuario", strBuilder, MessageType.Deny);
            }

        }
        
        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {

            var strBuilder = new StringBuilder();
            string userId  = this.Request.QueryString["id"];

            if (userId == null)
            {
                Session["ErrorMessage"] = "Usuario no encontrado";
                this.Response.Redirect("~/Views/Usuarios/Usuarios.aspx");
            }

            try
            {
                // recibimos los datos del input via ajax del formulario y los validamos
                var email = this.txtEmail.Text;

                if (email == null || !EmailValidator.IsValidCorporateEmail(email))
                {
                    strBuilder.Append("El email no es valido. Debe ser un email corporativo de la UIMP.");
                    this.ShowMessage(this.RadNotification, "Email no valido", strBuilder, MessageType.Warning);
                    return;
                }

                var user = new User
                {
                    USU_CODIGO = Convert.ToInt32(userId),
                    USU_NOMBRE = this.txtName.Text,
                    USU_APELLIDOS = this.txtLastName.Text,
                    USU_LOGIN = this.txtLogin.Text,
                    USU_EMAIL = email,
                    USU_I_G = this.radDropType.SelectedValue,
                    USU_NIVEL = Convert.ToByte(this.radDropLevel.SelectedValue),
                    USU_CODIGO_MODIFICACION = LoginUser.USU_CODIGO
                };

                var update = userService.UpdateUser(user);

                switch (update.ResponseCode)
                {
                    case ResponseCode.Invalid:
                        strBuilder.Append("Debe completar todos los datos del Usuario");
                        break;
                    case ResponseCode.Found:
                        strBuilder.Append("Ya existe un Usuario con ese Login");
                        break;
                }

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    this.ShowMessage(this.RadNotification, "Error insertando el Usuario", strBuilder, MessageType.Warning);
                }

                if (update.ResponseCode == ResponseCode.Ok)
                {
                    strBuilder.Append("El Usuario ha sido actualizado correctamente");
                    this.ShowMessage(this.RadNotification, "Usuario actualizado con exito", strBuilder, MessageType.Ok);
                }

            }
            catch (Exception ex)
            {
                LogError(ex, "Error insertando el Usuario.");
                strBuilder.Append("Ha ocurrido un error insertando el Usuario en cuestión.");
                this.ShowMessage(this.RadNotification, "Error insertando el Usuario", strBuilder, MessageType.Deny);
            }

        }

        protected void btnUpdateStatus_Onclick(object sender, EventArgs e)
        {

            var strBuilder = new StringBuilder();
            var userId = this.Request.QueryString["id"];

            if (userId == null)
            {
                Session["ErrorMessage"] = "Usuario no encontrado";
                this.Response.Redirect("~/Views/Usuarios/Usuarios.aspx");
            }

            User user = userService.GetUserByCode(Convert.ToInt32(userId));

            if (user == null)
            {
                Session["ErrorMessage"] = "Usuario no encontrado";
                this.Response.Redirect("~/Views/Usuarios/Usuarios.aspx");

            }

            if (user.USU_CODIGO == LoginUser.USU_CODIGO)
            {
                strBuilder.Append("No puedes desactivar tu propio usuario.");
                this.ShowMessage(this.RadNotification, "Error actualizando el estado del Usuario", strBuilder, MessageType.Deny);
                return;
            }

            try
            {

                bool obsolete = user.Obsolete == true ? false : true;
                var update    = userService.UpdateStatus(user.USU_CODIGO, obsolete);

                if (update.ResponseCode == ResponseCode.Ok)
                {
                    strBuilder.Append("El estado del Usuario ha sido actualizado correctamente.");
                    this.ShowMessage(this.RadNotification, "Estado del Usuario actualizado", strBuilder, MessageType.Ok);
                    this.btnStatus.Text = obsolete == true ? "Activar" : "Desactivar";
                }
                else
                {
                    strBuilder.Append("Ha ocurrido un error actualizando el estado del Usuario en cuestión.");
                    this.ShowMessage(this.RadNotification, "Error actualizando el estado del Usuario", strBuilder, MessageType.Deny);
                }

            }

            catch (Exception ex)
            {
                LogError(ex, "Error actualizando el estado del Usuario.");
                strBuilder.Append("Ha ocurrido un error actualizando el estado del Usuario en cuestión.");
                this.ShowMessage(this.RadNotification, "Error actualizando el estado del Usuario", strBuilder, MessageType.Deny);
            }

        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Views/Systems/Users/Users.aspx");
        }

        protected void btnSendMailNewUser_Onclick(object sender, EventArgs e)
        {

            string usu_login     = txtLogin.Text.Trim();
            var strBuilder       = new StringBuilder();
            var appSettingsEmail = (NameValueCollection)ConfigurationManager.GetSection("appSettingsEmail");

            if (string.IsNullOrEmpty(usu_login))
            {
                strBuilder.Append("El campo Usuario es requerido.");
                this.ShowMessage(this.RadNotification, "El campo Usuario es requerido", strBuilder, MessageType.Warning);
                return;
            }

            var user = userService.GetUserByUserName(usu_login);

            if (user == null)
            {
                strBuilder.Append("El usuario no se encuentra registrado.");
                this.ShowMessage(this.RadNotification, "El usuario no se encuentra registrado", strBuilder, MessageType.Warning);
                return;
            }

            if (string.IsNullOrEmpty(user.USU_EMAIL) || string.IsNullOrEmpty(user.USU_PASSWORD) || string.IsNullOrEmpty(user.USU_NOMBRE) || string.IsNullOrEmpty(user.USU_APELLIDOS))
            {
                strBuilder.Append("Faltan datos de acceso. Por favor, contacte con el admin");
                this.ShowMessage(this.RadNotification, "Faltan datos de acceso. Por favor, contacte con el admin", strBuilder, MessageType.Warning);
                return;
            }

            if (!EmailValidator.IsValidCorporateEmail(user.USU_EMAIL))
            {
                strBuilder.Append("El email no es valido. Debe ser un email corporativo de la UIMP.");
                this.ShowMessage(this.RadNotification, "Email no valido", strBuilder, MessageType.Warning);
                return;
            }

            string from      = appSettingsEmail["smtp.from.noreply"];
            string userName  = user.USU_NOMBRE + " " + user.USU_APELLIDOS;
            string userLogin = user.USU_LOGIN;
            string userEmail = user.USU_EMAIL;
            string password  = SessionHelper.Decrypt(user.USU_PASSWORD);
            string subject   = "Credenciales de acceso a la plataforma";
            string emailBody = EmailTemplateHelper.GetEmailBodyRecoveryCredentias("~/Views/EmailTemplates/CredentialsTemplate.html", userName, userLogin, password);
            bool isBodyHtml  = true;

            EmailSender.SendEmail(from, userEmail, subject, emailBody, isBodyHtml);

            this.btnSendEmailNewUser.Visible = false;

            strBuilder.Append("Se ha enviado un correo electrónico con las credenciales de acceso.");
            this.ShowMessage(this.RadNotification, "Correo electrónico enviado", strBuilder, MessageType.Ok);


            return;

        }


        #endregion

        #region Public Static Methods


        [WebMethod]
        public static string DeleteUser(string user_id)
        {

            JavaScriptSerializer js = new JavaScriptSerializer();

            User user = userService.GetUserByCode(Convert.ToInt32(user_id));

            if (user == null)
            {
                return js.Serialize(new
                {
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    id = user_id,
                    message = "Usuario no encontrado",
                    status = "error"
                });              
            }

            //eliminar usuario
            var response = userService.DeleteUser(user.USU_CODIGO);

            if (response.ResponseCode == ResponseCode.Ok)
            {
                return js.Serialize(new
                {
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    id = user_id,
                    message = "Exito eliminando el usuario",
                    status = "ok"
                });
            }

            if (response.ResponseCode == ResponseCode.NotFound)
            {
                return js.Serialize(new
                {
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    id = user_id,
                    message = "Usuario no encontrado",
                    status = "error"
                });
            }

            if (response.ResponseCode == ResponseCode.Invalid)
            {
                return js.Serialize(new
                {
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    id = user_id,
                    message = "Este usuario no puede ser eliminado",
                    status = "error"
                });
            }

            return js.Serialize(new
            {
                date    = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                id = user_id,
                message = "Error desconocido",
                status = "error"
            });
        }

        [WebMethod]
        public static string SendEmail(string user_id)
        {

            JavaScriptSerializer js = new JavaScriptSerializer();
            User user               = userService.GetUserByCode(Convert.ToInt32(user_id));
            var appSettingsEmail    = (NameValueCollection)ConfigurationManager.GetSection("appSettingsEmail");

            if (user == null)
            {
                return js.Serialize(new
                {
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    id = user_id,
                    message = "Usuario no encontrado",
                    status = "error"
                });
            }

            if(user.USU_EMAIL == null)
            {
                return js.Serialize(new
                {
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    id = user_id,
                    message = "Este usuario no tiene un email asociado",
                    status = "error"
                });
            }

            if (!EmailValidator.IsValidCorporateEmail(user.USU_EMAIL))
            {
                return js.Serialize(new
                {
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    id = user_id,
                    message = "Este usuario no tiene un email corporativo válido",
                    status = "error"
                });
            }

            if (string.IsNullOrEmpty(user.USU_EMAIL) || string.IsNullOrEmpty(user.USU_PASSWORD) || string.IsNullOrEmpty(user.USU_NOMBRE) || string.IsNullOrEmpty(user.USU_APELLIDOS))
            {

                return js.Serialize(new
                {
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    id = user_id,
                    message = "Faltan datos de acceso. Por favor complete todos los campos",
                    status = "error"
                });

            }

            if (!user.USU_EMAIL.Contains("@") || !user.USU_EMAIL.Contains("."))
            {
                return js.Serialize(new
                {
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    id = user_id,
                    message = "Email no valido",
                    status = "error"
                });
            }

            // vamos a comprobar que el algoritmo de encriptacionsea valido
            if (!SessionHelper.IsValidEncryption(user.USU_PASSWORD))
            {
                return js.Serialize(new
                {
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    id = user_id,
                    message = "El cifrado de la contraeña no es valido. Resetea de nuevo la contraseña",
                    status = "error"
                });
            }

            string from = appSettingsEmail["smtp.from.noreply"];
            string userName  = user.USU_NOMBRE + " " + user.USU_APELLIDOS;
            string userLogin = user.USU_LOGIN;
            string userEmail = user.USU_EMAIL;
            string password  = SessionHelper.Decrypt(user.USU_PASSWORD);
            string subject   = "Credenciales de acceso a la plataforma CONTAPRE";
            string emailBody = EmailTemplateHelper.GetEmailBodyRecoveryCredentias("~/Views/EmailTemplates/CredentialsTemplate.html", userName, userLogin, password);
            bool isBodyHtml  = true;

            EmailSender.SendEmail(from,userEmail, subject, emailBody, isBodyHtml);

            return js.Serialize(new
            {
                date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                id = user_id,
                message = "Email enviado",
                status = "ok"
            });
        }

        [WebMethod]
        public static string ResetPassword(string user_id)
        {

            JavaScriptSerializer js = new JavaScriptSerializer();

            User user = userService.GetUserByCode(Convert.ToInt32(user_id));

            if (user == null)
            {
                return js.Serialize(new
                {
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    id = user_id,
                    message = "Usuario no encontrado",
                    status = "error"
                });
            }
            string plainPassword = PlainPasswordGenerator.GeneratePassword();
            user.USU_PASSWORD    = SessionHelper.Encrypt(plainPassword);
            var update           = userService.UpdatePassword(user);

            if (update.ResponseCode == ResponseCode.Ok)
            {
                return js.Serialize(new
                {
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    id = user_id,
                    message = "Contraseña reseteada",
                    status = "ok"
                });
            }

            if (update.ResponseCode == ResponseCode.NotFound)
            {
                return js.Serialize(new
                {
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    id = user_id,
                    message = "El usuario no existe o se ha eliminado del sistema",
                    status = "error"
                });
            }

            return js.Serialize(new
            {
                date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                id = user_id,
                message = "Error desconocido",
                status = "error"
            });
        }


        #endregion

    }


}