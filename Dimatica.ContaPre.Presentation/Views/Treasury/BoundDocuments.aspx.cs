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
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class BoundDocuments : BasePage
    {
        #region Fields

        private IAccountingDocumentsService accountingDocumentsService = DependencyFactory.GetInstance<IAccountingDocumentsService>();

        private IProvidersService providersService = DependencyFactory.GetInstance<IProvidersService>();

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

        public DateTime TreasuryDate
        {
            get
            {
                var o = this.Session["_treasuryDate"];

                if (o == null)
                {
                    o = DateTime.Now;
                    this.Session["_treasuryDate"] = o;
                }

                return Convert.ToDateTime(o);
            }

            set
            {
                this.Session["_treasuryDate"] = value;
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
                this.Session["_currentPage"] = "BoundDocuments";

                var treasuryId = this.Request.QueryString["treasuryId"];
                var year = this.Request.QueryString["year"];
                var date = this.Request.QueryString["date"];

                if (string.IsNullOrWhiteSpace(treasuryId) || string.IsNullOrWhiteSpace(year) || string.IsNullOrWhiteSpace(date))
                {
                    this.Response.Redirect("~//Views//Treasury//ManageTreasury.aspx");
                }
                else
                {
                    this.TreasuryId = Convert.ToInt32(treasuryId);
                    this.TreasuryDate = Convert.ToDateTime(date, new CultureInfo("es-ES"));

                    this.RmyYear.SelectedDate = new DateTime(Convert.ToInt32(year), 1, 1);

                    var providers = this.providersService.GetProvidersToCombo();
                    providers.Insert(
                                     0,
                                     new PRE_PROVEEDOR
                                     {
                                         PROV_CODIGO = -1,
                                         PROV_NOMBRE = "< Seleccione >"
                                     });

                    this.RcProviders.DataSource = providers;
                    this.RcProviders.DataBind();
                }
            }
        }

        protected void RgDocuments_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.FillDocuments(false);
        }

        protected void btnFind_OnClick(object sender, EventArgs e)
        {
            this.FillDocuments(true);
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var work = false;
            foreach (GridDataItem item in this.RgDocuments.MasterTableView.Items)
            {
                var checkBound = (RadCheckBox)item["Bound"].FindControl("checkBound");

                if ((bool)checkBound.Checked)
                {
                    work = true;
                    var origin = (string)item.GetDataKeyValue("ORIGEN");

                    int? documentId = null;
                    int? fileId = null;
                    var originCode = 0;

                    switch (origin)
                    {
                        case "I":
                            documentId = (int)item.GetDataKeyValue("DOC_CODIGO");
                            originCode = 2;
                            break;
                        case "G":
                            documentId = (int)item.GetDataKeyValue("DOC_CODIGO");
                            originCode = 1;
                            break;
                        case "EI":
                            fileId = (int)item.GetDataKeyValue("DOC_CODIGO");
                            originCode = 4;
                            break;
                        case "EG":
                            fileId = (int)item.GetDataKeyValue("DOC_CODIGO");
                            originCode = 3;
                            break;
                    }


                    var amount = (decimal)item.GetDataKeyValue("LIQUIDO");
                    var year = (int)item.GetDataKeyValue("ANO_PRESUPUESTO");
                    var checkNumber = (string)item.GetDataKeyValue("DOC_NUMERO_CHEQUE");
                    var documentApplication = (string)item.GetDataKeyValue("DOCUMENTO_APLICACION");
                    var fileNumber = item.GetDataKeyValue("NUMERO_EXPEDIENTE") == null ? (int?)null : (int)item.GetDataKeyValue("NUMERO_EXPEDIENTE");
                    var description = (string)item.GetDataKeyValue("PROV_NOMBRE");

                    var checkFinish = (RadCheckBox)item["Finish"].FindControl("checkFinish");
                    var finish = (bool)checkFinish.Checked;

                    var document = new PRE_TESORERIA_DOCUMENTO
                    {
                        TES_CODIGO = this.TreasuryId,
                        DOC_CODIGO = documentId,
                        EXP_EXTRAP_CODIGO = fileId,
                        TESD_IMPORTE_LIQUIDO = amount,
                        TESD_ANO_PRESUPUESTO = Convert.ToInt16(year),
                        TESD_NUMERO_CHEQUE = checkNumber,
                        ORI_CODIGO = Convert.ToByte(originCode),
                        TESD_DOCUMENTO = documentApplication,
                        TESD_APLICACION = documentApplication,
                        TESD_DESCRIPCION = description,
                        TESD_NUMERO_EXPEDIENTE = fileNumber,
                        Finish = finish,
                        USU_CODIGO = LoginUser.USU_CODIGO
                    };

                    var bound = this.treasuriesService.BoundDocument(document);

                    var connectToToOracle = Convert.ToBoolean(ConfigurationManager.AppSettings["connectToOracle"]);
                    if (connectToToOracle)
                    {
                        if (documentId != null)
                        {
                            if (documentId > 0)
                            {
                                var bills = this.accountingDocumentsService.GetPurchases((int)document.DOC_CODIGO);

                                foreach (var bill in bills)
                                {
                                    var updatePayBank = this.accountingDocumentsService.UpdatePayBankOracle(this.TreasuryDate, (int)bill.CODFACTURAGEI);

                                    var updateHistory = this.accountingDocumentsService.UpdateHistoryOracle(bill.NCERTIFICADO, $"Se ha pagado la factura con fecha {this.TreasuryDate.ToString("d", new CultureInfo("es-ES"))}");
                                }
                            }
                        }
                    }
                }
            }

            if (work)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "updateStatus", "updateStatus(0);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "updateStatus", "updateStatus(1);", true);
            }
        }

        private void FillDocuments(bool manual)
        {
            var documents = new List<PRE_DOCUMENTO_CONTABLE>();

            if (this.TreasuryId != 0)
            {
                var year = this.RmyYear.SelectedDate?.Year;
                var amount = this.RntTreasuryAmount.Value == null ? (decimal?)null : Convert.ToDecimal(this.RntTreasuryAmount.Value);
                var provider = this.RcProviders.SelectedValue.Equals("-1") ? (int?)null : Convert.ToInt32(this.RcProviders.SelectedValue);
                var checkNumber = string.IsNullOrWhiteSpace(this.RtbCheckNumber.Text) ? string.Empty : this.RtbCheckNumber.Text;
                var originCode = Convert.ToInt32(this.RcOriginCode.SelectedValue);

                var documentsResult = this.treasuriesService.GetDocumentsToBound(year, amount, provider, checkNumber, originCode);

                foreach (var document in documentsResult)
                {
                    documents.Add(document);
                }
            }

            this.RgDocuments.DataSource = documents;

            if (manual)
            {
                this.RgDocuments.DataBind();
            }

            var total = this.treasuriesService.GetAmount(this.TreasuryId);
            var totalDocuments = this.treasuriesService.GetDocumentsAmount(this.TreasuryId);

            this.txtTotal.Text = total.ToString("N");
            this.txtTotalDocuments.Text = totalDocuments.ToString("N");
        }

        #endregion
    }
}