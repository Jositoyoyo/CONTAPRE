namespace Dimatica.ContaPre.Presentation.Views.Shared
{
    #region NameSpaces

    using System;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI;

    using Dimatica.ContaPre.OL.Models;

    using NLog;

    using Telerik.Web.UI;

    #endregion

    public class BasePage : Page
    {
        #region Static Fields and Constants

        private static User loginUser;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region MessageType enum

        public enum MessageType
        {
            Info,

            Delete,

            Deny,

            Edit,

            Ok,

            Warning,

            None
        }

        #endregion

        #region Public Properties

        public static User LoginUser
        {
            get
            {
                return loginUser;
            }

            set
            {
                loginUser = value;
                HttpContext.Current.Session["_userLevel"] = value == null ? "10" : value.USU_NIVEL.ToString();
            }
        }

        #endregion

        #region Public Methods

        protected void InitializePage(string currentPage)
        {

            if (currentPage != "")
            {
                Session["_currentPage"] = currentPage;
            }

            // Verificar si hay un mensaje de error en la sesión
            if (Session["ErrorMessage"] != null)
             {
                string errorMessage = Session["ErrorMessage"].ToString();
                Session.Remove("ErrorMessage");
                ScriptManager.RegisterStartupScript(this, GetType(), "showErrorAlert", $"alert('{errorMessage}');", true);
             }

            if(LoginUser != null)
            {
                ViewState["CurrentUserId"]     = LoginUser.USU_NIVEL.ToString();
                ViewState["CurrentUserCodigo"] = LoginUser.USU_CODIGO.ToString();
            }
            
        }

        public void ShowMessage(RadNotification notificationPanel, string title, StringBuilder message, MessageType type)
        {
            notificationPanel.Title = title;
            notificationPanel.Text = message.ToString();
            notificationPanel.TitleIcon = type.ToString().ToLower();
            notificationPanel.ContentIcon = type.ToString().ToLower();
            notificationPanel.Show();
        }

        public static void LogError(Exception ex, string message)
        {
            Logger.Error(ex, message);
        }

        public static void LogWarn(string message)
        {
            Logger.Warn(message);
        }

        #endregion

        #region Private Methods

        protected DateTime? SetFilteredDate(GridItem item, string element)
        {
            if (item.OwnerTableView.GetColumn(element).CurrentFilterValue == string.Empty)
            {
                return new DateTime?();
            }
            else
            {
                return DateTime.Parse(item.OwnerTableView.GetColumn(element).CurrentFilterValue);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            var path = Path.GetFileName(this.Request.Url.AbsolutePath);

            if (LoginUser == null && path != "Login.aspx" && path != "ForgotPassword.aspx")
            {
                this.Session["_originPage"] = this.Request.Url.AbsolutePath;
                this.Response.Redirect("~/Views/Account/Login.aspx");
            }
        }

        #endregion
    }
}