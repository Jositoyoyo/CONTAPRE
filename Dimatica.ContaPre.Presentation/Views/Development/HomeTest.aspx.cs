namespace Dimatica.ContaPre.Presentation.Views.Development
{
    #region NameSpaces

    using System;

    using Dimatica.ContaPre.Presentation.Views.Shared;

    #endregion

    public partial class HomeTest : BasePage
    {
        #region Private Methods

        protected void Page_Load(object sender, EventArgs e) 
        {

            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "EnviarEmail";
            }

        }

        protected void btnUpdateLabel_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Etiqueta actualizada: " + DateTime.Now.ToString();
        }

        #endregion
    }
}