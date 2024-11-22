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

    public partial class IncomesByPlaceFilter : BasePage
    {
        #region Fields

        private IBudgetsService budgetsService = DependencyFactory.GetInstance<IBudgetsService>();

        private ICostPlacesService costPlacesService = DependencyFactory.GetInstance<ICostPlacesService>();

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

                var places = this.costPlacesService.GetCostPlacesToCombo();
                places.Insert(
                              0,
                              new PRE_CENTRO_COSTE
                                      {
                                              CEN_CODIGO = -1
                                      });
                this.RcPlaces.DataSource = places;
                this.RcPlaces.DataBind();
            }
        }

        #endregion
    }
}