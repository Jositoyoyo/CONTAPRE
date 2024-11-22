namespace Dimatica.ContaPre.Presentation.Views.Systems
{
    #region NameSpaces

    using System;
    using System.Text;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.Presentation.Helpers;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    #endregion

    public partial class ChangePassword : BasePage
    {
        #region Fields

        IUserService userService = DependencyFactory.GetInstance<IUserService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "ChangePassword";

                if (LoginUser == null)
                {
                    this.Response.Redirect("~/Views/Home/Home.aspx");
                }
                else
                {
                    this.lblUserName.Text = LoginUser.USU_LOGIN;
                }
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {

            var strBuilder = new StringBuilder();

            if (string.IsNullOrEmpty(this.txtCurrentPass.Text.Trim()) || string.IsNullOrEmpty(this.txtNewPass.Text.Trim()) || string.IsNullOrEmpty(this.txtConfirmNewPass.Text.Trim()))
            {
                return;
            }

            var currentPassword = SessionHelper.Encrypt(this.txtCurrentPass.Text);

            var login = this.userService.Login(this.lblUserName.Text, currentPassword);

            if (login == null)
            {
                strBuilder.Append("Las credenciales aportadas no coinciden con el usuario en cuestión.");
                this.ShowMessage(this.RadNotification, "Imposible cambiar password", strBuilder, MessageType.Warning);
                return;
            }

            if (string.IsNullOrEmpty(this.txtNewPass.Text.Trim()) || string.IsNullOrEmpty(this.txtConfirmNewPass.Text.Trim()))
            {
                strBuilder.Append("Los contraseñas nuevas no pueden ir vacias");
                this.ShowMessage(this.RadNotification, "Nose puede cambiar password", strBuilder, MessageType.Warning);
                return;
            }

            var newPassword     = this.txtNewPass.Text.Trim();
            var passwordPattern = @"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(newPassword, passwordPattern))
            {
                strBuilder.Append("La nueva contraseña no cumple con los requisitos de seguridad. La contraseña debe contener al menos 8 caracteres, incluyendo una letra mayúscula y un número. No se permiten caracteres especiales ni espacios.");
                this.ShowMessage(this.RadNotification, "Error de validación", strBuilder, MessageType.Warning);
                return;
            }

            if (!this.txtNewPass.Text.Trim().Equals(this.txtConfirmNewPass.Text.Trim()))
            {
                strBuilder.Append("Los contraseñas nuevas deben coincidir.");
                this.ShowMessage(this.RadNotification, "Nose puede cambiar password", strBuilder, MessageType.Warning);
                return;
            }

            string newPass = this.txtNewPass.Text.Trim();

            login.USU_PASSWORD = SessionHelper.Encrypt(newPass);

            var update = this.userService.UpdatePassword(login);

            if (update.ResponseCode == ResponseCode.Ok)
            {
                strBuilder.Append("Contraseña actualizada con éxito. Por favor no olvide guardarla en un lugar seguro.");
                this.ShowMessage(this.RadNotification, "Contraseña actualizada con éxito", strBuilder, MessageType.Ok);
                return;
            }

            if (update.ResponseCode == ResponseCode.NotFound)
            {
                strBuilder.Append("Ha ocurrido un error al actualizar la contraseña. Consulte con el admin del sistema.");
                this.ShowMessage(this.RadNotification, "El usuario no existe o se ha eliminado del sistema", strBuilder, MessageType.Warning);
                return;
            }

            strBuilder.Append("Error modificando el password de el Usuario.Consulte con el admin del sistem.");
            this.ShowMessage(this.RadNotification, "Error modificando el password de el Usuario", strBuilder, MessageType.Warning);
            return;
            
        }

    }

    #endregion
}