namespace Dimatica.ContaPre.Presentation.Views.Spend
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Web.UI;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Telerik.Web.UI;

    #endregion

    public partial class SeePurchaseBills : BasePage
    {
        #region Fields

        private IAccountingDocumentsService accountingDocumentsService = DependencyFactory.GetInstance<IAccountingDocumentsService>();

        private IAccountingRecordsService accountingRecordsService = DependencyFactory.GetInstance<IAccountingRecordsService>();

        private IBillPurchasesService billPurchasesService = DependencyFactory.GetInstance<IBillPurchasesService>();

        private IBudgetApplicationsService budgetApplicationsService = DependencyFactory.GetInstance<IBudgetApplicationsService>();

        private IBudgetsService budgetsService = DependencyFactory.GetInstance<IBudgetsService>();

        private IExtraBudgetaryRecordsService extraBudgetaryRecordsService = DependencyFactory.GetInstance<IExtraBudgetaryRecordsService>();

        private IProvidersService providersService = DependencyFactory.GetInstance<IProvidersService>();

        #endregion

        #region Public Properties

        public int ParentId
        {
            get
            {
                var o = this.Session["_parentId"];

                if (o == null)
                {
                    o = -1;
                    this.Session["_parentId"] = o;
                }

                return Convert.ToInt32(o);
            }

            set
            {
                this.Session["_parentId"] = value;
            }
        }

        public int FileId
        {
            get
            {
                var o = this.Session["_fileId"];

                if (o == null)
                {
                    o = -1;
                    this.Session["_fileId"] = o;
                }

                return Convert.ToInt32(o);
            }

            set
            {
                this.Session["_fileId"] = value;
            }
        }

        public int AdministrativeId
        {
            get
            {
                var o = this.Session["_administrativeId"];

                if (o == null)
                {
                    o = 0;
                    this.Session["_administrativeId"] = o;
                }

                return Convert.ToInt32(o);
            }

            set
            {
                this.Session["_administrativeId"] = value;
            }
        }

        public string Description
        {
            get
            {
                var o = this.Session["_description"];

                if (o == null)
                {
                    o = string.Empty;
                    this.Session["_description"] = o;
                }

                return o.ToString();
            }

            set
            {
                this.Session["_description"] = value;
            }
        }

        public int BudgetYear
        {
            get
            {
                var o = this.Session["_budgetYear"];

                if (o == null)
                {
                    o = 0;
                    this.Session["_budgetYear"] = o;
                }

                return Convert.ToInt32(o);
            }

            set
            {
                this.Session["_budgetYear"] = value;
            }
        }

        public int YearNumber
        {
            get
            {
                var o = this.Session["_yearNumber"];

                if (o == null)
                {
                    o = 0;
                    this.Session["_yearNumber"] = o;
                }

                return Convert.ToInt32(o);
            }

            set
            {
                this.Session["_yearNumber"] = value;
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
                var fileId = this.Request.QueryString["fileId"];
                var administrativeId = this.Request.QueryString["administrativeId"];
                var description = this.Request.QueryString["description"];
                var budgetYear = this.Request.QueryString["budgetYear"];
                var yearNumber = this.Request.QueryString["yearNumber"];

                if (string.IsNullOrWhiteSpace(documentId) || string.IsNullOrWhiteSpace(fileId) || string.IsNullOrWhiteSpace(administrativeId) || string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(budgetYear) || string.IsNullOrWhiteSpace(yearNumber))
                {
                    this.Response.Redirect("~//Views//Spend//SpendRecords.aspx");
                }
                else
                {
                    this.ParentId = Convert.ToInt32(documentId);
                    this.FileId = Convert.ToInt32(fileId);
                    this.AdministrativeId = Convert.ToInt32(administrativeId);
                    this.Description = description;
                    this.BudgetYear = Convert.ToInt32(budgetYear);
                    this.YearNumber = Convert.ToInt32(yearNumber);

                    var totalDocument = this.accountingRecordsService.GetApplicationsAmount(this.ParentId);
                    var totalBills = this.accountingRecordsService.GetBillsAmount(this.ParentId);

                    this.TxtDescription.InnerText = this.Description;
                    this.TxtTotalDocument.InnerText = totalDocument.ToString("N");
                    this.TxtTotalBills.InnerText = totalBills.ToString("N");
                    this.TxtAccountingRecord.InnerText = this.YearNumber == -1 ? $"/{this.BudgetYear}" : $"{this.YearNumber}/{this.BudgetYear}";

                    if (totalDocument != totalBills)
                    {
                        this.TxtTotalBills.Style.Add("color", "red");
                    }
                }
            }
        }

        protected void RgPurchaseBills_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var result = new List<PRE_FACTURA_COMPRA>();

            if (this.FileId != -1 && this.ParentId != -1)
            {
                result = this.accountingRecordsService.GetPurchaseBills(this.FileId, this.ParentId);
            }

            this.RgPurchaseBills.DataSource = result;
        }

        protected void RgPurchaseBills_OnDeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item.OwnerTableView.Name == "ParentGrid")
            {
                var item = e.Item as GridDataItem;
                var documentId = (int)item.GetDataKeyValue("DOC_CODIGO");

                if (documentId != 0)
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(3);", true);
                }
                else
                {
                    var date = (DateTime)item.GetDataKeyValue("FA_FIRMA_RO");
                    var providerId = (int)item.GetDataKeyValue("PROV_COD_PROVEEDOR");

                    var result = this.accountingRecordsService.GetProvidersPurchaseBills(this.FileId, date.ToString("d", new CultureInfo("es-Es")), providerId);

                    if (!result.Any())
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(4);", true);
                    }
                    else
                    {
                        var hasDocument = false;
                        foreach (var bill in result)
                        {
                            if (bill.DOC_CODIGO != 0)
                            {
                                hasDocument = true;

                                continue;
                            }


                            var id = bill.FA_CODIGO;

                            var delete = this.billPurchasesService.DeleteBillPurchase(id);

                            if (delete.ResponseCode != ResponseCode.Ok)
                            {
                                System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(5);", true);
                            }
                            else
                            {
                                // TODO: aqui se llamaba a oracle
                                //expediente.actualizaFechaTrasladoGEI("", codFacturaGEI, bModificado)
                                //expediente.actualizaImportadaContabl("N", codFacturaGEI, bModificado)
                            }
                        }

                        if (hasDocument)
                        {
                            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(6);", true);
                        }
                    }
                }
            }

            if (e.Item.OwnerTableView.Name == "BillsGrid")
            {
                var item = e.Item as GridDataItem;
                var documentId = (int)item.GetDataKeyValue("DOC_CODIGO");

                if (documentId != 0)
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(0);", true);
                }
                else
                {
                    var id = (int)item.GetDataKeyValue("FA_CODIGO");

                    var delete = this.billPurchasesService.DeleteBillPurchase(id);

                    switch (delete.ResponseCode)
                    {
                        case ResponseCode.Invalid:
                            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(1);", true);

                            break;
                        case ResponseCode.Found:
                            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(0);", true);

                            break;
                        case ResponseCode.NotFound:
                            System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(2);", true);

                            break;
                    }

                    if (delete.ResponseCode == ResponseCode.Ok)
                    {
                        // TODO: aqui se llamaba a oracle
                        //expediente.actualizaFechaTrasladoGEI("", codFacturaGEI, bModificado)
                        //expediente.actualizaImportadaContabl("N", codFacturaGEI, bModificado)
                    }
                }
            }
        }

        protected void RgPurchaseBills_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "UpdatePurchaseBill")
            {
                var item = e.Item as GridDataItem;
                var documentId = (int)item.GetDataKeyValue("DOC_CODIGO");

                if (documentId == 0)
                {
                    var date = (DateTime)item.GetDataKeyValue("FA_FIRMA_RO");
                    var providerId = (int)item.GetDataKeyValue("PROV_COD_PROVEEDOR");

                    var update = this.billPurchasesService.UpdateDocument(this.FileId, date.ToString("d", new CultureInfo("es-ES")), providerId, this.ParentId, LoginUser.USU_CODIGO);

                    if (update.ResponseCode == ResponseCode.Ok)
                    {
                        var bills = this.billPurchasesService.GetByDocument(this.ParentId);

                        foreach (var bill in bills)
                        {
                            var applications = this.accountingDocumentsService.GetApplicationsByDocumentCacs(this.ParentId, bill.APP_PRESUP);

                            var cacsCode = string.Empty;

                            if (!applications.Any())
                            {
                                var chapters = this.budgetApplicationsService.GetChaptersNumbersByTypeByYear("G", this.BudgetYear);

                                foreach (var chapter in chapters)
                                {
                                    if (!chapter.CacsNumber.Equals(bill.APP_PRESUP))
                                    {
                                        continue;
                                    }

                                    cacsCode = chapter.CacsCode;
                                    break;
                                }

                                if (!string.IsNullOrWhiteSpace(cacsCode))
                                {
                                    var application = new PRE_DOCUMENTO_APLICACION
                                    {
                                        CACS_CODIGO = cacsCode,
                                        DOCA_IMPORTE = bill.FA_IMPORTE_INTEGRO,
                                        DOCA_ANO_PRESUPUESTO = Convert.ToInt16(this.BudgetYear),
                                        DOCA_I_G = "G",
                                        DOC_CODIGO = this.ParentId,
                                        USU_CODIGO = LoginUser.USU_CODIGO
                                    };

                                    var insertApplication = this.accountingDocumentsService.InsertApplication(application);
                                }
                            }
                            else
                            {
                                if (bill.FA_IMPORTE_INTEGRO != 0)
                                {
                                    cacsCode = applications.FirstOrDefault().CACS_CODIGO;

                                    var application = new PRE_DOCUMENTO_APLICACION
                                    {
                                        DOCA_CODIGO = applications.FirstOrDefault().DOCA_CODIGO,
                                        CACS_CODIGO = cacsCode,
                                        DOCA_IMPORTE = bill.FA_IMPORTE_INTEGRO,
                                        DOCA_ANO_PRESUPUESTO = Convert.ToInt16(this.BudgetYear),
                                        DOCA_I_G = "G",
                                        USU_CODIGO = LoginUser.USU_CODIGO
                                    };
                                    var updateApplication = this.accountingDocumentsService.UpdateApplication(application);
                                }
                                else
                                {
                                    var deleteApplication = this.accountingDocumentsService.DeleteApplication(applications.FirstOrDefault().DOCA_CODIGO, LoginUser.USU_CODIGO);
                                }
                            }

                            if (this.ExceedRcPhase())
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(8);", true);
                            }
                            else
                            {
                                if (this.ExceedAdPhase())
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(9);", true);
                                }
                                else
                                {
                                    var applicationAmount = this.budgetsService.CheckApplicationAmount(cacsCode, this.BudgetYear, "G");

                                    if (applicationAmount.ResponseCode == ResponseCode.Ok)
                                    {
                                        var articleBudget = ((Tuple<decimal, decimal>)applicationAmount.ResponseMethod).Item1;
                                        var articleCredit = ((Tuple<decimal, decimal>)applicationAmount.ResponseMethod).Item2;
                                        var articleDiff = articleCredit - articleBudget;

                                        if (articleDiff > 0)
                                        {
                                            var cacsCodeSplit = cacsCode.Split('/');

                                            if (cacsCodeSplit.Length >= 2)
                                            {
                                                var articleCode = $"{cacsCodeSplit[0]}/{cacsCodeSplit[1]}//";
                                                var articleInfo = this.budgetApplicationsService.GetApplicationInfo(articleCode, this.BudgetYear, "G");

                                                if (articleInfo.ResponseCode == ResponseCode.Ok)
                                                {
                                                    var articleNumber = ((Tuple<string, string, decimal, decimal, decimal>)articleInfo.ResponseMethod).Item1;
                                                    var articleName = ((Tuple<string, string, decimal, decimal, decimal>)articleInfo.ResponseMethod).Item2;

                                                    var chapterApplicationAmount = this.budgetsService.CheckChapterApplicationAmount(cacsCode, this.BudgetYear, "G");

                                                    var chapterCode = $"{cacsCodeSplit[0]}///";
                                                    decimal chapterBudget = 0;
                                                    decimal chapterCredit = 0;
                                                    decimal chapterDiff = 0;
                                                    var chapterNumber = string.Empty;
                                                    var chapterName = string.Empty;
                                                    var hasChapter = 0;

                                                    if (chapterApplicationAmount.ResponseCode == ResponseCode.Ok)
                                                    {
                                                        chapterBudget = ((Tuple<decimal, decimal>)chapterApplicationAmount.ResponseMethod).Item1;
                                                        chapterCredit = ((Tuple<decimal, decimal>)chapterApplicationAmount.ResponseMethod).Item2;
                                                        chapterDiff = chapterCredit - chapterBudget;

                                                        var chapterInfo = this.budgetApplicationsService.GetApplicationInfo(chapterCode, this.BudgetYear, "G");

                                                        if (chapterInfo.ResponseCode == ResponseCode.Ok)
                                                        {
                                                            chapterNumber = ((Tuple<string, string, decimal, decimal, decimal>)chapterInfo.ResponseMethod).Item1;
                                                            chapterName = ((Tuple<string, string, decimal, decimal, decimal>)chapterInfo.ResponseMethod).Item2;
                                                            hasChapter = 1;
                                                        }
                                                    }

                                                    var script = $"showBigError('{cacsCode}', '{articleNumber}', '{articleName}', '{this.BudgetYear}', '{articleBudget:N}', '{articleCredit:N}', '{articleDiff:N}', {hasChapter}, '{chapterNumber}', '{chapterName}', '{chapterBudget:N}', '{chapterCredit:N}', '{chapterDiff:N}');";
                                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showBigError", script, true);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        this.UpdateDcDescription();

                        var retentionAmount = this.billPurchasesService.GetDocumentRetentionAmount(this.ParentId);

                        if (retentionAmount != 0)
                        {
                            var accountingDocument = this.accountingDocumentsService.GetById(this.ParentId);
                            var accountingDocumentDate = accountingDocument.DOC_FECHA_ASIENTO_DIARIO;

                            var accountingRecord = this.accountingRecordsService.GetById(this.FileId);
                            var yearNumber = accountingRecord.EXP_NUM_EXP_CONTABLE_ANUAL;

                            var provider = this.accountingDocumentsService.GetProviderDc(this.ParentId);

                            var extraBudgetaries = this.extraBudgetaryRecordsService.GetByDocumentId(this.ParentId, 1);

                            if (!extraBudgetaries.Any())
                            {
                                var extraBudgetary = new PRE_EXP_EXTRAPRE
                                {
                                    EXP_CODIGO = this.FileId,
                                    EXTRAPRE_CODIGO = 35,
                                    EXP_EXTRAP_ANO_PRESUPUESTO = Convert.ToInt16(this.BudgetYear),
                                    TIP_EXTRAP_CODIGO = 1,
                                    DOC_CODIGO = this.ParentId,
                                    EXP_EXTRAP_FECHA = accountingDocumentDate,
                                    EXP_EXTRAP_IMPORTE = retentionAmount,
                                    PROV_CODIGO_PROVEEDOR = provider == 0 ? (int?)null : provider,
                                    EXP_EXTRAP_NUMERO = 0,
                                    EXP_ENLAZADO_TESORERIA = false,
                                    EXP_NUM_EXP_CONTABLE_ANUAL = yearNumber,
                                    USU_CODIGO = LoginUser.USU_CODIGO
                                };

                                var insert = this.extraBudgetaryRecordsService.InsertExtraBudgetary(extraBudgetary);
                            }
                            else
                            {
                                foreach (var extraBudgetary in extraBudgetaries)
                                {
                                    if (extraBudgetary.EXTRAPRE_CODIGO != 35)
                                    {
                                        continue;
                                    }

                                    var extraBudgetaryToUpdate = new PRE_EXP_EXTRAPRE
                                    {
                                        EXP_EXTRAP_CODIGO = extraBudgetary.EXP_EXTRAP_CODIGO,
                                        EXP_CODIGO = this.FileId,
                                        EXTRAPRE_CODIGO = 35,
                                        EXP_EXTRAP_FECHA = accountingDocumentDate,
                                        EXP_EXTRAP_IMPORTE = retentionAmount,
                                        USU_CODIGO = LoginUser.USU_CODIGO
                                    };

                                    var updateExtraBudgetary = this.extraBudgetaryRecordsService.UpdateExtraBudgetary(extraBudgetaryToUpdate);
                                }
                            }
                        }

                        var boeAmount = this.billPurchasesService.GetDocumentBoeAmount(this.ParentId);

                        if (boeAmount != 0)
                        {
                            var incomeDiscounts = this.accountingDocumentsService.GetIncomeDiscountsCount(this.ParentId);

                            if (incomeDiscounts == 0)
                            {
                                var accountingRecord = this.accountingRecordsService.GetById(this.FileId);
                                var provenanceCode = accountingRecord.CEN_CODIGO == null ? 0 : accountingRecord.CEN_CODIGO;
                                var provider = this.accountingDocumentsService.GetProviderDc(this.ParentId);
                                var description = $"DTO. ING.(EXP: {this.FileId}/{this.BudgetYear})";

                                var accountingRecordToInsert = new PRE_EXPEDIENTE_CONTABLE
                                {
                                    EXP_I_G = "I",
                                    EXP_DESCRIPCION = description,
                                    EXP_PLURIANUAL = false,
                                    EXP_CUADRADO = false,
                                    EXP_ANO_PRESUPUESTO = Convert.ToInt16(this.BudgetYear),
                                    PROV_CODIGO = provider == 0 ? (int?)null : provider,
                                    PROC_CODIGO = 20,
                                    CEN_CODIGO = provenanceCode,
                                    DOC_CODIGO_G_DESCUENTO_I = this.ParentId,
                                    USU_CODIGO = LoginUser.USU_CODIGO
                                };

                                var insertAccountingRecord = this.accountingRecordsService.InsertAccountingRecord(accountingRecordToInsert);

                                if (insertAccountingRecord.ResponseCode == ResponseCode.Ok)
                                {
                                    var accountingRecordId = (int)insertAccountingRecord.ResponseMethod;
                                    var documentTypeCode = 41;
                                    var restrictedAccountCode = 10;
                                    var accountingDocument = this.accountingDocumentsService.GetById(this.ParentId);
                                    var operationIncome = (DateTime)accountingDocument.DOC_FECHA_PROPUESTA;
                                    var proposalDate = operationIncome.AddDays(1);
                                    var effectiveDate = operationIncome.AddDays(2);

                                    if (provider != 0)
                                    {
                                        var realProvider = this.providersService.GetById(provider);

                                        if (realProvider != null)
                                        {
                                            description = realProvider.PROV_NOMBRE;
                                        }
                                        else
                                        {
                                            description = string.Empty;
                                        }
                                    }
                                    else
                                    {
                                        description = string.Empty;
                                    }

                                    var accountingDocumentToInsert = new PRE_DOCUMENTO_CONTABLE
                                    {
                                        DOC_I_G = "I",
                                        DOC_FECHA_PROPUESTA = proposalDate,
                                        DOC_FECHA_ASIENTO_DIARIO = effectiveDate,
                                        DOC_DESCRIPCION = description,
                                        DOC_NUMERO_MOVIMIENTO_I = 0,
                                        DOC_FECHA_MOVIMIENTO_I = operationIncome,
                                        EXP_CODIGO = accountingRecordId,
                                        TIPD_CODIGO = documentTypeCode,
                                        CUE_CODIGO = restrictedAccountCode,
                                        DOC_ENLAZADO_TESORERIA = false,
                                        DOC_CODIGO_G_DESCUENTO_I = this.ParentId,
                                        USU_CODIGO = LoginUser.USU_CODIGO
                                    };

                                    var insertAccountingDocument = this.accountingDocumentsService.InsertAccountingDocument(accountingDocumentToInsert);

                                    if (insertAccountingDocument.ResponseCode == ResponseCode.Ok)
                                    {
                                        var accountingDocumentId = (int)insertAccountingDocument.ResponseMethod;

                                        var applications = this.budgetApplicationsService.GetChaptersNumbersByTypeByYear("I", this.BudgetYear);
                                        var cacsCode = string.Empty;

                                        foreach (var application in applications)
                                        {
                                            if (!application.CacsNumber.Equals("399.99"))
                                            {
                                                continue;
                                            }

                                            cacsCode = application.CacsCode;
                                            break;
                                        }

                                        if (!string.IsNullOrWhiteSpace(cacsCode) && boeAmount > 0)
                                        {
                                            var applicationDocument = new PRE_DOCUMENTO_APLICACION
                                            {
                                                CACS_CODIGO = cacsCode,
                                                DOCA_IMPORTE = boeAmount,
                                                DOCA_ANO_PRESUPUESTO = Convert.ToInt16(this.BudgetYear),
                                                DOCA_I_G = "I",
                                                DOC_CODIGO = accountingDocumentId,
                                                USU_CODIGO = LoginUser.USU_CODIGO
                                            };

                                            var insertApplication = this.accountingDocumentsService.InsertApplication(applicationDocument);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                int? codeFileIncome = null;
                                var accountingRecord = this.accountingDocumentsService.GetAccountingRecord(this.ParentId);

                                if (accountingRecord != null)
                                {
                                    if (accountingRecord.PROC_CODIGO == 20)
                                    {
                                        codeFileIncome = accountingRecord.EXP_CODIGO;
                                    }
                                }

                                var documents = this.accountingDocumentsService.GetByIdAndType("I", codeFileIncome);

                                if (documents.Any())
                                {
                                    int? codeDocumentIncome = documents.FirstOrDefault().DOC_CODIGO;

                                    var applications = this.accountingDocumentsService.GetApplicationsByDocumentId((int)codeDocumentIncome);

                                    if (applications.Any())
                                    {
                                        var applicationCode = applications.FirstOrDefault().DOCA_CODIGO;
                                        var cacsCode = applications.FirstOrDefault().CACS_CODIGO;

                                        var budgetApplications = this.budgetApplicationsService.GetNumbersByTypeByYear("I", this.BudgetYear);

                                        foreach (var application in budgetApplications)
                                        {
                                            if (!application.CacsNumber.Equals("399.99"))
                                            {
                                                continue;
                                            }

                                            cacsCode = application.CacsCode;
                                            break;
                                        }

                                        if (boeAmount > 0)
                                        {
                                            var documentApplication = new PRE_DOCUMENTO_APLICACION
                                                                              {
                                                                                      DOCA_CODIGO = applicationCode,
                                                                                      CACS_CODIGO = cacsCode,
                                                                                      DOCA_IMPORTE = boeAmount,
                                                                                      DOCA_ANO_PRESUPUESTO = Convert.ToByte(this.BudgetYear),
                                                                                      DOCA_I_G = "I",
                                                                                      USU_CODIGO = LoginUser.USU_CODIGO
                                                                              };

                                            var updateApplication = this.accountingDocumentsService.UpdateApplication(documentApplication);
                                        }
                                        else
                                        {
                                            var deleteApplication = this.accountingDocumentsService.DeleteApplication(applicationCode, LoginUser.USU_CODIGO);
                                            var deleteDocument = this.accountingDocumentsService.DeleteAccountingDocument((int)codeDocumentIncome, LoginUser.USU_CODIGO);
                                            var deleteFile = this.accountingRecordsService.DeleteAccountingRecord((int)codeFileIncome, LoginUser.USU_CODIGO);
                                        }
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(10);", true);
                                }
                            }
                        }

                        var billPurchases = this.accountingRecordsService.GetProvidersPurchaseBills(this.FileId, date.ToString("d", new CultureInfo("es-ES")), providerId);

                        foreach (var bill in billPurchases)
                        {
                            // TODO: aqui se llamaba a oracle
                            //var updateOracle = expediente.actualizaFechaContableGEI(Date.Today.ToShortDateString, CType(dt.Rows(i).Item("codFacturaGEI"), Integer), bSeActualizo)

                            //if (updateOracle)
                            //{
                            //    expediente.actualizaHistoricoRegFra(dt.Rows(i).Item("ncertificado"), "La factura ha sido contabilizada.")
                            //}
                        }


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showError", "showError(7);", true);
                    }

                }
                else
                {
                    
                }
            }
        }


        protected void RgPurchaseBills_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "ParentGrid")
            {
                var dataBoundItem = e.Item as GridDataItem;
                var documentId = (int)dataBoundItem.GetDataKeyValue("DOC_CODIGO");

                dataBoundItem["DeleteColumn"].Visible = documentId == 0;

                dataBoundItem["EditColumn"].CssClass = documentId == 0 ? "fas fa-trash-restore-alt" : "fas fa-trash-alt";

                dataBoundItem["EditColumn"].ToolTip = documentId == 0 ? "Agregar" : "Eliminar";
            }
        }

        protected void RgPurchaseBills_OnDetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            var item = (GridDataItem)e.DetailTableView.ParentItem;
            var date = (DateTime)item.GetDataKeyValue("FA_FIRMA_RO");
            var providerId = (int)item.GetDataKeyValue("PROV_COD_PROVEEDOR");

            var result = this.accountingRecordsService.GetProvidersPurchaseBills(this.FileId, date.ToString("d", new CultureInfo("es-Es")), providerId);

            e.DetailTableView.DataSource = result;
        }

        #endregion

        private bool ExceedRcPhase()
        {
            var result = false;

            var rcPositiveAmount = this.accountingRecordsService.GetRcAmount(this.FileId, true, null);
            var rcNegativeAmount = this.accountingRecordsService.GetRcAmount(this.FileId, false, null);

            var adPositiveAmount = this.accountingRecordsService.GetAdAmount(this.FileId, true, null);
            var adNegativeAmount = this.accountingRecordsService.GetAdAmount(this.FileId, false, null);

            var oPositiveAmount = this.accountingRecordsService.GetOAmount(this.FileId, true, null);
            var oNegativeAmount = this.accountingRecordsService.GetOAmount(this.FileId, false, null);

            var pPositiveAmount = this.accountingRecordsService.GetPAmount(this.FileId, true, null);
            var pNegativeAmount = this.accountingRecordsService.GetPAmount(this.FileId, false, null);

            if (rcPositiveAmount - rcNegativeAmount == adPositiveAmount - adNegativeAmount)
            {
                if (adPositiveAmount - rcNegativeAmount == oPositiveAmount - oNegativeAmount)
                {
                    if (oPositiveAmount - oNegativeAmount == pPositiveAmount - pNegativeAmount)
                    {
                        var update = this.accountingRecordsService.UpdateAccountingRecordSquare(this.FileId, true, LoginUser.USU_CODIGO);
                    }
                    else
                    {
                        var update = this.accountingRecordsService.UpdateAccountingRecordSquare(this.FileId, false, LoginUser.USU_CODIGO);
                    }
                }
                else
                {
                    var update = this.accountingRecordsService.UpdateAccountingRecordSquare(this.FileId, false, LoginUser.USU_CODIGO);
                }
            }
            else
            {
                var update = this.accountingRecordsService.UpdateAccountingRecordSquare(this.FileId, false, LoginUser.USU_CODIGO);
            }

            if (rcPositiveAmount - rcNegativeAmount < adPositiveAmount - adNegativeAmount || rcPositiveAmount - rcNegativeAmount < oPositiveAmount - oNegativeAmount || rcPositiveAmount - rcNegativeAmount < pPositiveAmount - pNegativeAmount)
            {
                result = true;
            }

            return result;
        }

        private bool ExceedAdPhase()
        {
            var result = false;

            var adPositiveAmount = this.accountingRecordsService.GetAdAmount(this.FileId, true, null);
            var adNegativeAmount = this.accountingRecordsService.GetAdAmount(this.FileId, false, null);

            var oPositiveAmount = this.accountingRecordsService.GetOAmount(this.FileId, true, null);
            var oNegativeAmount = this.accountingRecordsService.GetOAmount(this.FileId, false, null);

            var pPositiveAmount = this.accountingRecordsService.GetPAmount(this.FileId, true, null);
            var pNegativeAmount = this.accountingRecordsService.GetPAmount(this.FileId, false, null);

            if (adPositiveAmount - adNegativeAmount < oPositiveAmount - oNegativeAmount || adPositiveAmount - adNegativeAmount < pPositiveAmount - pNegativeAmount)
            {
                result = true;
            }

            return result;
        }


        private void UpdateDcDescription()
        {
            var purchases = this.accountingDocumentsService.GetPurchases(this.ParentId);
            var hasPurchases = false;
            var strPurchases = string.Empty;

            if (purchases.Any())
            {
                hasPurchases = true;
                strPurchases = "FACT Nro ";
            }

            foreach (var purchase in purchases)
            {
                var strAux = $"{strPurchases}{purchase.FA_NUM_FACTURA}";

                strPurchases = strAux.Length <= 75 ? strAux : "Diversas facturas. ";
            }

            if (hasPurchases)
            {
                if (!strPurchases.Equals("Diversas facturas. "))
                {
                    strPurchases = $"{strPurchases.Substring(0, strPurchases.Length - 2)}. ";
                }
            }

            var strDescription = strPurchases;

            var accountingRecord = this.accountingRecordsService.GetById(this.FileId);

            strDescription = hasPurchases ? $"{strDescription}{accountingRecord.EXP_DESCRIPCION}" : accountingRecord.EXP_DESCRIPCION;

            if (strPurchases.Length > 75)
            {
                strPurchases = strPurchases.Substring(0, 74);
            }

            if (strDescription.Length > 255)
            {
                strDescription = strDescription.Substring(0, 254);
            }

            var document = new PRE_DOCUMENTO_CONTABLE
            {
                DOC_CODIGO = this.ParentId,
                DOC_DESCRIPCION = strDescription,
                DOC_FACTURA = strPurchases,
                USU_CODIGO = LoginUser.USU_CODIGO
            };

            var updateDescription = this.accountingDocumentsService.UpdateDcDescription(document);
        }

    }
}