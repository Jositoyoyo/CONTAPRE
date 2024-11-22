namespace Dimatica.ContaPre.Presentation.Views.Spend
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class SeeNotesTreasuries : BasePage
    {
        #region Fields

        private ITreasuriesService treasuriesService = DependencyFactory.GetInstance<ITreasuriesService>();

        #endregion

        #region Public Properties

        public int ParentId
        {
            get
            {
                var o = this.Session["_parentId"];

                if (o == null)
                {
                    o = 0;
                    this.Session["_parentId"] = o;
                }

                return Convert.ToInt32(o);
            }

            set
            {
                this.Session["_parentId"] = value;
            }
        }

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

        public bool HasTreasuries
        {
            get
            {
                var o = this.Session["_hasTreasuries"];

                if (o == null)
                {
                    o = false;
                    this.Session["_hasTreasuries"] = o;
                }

                return Convert.ToBoolean(o);
            }

            set
            {
                this.Session["_hasTreasuries"] = value;
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
                var treasuryId = this.Request.QueryString["treasuryId"];

                if (string.IsNullOrWhiteSpace(documentId) || string.IsNullOrWhiteSpace(treasuryId))
                {
                    this.Response.Redirect("~//Views//Spend//SpendRecords.aspx");
                }
                else
                {
                    this.ParentId = Convert.ToInt32(documentId);
                    this.TreasuryId = Convert.ToInt32(treasuryId);
                }
            }
        }

        protected void RgNotesTreasuries_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var treasuries = new List<PRE_TESORERIA>();

            if (this.TreasuryId != -1)
            {
                treasuries = this.treasuriesService.GetAllById(this.TreasuryId);

                this.HasTreasuries = treasuries.Any();
            }

            this.RgNotesTreasuries.DataSource = treasuries;
        }

        protected void RgBreakdownNotesTreasuries_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var treasuries = new List<PRE_TESORERIA>();

            if (!this.HasTreasuries && this.TreasuryId != -1)
            {
                treasuries = this.treasuriesService.GetDocuments(null, null, this.TreasuryId);
            }
            else
            {
                treasuries = this.treasuriesService.GetDocuments(this.ParentId, null, null);
            }

            this.RgBreakdownNotesTreasuries.DataSource = treasuries;
        }

        #endregion
    }
}