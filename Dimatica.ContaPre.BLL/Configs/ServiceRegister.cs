namespace Dimatica.ContaPre.BLL.Configs
{
    #region NameSpaces

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.BLL.Services;

    using LightInject;

    #endregion

    public class ServiceRegister : ICompositionRoot
    {
        #region ICompositionRoot Members

        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IAccountingDocumentsService, AccountingDocumentsService>();
            serviceRegistry.Register<IAccountingRecordsService, AccountingRecordsService>();
            serviceRegistry.Register<IAccountPgcpService, AccountPgcpService>();
            serviceRegistry.Register<IAccountRestrictedService, AccountRestrictedService>();
            serviceRegistry.Register<IAdministrativeRecordsService, AdministrativeRecordsService>();
            serviceRegistry.Register<IApplicationService, ApplicationService>();
            serviceRegistry.Register<IBillPurchasesService, BillPurchasesService>();
            serviceRegistry.Register<IBudgetApplicationsService, BudgetApplicationsService>();
            serviceRegistry.Register<IBudgetsService, BudgetsService>();
            serviceRegistry.Register<IContractTypesService, ContractTypesService>();
            serviceRegistry.Register<ICostPlacesService, CostPlacesService>();
            serviceRegistry.Register<ICreditModificationBudgetsService, CreditModificationBudgetsService>();
            serviceRegistry.Register<ICreditModificationsService, CreditModificationsService>();
            serviceRegistry.Register<ICreditModificationTypesService, CreditModificationTypesService>();
            serviceRegistry.Register<IDocumentTypesService, DocumentTypesService>();
            serviceRegistry.Register<IExtraBudgetaryApplicationsService, ExtraBudgetaryApplicationsService>();
            serviceRegistry.Register<IExtraBudgetaryRecordsService, ExtraBudgetaryRecordsService>();
            serviceRegistry.Register<IOriginsService, OriginsService>();
            serviceRegistry.Register<IParametersService, ParametersService>();
            serviceRegistry.Register<IPayFormsService, PayFormsService>();
            serviceRegistry.Register<IPayTypesService, PayTypesService>();
            serviceRegistry.Register<IProgramsService, ProgramsService>();
            serviceRegistry.Register<IProvenancesService, ProvenancesService>();
            serviceRegistry.Register<IProvidersService, ProvidersService>();
            serviceRegistry.Register<IProvincesService, ProvincesService>();
            serviceRegistry.Register<IRecordTypesService, RecordTypesService>();
            serviceRegistry.Register<IRectificationsService, RectificationsService>();
            serviceRegistry.Register<ISingsService, SingsService>();
            serviceRegistry.Register<ITonnageSheetsService, TonnageSheetsService>();
            serviceRegistry.Register<ITreasuriesService, TreasuriesService>();
            serviceRegistry.Register<ITreasuryLinesService, TreasuryLinesService>();
            serviceRegistry.Register<IUserService, UserService>();
        }

        #endregion
    }
}