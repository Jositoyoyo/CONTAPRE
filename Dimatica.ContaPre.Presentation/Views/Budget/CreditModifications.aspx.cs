namespace Dimatica.ContaPre.Presentation.Views.Budget
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class CreditModifications : BasePage
    {
        #region Static Fields and Constants

        private static ICreditModificationsService creditModificationsService = DependencyFactory.GetInstance<ICreditModificationsService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "CreditModifications";

                var years = creditModificationsService.GetYears();

                this.RcYears.DataSource = years;
                this.RcYears.DataBind();
            }
        }

        protected void RgCreditModifications_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.FillCreditModifications(false);
        }

        protected void RgCreditModifications_OnPreRender(object sender, EventArgs e)
        {
            if (LoginUser.USU_NIVEL.ToString() == "10")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "hideAction();", true);
                this.RgCreditModifications.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
        }

        protected void RgCreditModifications_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "UpdateCreditModification")
            {
                var item = e.Item as GridDataItem;
                var creditModificationId = (int)item.GetDataKeyValue("MOD_CODIGO");

                this.Response.Redirect($"~/Views/Budget/ManageCreditModification.aspx?id={creditModificationId}");
            }
        }

        private void FillCreditModifications(bool manual)
        {
            var creditModifications = new List<PRE_MODIFICACION_CREDITO>();
            this.RgCreditModifications.DataSource = creditModifications;

            if (manual)
            {
                this.RgCreditModifications.DataBind();
            }

            var year = string.IsNullOrWhiteSpace(this.RcYears.SelectedValue) ? string.Empty : this.RcYears.SelectedValue;

            if (!string.IsNullOrEmpty(year))
            {
                var since = this.txtSince.Value == null ? (int?)null : Convert.ToInt32(this.txtSince.Value);
                var until = this.txtUntil.Value == null ? (int?)null : Convert.ToInt32(this.txtUntil.Value);

                var realYear = Convert.ToInt32(year);
                var creditModificationsResult = creditModificationsService.GetByFilters(realYear, since, until);

                foreach (var creditModification in creditModificationsResult)
                {
                    creditModifications.Add(creditModification);
                }
            }

            this.RgCreditModifications.DataSource = creditModifications;

            if (manual)
            {
                this.RgCreditModifications.DataBind();
            }
        }

        protected void btnFind_OnClick(object sender, EventArgs e)
        {
            this.FillCreditModifications(true);
        }

        #endregion
    }
}