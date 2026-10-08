namespace Dimatica.ContaPre.Presentation.Views.Account
{
    #region NameSpaces

    using System;
    using System.Configuration;
    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.Presentation.Helpers.ApplicationVersion;
    using Dimatica.ContaPre.Presentation.Helpers.Session;
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
                this.ErrorLogin.InnerText  = string.Empty;
                this.version.InnerText = string.Empty;
                this.version.Attributes.Remove("title");

                var applicationVersion = ApplicationVersionLoader.LoadApplicationVersion();
                this.version.InnerText = applicationVersion.Version;

                if (!string.IsNullOrWhiteSpace(applicationVersion.ReleaseDate))
                {
                    this.version.Attributes["title"] = $"Release Date : {applicationVersion.ReleaseDate}";
                }

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
                this.RedirectToOriginPage();
            }
            catch (Exception ex)
            {
                LogError(ex, "Error en el Login");
                this.ResetError("Error de conexión...");
            }
        }

        private void RedirectToOriginPage()
        {
            var originPage = this.Session["_originPage"];

            if (originPage == null)
            {
                this.Response.Redirect("~/Views/Home/Home.aspx");
                return;
            }

            var path = $"~{originPage}";
            this.Response.Redirect(path);
        }

        private void ResetError(string message)
        {
            this.ErrorLogin.InnerText = message;
        }

        #endregion
    }
}
