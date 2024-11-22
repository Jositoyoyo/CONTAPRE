namespace Dimatica.ContaPre.Presentation.Views.ExtraBudgetary
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using System.Web.UI;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class SeeTreasuryNotes : BasePage
    {
        #region Public Properties

        public int TreasuryId
        {
            get
            {
                var o = this.Session["_treasuryId"];

                if (o == null)
                {
                    o = 0;
                    this.Session["_treasuryId"] = o;
                }

                return Convert.ToInt32(o);
            }

            set
            {
                this.Session["_treasuryId"] = value;
            }
        }

        public int ExtraBudgetaryId
        {
            get
            {
                var o = this.Session["_extraBudgetaryId"];

                if (o == null)
                {
                    o = 0;
                    this.Session["_extraBudgetaryId"] = o;
                }

                return Convert.ToInt32(o);
            }

            set
            {
                this.Session["_extraBudgetaryId"] = value;
            }
        }

        #endregion


        private ITreasuriesService treasuriesService = DependencyFactory.GetInstance<ITreasuriesService>();

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");

            if (!this.IsPostBack)
            {
                var extraBudgetaryId = this.Request.QueryString["extraBudgetaryId"];
                var treasuryId = this.Request.QueryString["treasuryId"];

                if (string.IsNullOrWhiteSpace(extraBudgetaryId) || string.IsNullOrWhiteSpace(treasuryId))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseWindows", $"CloseWindows(1);", true);
                }
                else
                {
                    this.ExtraBudgetaryId = Convert.ToInt32(extraBudgetaryId);
                    this.TreasuryId = Convert.ToInt32(treasuryId);
                }
            }
        }

        #endregion

        protected void RgTreasuryNotes_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var list = new List<PRE_TESORERIA>();

            if (this.TreasuryId != 0)
            {
                list = this.treasuriesService.GetAllById(this.TreasuryId);
            }

            this.RgTreasuryNotes.DataSource = list;
        }

        protected void RgDetails_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var list = new List<PRE_TESORERIA>();

            if (this.TreasuryId != 0)
            {
                list = this.treasuriesService.GetDocuments(null, this.ExtraBudgetaryId, this.TreasuryId);
            }

            this.RgDetails.DataSource = list;
        }
    }
}