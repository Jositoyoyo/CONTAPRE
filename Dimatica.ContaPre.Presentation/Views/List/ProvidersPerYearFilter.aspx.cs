namespace Dimatica.ContaPre.Presentation.Views.List
{
    #region NameSpaces

    using System;
    using System.Globalization;
    using System.Threading;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    #endregion

    public partial class ProvidersPerYearFilter : BasePage
    {
        #region Fields

        private IBudgetsService budgetsService = DependencyFactory.GetInstance<IBudgetsService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");

            if (!this.IsPostBack)
            {
                var years = this.budgetsService.GetYearsByType("G");

                this.RcYears.DataSource = years;
                this.RcYears.DataBind();
            }
        }

        #endregion
    }
}