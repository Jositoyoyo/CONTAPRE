namespace Dimatica.ContaPre.BLL.IntegrationTest.Services
{
    using Dimatica.ContaPre.BLL.Services;
    using Dimatica.ContaPre.BLL.IntegrationTest.Support;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [TestCategory("Integration")]
    [DoNotParallelize]
    public class ServiceIntegrationTests
    {
        [TestMethod]
        public void AccountingDocumentsService_CanReadDocument()
        {
            IntegrationDatabase.Configure(AccountingDocumentsDataContext.Instance);
            new AccountingDocumentsService().GetById(0);
        }

        [TestMethod]
        public void AccountingRecordsService_CanReadRecord()
        {
            IntegrationDatabase.Configure(AccountingRecordsDataContext.Instance);
            new AccountingRecordsService().GetById(0);
        }

        [TestMethod]
        public void AccountPgcpService_CanReadAccounts()
        {
            IntegrationDatabase.Configure(AccountPgcpDataContext.Instance);
            Assert.IsNotNull(new AccountPgcpService().GetAccountsToCombo());
        }

        [TestMethod]
        public void AccountRestrictedService_CanReadAccounts()
        {
            IntegrationDatabase.Configure(AccountRestrictedDataContext.Instance);
            Assert.IsNotNull(new AccountRestrictedService().GetAccountsToCombo(string.Empty));
        }

        [TestMethod]
        public void AdministrativeRecordsService_CanReadRecord()
        {
            IntegrationDatabase.Configure(AdministrativeRecordsDataContext.Instance);
            new AdministrativeRecordsService().GetById(0);
        }

        [TestMethod]
        public void ApplicationService_CanReadApplications()
        {
            IntegrationDatabase.Configure(ApplicationDataContext.Instance);
            Assert.IsNotNull(new ApplicationService().GetApplications(string.Empty));
        }

        [TestMethod]
        public void BillPurchasesService_CanReadPurchases()
        {
            IntegrationDatabase.Configure(BillPurchasesDataContext.Instance);
            Assert.IsNotNull(new BillPurchasesService().GetByDocument(0));
        }

        [TestMethod]
        public void BudgetApplicationsService_CanReadNumbers()
        {
            IntegrationDatabase.Configure(BudgetApplicationsDataContext.Instance);
            Assert.IsNotNull(new BudgetApplicationsService().GetNumbersByTypeByYear(string.Empty, 0));
        }

        [TestMethod]
        public void BudgetsService_CanReadIncomeCompliance()
        {
            IntegrationDatabase.Configure(BudgetsDataContext.Instance);
            Assert.IsNotNull(new BudgetsService().GetIncomeLevelCompliance(0));
        }

        [TestMethod]
        public void ContractTypesService_CanReadContractTypes()
        {
            IntegrationDatabase.Configure(ContractTypesDataContext.Instance);
            Assert.IsNotNull(new ContractTypesService().GetContractTypes());
        }

        [TestMethod]
        public void CostPlacesService_CanReadCostPlaces()
        {
            IntegrationDatabase.Configure(CostPlacesDataContext.Instance);
            Assert.IsNotNull(new CostPlacesService().GetCostPlacesToCombo());
        }

        [TestMethod]
        public void CreditModificationBudgetsService_CanReadBudgets()
        {
            IntegrationDatabase.Configure(CreditModificationBudgetsDataContext.Instance);
            Assert.IsNotNull(new CreditModificationBudgetsService().GetByCreditModification(0, string.Empty));
        }

        [TestMethod]
        public void CreditModificationsService_CanReadYears()
        {
            IntegrationDatabase.Configure(CreditModificationsDataContext.Instance);
            Assert.IsNotNull(new CreditModificationsService().GetYears());
        }

        [TestMethod]
        public void CreditModificationTypesService_CanReadTypes()
        {
            IntegrationDatabase.Configure(CreditModificationTypesDataContext.Instance);
            Assert.IsNotNull(new CreditModificationTypesService().GetCreditModificationTypes(string.Empty));
        }

        [TestMethod]
        public void DocumentTypesService_CanReadDocumentTypes()
        {
            IntegrationDatabase.Configure(DocumentTypesDataContext.Instance);
            Assert.IsNotNull(new DocumentTypesService().GetDocumentTypesDrPhase());
        }

        [TestMethod]
        public void ExtraBudgetaryApplicationsService_CanReadApplications()
        {
            IntegrationDatabase.Configure(ExtraBudgetaryApplicationsDataContext.Instance);
            Assert.IsNotNull(new ExtraBudgetaryApplicationsService().GetExtraBudgetaryApplications());
        }

        [TestMethod]
        public void ExtraBudgetaryRecordsService_CanReadAmount()
        {
            IntegrationDatabase.Configure(ExtraBudgetaryRecordsDataContext.Instance);
            new ExtraBudgetaryRecordsService().GetSumAmount(0);
        }

        [TestMethod]
        public void OriginsService_CanReadOrigins()
        {
            IntegrationDatabase.Configure(OriginsDataContext.Instance);
            Assert.IsNotNull(new OriginsService().GetOrigins());
        }

        [TestMethod]
        public void ParametersService_CanReadParameters()
        {
            IntegrationDatabase.Configure(ParametersDataContext.Instance);
            Assert.IsNotNull(new ParametersService().GetParameters());
        }

        [TestMethod]
        public void PayFormsService_CanReadPayForms()
        {
            IntegrationDatabase.Configure(PayFormsDataContext.Instance);
            Assert.IsNotNull(new PayFormsService().GetPayForms());
        }

        [TestMethod]
        public void PayTypesService_CanReadPayTypes()
        {
            IntegrationDatabase.Configure(PayTypesDataContext.Instance);
            Assert.IsNotNull(new PayTypesService().GetPayTypes());
        }

        [TestMethod]
        public void ProgramsService_CanReadPrograms()
        {
            IntegrationDatabase.Configure(ProgramsDataContext.Instance);
            Assert.IsNotNull(new ProgramsService().GetPrograms());
        }

        [TestMethod]
        public void ProvenancesService_CanReadProvenances()
        {
            IntegrationDatabase.Configure(ProvenancesDataContext.Instance);
            Assert.IsNotNull(new ProvenancesService().GetProvenances(string.Empty));
        }

        [TestMethod]
        public void ProvidersService_CanReadProviders()
        {
            IntegrationDatabase.Configure(ProvidersDataContext.Instance);
            Assert.IsNotNull(new ProvidersService().GetProviders());
        }

        [TestMethod]
        public void ProvincesService_CanReadProvinces()
        {
            IntegrationDatabase.Configure(ProvincesDataContext.Instance);
            Assert.IsNotNull(new ProvincesService().GetProvincesToCombo());
        }

        [TestMethod]
        public void RecordTypesService_CanReadRecordTypes()
        {
            IntegrationDatabase.Configure(RecordTypesDataContext.Instance);
            Assert.IsNotNull(new RecordTypesService().GetRecordTypes());
        }

        [TestMethod]
        public void RectificationsService_CanReadRectifications()
        {
            IntegrationDatabase.Configure(RectificationsDataContext.Instance);
            Assert.IsNotNull(new RectificationsService().GetRectificationsByCodes(string.Empty, string.Empty));
        }

        [TestMethod]
        public void SingsService_CanReadPointing()
        {
            IntegrationDatabase.Configure(SingsDataContext.Instance);
            new SingsService().GetById(0);
        }

        [TestMethod]
        public void TonnageSheetsService_CanReadSheet()
        {
            IntegrationDatabase.Configure(TonnageSheetsDataContext.Instance);
            new TonnageSheetsService().GetById(0);
        }

        [TestMethod]
        public void TreasuriesService_CanReadTreasury()
        {
            IntegrationDatabase.Configure(TreasuriesDataContext.Instance);
            new TreasuriesService().GetById(0);
        }

        [TestMethod]
        public void TreasuryLinesService_CanReadLines()
        {
            IntegrationDatabase.Configure(TreasuryLinesDataContext.Instance);
            Assert.IsNotNull(new TreasuryLinesService().GetTreasuryLines());
        }

        [TestMethod]
        public void UserService_CanReadUsers()
        {
            IntegrationDatabase.Configure(UserDataContext.Instance);
            Assert.IsNotNull(new UserService().GetUsers());
        }
    }
}
