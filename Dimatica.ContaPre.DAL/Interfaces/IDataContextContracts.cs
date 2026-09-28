using System;
using System.Collections.Generic;

using Dimatica.ContaPre.OL.Business;
using Dimatica.ContaPre.OL.Models;
using Dimatica.ContaPre.OL.Procedures;

namespace Dimatica.ContaPre.DAL.Interfaces
{
    public interface IAccountingDocumentsDataContext
    {
        PRE_DOCUMENTO_CONTABLE GetById(int id);
        List<PRE_DOCUMENTO_CONTABLE> GetByIdAndType(string type, int? accountingRecordId);
        Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> GetByIdReport(int id);
        Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> GetSpendRcById(int id);
        Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> GetSpendAdById(int id);
        Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> GetSpendOById(int id);
        Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> GetSpendPById(int id);
        Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>, List<PRE_EXPEDIENTE_CONTABLE>> GetSpendIncomeDiscountsById(int id);
        Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, PRE_DOCUMENTO_APLICACION, List<PRE_EXP_EXTRAPRE>> GetSpendPRecordById(int id);
        Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>, List<PRE_EXPEDIENTE_CONTABLE>> GetSpendORecordById(int id);
        Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, PRE_DOCUMENTO_APLICACION, List<PRE_EXPEDIENTE_CONTABLE>> GetSpendPRecordDiscountsById(int id);
        int GetNextTonnageSheetNumber(int year);
        int GetNextTonnageSheetNumber50(int year);
        int GetLastMiNumber(int year);
        int CheckMiNumber(int year, int miNumber, int documentId, int id);
        int CheckTonnageSheet(int tonnageSheetYear, int tonnageSheet, bool is50);
        Response InsertAccountingDocument(PRE_DOCUMENTO_CONTABLE accountingDocument);
        Response UpdateAccountingDocument(PRE_DOCUMENTO_CONTABLE accountingDocument);
        Response DeleteAccountingDocument(int accountingId, int userId);
        Response UpdateTonnageSheet(int? tonnageSheet, int? tonnageSheetYear, int? tonnageSheet50, int? tonnageSheet50Year, int account, int userId);
        List<PRE_DOCUMENTO_APLICACION> GetApplicationsByDocumentId(int documentId);
        List<PRE_DOCUMENTO_APLICACION> GetApplicationsByDocumentCacs(int documentId, string cacsCode);
        Response InsertApplication(PRE_DOCUMENTO_APLICACION application);
        Response UpdateApplication(PRE_DOCUMENTO_APLICACION application);
        Response DeleteApplication(int id, int userId);
        Response UpdateIncomeDiscounts(PRE_DOCUMENTO_CONTABLE accountingDocument);
        Response UpdateDcDescription(PRE_DOCUMENTO_CONTABLE accountingDocument);
        Response UpdateRepair(PRE_DOCUMENTO_CONTABLE accountingDocument);
        List<PRE_FACTURA_COMPRA> GetPurchases(int accountingDocumentId);
        int GetPurchasesCount(int accountingDocumentId);
        int GetIncomeDiscountsCount(int accountingDocumentId);
        int GetDocumentsCount(int accountingDocumentId);
        bool HaveAdPhase(int accountingDocumentId);
        bool HaveOPhase(int accountingDocumentId);
        bool HavePPhase(int accountingDocumentId);
        bool HaveExtraBudgetaries(int accountingDocumentId);
        bool HaveIncomeDiscounts(int accountingDocumentId);
        bool HaveRcPhase(int accountingDocumentId);
        PRE_EXPEDIENTE_CONTABLE GetAccountingRecord(int accountingDocumentId);
        int GetProviderDc(int accountingDocumentId);
        Response UpdateBillConcept(int id, string concept, int userId);
        Response UpdateTransferNumber(int id, string checkNumber, int userId);
        Response UpdatePayBankOracle(DateTime? payBankDate, int billCode);
        Response UpdateHistoryOracle(string certificate, string description);
    }

    public interface IAccountingRecordsDataContext
    {
        List<PRE_EXPEDIENTE_CONTABLE> GetSpends(int? year, int? recordNumberYear, int? provenanceCode, int? providerCode, string providerNif, bool? multiYear, string order, string orderSent);
        List<PRE_EXPEDIENTE_CONTABLE> GetSpendsReports(int? year, int? recordNumberYear, int? provenanceCode, int? providerCode, string providerNif, bool? multiYear);
        List<PRE_EXPEDIENTE_CONTABLE> GetSpendsWithAppAmount(int? year, int? chapterCode, int? articleCode, int? conceptCode, int? subConceptCode, decimal? sinceAmount, decimal? untilAmount, bool? isBound, bool? isSquare, int? providerCode, string providerNif, int? documentTypeCode, int? docNumber, string order, string sense);
        List<PRE_EXPEDIENTE_CONTABLE> GetIncomes(int year, string description, bool? isSquare, int? providerCode, int? operationYear, string type, int? docNumber, string order);
        List<PRE_EXPEDIENTE_CONTABLE> GetIncomesWithAppAmount(int? year, string description, bool? isSquare, int? providerCode, string sinceDate, string untilDate, decimal? sinceAmount, decimal? untilAmount, int? chapterCode, int? articleCode, int? conceptCode, int? subConceptCode, bool? isBound, int? operationYear, string type, int? docNumber, string order, string sense);
        List<PRE_EXPEDIENTE_CONTABLE> GetByAdministrativeRecord(int administrativeRecordId);
        PRE_EXPEDIENTE_CONTABLE GetById(int id);
        List<PRE_EXPEDIENTE_CONTABLE> GetIncomesDr(int? budgetYear, int? exerciseYear, int? groupNumber, int? documentCode);
        Tuple<PRE_DOCUMENTO_CONTABLE, List<PRE_DOCUMENTO_APLICACION>, PRE_PARAMETROS> GetSpendCertificate(int documentCode);
        Tuple<List<PRE_EXPEDIENTE_CONTABLE>, PRE_PARAMETROS> GetIncomesDrReport(int? budgetYear, int? exerciseYear, int? groupNumber, int? documentCode);
        List<PRE_EXPEDIENTE_CONTABLE> GetIncomesAnnexedDr(int? budgetYear, int? exerciseYear, int? groupNumber, int? documentCode);
        List<PRE_FACTURA_COMPRA> GetPurchaseBills(int accountingId, int documentId);
        List<PRE_PROVEEDOR> GetProviders(int accountingId);
        List<PRE_EXPEDIENTE_CONTABLE> GetMultiYears(int accountingId);
        List<PRE_FACTURA_COMPRA> GetProvidersPurchaseBills(int fileId, string date, int providerId);
        decimal GetDrAmount(int id);
        decimal GetMiAmount(int id);
        decimal GetRcAmount(int accountingId, bool isPositive, int? documentId);
        decimal GetAdAmount(int accountingId, bool isPositive, int? documentId);
        decimal GetOAmount(int accountingId, bool isPositive, int? documentId);
        decimal GetPAmount(int accountingId, bool isPositive, int? documentId);
        decimal GetDiscountIEcAmount(int accountingId);
        decimal GetApplicationsAmount(int documentId);
        decimal GetBillsAmount(int documentId);
        int GetLastYearNumber(int year, string type);
        int GetLastDrNumber(int year);
        List<Year> GetYears(string type);
        Response InsertAccountingRecord(PRE_EXPEDIENTE_CONTABLE accountingRecord);
        Response UpdateAccountingRecord(PRE_EXPEDIENTE_CONTABLE accountingRecord);
        Response UpdateAccountingRecordSquare(int id, bool square, int userId);
        Response DeleteAccountingRecord(int id, int userId);
        Response GroupDr(int groupNumber, int userId, List<int> documents);
        List<PRE_EXPEDIENTE_CONTABLE> GetSpendsByConcept(int year, string cacsCode, DateTime? since, DateTime? until);
        List<PRE_EXPEDIENTE_CONTABLE> GetIncomesByConcept(int year, string cacsCode, string since, string until);
        List<PRE_EXPEDIENTE_CONTABLE> GetIncomesByConceptDrMi(int year, string cacsCode, DateTime? since, DateTime? until);
        List<PRE_EXPEDIENTE_CONTABLE> GetIncomesByPlace(int year, int? place, int since, int until);
        List<PRE_EXPEDIENTE_CONTABLE> GetSpendsByPlace(int year, int? place, int since, int until, string cacsCode);
        List<PRE_EXPEDIENTE_CONTABLE> GetIncomesDrAgreement(DateTime since, DateTime until);
        List<PRE_EXPEDIENTE_CONTABLE> GetIncomesPendingRightsRecognized(DateTime date);
    }

    public interface IAccountPgcpDataContext
    {
        List<PRE_CUENTA_PGCP> GetAccountsToCombo();
    }

    public interface IAccountRestrictedDataContext
    {
        List<PRE_CUENTA_RESTRINGIDA> GetAccountsRestricted(string type);
        List<PRE_HOJA_ARQUEO> GetStatementAccounts(int? exerciseYear, int? budgetYear, int restrictedAccount, string sinceDate, string untilDate);
        List<PRE_CUENTA_RESTRINGIDA> GetAccountsToCombo(string type);
        Response GetIdByDescription(string description, string type);
        Response InsertAccount(PRE_CUENTA_RESTRINGIDA account);
        Response UpdateAccount(PRE_CUENTA_RESTRINGIDA account);
        Response DeleteAccount(int accountId);
    }

    public interface IAdministrativeRecordsDataContext
    {
        List<PRE_EXPEDIENTE_ADMINISTRATIVO> GetSpends(int? exerciseYear, int? recordNumber, int? provenanceId, string description);
        PRE_EXPEDIENTE_ADMINISTRATIVO GetById(int administrativeRecordId);
        Response InsertAdministrativeRecord(PRE_EXPEDIENTE_ADMINISTRATIVO administrativeRecord);
        Response UpdateAdministrativeRecord(PRE_EXPEDIENTE_ADMINISTRATIVO administrativeRecord);
        Response DeleteAdministrativeRecord(int id, int userId);
        int GetNextOrder(int year, int provenanceId);
    }

    public interface IApplicationDataContext
    {
        List<Application> GetApplications(string type);
        Response UpdateApplication(Application application, int userId);
        Response UpdateStatus(Application application, int userId);
        Response DeleteApplication(Application application);
        List<PRE_CAPITULO> GetChapters(string type);
        List<PRE_ARTICULO> GetArticles(string type, int chapterCode);
        List<PRE_CONCEPTO> GetConcepts(string type, int articleCode);
        List<PRE_SUBCONCEPTO> GetSubconcepts(string type, int conceptCode);
        Response InsertChapter(PRE_CAPITULO chapter);
        Response InsertArticle(PRE_ARTICULO article);
        Response InsertConcept(PRE_CONCEPTO concept);
        Response InsertSuboncept(PRE_SUBCONCEPTO subconcept);
    }

    public interface IBillPurchasesDataContext
    {
        List<PRE_FACTURA_COMPRA> GetBillPurchases(int? exerciseYear, int? administrativeId, string billNumber, string billDate, decimal? billAmount, string roDate, int? providerId, decimal? roAmount, string empty);
        List<PRE_FACTURA_COMPRA> GetByDocument(int documentId);
        List<PRE_FACTURA_COMPRA> GetByAccountingRecord(int accountingRecordId);
        Response DeleteByAccountingRecord(int accountingRecordId);
        Response DeleteBillPurchase(int id);
        Response UpdateDocument(int accountingId, string date, int providerId, int documentId, int userId);
        decimal GetDocumentRetentionAmount(int documentId);
        decimal GetDocumentBoeAmount(int documentId);
    }

    public interface IBudgetApplicationsDataContext
    {
        List<BudgetApplication> GetNumbersByTypeByYear(string type, int year);
        List<BudgetApplication> GetChaptersNumbersByTypeByYear(string type, int year);
        Response GetApplicationInfo(string cacsCode, int year, string type);
    }

    public interface IBudgetsDataContext
    {
        List<Year> GetYearsByType(string type);
        List<Budget> GetBudgetsByType(int year, string type);
        List<Budget> GetIncomeLevelCompliance(int year);
        List<Budget> GetSpendLevelCompliance(int year);
        List<Budget> GetBudgetsToNewByType(string type);
        int GetProgramCodeByYear(int year);
        Response InsertMany(int year, string type, List<Budget> budgets, int userId);
        Response UpdateMany(string type, List<Budget> budgets, int userId);
        Response AddMany(int chapterId, int year, string type, int userId);
        Response DeleteMany(int chapterId, int year, string type, int userId);
        Response CloseBudget(int year, string type, int userId);
        Response DeleteBudget(int year, string type, int userId);
        Response CheckApplicationAmount(string cacsCode, int year, string type);
        Response CheckChapterApplicationAmount(string cacsCode, int year, string type);
        List<Budget> GetSpendProvisionalStatus(int year, DateTime date, bool withOutPending);
        List<Budget> GetSpendComplianceGrade(int year, DateTime since, DateTime until);
        List<Budget> GetIncomeProvisionalStatus(int year, DateTime date, bool withOutPending);
    }

    public interface IContractTypesDataContext
    {
        List<PRE_TIPO_CONTRATO> GetContractTypes();
    }

    public interface ICostPlacesDataContext
    {
        List<PRE_CENTRO_COSTE> GetCostPlacesToCombo();
        List<PRE_CENTRO_COSTE> GetCostPlacesOriginToCombo();
        Response GetIdByDescription(string description);
    }

    public interface ICreditModificationBudgetsDataContext
    {
        List<PRE_MODIF_CREDITO_PRESUPUESTO> GetByCreditModification(int creditModificationId, string type);
        Response InsertCreditModificationBudget(PRE_MODIF_CREDITO_PRESUPUESTO creditModificationBudget, int year);
        Response UpdateCreditModificationBudget(PRE_MODIF_CREDITO_PRESUPUESTO creditModificationBudget, int year);
        Response DeleteCreditModificationBudget(int budgetId, int creditModificationId, string type, int userId);
    }

    public interface ICreditModificationsDataContext
    {
        List<Year> GetYears();
        List<PRE_MODIFICACION_CREDITO> GetByFilters(int year, int? since, int? until);
        PRE_MODIFICACION_CREDITO GetById(int creditModificationId);
        int GetNextOrderByYear(int year);
        Response InsertCreditModification(PRE_MODIFICACION_CREDITO creditModification);
        Response UpdateCreditModification(PRE_MODIFICACION_CREDITO creditModification);
        Response GenerateRecordsCreditModification(PRE_MODIFICACION_CREDITO creditModification);
        Response ExecuteCreditModification(int creditModificationId, int userId);
    }

    public interface ICreditModificationTypesDataContext
    {
        List<PRE_TIPO_MODIFICACION_CREDITO> GetCreditModificationTypes(string type);
        Response InsertCreditModificationType(PRE_TIPO_MODIFICACION_CREDITO creditModificationType);
        Response UpdateCreditModificationType(PRE_TIPO_MODIFICACION_CREDITO creditModificationType);
        Response DeleteCreditModificationType(int creditModificationTypeId);
    }

    public interface IDocumentTypesDataContext
    {
        List<PRE_TIPO_DOCUMENTO> GetDocumentTypes(string type);
        List<PRE_TIPO_DOCUMENTO> GetDocumentTypesToCombo(string type);
        List<PRE_TIPO_DOCUMENTO> GetDocumentTypesDrPhase();
        Response InsertDocumentType(PRE_TIPO_DOCUMENTO documentType);
        Response UpdateDocumentType(PRE_TIPO_DOCUMENTO documentType);
        Response DeleteDocumentType(int documentTypeId);
    }

    public interface IExtraBudgetaryApplicationsDataContext
    {
        List<PRE_EXTRAPRESUPUESTARIA> GetExtraBudgetaryApplications();
        List<PRE_EXTRAPRESUPUESTARIA> GetExtraBudgetaryApplicationsToCombo();
        List<PRE_TIPO_EXTRAP> GetExtraBudgetaryTypes();
        Response InsertExtraBudgetaryApplication(PRE_EXTRAPRESUPUESTARIA application);
        Response UpdateExtraBudgetaryApplication(PRE_EXTRAPRESUPUESTARIA application);
        Response DeleteExtraBudgetaryApplication(int applicationId);
        int GetNextNumber(int year);
        List<PRE_EXP_EXTRAPRE> GetStatus(int extraBudgetaryApplication, int year);
        PRE_EXP_EXTRAPRE GetInfo(int extraBudgetaryApplication);
    }

    public interface IExtraBudgetaryRecordsDataContext
    {
        List<PRE_EXP_EXTRAPRE> GetByDocumentId(int accountingDocumentId, int type);
        List<PRE_EXP_EXTRAPRE> GetByFilters(int? year, int? extraBudgetaryType, int? extraBudgetaryApplication, bool? isBound, string sinceDate, string untilDate, int? fileNumberSince, int? fileNumberUntil, int? providerCode);
        List<PRE_EXP_EXTRAPRE> GetByBoundId(int extraBudgetaryId);
        Tuple<PRE_PARAMETROS, PRE_EXP_EXTRAPRE> GetMi(int extraBudgetaryId);
        Tuple<PRE_EXP_EXTRAPRE, PRE_PARAMETROS, List<PRE_EXP_EXTRAPRE>> GetPmp(int extraBudgetaryId);
        List<PRE_EXP_EXTRAPRE> GetDebitAndCredit(int extraBudgetaryId, DateTime since, DateTime until);
        PRE_EXP_EXTRAPRE GetById(int extraBudgetaryId);
        Response InsertExtraBudgetary(PRE_EXP_EXTRAPRE extraBudgetary);
        Response UpdateExtraBudgetaryAll(PRE_EXP_EXTRAPRE extraBudgetary);
        Response UpdateExtraBudgetary(PRE_EXP_EXTRAPRE extraBudgetary);
        Response DeleteExtraBudgetary(int extraBudgetaryId, int userId);
        Response DeleteExtraBudgetaryDiscount(int extraBudgetaryId);
        decimal GetSumAmount(int accountingId);
        decimal GetSumBoundAmount(int extraBudgetaryId);
        Response UpdateRepair(PRE_DOCUMENTO_CONTABLE accountingDocument);
    }

    public interface IOriginsDataContext
    {
        List<PRE_ORIGEN> GetOrigins();
    }

    public interface IParametersDataContext
    {
        List<PRE_PARAMETROS> GetParameters();
        Response UpdateParameter(PRE_PARAMETROS parameters);
    }

    public interface IPayFormsDataContext
    {
        List<PRE_FORMA_PAGO> GetPayForms();
        Response InsertPayForm(PRE_FORMA_PAGO payForm);
        Response UpdatePayForm(PRE_FORMA_PAGO payForm);
        Response DeletePayForm(byte payFormId);
    }

    public interface IPayTypesDataContext
    {
        List<PRE_TIPO_PAGO> GetPayTypes();
        Response InsertPayType(PRE_TIPO_PAGO payType);
        Response UpdatePayType(PRE_TIPO_PAGO payType);
        Response DeletePayType(byte payTypeId);
    }

    public interface IProgramsDataContext
    {
        List<PRE_PROGRAMA> GetPrograms();
        Response InsertProgram(PRE_PROGRAMA program);
        Response UpdateProgram(PRE_PROGRAMA program);
        Response DeleteProgram(byte programId);
    }

    public interface IProvenancesDataContext
    {
        List<PRE_PROCEDENCIA> GetProvenances(string type);
        Response GetIdByDescription(string description, string type);
        Response InsertProvenance(PRE_PROCEDENCIA provenance);
        Response UpdateProvenance(PRE_PROCEDENCIA provenance);
        Response DeleteProvenance(int provenanceId);
    }

    public interface IProvidersDataContext
    {
        PRE_PROVEEDOR GetById(int providerId);
        List<PRE_PROVEEDOR> GetProviders();
        List<PRE_PROVEEDOR> GetProvidersToCombo();
        List<PRE_PROVEEDOR> GetBillProvidersToCombo();
        List<PRE_PROVEEDOR> GetProvidersByNameAndNif(string name, string nif);
        List<PRE_PROVEEDOR> FindProviders(string name, string nif);
        Response InsertProvider(PRE_PROVEEDOR provider);
        Response UpdateProvider(PRE_PROVEEDOR provider);
        Response DeleteProvider(int providerId);
        Response UpdateBranchDatas(int id, string ccCe, string ccCo, string ccDc, string ccNc, string branchName, string branchAddress, string branchLocation, int userId);
        List<PRE_PROVEEDOR> GetByYearHasSpend(int year);
    }

    public interface IProvincesDataContext
    {
        List<Provincia> GetProvincesToCombo();
    }

    public interface IRecordTypesDataContext
    {
        List<PRE_TIPO_REGISTRO> GetRecordTypes();
    }

    public interface IRectificationsDataContext
    {
        List<Rectification> GetRectifications(string sign, string type, int year);
        List<Rectification> GetRectificationsByCodes(string iCodes, string eCodes);
    }

    public interface ISingsDataContext
    {
        List<PRE_SENALAMIENTO> GetByFilters(int year, int? pointingNumber, string date, decimal? amount, int? fileNumber, string type);
        PRE_SENALAMIENTO GetById(int id);
        List<PRE_SENALAMIENTO_DOCUMENTO> GetDocumentsByPointingId(int pointingId);
        List<PRE_SENALAMIENTO_DOCUMENTO> GetTransfersByPointingId(int pointingId, bool groupTransfers);
        List<PRE_DOCUMENTO_CONTABLE> GetPendingDocuments(int year, string documentType);
        Response InsertPointing(PRE_SENALAMIENTO pointing);
        Response DeletePointing(int id, int userId);
        int GetNextNumber(int year);
        Response InsertDocumentSing(int pointingId, int? documentCode, int? fileCode, int userId);
        Response DeleteDocument(int id, int userId);
    }

    public interface ITonnageSheetsDataContext
    {
        PRE_DETALLE_HOJA_ARQUEO GetDetailByFilters(int tonnageSheetCode, int? fileNumber, int? lineNumber);
        Response InsertTonnageSheet(PRE_HOJA_ARQUEO tonnageSheet);
        Response UpdateTonnageSheet(PRE_HOJA_ARQUEO tonnageSheet);
        Response InsertTonnageSheetDetail(PRE_DETALLE_HOJA_ARQUEO tonnageSheetDetail);
        Response UpdateTonnageSheetDetail(int tonnageSheetDetailCode, bool check, int userId);
        PRE_HOJA_ARQUEO GetById(int id);
        List<PRE_HOJA_ARQUEO> GetRestrictedAccountStatement(int? exerciseYear, int? budgetYear, int accountId, string sinceDate, string untilDate);
        int GetTonnageSheetCode(int exerciseYear, int number, bool isFifty);
        List<PRE_HOJA_ARQUEO> GetTonnageSheetsDetails(int exerciseYear, int number, bool isFifty);
        List<PRE_HOJA_ARQUEO> GetTonnageSheetsDetailsWithoutSheet(int exerciseYear, int number, bool isFifty);
        List<PRE_HOJA_ARQUEO> GetByFilters(int? exerciseYear, int? accountingCode, int? sheetSinceNumber, int? sheetUntilNumber, string sheetDateSince, string sheetDateUntil, string order, string sense);
        List<PRE_DETALLE_HOJA_ARQUEO> GetDetailsById(int tonnageSheetId);
        Response DeleteDetail(int detailId, int userId);
        Response DeleteTonnageSheet(int id, int userId);
    }

    public interface ITreasuriesDataContext
    {
        PRE_TESORERIA GetById(int treasuryId);
        List<PRE_TESORERIA> GetAllById(int treasuryId);
        List<PRE_TESORERIA> GetDocuments(int? documentId, int? extraBudgetaryId, int? treasuryId);
        List<PRE_DOCUMENTO_CONTABLE> GetDocumentsToBound(int? budgetYear, decimal? amount, int? providerCode, string checkNumber, int originCode);
        List<PRE_DOCUMENTO_CONTABLE> GetPaymentsRegister(int year, int? originCode, int? providerIncomes, int? providerSpends, string sinceDate, string untilDate);
        List<PRE_TESORERIA> GetTreasuryByFilters(int? exerciseYear, int? origingCode, decimal? importe, string sinceBankDate, string untilBankDate, int? payFormCode, string checkNumber, bool? treasuryHave, string description, string sinceDateEntry, string untilDateEntry, int? registerTypeCode, bool? isCanceled, bool? isBound, string pendingDate, int? sinceDocYear, int? untilDocYear, string order);
        List<PRE_TESORERIA> GetAccountingBook(DateTime since, DateTime until);
        List<PRE_TESORERIA> GetBankStatement(DateTime since, DateTime until);
        List<PRE_TESORERIA> GetBlockListing(DateTime register);
        List<PRE_TESORERIA> GetPaymentRecord(DateTime since, DateTime until);
        List<PRE_DETALLE_HOJA_ARQUEO> GetTonnageSheetDetails(int tonnageSheetCode);
        int GetBoundDocumentsCount(int treasuryId);
        Response InsertTreasury(PRE_TESORERIA treasury);
        Response UpdateTreasury(PRE_TESORERIA treasury);
        Response DeleteBankDate(int treasuryId, int userId);
        Response DeleteTreasury(int treasuryId, int userId);
        Response DeleteDocument(int treasuryDocumentId, int userId);
        decimal GetAmount(int treasuryId);
        decimal GetDocumentsAmount(int treasuryId);
        Response BoundDocument(PRE_TESORERIA_DOCUMENTO document);
        bool FoundTreasuryOracle(int treasuryId);
        string GetRecognizedRightsOracle(int treasuryId);
        Response InsertTreasuryOracle(PRE_TESORERIA treasury);
        Response UpdateTreasuryOracle(PRE_TESORERIA treasury);
        Response DeleteTreasuryOracle(int treasuryId);
        Response DeleteBankDateOracle(int treasuryId);
    }

    public interface ITreasuryLinesDataContext
    {
        List<PRE_LINEA_TESORERIA> GetTreasuryLines();
        Response InsertTreasuryLine(PRE_LINEA_TESORERIA treasuryLine);
        Response UpdateTreasuryLine(PRE_LINEA_TESORERIA treasuryLine);
        Response DeleteTreasuryLine(int treasuryLineId);
    }

    public interface IUserDataContext
    {
        List<User> GetUsers();
        User Login(string login, string password);
        Response InsertUser(User user);
        Response UpdateUser(User user);
        Response UpdatePassword(User user);
        Response UpdateStatus(int userId, bool obsolete);
        Response DeleteUser(int userId);
        User GetUserByCode(int userId);
        User GetUserByEmail(string email);
        User GetUserByUserName(string usu_login);
    }

}
