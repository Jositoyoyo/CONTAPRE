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

    public partial class SeeProviders : BasePage
    {
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

        protected void RgProviders_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var providers = new List<PRE_PROVEEDOR>();

            if (this.FileId != 0)
            {
                providers = this.accountingRecordsService.GetProviders(this.FileId);
            }

            this.RgProviders.DataSource = providers;
        }

        #endregion
    }
}