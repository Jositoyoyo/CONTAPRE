namespace Dimatica.ContaPre.Presentation.Views.ReportViewer
{
    #region NameSpaces

    using System;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Web;

    using Dimatica.ContaPre.BLL.Configs;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.Presentation.DataSets;
    using Dimatica.ContaPre.Presentation.Views.Shared;

    using Humanizer;

    using Microsoft.Reporting.WebForms;

    #endregion

    public partial class CustomReportViewer : BasePage
    {
        #region Fields

        IAccountingDocumentsService accountingDocumentsService = DependencyFactory.GetInstance<IAccountingDocumentsService>();

        IAccountingRecordsService accountingRecordsService = DependencyFactory.GetInstance<IAccountingRecordsService>();

        IAccountRestrictedService accountRestrictedService = DependencyFactory.GetInstance<IAccountRestrictedService>();

        IApplicationService applicationService = DependencyFactory.GetInstance<IApplicationService>();

        IBillPurchasesService billPurchasesService = DependencyFactory.GetInstance<IBillPurchasesService>();

        IBudgetsService budgetsService = DependencyFactory.GetInstance<IBudgetsService>();

        ICreditModificationBudgetsService creditModificationBudgetsService = DependencyFactory.GetInstance<ICreditModificationBudgetsService>();

        ICreditModificationTypesService creditModificationTypesService = DependencyFactory.GetInstance<ICreditModificationTypesService>();

        IDocumentTypesService documentTypesService = DependencyFactory.GetInstance<IDocumentTypesService>();

        private IExtraBudgetaryRecordsService extraBudgetaryRecordsService = DependencyFactory.GetInstance<IExtraBudgetaryRecordsService>();

        IExtraBudgetaryRecordsService extraBudgetaryService = DependencyFactory.GetInstance<IExtraBudgetaryRecordsService>();

        IExtraBudgetaryApplicationsService extraBudgetaryApplicationsService = DependencyFactory.GetInstance<IExtraBudgetaryApplicationsService>();

        IPayFormsService payFormsService = DependencyFactory.GetInstance<IPayFormsService>();

        IPayTypesService payTypesService = DependencyFactory.GetInstance<IPayTypesService>();

        IProgramsService programsService = DependencyFactory.GetInstance<IProgramsService>();

        IProvidersService providersService = DependencyFactory.GetInstance<IProvidersService>();

        IProvenancesService provenancesService = DependencyFactory.GetInstance<IProvenancesService>();

        IRectificationsService rectificationsService = DependencyFactory.GetInstance<IRectificationsService>();

        ISingsService singsService = DependencyFactory.GetInstance<ISingsService>();

        ITreasuryLinesService treasuryLinesService = DependencyFactory.GetInstance<ITreasuryLinesService>();

        ITreasuriesService treasuriesService = DependencyFactory.GetInstance<ITreasuriesService>();

        #endregion

        #region Private Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            var reportType = this.Request.QueryString["report"];

            if (!this.IsPostBack)
            {
                var mesagge = "Reportes";

                switch (reportType)
                {
                    case "spendLevelCompliance":
                        mesagge = "Grado de Cumplimiento de Gastos";
                        this.SpendLevelCompliance();

                        break;
                    case "spendsRecordsList":
                        mesagge = "Listado de Expedientes";
                        this.SpendRecordsList();

                        break;
                    case "spendBillPaymentList":
                        mesagge = "Listado Facturas Compras";
                        this.SpendBillPaymentList();

                        break;
                    case "spendApplicationAmount":
                        mesagge = "Listado Aplicación Importe";
                        this.SpendApplicationAmount();

                        break;
                    case "spendCertificate":
                        mesagge = "Certificado";
                        this.SpendCertificate();

                        break;
                    case "spendCreditModification":
                        mesagge = "Modificación de Crédito de Gastos";
                        this.SpendCreditModification();

                        break;
                    #region SpendP
                    case "spendP":
                        mesagge = "Reconocimiento Obligaciones";
                        this.SpendP();

                        break;
                    #endregion
                    #region SpendAnnexP
                    case "spendAnnexP":
                        mesagge = "Reconocimiento Obligaciones";
                        this.SpendAnnexP();

                        break;
                    #endregion
                    #region spendRC
                    case "spendRC":
                        mesagge = "Retención de crédito";
                        this.SpendRC();

                        break;
                    #endregion
                    #region spendAnnexRc
                    case "spendAnnexRc":
                        mesagge = "Retención de crédito - Anexo";
                        this.SpendAnnexRc();

                        break;
                    #endregion
                    #region spendAD
                    case "spendAD":
                        mesagge = "Autorización y compromiso sobre crédito retenido";
                        this.SpendAD();

                        break;
                    #endregion
                    #region spendO
                    case "spendO":
                        mesagge = "Reconocimiento Obligaciones";
                        this.SpendO();

                        break;
                    #endregion
                    #region spendORecord
                    case "spendORecord":
                        mesagge = "Reconocimiento Obligaciones";
                        this.SpendORecord();

                        break;
                    #endregion
                    #region spendPRecord
                    case "spendPRecord":
                        mesagge = "Reconocimiento Obligaciones";
                        this.SpendPRecord();

                        break;
                    #endregion
                    #region spendPRecordDiscounts
                    case "spendPRecordDiscounts":
                        mesagge = "Reconocimiento Obligaciones";
                        this.SpendPRecordDiscounts();

                        break;
                    #endregion
                    #region spendPRecord
                    case "spendIncomeDiscounts":
                        mesagge = "Reconocimiento Obligaciones";
                        this.SpendIncomeDiscounts();

                        break;
                    #endregion
                    case "incomeLevelCompliance":
                        mesagge = "Grado de Cumplimiento de Ingresos";
                        this.IncomeLevelCompliance();

                        break;
                    case "incomeCreditModification":
                        mesagge = "Modificación de Crédito de Ingresos";
                        this.IncomeCreditModification();

                        break;
                    case "incomeDr":
                        mesagge = "Resumen Contable";
                        this.IncomeDr();

                        break;
                    case "incomeMi":
                        mesagge = "Documento Contable";
                        this.IncomeMi();

                        break;
                    case "incomePmp":
                        mesagge = "Documento Contable";
                        this.IncomePmp();

                        break;
                    case "incomeAnnexedDr":
                        mesagge = "Anexo de DR";
                        this.IncomeAnnexedDr();

                        break;
                    case "incomeFiles":
                        mesagge = "Expedientes de Ingresos";
                        this.IncomeFiles();

                        break;
                    case "incomeStatementAccounts":
                        mesagge = "Estado de Cuenta Restringida";
                        this.IncomeStatementAccounts();

                        break;
                    case "tonnageSheetDetail":
                        mesagge = "Hoja de Arqueo";
                        this.TonnageSheetDetail();

                        break;
                    case "treasuryNotesTreasuries":
                        mesagge = "Apuntes de Tesorería";
                        this.TreasuryNotesTreasuries();

                        break;
                    case "extraBudgetariesList":
                        mesagge = "Listado de Expedientes Extrapresupuestarias";
                        this.ExtraBudgetariesList();

                        break;
                    case "extraBudgetaryCurrent":
                        mesagge = "Expediente Extrapresupuestario";
                        this.ExtraBudgetaryCurrent();

                        break;
                    case "pointingList":
                        mesagge = "Listado de Señalamientos";
                        this.PointingListReport();

                        break;
                    case "pointingPendingDocs":
                        mesagge = "Listado de Documentos no incluidos";
                        this.PointingPendingDocs();

                        break;
                    case "pointingCurrentReport":
                        mesagge = "Señalamiento Actual";
                        this.PointingCurrentReport();

                        break;
                    case "listSpendProvisionalStatus":
                        mesagge = "Estado Provisional del Ejercicio de Gastos";
                        this.ListSpendProvisionalStatus();

                        break;
                    case "listSpendsByConcept":
                        mesagge = "Estado del Ejercicio por Concepto de Gastos";
                        this.ListSpendsByConcept();

                        break;
                    case "listSpendsByPlace":
                        mesagge = "Gastos efectuados";
                        this.ListSpendsByPlace();

                        break;
                    case "listSpendsComplianceGrade":
                        mesagge = "Grado de Cumplimiento del Presupuesto de Gastos";
                        this.ListSpendComplianceGrade();

                        break;
                    case "listSpendsProvidersByYear":
                        mesagge = "Listado de Proveedores";
                        this.ListSpendProvidersByYear();

                        break;
                    case "listIncomeProvisionalStatus":
                        mesagge = "Estado Provisional del Ejercicio de Ingresos";
                        this.ListIncomeProvisionalStatus();

                        break;
                    case "listIncomeByConcept":
                        mesagge = "Estado del Ejercicio por Concepto de Ingresos";
                        this.ListIncomesByConcept();

                        break;
                    case "listIncomeByConceptDrMi":
                        mesagge = "Situación por Concepto DR-MI";
                        this.ListIncomesByConceptDrMi();

                        break;
                    case "listIncomeByPlace":
                        mesagge = "Ingresos reales efectuados";
                        this.ListIncomesByPlace();

                        break;
                    case "listIncomeRightsRecognized":
                        mesagge = "Derechos Reconocidos pendientes de ingresar";
                        this.ListIncomeRightsRecognized();

                        break;
                    case "listIncomeDrAgreement":
                        mesagge = "Reconocimiento de derechos por convenio";
                        this.ListIncomesDrAgreement();

                        break;
                    case "listExtraBudgetary":
                        mesagge = "Estado de Cuentas Extrapresupuestarias";
                        this.ListExtraBudgetary();

                        break;
                    case "listTreasuryAccountingBook":
                        mesagge = "Libro de Banco de España";
                        this.ListTreasuryAccountingBook();

                        break;
                    case "listTreasuryBankStatement":
                        mesagge = "Libro de Extracto de Banco de España";
                        this.ListTreasuryBankStatement();

                        break;
                    case "listTreasuryBlockListing":
                        mesagge = "Libro de Cuadre";
                        this.ListTreasuryBlockListing();

                        break;
                    case "listTreasuryPaymentRecord":
                        mesagge = "Registro de Pagos de Contabilidad";
                        this.ListTreasuryPaymentRecord();

                        break;
                    case "maintenanceApplications":
                        mesagge = "Aplicaciones del Presupuesto";
                        this.MaintenanceApplications();

                        break;
                    case "maintenanceAccountsRestricted":
                        mesagge = "Cuentas Restringidas";
                        this.MaintenanceAccountsRestricted();

                        break;
                    case "maintenanceExtraBudgetaryApplications":
                        mesagge = "Aplicaciones Extrapresupuestarias";
                        this.MaintenanceExtraBudgetaryApplications();

                        break;
                    case "maintenancePayForms":
                        mesagge = "Formas de Pago";
                        this.MaintenancePayForms();

                        break;
                    case "maintenanceTreasuryLines":
                        mesagge = "Líneas de Tesorería";
                        this.MaintenanceTreasuryLines();

                        break;
                    case "maintenancePrograms":
                        mesagge = "Programas";
                        this.MaintenancePrograms();

                        break;
                    case "maintenanceProvenances":
                        mesagge = "Procedencias";
                        this.MaintenanceProvenances();

                        break;
                    case "maintenanceProviders":
                        mesagge = "Proveedores";
                        this.MaintenanceProviders();

                        break;
                    case "maintenanceDocumentTypes":
                        mesagge = "Tipos de Documentos";
                        this.MaintenanceDocumentTypes();

                        break;
                    case "maintenanceCreditModificationTypes":
                        mesagge = "Tipos de Modificación de Crédito";
                        this.MaintenanceCreditModificationTypes();

                        break;
                    case "maintenancePayTypes":
                        mesagge = "Tipos de Pago";
                        this.MaintenancePayTypes();

                        break;
                    case "rectificationsList":
                        mesagge = "Rectificaciones Aplicación";
                        this.RectificationsList();

                        break;
                }

                this.titleHeader.InnerText = mesagge;
            }
        }

        private void IncomeLevelCompliance()
        {
            var year = this.Request.QueryString["year"];
            var yearValue = DateTime.Now.Year;

            if (!string.IsNullOrWhiteSpace(year))
            {
                yearValue = Convert.ToInt32(year);
            }

            var dataSet = new ReportsDataSet();

            var budgets = this.budgetsService.GetIncomeLevelCompliance(yearValue);
            decimal totalAmount = 0;

            foreach (var b in budgets)
            {
                if (b.Level.Equals("CAP"))
                {
                    totalAmount += b.Amount;
                }
            }

            foreach (var b in budgets)
            {
                if (b.Amount == 0)
                {
                    continue;
                }

                var budget = dataSet.IncomeLevelCompliance.NewIncomeLevelComplianceRow();

                budget.Year = b.Year;
                budget.Level = b.Level;
                budget.ChapterId = b.ChapterId == null ? string.Empty : b.ChapterId.ToString();
                budget.ArticleId = b.ArticleId == null ? string.Empty : b.ArticleId.ToString();
                budget.ConceptId = b.ConceptId == null ? string.Empty : b.ConceptId.ToString();
                budget.SubConceptId = b.SubConceptId == null ? string.Empty : b.SubConceptId.ToString();
                budget.ApplicationNumber = b.ApplicationNumber;
                budget.Description = b.Description;
                budget.Amount = b.Amount;
                budget.DrAmount = b.DrAmount;
                budget.MiAmount = b.MiAmount;
                budget.Pending = b.Pending;
                budget.TotalAmount = totalAmount;

                dataSet.IncomeLevelCompliance.AddIncomeLevelComplianceRow(budget);
            }

            var rpt = new ReportDataSource("IncomeLevelCompliance", (DataTable)dataSet.IncomeLevelCompliance);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Budget/BudgetIncomeLevelCompliance.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = $"Grado de Cumplimiento de Ingresos del Año {yearValue}";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void SpendLevelCompliance()
        {
            var year = this.Request.QueryString["year"];
            var yearValue = DateTime.Now.Year;

            if (!string.IsNullOrWhiteSpace(year))
            {
                yearValue = Convert.ToInt32(year);
            }

            var dataSet = new ReportsDataSet();

            var budgets = this.budgetsService.GetSpendLevelCompliance(yearValue);
            decimal totalAmount = 0;

            foreach (var b in budgets)
            {
                if (b.Level.Equals("ART"))
                {
                    totalAmount += b.Amount;
                }
            }

            foreach (var b in budgets)
            {
                var budget = dataSet.SpendLevelCompliance.NewSpendLevelComplianceRow();

                budget.Year = b.Year;
                budget.Level = b.Level;
                budget.ChapterId = b.ChapterId == null ? string.Empty : b.ChapterId.ToString();
                budget.ArticleId = b.ArticleId == null ? string.Empty : b.ArticleId.ToString();
                budget.ConceptId = b.ConceptId == null ? string.Empty : b.ConceptId.ToString();
                budget.SubConceptId = b.SubConceptId == null ? string.Empty : b.SubConceptId.ToString();
                budget.ApplicationNumber = b.ApplicationNumber;
                budget.Description = b.Description;
                budget.Amount = b.Amount;
                budget.RcAmount = b.RcAmount;
                budget.AdAmount = b.AdAmount;
                budget.OAmount = b.OAmount;
                budget.PAmount = b.PAmount;
                budget.TotalAmount = totalAmount;

                dataSet.SpendLevelCompliance.AddSpendLevelComplianceRow(budget);
            }

            var rpt = new ReportDataSource("SpendLevelCompliance", (DataTable)dataSet.SpendLevelCompliance);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Budget/BudgetSpendLevelCompliance.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = $"Grado de Cumplimiento de Gastos del Año {yearValue}";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void SpendRecordsList()
        {
            var budgetYears = this.Request.QueryString["budgetYears"];
            var budgetYearsValue = string.IsNullOrWhiteSpace(budgetYears) ? (int?)null : Convert.ToInt32(budgetYears);

            var recordNumber = this.Request.QueryString["number"];
            var recordNumberValue = string.IsNullOrWhiteSpace(recordNumber) ? (int?)null : Convert.ToInt32(recordNumber);

            var provenances = this.Request.QueryString["provenances"];
            var provenancesValue = string.IsNullOrWhiteSpace(provenances) ? (int?)null : Convert.ToInt32(provenances);

            var providers = this.Request.QueryString["providers"];
            var providersValue = string.IsNullOrWhiteSpace(providers) ? (int?)null : Convert.ToInt32(providers);

            var providerNif = this.Request.QueryString["providerNif"];
            var providerNifValue = string.IsNullOrWhiteSpace(providerNif) ? string.Empty : providerNif;

            var multiYear = this.Request.QueryString["multiYear"];
            var multiYearValue = string.IsNullOrWhiteSpace(multiYear) ? (bool?)null : multiYear.Equals("1");

            if (budgetYearsValue == null && recordNumberValue == null && provenancesValue == null && providersValue == null && string.IsNullOrWhiteSpace(providerNifValue) && multiYearValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var spends = this.accountingRecordsService.GetSpendsReports(budgetYearsValue, recordNumberValue, provenancesValue, providersValue, providerNifValue, multiYearValue);

            foreach (var s in spends)
            {
                if (s.EXP_NUM_EXP_CONTABLE_ANUAL == null)
                {
                    continue;
                }
                
                var spend = dataSet.SpendRecordsList.NewSpendRecordsListRow();

                spend.Number = s.EXP_NUM_EXP_CONTABLE_ANUAL.ToString();
                spend.Year = s.EXP_ANO_PRESUPUESTO.ToString();
                spend.ProviderName = s.PROV_NOMBRE;
                spend.GroupKey = $"{spend.Number}-{spend.Year}-{spend.ProviderName}";
                spend.Description = s.DOC_DESCRIPCION;

                var amount = s.IMPORTE;

                if (!s.TIPD_POSITIVO)
                {
                    amount *= -1;
                }

                if (!s.FASE_RC && !s.FASE_P)
                {
                    spend.RcAmount = 0;
                    spend.PAmount = 0;
                }
                else
                {
                    if (s.FASE_RC)
                    {
                        spend.RcAmount = amount;
                        spend.PAmount = 0;
                    }
                    else
                    {
                        spend.PAmount = amount;
                        spend.RcAmount = 0;
                    }
                }

                dataSet.SpendRecordsList.AddSpendRecordsListRow(spend);
            }

            var rpt = new ReportDataSource("SpendRecordsList", (DataTable)dataSet.SpendRecordsList);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Spend/SpendRecordsList.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Listado de Expedientes";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void SpendBillPaymentList()
        {
            var provider = this.Request.QueryString["provider"];
            var providerValue = string.IsNullOrWhiteSpace(provider) ? (int?)null : Convert.ToInt32(provider);

            var billNumber = this.Request.QueryString["billNumber"];
            var billNumberValue = string.IsNullOrWhiteSpace(billNumber) ? string.Empty : billNumber;

            var roAmount = this.Request.QueryString["roAmount"];
            var roAmountValue = string.IsNullOrWhiteSpace(roAmount) ? (decimal?)null : Convert.ToDecimal(roAmount);

            var exerciseYear = this.Request.QueryString["exerciseYear"];
            var exerciseYearValue = string.IsNullOrWhiteSpace(exerciseYear) ? (int?)null : Convert.ToInt32(exerciseYear);

            var billDate = this.Request.QueryString["billDate"];
            var billDateValue = string.IsNullOrWhiteSpace(billDate) ? string.Empty : Convert.ToDateTime(billDate).ToString("d", new CultureInfo("es-ES"));

            var roDate = this.Request.QueryString["roDate"];
            var roDateValue = string.IsNullOrWhiteSpace(roDate) ? string.Empty : Convert.ToDateTime(roDate).ToString("d", new CultureInfo("es-ES"));

            var administrativeId = this.Request.QueryString["administrativeId"];
            var administrativeIdValue = string.IsNullOrWhiteSpace(administrativeId) ? (int?)null : Convert.ToInt32(administrativeId);

            var billAmount = this.Request.QueryString["billAmount"];
            var billAmountValue = string.IsNullOrWhiteSpace(billAmount) ? (decimal?)null : Convert.ToDecimal(billAmount);

            if (providerValue == null && string.IsNullOrWhiteSpace(billNumberValue) && roAmountValue == null && exerciseYearValue == null && string.IsNullOrWhiteSpace(billDateValue) && string.IsNullOrWhiteSpace(roDateValue) && administrativeIdValue == null && billAmountValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var purchases = this.billPurchasesService.GetBillPurchases(exerciseYearValue, administrativeIdValue, billNumberValue, billDateValue, billAmountValue, roDateValue, providerValue, roAmountValue, string.Empty);

            foreach (var p in purchases)
            {
                var purchase = dataSet.SpendBillPaymentList.NewSpendBillPaymentListRow();

                purchase.RoDate = roDateValue;
                purchase.BillNumber = p.FA_NUM_FACTURA;
                purchase.BillDate = p.FA_FECHA_FACTURA == null ? string.Empty : ((DateTime)p.FA_FECHA_FACTURA).ToString("d", new CultureInfo("es-ES"));
                purchase.Amount = (decimal)p.FA_IMPORTE_INTEGRO;
                purchase.BaseAmount = (decimal)p.FA_BASE_IMPONIBLE;
                purchase.IvaAmount = (decimal)p.FA_IMPORTE_IVA;
                purchase.IrpfAmount = (decimal)p.FA_IMPORTE_RETENCION;
                purchase.BoeAmount = (decimal)p.FA_IMPORTE_BOE;
                purchase.Bound = p.BoundLabel;
                purchase.AccountingFile = p.RecordNumber;

                dataSet.SpendBillPaymentList.AddSpendBillPaymentListRow(purchase);
            }

            var rpt = new ReportDataSource("SpendBillPaymentList", (DataTable)dataSet.SpendBillPaymentList);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Spend/BillPaymentList.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Listado Facturas Compras";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void SpendApplicationAmount()
        {
            var year = this.Request.QueryString["year"];
            var yearValue = string.IsNullOrWhiteSpace(year) ? (int?)null : Convert.ToInt32(year);

            var application = this.Request.QueryString["application"];

            int? chapterCode = null;
            int? articleCode = null;
            int? conceptCode = null;
            int? subConceptCode = null;

            if (!string.IsNullOrWhiteSpace(application))
            {
                var splits = application.Split('/');

                if (splits.Length >= 1)
                {
                    if (!string.IsNullOrWhiteSpace(splits[0]))
                    {
                        chapterCode = Convert.ToInt32(splits[0]);
                    }
                }

                if (splits.Length >= 2)
                {
                    if (!string.IsNullOrWhiteSpace(splits[1]))
                    {
                        articleCode = Convert.ToInt32(splits[1]);
                    }
                }

                if (splits.Length >= 3)
                {
                    if (!string.IsNullOrWhiteSpace(splits[2]))
                    {
                        conceptCode = Convert.ToInt32(splits[2]);
                    }
                }

                if (splits.Length >= 4)
                {
                    if (!string.IsNullOrWhiteSpace(splits[3]))
                    {
                        subConceptCode = Convert.ToInt32(splits[3]);
                    }
                }
            }

            var sinceAmount = this.Request.QueryString["sinceAmount"];
            var sinceAmountValue = string.IsNullOrWhiteSpace(sinceAmount) ? (decimal?)null : Convert.ToDecimal(sinceAmount);

            var untilAmount = this.Request.QueryString["untilAmount"];
            var untilAmountValue = string.IsNullOrWhiteSpace(untilAmount) ? (decimal?)null : Convert.ToDecimal(untilAmount);

            var bound = this.Request.QueryString["bound"];
            var boundValue = string.IsNullOrWhiteSpace(bound) ? (bool?)null : bound.Equals("1");

            var square = this.Request.QueryString["square"];
            var squareValue = string.IsNullOrWhiteSpace(square) ? (bool?)null : square.Equals("1");

            var provider = this.Request.QueryString["provider"];
            var providerValue = string.IsNullOrWhiteSpace(provider) ? (int?)null : Convert.ToInt32(provider);

            var providerNif = this.Request.QueryString["providerNif"];
            var providerNifValue = string.IsNullOrWhiteSpace(providerNif) ? string.Empty : providerNif;

            var docType = this.Request.QueryString["docType"];
            var docTypeValue = string.IsNullOrWhiteSpace(docType) ? (int?)null : Convert.ToInt32(docType);

            var docNumber = this.Request.QueryString["docNumber"];
            var docNumberValue = string.IsNullOrWhiteSpace(docNumber) ? (int?)null : Convert.ToInt32(docNumber);

            if (yearValue == null && string.IsNullOrWhiteSpace(application) && untilAmountValue == null && sinceAmountValue == null && boundValue == null && squareValue == null && providerValue == null && string.IsNullOrWhiteSpace(providerNifValue) && docTypeValue == null && docNumberValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var applications = this.accountingRecordsService.GetSpendsWithAppAmount(yearValue, chapterCode, articleCode, conceptCode, subConceptCode, sinceAmountValue, untilAmountValue, boundValue, squareValue, providerValue, providerNifValue, docTypeValue, docNumberValue, null, null);

            foreach (var a in applications.OrderBy(a => a.EXP_NUM_EXP_CONTABLE_ANUAL))
            {
                var app = dataSet.SpendApplicationAmount.NewSpendApplicationAmountRow();

                app.Year = a.EXP_ANO_PRESUPUESTO == null ? string.Empty : a.EXP_ANO_PRESUPUESTO.ToString();
                app.FileNumber = a.EXP_NUM_EXP_CONTABLE_ANUAL == null ? string.Empty : a.EXP_NUM_EXP_CONTABLE_ANUAL.ToString();
                app.Application = a.CACS_NUMERO;
                app.DocType = a.TIPO_DOC;
                app.Amount = a.IMPORTE;
                app.Bound = a.DOC_ENLAZADO_TESORERIA_LABEL;
                app.Description = a.EA_DESCRIPCION;
                app.ProviderName = a.PROV_NOMBRE;
                app.Square = a.EXP_CUADRADO_LABEL;

                dataSet.SpendApplicationAmount.AddSpendApplicationAmountRow(app);
            }

            var rpt = new ReportDataSource("SpendApplicationAmount", (DataTable)dataSet.SpendApplicationAmount);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Spend/ApplicationAmount.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Listado Aplicación Importe";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void SpendCertificate()
        {
            var documentId = this.Request.QueryString["documentId"];
            var documentIdValue = string.IsNullOrWhiteSpace(documentId) ? (int?)null : Convert.ToInt32(documentId);

            var name = this.Request.QueryString["name"];
            var nameValue = string.IsNullOrWhiteSpace(name) ? string.Empty : name;

            var ocupation = this.Request.QueryString["ocupation"];
            var ocupationValue = string.IsNullOrWhiteSpace(ocupation) ? string.Empty : ocupation;

            if (documentIdValue == null || string.IsNullOrWhiteSpace(nameValue) || string.IsNullOrWhiteSpace(ocupationValue))
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var result = this.accountingRecordsService.GetSpendCertificate((int)documentIdValue);

            var ministery = result.Item3.MINISTERIO.ToUpperInvariant();
            var university = result.Item3.NOMBRE_ORGANISMO;
            var workerNameOcup = $"{nameValue} {ocupationValue} de la {result.Item3.NOMBRE_ORGANISMO}".ToUpperInvariant();
            var day = string.Empty;
            var month = string.Empty;
            var year = string.Empty;

            if (result.Item1.DOC_FECHA_PROPUESTA != null)
            {
                day = ((DateTime)result.Item1.DOC_FECHA_PROPUESTA).Day.ToWords(new CultureInfo("es-ES"));
                month = this.GetMonthLabel(((DateTime)result.Item1.DOC_FECHA_PROPUESTA).Month).ToLowerInvariant();
                year = ((DateTime)result.Item1.DOC_FECHA_PROPUESTA).Year.ToWords(new CultureInfo("es-ES"));
            }

            var literalDate = $"{day} de {month} de {year}";
            var initialText = $"Que con fecha {literalDate} en el programa {result.Item3.CODIGO_MINISTERIO} {result.Item3.CODIGO_ORGANISMO} {result.Item1.PRO_NUMERO} y con las aplicaciones";
            var fileNumber = result.Item1.EXP_NUM_EXP_CONTABLE_ANUAL == null ? string.Empty : result.Item1.EXP_NUM_EXP_CONTABLE_ANUAL.ToString();
           
            decimal total = 0;

            foreach (var cm in result.Item2)
            {
                var value = (decimal)cm.DOCA_IMPORTE;

                total += value;
            }

            var totalLetters = this.GetAmountToWords(total);

            var finalText = $"para el expediente contable {fileNumber} de {result.Item1.DOC_DESCRIPCION} por un importe de {totalLetters} ({total.ToString("N", new CultureInfo("es-ES"))} €).";

            if (result.Item2.Any())
            {
                foreach (var a in result.Item2)
                {
                    var app = dataSet.SpendCertificate.NewSpendCertificateRow();

                    app.Ministery = ministery;
                    app.University = university;
                    app.WorkerNameOcup = workerNameOcup;
                    app.InitialText = initialText;
                    app.Application = a.CACS_NUMERO;
                    app.Amount = (decimal)a.DOCA_IMPORTE;
                    app.FinalText = finalText;
                    app.LiteralDate = literalDate;

                    dataSet.SpendCertificate.AddSpendCertificateRow(app);
                }
            }
            else
            {
                var app = dataSet.SpendCertificate.NewSpendCertificateRow();

                app.Ministery = ministery;
                app.University = university;
                app.WorkerNameOcup = workerNameOcup;
                app.InitialText = initialText;
                app.FinalText = finalText;
                app.LiteralDate = literalDate;

                dataSet.SpendCertificate.AddSpendCertificateRow(app);
            }

            var rpt = new ReportDataSource("SpendCertificate", (DataTable)dataSet.SpendCertificate);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Spend/Certificate.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Certificado";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void IncomeCreditModification()
        {
            var id = this.Request.QueryString["id"];
            var idValue = string.IsNullOrWhiteSpace(id) ? (int?)null : Convert.ToInt32(id);

            var description = this.Request.QueryString["description"];
            var descriptionValue = string.IsNullOrWhiteSpace(description) ? string.Empty : description;

            var year = this.Request.QueryString["year"];
            var yearValue = string.IsNullOrWhiteSpace(year) ? (int?)null : Convert.ToInt32(year);

            var order = this.Request.QueryString["order"];
            var orderValue = string.IsNullOrWhiteSpace(order) ? (int?)null : Convert.ToInt32(order);

            var proposalDate = this.Request.QueryString["proposalDate"];
            var proposalDateValue = string.IsNullOrWhiteSpace(proposalDate) ? string.Empty : proposalDate;

            var modificationType = this.Request.QueryString["modificationType"];
            var modificationTypeValue = string.IsNullOrWhiteSpace(modificationType) ? (int?)null : Convert.ToInt32(modificationType);

            var modificationTypeDescription = this.Request.QueryString["modificationTypeDescription"];
            var modificationTypeDescriptionValue = string.IsNullOrWhiteSpace(modificationTypeDescription) ? string.Empty : modificationTypeDescription;

            var dataSet = new ReportsDataSet();

            var creditModifications = this.creditModificationBudgetsService.GetByCreditModification((int)idValue, "I");

            decimal total = 0;

            foreach (var cm in creditModifications)
            {
                var value = (decimal)cm.MODP_IMPORTE;

                if (cm.MODP_POSITIVO == false)
                {
                    value *= -1;
                }

                total += value;
            }
            
            var totalLetters = this.GetAmountToWords(total);

            foreach (var c in creditModifications)
            {
                var creditModification = dataSet.BudgetCreditmodification.NewBudgetCreditmodificationRow();

                creditModification.OrderNumber = (int)orderValue;
                creditModification.Year = (int)yearValue;
                creditModification.ModificationType = $"0{(int)modificationTypeValue}";
                creditModification.ModificationTypeDescription = modificationTypeDescriptionValue;
                creditModification.Amount = (bool)c.MODP_POSITIVO ? (decimal)c.MODP_IMPORTE : (decimal)c.MODP_IMPORTE * -1;
                creditModification.CacsNumber = c.CACS_NUMERO;
                creditModification.Description = descriptionValue;
                creditModification.ProposalDate = proposalDateValue;
                creditModification.TotalAmount = total;
                creditModification.TotalAmountLetters = totalLetters;
                creditModification.Label = "CONTABILIDAD DEL PRESUPUESTO DE INGRESOS";

                dataSet.BudgetCreditmodification.AddBudgetCreditmodificationRow(creditModification);
            }

            var rpt = new ReportDataSource("BudgetCreditmodification", (DataTable)dataSet.BudgetCreditmodification);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Budget/BudgetCreditModification.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = $"Contabilidad del Presupuesto de Ingresos {orderValue}-{yearValue}";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void SpendCreditModification()
        {
            var id = this.Request.QueryString["id"];
            var idValue = string.IsNullOrWhiteSpace(id) ? (int?)null : Convert.ToInt32(id);

            var description = this.Request.QueryString["description"];
            var descriptionValue = string.IsNullOrWhiteSpace(description) ? string.Empty : description;

            var year = this.Request.QueryString["year"];
            var yearValue = string.IsNullOrWhiteSpace(year) ? (int?)null : Convert.ToInt32(year);

            var order = this.Request.QueryString["order"];
            var orderValue = string.IsNullOrWhiteSpace(order) ? (int?)null : Convert.ToInt32(order);

            var proposalDate = this.Request.QueryString["proposalDate"];
            var proposalDateValue = string.IsNullOrWhiteSpace(proposalDate) ? string.Empty : proposalDate;

            var modificationType = this.Request.QueryString["modificationType"];
            var modificationTypeValue = string.IsNullOrWhiteSpace(modificationType) ? (int?)null : Convert.ToInt32(modificationType);

            var modificationTypeDescription = this.Request.QueryString["modificationTypeDescription"];
            var modificationTypeDescriptionValue = string.IsNullOrWhiteSpace(modificationTypeDescription) ? string.Empty : modificationTypeDescription;

            var dataSet = new ReportsDataSet();

            var creditModifications = this.creditModificationBudgetsService.GetByCreditModification((int)idValue, "G");

            decimal total = 0;

            foreach (var cm in creditModifications)
            {
                var value = (decimal)cm.MODP_IMPORTE;

                if (cm.MODP_POSITIVO == false)
                {
                    value *= -1;
                }

                total += value;
            }

            var totalLetters = this.GetAmountToWords(total);

            foreach (var c in creditModifications)
            {
                var creditModification = dataSet.BudgetCreditmodification.NewBudgetCreditmodificationRow();

                creditModification.OrderNumber = (int)orderValue;
                creditModification.Year = (int)yearValue;
                creditModification.ModificationType = $"0{(int)modificationTypeValue}";
                creditModification.ModificationTypeDescription = modificationTypeDescriptionValue;
                creditModification.Amount = (bool)c.MODP_POSITIVO ? (decimal)c.MODP_IMPORTE : (decimal)c.MODP_IMPORTE * -1;
                creditModification.CacsNumber = c.CACS_NUMERO;
                creditModification.Description = descriptionValue;
                creditModification.ProposalDate = proposalDateValue;
                creditModification.TotalAmount = total;
                creditModification.TotalAmountLetters = totalLetters;
                creditModification.Label = "CONTABILIDAD DEL PRESUPUESTO DE GASTOS";

                dataSet.BudgetCreditmodification.AddBudgetCreditmodificationRow(creditModification);
            }

            var rpt = new ReportDataSource("BudgetCreditmodification", (DataTable)dataSet.BudgetCreditmodification);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Budget/BudgetCreditModification.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = $"Contabilidad del Presupuesto de Gastos {orderValue}_{yearValue}";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void SpendRC()
        {
            var documentId = this.Request.QueryString["documentId"];
            var documentIdValue = string.IsNullOrWhiteSpace(documentId) ? (int?)null : Convert.ToInt32(documentId);

            if (documentIdValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var dataTables = this.accountingDocumentsService.GetSpendRcById((int)documentIdValue);

            var ministeryName = dataTables.Item2 == null ? string.Empty : dataTables.Item2.MINISTERIO;
            var ministeryCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_MINISTERIO;
            var companyCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_ORGANISMO;
            var university = dataTables.Item2 == null ? string.Empty : dataTables.Item2.NOMBRE_ORGANISMO;
            var nature = dataTables.Item1.TIPD_CLAVE == null ? string.Empty : dataTables.Item1.TIPD_CLAVE.ToString();
            var exerciseYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("yyyy");
            int proposalYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? 0 : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).Year;
            int budgetYearInt = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? 0 : (short)dataTables.Item1.EXP_ANO_PRESUPUESTO; 
            var budgetYear = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? string.Empty : dataTables.Item1.EXP_ANO_PRESUPUESTO.ToString();
            var proposalDate = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("d", new CultureInfo("es-ES"));
            var exercise = GetExercise(proposalYear, budgetYearInt);
            string description = dataTables.Item1.DOC_DESCRIPCION == null ? string.Empty : dataTables.Item1.DOC_DESCRIPCION;
            string groupName = dataTables.Item1.DOC_CODIGO.ToString();

            var fileNumber = dataTables.Item1.EXP_NUM_EXP_CONTABLE_ANUAL== null ? string.Empty : dataTables.Item1.EXP_NUM_EXP_CONTABLE_ANUAL.ToString();

            string natureIncomeName = dataTables.Item1.TIPD_NOMBRE_CORTO == null ? string.Empty : dataTables.Item1.TIPD_NOMBRE_CORTO;


            string sign = dataTables.Item1.TIPD_POSITIVO? "POSITIVO" : "NEGATIVO";
            string signNumber = dataTables.Item1.TIPD_POSITIVO ? "0" : "1";
            
            string text = string.Empty;
            bool haveRc = this.accountingDocumentsService.HaveRcPhase((int)documentIdValue);
            bool haveAd = this.accountingDocumentsService.HaveAdPhase((int)documentIdValue);
            //bool haveO = this.accountingDocumentsService.HaveOPhase((int)documentIdValue);
            //bool haveP = this.accountingDocumentsService.HavePPhase((int)documentIdValue);
            if (haveRc)    text = "Retención de crédito para gastar";
            else if (haveAd) text = "Autorización y compromiso sobre crédito retenido";

            string proNumero = dataTables.Item1.PRO_NUMERO == null ? string.Empty : dataTables.Item1.PRO_NUMERO;


            decimal total = 0;
            Decimal contador = 0;
            string cacsNumero = string.Empty;
            if (dataTables.Item3.Any())
            {
                foreach (var i in dataTables.Item3)
                {
                    contador += 1;
                    total += (decimal)i.DOCA_IMPORTE;
                    if (contador == 1) cacsNumero = i.CACS_NUMERO;
                }
            }

            var spend = dataSet.SpendRC.NewSpendRCRow();

            spend.TotalAmountToWord = GetAmountToWords(total);
            spend.TotalAmount = (decimal)total;
            spend.TotalApplications = contador;
            spend.Sign = sign;
            spend.SignNumber = signNumber;
            spend.Text = text;
            spend.DetEconomica = contador == 1 ? cacsNumero : string.Empty;
            spend.DetFuncional = contador == 1 ? proNumero : string.Empty;
            spend.BudgetYear = budgetYear;
            spend.MinisteryName = ministeryName;
            spend.MinisteryCode = ministeryCode;
            spend.CompanyCode = companyCode;
            spend.SchoolName = university;
            spend.NatureIncome = nature;
            spend.ExerciseYear = exerciseYear;
            spend.Exercise = exercise;
            spend.Description = description;
            spend.GroupName = groupName;
            spend.FileNumber = fileNumber;
            spend.NatureIncomeName = natureIncomeName;
            spend.NatureIncome = nature;
            spend.ProposalDate = proposalDate;

            dataSet.SpendRC.AddSpendRCRow(spend);

            var rpt = new ReportDataSource("SpendRC", (DataTable)dataSet.SpendRC);
            this.CallReport(rpt, "Reports/Spend/SpendRC.rdlc", "Retención de Crédito");
        }

        private void SpendAnnexRc()
        {
            var documentId = this.Request.QueryString["documentId"];
            var documentIdValue = string.IsNullOrWhiteSpace(documentId) ? (int?)null : Convert.ToInt32(documentId);

            if (documentIdValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var dataTables = this.accountingDocumentsService.GetSpendRcById((int)documentIdValue);

            var ministeryName = dataTables.Item2 == null ? string.Empty : dataTables.Item2.MINISTERIO;
            var ministeryCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_MINISTERIO;
            var companyCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_ORGANISMO;
            var university = dataTables.Item2 == null ? string.Empty : dataTables.Item2.NOMBRE_ORGANISMO;
            var nature = dataTables.Item1.TIPD_CLAVE == null ? string.Empty : dataTables.Item1.TIPD_CLAVE.ToString();
            var exerciseYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("yyyy");
            int proposalYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? 0 : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).Year;
            int budgetYearInt = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? 0 : (short)dataTables.Item1.EXP_ANO_PRESUPUESTO;
            var budgetYear = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? string.Empty : dataTables.Item1.EXP_ANO_PRESUPUESTO.ToString();
            var proposalDate = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("d", new CultureInfo("es-ES"));
            var exercise = GetExercise(proposalYear, budgetYearInt);
            string description = dataTables.Item1.DOC_DESCRIPCION == null ? string.Empty : dataTables.Item1.DOC_DESCRIPCION;
            string groupName = dataTables.Item1.DOC_CODIGO.ToString();


            var fileNumber = dataTables.Item1.EXP_NUM_EXP_CONTABLE_ANUAL == null ? string.Empty : dataTables.Item1.EXP_NUM_EXP_CONTABLE_ANUAL.ToString();
            string natureIncomeName = dataTables.Item1.TIPD_NOMBRE_CORTO == null ? string.Empty : dataTables.Item1.TIPD_NOMBRE_CORTO;


            string sign = dataTables.Item1.TIPD_POSITIVO ? "POSITIVO" : "NEGATIVO";
            string signNumber = dataTables.Item1.TIPD_POSITIVO ? "0" : "1";

            string proNumero = dataTables.Item1.PRO_NUMERO == null ? string.Empty : dataTables.Item1.PRO_NUMERO;


            Double total = 0;
            Decimal contador = 0;
            string cacsNumero = string.Empty;
            if (dataTables.Item3.Any())
            {
                foreach (var i in dataTables.Item3)
                {
                    contador += 1;
                    total += Decimal.ToDouble((decimal)i.DOCA_IMPORTE);
                    if (contador == 1) cacsNumero = i.CACS_NUMERO;

                    var spend = dataSet.SpendRC.NewSpendRCRow();
                    spend.ProNumero = proNumero;
                    spend.Application = i.CACS_NUMERO;
                    spend.Amount = (decimal)i.DOCA_IMPORTE;
                    spend.Account = i.CUEP_NUMERO;
                    spend.TotalAmount = (decimal)total;
                    spend.TotalApplications = contador;
                    spend.Sign = sign;
                    spend.SignNumber = signNumber;
                    spend.BudgetYear = budgetYear;
                    spend.MinisteryName = ministeryName;
                    spend.MinisteryCode = ministeryCode;
                    spend.CompanyCode = companyCode;
                    spend.SchoolName = university;
                    spend.NatureIncome = nature;
                    spend.ExerciseYear = exerciseYear;
                    spend.Exercise = exercise;
                    spend.Description = description;
                    spend.GroupName = groupName;
                    spend.FileNumber = fileNumber;
                    spend.NatureIncomeName = natureIncomeName;
                    spend.ProposalDate = proposalDate;
                    dataSet.SpendRC.AddSpendRCRow(spend);
                }
            }
            else
            {
                //no debería pasar por aquí ya que solo se muestra el anexo si hay más de un registro DA
                var spend = dataSet.SpendRC.NewSpendRCRow();
                spend.Sign = sign;
                spend.SignNumber = signNumber;
                spend.BudgetYear = budgetYear;
                spend.MinisteryName = ministeryName;
                spend.MinisteryCode = ministeryCode;
                spend.CompanyCode = companyCode;
                spend.SchoolName = university;
                spend.NatureIncome = nature;
                spend.ExerciseYear = exerciseYear;
                spend.Exercise = exercise;
                spend.Description = description;
                spend.GroupName = groupName;
                spend.FileNumber = fileNumber;
                spend.NatureIncomeName = natureIncomeName;
                spend.ProposalDate = proposalDate;
                dataSet.SpendRC.AddSpendRCRow(spend);
            }


            var rpt = new ReportDataSource("SpendRC", (DataTable)dataSet.SpendRC);

            this.CallReport(rpt, "Reports/Spend/SpendAnnexRc.rdlc", "Retención de Crédito - Anexo");
        }

        private void SpendAD()
        {
            var documentId = this.Request.QueryString["documentId"];
            var documentIdValue = string.IsNullOrWhiteSpace(documentId) ? (int?)null : Convert.ToInt32(documentId);

            if (documentIdValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var dataTables = this.accountingDocumentsService.GetSpendAdById((int)documentIdValue);

            var ministeryName = dataTables.Item2 == null ? string.Empty : dataTables.Item2.MINISTERIO;
            var ministeryCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_MINISTERIO;
            var companyCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_ORGANISMO;
            var university = dataTables.Item2 == null ? string.Empty : dataTables.Item2.NOMBRE_ORGANISMO;
            var nature = dataTables.Item1.TIPD_CLAVE == null ? string.Empty : dataTables.Item1.TIPD_CLAVE.ToString();
            var exerciseYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("yyyy");
            int proposalYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? 0 : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).Year;
            int budgetYearInt = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? 0 : (short)dataTables.Item1.EXP_ANO_PRESUPUESTO;
            var budgetYear = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? string.Empty : dataTables.Item1.EXP_ANO_PRESUPUESTO.ToString();
            var proposalDate = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("d", new CultureInfo("es-ES"));
            var exercise = GetExercise(proposalYear, budgetYearInt);
            string description = dataTables.Item1.DOC_DESCRIPCION == null ? string.Empty : dataTables.Item1.DOC_DESCRIPCION;
            string groupName = dataTables.Item1.DOC_CODIGO.ToString();
            var proNif = GetNifOk(dataTables.Item1.PROV_NIF == null ? string.Empty : dataTables.Item1.PROV_NIF);
            var proName = dataTables.Item1.PROV_NOMBRE == null ? string.Empty : dataTables.Item1.PROV_NOMBRE;

            var fileNumber = dataTables.Item1.EXP_NUM_EXP_CONTABLE_ANUAL == null ? string.Empty : dataTables.Item1.EXP_NUM_EXP_CONTABLE_ANUAL.ToString();
            string natureIncomeName = dataTables.Item1.TIPD_NOMBRE_CORTO == null ? string.Empty : dataTables.Item1.TIPD_NOMBRE_CORTO;


            string sign = dataTables.Item1.TIPD_POSITIVO ? "POSITIVO" : "NEGATIVO";
            string signNumber = dataTables.Item1.TIPD_POSITIVO ? "0" : "1";

            string text = string.Empty;
            bool haveRc = this.accountingDocumentsService.HaveRcPhase((int)documentIdValue);
            bool haveAd = this.accountingDocumentsService.HaveAdPhase((int)documentIdValue);

            if (haveRc) text = "Retención de crédito para gastar";
            else if (haveAd) text = "Autorización y compromiso sobre crédito retenido";

            string proNumero = dataTables.Item1.PRO_NUMERO == null ? string.Empty : dataTables.Item1.PRO_NUMERO;

            Decimal total = 0;
            Decimal contador = 0;
            string cacsNumero = string.Empty;
            if (dataTables.Item3.Any())
            {
                foreach (var i in dataTables.Item3)
                {
                    contador += 1;
                    total += (decimal)i.DOCA_IMPORTE;
                    if (contador == 1) cacsNumero = i.CACS_NUMERO;
                }
            }

            var spend = dataSet.SpendAD.NewSpendADRow();

            spend.TotalAmountToWord = GetAmountToWords(total);
            spend.TotalAmount = (decimal)total;
            spend.TotalApplications = contador;
            spend.Sign = sign;
            spend.SignNumber = signNumber;
            spend.Text = text;
            spend.DetEconomica = contador == 1 ? cacsNumero : string.Empty;
            spend.DetFuncional = contador == 1 ? proNumero : string.Empty;
            spend.BudgetYear = budgetYear;
            spend.Sign1 = GetSignLine(total, 1);
            spend.Sign2 = GetSignLine(total, 2);
            spend.Sign3 = GetSignLine(total, 3);
            spend.MinisteryName = ministeryName;
            spend.MinisteryCode = ministeryCode;
            spend.CompanyCode = companyCode;
            spend.SchoolName = university;
            spend.NatureIncome = nature;
            spend.ExerciseYear = exerciseYear;
            spend.Exercise = exercise;
            spend.Description = description;
            spend.GroupName = groupName;
            spend.FileNumber = fileNumber;
            spend.NatureIncomeName = natureIncomeName;
            spend.ProposalDate = proposalDate;
            spend.ProviderNif = proNif;
            spend.ProviderName = proName;

            dataSet.SpendAD.AddSpendADRow(spend);

            var rpt = new ReportDataSource("SpendAD", (DataTable)dataSet.SpendAD);

            this.CallReport(rpt, "Reports/Spend/SpendAD.rdlc", "Autorización y compromiso sobre crédito retenido");
        }

        private void SpendO()
        {
            var documentId = this.Request.QueryString["documentId"];
            var documentIdValue = string.IsNullOrWhiteSpace(documentId) ? (int?)null : Convert.ToInt32(documentId);

            if (documentIdValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var dataTables = this.accountingDocumentsService.GetSpendOById((int)documentIdValue);

            var ministeryName = dataTables.Item2 == null ? string.Empty : dataTables.Item2.MINISTERIO;
            var ministeryCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_MINISTERIO;
            var companyCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_ORGANISMO;
            var university = dataTables.Item2 == null ? string.Empty : dataTables.Item2.NOMBRE_ORGANISMO;
            var nature = dataTables.Item1.TIPD_CLAVE == null ? string.Empty : dataTables.Item1.TIPD_CLAVE.ToString();
            int natureInt = dataTables.Item1.TIPD_CLAVE == null ? 0 : (int)dataTables.Item1.TIPD_CLAVE;
            var exerciseYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("yyyy");
            int proposalYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? 0 : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).Year;
            int budgetYearInt = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? 0 : (short)dataTables.Item1.EXP_ANO_PRESUPUESTO;
            var budgetYear = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? string.Empty : dataTables.Item1.EXP_ANO_PRESUPUESTO.ToString();
            var proposalDate = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("d", new CultureInfo("es-ES"));
            var exercise = GetExercise(proposalYear, budgetYearInt);
            string description = dataTables.Item1.DOC_DESCRIPCION == null ? string.Empty : dataTables.Item1.DOC_DESCRIPCION;
            string groupName = dataTables.Item1.DOC_CODIGO.ToString();
            var proNif = GetNifOk(dataTables.Item1.PROV_NIF == null ? string.Empty : dataTables.Item1.PROV_NIF);
            var proName = dataTables.Item1.PROV_NOMBRE == null ? string.Empty : dataTables.Item1.PROV_NOMBRE;

            var fileNumber = dataTables.Item1.EXP_NUM_EXP_CONTABLE_ANUAL == null ? string.Empty : dataTables.Item1.EXP_NUM_EXP_CONTABLE_ANUAL.ToString();
            string natureName = dataTables.Item1.TIPD_NOMBRE_CORTO == null ? string.Empty : dataTables.Item1.TIPD_NOMBRE_CORTO;
            string senNumero = dataTables.Item1.SEN_NUMERO == null ? string.Empty : dataTables.Item1.SEN_NUMERO.ToString();
            string cenAreaOrigen = dataTables.Item1.CEN_AREA_ORIGEN == null ? string.Empty : dataTables.Item1.CEN_AREA_ORIGEN.ToString();
            string forCodigo = dataTables.Item1.FOR_CODIGO == null ? string.Empty : dataTables.Item1.FOR_CODIGO.ToString();
            string tippCodigo = dataTables.Item1.TIPP_CODIGO == null ? string.Empty : dataTables.Item1.TIPP_CODIGO.ToString();
            string ordinalPerceiver = dataTables.Item1.CUE_ORDINAL_PERCEPTOR == null ? string.Empty : dataTables.Item1.CUE_ORDINAL_PERCEPTOR.ToString();

            string bookEntryDate = dataTables.Item1.DOC_FECHA_ASIENTO_DIARIO == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_ASIENTO_DIARIO).ToString("d", new CultureInfo("es-ES"));

            string sign = dataTables.Item1.TIPD_POSITIVO ? "POSITIVO" : "NEGATIVO";
            string signNumber = dataTables.Item1.TIPD_POSITIVO ? "0" : "1";

            string text = string.Empty;
            if (natureName.Equals("ADO") && natureInt == 240) text = "Autorización, compromiso y reconocimiento de obligaciones sobre crédito disponible.";
            if (natureName.Equals("ADO") && natureInt == 250) text = "Autorización, compromiso y reconocimiento de obligaciones sobre crédito retenido.";
            if (natureName.Equals("O") && natureInt == 400) text = "Reconocimiento de obligaciones.";
            if (natureName.Equals("O") && natureInt == 700) text = "Rectificación de operaciones pagadas. Reconocimiento de obligaciones.";


            string proNumero = dataTables.Item1.PRO_NUMERO == null ? string.Empty : dataTables.Item1.PRO_NUMERO;

            Decimal total = 0;
            Decimal contador = 0;
            string cacsNumero = string.Empty;
            string cuepNumero = string.Empty;
            if (dataTables.Item3.Any())
            {
                foreach (var i in dataTables.Item3)
                {
                    contador += 1;
                    total += (decimal)i.DOCA_IMPORTE;
                    if (contador == 1) cacsNumero = i.CACS_NUMERO;
                    if (contador == 1) cuepNumero = i.CUEP_NUMERO;
                }
            }

            var spend = dataSet.SpendO.NewSpendORow();

            spend.Account = cuepNumero;
            spend.TotalAmountToWord = GetAmountToWords(total);
            spend.TotalAmount = (decimal)total;
            spend.TotalApplications = contador;
            spend.Sign = sign;
            spend.SignNumber = signNumber;
            spend.Text = text;
            spend.DetEconomica = contador == 1 ? cacsNumero : string.Empty;
            spend.DetFuncional = contador == 1 ? proNumero : string.Empty;
            spend.BudgetYear = budgetYear;
            spend.Sign1 = GetSignLine(total, 1);
            spend.Sign2 = GetSignLine(total, 2);
            spend.Sign3 = GetSignLine(total, 3);
            spend.MinisteryName = ministeryName;
            spend.MinisteryCode = ministeryCode;
            spend.CompanyCode = companyCode;
            spend.SchoolName = university;
            spend.Nature = nature;
            spend.ExerciseYear = exerciseYear;
            spend.Exercise = exercise;
            spend.Description = description;
            spend.GroupName = groupName;
            spend.FileNumber = fileNumber;
            spend.NatureName = natureName;
            spend.ProposalDate = proposalDate;
            spend.OrdinalPerceiver = ordinalPerceiver;
            spend.TippCodigo = tippCodigo;
            spend.ForCodigo = forCodigo;
            spend.CenAreaOrigen = cenAreaOrigen;
            spend.SenNumero = senNumero;
            spend.ProviderNif = proNif;
            spend.ProviderName = proName;
            spend.BookEntryDate = bookEntryDate;

            dataSet.SpendO.AddSpendORow(spend);

            var rpt = new ReportDataSource("SpendO", (DataTable)dataSet.SpendO);

            this.CallReport(rpt, "Reports/Spend/SpendO.rdlc", "Reconocimiento Obligaciones");
        }

        private void SpendP()
        {
            var documentId = this.Request.QueryString["documentId"];
            var documentIdValue = string.IsNullOrWhiteSpace(documentId) ? (int?)null : Convert.ToInt32(documentId);

            if (documentIdValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var dataTables = this.accountingDocumentsService.GetSpendPById((int)documentIdValue);

            var ministeryName = dataTables.Item2 == null ? string.Empty : dataTables.Item2.MINISTERIO;
            var ministeryCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_MINISTERIO;
            var companyCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_ORGANISMO;
            var university = dataTables.Item2 == null ? string.Empty : dataTables.Item2.NOMBRE_ORGANISMO;
            var nature = dataTables.Item1.TIPD_CLAVE == null ? string.Empty : dataTables.Item1.TIPD_CLAVE.ToString();
            int natureInt = dataTables.Item1.TIPD_CLAVE == null ? 0 : (int)dataTables.Item1.TIPD_CLAVE;
            var exerciseYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("yyyy");
            int proposalYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? 0 : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).Year;
            int budgetYearInt = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? 0 : (short)dataTables.Item1.EXP_ANO_PRESUPUESTO;
            var budgetYear = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? string.Empty : dataTables.Item1.EXP_ANO_PRESUPUESTO.ToString();
            var proposalDate = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("d", new CultureInfo("es-ES"));
            var exercise = GetExercise(proposalYear, budgetYearInt);
            string description = dataTables.Item1.DOC_DESCRIPCION == null ? string.Empty : dataTables.Item1.DOC_DESCRIPCION;
            string groupName = dataTables.Item1.DOC_CODIGO.ToString();
            var proNif = GetNifOk(dataTables.Item1.PROV_NIF == null ? string.Empty : dataTables.Item1.PROV_NIF);
            var proName = dataTables.Item1.PROV_NOMBRE == null ? string.Empty : dataTables.Item1.PROV_NOMBRE;

            var fileNumber = dataTables.Item1.EXP_NUM_EXP_CONTABLE_ANUAL == null ? string.Empty : dataTables.Item1.EXP_NUM_EXP_CONTABLE_ANUAL.ToString();

            string natureName = dataTables.Item1.TIPD_NOMBRE_CORTO == null ? string.Empty : dataTables.Item1.TIPD_NOMBRE_CORTO;
            string senNumero = dataTables.Item1.SEN_NUMERO == null ? string.Empty : dataTables.Item1.SEN_NUMERO.ToString();
            string cenAreaOrigen = dataTables.Item1.CEN_AREA_ORIGEN == null ? string.Empty : dataTables.Item1.CEN_AREA_ORIGEN.ToString();
            string forCodigo = dataTables.Item1.FOR_CODIGO == null ? string.Empty : dataTables.Item1.FOR_CODIGO.ToString();
            string tippCodigo = dataTables.Item1.TIPP_CODIGO == null ? string.Empty : dataTables.Item1.TIPP_CODIGO.ToString();
            string ordinalPerceiver = dataTables.Item1.CUE_ORDINAL_PERCEPTOR == null ? string.Empty : dataTables.Item1.CUE_ORDINAL_PERCEPTOR.ToString();

            string cheque = dataTables.Item1.DOC_NUMERO_CHEQUE == null ? string.Empty : "T. Bco. nº " + dataTables.Item1.DOC_NUMERO_CHEQUE;


            string sign = dataTables.Item1.TIPD_POSITIVO ? "POSITIVO" : "NEGATIVO";
            string signNumber = dataTables.Item1.TIPD_POSITIVO ? "0" : "1";

            string text = string.Empty;
            if (natureName.Equals("OP")) text = "Obligaciones reconocidas.";
            if (natureName.Equals("P")) text = "Ordenación del pago.";
            if (natureName.Equals("ADOP"))
            {
                if (natureInt == 240) text = "Autorización, compromiso y reconocimiento de obligaciones sobre crédito disponible.";
                if (natureInt == 250) text = "Autorización, compromiso y reconocimiento de obligaciones sobre crédito retenido.";
                if (natureInt == 740) text = "Rectificación operaciones pagadas. Autorización, compromiso y reconocimiento de obligaciones sobre disponible.";
                if (natureInt == 750) text = "Rectificación operaciones pagadas. Autorización, compromiso y reconocimiento de obligaciones sobre retenido.";
                if (natureInt == 810) text = "Reintegro de ADOP sobre disponible.";
                if (natureInt == 820) text = "Reintegro de ADOP sobre retenido.";

            }

            if (natureName.Equals("OP")) natureName = "O";
            else if (natureInt == 250) natureName = "ADO";

            if (natureInt == 410)
            {
                natureInt = 400;
                nature = "400";
            }

            string proNumero = dataTables.Item1.PRO_NUMERO == null ? string.Empty : dataTables.Item1.PRO_NUMERO;

            Decimal total = 0;
            Decimal contador = 0;
            string cacsNumero = string.Empty;
            string cuepNumero = string.Empty;
            if (dataTables.Item3.Any())
            {
                foreach (var i in dataTables.Item3)
                {
                    contador += 1;
                    total += (decimal)i.DOCA_IMPORTE;
                    if (contador == 1) cacsNumero = i.CACS_NUMERO;
                    if (contador == 1) cuepNumero = i.CUEP_NUMERO;
                }
            }




            var spend = dataSet.SpendP.NewSpendPRow();

            spend.TotalAmountToWord = GetAmountToWords(total);
            spend.TotalAmount = (decimal)total;
            spend.TotalApplications = contador;
            spend.Sign = sign;
            spend.SignNumber = signNumber;
            spend.Text = text;
            spend.DetEconomica = contador == 1 ? cacsNumero : string.Empty;
            spend.DetFuncional = contador == 1 ? proNumero : string.Empty;
            spend.BudgetYear = budgetYear;
            spend.Sign1 = GetSignLine(total, 1);
            spend.Sign2 = GetSignLine(total, 2);
            spend.Sign3 = GetSignLine(total, 3);
            spend.CtaPGCP = contador == 1 ? cuepNumero : string.Empty;

            spend.MinisteryName = ministeryName;
            spend.MinisteryCode = ministeryCode;
            spend.CompanyCode = companyCode;
            spend.SchoolName = university;
            spend.Nature = nature;
            spend.ExerciseYear = exerciseYear;
            spend.Exercise = exercise;
            spend.Description = description;
            spend.GroupName = groupName;
            spend.FileNumber = fileNumber;
            spend.NatureName = natureName;
            spend.ProposalDate = proposalDate;
            spend.OrdinalPerceiver = ordinalPerceiver;
            spend.TippCodigo = tippCodigo;
            spend.ForCodigo = forCodigo;
            spend.CenAreaOrigen = cenAreaOrigen;
            spend.SenNumero = senNumero;
            spend.ProviderNif = proNif;
            spend.ProviderName = proName;
            spend.Cheque = cheque;

            dataSet.SpendP.AddSpendPRow(spend);

            var rpt = new ReportDataSource("SpendP", (DataTable)dataSet.SpendP);

            this.CallReport(rpt, "Reports/Spend/SpendP.rdlc", "Reconocimiento Obligaciones");
        }

        private void SpendAnnexP()
        {
            var documentId = this.Request.QueryString["documentId"];
            var documentIdValue = string.IsNullOrWhiteSpace(documentId) ? (int?)null : Convert.ToInt32(documentId);

            if (documentIdValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var dataTables = this.accountingDocumentsService.GetSpendPById((int)documentIdValue);

            var ministeryName = dataTables.Item2 == null ? string.Empty : dataTables.Item2.MINISTERIO;
            var ministeryCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_MINISTERIO;
            var companyCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_ORGANISMO;
            var university = dataTables.Item2 == null ? string.Empty : dataTables.Item2.NOMBRE_ORGANISMO;
            var nature = dataTables.Item1.TIPD_CLAVE == null ? string.Empty : dataTables.Item1.TIPD_CLAVE.ToString();
            int natureInt = dataTables.Item1.TIPD_CLAVE == null ? 0 : (int)dataTables.Item1.TIPD_CLAVE;
            var exerciseYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("yyyy");
            int proposalYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? 0 : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).Year;
            int budgetYearInt = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? 0 : (short)dataTables.Item1.EXP_ANO_PRESUPUESTO;
            var budgetYear = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? string.Empty : dataTables.Item1.EXP_ANO_PRESUPUESTO.ToString();
            var proposalDate = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("d", new CultureInfo("es-ES"));
            var exercise = GetExercise(proposalYear, budgetYearInt);
            string description = dataTables.Item1.DOC_DESCRIPCION == null ? string.Empty : dataTables.Item1.DOC_DESCRIPCION;
            string groupName = dataTables.Item1.DOC_CODIGO.ToString();

            var fileNumber = dataTables.Item1.EXP_NUM_EXP_CONTABLE_ANUAL == null ? string.Empty : dataTables.Item1.EXP_NUM_EXP_CONTABLE_ANUAL.ToString();
            string natureName = dataTables.Item1.TIPD_NOMBRE_CORTO == null ? string.Empty : dataTables.Item1.TIPD_NOMBRE_CORTO;


            string sign = dataTables.Item1.TIPD_POSITIVO ? "POSITIVO" : "NEGATIVO";
            string signNumber = dataTables.Item1.TIPD_POSITIVO ? "0" : "1";

            if (natureInt == 250 && natureName.Equals("ADOP")) natureName = "ADO";
            if (natureInt == 240 && natureName.Equals("ADOP")) natureName = "ADO";

            string proNumero = dataTables.Item1.PRO_NUMERO == null ? string.Empty : dataTables.Item1.PRO_NUMERO;



            Double total = 0;
            Decimal contador = 0;
            string cacsNumero = string.Empty;
            if (dataTables.Item3.Any())
            {
                foreach (var i in dataTables.Item3)
                {
                    contador += 1;
                    total += Decimal.ToDouble((decimal)i.DOCA_IMPORTE);
                    if (contador == 1) cacsNumero = i.CACS_NUMERO;

                    var spend = dataSet.SpendP.NewSpendPRow();
                    spend.ProNumero = proNumero;
                    spend.Application = i.CACS_NUMERO;
                    spend.Amount = (decimal)i.DOCA_IMPORTE;
                    spend.Account = i.CUEP_NUMERO;
                    spend.TotalAmount = (decimal)total;
                    spend.TotalApplications = contador;
                    spend.Sign = sign;
                    spend.SignNumber = signNumber;
                    spend.BudgetYear = budgetYear;
                    spend.MinisteryName = ministeryName;
                    spend.MinisteryCode = ministeryCode;
                    spend.CompanyCode = companyCode;
                    spend.SchoolName = university;
                    spend.Nature = nature;
                    spend.ExerciseYear = exerciseYear;
                    spend.Exercise = exercise;
                    spend.Description = description;
                    spend.GroupName = groupName;
                    spend.FileNumber = fileNumber;
                    spend.NatureName = natureName;
                    spend.ProposalDate = proposalDate;
                    dataSet.SpendP.AddSpendPRow(spend);
                }
            }
            else
            {
                //no debería pasar por aquí ya que solo se muestra el anexo si hay más de un registro DA
                var spend = dataSet.SpendP.NewSpendPRow();
                spend.Sign = sign;
                spend.SignNumber = signNumber;
                spend.BudgetYear = budgetYear;
                spend.MinisteryName = ministeryName;
                spend.MinisteryCode = ministeryCode;
                spend.CompanyCode = companyCode;
                spend.SchoolName = university;
                spend.Nature = nature;
                spend.ExerciseYear = exerciseYear;
                spend.Exercise = exercise;
                spend.Description = description;
                spend.GroupName = groupName;
                spend.FileNumber = fileNumber;
                spend.NatureName = natureName;
                spend.ProposalDate = proposalDate;
                dataSet.SpendP.AddSpendPRow(spend);
            }

            var rpt = new ReportDataSource("SpendP", (DataTable)dataSet.SpendP);

            this.CallReport(rpt, "Reports/Spend/SpendAnnexP.rdlc", "Reconocimiento Obligaciones");
        }

        private string GetSignLine(Decimal total, int line)
        {
            string sign1 = "LA RECTORA";
            string sign2 = string.Empty;
            string sign3 = string.Empty;
            if (total <= 60000)
            {
                sign1 = "EL GERENTE";
                sign2 = "(P.D. 19-03-2013 BOE. 01-04-2013)";
                sign3 = string.Empty;
            }
            if (line == 1) return sign1;
            if (line == 2) return sign2;
            return sign3;
        }

        private void SpendORecord()
        {
            var documentId = this.Request.QueryString["documentId"];
            var documentIdValue = string.IsNullOrWhiteSpace(documentId) ? (int?)null : Convert.ToInt32(documentId);

            if (documentIdValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var dataTables = this.accountingDocumentsService.GetSpendORecordById((int)documentIdValue);

            var ministeryName = dataTables.Item2 == null ? string.Empty : dataTables.Item2.MINISTERIO;
            var ministeryCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_MINISTERIO;
            var companyCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_ORGANISMO;
            var university = dataTables.Item2 == null ? string.Empty : dataTables.Item2.NOMBRE_ORGANISMO;
            var nature = dataTables.Item1.TIPD_CLAVE == null ? string.Empty : dataTables.Item1.TIPD_CLAVE.ToString();
            int natureInt = dataTables.Item1.TIPD_CLAVE == null ? 0 : (int)dataTables.Item1.TIPD_CLAVE;
            var exerciseYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("yyyy");
            int proposalYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? 0 : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).Year;
            int budgetYearInt = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? 0 : (short)dataTables.Item1.EXP_ANO_PRESUPUESTO;
            var budgetYear = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? string.Empty : dataTables.Item1.EXP_ANO_PRESUPUESTO.ToString();
            var proposalDate = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("d", new CultureInfo("es-ES"));
            var exercise = GetExercise(proposalYear, budgetYearInt);
            string description = dataTables.Item1.DOC_DESCRIPCION == null ? string.Empty : dataTables.Item1.DOC_DESCRIPCION;
            string groupName = dataTables.Item1.DOC_CODIGO.ToString();
            var proNif = GetNifOk(dataTables.Item1.PROV_NIF == null ? string.Empty : dataTables.Item1.PROV_NIF);
            var proName = dataTables.Item1.PROV_NOMBRE == null ? string.Empty : dataTables.Item1.PROV_NOMBRE;

            var fileNumber = dataTables.Item1.EXP_NUM_EXP_CONTABLE_ANUAL == null ? string.Empty : dataTables.Item1.EXP_NUM_EXP_CONTABLE_ANUAL.ToString();
            string natureName = dataTables.Item1.TIPD_NOMBRE_CORTO == null ? string.Empty : dataTables.Item1.TIPD_NOMBRE_CORTO;
            string senNumero = dataTables.Item1.SEN_NUMERO == null ? string.Empty : dataTables.Item1.SEN_NUMERO.ToString();
            string cenAreaOrigen = dataTables.Item1.CEN_AREA_ORIGEN == null ? string.Empty : dataTables.Item1.CEN_AREA_ORIGEN.ToString();
            string forCodigo = dataTables.Item1.FOR_CODIGO == null ? string.Empty : dataTables.Item1.FOR_CODIGO.ToString();
            string tippCodigo = dataTables.Item1.TIPP_CODIGO == null ? string.Empty : dataTables.Item1.TIPP_CODIGO.ToString();
            string ordinalPerceiver = dataTables.Item1.CUE_ORDINAL_PERCEPTOR == null ? string.Empty : dataTables.Item1.CUE_ORDINAL_PERCEPTOR.ToString();

            string cheque = dataTables.Item1.DOC_NUMERO_CHEQUE == null ? string.Empty : "T. Bco. nº " + dataTables.Item1.DOC_NUMERO_CHEQUE;

            string bookEntryDate = dataTables.Item1.DOC_FECHA_ASIENTO_DIARIO == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_ASIENTO_DIARIO).ToString("d", new CultureInfo("es-ES"));

            string sign = dataTables.Item1.TIPD_POSITIVO ? "POSITIVO" : "NEGATIVO";
            string signNumber = dataTables.Item1.TIPD_POSITIVO ? "0" : "1";

            string text = string.Empty;
            if (natureName.Equals("OP")) text = "Obligaciones reconocidas.";
            if (natureName.Equals("P")) text = "Ordenación del pago.";
            if (natureName.Equals("ADOP"))
            {
                if (natureInt == 240) text = "Autorización, compromiso y reconocimiento de obligaciones sobre crédito disponible.";
                if (natureInt == 250) text = "Autorización, compromiso y reconocimiento de obligaciones sobre crédito retenido.";
            }
            if (natureName.Equals("O"))
            {
                if (natureInt == 400) text = "Reconocimiento de obligaciones.";
                if (natureInt == 700) text = "Rectificación de operaciones pagadas. Reconocimiento de obligaciones.";
            }


            string proNumero = dataTables.Item1.PRO_NUMERO == null ? string.Empty : dataTables.Item1.PRO_NUMERO;

            Decimal total = 0;
            Decimal contador = 0;
            string cacsNumero = string.Empty;
            string cuepNumero = string.Empty;
            if (dataTables.Item3.Any())
            {
                foreach (var i in dataTables.Item3)
                {
                    contador += 1;
                    total += (decimal)i.DOCA_IMPORTE;
                    if (contador == 1) cacsNumero = i.CACS_NUMERO;
                    if (contador == 1) cuepNumero = i.CUEP_NUMERO;
                }
            }

            if (dataTables.Item4.Any())
            {
                var spend = dataSet.SpendORecord.NewSpendORecordRow();

                spend.TotalAmountToWord = GetAmountToWords(total);
                spend.TotalAmount = (decimal)total;
                spend.TotalApplications = contador;
                spend.Sign = sign;
                spend.SignNumber = signNumber;
                spend.Text = text;
                spend.DetEconomica = contador == 1 ? cacsNumero : string.Empty;
                spend.DetFuncional = contador == 1 ? proNumero : string.Empty;
                spend.BudgetYear = budgetYear;
                spend.Sign1 = GetSignLine(total, 1);
                spend.Sign2 = GetSignLine(total, 2);
                spend.Sign3 = GetSignLine(total, 3);
                spend.CtaPGCP = contador == 1 ? cuepNumero : string.Empty;

                spend.MinisteryName = ministeryName;
                spend.MinisteryCode = ministeryCode;
                spend.CompanyCode = companyCode;
                spend.SchoolName = university;
                spend.Nature = nature;
                spend.ExerciseYear = exerciseYear;
                spend.Exercise = exercise;
                spend.Description = description;
                spend.GroupName = groupName;
                spend.FileNumber = fileNumber;
                spend.NatureName = natureName;
                spend.ProposalDate = proposalDate;
                spend.OrdinalPerceiver = ordinalPerceiver;
                spend.TippCodigo = tippCodigo;
                spend.ForCodigo = forCodigo;
                spend.CenAreaOrigen = cenAreaOrigen;
                spend.SenNumero = senNumero;
                spend.ProviderNif = proNif;
                spend.ProviderName = proName;
                spend.Cheque = cheque;
                spend.BookEntryDate = bookEntryDate;

                dataSet.SpendORecord.AddSpendORecordRow(spend);
            }
            else
            {
                Decimal totalDescuento = 0;
                int numRows = dataTables.Item4.Count();
                int counter = 0;

                foreach (var extraBudgetary in dataTables.Item4)
                {
                    counter++;
                    var spend = dataSet.SpendORecord.NewSpendORecordRow();

                    spend.DiscountAmount = extraBudgetary.EXP_EXTRAP_IMPORTE;
                    spend.DiscountNumber = extraBudgetary.EXTRAPRE_NUMERO != null? extraBudgetary.EXTRAPRE_NUMERO.ToString() : string.Empty;
                    spend.DiscountDescription = extraBudgetary.EXTRAPRE_DESCRIPCION != null? extraBudgetary.EXTRAPRE_DESCRIPCION : string.Empty;

                    totalDescuento = totalDescuento + spend.DiscountAmount;

                    if (counter == numRows) //Last Row
                    {
                        spend.DiscountTotalAmount = totalDescuento;
                        spend.LiquidTotalAmount = Convert.ToDecimal(total) - totalDescuento;
                    }

                    if (counter == 1) //First Row
                    {
                        spend.TotalAmountToWord = GetAmountToWords(total);
                        spend.TotalAmount = (decimal)total;
                        spend.TotalApplications = contador;
                        spend.Sign = sign;
                        spend.SignNumber = signNumber;
                        spend.Text = text;
                        spend.DetEconomica = contador == 1 ? cacsNumero : string.Empty;
                        spend.DetFuncional = contador == 1 ? proNumero : string.Empty;
                        spend.BudgetYear = budgetYear;
                        spend.Sign1 = GetSignLine(total, 1);
                        spend.Sign2 = GetSignLine(total, 2);
                        spend.Sign3 = GetSignLine(total, 3);
                        spend.CtaPGCP = contador == 1 ? cuepNumero : string.Empty;

                        spend.MinisteryName = ministeryName;
                        spend.MinisteryCode = ministeryCode;
                        spend.CompanyCode = companyCode;
                        spend.SchoolName = university;
                        spend.Nature = nature;
                        spend.ExerciseYear = exerciseYear;
                        spend.Exercise = exercise;
                        spend.Description = description;
                        spend.GroupName = groupName;
                        spend.FileNumber = fileNumber;
                        spend.NatureName = natureName;
                        spend.ProposalDate = proposalDate;
                        spend.OrdinalPerceiver = ordinalPerceiver;
                        spend.TippCodigo = tippCodigo;
                        spend.ForCodigo = forCodigo;
                        spend.CenAreaOrigen = cenAreaOrigen;
                        spend.SenNumero = senNumero;
                        spend.ProviderNif = proNif;
                        spend.ProviderName = proName;
                        spend.Cheque = cheque;
                        spend.BookEntryDate = bookEntryDate;
                    }

                    dataSet.SpendORecord.AddSpendORecordRow(spend);
                }
            }

            var rpt = new ReportDataSource("SpendORecord", (DataTable)dataSet.SpendORecord);

            this.CallReport(rpt, "Reports/Spend/SpendORecord.rdlc", "Reconocimiento Obligaciones");
        }

        private void SpendPRecord()
        {
            var documentId = this.Request.QueryString["documentId"];
            var documentIdValue = string.IsNullOrWhiteSpace(documentId) ? (int?)null : Convert.ToInt32(documentId);

            if (documentIdValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var dataTables = this.accountingDocumentsService.GetSpendPRecordById((int)documentIdValue);


            var ministeryName = dataTables.Item2 == null ? string.Empty : dataTables.Item2.MINISTERIO;
            var ministeryCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_MINISTERIO;
            var companyCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_ORGANISMO;
            var university = dataTables.Item2 == null ? string.Empty : dataTables.Item2.NOMBRE_ORGANISMO;
            var nature = dataTables.Item1.TIPD_CLAVE == null ? string.Empty : dataTables.Item1.TIPD_CLAVE.ToString();
            int natureInt = dataTables.Item1.TIPD_CLAVE == null ? 0 : (int)dataTables.Item1.TIPD_CLAVE;
            var exerciseYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("yyyy");
            int proposalYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? 0 : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).Year;
            int budgetYearInt = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? 0 : (short)dataTables.Item1.EXP_ANO_PRESUPUESTO;
            var budgetYear = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? string.Empty : dataTables.Item1.EXP_ANO_PRESUPUESTO.ToString();
            var proposalDate = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("d", new CultureInfo("es-ES"));
            var exercise = GetExercise(proposalYear, budgetYearInt);
            string description = dataTables.Item1.DOC_DESCRIPCION == null ? string.Empty : dataTables.Item1.DOC_DESCRIPCION;
            //string groupName = dataTables.Item1.DOC_CODIGO.ToString();
            var proNif = GetNifOk(dataTables.Item1.PROV_NIF == null ? string.Empty : dataTables.Item1.PROV_NIF.Replace("\r", string.Empty).Replace("\n", string.Empty));
            var proName = dataTables.Item1.PROV_NOMBRE == null ? string.Empty : dataTables.Item1.PROV_NOMBRE.Replace("\r", string.Empty).Replace("\n", string.Empty);

            var fileNumber = dataTables.Item1.EXP_NUM_EXP_CONTABLE_ANUAL == null ? string.Empty : dataTables.Item1.EXP_NUM_EXP_CONTABLE_ANUAL.ToString();
            string natureName = dataTables.Item1.TIPD_NOMBRE_CORTO == null ? string.Empty : dataTables.Item1.TIPD_NOMBRE_CORTO;
            string senNumero = dataTables.Item1.SEN_NUMERO == null ? string.Empty : dataTables.Item1.SEN_NUMERO.ToString();
            string cenAreaOrigen = dataTables.Item1.CEN_AREA_ORIGEN == null ? string.Empty : dataTables.Item1.CEN_AREA_ORIGEN.ToString();
            string forCodigo = dataTables.Item1.FOR_CODIGO == null ? string.Empty : dataTables.Item1.FOR_CODIGO.ToString();
            string tippCodigo = dataTables.Item1.TIPP_CODIGO == null ? string.Empty : dataTables.Item1.TIPP_CODIGO.ToString();
            string ordinalPerceiver = dataTables.Item1.CUE_ORDINAL_PERCEPTOR == null ? string.Empty : dataTables.Item1.CUE_ORDINAL_PERCEPTOR.ToString();

            string cheque = dataTables.Item1.DOC_NUMERO_CHEQUE == null ? string.Empty : "T. Bco. nº " + dataTables.Item1.DOC_NUMERO_CHEQUE;
            string sign = dataTables.Item1.TIPD_POSITIVO ? "POSITIVO" : "NEGATIVO";
            string signNumber = dataTables.Item1.TIPD_POSITIVO ? "0" : "1";

            string text = string.Empty;
            if (natureName.Equals("OP") && natureInt == 410) text = "Reconocimiento de obligaciones.";
            if (natureName.Equals("P") && natureInt == 500) text = "Ordenación del pago.";
            if (natureName.Equals("ADOP"))
            {
                if (natureInt == 240) text = "Autorización, compromiso y reconocimiento de obligaciones sobre crédito disponible.";
                if (natureInt == 250) text = "Autorización, compromiso y reconocimiento de obligaciones y pago sobre crédito retenido.";
                if (natureInt == 740) text = "Rectificacion de operaciones pagadas. Autorización, compromiso y reconocimiento de obligaciones y pago sobre crédito disponible.";
                if (natureInt == 750) text = "Rectificacion de operaciones pagadas. Autorización, compromiso y reconocimiento de obligaciones y pago sobre crédito retenido.";
                if (natureInt == 810) text = "Reintegro de ADOP sobre disponible";
                if (natureInt == 820) text = "Reintegro de ADOP sobre retenido";
            }

            if (natureName.Equals("ADOP") && natureInt == 240) natureName = "ADO";

            string proNumero = dataTables.Item1.PRO_NUMERO == null ? string.Empty : dataTables.Item1.PRO_NUMERO;



            Decimal contador = 1;
            Decimal total = (decimal)dataTables.Item3.DOCA_IMPORTE;
            string cacsNumero = dataTables.Item3.CACS_NUMERO != null ? dataTables.Item3.CACS_NUMERO : string.Empty;
            string cuepNumero = dataTables.Item3.CUEP_NUMERO != null ? dataTables.Item3.CUEP_NUMERO : string.Empty;
            decimal docaImporte = dataTables.Item3.DOCA_IMPORTE != null ? (decimal)dataTables.Item3.DOCA_IMPORTE : 0;

            if (!dataTables.Item4.Any())
            {
                var spend = dataSet.SpendPRecord.NewSpendPRecordRow();

                spend.TotalAmountToWord = GetAmountToWords(total);
                spend.TotalAmount = (decimal)total;
                spend.TotalApplications = contador;
                spend.Sign = sign;
                spend.SignNumber = signNumber;
                spend.Text = text;
                //spend.DetEconomica = contador == 1 ? cacsNumero : string.Empty;
                spend.Application = cacsNumero;
                spend.Amount = docaImporte;
                spend.Account = cuepNumero;
                spend.DetFuncional = proNumero;
                spend.BudgetYear = budgetYear;
                spend.Sign1 = GetSignLine(total, 1);
                spend.Sign2 = GetSignLine(total, 2);
                spend.Sign3 = GetSignLine(total, 3);
                //spend.CtaPGCP = contador == 1 ? cuepNumero : string.Empty;

                spend.MinisteryCode = ministeryCode;
                spend.CompanyCode = companyCode;
                spend.SchoolName = university;
                spend.Nature = nature;
                spend.ExerciseYear = exerciseYear;
                spend.Exercise = exercise;
                spend.Description = description;
                //spend.GroupName = groupName;
                spend.FileNumber = fileNumber;
                spend.NatureName = natureName;
                spend.ProposalDate = proposalDate;
                spend.OrdinalPerceiver = ordinalPerceiver;
                spend.TippCodigo = tippCodigo;
                spend.ForCodigo = forCodigo;
                spend.CenAreaOrigen = cenAreaOrigen;
                spend.ProviderNif = proNif;
                spend.ProviderName = proName;
                spend.Cheque = cheque;
                spend.MinisteryName = ministeryName;
                spend.SenNumero = senNumero;

                dataSet.SpendPRecord.AddSpendPRecordRow(spend);
            }
            else
            {
                Decimal totalDescuento = 0;
                int numRows = dataTables.Item4.Count();
                int counter = 0;

                foreach (var extraBudgetary in dataTables.Item4)
                {
                    counter++;
                    var spend = dataSet.SpendPRecord.NewSpendPRecordRow();

                    spend.DiscountAmount = extraBudgetary.EXP_EXTRAP_IMPORTE != null ? (Decimal)extraBudgetary.EXP_EXTRAP_IMPORTE : 0;
                    spend.DiscountNumber = extraBudgetary.EXTRAPRE_NUMERO != null ? extraBudgetary.EXTRAPRE_NUMERO.ToString() : string.Empty;
                    spend.DiscountDescription = extraBudgetary.EXTRAPRE_DESCRIPCION != null ? extraBudgetary.EXTRAPRE_DESCRIPCION : string.Empty;

                    totalDescuento = totalDescuento + spend.DiscountAmount;

                    if (counter == numRows) //Last Row
                    {
                        spend.DiscountTotalAmount = totalDescuento;
                        spend.LiquidTotalAmount = Convert.ToDecimal(total) - totalDescuento;
                    }

                    if (counter == 1) //First Row
                    {
                        spend.TotalAmountToWord = GetAmountToWords(total);
                        spend.TotalAmount = (decimal)total;
                        spend.TotalApplications = contador;
                        spend.Application = cacsNumero;
                        spend.Amount = docaImporte;
                        spend.Account = cuepNumero;
                        spend.DetFuncional = proNumero;
                        spend.Sign = sign;
                        spend.SignNumber = signNumber;
                        spend.Text = text;
                        //spend.DetEconomica = contador == 1 ? cacsNumero : string.Empty;
                        spend.DetFuncional = contador == 1 ? proNumero : string.Empty;
                        spend.BudgetYear = budgetYear;
                        spend.Sign1 = GetSignLine(total, 1);
                        spend.Sign2 = GetSignLine(total, 2);
                        spend.Sign3 = GetSignLine(total, 3);
                        //spend.CtaPGCP = contador == 1 ? cuepNumero : string.Empty;

                        spend.MinisteryCode = ministeryCode;
                        spend.CompanyCode = companyCode;
                        spend.SchoolName = university;
                        spend.Nature = nature;
                        spend.ExerciseYear = exerciseYear;
                        spend.Exercise = exercise;
                        spend.Description = description;
                        //spend.GroupName = groupName;
                        spend.FileNumber = fileNumber;
                        spend.NatureName = natureName;
                        spend.ProposalDate = proposalDate;
                        spend.OrdinalPerceiver = ordinalPerceiver;
                        spend.TippCodigo = tippCodigo;
                        spend.ForCodigo = forCodigo;
                        spend.CenAreaOrigen = cenAreaOrigen;
                        spend.ProviderNif = proNif;
                        spend.ProviderName = proName;
                        spend.Cheque = cheque;
                        spend.MinisteryName = ministeryName;
                        spend.SenNumero = senNumero;
                    }

                    dataSet.SpendPRecord.AddSpendPRecordRow(spend);
                }
            }

            var rpt = new ReportDataSource("SpendPRecord", (DataTable)dataSet.SpendPRecord);
            this.CallReport(rpt, "Reports/Spend/SpendPRecord.rdlc", "Reconocimiento Obligaciones");
        }

        private void SpendIncomeDiscounts()
        {
            var documentId = this.Request.QueryString["documentId"];
            var documentIdValue = string.IsNullOrWhiteSpace(documentId) ? (int?)null : Convert.ToInt32(documentId);

            if (documentIdValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var dataTables = this.accountingDocumentsService.GetSpendIncomeDiscountsById((int)documentIdValue);

            var ministeryName = dataTables.Item2 == null ? string.Empty : dataTables.Item2.MINISTERIO;
            var ministeryCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_MINISTERIO;
            var companyCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_ORGANISMO;
            var university = dataTables.Item2 == null ? string.Empty : dataTables.Item2.NOMBRE_ORGANISMO;
            var nature = dataTables.Item1.TIPD_CLAVE == null ? string.Empty : dataTables.Item1.TIPD_CLAVE.ToString();
            int natureInt = dataTables.Item1.TIPD_CLAVE == null ? 0 : (int)dataTables.Item1.TIPD_CLAVE;
            string natureName = dataTables.Item1.TIPD_NOMBRE_CORTO == null ? string.Empty : dataTables.Item1.TIPD_NOMBRE_CORTO;
            var exerciseYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("yyyy");
            int proposalYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? 0 : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).Year;
            int budgetYearInt = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? 0 : (short)dataTables.Item1.EXP_ANO_PRESUPUESTO;
            var budgetYear = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? string.Empty : dataTables.Item1.EXP_ANO_PRESUPUESTO.ToString();
            var proposalDate = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("d", new CultureInfo("es-ES"));
            var exercise = GetExercise(proposalYear, budgetYearInt);
            string description = dataTables.Item1.DOC_DESCRIPCION == null ? string.Empty : dataTables.Item1.DOC_DESCRIPCION;
            var proNif = GetNifOk(dataTables.Item1.PROV_NIF == null ? string.Empty : dataTables.Item1.PROV_NIF);
            var proName = dataTables.Item1.PROV_NOMBRE == null ? string.Empty : dataTables.Item1.PROV_NOMBRE;

            var fileNumber = dataTables.Item1.EXP_NUM_EXP_CONTABLE_ANUAL == null ? string.Empty : dataTables.Item1.EXP_NUM_EXP_CONTABLE_ANUAL.ToString();
            string proNumero = dataTables.Item1.PRO_NUMERO == null ? string.Empty : dataTables.Item1.PRO_NUMERO;
            string cenAreaOrigen = dataTables.Item1.CEN_AREA_ORIGEN == null ? string.Empty : dataTables.Item1.CEN_AREA_ORIGEN.ToString();
            string forCodigo = dataTables.Item1.FOR_CODIGO == null ? string.Empty : dataTables.Item1.FOR_CODIGO.ToString();
            string tippCodigo = dataTables.Item1.TIPP_CODIGO == null ? string.Empty : dataTables.Item1.TIPP_CODIGO.ToString();
            string ordinalPerceiver = dataTables.Item1.CUE_ORDINAL_PERCEPTOR == null ? string.Empty : dataTables.Item1.CUE_ORDINAL_PERCEPTOR.ToString();


            string cheque = dataTables.Item1.DOC_NUMERO_CHEQUE == null ? string.Empty : "T. Bco. nº " + dataTables.Item1.DOC_NUMERO_CHEQUE;
            string sign = dataTables.Item1.TIPD_POSITIVO ? "POSITIVO" : "NEGATIVO";
            string signNumber = dataTables.Item1.TIPD_POSITIVO ? "0" : "1";

            string text = string.Empty;
            if (natureName.Equals("O") && natureInt == 400) text = "Reconocimiento de obligaciones.";
            if (natureName.Equals("O") && natureInt == 700) text = "Rectificación de operaciones pagadas. Reconocimiento de obligaciones.";
            if (natureName.Equals("ADO"))
            {
                if (natureInt == 240) text = "Autorización, compromiso y reconocimiento de obligaciones sobre crédito disponible.";
                if (natureInt == 250) text = "Autorización, compromiso y reconocimiento de obligaciones sobre crédito retenido.";
            }

            if (natureName.Equals("OP") && natureInt == 410) text = "Reconocimiento de obligaciones.";
            if (natureName.Equals("P") && natureInt == 500) text = "Ordenación del pago.";
            if (natureName.Equals("ADOP"))
            {
                if (natureInt == 240) text = "Autorización, compromiso y reconocimiento de obligaciones y pago sobre crédito disponible.";
                if (natureInt == 250) text = "Autorización, compromiso y reconocimiento de obligaciones y pago sobre crédito retenido.";
                if (natureInt == 740) text = "Rectificacion de operaciones pagadas. Autorización, compromiso y reconocimiento de obligaciones y pago sobre crédito disponible.";
                if (natureInt == 750) text = "Rectificacion de operaciones pagadas. Autorización, compromiso y reconocimiento de obligaciones y pago sobre crédito retenido.";
                if (natureInt == 810) text = "Reintegro de ADOP sobre disponible";
                if (natureInt == 820) text = "Reintegro de ADOP sobre retenido";
            }

            string cacsNumero = string.Empty;
            string cuepNumero = string.Empty;
            decimal docaImporte =  0;
            Decimal contador = 0;
            Decimal total = 0;
            if (dataTables.Item3.Any())
            {
                foreach (var item in dataTables.Item3)
                {
                    contador++;
                    total = (decimal)item.DOCA_IMPORTE;
                    cacsNumero = item.CACS_NUMERO != null ? item.CACS_NUMERO : string.Empty;
                    cuepNumero = item.CUEP_NUMERO != null ? item.CUEP_NUMERO : string.Empty;
                    docaImporte = item.DOCA_IMPORTE != null ? (decimal) item.DOCA_IMPORTE : 0;
                    break;
                }
            }


            if (!dataTables.Item4.Any())
            {
                var spend = dataSet.SpendIncomeDiscounts.NewSpendIncomeDiscountsRow();

                spend.TotalAmountToWord = GetAmountToWords(total);
                spend.TotalAmount = (decimal)total;
                spend.TotalApplications = contador;
                spend.Sign = sign;
                spend.SignNumber = signNumber;
                spend.Text = text;
                spend.DetEconomica = cacsNumero;
                spend.Amount = docaImporte;
                spend.Account = cuepNumero;
                spend.DetFuncional = proNumero;
                spend.BudgetYear = budgetYear;
                spend.Sign1 = GetSignLine(total, 1);
                spend.Sign2 = GetSignLine(total, 2);
                spend.Sign3 = GetSignLine(total, 3);
                spend.CtaPGCP = contador == 1 ? cuepNumero : string.Empty;

                spend.MinisteryCode = ministeryCode;
                spend.CompanyCode = companyCode;
                spend.SchoolName = university;
                spend.Nature = nature;
                spend.ExerciseYear = exerciseYear;
                spend.Exercise = exercise;
                spend.Description = description;
                spend.FileNumber = fileNumber;
                spend.NatureName = natureName;
                spend.ProposalDate = proposalDate;
                spend.OrdinalPerceiver = ordinalPerceiver;
                spend.TippCodigo = tippCodigo;
                spend.ForCodigo = forCodigo;
                spend.CenAreaOrigen = cenAreaOrigen;
                spend.ProviderNif = proNif;
                spend.ProviderName = proName;
                spend.Cheque = cheque;
                spend.MinisteryName = ministeryName;

                dataSet.SpendIncomeDiscounts.AddSpendIncomeDiscountsRow(spend);
            }
            else
            {
                Decimal totalDescuento = 0;
                int numRows = dataTables.Item4.Count();
                int counter = 0;

                foreach (var extraBudgetary in dataTables.Item4)
                {
                    counter++;
                    var spend = dataSet.SpendIncomeDiscounts.NewSpendIncomeDiscountsRow();

                    spend.DiscountAmount = (Decimal)extraBudgetary.DOCA_IMPORTE;
                    spend.DiscountCode = extraBudgetary.CACS_NUMERO != null ? extraBudgetary.CACS_NUMERO.ToString() : string.Empty;
                    spend.DiscountDescription = extraBudgetary.EXP_DESCRIPCION != null ? extraBudgetary.EXP_DESCRIPCION : string.Empty;

                    totalDescuento = totalDescuento + spend.DiscountAmount;

                    if (counter == numRows) //Last Row
                    {
                        spend.TotalDiscountAmount = totalDescuento;
                        spend.TotalLiquidAmount = Convert.ToDecimal(total) - totalDescuento;
                    }

                    if (counter == 1) //First Row
                    {
                        spend.TotalAmountToWord = GetAmountToWords(total);
                        spend.TotalAmount = (decimal)total;
                        spend.TotalApplications = contador;
                        spend.Amount = docaImporte;
                        spend.Account = cuepNumero;
                        spend.DetEconomica = cacsNumero;
                        spend.DetFuncional = proNumero;
                        spend.Sign = sign;
                        spend.SignNumber = signNumber;
                        spend.Text = text;
                        spend.DetEconomica = cacsNumero;
                        spend.DetFuncional = proNumero;
                        spend.BudgetYear = budgetYear;
                        spend.Sign1 = GetSignLine(total, 1);
                        spend.Sign2 = GetSignLine(total, 2);
                        spend.Sign3 = GetSignLine(total, 3);
                        spend.CtaPGCP = contador == 1 ? cuepNumero : string.Empty;

                        spend.MinisteryCode = ministeryCode;
                        spend.CompanyCode = companyCode;
                        spend.SchoolName = university;
                        spend.Nature = nature;
                        spend.ExerciseYear = exerciseYear;
                        spend.Exercise = exercise;
                        spend.Description = description;
                        spend.FileNumber = fileNumber;
                        spend.NatureName = natureName;
                        spend.ProposalDate = proposalDate;
                        spend.OrdinalPerceiver = ordinalPerceiver;
                        spend.TippCodigo = tippCodigo;
                        spend.ForCodigo = forCodigo;
                        spend.CenAreaOrigen = cenAreaOrigen;
                        spend.ProviderNif = proNif;
                        spend.ProviderName = proName;
                        spend.Cheque = cheque;
                        spend.MinisteryName = ministeryName;
                    }

                    dataSet.SpendIncomeDiscounts.AddSpendIncomeDiscountsRow(spend);
                }
            }

            var rpt = new ReportDataSource("SpendIncomeDiscounts", (DataTable)dataSet.SpendIncomeDiscounts);
            this.CallReport(rpt, "Reports/Spend/SpendIncomeDiscounts.rdlc", "Reconocimiento Obligaciones");
        }

        private void SpendPRecordDiscounts()
        {
            var documentId = this.Request.QueryString["documentId"];
            var documentIdValue = string.IsNullOrWhiteSpace(documentId) ? (int?)null : Convert.ToInt32(documentId);

            if (documentIdValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var dataTables = this.accountingDocumentsService.GetSpendPRecordDiscountsById((int)documentIdValue);

            var ministeryName = dataTables.Item2 == null ? string.Empty : dataTables.Item2.MINISTERIO;
            var ministeryCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_MINISTERIO;
            var companyCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_ORGANISMO;
            var university = dataTables.Item2 == null ? string.Empty : dataTables.Item2.NOMBRE_ORGANISMO;
            var nature = dataTables.Item1.TIPD_CLAVE == null ? string.Empty : dataTables.Item1.TIPD_CLAVE.ToString();
            int natureInt = dataTables.Item1.TIPD_CLAVE == null ? 0 : (int)dataTables.Item1.TIPD_CLAVE;
            string natureName = dataTables.Item1.TIPD_NOMBRE_CORTO == null ? string.Empty : dataTables.Item1.TIPD_NOMBRE_CORTO;
            var exerciseYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("yyyy");
            int proposalYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? 0 : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).Year;
            int budgetYearInt = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? 0 : (short)dataTables.Item1.EXP_ANO_PRESUPUESTO;
            var budgetYear = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? string.Empty : dataTables.Item1.EXP_ANO_PRESUPUESTO.ToString();
            var proposalDate = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("d", new CultureInfo("es-ES"));
            var exercise = GetExercise(proposalYear, budgetYearInt);
            string description = dataTables.Item1.DOC_DESCRIPCION == null ? string.Empty : dataTables.Item1.DOC_DESCRIPCION;
            var proNif = GetNifOk(dataTables.Item1.PROV_NIF == null ? string.Empty : dataTables.Item1.PROV_NIF);
            var proName = dataTables.Item1.PROV_NOMBRE == null ? string.Empty : dataTables.Item1.PROV_NOMBRE;

            var fileNumber = dataTables.Item1.EXP_NUM_EXP_CONTABLE_ANUAL == null ? string.Empty : dataTables.Item1.EXP_NUM_EXP_CONTABLE_ANUAL.ToString();
            string proNumero = dataTables.Item1.PRO_NUMERO == null ? string.Empty : dataTables.Item1.PRO_NUMERO;
            string cenAreaOrigen = dataTables.Item1.CEN_AREA_ORIGEN == null ? string.Empty : dataTables.Item1.CEN_AREA_ORIGEN.ToString();
            string forCodigo = dataTables.Item1.FOR_CODIGO == null ? string.Empty : dataTables.Item1.FOR_CODIGO.ToString();
            string tippCodigo = dataTables.Item1.TIPP_CODIGO == null ? string.Empty : dataTables.Item1.TIPP_CODIGO.ToString();
            string ordinalPerceiver = dataTables.Item1.CUE_ORDINAL_PERCEPTOR == null ? string.Empty : dataTables.Item1.CUE_ORDINAL_PERCEPTOR.ToString();


            string cheque = dataTables.Item1.DOC_NUMERO_CHEQUE == null ? string.Empty : "T. Bco. nº " + dataTables.Item1.DOC_NUMERO_CHEQUE;
            string sign = dataTables.Item1.TIPD_POSITIVO ? "POSITIVO" : "NEGATIVO";
            string signNumber = dataTables.Item1.TIPD_POSITIVO ? "0" : "1";

            string senNumero = dataTables.Item1.SEN_NUMERO == null ? string.Empty : dataTables.Item1.SEN_NUMERO.ToString();

            string text = string.Empty;

            if (natureName.Equals("OP") && natureInt == 410) text = "Reconocimiento de obligaciones.";
            if (natureName.Equals("P") && natureInt == 500) text = "Ordenación del pago.";
            if (natureName.Equals("ADOP"))
            {
                if (natureInt == 240) text = "Autorización, compromiso y reconocimiento de obligaciones sobre crédito disponible.";
                if (natureInt == 250) text = "Autorización, compromiso y reconocimiento de obligaciones y pago sobre crédito retenido.";
                if (natureInt == 740) text = "Rectificacion de operaciones pagadas. Autorización, compromiso y reconocimiento de obligaciones y pago sobre crédito disponible.";
                if (natureInt == 750) text = "Rectificacion de operaciones pagadas. Autorización, compromiso y reconocimiento de obligaciones y pago sobre crédito retenido.";
                if (natureInt == 810) text = "Reintegro de ADOP sobre disponible";
                if (natureInt == 820) text = "Reintegro de ADOP sobre retenido";
            }


            if (natureName.Equals("ADOP") && natureInt == 240) natureName = "ADO";
            if (natureName.Equals("ADOP") && natureInt == 250) natureName = "ADO";


            Decimal contador = 1;
            Decimal total = (decimal)dataTables.Item3.DOCA_IMPORTE;
            string cacsNumero = dataTables.Item3.CACS_NUMERO != null ? dataTables.Item3.CACS_NUMERO : string.Empty;
            string cuepNumero = dataTables.Item3.CUEP_NUMERO != null ? dataTables.Item3.CUEP_NUMERO : string.Empty;
            decimal docaImporte = dataTables.Item3.DOCA_IMPORTE != null ? (decimal)dataTables.Item3.DOCA_IMPORTE : 0;

            if (!dataTables.Item4.Any())
            {
                var spend = dataSet.SpendPRecordDiscounts.NewSpendPRecordDiscountsRow();

                spend.TotalAmountToWord = GetAmountToWords(total);
                spend.TotalAmount = (decimal)total;
                spend.TotalApplications = contador;
                spend.Sign = sign;
                spend.SignNumber = signNumber;
                spend.Text = text;
                spend.DetEconomica = cacsNumero;
                spend.Amount = docaImporte;
                spend.Account = cuepNumero;
                spend.DetFuncional = proNumero;
                spend.BudgetYear = budgetYear;
                spend.Sign1 = GetSignLine(total, 1);
                spend.Sign2 = GetSignLine(total, 2);
                spend.Sign3 = GetSignLine(total, 3);
                spend.CtaPGCP = contador == 1 ? cuepNumero : string.Empty;

                spend.MinisteryCode = ministeryCode;
                spend.CompanyCode = companyCode;
                spend.SchoolName = university;
                spend.Nature = nature;
                spend.ExerciseYear = exerciseYear;
                spend.Exercise = exercise;
                spend.Description = description;
                spend.FileNumber = fileNumber;
                spend.NatureName = natureName;
                spend.ProposalDate = proposalDate;
                spend.OrdinalPerceiver = ordinalPerceiver;
                spend.TippCodigo = tippCodigo;
                spend.ForCodigo = forCodigo;
                spend.CenAreaOrigen = cenAreaOrigen;
                spend.ProviderNif = proNif;
                spend.ProviderName = proName;
                spend.Cheque = cheque;
                spend.MinisteryName = ministeryName;
                spend.SenNumero = senNumero;

                dataSet.SpendPRecordDiscounts.AddSpendPRecordDiscountsRow(spend);
            }
            else
            {
                Decimal totalDescuento = 0;
                int numRows = dataTables.Item4.Count();
                int counter = 0;

                foreach (var extra in dataTables.Item4)
                {
                    counter++;
                    var spend = dataSet.SpendPRecordDiscounts.NewSpendPRecordDiscountsRow();

                    spend.DiscountAmount = (Decimal)extra.IMPORTE_DESCUENTO;
                    spend.DiscountCode = extra.NUMERO_APLICACION != null ? extra.NUMERO_APLICACION.ToString() : string.Empty;
                    spend.DiscountDescription = extra.NOMBRE_APLICACION != null ? extra.NOMBRE_APLICACION : string.Empty;
                    spend.DiscountCtaPGCP = extra.CUENTA_PGCP != null ? extra.CUENTA_PGCP : string.Empty;

                    totalDescuento = totalDescuento + spend.DiscountAmount;

                    if (counter == numRows) //Last Row
                    {
                        spend.TotalDiscountAmount = totalDescuento;
                        spend.TotalLiquidAmount = Convert.ToDecimal(total) - totalDescuento;
                    }

                    if (counter == 1) //First Row
                    {
                        spend.TotalAmountToWord = GetAmountToWords(total);
                        spend.TotalAmount = (decimal)total;
                        spend.TotalApplications = contador;
                        spend.Amount = docaImporte;
                        spend.Account = cuepNumero;
                        spend.DetFuncional = proNumero;
                        spend.Sign = sign;
                        spend.SignNumber = signNumber;
                        spend.Text = text;
                        spend.DetEconomica = cacsNumero;
                        spend.DetFuncional = proNumero;
                        spend.BudgetYear = budgetYear;
                        spend.Sign1 = GetSignLine(total, 1);
                        spend.Sign2 = GetSignLine(total, 2);
                        spend.Sign3 = GetSignLine(total, 3);
                        spend.CtaPGCP = contador == 1 ? cuepNumero : string.Empty;

                        spend.MinisteryCode = ministeryCode;
                        spend.CompanyCode = companyCode;
                        spend.SchoolName = university;
                        spend.Nature = nature;
                        spend.ExerciseYear = exerciseYear;
                        spend.Exercise = exercise;
                        spend.Description = description;
                        spend.FileNumber = fileNumber;
                        spend.NatureName = natureName;
                        spend.ProposalDate = proposalDate;
                        spend.OrdinalPerceiver = ordinalPerceiver;
                        spend.TippCodigo = tippCodigo;
                        spend.ForCodigo = forCodigo;
                        spend.CenAreaOrigen = cenAreaOrigen;
                        spend.ProviderNif = proNif;
                        spend.ProviderName = proName;
                        spend.Cheque = cheque;
                        spend.MinisteryName = ministeryName;
                        spend.SenNumero = senNumero;
                    }

                    dataSet.SpendPRecordDiscounts.AddSpendPRecordDiscountsRow(spend);
                }
            }


            var rpt = new ReportDataSource("SpendPRecordDiscounts", (DataTable)dataSet.SpendPRecordDiscounts);
            this.CallReport(rpt, "Reports/Spend/SpendPRecordDiscounts.rdlc", "Reconocimiento Obligaciones");
        }

        private void CallReport(ReportDataSource rpt, string path, String displayName)
        {
            this.ReportViewer.LocalReport.ReportPath = path;
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = displayName;
            this.ReportViewer.LocalReport.Refresh();
        }

        private void IncomeDr()
        {
            var budgetYear = this.Request.QueryString["budgetYear"];
            var budgetYearValue = string.IsNullOrWhiteSpace(budgetYear) ? (int?)null : Convert.ToInt32(budgetYear);

            var exerciseYear = this.Request.QueryString["exerciseYear"];
            var exerciseYearValue = string.IsNullOrWhiteSpace(exerciseYear) ? (int?)null : Convert.ToInt32(exerciseYear);

            var groupNumber = this.Request.QueryString["groupNumber"];
            var groupNumberValue = string.IsNullOrWhiteSpace(groupNumber) ? (int?)null : Convert.ToInt32(groupNumber);

            var documentCode = this.Request.QueryString["documentCode"];
            var documentCodeValue = string.IsNullOrWhiteSpace(documentCode) ? (int?)null : Convert.ToInt32(documentCode);

            var dataSet = new ReportsDataSet();

            var dataTables = this.accountingRecordsService.GetIncomesDrReport(budgetYearValue, exerciseYearValue, groupNumberValue, documentCodeValue);

            var ministeryCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_MINISTERIO;
            var companyCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_ORGANISMO;
            var university = dataTables.Item2 == null ? string.Empty : dataTables.Item2.NOMBRE_ORGANISMO;

            foreach (var i in dataTables.Item1.OrderBy(i => i.DOC_FECHA_MOVIMIENTO_I))
            {
                var income = dataSet.IncomeDr.NewIncomeDrRow();

                income.MinisteryCode = ministeryCode;
                income.CompanyCode = companyCode;
                income.SchoolName = university;
                income.MovNumber = i.DOC_NUMERO_MOVIMIENTO_I == null ? string.Empty : i.DOC_NUMERO_MOVIMIENTO_I.ToString();
                income.PhaseCode = i.TIPD_CLAVE == null ? string.Empty : i.TIPD_CLAVE.ToString();
                income.DocsNumber = dataTables.Item1.Count.ToString();
                income.Date = i.DOC_FECHA_MOVIMIENTO_I == null ? string.Empty : ((DateTime)i.DOC_FECHA_MOVIMIENTO_I).ToString("d", new CultureInfo("es-ES"));
                income.ExerciseYear = i.EXP_ANO_PRESUPUESTO == null ? string.Empty : i.EXP_ANO_PRESUPUESTO.ToString();
                income.BudgetYear = i.DOCA_ANO_PRESUPUESTO == null ? string.Empty : i.DOCA_ANO_PRESUPUESTO.ToString();
                income.Application = i.CACS_NUMERO;
                income.Amount = i.DOCA_IMPORTE;
                income.ProposalDate = i.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)i.DOC_FECHA_PROPUESTA).ToString("d", new CultureInfo("es-ES"));
                income.EfectiveDate = i.DOC_FECHA_ASIENTO_DIARIO == null ? string.Empty : ((DateTime)i.DOC_FECHA_ASIENTO_DIARIO).ToString("d", new CultureInfo("es-ES"));

                dataSet.IncomeDr.AddIncomeDrRow(income);
            }

            var rpt = new ReportDataSource("IncomeDr", (DataTable)dataSet.IncomeDr);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Income/AccountingDocDR.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Resumen Contable";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void IncomeMi()
        {
            var documentId = this.Request.QueryString["documentId"];
            var documentIdValue = string.IsNullOrWhiteSpace(documentId) ? (int?)null : Convert.ToInt32(documentId);

            if (documentIdValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var dataTables = this.accountingDocumentsService.GetByIdReport((int)documentIdValue);

            var ministeryCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_MINISTERIO;
            var companyCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_ORGANISMO;
            var university = dataTables.Item2 == null ? string.Empty : dataTables.Item2.NOMBRE_ORGANISMO;
            var ministeryName = dataTables.Item2 == null ? string.Empty : dataTables.Item2.MINISTERIO;

            var letterPay = dataTables.Item1.HOJ_NUMERO == null ? string.Empty : dataTables.Item1.HOJ_NUMERO.ToString();
            var nature = dataTables.Item1.TIPD_CLAVE == null ? string.Empty : dataTables.Item1.TIPD_CLAVE.ToString();
            var movNumber = dataTables.Item1.DOC_NUMERO_MOVIMIENTO_I == null ? string.Empty : dataTables.Item1.DOC_NUMERO_MOVIMIENTO_I.ToString();
            //var exerciseYear = dataTables.Item1.DOC_FECHA_MOVIMIENTO_I == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_MOVIMIENTO_I).ToString("yyyy");
            var exerciseYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("yyyy");
            var budgetYear = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? string.Empty : dataTables.Item1.EXP_ANO_PRESUPUESTO.ToString();
            var proNif = GetNifOk(dataTables.Item1.PROV_NIF == null ? string.Empty : dataTables.Item1.PROV_NIF);
            var proName = dataTables.Item1.PROV_NOMBRE == null ? string.Empty : dataTables.Item1.PROV_NOMBRE;
            var proAddress = dataTables.Item1.PROV_DIRECCION == null ? string.Empty : dataTables.Item1.PROV_DIRECCION;
            var proPop = dataTables.Item1.PROV_POBLACION == null ? string.Empty : dataTables.Item1.PROV_POBLACION;
            var proCodPos = dataTables.Item1.PROV_CODIGO_POSTAL == null ? string.Empty : dataTables.Item1.PROV_CODIGO_POSTAL;
            var proOrigin = dataTables.Item1.CEN_AREA_ORIGEN == null ? string.Empty : dataTables.Item1.CEN_AREA_ORIGEN;
            var proposalDate = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("d", new CultureInfo("es-ES"));
            var effectiveDate = dataTables.Item1.DOC_FECHA_ASIENTO_DIARIO == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_ASIENTO_DIARIO).ToString("d", new CultureInfo("es-ES"));
            var date = dataTables.Item1.DOC_FECHA_MOVIMIENTO_I == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_MOVIMIENTO_I).ToString("d", new CultureInfo("es-ES"));

            int proposalYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? 0: ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).Year; 
            int budgetYearInt = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? 0 : (short) dataTables.Item1.EXP_ANO_PRESUPUESTO ;
            var exercise = GetExercise(proposalYear, budgetYearInt);
            string description = dataTables.Item1.DOC_DESCRIPCION == null ? string.Empty : dataTables.Item1.DOC_DESCRIPCION;

            if (dataTables.Item3.Any())
            {
                foreach (var i in dataTables.Item3)
                {
                    var income = dataSet.IncomeMi.NewIncomeMiRow();

                    income.MinisteryCode = ministeryCode;
                    income.CompanyCode = companyCode;
                    income.SchoolName = university;
                    income.MinisteryName = ministeryName;
                    income.LetterPay = letterPay;
                    income.NatureIncome = nature;
                    income.MovNumber = movNumber;
                    income.ExerciseYear = exerciseYear;
                    income.BudgetYear = budgetYear;
                    income.Application = "1 "+i.CACS_NUMERO;
                    income.Amount = (decimal)i.DOCA_IMPORTE;
                    income.Account = i.CUEP_NUMERO;
                    income.ProviderNif = proNif;
                    income.ProviderName = proName;
                    income.ProviderAddress = proAddress;
                    income.ProviderPopulation = proPop;
                    income.ProviderCodPos = proCodPos;
                    income.ProviderOrigin = proOrigin;
                    income.ProposalDate = proposalDate;
                    income.EffectiveDate = effectiveDate;
                    income.Date = date;
                    income.Exercise = exercise;
                    income.Description = description;

                    dataSet.IncomeMi.AddIncomeMiRow(income);
                }
            }
            else
            {
                var income = dataSet.IncomeMi.NewIncomeMiRow();

                income.MinisteryCode = ministeryCode;
                income.CompanyCode = companyCode;
                income.SchoolName = university;
                income.MinisteryName = ministeryName;
                income.LetterPay = letterPay;
                income.NatureIncome = nature;
                income.MovNumber = movNumber;
                income.ExerciseYear = exerciseYear;
                income.BudgetYear = budgetYear;
                income.ProviderNif = proNif;
                income.ProviderName = proName;
                income.ProviderAddress = proAddress;
                income.ProviderPopulation = proPop;
                income.ProviderCodPos = proCodPos;
                income.ProviderOrigin = proOrigin;
                income.ProposalDate = proposalDate;
                income.EffectiveDate = effectiveDate;
                income.Date = date;
                income.Exercise = exercise;
                income.Description = description;

                dataSet.IncomeMi.AddIncomeMiRow(income);
            }

            var rpt = new ReportDataSource("IncomeMi", (DataTable)dataSet.IncomeMi);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Income/AccountingDocMI.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Documento Contable";
            this.ReportViewer.LocalReport.Refresh();
        }

        private string GetExercise(int proposalYear, int budgetYear)
        {
            var exercise = "EJERCICIO CORRIENTE";
            if (proposalYear > budgetYear) exercise = "EJERCICIO ANTERIOR";
            if (proposalYear < budgetYear) exercise = "EJERCICIO POSTERIOR";
            return exercise;
        }

        private string GetNifOk(string nif)
        {
            if (!String.IsNullOrEmpty(nif))
            {
                if ("-".Equals(nif.Substring(0, 1)))
                {
                    return nif.Substring(1);
                }
                else if ("-".Equals(nif.Substring(nif.Length - 1, 1)))
                {
                    return nif.Substring(0, nif.Length - 1);
                }
            }
            return nif;
        }

        private string GetAmountToWords(Decimal total)
        {
            var naturalTotal = Convert.ToDouble(total.ToString("N"));
            var integerPart = (int)total;
            var decimalPart = (int)(Math.Round(naturalTotal - (int)naturalTotal, 2) * 100);

            var integerLetters = $"{integerPart.ToWords(new CultureInfo("es-ES"))}";
            var decimalLetters = decimalPart == 1 ? "un céntimo" : $"{decimalPart.ToWords(new CultureInfo("es-ES"))} céntimos";

            if (decimalPart != 0)
            {
                integerLetters = $"{integerLetters} con {decimalLetters}";
            }

            return integerLetters.ToUpperInvariant();
        }

        private void IncomePmp()
        {
            var documentId = this.Request.QueryString["documentId"];
            var documentIdValue = string.IsNullOrWhiteSpace(documentId) ? (int?)null : Convert.ToInt32(documentId);

            if (documentIdValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var dataTables = this.accountingDocumentsService.GetByIdReport((int)documentIdValue);

            var ministeryCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_MINISTERIO;
            var companyCode = dataTables.Item2 == null ? string.Empty : dataTables.Item2.CODIGO_ORGANISMO;
            var university = dataTables.Item2 == null ? string.Empty : dataTables.Item2.NOMBRE_ORGANISMO;
            var nature = dataTables.Item1.TIPD_CLAVE == null ? string.Empty : dataTables.Item1.TIPD_CLAVE.ToString();
            var exerciseYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("yyyy");
            var proNif = GetNifOk(dataTables.Item1.PROV_NIF == null ? string.Empty : dataTables.Item1.PROV_NIF);
            var proName = dataTables.Item1.PROV_NOMBRE == null ? string.Empty : dataTables.Item1.PROV_NOMBRE;
            int proposalYear = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? 0 : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).Year;
            int budgetYearInt = dataTables.Item1.EXP_ANO_PRESUPUESTO == null ? 0 : (short)dataTables.Item1.EXP_ANO_PRESUPUESTO;
            var proposalDate = dataTables.Item1.DOC_FECHA_PROPUESTA == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_PROPUESTA).ToString("d", new CultureInfo("es-ES"));
            var exercise = GetExercise(proposalYear, budgetYearInt);
            string description = dataTables.Item1.DOC_DESCRIPCION == null ? string.Empty : dataTables.Item1.DOC_DESCRIPCION;
            string groupName = dataTables.Item1.DOC_CODIGO.ToString();

            var movNumber = dataTables.Item1.DOC_NUMERO_MOVIMIENTO_I == null ? string.Empty : dataTables.Item1.DOC_NUMERO_MOVIMIENTO_I.ToString();
            var movYear = dataTables.Item1.DOC_FECHA_MOVIMIENTO_I == null ? string.Empty : ((DateTime)dataTables.Item1.DOC_FECHA_MOVIMIENTO_I).Year.ToString();
            string fileNumber = movNumber + " - " + movYear;
            string code = dataTables.Item1.CEN_CODIGO == null ? string.Empty : dataTables.Item1.CEN_CODIGO.ToString();
            string codeName = dataTables.Item1.CEN_DESCRIPCION == null ? string.Empty : dataTables.Item1.CEN_DESCRIPCION.ToString();
            string natureIncomeName = dataTables.Item1.TIPD_NOMBRE_CORTO == null ? string.Empty : dataTables.Item1.TIPD_NOMBRE_CORTO;
            string paymentCode = dataTables.Item1.TIPP_CODIGO == null ? string.Empty : dataTables.Item1.TIPP_CODIGO.ToString();
            string paymentName = dataTables.Item1.FOR_CODIGO == null ? string.Empty : dataTables.Item1.FOR_CODIGO.ToString();
            string ordinalPerceiver = dataTables.Item1.CUE_ORDINAL_PERCEPTOR == null ? string.Empty : dataTables.Item1.CUE_ORDINAL_PERCEPTOR.ToString();
            string checkNumber = dataTables.Item1.DOC_NUMERO_CHEQUE == null ? string.Empty : dataTables.Item1.DOC_NUMERO_CHEQUE;
            int senNumero = dataTables.Item1.SEN_NUMERO == null ? 0 : (int)dataTables.Item1.SEN_NUMERO;

            if (dataTables.Item3.Any())
            {
                Decimal total = 0;
                int rows = dataTables.Item3.Count();
                int count1 = 0;
                foreach (var i in dataTables.Item3)
                {
                    count1 += 1;
                    var income = dataSet.IncomePMP.NewIncomePMPRow();

                    income.MinisteryCode = ministeryCode;
                    income.CompanyCode = companyCode;
                    income.SchoolName = university;
                    income.NatureIncome = nature;
                    income.ExerciseYear = exerciseYear;
                    income.Application = "1 "+i.CACS_NUMERO;
                    income.Amount = (decimal)i.DOCA_IMPORTE;
                    income.Account = i.CUEP_NUMERO;
                    income.ProviderNif = proNif;
                    income.ProviderName = proName;
                    income.Exercise = exercise;
                    income.Description = description;
                    income.GroupName = groupName;
                    income.FileNumber = fileNumber;
                    income.Code = code;
                    income.CodeName = codeName;
                    income.NatureIncomeName = natureIncomeName;
                    income.PaymentCode = paymentCode;
                    income.PaymentName = paymentName;
                    income.OrdinalPerceiver = ordinalPerceiver;
                    income.CheckNumber = checkNumber;
                    income.ProposalDate = proposalDate;
                    income.SignNumber = senNumero;

                    total += (decimal)i.DOCA_IMPORTE;
                    if(count1 == rows)
                    {
                        income.TotalAmountToWord = GetAmountToWords(total);
                    }

                    dataSet.IncomePMP.AddIncomePMPRow(income);
                }
            }
            else
            {
                var income = dataSet.IncomePMP.NewIncomePMPRow();

                income.MinisteryCode = ministeryCode;
                income.CompanyCode = companyCode;
                income.SchoolName = university;
                income.NatureIncome = nature;
                income.ExerciseYear = exerciseYear;
                income.ProviderNif = proNif;
                income.ProviderName = proName;
                income.Exercise = exercise;
                income.Description = description;
                income.GroupName = groupName;
                income.FileNumber = fileNumber;
                income.Code = code;
                income.CodeName = codeName;
                income.NatureIncomeName = natureIncomeName;
                income.PaymentCode = paymentCode;
                income.PaymentName = paymentName;
                income.OrdinalPerceiver = ordinalPerceiver;
                income.CheckNumber = checkNumber;
                income.TotalAmountToWord = "";
                income.ProposalDate = proposalDate;
                income.SignNumber = senNumero;

                dataSet.IncomePMP.AddIncomePMPRow(income);
            }

            var rpt = new ReportDataSource("IncomePMP", (DataTable)dataSet.IncomePMP);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Income/AccountingDocPMP.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Resumen Contable";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void IncomeAnnexedDr()
        {
            var budgetYear = this.Request.QueryString["budgetYear"];
            var budgetYearValue = string.IsNullOrWhiteSpace(budgetYear) ? (int?)null : Convert.ToInt32(budgetYear);

            var exerciseYear = this.Request.QueryString["exerciseYear"];
            var exerciseYearValue = string.IsNullOrWhiteSpace(exerciseYear) ? (int?)null : Convert.ToInt32(exerciseYear);

            var groupNumber = this.Request.QueryString["groupNumber"];
            var groupNumberValue = string.IsNullOrWhiteSpace(groupNumber) ? (int?)null : Convert.ToInt32(groupNumber);

            var documentCode = this.Request.QueryString["documentCode"];
            var documentCodeValue = string.IsNullOrWhiteSpace(documentCode) ? (int?)null : Convert.ToInt32(documentCode);

            var dataSet = new ReportsDataSet();

            var incomes = this.accountingRecordsService.GetIncomesAnnexedDr(budgetYearValue, exerciseYearValue, groupNumberValue, documentCodeValue);

            foreach (var i in incomes)
            {
                var income = dataSet.AnnexedDr.NewAnnexedDrRow();

                income.GroupDr = groupNumber;
                income.ProviderName = i.PROV_NOMBRE;
                income.Application = i.CACS_NUMERO;
                income.Amount = i.DOCA_IMPORTE;
                income.Description = i.EXP_DESCRIPCION;

                dataSet.AnnexedDr.AddAnnexedDrRow(income);
            }

            var rpt = new ReportDataSource("AnnexedDr", (DataTable)dataSet.AnnexedDr);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Income/IncomeAnnexedDr.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = $"Anexo al DR N⁰ {groupNumberValue}";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void IncomeFiles()
        {
            var title = this.Request.QueryString["title"];
            var titleValue = string.IsNullOrWhiteSpace(title) ? string.Empty : title;

            var order = this.Request.QueryString["order"];
            var orderValue = string.IsNullOrWhiteSpace(order) ? string.Empty : order;

            var budgetYear = this.Request.QueryString["budgetYear"];
            var budgetYearValue = string.IsNullOrWhiteSpace(budgetYear) ? (int?)null : Convert.ToInt32(budgetYear);

            var description = this.Request.QueryString["description"];
            var descriptionValue = string.IsNullOrWhiteSpace(description) ? string.Empty : description;

            var square = this.Request.QueryString["square"];
            var squareValue = string.IsNullOrWhiteSpace(square) ? (bool?)null : square.Equals("1");

            var provider = this.Request.QueryString["provider"];
            var providerValue = string.IsNullOrWhiteSpace(provider) ? (int?)null : Convert.ToInt32(provider);

            var exerciseYear = this.Request.QueryString["exerciseYear"];
            var exerciseYearValue = string.IsNullOrWhiteSpace(exerciseYear) ? (int?)null : Convert.ToInt32(exerciseYear);

            var typeCode = this.Request.QueryString["typeCode"];
            var typeCodeValue = string.IsNullOrWhiteSpace(typeCode) ? string.Empty : typeCode;

            var docNumber = this.Request.QueryString["docNumber"];
            var docNumberValue = string.IsNullOrWhiteSpace(docNumber) ? (int?)null : Convert.ToInt32(docNumber);

            var dataSet = new ReportsDataSet();

            var incomes = this.accountingRecordsService.GetIncomesWithAppAmount(budgetYearValue, descriptionValue, squareValue, providerValue, string.Empty, string.Empty, null, null, null, null, null, null, null, exerciseYearValue, typeCodeValue, docNumberValue, orderValue, string.Empty);

            foreach (var i in incomes)
            {
                var income = dataSet.IncomeFiles.NewIncomeFilesRow();

                income.Title = titleValue;
                income.Year = (int)i.EXP_ANO_PRESUPUESTO;
                income.FileNumber = i.EXP_NUMERO == null ? string.Empty : i.EXP_NUMERO.ToString();
                income.Description = i.EXP_DESCRIPCION;
                income.Provider = i.PROV_NOMBRE;
                income.Application = i.CACS_NUMERO;
                income.DocumentType = i.TIPO_DOC;
                income.Amount = i.IMPORTE;

                dataSet.IncomeFiles.AddIncomeFilesRow(income);
            }

            var rpt = new ReportDataSource("IncomeFiles", (DataTable)dataSet.IncomeFiles);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Income/IncomeFiles.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = titleValue;
            this.ReportViewer.LocalReport.Refresh();
        }

        private void IncomeStatementAccounts()
        {
            var exerciseYear = this.Request.QueryString["exerciseYear"];
            var exerciseYearValue = string.IsNullOrWhiteSpace(exerciseYear) ? (int?)null : Convert.ToInt32(exerciseYear);

            var accounts = this.Request.QueryString["accounts"];
            var accountsValue = string.IsNullOrWhiteSpace(accounts) ? (int?)null : Convert.ToInt32(accounts);

            var accountsLabel = this.Request.QueryString["accountsLabel"];
            var accountsLabelValue = string.IsNullOrWhiteSpace(accountsLabel) ? string.Empty : accountsLabel;

            var budgetYear = this.Request.QueryString["budgetYear"];
            var budgetYearValue = string.IsNullOrWhiteSpace(budgetYear) ? (int?)null : Convert.ToInt32(budgetYear);

            var since = this.Request.QueryString["since"];
            var sinceValue = string.IsNullOrWhiteSpace(since) ? string.Empty : Convert.ToDateTime(since).ToString("yyyy-MM-dd");

            var until = this.Request.QueryString["until"];
            var untilValue = string.IsNullOrWhiteSpace(until) ? string.Empty : Convert.ToDateTime(until).ToString("yyyy-MM-dd");

            if (exerciseYearValue == null || accountsValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var incomes = this.accountRestrictedService.GetStatementAccounts(exerciseYearValue, budgetYearValue, (int)accountsValue, sinceValue, untilValue);

            decimal balance = 0;
            var number = -1;
            foreach (var i in incomes)
            {
                if (i.DET_NUMERO_EXPEDIENTE == null)
                {
                    continue;
                }

                if (number != i.DET_NUMERO_EXPEDIENTE)
                {
                    number = (int)i.DET_NUMERO_EXPEDIENTE;
                    balance = 0;
                }

                var income = dataSet.IncomeStatementAccounts.NewIncomeStatementAccountsRow();

                income.Year = exerciseYearValue.ToString();
                income.Account = accountsLabel;
                income.Date = i.DET_FECHA_APUNTE == null ? string.Empty : ((DateTime)i.DET_FECHA_APUNTE).ToString("d", new CultureInfo("es-ES"));
                income.Number = i.DET_NUMERO_EXPEDIENTE == null ? string.Empty : i.DET_NUMERO_EXPEDIENTE.ToString();
                income.Description = i.DOC_DESCRIPCION;
                income.Balance = 0;

                if ((bool)i.HOJ_ARQUEO50 == false)
                {
                    income.Credit = (decimal)i.DET_IMPORTE;
                    balance += (decimal)i.DET_IMPORTE;
                }
                else
                {
                    income.Debit = (decimal)i.DET_IMPORTE;
                    balance -= (decimal)i.DET_IMPORTE;
                }

                income.Balance = balance;

                dataSet.IncomeStatementAccounts.AddIncomeStatementAccountsRow(income);
            }

            var rpt = new ReportDataSource("IncomeStatementAccounts", (DataTable)dataSet.IncomeStatementAccounts);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Income/RestrictedStatementAccounts.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Estado Cuenta Restringida";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void TonnageSheetDetail()
        {
            var tonnageSheetCode = this.Request.QueryString["tonnageSheetCode"];
            var tonnageSheetCodeValue = string.IsNullOrWhiteSpace(tonnageSheetCode) ? (int?)null : Convert.ToInt32(tonnageSheetCode);

            var sheetNumber = this.Request.QueryString["sheetNumber"];
            var sheetNumberValue = string.IsNullOrWhiteSpace(sheetNumber) ? (int?)null : Convert.ToInt32(sheetNumber);

            var is50 = this.Request.QueryString["is50"];
            var is50Value = string.IsNullOrWhiteSpace(is50) ? (bool?)null : is50.Equals("1")|| is50.Equals("True");

            var date = this.Request.QueryString["date"];
            var dateValue = string.IsNullOrWhiteSpace(date) ? string.Empty : Convert.ToDateTime(date).ToString("yyyy-MM-dd");

            if (tonnageSheetCodeValue == null || sheetNumberValue == null || is50Value == null || string.IsNullOrWhiteSpace(dateValue))
            {
                return;
            }

            var tonnageSheetDetails = this.treasuriesService.GetTonnageSheetDetails((int)tonnageSheetCodeValue);

            var dataSet = new ReportsDataSet();
            if (tonnageSheetDetails.Any())
            {
                foreach (var item in tonnageSheetDetails)
                {
                    var detail = dataSet.TonnageSheetDetails.NewTonnageSheetDetailsRow();
                    detail.BankOrdinal = item.ORDINAL_BANCARIO != null ? item.ORDINAL_BANCARIO : String.Empty;
                    detail.FileNumber = item.DET_NUMERO_EXPEDIENTE != null ? ((int)item.DET_NUMERO_EXPEDIENTE).ToString() : String.Empty;
                    detail.Amount = item.DET_IMPORTE != null ? (decimal)item.DET_IMPORTE : 0;
                    detail.LineNumber = item.LIN_NUMERO != null ? ((int)item.LIN_NUMERO).ToString() : String.Empty;
                    detail.OriginLineDescription =item.LIN_ORIGEN_DESCRIPCION != null ? item.LIN_ORIGEN_DESCRIPCION : String.Empty;
                    detail.LineDescription = item.LIN_DESCRIPCION != null ? item.LIN_DESCRIPCION : String.Empty;
                    detail.SheetNumber = sheetNumber;
                    detail.SheetDate = dateValue;
                    dataSet.TonnageSheetDetails.AddTonnageSheetDetailsRow(detail);
                }
            }
            else
            {
                var detail = dataSet.TonnageSheetDetails.NewTonnageSheetDetailsRow();
                detail.SheetNumber = sheetNumber;
                detail.SheetDate = dateValue;
                dataSet.TonnageSheetDetails.AddTonnageSheetDetailsRow(detail);
            }

            var rpt = new ReportDataSource("TonnageSheetDetails", (DataTable)dataSet.TonnageSheetDetails);

            // si is50Value es true es el reporte cr_hoja_arqueo50 y si es false es el reporte cr_hoja_arqueo
            if ((bool)is50Value)
            {
                this.CallReport(rpt, "Reports/Treasury/TonnageSheetDetail50.rdlc", "Hoja de Arqueo - Salida");
            }
            else
            {
                this.CallReport(rpt, "Reports/Treasury/TonnageSheetDetail.rdlc", "Hoja de Arqueo - Entrada");
            }
        }


        private void TreasuryNotesTreasuries()
        {
            var title = this.Request.QueryString["title"];
            var titleValue = string.IsNullOrWhiteSpace(title) ? string.Empty : title;

            var order = this.Request.QueryString["order"];
            var orderValue = string.IsNullOrWhiteSpace(order) ? string.Empty : order;

            var exerciseYear = this.Request.QueryString["exerciseYear"];
            var exerciseYearValue = string.IsNullOrWhiteSpace(exerciseYear) ? (int?)null : Convert.ToInt32(exerciseYear);

            var originCode = this.Request.QueryString["originCode"];
            var originCodeValue = string.IsNullOrWhiteSpace(originCode) ? (int?)null : Convert.ToInt32(originCode);

            var amount = this.Request.QueryString["amount"];
            var amountValue = string.IsNullOrWhiteSpace(amount) ? (decimal?)null : Convert.ToDecimal(amount);

            var sinceBankDate = this.Request.QueryString["sinceBankDate"];
            var sinceBankDateValue = string.IsNullOrWhiteSpace(sinceBankDate) ? string.Empty : Convert.ToDateTime(sinceBankDate).ToString("yyyy-MM-dd");

            var untilBankDate = this.Request.QueryString["untilBankDate"];
            var untilBankDateValue = string.IsNullOrWhiteSpace(untilBankDate) ? string.Empty : Convert.ToDateTime(untilBankDate).ToString("yyyy-MM-dd");

            var payFormCode = this.Request.QueryString["payFormCode"];
            var payFormCodeValue = string.IsNullOrWhiteSpace(payFormCode) ? (int?)null : Convert.ToInt32(payFormCode);

            var checkNumber = this.Request.QueryString["checkNumber"];
            var checkNumberValue = string.IsNullOrWhiteSpace(checkNumber) ? string.Empty : checkNumber;

            var treasuryHave = this.Request.QueryString["treasuryHave"];
            var treasuryHaveValue = string.IsNullOrWhiteSpace(treasuryHave) ? (bool?)null : treasuryHave.Equals("1");

            var description = this.Request.QueryString["description"];
            var descriptionValue = string.IsNullOrWhiteSpace(description) ? string.Empty : description;

            var sinceDateEntry = this.Request.QueryString["sinceDateEntry"];
            var sinceDateEntryValue = string.IsNullOrWhiteSpace(sinceDateEntry) ? string.Empty : Convert.ToDateTime(sinceDateEntry).ToString("yyyy-MM-dd");
            
            var untilDateEntry = this.Request.QueryString["untilDateEntry"];
            var untilDateEntryValue = string.IsNullOrWhiteSpace(untilDateEntry) ? string.Empty : Convert.ToDateTime(untilDateEntry).ToString("yyyy-MM-dd");

            var registerTypeCode = this.Request.QueryString["registerTypeCode"];
            var registerTypeCodeValue = string.IsNullOrWhiteSpace(registerTypeCode) ? (int?)null : Convert.ToInt32(registerTypeCode);

            var isCanceled = this.Request.QueryString["isCanceled"];
            var isCanceledValue = string.IsNullOrWhiteSpace(isCanceled) ? (bool?)null : isCanceled.Equals("1");

            var isBound = this.Request.QueryString["isBound"];
            var isBoundValue = string.IsNullOrWhiteSpace(isBound) ? (bool?)null : isBound.Equals("1");

            var pendingDate = this.Request.QueryString["pendingDate"];
            var pendingDateValue = string.IsNullOrWhiteSpace(pendingDate) ? string.Empty : Convert.ToDateTime(pendingDate).ToString("yyyy-MM-dd");

            var sinceDocYear = this.Request.QueryString["sinceDocYear"];
            var sinceDocYearValue = string.IsNullOrWhiteSpace(sinceDocYear) ? (int?)null : Convert.ToInt32(sinceDocYear);

            var untilDocYear = this.Request.QueryString["untilDocYear"];
            var untilDocYearValue = string.IsNullOrWhiteSpace(untilDocYear) ? (int?)null : Convert.ToInt32(untilDocYear);

            var dataSet = new ReportsDataSet();

            var treasuries = this.treasuriesService.GetTreasuryByFilters(exerciseYearValue, originCodeValue, amountValue, sinceBankDateValue, untilBankDateValue, payFormCodeValue, checkNumberValue, treasuryHaveValue, descriptionValue, sinceDateEntryValue, untilDateEntryValue, registerTypeCodeValue, isCanceledValue, isBoundValue, pendingDateValue, sinceDocYearValue, untilDocYearValue, orderValue);

            foreach (var t in treasuries)
            {
                var treasury = dataSet.TreasuryNotesTreasuries.NewTreasuryNotesTreasuriesRow();

                treasury.Year = exerciseYearValue == null ? string.Empty : exerciseYearValue.ToString();
                treasury.Title = titleValue;
                treasury.Date = t.TES_FECHA_APUNTE == null ? string.Empty : ((DateTime)t.TES_FECHA_APUNTE).ToString("d", new CultureInfo("es-ES"));
                treasury.CheckNumber = t.TES_NUMERO_CHEQUE;
                treasury.Description = t.TES_DESCRIPCION;
                treasury.Application = t.TES_APLICACION;
                treasury.EffectiveDate = t.TES_FECHA_BANCO == null ? string.Empty : ((DateTime)t.TES_FECHA_BANCO).ToString("d", new CultureInfo("es-ES"));

                treasury.Debit = 0;
                treasury.Credit = 0;

                if (t.TES_HABER != null && (bool)t.TES_HABER == false)
                {
                    treasury.Debit = (decimal)t.TES_TOTAL_IMPORTE_LIQUIDO;
                }
                else
                {
                    treasury.Credit = (decimal)t.TES_TOTAL_IMPORTE_LIQUIDO;
                }

                dataSet.TreasuryNotesTreasuries.AddTreasuryNotesTreasuriesRow(treasury);
            }

            var rpt = new ReportDataSource("TreasuryNotesTreasuries", (DataTable)dataSet.TreasuryNotesTreasuries);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Treasury/TreasuryNotesTreasuries.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = titleValue;
            this.ReportViewer.LocalReport.Refresh();
        }
        

        private void ExtraBudgetariesList()
        {
            var exerciseYear = this.Request.QueryString["exerciseYear"];
            var exerciseYearValue = string.IsNullOrWhiteSpace(exerciseYear) ? (int?)null : Convert.ToInt32(exerciseYear);

            var extraBudgetaryTypes = this.Request.QueryString["extraBudgetaryTypes"];
            var extraBudgetaryTypesValue = string.IsNullOrWhiteSpace(extraBudgetaryTypes) ? (int?)null : extraBudgetaryTypes.Equals("-2") ? 0 : Convert.ToInt32(extraBudgetaryTypes);

            var extraBudgetaryApplications = this.Request.QueryString["extraBudgetaryApplications"];
            var extraBudgetaryApplicationsValue = string.IsNullOrWhiteSpace(extraBudgetaryApplications) ? (int?)null : Convert.ToInt32(extraBudgetaryApplications);

            var isBound = this.Request.QueryString["isBound"];
            var isBoundValue = string.IsNullOrWhiteSpace(isBound) ? (bool?)null : isBound.Equals("1");

            var since = this.Request.QueryString["since"];
            var sinceValue = string.IsNullOrWhiteSpace(since) ? string.Empty : Convert.ToDateTime(since).ToString("yyyy-MM-dd");

            var until = this.Request.QueryString["until"];
            var untilValue = string.IsNullOrWhiteSpace(until) ? string.Empty : Convert.ToDateTime(until).ToString("yyyy-MM-dd");

            var sinceNumber = this.Request.QueryString["sinceNumber"];
            var sinceNumberValue = string.IsNullOrWhiteSpace(sinceNumber) ? (int?)null : Convert.ToInt32(sinceNumber);

            var untilNumber = this.Request.QueryString["untilNumber"];
            var untilNumberValue = string.IsNullOrWhiteSpace(untilNumber) ? (int?)null : Convert.ToInt32(untilNumber);

            var providers = this.Request.QueryString["providers"];
            var providersValue = string.IsNullOrWhiteSpace(providers) ? (int?)null : Convert.ToInt32(providers);

            var dataSet = new ReportsDataSet();

            var treasuries = this.extraBudgetaryRecordsService.GetByFilters(exerciseYearValue, extraBudgetaryTypesValue, extraBudgetaryApplicationsValue, isBoundValue, sinceValue, untilValue, sinceNumberValue, untilNumberValue, providersValue);

            foreach (var t in treasuries.OrderBy(t => t.EXP_EXTRAP_NUMERO))
            {
                if (t.EXP_EXTRAP_NUMERO == 0)
                {
                    continue;
                }

                var treasury = dataSet.ExtraBudgetariesList.NewExtraBudgetariesListRow();

                treasury.FileNumber = t.EXP_EXTRAP_NUMERO == null ? string.Empty : t.EXP_EXTRAP_NUMERO.ToString();
                treasury.Year = t.EXP_EXTRAP_ANO_PRESUPUESTO == null ? string.Empty : t.EXP_EXTRAP_ANO_PRESUPUESTO.ToString();
                treasury.Application = $"{t.EXTRAPRE_NUMERO}  {t.EXTRAPRE_DESCRIPCION}";
                treasury.Date = t.EXP_EXTRAP_FECHA == null ? string.Empty : ((DateTime)t.EXP_EXTRAP_FECHA).ToString("d", new CultureInfo("es-ES"));
                treasury.Amount = (decimal)t.EXP_EXTRAP_IMPORTE;
                treasury.Type = t.TIPO_DOC;
                treasury.Ordinal = t.NUM_ORDINAL_PAGADOR == null ? string.Empty : $"{t.NUM_ORDINAL_PAGADOR}  {t.ORDINAL_PAGADOR}";

                dataSet.ExtraBudgetariesList.AddExtraBudgetariesListRow(treasury);
            }

            var rpt = new ReportDataSource("ExtraBudgetariesList", (DataTable)dataSet.ExtraBudgetariesList);

            this.ReportViewer.LocalReport.ReportPath = "Reports/ExtraBudgetary/ExtraBudgetaryList.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Listado de Expedientes Extrapresupuestarias";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void ExtraBudgetaryCurrent()
        {
            var extraBudgetary = this.Request.QueryString["extraBudgetary"];
            var extraBudgetaryValue = string.IsNullOrWhiteSpace(extraBudgetary) ? (int?)null : Convert.ToInt32(extraBudgetary);

            var type = this.Request.QueryString["type"];
            var typeValue = string.IsNullOrWhiteSpace(type) ? string.Empty : type;

            if (extraBudgetaryValue == null || string.IsNullOrWhiteSpace(typeValue))
            {
                return;
            }

            switch (type)
            {
                case "2":
                    this.ExtraBudgetaryMi((int)extraBudgetaryValue);
                    break;
                case "3":
                    this.ExtraBudgetaryPmp((int)extraBudgetaryValue);
                    break;
            }
        }


        private void ExtraBudgetaryMi(int extraBudgetaryId)
        {
            var dataSet = new ReportsDataSet();

            var dataTables = this.extraBudgetaryRecordsService.GetMi(extraBudgetaryId);

            var ministeryCode = dataTables.Item1 == null ? string.Empty : dataTables.Item1.CODIGO_MINISTERIO;
            var universityCode = dataTables.Item1 == null ? string.Empty : dataTables.Item1.CODIGO_ORGANISMO;
            var ministery = dataTables.Item1 == null ? string.Empty : dataTables.Item1.MINISTERIO;
            var university = dataTables.Item1 == null ? string.Empty : dataTables.Item1.NOMBRE_ORGANISMO;

            var extra = dataSet.ExtraBudgetaryFileMI.NewExtraBudgetaryFileMIRow();
            var item = dataTables.Item2;
                
            extra.MinisteryCode = ministeryCode;
            extra.UniversityName = university;
            extra.MinisteryName = ministery;
            extra.UniversityCode = universityCode;

            extra.BudgetaryYear = item.EXP_EXTRAP_ANO_PRESUPUESTO == null ? string.Empty : item.EXP_EXTRAP_ANO_PRESUPUESTO.ToString();
            extra.Date = item.EXP_EXTRAP_FECHA == null ? string.Empty :((DateTime) item.EXP_EXTRAP_FECHA).ToString("d", new CultureInfo("es-ES"));
            extra.Amount = item.EXP_EXTRAP_IMPORTE == null ? 0 : (decimal) item.EXP_EXTRAP_IMPORTE;
            extra.FreeText = item.EXP_EXTRAP_TEXTO == null ? string.Empty : item.EXP_EXTRAP_TEXTO;
            extra.FreeText = HttpUtility.HtmlDecode(extra.FreeText);
            extra.Number = item.EXP_EXTRAP_NUMERO == null ? string.Empty : item.EXP_EXTRAP_NUMERO.ToString();
            CultureInfo esES = CultureInfo.CreateSpecificCulture("es-ES");
            extra.Concept = item.EXTRAPRE_NUMERO == null ? string.Empty : ((int)item.EXTRAPRE_NUMERO).ToString("##,#", esES);
            extra.ProviderName = item.INTERESADO == null ? string.Empty : item.INTERESADO;
            extra.PaymentLetter = item.HOJ_NUMERO == null ? string.Empty : item.HOJ_NUMERO.ToString();
            DateTime dateT = item.EXP_EXTRAP_FECHA == null ? DateTime.MinValue : (DateTime)item.EXP_EXTRAP_FECHA;
            extra.ExerciseYear = dateT.Year.ToString();
            extra.Exercise = this.GetExercise(dateT.Year, item.EXP_EXTRAP_ANO_PRESUPUESTO == null ? 0 : (int)item.EXP_EXTRAP_ANO_PRESUPUESTO);
            extra.ProviderNif = this.GetNifOk(item.PROV_NIF == null ? string.Empty : item.PROV_NIF);

            dataSet.ExtraBudgetaryFileMI.AddExtraBudgetaryFileMIRow(extra);

            var rpt = new ReportDataSource("ExtraBudgetaryFileMI", (DataTable)dataSet.ExtraBudgetaryFileMI);
            this.CallReport(rpt, "Reports/ExtraBudgetary/ExtraBudgetaryFileMI.rdlc", "Expediente Extrapresupuestario MI");
        }


        private void ExtraBudgetaryPmp(int extraBudgetaryId)
        {
            var dataSet = new ReportsDataSet();

            var dataTables = this.extraBudgetaryRecordsService.GetPmp(extraBudgetaryId);

            var ministeryCode = dataTables.Item2.CODIGO_MINISTERIO == null ? string.Empty : dataTables.Item2.CODIGO_MINISTERIO;
            var universityCode = dataTables.Item2.CODIGO_ORGANISMO == null ? string.Empty : dataTables.Item2.CODIGO_ORGANISMO;
            var ministery = dataTables.Item2.MINISTERIO == null ? string.Empty : dataTables.Item2.MINISTERIO;
            var university = dataTables.Item2.NOMBRE_ORGANISMO == null ? string.Empty : dataTables.Item2.NOMBRE_ORGANISMO;

            var fileNumber = dataTables.Item1.EXP_EXTRAP_ANO_PRESUPUESTO == null ? string.Empty : $"{dataTables.Item1.EXP_EXTRAP_NUMERO} - {dataTables.Item1.EXP_EXTRAP_ANO_PRESUPUESTO.ToString().Substring(2, 2)}";
            var exerciseYear = dataTables.Item1.EXP_EXTRAP_ANO_PRESUPUESTO == null ? string.Empty : dataTables.Item1.EXP_EXTRAP_ANO_PRESUPUESTO.ToString();
            CultureInfo esES = CultureInfo.CreateSpecificCulture("es-ES");
            var concept = dataTables.Item1.EXTRAPRE_NUMERO == null ? string.Empty : ((int)dataTables.Item1.EXTRAPRE_NUMERO).ToString("##,#", esES);
            var description = dataTables.Item1.EXTRAPRE_DESCRIPCION == null ? string.Empty : dataTables.Item1.EXTRAPRE_DESCRIPCION;
            var amount = dataTables.Item1.EXP_EXTRAP_IMPORTE == null ? 0 : (decimal)dataTables.Item1.EXP_EXTRAP_IMPORTE;
            var provNif = dataTables.Item1.PROV_NIF == null ? string.Empty : dataTables.Item1.PROV_NIF;
            provNif = this.GetNifOk(provNif);
            var provName = dataTables.Item1.INTERESADO == null ? string.Empty : dataTables.Item1.INTERESADO;
            var senCode = dataTables.Item1.SEN_NUMERO == null ? string.Empty : dataTables.Item1.SEN_NUMERO.ToString();
            var checkNumber = dataTables.Item1.EXP_EXTRAP_NUMERO_CHEQUE == null ? string.Empty : dataTables.Item1.EXP_EXTRAP_NUMERO_CHEQUE;
            var payType = dataTables.Item1.TIPP_CODIGO == null ? string.Empty : dataTables.Item1.TIPP_CODIGO.ToString();
            var payForm = dataTables.Item1.FOR_CODIGO == null ? string.Empty : dataTables.Item1.FOR_CODIGO.ToString();
            DateTime dateT = dataTables.Item1.EXP_EXTRAP_FECHA == null ? DateTime.Today : (DateTime)dataTables.Item1.EXP_EXTRAP_FECHA;
            var date = dataTables.Item1.EXP_EXTRAP_FECHA == null ? string.Empty : dataTables.Item1.EXP_EXTRAP_FECHA == null ? string.Empty : ((DateTime)dataTables.Item1.EXP_EXTRAP_FECHA).ToString("d", new CultureInfo("es-ES"));

            string freeText = dataTables.Item1.EXP_EXTRAP_TEXTO == null ? string.Empty : dataTables.Item1.EXP_EXTRAP_TEXTO;
            freeText = HttpUtility.HtmlDecode(freeText);
            string ordinalPerceiver = dataTables.Item1.CUE_ORDINAL_PERCEPTOR == null ? string.Empty : ((int)dataTables.Item1.CUE_ORDINAL_PERCEPTOR).ToString();
            string exercise = this.GetExercise(dateT.Year, Int32.Parse(exerciseYear));
            int totalApplications = 1;
            Decimal totalAmount = amount;
            string totalAmountToWord = GetAmountToWords(totalAmount);
            Decimal totalDiscount = 0;

            if (dataTables.Item3.Any())
            {
                int counter = 0;
                foreach (var discount in dataTables.Item3)
                {
                    counter++;

                    var extraBudgetary = dataSet.ExtraBudgetaryFilePMP.NewExtraBudgetaryFilePMPRow();

                    extraBudgetary.DiscountCode = discount.CODIGO_DESCUENTO;
                    extraBudgetary.DiscountDescription = discount.EXTRAPRE_DESCRIPCION;
                    extraBudgetary.DiscountAmount = (decimal)discount.EXP_EXTRAP_IMPORTE;
                    extraBudgetary.DiscountAccount = discount.CUEP_NUMERO;

                    totalDiscount += (decimal)discount.EXP_EXTRAP_IMPORTE;

                    if (counter == dataTables.Item3.Count())
                    {
                        extraBudgetary.TotalDiscountAmount = totalDiscount;
                        extraBudgetary.TotalLiquidAmount = totalAmount - totalDiscount;
                    }

                    if (counter == 1)
                    {
                        extraBudgetary.MinisteryCode = ministeryCode;
                        extraBudgetary.UniversityName = university;
                        extraBudgetary.MinisteryName = ministery;
                        extraBudgetary.UniversityCode = universityCode;

                        extraBudgetary.FileNumber = fileNumber;
                        extraBudgetary.ExerciseYear = exerciseYear;
                        extraBudgetary.Concept = concept;
                        extraBudgetary.Description = description;
                        extraBudgetary.Amount = amount;

                        extraBudgetary.ProviderNif = provNif;
                        extraBudgetary.ProviderName = provName;
                        extraBudgetary.SenCode = senCode;
                        extraBudgetary.CheckNumber = checkNumber;
                        extraBudgetary.PayType = payType;
                        extraBudgetary.PayForm = payForm;
                        extraBudgetary.Date = date;

                        extraBudgetary.FreeText = freeText;
                        extraBudgetary.OrdinalPerceiver = ordinalPerceiver;
                        extraBudgetary.Exercise = exercise;
                        extraBudgetary.TotalApplications = totalApplications;
                        extraBudgetary.TotalAmount = totalAmount;
                        extraBudgetary.TotalAmountToWord = totalAmountToWord;

                    }

                    dataSet.ExtraBudgetaryFilePMP.AddExtraBudgetaryFilePMPRow(extraBudgetary);
                }
            }
            else
            {
                var extraBudgetary = dataSet.ExtraBudgetaryFilePMP.NewExtraBudgetaryFilePMPRow();

                extraBudgetary.MinisteryCode = ministeryCode;
                extraBudgetary.MinisteryName = ministery;
                extraBudgetary.UniversityName = university;
                extraBudgetary.UniversityCode = universityCode;

                extraBudgetary.FileNumber = fileNumber;
                extraBudgetary.ExerciseYear = exerciseYear;
                extraBudgetary.Concept = concept;
                extraBudgetary.Description = description;
                extraBudgetary.Amount = amount;

                extraBudgetary.ProviderNif = provNif;
                extraBudgetary.ProviderName = provName;
                extraBudgetary.SenCode = senCode;
                extraBudgetary.CheckNumber = checkNumber;
                extraBudgetary.PayType = payType;
                extraBudgetary.PayForm = payForm;
                extraBudgetary.Date = date;

                extraBudgetary.FreeText = freeText;
                extraBudgetary.OrdinalPerceiver = ordinalPerceiver;
                extraBudgetary.Exercise = exercise;
                extraBudgetary.TotalApplications = totalApplications;
                extraBudgetary.TotalAmount = totalAmount;
                extraBudgetary.TotalAmountToWord = totalAmountToWord;
                extraBudgetary.TotalLiquidAmount = totalAmount;

                dataSet.ExtraBudgetaryFilePMP.AddExtraBudgetaryFilePMPRow(extraBudgetary);
            }

            var rpt = new ReportDataSource("ExtraBudgetaryFilePMP", (DataTable)dataSet.ExtraBudgetaryFilePMP);
            this.CallReport(rpt, "Reports/ExtraBudgetary/ExtraBudgetaryFilePMP.rdlc", "Expediente Extrapresupuestario PMP");
        }

        private void PointingListReport()
        {
            var year = this.Request.QueryString["year"];
            var yearValue = string.IsNullOrWhiteSpace(year) ? (int?)null : Convert.ToInt32(year);

            var pointingNumber = this.Request.QueryString["pointingNumber"];
            var pointingNumberValue = string.IsNullOrWhiteSpace(pointingNumber) ? (int?)null : Convert.ToInt32(pointingNumber);

            var date = this.Request.QueryString["date"];
            var dateValue = string.IsNullOrWhiteSpace(date) ? string.Empty : Convert.ToDateTime(date).ToString("d", new CultureInfo("es-ES"));

            var amount = this.Request.QueryString["amount"];
            var amountValue = string.IsNullOrWhiteSpace(amount) ? (decimal?)null : Convert.ToDecimal(amount);

            var fileNumber = this.Request.QueryString["fileNumber"];
            var fileNumberValue = string.IsNullOrWhiteSpace(fileNumber) ? (int?)null : Convert.ToInt32(fileNumber);

            var type = this.Request.QueryString["type"];
            var typeValue = string.IsNullOrWhiteSpace(type) ? string.Empty : type;

            if (yearValue == null)
            {
                return;
            }

            var sings = this.singsService.GetByFilters((int)yearValue, pointingNumberValue, dateValue, amountValue, fileNumberValue, typeValue);

            var dataSet = new ReportsDataSet();

            foreach (var p in sings)
            {
                var pointing = dataSet.PointingListReport.NewPointingListReportRow();

                pointing.PointingNumber = p.SEN_NUMERO == null ? string.Empty : p.SEN_NUMERO.ToString();
                pointing.Year = p.SEN_ANO == null ? string.Empty : p.SEN_ANO.ToString();
                pointing.Date = p.SEN_FECHA == null ? string.Empty : ((DateTime)p.SEN_FECHA).ToString("d", new CultureInfo("es-ES"));
                pointing.DocNumbers = p.NUMERO_DOCUMENTOS.ToString();
                pointing.Amount = p.SEN_TOTAL_LIQUIDO;

                dataSet.PointingListReport.AddPointingListReportRow(pointing);
            }

            var rpt = new ReportDataSource("PointingListReport", (DataTable)dataSet.PointingListReport);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Pointing/PointingList.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Listado de Señalamientos";
            this.ReportViewer.LocalReport.Refresh();
        }
        private void PointingPendingDocs()
        {
            var year = this.Request.QueryString["year"];
            var yearValue = string.IsNullOrWhiteSpace(year) ? (int?)null : Convert.ToInt32(year);

            var type = this.Request.QueryString["type"];
            var typeValue = string.IsNullOrWhiteSpace(type) ? string.Empty : type;

            if (yearValue == null)
            {
                return;
            }

            var sings = this.singsService.GetPendingDocuments((int)yearValue, typeValue);

            var dataSet = new ReportsDataSet();

            foreach (var p in sings)
            {
                var pointing = dataSet.PointingPendingDocs.NewPointingPendingDocsRow();

                pointing.DocNumber = p.NUMERO_DOCUMENTO == null ? string.Empty : p.NUMERO_DOCUMENTO.ToString();
                pointing.DocType = p.TIPO_DOCUMENTO;
                pointing.Date = p.DOC_FECHA_ASIENTO_DIARIO == null ? string.Empty : ((DateTime)p.DOC_FECHA_ASIENTO_DIARIO).ToString("d", new CultureInfo("es-ES"));
                pointing.Concept = p.CONCEPTO;
                pointing.Percertor = p.PROV_NOMBRE;
                pointing.Amount = p.LIQUIDO;
                pointing.CheckNumber = p.DOC_NUMERO_CHEQUE;

                dataSet.PointingPendingDocs.AddPointingPendingDocsRow(pointing);
            }

            var rpt = new ReportDataSource("PointingPendingDocs", (DataTable)dataSet.PointingPendingDocs);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Pointing/PendingDoc.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Listado de Documentos no incluidos";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void PointingCurrentReport()
        {
            var pointingId = this.Request.QueryString["pointingId"];
            var pointingIdValue = string.IsNullOrWhiteSpace(pointingId) ? (int?)null : Convert.ToInt32(pointingId);

            var pointingNumber = this.Request.QueryString["pointingNumber"];
            var pointingNumberValue = string.IsNullOrWhiteSpace(pointingNumber) ? string.Empty : pointingNumber;

            var pointingYear = this.Request.QueryString["pointingYear"];
            var pointingYearValue = string.IsNullOrWhiteSpace(pointingYear) ? string.Empty : pointingYear;
            
            if (pointingIdValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var documents = this.singsService.GetDocumentsByPointingId((int)pointingIdValue);

            foreach (var p in documents)
            {
                var pointing = dataSet.PointingCurrentReport.NewPointingCurrentReportRow();

                pointing.PointingNumber = pointingNumberValue;
                pointing.PointingYear = pointingYearValue;
                pointing.DocNumber = p.NUMERO_DOCUMENTO == null ? string.Empty : p.NUMERO_DOCUMENTO.ToString();
                pointing.Concept = p.CONCEPTO;
                pointing.Perceptor = p.PERCEPTOR;
                pointing.Amount = p.IMPORTE_INTEGRO;
                pointing.Irpf = p.IRPF;
                pointing.SocialSec = p.SEGURIDAD_SOCIAL;
                pointing.Boe = p.BOE;
                pointing.Pasivos = p.D_PASIVOS;
                pointing.Muface = p.MUFACE;
                pointing.Balance = p.IMPORTE_LIQUIDO;
                pointing.TransferNumber = p.NUMERO_CHEQUE;

                dataSet.PointingCurrentReport.AddPointingCurrentReportRow(pointing);
            }

            var rpt = new ReportDataSource("PointingCurrentReport", (DataTable)dataSet.PointingCurrentReport);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Pointing/Pointing.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Señalamiento Actual";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void ListSpendProvisionalStatus()
        {
            var year = this.Request.QueryString["year"];
            var yearValue = string.IsNullOrWhiteSpace(year) ? (int?)null : Convert.ToInt32(year);

            var effectiveDate = this.Request.QueryString["effectiveDate"];
            var effectiveDateValue = string.IsNullOrWhiteSpace(effectiveDate) ? (DateTime?)null : Convert.ToDateTime(effectiveDate);

            var withOutPending = this.Request.QueryString["withOutPending"];
            var withOutPendingValue = string.IsNullOrWhiteSpace(withOutPending) ? (bool?)null : Convert.ToBoolean(withOutPending);

            if (yearValue == null || effectiveDateValue == null || withOutPendingValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var spends = this.budgetsService.GetSpendProvisionalStatus((int)yearValue, (DateTime)effectiveDateValue, (bool)withOutPendingValue);

            foreach (var bud in spends.Where(b => b.Level.Equals("CON")))
            {
                decimal totalAd = 0;
                decimal totalP = 0;
                var hasSons = false;

                foreach (var budget in spends.Where(b => b.ChapterId.Equals(bud.ChapterId) && b.ArticleId.Equals(bud.ArticleId) && b.ConceptId.Equals(bud.ConceptId) && b.Level.Equals("SUB")).ToList())
                {
                    totalAd += budget.AdAmount;
                    totalP += budget.PAmount;
                    hasSons = true;
                }

                if (!hasSons)
                {
                    continue;
                }

                bud.AdAmount = totalAd;
                bud.PAmount = totalP;
            }

            foreach (var bud in spends.Where(b => b.Level.Equals("ART")))
            {
                decimal totalAd = 0;
                decimal totalP = 0;
                var hasSons = false;

                foreach (var budget in spends.Where(b => b.ChapterId.Equals(bud.ChapterId) && b.ArticleId.Equals(bud.ArticleId) && b.Level.Equals("CON")).ToList())
                {
                    totalAd += budget.AdAmount;
                    totalP += budget.PAmount;
                    hasSons = true;
                }

                if (!hasSons)
                {
                    continue;
                }

                bud.AdAmount = totalAd;
                bud.PAmount = totalP;
            }

            foreach (var bud in spends.Where(b => b.Level.Equals("CAP")))
            {
                decimal totalAd = 0;
                decimal totalP = 0;
                var hasSons = false;

                foreach (var budget in spends.Where(b => b.ChapterId.Equals(bud.ChapterId) && b.Level.Equals("ART")).ToList())
                {
                    totalAd += budget.AdAmount;
                    totalP += budget.PAmount;
                    hasSons = true;
                }

                if (!hasSons)
                {
                    continue;
                }

                bud.AdAmount = totalAd;
                bud.PAmount = totalP;
            }

            foreach (var budget in spends)
            {
                budget.PendingAd = budget.FinalAmount - budget.AdAmount;
                budget.PendingP = budget.FinalAmount - budget.PAmount;
            }

            decimal totalAmount = 0;
            decimal totalUpdateAmount = 0;
            decimal totalFinalAmount = 0;
            decimal totalAdAmount = 0;
            decimal totalAdPending = 0;
            decimal totalPAmount = 0;
            decimal totalPPending = 0;

            foreach (var budget in spends.Where(b => b.Level.Equals("CAP")))
            {
                totalAmount += budget.Amount;
                totalUpdateAmount += budget.UpdateAmount;
                totalFinalAmount += budget.FinalAmount;
                totalAdAmount += budget.AdAmount;
                totalAdPending += budget.PendingAd;
                totalPAmount += budget.PAmount;
                totalPPending += budget.PendingP;
            }

            foreach (var s in spends)
            {
                var spend = dataSet.SpendProvisionalStatus.NewSpendProvisionalStatusRow();

                spend.Application = s.ApplicationNumber;
                spend.Level = s.Level;
                spend.Amount = s.Amount;
                spend.UpdateAmount = s.UpdateAmount;
                spend.FinalAmount = s.FinalAmount;
                spend.AdAmount = s.AdAmount;
                spend.AdPending = s.PendingAd;
                spend.PAmount = s.PAmount;
                spend.PPending = s.PendingP;
                spend.Year = (int)yearValue;
                spend.Date = (DateTime)effectiveDateValue;
                spend.TotalAmount = totalAmount;
                spend.TotalUpdateAmount = totalUpdateAmount;
                spend.TotalFinalAmount = totalFinalAmount;
                spend.TotalAdAmount = totalAdAmount;
                spend.TotalAdPending = totalAdPending;
                spend.TotalPAmount = totalPAmount;
                spend.TotalPPending = totalPPending;

                dataSet.SpendProvisionalStatus.AddSpendProvisionalStatusRow(spend);
            }

            var rpt = new ReportDataSource("SpendProvisionalStatus", (DataTable)dataSet.SpendProvisionalStatus);

            this.ReportViewer.LocalReport.ReportPath = (bool)withOutPendingValue ? "Reports/List/SpendProvisionalStatus.rdlc" : "Reports/List/SpendProvisionalStatusPending.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = (bool)withOutPendingValue ? "Estado Provisional del Ejercicio de Gastos" : "Estado Provisional del Ejercicio de Gastos con Pendientes";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void ListSpendsByConcept()
        {
            var year = this.Request.QueryString["year"];
            var yearValue = string.IsNullOrWhiteSpace(year) ? (int?)null : Convert.ToInt32(year);

            var application = this.Request.QueryString["application"];
            var applicationValue = string.IsNullOrWhiteSpace(application) ? string.Empty : application;

            var since = this.Request.QueryString["since"];
            var sinceValue = string.IsNullOrWhiteSpace(since) ? (DateTime?)null : Convert.ToDateTime(since);

            var until = this.Request.QueryString["until"];
            var untilValue = string.IsNullOrWhiteSpace(until) ? (DateTime?)null : Convert.ToDateTime(until);

            if (yearValue == null || string.IsNullOrWhiteSpace(applicationValue))
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var spends = this.accountingRecordsService.GetSpendsByConcept((int)yearValue, applicationValue, sinceValue, untilValue);

            var datesLabel = sinceValue == null && untilValue == null ? string.Empty : $"Fecha asiento desde: {((DateTime)sinceValue).ToString("d", new CultureInfo("es-ES"))} hasta: {((DateTime)untilValue).ToString("d", new CultureInfo("es-ES"))}";

            var othersAuthorized = spends.Where(s => ((DateTime)s.DOC_FECHA_MOVIMIENTO_I).Year != yearValue).Sum(s => s.AUTORIZADO);
            var totalAuthorized = othersAuthorized;
            var budgetBalance = spends.Any() ? spends.FirstOrDefault().IMPORTE : 0;
            budgetBalance -= othersAuthorized;

            var othersOp = spends.Where(s => ((DateTime)s.DOC_FECHA_MOVIMIENTO_I).Year != yearValue).Sum(s => s.OP);
            var totalOp = othersOp;

            var realSpends = spends.Where(s => ((DateTime)s.DOC_FECHA_MOVIMIENTO_I).Year == yearValue).ToList();

            foreach (var s in realSpends)
            {
                totalAuthorized += s.AUTORIZADO;
                budgetBalance -= s.AUTORIZADO;

                totalOp += s.OP;

                var spend = dataSet.SpendByConcept.NewSpendByConceptRow();

                spend.Year = (int)yearValue;
                spend.ApplicationNumber = s.NUMERO_APLICACION;
                spend.ApplicationName = s.NOMBRE_APLICACION;
                spend.InitialBudget = s.IMPORTE;
                spend.DatesLabel = datesLabel;
                spend.Date = ((DateTime)s.DOC_FECHA_MOVIMIENTO_I).ToString("d", new CultureInfo("es-ES"));
                spend.FileNumber = s.EXP_NUM_EXP_CONTABLE_ANUAL.ToString();
                spend.DocumentType = s.TIPO_DOC;
                spend.Provider = s.PROV_NOMBRE;
                spend.Application = s.APLICACION;
                spend.CreditModification = s.MODIF_CREDITO;
                spend.CurrentAuthorized = s.AUTORIZADO;
                spend.TotalAuthorized = totalAuthorized;
                spend.BudgetBalance = budgetBalance;
                spend.OtherAuthorized = othersAuthorized;
                spend.OtherOp = othersOp;
                spend.CurrentOp = s.OP;
                spend.TotalOp = totalOp;
                spend.OpBalance = spend.TotalAuthorized - spend.TotalOp;

                dataSet.SpendByConcept.AddSpendByConceptRow(spend);
            }

            var rpt = new ReportDataSource("SpendByConcept", (DataTable)dataSet.SpendByConcept);

            this.ReportViewer.LocalReport.ReportPath = "Reports/List/SpendByConcept.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Estado del Ejercicio de Gastos";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void ListSpendsByPlace()
        {
            var year = this.Request.QueryString["year"];
            var yearValue = string.IsNullOrWhiteSpace(year) ? (int?)null : Convert.ToInt32(year);

            var place = this.Request.QueryString["place"];
            var placeValue = string.IsNullOrWhiteSpace(place) ? (int?)null : Convert.ToInt32(place);

            var since = this.Request.QueryString["since"];
            var sinceValue = string.IsNullOrWhiteSpace(since) ? (int?)null : Convert.ToInt32(since);

            var until = this.Request.QueryString["until"];
            var untilValue = string.IsNullOrWhiteSpace(until) ? (int?)null : Convert.ToInt32(until);

            var application = this.Request.QueryString["application"];
            var applicationValue = string.IsNullOrWhiteSpace(application) ? string.Empty : application;

            if (yearValue == null || sinceValue == null || untilValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var spends = this.accountingRecordsService.GetSpendsByPlace((int)yearValue, placeValue, (int)sinceValue, (int)untilValue, applicationValue);

            foreach (var s in spends)
            {
                var spend = dataSet.SpendByPlace.NewSpendByPlaceRow();

                spend.Year = year;
                spend.Since = this.GetMonthLabel((int)sinceValue);
                spend.Until = this.GetMonthLabel((int)untilValue);
                spend.Place = s.DOC_DESCRIPCION;
                spend.Month = this.GetMonthLabel(s.MES);
                spend.Description = s.PROV_NOMBRE;
                spend.MiAmount = s.IMPORTE;

                if (!s.TIPD_POSITIVO)
                {
                    spend.MiAmount *= -1;
                }

                dataSet.SpendByPlace.AddSpendByPlaceRow(spend);
            }

            var rpt = new ReportDataSource("SpendByPlace", (DataTable)dataSet.SpendByPlace);

            this.ReportViewer.LocalReport.ReportPath = "Reports/List/SpendByPlace.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Gastos efecturados";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void ListSpendComplianceGrade()
        {
            var year = this.Request.QueryString["year"];
            var yearValue = string.IsNullOrWhiteSpace(year) ? (int?)null : Convert.ToInt32(year);

            var since = this.Request.QueryString["since"];
            var sinceValue = string.IsNullOrWhiteSpace(since) ? (DateTime?)null : Convert.ToDateTime(since);

            var until = this.Request.QueryString["until"];
            var untilValue = string.IsNullOrWhiteSpace(until) ? (DateTime?)null : Convert.ToDateTime(until);

            if (yearValue == null || sinceValue == null || untilValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var spends = this.budgetsService.GetSpendComplianceGrade((int)yearValue, (DateTime)sinceValue, (DateTime)untilValue);

            foreach (var s in spends)
            {
                var spend = dataSet.SpendComplianceGrade.NewSpendComplianceGradeRow();

                spend.Year = (int)yearValue;
                spend.ApplicationNumber = s.ApplicationNumber;
                spend.ApplicationName = s.ApplicationName;
                spend.FinalAmount = s.FinalAmount;
                spend.RcAmount = s.RcAmount;
                spend.RcBalance = s.RcBalanceAmount;
                spend.AdAmount = s.AdAmount;
                spend.OAmount = s.OAmount;
                spend.PAmount = s.PAmount;
                spend.BalanceAmount = s.Amount;

                dataSet.SpendComplianceGrade.AddSpendComplianceGradeRow(spend);
            }

            var rpt = new ReportDataSource("SpendComplianceGrade", (DataTable)dataSet.SpendComplianceGrade);

            this.ReportViewer.LocalReport.ReportPath = "Reports/List/SpendComplianceGrade.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Grado de Cumplimiento no Vinculante";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void ListSpendProvidersByYear()
        {
            var year = this.Request.QueryString["year"];
            var yearValue = string.IsNullOrWhiteSpace(year) ? (int?)null : Convert.ToInt32(year);

            if (yearValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var providers = this.providersService.GetByYearHasSpend((int)yearValue);

            foreach (var p in providers)
            {
                var provider = dataSet.SpendProviders.NewSpendProvidersRow();

                provider.Year = (int)yearValue;
                provider.Name = p.PROV_NOMBRE;
                provider.Nif = p.PROV_NIF;

                dataSet.SpendProviders.AddSpendProvidersRow(provider);
            }

            var rpt = new ReportDataSource("SpendProviders", (DataTable)dataSet.SpendProviders);

            this.ReportViewer.LocalReport.ReportPath = "Reports/List/SpendProvidersByYear.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Listado de Proveedores";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void ListIncomeProvisionalStatus()
        {
            var year = this.Request.QueryString["year"];
            var yearValue = string.IsNullOrWhiteSpace(year) ? (int?)null : Convert.ToInt32(year);

            var effectiveDate = this.Request.QueryString["effectiveDate"];
            var effectiveDateValue = string.IsNullOrWhiteSpace(effectiveDate) ? (DateTime?)null : Convert.ToDateTime(effectiveDate);

            var withOutPending = this.Request.QueryString["withOutPending"];
            var withOutPendingValue = string.IsNullOrWhiteSpace(withOutPending) ? (bool?)null : Convert.ToBoolean(withOutPending);

            if (yearValue == null || effectiveDateValue == null || withOutPendingValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var incomes = this.budgetsService.GetIncomeProvisionalStatus((int)yearValue, (DateTime)effectiveDateValue, (bool)withOutPendingValue);

            foreach (var bud in incomes.Where(b => b.Level.Equals("CON")))
            {
                decimal totalDr = 0;
                decimal totalMi = 0;
                var hasSons = false;

                foreach (var budget in incomes.Where(b => b.ChapterId.Equals(bud.ChapterId) && b.ArticleId.Equals(bud.ArticleId) && b.ConceptId.Equals(bud.ConceptId) && b.Level.Equals("SUB")).ToList())
                {
                    totalDr += budget.DrAmount;
                    totalMi += budget.MiAmount;
                    hasSons = true;
                }

                if (!hasSons)
                {
                    continue;
                }

                bud.DrAmount = totalDr;
                bud.MiAmount = totalMi;
            }

            foreach (var bud in incomes.Where(b => b.Level.Equals("ART")))
            {
                decimal totalDr = 0;
                decimal totalMi = 0;
                var hasSons = false;

                foreach (var budget in incomes.Where(b => b.ChapterId.Equals(bud.ChapterId) && b.ArticleId.Equals(bud.ArticleId) && b.Level.Equals("CON")).ToList())
                {
                    totalDr += budget.DrAmount;
                    totalMi += budget.MiAmount;
                    hasSons = true;
                }

                if (!hasSons)
                {
                    continue;
                }

                bud.DrAmount = totalDr;
                bud.MiAmount = totalMi;
            }

            foreach (var bud in incomes.Where(b => b.Level.Equals("CAP")))
            {
                decimal totalDr = 0;
                decimal totalMi = 0;
                var hasSons = false;

                foreach (var budget in incomes.Where(b => b.ChapterId.Equals(bud.ChapterId) && b.Level.Equals("ART")).ToList())
                {
                    totalDr += budget.DrAmount;
                    totalMi += budget.MiAmount;
                    hasSons = true;
                }

                if (!hasSons)
                {
                    continue;
                }

                bud.DrAmount = totalDr;
                bud.MiAmount = totalMi;
            }

            foreach (var budget in incomes)
            {
                budget.PendingDr = budget.FinalAmount - budget.DrAmount;
                budget.PendingMi = budget.FinalAmount - budget.MiAmount;
            }

            decimal totalAmount = 0;
            decimal totalUpdateAmount = 0;
            decimal totalFinalAmount = 0;
            decimal totalDrAmount = 0;
            decimal totalDrPending = 0;
            decimal totalMiAmount = 0;
            decimal totalMiPending = 0;

            foreach (var budget in incomes.Where(b => b.Level.Equals("CAP")))
            {
                totalAmount += budget.Amount;
                totalUpdateAmount += budget.UpdateAmount;
                totalFinalAmount += budget.FinalAmount;
                totalDrAmount += budget.DrAmount;
                totalDrPending += budget.PendingDr;
                totalMiAmount += budget.MiAmount;
                totalMiPending += budget.PendingMi;
            }

            foreach (var i in incomes)
            {
                var income = dataSet.IncomeProvisionalStatus.NewIncomeProvisionalStatusRow();

                income.Application = i.ApplicationNumber;
                income.Level = i.Level;
                income.Amount = i.Amount;
                income.UpdateAmount = i.UpdateAmount;
                income.FinalAmount = i.FinalAmount;
                income.DrAmount = i.DrAmount;
                income.DrPending = i.PendingDr;
                income.MiAmount = i.MiAmount;
                income.MiPending = i.PendingMi;
                income.Year = (int)yearValue;
                income.Date = (DateTime)effectiveDateValue;
                income.TotalAmount = totalAmount;
                income.TotalUpdateAmount = totalUpdateAmount;
                income.TotalFinalAmount = totalFinalAmount;
                income.TotalDrAmount = totalDrAmount;
                income.TotalDrPending = totalDrPending;
                income.TotalMiAmount = totalMiAmount;
                income.TotalMiPending = totalMiPending;

                dataSet.IncomeProvisionalStatus.AddIncomeProvisionalStatusRow(income);
            }

            var rpt = new ReportDataSource("IncomeProvisionalStatus", (DataTable)dataSet.IncomeProvisionalStatus);

            this.ReportViewer.LocalReport.ReportPath = (bool)withOutPendingValue ? "Reports/List/IncomeProvisionalStatus.rdlc" : "Reports/List/IncomeProvisionalStatusPending.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = (bool)withOutPendingValue ? "Estado Provisional del Ejercicio de Ingresos" : "Estado Provisional del Ejercicio de Ingresos con Pendientes";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void ListIncomesByConcept()
        {
            var year = this.Request.QueryString["year"];
            var yearValue = string.IsNullOrWhiteSpace(year) ? (int?)null : Convert.ToInt32(year);

            var application = this.Request.QueryString["application"];
            var applicationValue = string.IsNullOrWhiteSpace(application) ? string.Empty : application;

            var applicationLabel = this.Request.QueryString["applicationLabel"];
            var applicationLabelValue = string.IsNullOrWhiteSpace(applicationLabel) ? string.Empty : applicationLabel;

            var since = this.Request.QueryString["since"];
            var sinceValue = string.IsNullOrWhiteSpace(since) ? string.Empty : Convert.ToDateTime(since).ToString("yyyy-MM-dd");

            var until = this.Request.QueryString["until"];
            var untilValue = string.IsNullOrWhiteSpace(until) ? string.Empty : Convert.ToDateTime(until).ToString("yyyy-MM-dd");

            if (yearValue == null || string.IsNullOrWhiteSpace(applicationValue))
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var incomes = this.accountingRecordsService.GetIncomesByConcept((int)yearValue, applicationValue, sinceValue, untilValue);

            var initialBudget = incomes.Any() ? incomes.FirstOrDefault().IMPORTE : 0;

            var fillInitial = false;
            foreach (var i in incomes)
            {
                var income = dataSet.IncomeByConcept.NewIncomeByConceptRow();

                income.Year = yearValue.ToString();
                income.Since = string.IsNullOrWhiteSpace(since) ? string.Empty : Convert.ToDateTime(since).ToString("d", new CultureInfo("es-ES")); 
                income.Until = string.IsNullOrWhiteSpace(until) ? string.Empty : Convert.ToDateTime(until).ToString("d", new CultureInfo("es-ES")); 
                income.Application = applicationLabelValue;
                income.InitialBudget = initialBudget;
                income.Date = ((DateTime)i.EXP_FECHA_MODIFICACION).ToString("d", new CultureInfo("es-ES"));
                income.OperationNumber = i.DOC_NUMERO_MOVIMIENTO_I == null ? string.Empty : i.DOC_NUMERO_MOVIMIENTO_I.ToString();
                income.Type = i.TIPO_DOC;
                income.ProviderName = i.PROV_NOMBRE;
                income.CurrentApplication = i.APLICACION;
                income.Mp = i.Mp;

                if (!fillInitial)
                {
                    income.CurrentInitialBudget = initialBudget;
                    fillInitial = true;
                }

                income.Dr = i.DrAmount;
                initialBudget -= income.Dr;
                income.DrBalance = initialBudget;
                income.Mi = i.MiAmount;
                income.MiBalance = income.Dr - income.Mi;

                dataSet.IncomeByConcept.AddIncomeByConceptRow(income);
            }

            var rpt = new ReportDataSource("IncomeByConcept", (DataTable)dataSet.IncomeByConcept);

            this.ReportViewer.LocalReport.ReportPath = "Reports/List/IncomeByConcept.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Estado del Ejercicio de Ingresos";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void ListIncomesByConceptDrMi()
        {
            var year = this.Request.QueryString["year"];
            var yearValue = string.IsNullOrWhiteSpace(year) ? (int?)null : Convert.ToInt32(year);

            var application = this.Request.QueryString["application"];
            var applicationValue = string.IsNullOrWhiteSpace(application) ? string.Empty : application;

            var since = this.Request.QueryString["since"];
            var sinceValue = string.IsNullOrWhiteSpace(since) ? (DateTime?)null : Convert.ToDateTime(since);

            var until = this.Request.QueryString["until"];
            var untilValue = string.IsNullOrWhiteSpace(until) ? (DateTime?)null : Convert.ToDateTime(until);

            if (yearValue == null || string.IsNullOrWhiteSpace(applicationValue))
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var incomes = this.accountingRecordsService.GetIncomesByConceptDrMi((int)yearValue, applicationValue, sinceValue, untilValue);

            foreach (var i in incomes)
            {
                var income = dataSet.IncomeByConceptDrMi.NewIncomeByConceptDrMiRow();

                income.ApplicationNumber = i.NUMERO_APLICACION;
                income.ApplicationName = i.NOMBRE_APLICACION;
                income.InitialBudget = i.IMPORTE;
                income.ModificationBudget = i.MODIF_CREDITO;
                income.FinalBudget = i.AUTORIZADO;
                income.Provider = i.PROV_NOMBRE;
                income.Date = ((DateTime)i.DOC_FECHA_MOVIMIENTO_I).ToString("d", new CultureInfo("es-ES"));
                income.FileNumber = i.DOC_NUMERO_MOVIMIENTO_I == null ? string.Empty : i.DOC_NUMERO_MOVIMIENTO_I.ToString();
                income.DocumentType = i.TIPO_DOC;
                income.Description = i.DOC_DESCRIPCION;
                income.CurrentDr = i.DrAmount;
                income.CurrentMi = i.MiAmount;
                income.CurrentBalance = income.CurrentDr - income.CurrentMi;

                dataSet.IncomeByConceptDrMi.AddIncomeByConceptDrMiRow(income);
            }

            var rpt = new ReportDataSource("IncomeByConceptDrMi", (DataTable)dataSet.IncomeByConceptDrMi);

            this.ReportViewer.LocalReport.ReportPath = "Reports/List/IncomeByConceptDrMi.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Situación por Concepto DR-MI";
            this.ReportViewer.LocalReport.Refresh();
        }


        private void ListIncomesByPlace()
        {
            var year = this.Request.QueryString["year"];
            var yearValue = string.IsNullOrWhiteSpace(year) ? (int?)null : Convert.ToInt32(year);

            var place = this.Request.QueryString["place"];
            var placeValue = string.IsNullOrWhiteSpace(place) ? (int?)null : Convert.ToInt32(place);

            var since = this.Request.QueryString["since"];
            var sinceValue = string.IsNullOrWhiteSpace(since) ? (int?)null : Convert.ToInt32(since);

            var until = this.Request.QueryString["until"];
            var untilValue = string.IsNullOrWhiteSpace(until) ? (int?)null : Convert.ToInt32(until);

            if (yearValue == null || sinceValue == null || untilValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var incomes = this.accountingRecordsService.GetIncomesByPlace((int)yearValue, placeValue, (int)sinceValue, (int)untilValue);

            foreach (var i in incomes)
            {
                var income = dataSet.IncomeByPlace.NewIncomeByPlaceRow();

                income.Year = year;
                income.Since = this.GetMonthLabel((int)sinceValue);
                income.Until = this.GetMonthLabel((int)untilValue);
                income.Place = i.DOC_DESCRIPCION;
                income.Month = this.GetMonthLabel(i.MES);
                income.Date = ((DateTime)i.DOC_FECHA_MOVIMIENTO_I).ToString("d", new CultureInfo("es-ES"));
                income.Description = i.PROV_NOMBRE;
                income.DocumentType = i.TIPO_DOC;
                income.MiAmount = i.IMPORTE;

                if (!i.TIPD_POSITIVO)
                {
                    income.MiAmount *= -1;
                }

                dataSet.IncomeByPlace.AddIncomeByPlaceRow(income);
            }

            var rpt = new ReportDataSource("IncomeByPlace", (DataTable)dataSet.IncomeByPlace);

            this.ReportViewer.LocalReport.ReportPath = "Reports/List/IncomeByPlace.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Ingresos reales efecturados";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void ListIncomeRightsRecognized()
        {
            var date = this.Request.QueryString["date"];
            var dateValue = string.IsNullOrWhiteSpace(date) ? (DateTime?)null : Convert.ToDateTime(date);

            if (dateValue == null) 
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var incomes = this.accountingRecordsService.GetIncomesPendingRightsRecognized((DateTime)dateValue);

            foreach (var i in incomes)
            {
                int year = i.EXP_ANO_PRESUPUESTO == null ? 0 : (int)i.EXP_ANO_PRESUPUESTO;

                var income = dataSet.IncomePendingRightsRecognized.NewIncomePendingRightsRecognizedRow();

                income.Date = ((DateTime)dateValue).ToString("d", new CultureInfo("es-ES"));
                income.Year = i.EXP_ANO_PRESUPUESTO == null ? string.Empty : i.EXP_ANO_PRESUPUESTO.ToString();
                income.ApplicationNumber = i.NUMERO_APLICACION;
                income.ApplicationName = i.APLICACION;
                income.ProviderName = i.PROV_NOMBRE;

                var listCode = new System.Collections.Generic.List<int> { 1, 2, 9, 10, 11, 12 };
                var tipDCodigo = i.TIPD_CODIGO;
                if (listCode.Contains(tipDCodigo))
                {
                    income.DrNumber = i.DOC_NUMERO_MOVIMIENTO_I == null ? string.Empty : i.DOC_NUMERO_MOVIMIENTO_I.ToString();
                }
                else
                {
                    income.DrNumber = string.Empty;
                }

                income.Amount = i.DOCA_IMPORTE;
                income.FileCode = i.EXP_CODIGO.ToString();
                income.LabelYear = i.EXP_ANO_PRESUPUESTO < ((DateTime)dateValue).Year ? "1" : "2";
                income.DRAmount = 0;
                income.MIAmount = 0;
                if (i.TIPD_FASE_DR_I && !i.TIPD_FASE_MI_I)
                {
                    income.DRAmount = i.DOCA_IMPORTE;
                }
                if (i.TIPD_FASE_MI_I && !i.TIPD_FASE_DR_I)
                {
                    income.MIAmount = i.DOCA_IMPORTE;
                }

                dataSet.IncomePendingRightsRecognized.AddIncomePendingRightsRecognizedRow(income);

            }

            var rpt = new ReportDataSource("IncomePendingRightsRecognized", (DataTable)dataSet.IncomePendingRightsRecognized);

            this.ReportViewer.LocalReport.ReportPath = "Reports/List/IncomePendingRightsRecognized.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Derechos Reconocidos pendientes de ingresar";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void ListIncomesDrAgreement()
        {
            var since = this.Request.QueryString["since"];
            var sinceValue = string.IsNullOrWhiteSpace(since) ? (DateTime?)null : Convert.ToDateTime(since);

            var until = this.Request.QueryString["until"];
            var untilValue = string.IsNullOrWhiteSpace(until) ? (DateTime?)null : Convert.ToDateTime(until);

            if (sinceValue == null || untilValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var incomes = this.accountingRecordsService.GetIncomesDrAgreement((DateTime)sinceValue, (DateTime)untilValue);

            foreach (var i in incomes)
            {
                var income = dataSet.IncomeDrAgreement.NewIncomeDrAgreementRow();

                income.Since = ((DateTime)sinceValue).ToString("d", new CultureInfo("es-ES"));
                income.Until = ((DateTime)untilValue).ToString("d", new CultureInfo("es-ES"));
                income.Type = i.REPORT_TYPE;
                income.Provider = i.PROV_NOMBRE;
                income.Amount = i.IMPORTE;

                dataSet.IncomeDrAgreement.AddIncomeDrAgreementRow(income);
            }

            var rpt = new ReportDataSource("IncomeDrAgreement", (DataTable)dataSet.IncomeDrAgreement);

            this.ReportViewer.LocalReport.ReportPath = "Reports/List/IncomeDrAgreement.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Reconocimiento de derechos por convenio";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void ListExtraBudgetary()
        {
            var application = this.Request.QueryString["application"];
            var applicationValue = string.IsNullOrWhiteSpace(application) ? (int?)null : Convert.ToInt32(application);

            var since = this.Request.QueryString["since"];
            var sinceValue = string.IsNullOrWhiteSpace(since) ? (DateTime?)null : Convert.ToDateTime(since);

            var until = this.Request.QueryString["until"];
            var untilValue = string.IsNullOrWhiteSpace(until) ? (DateTime?)null : Convert.ToDateTime(until);

            if (applicationValue == null || sinceValue == null || untilValue == null)
            {
                return;
            }

            var number = string.Empty;
            var name = "Todas aplicaciones extrapresup.";

            var app = this.extraBudgetaryApplicationsService.GetInfo((int)applicationValue);

            if (app != null)
            {
                number = app.EXTRAPRE_NUMERO.ToString();
                name = app.EXTRAPRE_DESCRIPCION;
            }

            var dataSet = new ReportsDataSet();

            var extraBudgetaries = this.extraBudgetaryService.GetDebitAndCredit((int)applicationValue, (DateTime)sinceValue, (DateTime)untilValue);

            decimal totalBalance = 0;
            foreach (var e in extraBudgetaries)
            {
                var extraBudgetary = dataSet.ExtraBudgetaryDebitCredit.NewExtraBudgetaryDebitCreditRow();

                extraBudgetary.Number = number;
                extraBudgetary.Name = name;
                extraBudgetary.Since = ((DateTime)sinceValue).ToString("d", new CultureInfo("es-ES"));
                extraBudgetary.Until = ((DateTime)untilValue).ToString("d", new CultureInfo("es-ES"));
                extraBudgetary.Date = e.EXP_EXTRAP_FECHA == null ? string.Empty : ((DateTime)e.EXP_EXTRAP_FECHA).ToString("d", new CultureInfo("es-ES"));
                extraBudgetary.ExtNumber = e.EXP_EXTRAP_NUMERO == null ? string.Empty : e.EXP_EXTRAP_NUMERO.ToString();
                extraBudgetary.DocumentType = e.EXTRAPRE_DESCRIPCION;
                extraBudgetary.Description = e.EXP_EXTRAP_TEXTO;
                extraBudgetary.Provider = e.TERCERO;
                extraBudgetary.DocumentNumber = e.EXP_EXTRAP_NUMERO_CHEQUE;
                extraBudgetary.Debit = 0;
                extraBudgetary.Credit = 0;
                extraBudgetary.Balance = 0;

                if (e.TIP_EXTRAP_CODIGO != null)
                {
                    if (e.TIP_EXTRAP_CODIGO == 3 || e.TIP_EXTRAP_CODIGO == 5 || e.TIP_EXTRAP_CODIGO == 1)
                    {
                        extraBudgetary.Debit = (decimal)e.EXP_EXTRAP_IMPORTE;
                        totalBalance -= extraBudgetary.Debit;
                    }
                    else
                    {
                        extraBudgetary.Credit = (decimal)e.EXP_EXTRAP_IMPORTE;
                        totalBalance += extraBudgetary.Credit;
                    }

                    extraBudgetary.Balance = totalBalance;
                }

                dataSet.ExtraBudgetaryDebitCredit.AddExtraBudgetaryDebitCreditRow(extraBudgetary);
            }

            var rpt = new ReportDataSource("ExtraBudgetaryDebitCredit", (DataTable)dataSet.ExtraBudgetaryDebitCredit);

            this.ReportViewer.LocalReport.ReportPath = "Reports/List/ExtraBudgetaryDebitCredit.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Estado de Cuentas Extrapresupuestarias";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void ListTreasuryAccountingBook()
        {
            var since = this.Request.QueryString["since"];
            var sinceValue = string.IsNullOrWhiteSpace(since) ? (DateTime?)null : Convert.ToDateTime(since);

            var until = this.Request.QueryString["until"];
            var untilValue = string.IsNullOrWhiteSpace(until) ? (DateTime?)null : Convert.ToDateTime(until);

            if (sinceValue == null || untilValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var tresuries = this.treasuriesService.GetAccountingBook((DateTime)sinceValue, (DateTime)untilValue);

            decimal totalBalance = 0;
            foreach (var t in tresuries)
            {
                var treasury = dataSet.TreasuryAccountingBook.NewTreasuryAccountingBookRow();

                treasury.Since = ((DateTime)sinceValue).ToString("d", new CultureInfo("es-ES"));
                treasury.Until = ((DateTime)untilValue).ToString("d", new CultureInfo("es-ES"));
                treasury.Date = t.TES_FECHA_APUNTE == null ? string.Empty : ((DateTime)t.TES_FECHA_APUNTE).ToString("d", new CultureInfo("es-ES"));
                treasury.CheckNumber = t.TES_NUMERO_CHEQUE;
                treasury.Description = t.TES_DESCRIPCION;
                treasury.Application = t.TES_APLICACION;
                treasury.EffectiveDate = t.TES_FECHA_BANCO == null ? string.Empty : ((DateTime)t.TES_FECHA_BANCO).ToString("d", new CultureInfo("es-ES"));

                treasury.Debit = 0;
                treasury.Credit = 0;
                treasury.Balance = 0;

                if (t.TES_HABER != null && (bool)t.TES_HABER == false)
                {
                    treasury.Debit = (decimal)t.TES_TOTAL_IMPORTE_LIQUIDO;
                    totalBalance += treasury.Debit;
                }
                else
                {
                    treasury.Credit = (decimal)t.TES_TOTAL_IMPORTE_LIQUIDO;
                    totalBalance -= treasury.Credit;
                }

                treasury.Balance = totalBalance;

                dataSet.TreasuryAccountingBook.AddTreasuryAccountingBookRow(treasury);
            }

            var rpt = new ReportDataSource("TreasuryAccountingBook", (DataTable)dataSet.TreasuryAccountingBook);

            this.ReportViewer.LocalReport.ReportPath = "Reports/List/TreasuryAccountingBook.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Libro de Banco de España";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void ListTreasuryBankStatement()
        {
            var since = this.Request.QueryString["since"];
            var sinceValue = string.IsNullOrWhiteSpace(since) ? (DateTime?)null : Convert.ToDateTime(since);

            var until = this.Request.QueryString["until"];
            var untilValue = string.IsNullOrWhiteSpace(until) ? (DateTime?)null : Convert.ToDateTime(until);

            if (sinceValue == null || untilValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var tresuries = this.treasuriesService.GetBankStatement((DateTime)sinceValue, (DateTime)untilValue);

            foreach (var t in tresuries)
            {
                var treasury = dataSet.TreasuryBankStatement.NewTreasuryBankStatementRow();

                treasury.Since = ((DateTime)sinceValue).ToString("d", new CultureInfo("es-ES"));
                treasury.Until = ((DateTime)untilValue).ToString("d", new CultureInfo("es-ES"));
                treasury.Date = t.TES_FECHA_APUNTE == null ? string.Empty : ((DateTime)t.TES_FECHA_APUNTE).ToString("d", new CultureInfo("es-ES"));
                treasury.CheckNumber = t.TES_NUMERO_CHEQUE;
                treasury.Description = t.TES_DESCRIPCION;
                treasury.Application = t.TES_APLICACION;
                treasury.EffectiveDate = t.TES_FECHA_BANCO == null ? string.Empty : ((DateTime)t.TES_FECHA_BANCO).ToString("d", new CultureInfo("es-ES"));

                treasury.Debit = 0;
                treasury.Credit = 0;

                if (t.TES_HABER != null && (bool)t.TES_HABER == false)
                {
                    treasury.Debit = (decimal)t.TES_TOTAL_IMPORTE_LIQUIDO;
                }
                else
                {
                    treasury.Credit = (decimal)t.TES_TOTAL_IMPORTE_LIQUIDO;
                }

                dataSet.TreasuryBankStatement.AddTreasuryBankStatementRow(treasury);
            }

            var rpt = new ReportDataSource("TreasuryBankStatement", (DataTable)dataSet.TreasuryBankStatement);

            this.ReportViewer.LocalReport.ReportPath = "Reports/List/TreasuryBankStatement.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Libro de Extracto de Banco de España";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void ListTreasuryBlockListing()
        {
            var amount = this.Request.QueryString["amount"];
            var amountValue = string.IsNullOrWhiteSpace(amount) ? (decimal?)null : Convert.ToDecimal(amount);

            var register = this.Request.QueryString["register"];
            var registerValue = string.IsNullOrWhiteSpace(register) ? (DateTime?)null : Convert.ToDateTime(register);

            if (amountValue == null || registerValue == null)
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var treasuries = this.treasuriesService.GetBlockListing((DateTime)registerValue);

            decimal balance = 0;
            decimal pending = 0;
            foreach (var t in treasuries)
            {
                if (t.TES_HABER != null && (bool)t.TES_HABER == false)
                {
                    balance += (decimal)t.TES_TOTAL_IMPORTE_LIQUIDO;
                }
                else
                {
                    balance -= (decimal)t.TES_TOTAL_IMPORTE_LIQUIDO;
                }

                if (t.TES_FECHA_BANCO == null
                    || (DateTime.Compare((DateTime)t.TES_FECHA_BANCO, (DateTime)registerValue) > 0
                        && DateTime.Compare((DateTime)t.TES_FECHA_APUNTE, (DateTime)registerValue) <= 0
                        && (t.TES_HABER != null && (bool)t.TES_HABER == true)
                       )
                   )
                {
                    if (t.TES_HABER != null && (bool)t.TES_HABER == true) 
                    { 
                        pending += (decimal)t.TES_TOTAL_IMPORTE_LIQUIDO;
                    } else { 
                        pending -= (decimal)t.TES_TOTAL_IMPORTE_LIQUIDO; 
                    }                        
                }
            }

            var treasury = dataSet.TreasuryBlockListing.NewTreasuryBlockListingRow();

            treasury.Year = ((DateTime)registerValue).Year.ToString();
            treasury.Date = ((DateTime)registerValue).ToString("d", new CultureInfo("es-ES"));
            treasury.CurrentAmount = (decimal)amountValue;
            treasury.BudgetAmount = balance + pending;
            treasury.PendingAmount = pending;

            dataSet.TreasuryBlockListing.AddTreasuryBlockListingRow(treasury);

            var rpt = new ReportDataSource("TreasuryBlockListing", (DataTable)dataSet.TreasuryBlockListing);

            this.ReportViewer.LocalReport.ReportPath = "Reports/List/TreasuryBlockListing.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Listado de Cuadre";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void ListTreasuryPaymentRecord()
        {
            var since = this.Request.QueryString["since"];
            var sinceValue = string.IsNullOrWhiteSpace(since) ? (DateTime?)null : Convert.ToDateTime(since);

            var until = this.Request.QueryString["until"];
            var untilValue = string.IsNullOrWhiteSpace(until) ? (DateTime?)null : Convert.ToDateTime(until);

            if (sinceValue == null || untilValue == null)
            {
                return;
            }

            var currentYearMonth = string.Empty;

            var dataSet = new ReportsDataSet();

            var tresuries = this.treasuriesService.GetPaymentRecord((DateTime)sinceValue, (DateTime)untilValue);

            foreach (var t in tresuries)
            {
                var yearMonth = t.TES_FECHA_APUNTE == null ? string.Empty : $"{((DateTime)t.TES_FECHA_APUNTE).Year}/{((DateTime)t.TES_FECHA_APUNTE).Month}";

                if (currentYearMonth.Equals(yearMonth))
                {

                }

                var treasury = dataSet.TreasuryPaymentRecord.NewTreasuryPaymentRecordRow();

                treasury.Since = ((DateTime)sinceValue).ToString("d", new CultureInfo("es-ES"));
                treasury.Until = ((DateTime)untilValue).ToString("d", new CultureInfo("es-ES"));
                treasury.Date = t.TES_FECHA_APUNTE == null ? string.Empty : ((DateTime)t.TES_FECHA_APUNTE).ToString("d", new CultureInfo("es-ES"));
                treasury.DocumentNumber = t.EXP_NUM_EXP_CONTABLE_ANUAL == null ? string.Empty : t.EXP_NUM_EXP_CONTABLE_ANUAL.ToString();
                treasury.Application = t.TES_APLICACION;
                treasury.Provider = t.PROV_NOMBRE;
                treasury.Integro = (decimal)t.TES_TOTAL_IMPORTE_LIQUIDO;
                treasury.Irpf = t.IRPF;
                treasury.Social = t.SEGURIDAD_SOCIAL;
                treasury.Boe = t.BOE;
                treasury.Pasivos = t.D_PASIVOS;
                treasury.Muface = t.MUFACE;
                treasury.Anticipos = t.ANTICIPO_HABERES;
                treasury.Intereses = t.INTERESES_ANTICIPOS;
                treasury.Total = treasury.Irpf + treasury.Social + treasury.Boe + treasury.Pasivos + treasury.Muface + treasury.Anticipos + treasury.Intereses;
                treasury.BankAmount = t.TESD_IMPORTE_LIQUIDO;
                treasury.CheckNumber = t.TES_NUMERO_CHEQUE;
                treasury.Month = t.TES_FECHA_APUNTE == null ? string.Empty : this.GetMonthLabel(((DateTime)t.TES_FECHA_APUNTE).Month).ToLower();
                treasury.YearMonth = t.TES_FECHA_APUNTE == null ? string.Empty : $"{((DateTime)t.TES_FECHA_APUNTE).Year}/{((DateTime)t.TES_FECHA_APUNTE).Month}";

                dataSet.TreasuryPaymentRecord.AddTreasuryPaymentRecordRow(treasury);
            }

            var rpt = new ReportDataSource("TreasuryPaymentRecord", (DataTable)dataSet.TreasuryPaymentRecord);

            this.ReportViewer.LocalReport.ReportPath = "Reports/List/TreasuryPaymentRecord.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Registro de Pagos de Contabilidad";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void MaintenanceApplications()
        {
            var type = this.Request.QueryString["type"];
            var typeValue = string.IsNullOrWhiteSpace(type) ? string.Empty : type;

            if (string.IsNullOrWhiteSpace(typeValue))
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var applications = this.applicationService.GetApplications(typeValue);

            var label = string.Empty;

            switch (typeValue)
            {
                case "I":
                    label = "Ingresos";

                    break;
                case "G":
                    label = "Gastos";

                    break;
            }

            foreach (var a in applications)
            {
                var application = dataSet.MaintenanceApllications.NewMaintenanceApllicationsRow();

                application.Label = label.ToUpper();
                application.Application = a.ApplicationLabel;
                application.Description = a.Description;
                application.Level = a.Level;

                dataSet.MaintenanceApllications.AddMaintenanceApllicationsRow(application);
            }

            var rpt = new ReportDataSource("MaintenanceApllications", (DataTable)dataSet.MaintenanceApllications);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Maintenance/Applications.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = $"Aplicaciones del presupuesto de {label}";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void MaintenanceAccountsRestricted()
        {
            var type = this.Request.QueryString["type"];
            var typeValue = string.IsNullOrWhiteSpace(type) ? string.Empty : type;

            if (string.IsNullOrWhiteSpace(typeValue))
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var accounts = this.accountRestrictedService.GetAccountsRestricted(type);

            var label = string.Empty;

            switch (typeValue)
            {
                case "I":
                    label = "Ingresos";

                    break;
                case "G":
                    label = "Gastos";

                    break;
            }

            foreach (var c in accounts)
            {
                var account = dataSet.MaintenanceAccountsRestricted.NewMaintenanceAccountsRestrictedRow();

                account.Label = label.ToUpper();
                account.OrdPerc = c.CUE_ORDINAL_PERCEPTOR == null ? string.Empty : c.CUE_ORDINAL_PERCEPTOR.ToString();
                account.CueDesc = c.CUE_DESCRIPCION;
                account.CueCC = c.CUE_CC;
                account.Sede = c.CenLabel;
                account.Banco = c.CUE_ENTIDAD;
                account.Direccion = c.CUE_DIRECCION;

                dataSet.MaintenanceAccountsRestricted.AddMaintenanceAccountsRestrictedRow(account);
            }

            var rpt = new ReportDataSource("MaintenanceAccountsRestricted", (DataTable)dataSet.MaintenanceAccountsRestricted);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Maintenance/RestrictedAccounts.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = $"Cuentas Restringidas de {label}";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void MaintenanceExtraBudgetaryApplications()
        {
            var dataSet = new ReportsDataSet();

            var applications = this.extraBudgetaryApplicationsService.GetExtraBudgetaryApplications();

            foreach (var a in applications)
            {
                var application = dataSet.MaintenanceApllications.NewMaintenanceApllicationsRow();

                application.Application = a.EXTRAPRE_NUMERO == null ? string.Empty : a.EXTRAPRE_NUMERO.ToString();
                application.Description = a.EXTRAPRE_DESCRIPCION;
                application.Level = a.CUEP_NUMERO;

                dataSet.MaintenanceApllications.AddMaintenanceApllicationsRow(application);
            }

            var rpt = new ReportDataSource("MaintenanceApllications", (DataTable)dataSet.MaintenanceApllications);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Maintenance/ExtraBudgetaryApplications.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Aplicaciones Extrapresupuestarias";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void MaintenancePayForms()
        {
            var dataSet = new ReportsDataSet();

            var payForms = this.payFormsService.GetPayForms();

            foreach (var p in payForms)
            {
                var application = dataSet.MaintenancePayForms.NewMaintenancePayFormsRow();

                application.Code = p.FOR_CODIGO.ToString();
                application.Description = p.FOR_DESCRIPCION;

                dataSet.MaintenancePayForms.AddMaintenancePayFormsRow(application);
            }

            var rpt = new ReportDataSource("MaintenancePayForms", (DataTable)dataSet.MaintenancePayForms);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Maintenance/PayForms.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Formas de Pago";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void MaintenanceTreasuryLines()
        {
            var dataSet = new ReportsDataSet();

            var treasuryLines = this.treasuryLinesService.GetTreasuryLines();

            foreach (var t in treasuryLines)
            {
                var treasury = dataSet.MaintenanceTreasuryLines.NewMaintenanceTreasuryLinesRow();

                treasury.Number = t.LIN_NUMERO.ToString();
                treasury.Description = t.LIN_DESCRIPCION;
                treasury.OriginDescription = t.LIN_ORIGEN_DESCRIPCION;
                treasury.OriginCode = t.LIN_ORIGEN_CODIGO == null ? string.Empty : t.LIN_ORIGEN_CODIGO.ToString();

                dataSet.MaintenanceTreasuryLines.AddMaintenanceTreasuryLinesRow(treasury);
            }

            var rpt = new ReportDataSource("MaintenanceTreasuryLines", (DataTable)dataSet.MaintenanceTreasuryLines);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Maintenance/TreasuryLines.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Líneas de Tesorería";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void MaintenancePrograms()
        {
            var dataSet = new ReportsDataSet();

            var programs = this.programsService.GetPrograms();

            foreach (var p in programs)
            {
                var program = dataSet.MaintenancePrograms.NewMaintenanceProgramsRow();

                program.Number = p.PRO_NUMERO;
                program.Description = p.PRO_DESCRIPCION;
                program.Default = (bool)p.PRO_POR_DEFECTO ? "Si" : "No";

                dataSet.MaintenancePrograms.AddMaintenanceProgramsRow(program);
            }

            var rpt = new ReportDataSource("MaintenancePrograms", (DataTable)dataSet.MaintenancePrograms);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Maintenance/Programs.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Programas";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void MaintenanceProvenances()
        {
            var type = this.Request.QueryString["type"];
            var typeValue = string.IsNullOrWhiteSpace(type) ? string.Empty : type;

            if (string.IsNullOrWhiteSpace(typeValue))
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var provenances = this.provenancesService.GetProvenances(typeValue);

            var label = string.Empty;

            switch (typeValue)
            {
                case "I":
                    label = "Ingresos";

                    break;
                case "G":
                    label = "Gastos";

                    break;
            }

            foreach (var p in provenances.OrderBy(p => p.PROC_CODIGO))
            {
                var application = dataSet.MaintenanceProvenances.NewMaintenanceProvenancesRow();

                application.Label = label.ToUpper();
                application.Code = p.PROC_CODIGO.ToString();
                application.Description = p.PROC_DESCRIPCION;

                dataSet.MaintenanceProvenances.AddMaintenanceProvenancesRow(application);
            }

            var rpt = new ReportDataSource("MaintenanceProvenances", (DataTable)dataSet.MaintenanceProvenances);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Maintenance/Provenances.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = $"Procedencias de {label}";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void MaintenanceProviders()
        {
            var name = this.Request.QueryString["name"];
            var nameValue = string.IsNullOrWhiteSpace(name) ? string.Empty : name;

            var nif = this.Request.QueryString["nif"];
            var nifValue = string.IsNullOrWhiteSpace(nif) ? string.Empty : nif;

            var dataSet = new ReportsDataSet();

            var providers = this.providersService.FindProviders(nameValue, nifValue);

            foreach (var p in providers)
            {
                var provider = dataSet.MaintenanceProviders.NewMaintenanceProvidersRow();

                provider.ProvName = p.PROV_NOMBRE;
                provider.ProvNif = p.PROV_NIF;
                provider.ContactPerson = p.PROV_PERSONA_CONTACTO;
                provider.PhoneNumber = p.PROV_TELEFONO;
                provider.Address = p.PROV_DIRECCION;
                provider.Province = p.ProvName;

                dataSet.MaintenanceProviders.AddMaintenanceProvidersRow(provider);
            }

            var rpt = new ReportDataSource("MaintenanceProviders", (DataTable)dataSet.MaintenanceProviders);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Maintenance/Providers.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Proveedores";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void MaintenanceDocumentTypes()
        {
            var type = this.Request.QueryString["type"];
            var typeValue = string.IsNullOrWhiteSpace(type) ? string.Empty : type;

            if (string.IsNullOrWhiteSpace(typeValue))
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var documentTypes = this.documentTypesService.GetDocumentTypes(typeValue);

            foreach (var d in documentTypes)
            {
                var application = dataSet.MaintenanceDocumentTypes.NewMaintenanceDocumentTypesRow();

                application.Code = d.TIPD_CLAVE == null ? string.Empty : d.TIPD_CLAVE.ToString();
                application.ShortName = d.TIPD_NOMBRE_CORTO;
                application.Sing = d.SingLabel;
                application.LargeName = d.TIPD_DESCRIPCION;
                application.Rc = (bool)d.TIPD_FASE_RC_G ? "Si" : "No";
                application.Ad = (bool)d.TIPD_FASE_AD_G ? "Si" : "No";
                application.O = (bool)d.TIPD_FASE_O_G ? "Si" : "No";
                application.P = (bool)d.TIPD_FASE_P_G ? "Si" : "No";
                application.Dr = (bool)d.TIPD_FASE_DR_I ? "Si" : "No";
                application.Mi = (bool)d.TIPD_FASE_MI_I ? "Si" : "No";

                dataSet.MaintenanceDocumentTypes.AddMaintenanceDocumentTypesRow(application);
            }

            var rpt = new ReportDataSource("MaintenanceDocumentTypes", (DataTable)dataSet.MaintenanceDocumentTypes);

            this.ReportViewer.LocalReport.ReportPath = typeValue.Equals("I") ? "Reports/Maintenance/DocumentTypesIncome.rdlc" : "Reports/Maintenance/DocumentTypesSpend.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = typeValue.Equals("I") ? "Tipos de Documentos de Ingresos" : "Tipos de Documentos de Gastos";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void MaintenanceCreditModificationTypes()
        {
            var type = this.Request.QueryString["type"];
            var typeValue = string.IsNullOrWhiteSpace(type) ? string.Empty : type;

            if (string.IsNullOrWhiteSpace(typeValue))
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var creditModificationTypes = this.creditModificationTypesService.GetCreditModificationTypes(typeValue);

            var label = string.Empty;

            switch (typeValue)
            {
                case "I":
                    label = "Ingresos";

                    break;
                case "G":
                    label = "Gastos";

                    break;
            }

            foreach (var c in creditModificationTypes)
            {
                var creditModificationType = dataSet.MaintenanceCreditModificationTypes.NewMaintenanceCreditModificationTypesRow();

                creditModificationType.Label = label.ToUpper();
                creditModificationType.Number = c.TIPM_NUMERO == null ? string.Empty : c.TIPM_NUMERO.ToString();
                creditModificationType.Description = c.TIPM_DESCRIPCION;

                dataSet.MaintenanceCreditModificationTypes.AddMaintenanceCreditModificationTypesRow(creditModificationType);
            }

            var rpt = new ReportDataSource("MaintenanceCreditModificationTypes", (DataTable)dataSet.MaintenanceCreditModificationTypes);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Maintenance/CreditModificationTypes.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = $"Tipos de Modificación de Crédito de {label}";
            this.ReportViewer.LocalReport.Refresh();
        }

        private void MaintenancePayTypes()
        {
            var dataSet = new ReportsDataSet();

            var payTypes = this.payTypesService.GetPayTypes();

            foreach (var p in payTypes)
            {
                var payType = dataSet.MaintenancePayTypes.NewMaintenancePayTypesRow();

                payType.Code = p.TIPP_CODIGO.ToString();
                payType.Description = p.TIPP_DESCRIPCION;

                dataSet.MaintenancePayTypes.AddMaintenancePayTypesRow(payType);
            }

            var rpt = new ReportDataSource("MaintenancePayTypes", (DataTable)dataSet.MaintenancePayTypes);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Maintenance/PayTypes.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Tipos de Pago";
            this.ReportViewer.LocalReport.Refresh();
        }


        private void RectificationsList()
        {
            var date = this.Request.QueryString["date"];
            var dateValue = string.IsNullOrWhiteSpace(date) ? (DateTime?)null : Convert.ToDateTime(date);

            var iCodes = this.Request.QueryString["iCodes"];
            var iCodesValue = string.IsNullOrWhiteSpace(iCodes) ? string.Empty : iCodes;

            var eCodes = this.Request.QueryString["eCodes"];
            var eCodesValue = string.IsNullOrWhiteSpace(eCodes) ? string.Empty : eCodes;

            if (dateValue == null || (string.IsNullOrWhiteSpace(iCodesValue) && string.IsNullOrWhiteSpace(eCodesValue)))
            {
                return;
            }

            var dataSet = new ReportsDataSet();

            var rectifications = this.rectificationsService.GetRectificationsByCodes(iCodesValue, eCodesValue);

            var label = string.Empty;

            foreach (var r in rectifications)
            {
                var rectification = dataSet.RectificationsList.NewRectificationsListRow();

                rectification.Date = ((DateTime)dateValue).ToString("d", new CultureInfo("es-ES"));
                rectification.Year = ((DateTime)dateValue).Year.ToString();
                rectification.Description = r.Description;
                rectification.Nature = r.Nature;
                rectification.BudgetYear = r.Year;
                rectification.Section = r.Origin;
                rectification.Application = r.Concept;
                rectification.Amount = r.Amount;
                rectification.Cta = r.Cta;
                rectification.Provider = r.ProviderName;

                dataSet.RectificationsList.AddRectificationsListRow(rectification);
            }

            var rpt = new ReportDataSource("RectificationsList", (DataTable)dataSet.RectificationsList);

            this.ReportViewer.LocalReport.ReportPath = "Reports/Rectification/RectificationsList.rdlc";
            this.ReportViewer.ProcessingMode = ProcessingMode.Local;
            this.ReportViewer.LocalReport.DataSources.Add(rpt);
            this.ReportViewer.LocalReport.EnableExternalImages = true;
            this.ReportViewer.LocalReport.DisplayName = "Rectificación Aplicación Definitiva";
            this.ReportViewer.LocalReport.Refresh();
        }

        #endregion


        private string GetMonthLabel(int month)
        {
            var monthLabel = string.Empty;
            switch (month)
            {
                case 1:
                    monthLabel = "ENERO";
                    break;
                case 2:
                    monthLabel = "FEBRERO";
                    break;
                case 3:
                    monthLabel = "MARZO";
                    break;
                case 4:
                    monthLabel = "ABRIL";
                    break;
                case 5:
                    monthLabel = "MAYO";
                    break;
                case 6:
                    monthLabel = "JUNIO";
                    break;
                case 7:
                    monthLabel = "JULIO";
                    break;
                case 8:
                    monthLabel = "AGOSTO";
                    break;
                case 9:
                    monthLabel = "SEPTIEMBRE";
                    break;
                case 10:
                    monthLabel = "OCTUBRE";
                    break;
                case 11:
                    monthLabel = "NOVIEMBRE";
                    break;
                case 12:
                    monthLabel = "DICIEMBRE";
                    break;
            }

            return monthLabel;
        }
    }
}