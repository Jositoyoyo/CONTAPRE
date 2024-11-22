namespace Dimatica.ContaPre.Presentation.Views.Treasury
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Threading;
    using System.Web.UI;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class SeeBoundDocuments : BasePage
    {
        #region Fields

        private IAccountingDocumentsService accountingDocumentsService = DependencyFactory.GetInstance<IAccountingDocumentsService>();

        private ITreasuriesService treasuriesService = DependencyFactory.GetInstance<ITreasuriesService>();

        #endregion

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

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");

            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "SeeBoundDocuments";

                var treasuryId = this.Request.QueryString["treasuryId"];

                if (string.IsNullOrWhiteSpace(treasuryId))
                {
                    this.Response.Redirect("~//Views//Treasury//ManageTreasury.aspx");
                }
                else
                {
                    this.TreasuryId = Convert.ToInt32(treasuryId);
                }
            }
        }

        protected void RgDocuments_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var documents = new List<PRE_TESORERIA>();

            if (this.TreasuryId != 0)
            {
                var documentsResult = this.treasuriesService.GetDocuments(null, null, this.TreasuryId);

                foreach (var document in documentsResult)
                {
                    documents.Add(document);
                }
            }

            this.RgDocuments.DataSource = documents;

            var total = this.treasuriesService.GetAmount(this.TreasuryId);
            var totalDocuments = this.treasuriesService.GetDocumentsAmount(this.TreasuryId);

            this.txtTotal.Text = total.ToString("N");
            this.txtTotalDocuments.Text = totalDocuments.ToString("N");
        }

        protected void RgDocuments_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            var treasuryDocumentId = (int)item.GetDataKeyValue("TESD_CODIGO");

            try
            {

                var connectToToOracle = Convert.ToBoolean(ConfigurationManager.AppSettings["connectToOracle"]);
                if (connectToToOracle)
                {
                    foreach (GridDataItem gridItem in this.RgDocuments.Items)
                    {
                        if (gridItem.GetDataKeyValue("DOC_CODIGO") != null)
                        {
                            var documentId = (int)gridItem.GetDataKeyValue("DOC_CODIGO");

                            var bills = this.accountingDocumentsService.GetPurchases(documentId);

                            foreach (var bill in bills)
                            {
                                var updatePayBank = this.accountingDocumentsService.UpdatePayBankOracle((DateTime?)null, (int)bill.CODFACTURAGEI);

                                var updateHistory = this.accountingDocumentsService.UpdateHistoryOracle(bill.NCERTIFICADO, "Se ha anulado el apunte de tesorería vinculado al pago de la factura.");
                            }
                        }
                    }
                }

                var delete = this.treasuriesService.DeleteDocument(treasuryDocumentId, LoginUser.USU_CODIGO);

                if (delete.ResponseCode != ResponseCode.Ok)
                {
                    switch (delete.ResponseCode)
                    {
                        case ResponseCode.Invalid:
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(0);", true);

                            break;
                        case ResponseCode.NotFound:
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(1);", true);

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex, "Error eliminando el Apunte de Tesorería.");
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(0);", true);
            }
        }

        #endregion
    }
}