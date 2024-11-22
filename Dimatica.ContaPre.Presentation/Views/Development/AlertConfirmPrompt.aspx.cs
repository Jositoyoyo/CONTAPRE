namespace Dimatica.ContaPre.Presentation.Views.Development
{

    #region NameSpaces

    using Dimatica.ContaPre.Presentation.Views.Shared;
    using System;

    #endregion

    public partial class AlertConfirmPrompt : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "AlertConfirmPrompt";
            }

        }
    }
}