namespace Dimatica.ContaPre.Presentation.Views.List
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Procedures;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class SituationConceptFilter : BasePage
    {
        #region Fields

        private IBudgetApplicationsService budgetApplicationsService = DependencyFactory.GetInstance<IBudgetApplicationsService>();

        private IBudgetsService budgetsService = DependencyFactory.GetInstance<IBudgetsService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");

            if (!this.IsPostBack)
            {
                var years = this.budgetsService.GetYearsByType("I");

                this.RcYears.DataSource = years;
                this.RcYears.DataBind();

                var selected = this.RcYears.SelectedValue;

                if (!string.IsNullOrWhiteSpace(selected))
                {
                    this.FillApplications(selected);
                }
            }
        }

        private void FillApplications(string year)
        {
            var applications = new List<BudgetApplication>();

            if (!string.IsNullOrWhiteSpace(year))
            {
                var realYear = Convert.ToInt32(year);

                applications = this.budgetApplicationsService.GetChaptersNumbersByTypeByYear("I", realYear);
            }

            this.RcApplications.DataSource = applications;
            this.RcApplications.DataBind();
        }

        protected void RcYears_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var year = string.IsNullOrWhiteSpace(e.Value) ? string.Empty : e.Value;
            this.FillApplications(year);
        }

        #endregion
    }
}