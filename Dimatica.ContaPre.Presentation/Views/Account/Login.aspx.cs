namespace Dimatica.ContaPre.Presentation.Views.Account
{
    #region NameSpaces

    using System;
    using System.Configuration;
    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.Presentation.Helpers;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    #endregion

    public partial class Login : BasePage
    {
        #region Fields

        IUserService userService = DependencyFactory.GetInstance<IUserService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                var signOut = this.Request.QueryString["SignOut"];

                if (signOut != null)
                {
                    if (signOut == "1")
                    {
                        LoginUser = null;
                        this.Session["_originPage"] = null;
                    }
                }

                this.environment.InnerText = ConfigurationManager.AppSettings["environment"];
                this.version.InnerText     = ConfigurationManager.AppSettings["version"];
                this.ErrorLogin.InnerText  = string.Empty;
            }
        }

        protected void BtnLogin_OnClick(object sender, EventArgs e)
        {
            try
            {
                this.ResetError(string.Empty);

                if (!this.IsValid)
                {
                    this.ResetError("Credenciales inválidas...");
                    return;
                }

                if (string.IsNullOrWhiteSpace(this.RTbMail.Text) && string.IsNullOrWhiteSpace(this.RTbPassword.Text))
                {
                    this.ResetError("Credenciales inválidas...");
                    return;
                }

                var encryptPassword = SessionHelper.Encrypt(this.RTbPassword.Text);
                var user = this.userService.Login(this.RTbMail.Text, encryptPassword);

                if (user == null)
                {
                    this.ResetError("Credenciales inválidas...");
                    return;
                }

                LoginUser = user;
                var originPage = this.Session["_originPage"];

                if (originPage == null)
                {
                    this.Response.Redirect("~/Views/Home/Home.aspx");
                }
                else
                {
                    var path = $"~{originPage}";
                    this.Response.Redirect(path);
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error en el Login");
                this.ResetError("Error de conexión...");
            }
        }

        private void ResetError(string message)
        {
            this.ErrorLogin.InnerText = message;
        }

        #endregion
    }
}