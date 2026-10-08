namespace Dimatica.ContaPre.Presentation.Views.Account
{
    #region NameSpaces

    using System;
    using System.Text;
    using System.Configuration;
    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.Presentation.Helpers.Email;
    using Dimatica.ContaPre.Presentation.Helpers.PlainPassword;
    using Dimatica.ContaPre.Presentation.Helpers.Session;
        using Dimatica.ContaPre.Presentation.Views.Shared;
    using System.Collections.Specialized;
    using Dimatica.ContaPre.Presentation.Helpers.DataValidation;
    using System.Collections.Generic;
    using Dimatica.ContaPre.OL.Business;

    #endregion

    public partial class ForgotPassword : BasePage
    {

        #region Static Fields and Constants

        private static IUserService userService = DependencyFactory.GetInstance<IUserService>();

        #endregion

        #region Page Events
       
        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {

            string usu_login     = RTbUser.Text.Trim();
            var strBuilder       = new StringBuilder();
            var appSettingsEmail = (NameValueCollection)ConfigurationManager.GetSection("appSettingsEmail");

            if (string.IsNullOrEmpty(usu_login))
            {
                this.lblError.Visible = true;
                this.lblError.InnerText = "El campo Usuario es requerido";
                strBuilder.Append("El campo Usuario es requerido.");
                this.ShowMessage(this.RadNotification, "El campo Usuario es requerido", strBuilder, MessageType.Warning);
                return;
            }    

            var user = userService.GetUserByUserName(usu_login);

            if (user == null)
            {
                this.lblError.Visible = true;
                this.lblError.InnerText = "El usuario no se encuentra registrado";
                strBuilder.Append("El usuario no se encuentra registrado.");
                this.ShowMessage(this.RadNotification, "El usuario no se encuentra registrado", strBuilder, MessageType.Warning);
                return;
            }

            if (string.IsNullOrEmpty(user.USU_EMAIL) || string.IsNullOrEmpty(user.USU_PASSWORD) || string.IsNullOrEmpty(user.USU_NOMBRE) || string.IsNullOrEmpty(user.USU_APELLIDOS))
            {
                this.lblError.Visible = true;
                this.lblError.InnerText = "No se pueden enviar credenciales";
                strBuilder.Append("Faltan datos de acceso. Por favor, contacte con el admin");
                this.ShowMessage(this.RadNotification, "Faltan datos de acceso. Por favor, contacte con el admin", strBuilder, MessageType.Warning);
                return;
            }

            if (!EmailValidator.IsValidCorporateEmail(user.USU_EMAIL))
            {
                this.lblError.Visible = true;
                this.lblError.InnerText = "No se pueden enviar credenciales";
                strBuilder.Append("El email no es valido. Debe ser un email corporativo de la UIMP.");
                this.ShowMessage(this.RadNotification, "Email no valido", strBuilder, MessageType.Warning);
                return;
            }

            // vamos a comprobar que el algoritmo de encriptacionsea valido. Si no es asi intentamos crear una nueva contraseña
            if (!SessionHelper.IsValidEncryption(user.USU_PASSWORD))
            {
                var plainPassword = PlainPasswordGenerator.GeneratePassword();
                user.USU_PASSWORD = SessionHelper.Encrypt(plainPassword);
                var update        = userService.UpdatePassword(user);

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    this.lblError.Visible = true;
                    this.lblError.InnerText = "No se pueden enviar credenciales";
                    strBuilder.Append("El cifrado de la contraeña no es valido y no se pudo crear una nueva. Por favor, contacte con el admin.");
                    this.ShowMessage(this.RadNotification, "Email no valido", strBuilder, MessageType.Warning);
                    return;
                }
        
            }            

            string from      = appSettingsEmail["smtp.from.noreply"];
            string userEmail = user.USU_EMAIL;
            string subject   = "Credenciales de acceso a la plataforma CONTAPRE";

            var parameters = new Dictionary<string, string>
            {
                { "UserName",  user.USU_NOMBRE + " " + user.USU_APELLIDOS },
                { "UserLogin", user.USU_LOGIN },
                { "Password", SessionHelper.Decrypt(user.USU_PASSWORD) },
                { "AppUrl",   ConfigurationManager.AppSettings["AppUrl"] ?? "" } 
            };

            string emailBody = EmailTemplate.GetEmailBodyDynamicParams("~/Views/EmailTemplates/CredentialsTemplate.html", parameters);
            bool isBodyHtml  = true;

            EmailSender.SendEmail(from, userEmail, subject, emailBody, isBodyHtml);
            
            strBuilder.Append("Se ha enviado un correo electrónico con las credenciales de acceso.");
            this.ShowMessage(this.RadNotification, "Correo electrónico enviado", strBuilder, MessageType.Ok);
            
            this.lblError.Visible          = false;
            this.btnForgotPassword.Visible = false;
            this.LabelUser.Visible         = false;  
            this.RTbUser.Visible           = false;
            this.lblMsg.InnerText          = "Por favor, revise su correo corporativo y siga las instrucciones de acceso";

            return;

        }

        #endregion
    }

}
