namespace Dimatica.ContaPre.Presentation.Views.List
{
    #region NameSpaces

    using System;
    using System.Globalization;
    using System.Threading;

    using Dimatica.ContaPre.Presentation.Views.Shared;

    #endregion

    public partial class PaymentRecordFilter : BasePage
    {
        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");

            if (!this.IsPostBack)
            {
                this.RdSince.SelectedDate = DateTime.Now;
                this.RdUntil.SelectedDate = DateTime.Now;
            }
        }

        #endregion
    }
}