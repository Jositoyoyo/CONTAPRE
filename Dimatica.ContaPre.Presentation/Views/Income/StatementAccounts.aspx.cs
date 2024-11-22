namespace Dimatica.ContaPre.Presentation.Views.Income
{
    #region NameSpaces

    using System;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class StatementAccounts : BasePage
    {
        #region Fields

        private IAccountRestrictedService accountRestrictedService = DependencyFactory.GetInstance<IAccountRestrictedService>();

        private ITonnageSheetsService tonnageSheetsService = DependencyFactory.GetInstance<ITonnageSheetsService>();

        private ITreasuriesService treasuriesService = DependencyFactory.GetInstance<ITreasuriesService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "StatementAccounts";

                this.RmyExerciseYear.SelectedDate = DateTime.Now;

                var accounts = this.accountRestrictedService.GetAccountsRestricted("I");
                this.RcAccounts.DataSource = accounts;
                this.RcAccounts.DataBind();
            }
        }

        protected void btnFind_OnClick(object sender, EventArgs e)
        {
            this.FillAccounts(true);
        }

        protected void RgAccounts_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.FillAccounts(false);
        }

        protected void RgAccounts_OnPreRender(object sender, EventArgs e) { }

        private void FillAccounts(bool manual)
        {
            var exerciseYear = this.RmyExerciseYear.SelectedDate?.Year;
            var budgetYear = this.RmyBudgetYear.SelectedDate?.Year;
            var accountId = Convert.ToInt32(this.RcAccounts.SelectedValue);
            var sinceDate = this.RdSince.SelectedDate?.ToString("yyyy-MM-dd");
            var untilDate = this.RdUntil.SelectedDate?.ToString("yyyy-MM-dd");

            var accounts = this.tonnageSheetsService.GetRestrictedAccountStatement(exerciseYear, budgetYear, accountId, sinceDate, untilDate);

            this.RgAccounts.DataSource = accounts;

            if (manual)
            {
                this.RgAccounts.DataBind();
            }

            this.RpbFilter.CollapseAllItems();
        }

        #endregion
    }
}