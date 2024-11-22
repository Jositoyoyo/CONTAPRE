namespace Dimatica.ContaPre.Presentation.Views.List
{
    #region NameSpaces

    using System;

    using Dimatica.ContaPre.Presentation.Views.Shared;

    #endregion

    public partial class TreasuriesList : BasePage
    {
        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "TreasuriesList";
            }
        }

        #endregion
    }
}