namespace Dimatica.ContaPre.Presentation.Views.List
{
    #region NameSpaces

    using System;
    using System.Globalization;
    using System.Threading;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    #endregion

    public partial class DebitAndCreditFilter : BasePage
    {
        #region Fields

        private IExtraBudgetaryApplicationsService extraBudgetaryApplicationsService = DependencyFactory.GetInstance<IExtraBudgetaryApplicationsService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");

            if (!this.IsPostBack)
            {
                var applications = this.extraBudgetaryApplicationsService.GetExtraBudgetaryApplicationsToCombo();

                applications.Insert(
                                    0,
                                    new PRE_EXTRAPRESUPUESTARIA
                                            {
                                                    EXTRAPRE_CODIGO = -2
                                            });

                this.RcApplications.DataSource = applications;
                this.RcApplications.DataBind();

                this.RdSince.SelectedDate = DateTime.Now;
                this.RdUntil.SelectedDate = DateTime.Now;
            }
        }

        #endregion
    }
}