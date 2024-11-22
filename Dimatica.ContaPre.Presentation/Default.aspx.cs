namespace Dimatica.ContaPre.Presentation
{
    #region NameSpaces

    using System;

    #endregion

    public partial class Default : System.Web.UI.Page
    {
        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Views/Account/Login.aspx");
        }

        #endregion
    }
}