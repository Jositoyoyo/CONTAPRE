namespace Dimatica.ContaPre.Presentation.Views.Spend
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class SeeMultiYears : BasePage
    {
        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");

            if (!this.IsPostBack)
            {
                var fileId = this.Request.QueryString["fileId"];

                if (string.IsNullOrWhiteSpace(fileId))
                {
                    this.Response.Redirect("~//Views//Spend//SpendRecords.aspx");
                }
                else
                {
                    this.FileId = Convert.ToInt32(fileId);
                }
            }
        }

        #endregion

        protected void RgDocuments_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var documents = new List<PRE_EXPEDIENTE_CONTABLE>();

            if (this.FileId != 0)
            {
                documents = this.accountingRecordsService.GetMultiYears(this.FileId);
            }

            this.RgDocuments.DataSource = documents;
        }

        #region Fields

        private IAccountingRecordsService accountingRecordsService = DependencyFactory.GetInstance<IAccountingRecordsService>();

        #endregion

        #region Public Properties

        public int FileId
        {
            get
            {
                var o = this.Session["_fileId"];

                if (o == null)
                {
                    o = 0;
                    this.Session["_fileId"] = o;
                }

                return Convert.ToInt32(o);
            }

            set
            {
                this.Session["_fileId"] = value;
            }
        }

        #endregion
    }
}