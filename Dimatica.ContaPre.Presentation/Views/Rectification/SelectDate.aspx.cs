namespace Dimatica.ContaPre.Presentation.Views.Rectification
{
    #region NameSpaces

    using System;
    using System.Globalization;
    using System.Threading;

    using Dimatica.ContaPre.Presentation.Views.Shared;

    #endregion

    public partial class SelectDate : BasePage
    {
        #region Private Properties

        private string ICodes
        {
            get
            {
                var iCodes = this.Session["_iCodes"];

                if (iCodes == null)
                {
                    iCodes = string.Empty;

                    this.Session["_iCodes"] = iCodes;
                }

                return iCodes.ToString();
            }

            set
            {
                this.Session["_iCodes"] = value;
            }
        }

        private string ECodes
        {
            get
            {
                var eCodes = this.Session["_eCodes"];

                if (eCodes == null)
                {
                    eCodes = string.Empty;

                    this.Session["_eCodes"] = eCodes;
                }

                return eCodes.ToString();
            }

            set
            {
                this.Session["_eCodes"] = value;
            }
        }

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");

            if (!this.IsPostBack)
            {
                var iCodes = this.Request.QueryString["iCodes"];
                var eCodes = this.Request.QueryString["eCodes"];

                this.ICodes = iCodes;
                this.ECodes = eCodes;
                this.RdDate.SelectedDate = DateTime.Now;
            }
        }

        #endregion
    }
}