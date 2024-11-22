namespace Dimatica.ContaPre.Presentation.Views.Spend
{
    #region NameSpaces

    using System;
    using System.Globalization;
    using System.Threading;

    using Dimatica.ContaPre.Presentation.Views.Shared;

    #endregion

    public partial class CertificateFilter : BasePage
    {
        #region Public Properties

        public int DocumentId
        {
            get
            {
                var o = this.Session["_documentId"];

                if (o == null)
                {
                    o = 0;
                    this.Session["_documentId"] = o;
                }

                return Convert.ToInt32(o);
            }

            set
            {
                this.Session["_documentId"] = value;
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
                var documentId = this.Request.QueryString["documentId"];

                if (string.IsNullOrWhiteSpace(documentId))
                {
                    this.Response.Redirect("~//Views//Spend//SpendRecords.aspx");
                }
                else
                {
                    this.DocumentId = Convert.ToInt32(documentId);
                }
            }
        }

        #endregion
    }
}