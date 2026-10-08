namespace Dimatica.ContaPre.UnitTest.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;

    using Dimatica.ContaPre.BLL.Services;
    using Dimatica.ContaPre.DAL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.OL.Procedures;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public sealed class RecordingDataContext :
        IAccountingDocumentsDataContext,
        IAccountingRecordsDataContext,
        IAccountPgcpDataContext,
        IAccountRestrictedDataContext,
        IAdministrativeRecordsDataContext,
        IApplicationDataContext,
        IBillPurchasesDataContext,
        IBudgetApplicationsDataContext,
        IBudgetsDataContext,
        IContractTypesDataContext,
        ICostPlacesDataContext,
        ICreditModificationBudgetsDataContext,
        ICreditModificationsDataContext,
        ICreditModificationTypesDataContext,
        IDocumentTypesDataContext,
        IExtraBudgetaryApplicationsDataContext,
        IExtraBudgetaryRecordsDataContext,
        IOriginsDataContext,
        IParametersDataContext,
        IPayFormsDataContext,
        IPayTypesDataContext,
        IProgramsDataContext,
        IProvenancesDataContext,
        IProvidersDataContext,
        IProvincesDataContext,
        IRecordTypesDataContext,
        IRectificationsDataContext,
        ISingsDataContext,
        ITonnageSheetsDataContext,
        ITreasuriesDataContext,
        ITreasuryLinesDataContext,
        IUserDataContext
    {
        public object ReturnValue { get; set; }

        public string LastMethodName { get; private set; }

        public object[] LastArguments { get; private set; }

        public void Reset()
        {
            LastMethodName = null;
            LastArguments = null;
        }

        private T RecordAndReturn<T>(string methodName, object[] arguments)
        {
            LastMethodName = methodName;
            LastArguments = arguments;
            return ReturnValue == null ? default(T) : (T)ReturnValue;
        }

        PRE_DOCUMENTO_CONTABLE IAccountingDocumentsDataContext.GetById(int id)
        {
            return RecordAndReturn<PRE_DOCUMENTO_CONTABLE>("IAccountingDocumentsDataContext.GetById", new object[] { id });
        }

        List<PRE_DOCUMENTO_CONTABLE> IAccountingDocumentsDataContext.GetByIdAndType(string type, int? accountingRecordId)
        {
            return RecordAndReturn<List<PRE_DOCUMENTO_CONTABLE>>("IAccountingDocumentsDataContext.GetByIdAndType", new object[] { type, accountingRecordId });
        }

        Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> IAccountingDocumentsDataContext.GetByIdReport(int id)
        {
            return RecordAndReturn<Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>>>("IAccountingDocumentsDataContext.GetByIdReport", new object[] { id });
        }

        Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> IAccountingDocumentsDataContext.GetSpendRcById(int id)
        {
            return RecordAndReturn<Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>>>("IAccountingDocumentsDataContext.GetSpendRcById", new object[] { id });
        }

        Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> IAccountingDocumentsDataContext.GetSpendAdById(int id)
        {
            return RecordAndReturn<Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>>>("IAccountingDocumentsDataContext.GetSpendAdById", new object[] { id });
        }

        Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> IAccountingDocumentsDataContext.GetSpendOById(int id)
        {
            return RecordAndReturn<Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>>>("IAccountingDocumentsDataContext.GetSpendOById", new object[] { id });
        }

        Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> IAccountingDocumentsDataContext.GetSpendPById(int id)
        {
            return RecordAndReturn<Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>>>("IAccountingDocumentsDataContext.GetSpendPById", new object[] { id });
        }

        Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>, List<PRE_EXPEDIENTE_CONTABLE>> IAccountingDocumentsDataContext.GetSpendIncomeDiscountsById(int id)
        {
            return RecordAndReturn<Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>, List<PRE_EXPEDIENTE_CONTABLE>>>("IAccountingDocumentsDataContext.GetSpendIncomeDiscountsById", new object[] { id });
        }

        Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, PRE_DOCUMENTO_APLICACION, List<PRE_EXP_EXTRAPRE>> IAccountingDocumentsDataContext.GetSpendPRecordById(int id)
        {
            return RecordAndReturn<Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, PRE_DOCUMENTO_APLICACION, List<PRE_EXP_EXTRAPRE>>>("IAccountingDocumentsDataContext.GetSpendPRecordById", new object[] { id });
        }

        Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>, List<PRE_EXPEDIENTE_CONTABLE>> IAccountingDocumentsDataContext.GetSpendORecordById(int id)
        {
            return RecordAndReturn<Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>, List<PRE_EXPEDIENTE_CONTABLE>>>("IAccountingDocumentsDataContext.GetSpendORecordById", new object[] { id });
        }

        Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, PRE_DOCUMENTO_APLICACION, List<PRE_EXPEDIENTE_CONTABLE>> IAccountingDocumentsDataContext.GetSpendPRecordDiscountsById(int id)
        {
            return RecordAndReturn<Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, PRE_DOCUMENTO_APLICACION, List<PRE_EXPEDIENTE_CONTABLE>>>("IAccountingDocumentsDataContext.GetSpendPRecordDiscountsById", new object[] { id });
        }

        int IAccountingDocumentsDataContext.GetNextTonnageSheetNumber(int year)
        {
            return RecordAndReturn<int>("IAccountingDocumentsDataContext.GetNextTonnageSheetNumber", new object[] { year });
        }

        int IAccountingDocumentsDataContext.GetNextTonnageSheetNumber50(int year)
        {
            return RecordAndReturn<int>("IAccountingDocumentsDataContext.GetNextTonnageSheetNumber50", new object[] { year });
        }

        int IAccountingDocumentsDataContext.GetLastMiNumber(int year)
        {
            return RecordAndReturn<int>("IAccountingDocumentsDataContext.GetLastMiNumber", new object[] { year });
        }

        int IAccountingDocumentsDataContext.CheckMiNumber(int year, int miNumber, int documentId, int id)
        {
            return RecordAndReturn<int>("IAccountingDocumentsDataContext.CheckMiNumber", new object[] { year, miNumber, documentId, id });
        }

        int IAccountingDocumentsDataContext.CheckTonnageSheet(int tonnageSheetYear, int tonnageSheet, bool is50)
        {
            return RecordAndReturn<int>("IAccountingDocumentsDataContext.CheckTonnageSheet", new object[] { tonnageSheetYear, tonnageSheet, is50 });
        }

        Response IAccountingDocumentsDataContext.InsertAccountingDocument(PRE_DOCUMENTO_CONTABLE accountingDocument)
        {
            return RecordAndReturn<Response>("IAccountingDocumentsDataContext.InsertAccountingDocument", new object[] { accountingDocument });
        }

        Response IAccountingDocumentsDataContext.UpdateAccountingDocument(PRE_DOCUMENTO_CONTABLE accountingDocument)
        {
            return RecordAndReturn<Response>("IAccountingDocumentsDataContext.UpdateAccountingDocument", new object[] { accountingDocument });
        }

        Response IAccountingDocumentsDataContext.DeleteAccountingDocument(int accountingId, int userId)
        {
            return RecordAndReturn<Response>("IAccountingDocumentsDataContext.DeleteAccountingDocument", new object[] { accountingId, userId });
        }

        Response IAccountingDocumentsDataContext.UpdateTonnageSheet(int? tonnageSheet, int? tonnageSheetYear, int? tonnageSheet50, int? tonnageSheet50Year, int account, int userId)
        {
            return RecordAndReturn<Response>("IAccountingDocumentsDataContext.UpdateTonnageSheet", new object[] { tonnageSheet, tonnageSheetYear, tonnageSheet50, tonnageSheet50Year, account, userId });
        }

        List<PRE_DOCUMENTO_APLICACION> IAccountingDocumentsDataContext.GetApplicationsByDocumentId(int documentId)
        {
            return RecordAndReturn<List<PRE_DOCUMENTO_APLICACION>>("IAccountingDocumentsDataContext.GetApplicationsByDocumentId", new object[] { documentId });
        }

        List<PRE_DOCUMENTO_APLICACION> IAccountingDocumentsDataContext.GetApplicationsByDocumentCacs(int documentId, string cacsCode)
        {
            return RecordAndReturn<List<PRE_DOCUMENTO_APLICACION>>("IAccountingDocumentsDataContext.GetApplicationsByDocumentCacs", new object[] { documentId, cacsCode });
        }

        Response IAccountingDocumentsDataContext.InsertApplication(PRE_DOCUMENTO_APLICACION application)
        {
            return RecordAndReturn<Response>("IAccountingDocumentsDataContext.InsertApplication", new object[] { application });
        }

        Response IAccountingDocumentsDataContext.UpdateApplication(PRE_DOCUMENTO_APLICACION application)
        {
            return RecordAndReturn<Response>("IAccountingDocumentsDataContext.UpdateApplication", new object[] { application });
        }

        Response IAccountingDocumentsDataContext.DeleteApplication(int id, int userId)
        {
            return RecordAndReturn<Response>("IAccountingDocumentsDataContext.DeleteApplication", new object[] { id, userId });
        }

        Response IAccountingDocumentsDataContext.UpdateIncomeDiscounts(PRE_DOCUMENTO_CONTABLE accountingDocument)
        {
            return RecordAndReturn<Response>("IAccountingDocumentsDataContext.UpdateIncomeDiscounts", new object[] { accountingDocument });
        }

        Response IAccountingDocumentsDataContext.UpdateDcDescription(PRE_DOCUMENTO_CONTABLE accountingDocument)
        {
            return RecordAndReturn<Response>("IAccountingDocumentsDataContext.UpdateDcDescription", new object[] { accountingDocument });
        }

        Response IAccountingDocumentsDataContext.UpdateRepair(PRE_DOCUMENTO_CONTABLE accountingDocument)
        {
            return RecordAndReturn<Response>("IAccountingDocumentsDataContext.UpdateRepair", new object[] { accountingDocument });
        }

        List<PRE_FACTURA_COMPRA> IAccountingDocumentsDataContext.GetPurchases(int accountingDocumentId)
        {
            return RecordAndReturn<List<PRE_FACTURA_COMPRA>>("IAccountingDocumentsDataContext.GetPurchases", new object[] { accountingDocumentId });
        }

        int IAccountingDocumentsDataContext.GetPurchasesCount(int accountingDocumentId)
        {
            return RecordAndReturn<int>("IAccountingDocumentsDataContext.GetPurchasesCount", new object[] { accountingDocumentId });
        }

        int IAccountingDocumentsDataContext.GetIncomeDiscountsCount(int accountingDocumentId)
        {
            return RecordAndReturn<int>("IAccountingDocumentsDataContext.GetIncomeDiscountsCount", new object[] { accountingDocumentId });
        }

        int IAccountingDocumentsDataContext.GetDocumentsCount(int accountingDocumentId)
        {
            return RecordAndReturn<int>("IAccountingDocumentsDataContext.GetDocumentsCount", new object[] { accountingDocumentId });
        }

        bool IAccountingDocumentsDataContext.HaveAdPhase(int accountingDocumentId)
        {
            return RecordAndReturn<bool>("IAccountingDocumentsDataContext.HaveAdPhase", new object[] { accountingDocumentId });
        }

        bool IAccountingDocumentsDataContext.HaveOPhase(int accountingDocumentId)
        {
            return RecordAndReturn<bool>("IAccountingDocumentsDataContext.HaveOPhase", new object[] { accountingDocumentId });
        }

        bool IAccountingDocumentsDataContext.HavePPhase(int accountingDocumentId)
        {
            return RecordAndReturn<bool>("IAccountingDocumentsDataContext.HavePPhase", new object[] { accountingDocumentId });
        }

        bool IAccountingDocumentsDataContext.HaveExtraBudgetaries(int accountingDocumentId)
        {
            return RecordAndReturn<bool>("IAccountingDocumentsDataContext.HaveExtraBudgetaries", new object[] { accountingDocumentId });
        }

        bool IAccountingDocumentsDataContext.HaveIncomeDiscounts(int accountingDocumentId)
        {
            return RecordAndReturn<bool>("IAccountingDocumentsDataContext.HaveIncomeDiscounts", new object[] { accountingDocumentId });
        }

        bool IAccountingDocumentsDataContext.HaveRcPhase(int accountingDocumentId)
        {
            return RecordAndReturn<bool>("IAccountingDocumentsDataContext.HaveRcPhase", new object[] { accountingDocumentId });
        }

        PRE_EXPEDIENTE_CONTABLE IAccountingDocumentsDataContext.GetAccountingRecord(int accountingDocumentId)
        {
            return RecordAndReturn<PRE_EXPEDIENTE_CONTABLE>("IAccountingDocumentsDataContext.GetAccountingRecord", new object[] { accountingDocumentId });
        }

        int IAccountingDocumentsDataContext.GetProviderDc(int accountingDocumentId)
        {
            return RecordAndReturn<int>("IAccountingDocumentsDataContext.GetProviderDc", new object[] { accountingDocumentId });
        }

        Response IAccountingDocumentsDataContext.UpdateBillConcept(int id, string concept, int userId)
        {
            return RecordAndReturn<Response>("IAccountingDocumentsDataContext.UpdateBillConcept", new object[] { id, concept, userId });
        }

        Response IAccountingDocumentsDataContext.UpdateTransferNumber(int id, string checkNumber, int userId)
        {
            return RecordAndReturn<Response>("IAccountingDocumentsDataContext.UpdateTransferNumber", new object[] { id, checkNumber, userId });
        }

        Response IAccountingDocumentsDataContext.UpdatePayBankOracle(DateTime? payBankDate, int billCode)
        {
            return RecordAndReturn<Response>("IAccountingDocumentsDataContext.UpdatePayBankOracle", new object[] { payBankDate, billCode });
        }

        Response IAccountingDocumentsDataContext.UpdateHistoryOracle(string certificate, string description)
        {
            return RecordAndReturn<Response>("IAccountingDocumentsDataContext.UpdateHistoryOracle", new object[] { certificate, description });
        }

        List<PRE_EXPEDIENTE_CONTABLE> IAccountingRecordsDataContext.GetSpends(int? year, int? recordNumberYear, int? provenanceCode, int? providerCode, string providerNif, bool? multiYear, string order, string orderSent)
        {
            return RecordAndReturn<List<PRE_EXPEDIENTE_CONTABLE>>("IAccountingRecordsDataContext.GetSpends", new object[] { year, recordNumberYear, provenanceCode, providerCode, providerNif, multiYear, order, orderSent });
        }

        List<PRE_EXPEDIENTE_CONTABLE> IAccountingRecordsDataContext.GetSpendsReports(int? year, int? recordNumberYear, int? provenanceCode, int? providerCode, string providerNif, bool? multiYear)
        {
            return RecordAndReturn<List<PRE_EXPEDIENTE_CONTABLE>>("IAccountingRecordsDataContext.GetSpendsReports", new object[] { year, recordNumberYear, provenanceCode, providerCode, providerNif, multiYear });
        }

        List<PRE_EXPEDIENTE_CONTABLE> IAccountingRecordsDataContext.GetSpendsWithAppAmount(int? year, int? chapterCode, int? articleCode, int? conceptCode, int? subConceptCode, decimal? sinceAmount, decimal? untilAmount, bool? isBound, bool? isSquare, int? providerCode, string providerNif, int? documentTypeCode, int? docNumber, string order, string sense)
        {
            return RecordAndReturn<List<PRE_EXPEDIENTE_CONTABLE>>("IAccountingRecordsDataContext.GetSpendsWithAppAmount", new object[] { year, chapterCode, articleCode, conceptCode, subConceptCode, sinceAmount, untilAmount, isBound, isSquare, providerCode, providerNif, documentTypeCode, docNumber, order, sense });
        }

        List<PRE_EXPEDIENTE_CONTABLE> IAccountingRecordsDataContext.GetIncomes(int year, string description, bool? isSquare, int? providerCode, int? operationYear, string type, int? docNumber, string order)
        {
            return RecordAndReturn<List<PRE_EXPEDIENTE_CONTABLE>>("IAccountingRecordsDataContext.GetIncomes", new object[] { year, description, isSquare, providerCode, operationYear, type, docNumber, order });
        }

        List<PRE_EXPEDIENTE_CONTABLE> IAccountingRecordsDataContext.GetIncomesWithAppAmount(int? year, string description, bool? isSquare, int? providerCode, string sinceDate, string untilDate, decimal? sinceAmount, decimal? untilAmount, int? chapterCode, int? articleCode, int? conceptCode, int? subConceptCode, bool? isBound, int? operationYear, string type, int? docNumber, string order, string sense)
        {
            return RecordAndReturn<List<PRE_EXPEDIENTE_CONTABLE>>("IAccountingRecordsDataContext.GetIncomesWithAppAmount", new object[] { year, description, isSquare, providerCode, sinceDate, untilDate, sinceAmount, untilAmount, chapterCode, articleCode, conceptCode, subConceptCode, isBound, operationYear, type, docNumber, order, sense });
        }

        List<PRE_EXPEDIENTE_CONTABLE> IAccountingRecordsDataContext.GetByAdministrativeRecord(int administrativeRecordId)
        {
            return RecordAndReturn<List<PRE_EXPEDIENTE_CONTABLE>>("IAccountingRecordsDataContext.GetByAdministrativeRecord", new object[] { administrativeRecordId });
        }

        PRE_EXPEDIENTE_CONTABLE IAccountingRecordsDataContext.GetById(int id)
        {
            return RecordAndReturn<PRE_EXPEDIENTE_CONTABLE>("IAccountingRecordsDataContext.GetById", new object[] { id });
        }

        List<PRE_EXPEDIENTE_CONTABLE> IAccountingRecordsDataContext.GetIncomesDr(int? budgetYear, int? exerciseYear, int? groupNumber, int? documentCode)
        {
            return RecordAndReturn<List<PRE_EXPEDIENTE_CONTABLE>>("IAccountingRecordsDataContext.GetIncomesDr", new object[] { budgetYear, exerciseYear, groupNumber, documentCode });
        }

        Tuple<PRE_DOCUMENTO_CONTABLE, List<PRE_DOCUMENTO_APLICACION>, PRE_PARAMETROS> IAccountingRecordsDataContext.GetSpendCertificate(int documentCode)
        {
            return RecordAndReturn<Tuple<PRE_DOCUMENTO_CONTABLE, List<PRE_DOCUMENTO_APLICACION>, PRE_PARAMETROS>>("IAccountingRecordsDataContext.GetSpendCertificate", new object[] { documentCode });
        }

        Tuple<List<PRE_EXPEDIENTE_CONTABLE>, PRE_PARAMETROS> IAccountingRecordsDataContext.GetIncomesDrReport(int? budgetYear, int? exerciseYear, int? groupNumber, int? documentCode)
        {
            return RecordAndReturn<Tuple<List<PRE_EXPEDIENTE_CONTABLE>, PRE_PARAMETROS>>("IAccountingRecordsDataContext.GetIncomesDrReport", new object[] { budgetYear, exerciseYear, groupNumber, documentCode });
        }

        List<PRE_EXPEDIENTE_CONTABLE> IAccountingRecordsDataContext.GetIncomesAnnexedDr(int? budgetYear, int? exerciseYear, int? groupNumber, int? documentCode)
        {
            return RecordAndReturn<List<PRE_EXPEDIENTE_CONTABLE>>("IAccountingRecordsDataContext.GetIncomesAnnexedDr", new object[] { budgetYear, exerciseYear, groupNumber, documentCode });
        }

        List<PRE_FACTURA_COMPRA> IAccountingRecordsDataContext.GetPurchaseBills(int accountingId, int documentId)
        {
            return RecordAndReturn<List<PRE_FACTURA_COMPRA>>("IAccountingRecordsDataContext.GetPurchaseBills", new object[] { accountingId, documentId });
        }

        List<PRE_PROVEEDOR> IAccountingRecordsDataContext.GetProviders(int accountingId)
        {
            return RecordAndReturn<List<PRE_PROVEEDOR>>("IAccountingRecordsDataContext.GetProviders", new object[] { accountingId });
        }

        List<PRE_EXPEDIENTE_CONTABLE> IAccountingRecordsDataContext.GetMultiYears(int accountingId)
        {
            return RecordAndReturn<List<PRE_EXPEDIENTE_CONTABLE>>("IAccountingRecordsDataContext.GetMultiYears", new object[] { accountingId });
        }

        List<PRE_FACTURA_COMPRA> IAccountingRecordsDataContext.GetProvidersPurchaseBills(int fileId, string date, int providerId)
        {
            return RecordAndReturn<List<PRE_FACTURA_COMPRA>>("IAccountingRecordsDataContext.GetProvidersPurchaseBills", new object[] { fileId, date, providerId });
        }

        decimal IAccountingRecordsDataContext.GetDrAmount(int id)
        {
            return RecordAndReturn<decimal>("IAccountingRecordsDataContext.GetDrAmount", new object[] { id });
        }

        decimal IAccountingRecordsDataContext.GetMiAmount(int id)
        {
            return RecordAndReturn<decimal>("IAccountingRecordsDataContext.GetMiAmount", new object[] { id });
        }

        decimal IAccountingRecordsDataContext.GetRcAmount(int accountingId, bool isPositive, int? documentId)
        {
            return RecordAndReturn<decimal>("IAccountingRecordsDataContext.GetRcAmount", new object[] { accountingId, isPositive, documentId });
        }

        decimal IAccountingRecordsDataContext.GetAdAmount(int accountingId, bool isPositive, int? documentId)
        {
            return RecordAndReturn<decimal>("IAccountingRecordsDataContext.GetAdAmount", new object[] { accountingId, isPositive, documentId });
        }

        decimal IAccountingRecordsDataContext.GetOAmount(int accountingId, bool isPositive, int? documentId)
        {
            return RecordAndReturn<decimal>("IAccountingRecordsDataContext.GetOAmount", new object[] { accountingId, isPositive, documentId });
        }

        decimal IAccountingRecordsDataContext.GetPAmount(int accountingId, bool isPositive, int? documentId)
        {
            return RecordAndReturn<decimal>("IAccountingRecordsDataContext.GetPAmount", new object[] { accountingId, isPositive, documentId });
        }

        decimal IAccountingRecordsDataContext.GetDiscountIEcAmount(int accountingId)
        {
            return RecordAndReturn<decimal>("IAccountingRecordsDataContext.GetDiscountIEcAmount", new object[] { accountingId });
        }

        decimal IAccountingRecordsDataContext.GetApplicationsAmount(int documentId)
        {
            return RecordAndReturn<decimal>("IAccountingRecordsDataContext.GetApplicationsAmount", new object[] { documentId });
        }

        decimal IAccountingRecordsDataContext.GetBillsAmount(int documentId)
        {
            return RecordAndReturn<decimal>("IAccountingRecordsDataContext.GetBillsAmount", new object[] { documentId });
        }

        int IAccountingRecordsDataContext.GetLastYearNumber(int year, string type)
        {
            return RecordAndReturn<int>("IAccountingRecordsDataContext.GetLastYearNumber", new object[] { year, type });
        }

        int IAccountingRecordsDataContext.GetLastDrNumber(int year)
        {
            return RecordAndReturn<int>("IAccountingRecordsDataContext.GetLastDrNumber", new object[] { year });
        }

        List<Year> IAccountingRecordsDataContext.GetYears(string type)
        {
            return RecordAndReturn<List<Year>>("IAccountingRecordsDataContext.GetYears", new object[] { type });
        }

        Response IAccountingRecordsDataContext.InsertAccountingRecord(PRE_EXPEDIENTE_CONTABLE accountingRecord)
        {
            return RecordAndReturn<Response>("IAccountingRecordsDataContext.InsertAccountingRecord", new object[] { accountingRecord });
        }

        Response IAccountingRecordsDataContext.UpdateAccountingRecord(PRE_EXPEDIENTE_CONTABLE accountingRecord)
        {
            return RecordAndReturn<Response>("IAccountingRecordsDataContext.UpdateAccountingRecord", new object[] { accountingRecord });
        }

        Response IAccountingRecordsDataContext.UpdateAccountingRecordSquare(int id, bool square, int userId)
        {
            return RecordAndReturn<Response>("IAccountingRecordsDataContext.UpdateAccountingRecordSquare", new object[] { id, square, userId });
        }

        Response IAccountingRecordsDataContext.DeleteAccountingRecord(int id, int userId)
        {
            return RecordAndReturn<Response>("IAccountingRecordsDataContext.DeleteAccountingRecord", new object[] { id, userId });
        }

        Response IAccountingRecordsDataContext.GroupDr(int groupNumber, int userId, List<int> documents)
        {
            return RecordAndReturn<Response>("IAccountingRecordsDataContext.GroupDr", new object[] { groupNumber, userId, documents });
        }

        List<PRE_EXPEDIENTE_CONTABLE> IAccountingRecordsDataContext.GetSpendsByConcept(int year, string cacsCode, DateTime? since, DateTime? until)
        {
            return RecordAndReturn<List<PRE_EXPEDIENTE_CONTABLE>>("IAccountingRecordsDataContext.GetSpendsByConcept", new object[] { year, cacsCode, since, until });
        }

        List<PRE_EXPEDIENTE_CONTABLE> IAccountingRecordsDataContext.GetIncomesByConcept(int year, string cacsCode, string since, string until)
        {
            return RecordAndReturn<List<PRE_EXPEDIENTE_CONTABLE>>("IAccountingRecordsDataContext.GetIncomesByConcept", new object[] { year, cacsCode, since, until });
        }

        List<PRE_EXPEDIENTE_CONTABLE> IAccountingRecordsDataContext.GetIncomesByConceptDrMi(int year, string cacsCode, DateTime? since, DateTime? until)
        {
            return RecordAndReturn<List<PRE_EXPEDIENTE_CONTABLE>>("IAccountingRecordsDataContext.GetIncomesByConceptDrMi", new object[] { year, cacsCode, since, until });
        }

        List<PRE_EXPEDIENTE_CONTABLE> IAccountingRecordsDataContext.GetIncomesByPlace(int year, int? place, int since, int until)
        {
            return RecordAndReturn<List<PRE_EXPEDIENTE_CONTABLE>>("IAccountingRecordsDataContext.GetIncomesByPlace", new object[] { year, place, since, until });
        }

        List<PRE_EXPEDIENTE_CONTABLE> IAccountingRecordsDataContext.GetSpendsByPlace(int year, int? place, int since, int until, string cacsCode)
        {
            return RecordAndReturn<List<PRE_EXPEDIENTE_CONTABLE>>("IAccountingRecordsDataContext.GetSpendsByPlace", new object[] { year, place, since, until, cacsCode });
        }

        List<PRE_EXPEDIENTE_CONTABLE> IAccountingRecordsDataContext.GetIncomesDrAgreement(DateTime since, DateTime until)
        {
            return RecordAndReturn<List<PRE_EXPEDIENTE_CONTABLE>>("IAccountingRecordsDataContext.GetIncomesDrAgreement", new object[] { since, until });
        }

        List<PRE_EXPEDIENTE_CONTABLE> IAccountingRecordsDataContext.GetIncomesPendingRightsRecognized(DateTime date)
        {
            return RecordAndReturn<List<PRE_EXPEDIENTE_CONTABLE>>("IAccountingRecordsDataContext.GetIncomesPendingRightsRecognized", new object[] { date });
        }

        List<PRE_CUENTA_PGCP> IAccountPgcpDataContext.GetAccountsToCombo()
        {
            return RecordAndReturn<List<PRE_CUENTA_PGCP>>("IAccountPgcpDataContext.GetAccountsToCombo", new object[0]);
        }

        List<PRE_CUENTA_RESTRINGIDA> IAccountRestrictedDataContext.GetAccountsRestricted(string type)
        {
            return RecordAndReturn<List<PRE_CUENTA_RESTRINGIDA>>("IAccountRestrictedDataContext.GetAccountsRestricted", new object[] { type });
        }

        List<PRE_HOJA_ARQUEO> IAccountRestrictedDataContext.GetStatementAccounts(int? exerciseYear, int? budgetYear, int restrictedAccount, string sinceDate, string untilDate)
        {
            return RecordAndReturn<List<PRE_HOJA_ARQUEO>>("IAccountRestrictedDataContext.GetStatementAccounts", new object[] { exerciseYear, budgetYear, restrictedAccount, sinceDate, untilDate });
        }

        List<PRE_CUENTA_RESTRINGIDA> IAccountRestrictedDataContext.GetAccountsToCombo(string type)
        {
            return RecordAndReturn<List<PRE_CUENTA_RESTRINGIDA>>("IAccountRestrictedDataContext.GetAccountsToCombo", new object[] { type });
        }

        Response IAccountRestrictedDataContext.GetIdByDescription(string description, string type)
        {
            return RecordAndReturn<Response>("IAccountRestrictedDataContext.GetIdByDescription", new object[] { description, type });
        }

        Response IAccountRestrictedDataContext.InsertAccount(PRE_CUENTA_RESTRINGIDA account)
        {
            return RecordAndReturn<Response>("IAccountRestrictedDataContext.InsertAccount", new object[] { account });
        }

        Response IAccountRestrictedDataContext.UpdateAccount(PRE_CUENTA_RESTRINGIDA account)
        {
            return RecordAndReturn<Response>("IAccountRestrictedDataContext.UpdateAccount", new object[] { account });
        }

        Response IAccountRestrictedDataContext.DeleteAccount(int accountId)
        {
            return RecordAndReturn<Response>("IAccountRestrictedDataContext.DeleteAccount", new object[] { accountId });
        }

        List<PRE_EXPEDIENTE_ADMINISTRATIVO> IAdministrativeRecordsDataContext.GetSpends(int? exerciseYear, int? recordNumber, int? provenanceId, string description)
        {
            return RecordAndReturn<List<PRE_EXPEDIENTE_ADMINISTRATIVO>>("IAdministrativeRecordsDataContext.GetSpends", new object[] { exerciseYear, recordNumber, provenanceId, description });
        }

        PRE_EXPEDIENTE_ADMINISTRATIVO IAdministrativeRecordsDataContext.GetById(int administrativeRecordId)
        {
            return RecordAndReturn<PRE_EXPEDIENTE_ADMINISTRATIVO>("IAdministrativeRecordsDataContext.GetById", new object[] { administrativeRecordId });
        }

        Response IAdministrativeRecordsDataContext.InsertAdministrativeRecord(PRE_EXPEDIENTE_ADMINISTRATIVO administrativeRecord)
        {
            return RecordAndReturn<Response>("IAdministrativeRecordsDataContext.InsertAdministrativeRecord", new object[] { administrativeRecord });
        }

        Response IAdministrativeRecordsDataContext.UpdateAdministrativeRecord(PRE_EXPEDIENTE_ADMINISTRATIVO administrativeRecord)
        {
            return RecordAndReturn<Response>("IAdministrativeRecordsDataContext.UpdateAdministrativeRecord", new object[] { administrativeRecord });
        }

        Response IAdministrativeRecordsDataContext.DeleteAdministrativeRecord(int id, int userId)
        {
            return RecordAndReturn<Response>("IAdministrativeRecordsDataContext.DeleteAdministrativeRecord", new object[] { id, userId });
        }

        int IAdministrativeRecordsDataContext.GetNextOrder(int year, int provenanceId)
        {
            return RecordAndReturn<int>("IAdministrativeRecordsDataContext.GetNextOrder", new object[] { year, provenanceId });
        }

        List<Application> IApplicationDataContext.GetApplications(string type)
        {
            return RecordAndReturn<List<Application>>("IApplicationDataContext.GetApplications", new object[] { type });
        }

        Response IApplicationDataContext.UpdateApplication(Application application, int userId)
        {
            return RecordAndReturn<Response>("IApplicationDataContext.UpdateApplication", new object[] { application, userId });
        }

        Response IApplicationDataContext.UpdateStatus(Application application, int userId)
        {
            return RecordAndReturn<Response>("IApplicationDataContext.UpdateStatus", new object[] { application, userId });
        }

        Response IApplicationDataContext.DeleteApplication(Application application)
        {
            return RecordAndReturn<Response>("IApplicationDataContext.DeleteApplication", new object[] { application });
        }

        List<PRE_CAPITULO> IApplicationDataContext.GetChapters(string type)
        {
            return RecordAndReturn<List<PRE_CAPITULO>>("IApplicationDataContext.GetChapters", new object[] { type });
        }

        List<PRE_ARTICULO> IApplicationDataContext.GetArticles(string type, int chapterCode)
        {
            return RecordAndReturn<List<PRE_ARTICULO>>("IApplicationDataContext.GetArticles", new object[] { type, chapterCode });
        }

        List<PRE_CONCEPTO> IApplicationDataContext.GetConcepts(string type, int articleCode)
        {
            return RecordAndReturn<List<PRE_CONCEPTO>>("IApplicationDataContext.GetConcepts", new object[] { type, articleCode });
        }

        List<PRE_SUBCONCEPTO> IApplicationDataContext.GetSubconcepts(string type, int conceptCode)
        {
            return RecordAndReturn<List<PRE_SUBCONCEPTO>>("IApplicationDataContext.GetSubconcepts", new object[] { type, conceptCode });
        }

        Response IApplicationDataContext.InsertChapter(PRE_CAPITULO chapter)
        {
            return RecordAndReturn<Response>("IApplicationDataContext.InsertChapter", new object[] { chapter });
        }

        Response IApplicationDataContext.InsertArticle(PRE_ARTICULO article)
        {
            return RecordAndReturn<Response>("IApplicationDataContext.InsertArticle", new object[] { article });
        }

        Response IApplicationDataContext.InsertConcept(PRE_CONCEPTO concept)
        {
            return RecordAndReturn<Response>("IApplicationDataContext.InsertConcept", new object[] { concept });
        }

        Response IApplicationDataContext.InsertSuboncept(PRE_SUBCONCEPTO subconcept)
        {
            return RecordAndReturn<Response>("IApplicationDataContext.InsertSuboncept", new object[] { subconcept });
        }

        List<PRE_FACTURA_COMPRA> IBillPurchasesDataContext.GetBillPurchases(int? exerciseYear, int? administrativeId, string billNumber, string billDate, decimal? billAmount, string roDate, int? providerId, decimal? roAmount, string empty)
        {
            return RecordAndReturn<List<PRE_FACTURA_COMPRA>>("IBillPurchasesDataContext.GetBillPurchases", new object[] { exerciseYear, administrativeId, billNumber, billDate, billAmount, roDate, providerId, roAmount, empty });
        }

        List<PRE_FACTURA_COMPRA> IBillPurchasesDataContext.GetByDocument(int documentId)
        {
            return RecordAndReturn<List<PRE_FACTURA_COMPRA>>("IBillPurchasesDataContext.GetByDocument", new object[] { documentId });
        }

        List<PRE_FACTURA_COMPRA> IBillPurchasesDataContext.GetByAccountingRecord(int accountingRecordId)
        {
            return RecordAndReturn<List<PRE_FACTURA_COMPRA>>("IBillPurchasesDataContext.GetByAccountingRecord", new object[] { accountingRecordId });
        }

        Response IBillPurchasesDataContext.DeleteByAccountingRecord(int accountingRecordId)
        {
            return RecordAndReturn<Response>("IBillPurchasesDataContext.DeleteByAccountingRecord", new object[] { accountingRecordId });
        }

        Response IBillPurchasesDataContext.DeleteBillPurchase(int id)
        {
            return RecordAndReturn<Response>("IBillPurchasesDataContext.DeleteBillPurchase", new object[] { id });
        }

        Response IBillPurchasesDataContext.UpdateDocument(int accountingId, string date, int providerId, int documentId, int userId)
        {
            return RecordAndReturn<Response>("IBillPurchasesDataContext.UpdateDocument", new object[] { accountingId, date, providerId, documentId, userId });
        }

        decimal IBillPurchasesDataContext.GetDocumentRetentionAmount(int documentId)
        {
            return RecordAndReturn<decimal>("IBillPurchasesDataContext.GetDocumentRetentionAmount", new object[] { documentId });
        }

        decimal IBillPurchasesDataContext.GetDocumentBoeAmount(int documentId)
        {
            return RecordAndReturn<decimal>("IBillPurchasesDataContext.GetDocumentBoeAmount", new object[] { documentId });
        }

        List<BudgetApplication> IBudgetApplicationsDataContext.GetNumbersByTypeByYear(string type, int year)
        {
            return RecordAndReturn<List<BudgetApplication>>("IBudgetApplicationsDataContext.GetNumbersByTypeByYear", new object[] { type, year });
        }

        List<BudgetApplication> IBudgetApplicationsDataContext.GetChaptersNumbersByTypeByYear(string type, int year)
        {
            return RecordAndReturn<List<BudgetApplication>>("IBudgetApplicationsDataContext.GetChaptersNumbersByTypeByYear", new object[] { type, year });
        }

        Response IBudgetApplicationsDataContext.GetApplicationInfo(string cacsCode, int year, string type)
        {
            return RecordAndReturn<Response>("IBudgetApplicationsDataContext.GetApplicationInfo", new object[] { cacsCode, year, type });
        }

        List<Year> IBudgetsDataContext.GetYearsByType(string type)
        {
            return RecordAndReturn<List<Year>>("IBudgetsDataContext.GetYearsByType", new object[] { type });
        }

        List<Budget> IBudgetsDataContext.GetBudgetsByType(int year, string type)
        {
            return RecordAndReturn<List<Budget>>("IBudgetsDataContext.GetBudgetsByType", new object[] { year, type });
        }

        List<Budget> IBudgetsDataContext.GetIncomeLevelCompliance(int year)
        {
            return RecordAndReturn<List<Budget>>("IBudgetsDataContext.GetIncomeLevelCompliance", new object[] { year });
        }

        List<Budget> IBudgetsDataContext.GetSpendLevelCompliance(int year)
        {
            return RecordAndReturn<List<Budget>>("IBudgetsDataContext.GetSpendLevelCompliance", new object[] { year });
        }

        List<Budget> IBudgetsDataContext.GetBudgetsToNewByType(string type)
        {
            return RecordAndReturn<List<Budget>>("IBudgetsDataContext.GetBudgetsToNewByType", new object[] { type });
        }

        int IBudgetsDataContext.GetProgramCodeByYear(int year)
        {
            return RecordAndReturn<int>("IBudgetsDataContext.GetProgramCodeByYear", new object[] { year });
        }

        Response IBudgetsDataContext.InsertMany(int year, string type, List<Budget> budgets, int userId)
        {
            return RecordAndReturn<Response>("IBudgetsDataContext.InsertMany", new object[] { year, type, budgets, userId });
        }

        Response IBudgetsDataContext.UpdateMany(string type, List<Budget> budgets, int userId)
        {
            return RecordAndReturn<Response>("IBudgetsDataContext.UpdateMany", new object[] { type, budgets, userId });
        }

        Response IBudgetsDataContext.AddMany(int chapterId, int year, string type, int userId)
        {
            return RecordAndReturn<Response>("IBudgetsDataContext.AddMany", new object[] { chapterId, year, type, userId });
        }

        Response IBudgetsDataContext.DeleteMany(int chapterId, int year, string type, int userId)
        {
            return RecordAndReturn<Response>("IBudgetsDataContext.DeleteMany", new object[] { chapterId, year, type, userId });
        }

        Response IBudgetsDataContext.CloseBudget(int year, string type, int userId)
        {
            return RecordAndReturn<Response>("IBudgetsDataContext.CloseBudget", new object[] { year, type, userId });
        }

        Response IBudgetsDataContext.DeleteBudget(int year, string type, int userId)
        {
            return RecordAndReturn<Response>("IBudgetsDataContext.DeleteBudget", new object[] { year, type, userId });
        }

        Response IBudgetsDataContext.CheckApplicationAmount(string cacsCode, int year, string type)
        {
            return RecordAndReturn<Response>("IBudgetsDataContext.CheckApplicationAmount", new object[] { cacsCode, year, type });
        }

        Response IBudgetsDataContext.CheckChapterApplicationAmount(string cacsCode, int year, string type)
        {
            return RecordAndReturn<Response>("IBudgetsDataContext.CheckChapterApplicationAmount", new object[] { cacsCode, year, type });
        }

        List<Budget> IBudgetsDataContext.GetSpendProvisionalStatus(int year, DateTime date, bool withOutPending)
        {
            return RecordAndReturn<List<Budget>>("IBudgetsDataContext.GetSpendProvisionalStatus", new object[] { year, date, withOutPending });
        }

        List<Budget> IBudgetsDataContext.GetSpendComplianceGrade(int year, DateTime since, DateTime until)
        {
            return RecordAndReturn<List<Budget>>("IBudgetsDataContext.GetSpendComplianceGrade", new object[] { year, since, until });
        }

        List<Budget> IBudgetsDataContext.GetIncomeProvisionalStatus(int year, DateTime date, bool withOutPending)
        {
            return RecordAndReturn<List<Budget>>("IBudgetsDataContext.GetIncomeProvisionalStatus", new object[] { year, date, withOutPending });
        }

        List<PRE_TIPO_CONTRATO> IContractTypesDataContext.GetContractTypes()
        {
            return RecordAndReturn<List<PRE_TIPO_CONTRATO>>("IContractTypesDataContext.GetContractTypes", new object[0]);
        }

        List<PRE_CENTRO_COSTE> ICostPlacesDataContext.GetCostPlacesToCombo()
        {
            return RecordAndReturn<List<PRE_CENTRO_COSTE>>("ICostPlacesDataContext.GetCostPlacesToCombo", new object[0]);
        }

        List<PRE_CENTRO_COSTE> ICostPlacesDataContext.GetCostPlacesOriginToCombo()
        {
            return RecordAndReturn<List<PRE_CENTRO_COSTE>>("ICostPlacesDataContext.GetCostPlacesOriginToCombo", new object[0]);
        }

        Response ICostPlacesDataContext.GetIdByDescription(string description)
        {
            return RecordAndReturn<Response>("ICostPlacesDataContext.GetIdByDescription", new object[] { description });
        }

        List<PRE_MODIF_CREDITO_PRESUPUESTO> ICreditModificationBudgetsDataContext.GetByCreditModification(int creditModificationId, string type)
        {
            return RecordAndReturn<List<PRE_MODIF_CREDITO_PRESUPUESTO>>("ICreditModificationBudgetsDataContext.GetByCreditModification", new object[] { creditModificationId, type });
        }

        Response ICreditModificationBudgetsDataContext.InsertCreditModificationBudget(PRE_MODIF_CREDITO_PRESUPUESTO creditModificationBudget, int year)
        {
            return RecordAndReturn<Response>("ICreditModificationBudgetsDataContext.InsertCreditModificationBudget", new object[] { creditModificationBudget, year });
        }

        Response ICreditModificationBudgetsDataContext.UpdateCreditModificationBudget(PRE_MODIF_CREDITO_PRESUPUESTO creditModificationBudget, int year)
        {
            return RecordAndReturn<Response>("ICreditModificationBudgetsDataContext.UpdateCreditModificationBudget", new object[] { creditModificationBudget, year });
        }

        Response ICreditModificationBudgetsDataContext.DeleteCreditModificationBudget(int budgetId, int creditModificationId, string type, int userId)
        {
            return RecordAndReturn<Response>("ICreditModificationBudgetsDataContext.DeleteCreditModificationBudget", new object[] { budgetId, creditModificationId, type, userId });
        }

        List<Year> ICreditModificationsDataContext.GetYears()
        {
            return RecordAndReturn<List<Year>>("ICreditModificationsDataContext.GetYears", new object[0]);
        }

        List<PRE_MODIFICACION_CREDITO> ICreditModificationsDataContext.GetByFilters(int year, int? since, int? until)
        {
            return RecordAndReturn<List<PRE_MODIFICACION_CREDITO>>("ICreditModificationsDataContext.GetByFilters", new object[] { year, since, until });
        }

        PRE_MODIFICACION_CREDITO ICreditModificationsDataContext.GetById(int creditModificationId)
        {
            return RecordAndReturn<PRE_MODIFICACION_CREDITO>("ICreditModificationsDataContext.GetById", new object[] { creditModificationId });
        }

        int ICreditModificationsDataContext.GetNextOrderByYear(int year)
        {
            return RecordAndReturn<int>("ICreditModificationsDataContext.GetNextOrderByYear", new object[] { year });
        }

        Response ICreditModificationsDataContext.InsertCreditModification(PRE_MODIFICACION_CREDITO creditModification)
        {
            return RecordAndReturn<Response>("ICreditModificationsDataContext.InsertCreditModification", new object[] { creditModification });
        }

        Response ICreditModificationsDataContext.UpdateCreditModification(PRE_MODIFICACION_CREDITO creditModification)
        {
            return RecordAndReturn<Response>("ICreditModificationsDataContext.UpdateCreditModification", new object[] { creditModification });
        }

        Response ICreditModificationsDataContext.GenerateRecordsCreditModification(PRE_MODIFICACION_CREDITO creditModification)
        {
            return RecordAndReturn<Response>("ICreditModificationsDataContext.GenerateRecordsCreditModification", new object[] { creditModification });
        }

        Response ICreditModificationsDataContext.ExecuteCreditModification(int creditModificationId, int userId)
        {
            return RecordAndReturn<Response>("ICreditModificationsDataContext.ExecuteCreditModification", new object[] { creditModificationId, userId });
        }

        List<PRE_TIPO_MODIFICACION_CREDITO> ICreditModificationTypesDataContext.GetCreditModificationTypes(string type)
        {
            return RecordAndReturn<List<PRE_TIPO_MODIFICACION_CREDITO>>("ICreditModificationTypesDataContext.GetCreditModificationTypes", new object[] { type });
        }

        Response ICreditModificationTypesDataContext.InsertCreditModificationType(PRE_TIPO_MODIFICACION_CREDITO creditModificationType)
        {
            return RecordAndReturn<Response>("ICreditModificationTypesDataContext.InsertCreditModificationType", new object[] { creditModificationType });
        }

        Response ICreditModificationTypesDataContext.UpdateCreditModificationType(PRE_TIPO_MODIFICACION_CREDITO creditModificationType)
        {
            return RecordAndReturn<Response>("ICreditModificationTypesDataContext.UpdateCreditModificationType", new object[] { creditModificationType });
        }

        Response ICreditModificationTypesDataContext.DeleteCreditModificationType(int creditModificationTypeId)
        {
            return RecordAndReturn<Response>("ICreditModificationTypesDataContext.DeleteCreditModificationType", new object[] { creditModificationTypeId });
        }

        List<PRE_TIPO_DOCUMENTO> IDocumentTypesDataContext.GetDocumentTypes(string type)
        {
            return RecordAndReturn<List<PRE_TIPO_DOCUMENTO>>("IDocumentTypesDataContext.GetDocumentTypes", new object[] { type });
        }

        List<PRE_TIPO_DOCUMENTO> IDocumentTypesDataContext.GetDocumentTypesToCombo(string type)
        {
            return RecordAndReturn<List<PRE_TIPO_DOCUMENTO>>("IDocumentTypesDataContext.GetDocumentTypesToCombo", new object[] { type });
        }

        List<PRE_TIPO_DOCUMENTO> IDocumentTypesDataContext.GetDocumentTypesDrPhase()
        {
            return RecordAndReturn<List<PRE_TIPO_DOCUMENTO>>("IDocumentTypesDataContext.GetDocumentTypesDrPhase", new object[0]);
        }

        Response IDocumentTypesDataContext.InsertDocumentType(PRE_TIPO_DOCUMENTO documentType)
        {
            return RecordAndReturn<Response>("IDocumentTypesDataContext.InsertDocumentType", new object[] { documentType });
        }

        Response IDocumentTypesDataContext.UpdateDocumentType(PRE_TIPO_DOCUMENTO documentType)
        {
            return RecordAndReturn<Response>("IDocumentTypesDataContext.UpdateDocumentType", new object[] { documentType });
        }

        Response IDocumentTypesDataContext.DeleteDocumentType(int documentTypeId)
        {
            return RecordAndReturn<Response>("IDocumentTypesDataContext.DeleteDocumentType", new object[] { documentTypeId });
        }

        List<PRE_EXTRAPRESUPUESTARIA> IExtraBudgetaryApplicationsDataContext.GetExtraBudgetaryApplications()
        {
            return RecordAndReturn<List<PRE_EXTRAPRESUPUESTARIA>>("IExtraBudgetaryApplicationsDataContext.GetExtraBudgetaryApplications", new object[0]);
        }

        List<PRE_EXTRAPRESUPUESTARIA> IExtraBudgetaryApplicationsDataContext.GetExtraBudgetaryApplicationsToCombo()
        {
            return RecordAndReturn<List<PRE_EXTRAPRESUPUESTARIA>>("IExtraBudgetaryApplicationsDataContext.GetExtraBudgetaryApplicationsToCombo", new object[0]);
        }

        List<PRE_TIPO_EXTRAP> IExtraBudgetaryApplicationsDataContext.GetExtraBudgetaryTypes()
        {
            return RecordAndReturn<List<PRE_TIPO_EXTRAP>>("IExtraBudgetaryApplicationsDataContext.GetExtraBudgetaryTypes", new object[0]);
        }

        Response IExtraBudgetaryApplicationsDataContext.InsertExtraBudgetaryApplication(PRE_EXTRAPRESUPUESTARIA application)
        {
            return RecordAndReturn<Response>("IExtraBudgetaryApplicationsDataContext.InsertExtraBudgetaryApplication", new object[] { application });
        }

        Response IExtraBudgetaryApplicationsDataContext.UpdateExtraBudgetaryApplication(PRE_EXTRAPRESUPUESTARIA application)
        {
            return RecordAndReturn<Response>("IExtraBudgetaryApplicationsDataContext.UpdateExtraBudgetaryApplication", new object[] { application });
        }

        Response IExtraBudgetaryApplicationsDataContext.DeleteExtraBudgetaryApplication(int applicationId)
        {
            return RecordAndReturn<Response>("IExtraBudgetaryApplicationsDataContext.DeleteExtraBudgetaryApplication", new object[] { applicationId });
        }

        int IExtraBudgetaryApplicationsDataContext.GetNextNumber(int year)
        {
            return RecordAndReturn<int>("IExtraBudgetaryApplicationsDataContext.GetNextNumber", new object[] { year });
        }

        List<PRE_EXP_EXTRAPRE> IExtraBudgetaryApplicationsDataContext.GetStatus(int extraBudgetaryApplication, int year)
        {
            return RecordAndReturn<List<PRE_EXP_EXTRAPRE>>("IExtraBudgetaryApplicationsDataContext.GetStatus", new object[] { extraBudgetaryApplication, year });
        }

        PRE_EXP_EXTRAPRE IExtraBudgetaryApplicationsDataContext.GetInfo(int extraBudgetaryApplication)
        {
            return RecordAndReturn<PRE_EXP_EXTRAPRE>("IExtraBudgetaryApplicationsDataContext.GetInfo", new object[] { extraBudgetaryApplication });
        }

        List<PRE_EXP_EXTRAPRE> IExtraBudgetaryRecordsDataContext.GetByDocumentId(int accountingDocumentId, int type)
        {
            return RecordAndReturn<List<PRE_EXP_EXTRAPRE>>("IExtraBudgetaryRecordsDataContext.GetByDocumentId", new object[] { accountingDocumentId, type });
        }

        List<PRE_EXP_EXTRAPRE> IExtraBudgetaryRecordsDataContext.GetByFilters(int? year, int? extraBudgetaryType, int? extraBudgetaryApplication, bool? isBound, string sinceDate, string untilDate, int? fileNumberSince, int? fileNumberUntil, int? providerCode)
        {
            return RecordAndReturn<List<PRE_EXP_EXTRAPRE>>("IExtraBudgetaryRecordsDataContext.GetByFilters", new object[] { year, extraBudgetaryType, extraBudgetaryApplication, isBound, sinceDate, untilDate, fileNumberSince, fileNumberUntil, providerCode });
        }

        List<PRE_EXP_EXTRAPRE> IExtraBudgetaryRecordsDataContext.GetByBoundId(int extraBudgetaryId)
        {
            return RecordAndReturn<List<PRE_EXP_EXTRAPRE>>("IExtraBudgetaryRecordsDataContext.GetByBoundId", new object[] { extraBudgetaryId });
        }

        Tuple<PRE_PARAMETROS, PRE_EXP_EXTRAPRE> IExtraBudgetaryRecordsDataContext.GetMi(int extraBudgetaryId)
        {
            return RecordAndReturn<Tuple<PRE_PARAMETROS, PRE_EXP_EXTRAPRE>>("IExtraBudgetaryRecordsDataContext.GetMi", new object[] { extraBudgetaryId });
        }

        Tuple<PRE_EXP_EXTRAPRE, PRE_PARAMETROS, List<PRE_EXP_EXTRAPRE>> IExtraBudgetaryRecordsDataContext.GetPmp(int extraBudgetaryId)
        {
            return RecordAndReturn<Tuple<PRE_EXP_EXTRAPRE, PRE_PARAMETROS, List<PRE_EXP_EXTRAPRE>>>("IExtraBudgetaryRecordsDataContext.GetPmp", new object[] { extraBudgetaryId });
        }

        List<PRE_EXP_EXTRAPRE> IExtraBudgetaryRecordsDataContext.GetDebitAndCredit(int extraBudgetaryId, DateTime since, DateTime until)
        {
            return RecordAndReturn<List<PRE_EXP_EXTRAPRE>>("IExtraBudgetaryRecordsDataContext.GetDebitAndCredit", new object[] { extraBudgetaryId, since, until });
        }

        PRE_EXP_EXTRAPRE IExtraBudgetaryRecordsDataContext.GetById(int extraBudgetaryId)
        {
            return RecordAndReturn<PRE_EXP_EXTRAPRE>("IExtraBudgetaryRecordsDataContext.GetById", new object[] { extraBudgetaryId });
        }

        Response IExtraBudgetaryRecordsDataContext.InsertExtraBudgetary(PRE_EXP_EXTRAPRE extraBudgetary)
        {
            return RecordAndReturn<Response>("IExtraBudgetaryRecordsDataContext.InsertExtraBudgetary", new object[] { extraBudgetary });
        }

        Response IExtraBudgetaryRecordsDataContext.UpdateExtraBudgetaryAll(PRE_EXP_EXTRAPRE extraBudgetary)
        {
            return RecordAndReturn<Response>("IExtraBudgetaryRecordsDataContext.UpdateExtraBudgetaryAll", new object[] { extraBudgetary });
        }

        Response IExtraBudgetaryRecordsDataContext.UpdateExtraBudgetary(PRE_EXP_EXTRAPRE extraBudgetary)
        {
            return RecordAndReturn<Response>("IExtraBudgetaryRecordsDataContext.UpdateExtraBudgetary", new object[] { extraBudgetary });
        }

        Response IExtraBudgetaryRecordsDataContext.DeleteExtraBudgetary(int extraBudgetaryId, int userId)
        {
            return RecordAndReturn<Response>("IExtraBudgetaryRecordsDataContext.DeleteExtraBudgetary", new object[] { extraBudgetaryId, userId });
        }

        Response IExtraBudgetaryRecordsDataContext.DeleteExtraBudgetaryDiscount(int extraBudgetaryId)
        {
            return RecordAndReturn<Response>("IExtraBudgetaryRecordsDataContext.DeleteExtraBudgetaryDiscount", new object[] { extraBudgetaryId });
        }

        decimal IExtraBudgetaryRecordsDataContext.GetSumAmount(int accountingId)
        {
            return RecordAndReturn<decimal>("IExtraBudgetaryRecordsDataContext.GetSumAmount", new object[] { accountingId });
        }

        decimal IExtraBudgetaryRecordsDataContext.GetSumBoundAmount(int extraBudgetaryId)
        {
            return RecordAndReturn<decimal>("IExtraBudgetaryRecordsDataContext.GetSumBoundAmount", new object[] { extraBudgetaryId });
        }

        Response IExtraBudgetaryRecordsDataContext.UpdateRepair(PRE_DOCUMENTO_CONTABLE accountingDocument)
        {
            return RecordAndReturn<Response>("IExtraBudgetaryRecordsDataContext.UpdateRepair", new object[] { accountingDocument });
        }

        List<PRE_ORIGEN> IOriginsDataContext.GetOrigins()
        {
            return RecordAndReturn<List<PRE_ORIGEN>>("IOriginsDataContext.GetOrigins", new object[0]);
        }

        List<PRE_PARAMETROS> IParametersDataContext.GetParameters()
        {
            return RecordAndReturn<List<PRE_PARAMETROS>>("IParametersDataContext.GetParameters", new object[0]);
        }

        Response IParametersDataContext.UpdateParameter(PRE_PARAMETROS parameters)
        {
            return RecordAndReturn<Response>("IParametersDataContext.UpdateParameter", new object[] { parameters });
        }

        List<PRE_FORMA_PAGO> IPayFormsDataContext.GetPayForms()
        {
            return RecordAndReturn<List<PRE_FORMA_PAGO>>("IPayFormsDataContext.GetPayForms", new object[0]);
        }

        Response IPayFormsDataContext.InsertPayForm(PRE_FORMA_PAGO payForm)
        {
            return RecordAndReturn<Response>("IPayFormsDataContext.InsertPayForm", new object[] { payForm });
        }

        Response IPayFormsDataContext.UpdatePayForm(PRE_FORMA_PAGO payForm)
        {
            return RecordAndReturn<Response>("IPayFormsDataContext.UpdatePayForm", new object[] { payForm });
        }

        Response IPayFormsDataContext.DeletePayForm(byte payFormId)
        {
            return RecordAndReturn<Response>("IPayFormsDataContext.DeletePayForm", new object[] { payFormId });
        }

        List<PRE_TIPO_PAGO> IPayTypesDataContext.GetPayTypes()
        {
            return RecordAndReturn<List<PRE_TIPO_PAGO>>("IPayTypesDataContext.GetPayTypes", new object[0]);
        }

        Response IPayTypesDataContext.InsertPayType(PRE_TIPO_PAGO payType)
        {
            return RecordAndReturn<Response>("IPayTypesDataContext.InsertPayType", new object[] { payType });
        }

        Response IPayTypesDataContext.UpdatePayType(PRE_TIPO_PAGO payType)
        {
            return RecordAndReturn<Response>("IPayTypesDataContext.UpdatePayType", new object[] { payType });
        }

        Response IPayTypesDataContext.DeletePayType(byte payTypeId)
        {
            return RecordAndReturn<Response>("IPayTypesDataContext.DeletePayType", new object[] { payTypeId });
        }

        List<PRE_PROGRAMA> IProgramsDataContext.GetPrograms()
        {
            return RecordAndReturn<List<PRE_PROGRAMA>>("IProgramsDataContext.GetPrograms", new object[0]);
        }

        Response IProgramsDataContext.InsertProgram(PRE_PROGRAMA program)
        {
            return RecordAndReturn<Response>("IProgramsDataContext.InsertProgram", new object[] { program });
        }

        Response IProgramsDataContext.UpdateProgram(PRE_PROGRAMA program)
        {
            return RecordAndReturn<Response>("IProgramsDataContext.UpdateProgram", new object[] { program });
        }

        Response IProgramsDataContext.DeleteProgram(byte programId)
        {
            return RecordAndReturn<Response>("IProgramsDataContext.DeleteProgram", new object[] { programId });
        }

        List<PRE_PROCEDENCIA> IProvenancesDataContext.GetProvenances(string type)
        {
            return RecordAndReturn<List<PRE_PROCEDENCIA>>("IProvenancesDataContext.GetProvenances", new object[] { type });
        }

        Response IProvenancesDataContext.GetIdByDescription(string description, string type)
        {
            return RecordAndReturn<Response>("IProvenancesDataContext.GetIdByDescription", new object[] { description, type });
        }

        Response IProvenancesDataContext.InsertProvenance(PRE_PROCEDENCIA provenance)
        {
            return RecordAndReturn<Response>("IProvenancesDataContext.InsertProvenance", new object[] { provenance });
        }

        Response IProvenancesDataContext.UpdateProvenance(PRE_PROCEDENCIA provenance)
        {
            return RecordAndReturn<Response>("IProvenancesDataContext.UpdateProvenance", new object[] { provenance });
        }

        Response IProvenancesDataContext.DeleteProvenance(int provenanceId)
        {
            return RecordAndReturn<Response>("IProvenancesDataContext.DeleteProvenance", new object[] { provenanceId });
        }

        PRE_PROVEEDOR IProvidersDataContext.GetById(int providerId)
        {
            return RecordAndReturn<PRE_PROVEEDOR>("IProvidersDataContext.GetById", new object[] { providerId });
        }

        List<PRE_PROVEEDOR> IProvidersDataContext.GetProviders()
        {
            return RecordAndReturn<List<PRE_PROVEEDOR>>("IProvidersDataContext.GetProviders", new object[0]);
        }

        List<PRE_PROVEEDOR> IProvidersDataContext.GetProvidersToCombo()
        {
            return RecordAndReturn<List<PRE_PROVEEDOR>>("IProvidersDataContext.GetProvidersToCombo", new object[0]);
        }

        List<PRE_PROVEEDOR> IProvidersDataContext.GetBillProvidersToCombo()
        {
            return RecordAndReturn<List<PRE_PROVEEDOR>>("IProvidersDataContext.GetBillProvidersToCombo", new object[0]);
        }

        List<PRE_PROVEEDOR> IProvidersDataContext.GetProvidersByNameAndNif(string name, string nif)
        {
            return RecordAndReturn<List<PRE_PROVEEDOR>>("IProvidersDataContext.GetProvidersByNameAndNif", new object[] { name, nif });
        }

        List<PRE_PROVEEDOR> IProvidersDataContext.FindProviders(string name, string nif)
        {
            return RecordAndReturn<List<PRE_PROVEEDOR>>("IProvidersDataContext.FindProviders", new object[] { name, nif });
        }

        Response IProvidersDataContext.InsertProvider(PRE_PROVEEDOR provider)
        {
            return RecordAndReturn<Response>("IProvidersDataContext.InsertProvider", new object[] { provider });
        }

        Response IProvidersDataContext.UpdateProvider(PRE_PROVEEDOR provider)
        {
            return RecordAndReturn<Response>("IProvidersDataContext.UpdateProvider", new object[] { provider });
        }

        Response IProvidersDataContext.DeleteProvider(int providerId)
        {
            return RecordAndReturn<Response>("IProvidersDataContext.DeleteProvider", new object[] { providerId });
        }

        Response IProvidersDataContext.UpdateBranchDatas(int id, string ccCe, string ccCo, string ccDc, string ccNc, string branchName, string branchAddress, string branchLocation, int userId)
        {
            return RecordAndReturn<Response>("IProvidersDataContext.UpdateBranchDatas", new object[] { id, ccCe, ccCo, ccDc, ccNc, branchName, branchAddress, branchLocation, userId });
        }

        List<PRE_PROVEEDOR> IProvidersDataContext.GetByYearHasSpend(int year)
        {
            return RecordAndReturn<List<PRE_PROVEEDOR>>("IProvidersDataContext.GetByYearHasSpend", new object[] { year });
        }

        List<Provincia> IProvincesDataContext.GetProvincesToCombo()
        {
            return RecordAndReturn<List<Provincia>>("IProvincesDataContext.GetProvincesToCombo", new object[0]);
        }

        List<PRE_TIPO_REGISTRO> IRecordTypesDataContext.GetRecordTypes()
        {
            return RecordAndReturn<List<PRE_TIPO_REGISTRO>>("IRecordTypesDataContext.GetRecordTypes", new object[0]);
        }

        List<Rectification> IRectificationsDataContext.GetRectifications(string sign, string type, int year)
        {
            return RecordAndReturn<List<Rectification>>("IRectificationsDataContext.GetRectifications", new object[] { sign, type, year });
        }

        List<Rectification> IRectificationsDataContext.GetRectificationsByCodes(string iCodes, string eCodes)
        {
            return RecordAndReturn<List<Rectification>>("IRectificationsDataContext.GetRectificationsByCodes", new object[] { iCodes, eCodes });
        }

        List<PRE_SENALAMIENTO> ISingsDataContext.GetByFilters(int year, int? pointingNumber, string date, decimal? amount, int? fileNumber, string type)
        {
            return RecordAndReturn<List<PRE_SENALAMIENTO>>("ISingsDataContext.GetByFilters", new object[] { year, pointingNumber, date, amount, fileNumber, type });
        }

        PRE_SENALAMIENTO ISingsDataContext.GetById(int id)
        {
            return RecordAndReturn<PRE_SENALAMIENTO>("ISingsDataContext.GetById", new object[] { id });
        }

        List<PRE_SENALAMIENTO_DOCUMENTO> ISingsDataContext.GetDocumentsByPointingId(int pointingId)
        {
            return RecordAndReturn<List<PRE_SENALAMIENTO_DOCUMENTO>>("ISingsDataContext.GetDocumentsByPointingId", new object[] { pointingId });
        }

        List<PRE_SENALAMIENTO_DOCUMENTO> ISingsDataContext.GetTransfersByPointingId(int pointingId, bool groupTransfers)
        {
            return RecordAndReturn<List<PRE_SENALAMIENTO_DOCUMENTO>>("ISingsDataContext.GetTransfersByPointingId", new object[] { pointingId, groupTransfers });
        }

        List<PRE_DOCUMENTO_CONTABLE> ISingsDataContext.GetPendingDocuments(int year, string documentType)
        {
            return RecordAndReturn<List<PRE_DOCUMENTO_CONTABLE>>("ISingsDataContext.GetPendingDocuments", new object[] { year, documentType });
        }

        Response ISingsDataContext.InsertPointing(PRE_SENALAMIENTO pointing)
        {
            return RecordAndReturn<Response>("ISingsDataContext.InsertPointing", new object[] { pointing });
        }

        Response ISingsDataContext.DeletePointing(int id, int userId)
        {
            return RecordAndReturn<Response>("ISingsDataContext.DeletePointing", new object[] { id, userId });
        }

        int ISingsDataContext.GetNextNumber(int year)
        {
            return RecordAndReturn<int>("ISingsDataContext.GetNextNumber", new object[] { year });
        }

        Response ISingsDataContext.InsertDocumentSing(int pointingId, int? documentCode, int? fileCode, int userId)
        {
            return RecordAndReturn<Response>("ISingsDataContext.InsertDocumentSing", new object[] { pointingId, documentCode, fileCode, userId });
        }

        Response ISingsDataContext.DeleteDocument(int id, int userId)
        {
            return RecordAndReturn<Response>("ISingsDataContext.DeleteDocument", new object[] { id, userId });
        }

        PRE_DETALLE_HOJA_ARQUEO ITonnageSheetsDataContext.GetDetailByFilters(int tonnageSheetCode, int? fileNumber, int? lineNumber)
        {
            return RecordAndReturn<PRE_DETALLE_HOJA_ARQUEO>("ITonnageSheetsDataContext.GetDetailByFilters", new object[] { tonnageSheetCode, fileNumber, lineNumber });
        }

        Response ITonnageSheetsDataContext.InsertTonnageSheet(PRE_HOJA_ARQUEO tonnageSheet)
        {
            return RecordAndReturn<Response>("ITonnageSheetsDataContext.InsertTonnageSheet", new object[] { tonnageSheet });
        }

        Response ITonnageSheetsDataContext.UpdateTonnageSheet(PRE_HOJA_ARQUEO tonnageSheet)
        {
            return RecordAndReturn<Response>("ITonnageSheetsDataContext.UpdateTonnageSheet", new object[] { tonnageSheet });
        }

        Response ITonnageSheetsDataContext.InsertTonnageSheetDetail(PRE_DETALLE_HOJA_ARQUEO tonnageSheetDetail)
        {
            return RecordAndReturn<Response>("ITonnageSheetsDataContext.InsertTonnageSheetDetail", new object[] { tonnageSheetDetail });
        }

        Response ITonnageSheetsDataContext.UpdateTonnageSheetDetail(int tonnageSheetDetailCode, bool check, int userId)
        {
            return RecordAndReturn<Response>("ITonnageSheetsDataContext.UpdateTonnageSheetDetail", new object[] { tonnageSheetDetailCode, check, userId });
        }

        PRE_HOJA_ARQUEO ITonnageSheetsDataContext.GetById(int id)
        {
            return RecordAndReturn<PRE_HOJA_ARQUEO>("ITonnageSheetsDataContext.GetById", new object[] { id });
        }

        List<PRE_HOJA_ARQUEO> ITonnageSheetsDataContext.GetRestrictedAccountStatement(int? exerciseYear, int? budgetYear, int accountId, string sinceDate, string untilDate)
        {
            return RecordAndReturn<List<PRE_HOJA_ARQUEO>>("ITonnageSheetsDataContext.GetRestrictedAccountStatement", new object[] { exerciseYear, budgetYear, accountId, sinceDate, untilDate });
        }

        int ITonnageSheetsDataContext.GetTonnageSheetCode(int exerciseYear, int number, bool isFifty)
        {
            return RecordAndReturn<int>("ITonnageSheetsDataContext.GetTonnageSheetCode", new object[] { exerciseYear, number, isFifty });
        }

        List<PRE_HOJA_ARQUEO> ITonnageSheetsDataContext.GetTonnageSheetsDetails(int exerciseYear, int number, bool isFifty)
        {
            return RecordAndReturn<List<PRE_HOJA_ARQUEO>>("ITonnageSheetsDataContext.GetTonnageSheetsDetails", new object[] { exerciseYear, number, isFifty });
        }

        List<PRE_HOJA_ARQUEO> ITonnageSheetsDataContext.GetTonnageSheetsDetailsWithoutSheet(int exerciseYear, int number, bool isFifty)
        {
            return RecordAndReturn<List<PRE_HOJA_ARQUEO>>("ITonnageSheetsDataContext.GetTonnageSheetsDetailsWithoutSheet", new object[] { exerciseYear, number, isFifty });
        }

        List<PRE_HOJA_ARQUEO> ITonnageSheetsDataContext.GetByFilters(int? exerciseYear, int? accountingCode, int? sheetSinceNumber, int? sheetUntilNumber, string sheetDateSince, string sheetDateUntil, string order, string sense)
        {
            return RecordAndReturn<List<PRE_HOJA_ARQUEO>>("ITonnageSheetsDataContext.GetByFilters", new object[] { exerciseYear, accountingCode, sheetSinceNumber, sheetUntilNumber, sheetDateSince, sheetDateUntil, order, sense });
        }

        List<PRE_DETALLE_HOJA_ARQUEO> ITonnageSheetsDataContext.GetDetailsById(int tonnageSheetId)
        {
            return RecordAndReturn<List<PRE_DETALLE_HOJA_ARQUEO>>("ITonnageSheetsDataContext.GetDetailsById", new object[] { tonnageSheetId });
        }

        Response ITonnageSheetsDataContext.DeleteDetail(int detailId, int userId)
        {
            return RecordAndReturn<Response>("ITonnageSheetsDataContext.DeleteDetail", new object[] { detailId, userId });
        }

        Response ITonnageSheetsDataContext.DeleteTonnageSheet(int id, int userId)
        {
            return RecordAndReturn<Response>("ITonnageSheetsDataContext.DeleteTonnageSheet", new object[] { id, userId });
        }

        PRE_TESORERIA ITreasuriesDataContext.GetById(int treasuryId)
        {
            return RecordAndReturn<PRE_TESORERIA>("ITreasuriesDataContext.GetById", new object[] { treasuryId });
        }

        List<PRE_TESORERIA> ITreasuriesDataContext.GetAllById(int treasuryId)
        {
            return RecordAndReturn<List<PRE_TESORERIA>>("ITreasuriesDataContext.GetAllById", new object[] { treasuryId });
        }

        List<PRE_TESORERIA> ITreasuriesDataContext.GetDocuments(int? documentId, int? extraBudgetaryId, int? treasuryId)
        {
            return RecordAndReturn<List<PRE_TESORERIA>>("ITreasuriesDataContext.GetDocuments", new object[] { documentId, extraBudgetaryId, treasuryId });
        }

        List<PRE_DOCUMENTO_CONTABLE> ITreasuriesDataContext.GetDocumentsToBound(int? budgetYear, decimal? amount, int? providerCode, string checkNumber, int originCode)
        {
            return RecordAndReturn<List<PRE_DOCUMENTO_CONTABLE>>("ITreasuriesDataContext.GetDocumentsToBound", new object[] { budgetYear, amount, providerCode, checkNumber, originCode });
        }

        List<PRE_DOCUMENTO_CONTABLE> ITreasuriesDataContext.GetPaymentsRegister(int year, int? originCode, int? providerIncomes, int? providerSpends, string sinceDate, string untilDate)
        {
            return RecordAndReturn<List<PRE_DOCUMENTO_CONTABLE>>("ITreasuriesDataContext.GetPaymentsRegister", new object[] { year, originCode, providerIncomes, providerSpends, sinceDate, untilDate });
        }

        List<PRE_TESORERIA> ITreasuriesDataContext.GetTreasuryByFilters(int? exerciseYear, int? origingCode, decimal? importe, string sinceBankDate, string untilBankDate, int? payFormCode, string checkNumber, bool? treasuryHave, string description, string sinceDateEntry, string untilDateEntry, int? registerTypeCode, bool? isCanceled, bool? isBound, string pendingDate, int? sinceDocYear, int? untilDocYear, string order)
        {
            return RecordAndReturn<List<PRE_TESORERIA>>("ITreasuriesDataContext.GetTreasuryByFilters", new object[] { exerciseYear, origingCode, importe, sinceBankDate, untilBankDate, payFormCode, checkNumber, treasuryHave, description, sinceDateEntry, untilDateEntry, registerTypeCode, isCanceled, isBound, pendingDate, sinceDocYear, untilDocYear, order });
        }

        List<PRE_TESORERIA> ITreasuriesDataContext.GetAccountingBook(DateTime since, DateTime until)
        {
            return RecordAndReturn<List<PRE_TESORERIA>>("ITreasuriesDataContext.GetAccountingBook", new object[] { since, until });
        }

        List<PRE_TESORERIA> ITreasuriesDataContext.GetBankStatement(DateTime since, DateTime until)
        {
            return RecordAndReturn<List<PRE_TESORERIA>>("ITreasuriesDataContext.GetBankStatement", new object[] { since, until });
        }

        List<PRE_TESORERIA> ITreasuriesDataContext.GetBlockListing(DateTime register)
        {
            return RecordAndReturn<List<PRE_TESORERIA>>("ITreasuriesDataContext.GetBlockListing", new object[] { register });
        }

        List<PRE_TESORERIA> ITreasuriesDataContext.GetPaymentRecord(DateTime since, DateTime until)
        {
            return RecordAndReturn<List<PRE_TESORERIA>>("ITreasuriesDataContext.GetPaymentRecord", new object[] { since, until });
        }

        List<PRE_DETALLE_HOJA_ARQUEO> ITreasuriesDataContext.GetTonnageSheetDetails(int tonnageSheetCode)
        {
            return RecordAndReturn<List<PRE_DETALLE_HOJA_ARQUEO>>("ITreasuriesDataContext.GetTonnageSheetDetails", new object[] { tonnageSheetCode });
        }

        int ITreasuriesDataContext.GetBoundDocumentsCount(int treasuryId)
        {
            return RecordAndReturn<int>("ITreasuriesDataContext.GetBoundDocumentsCount", new object[] { treasuryId });
        }

        Response ITreasuriesDataContext.InsertTreasury(PRE_TESORERIA treasury)
        {
            return RecordAndReturn<Response>("ITreasuriesDataContext.InsertTreasury", new object[] { treasury });
        }

        Response ITreasuriesDataContext.UpdateTreasury(PRE_TESORERIA treasury)
        {
            return RecordAndReturn<Response>("ITreasuriesDataContext.UpdateTreasury", new object[] { treasury });
        }

        Response ITreasuriesDataContext.DeleteBankDate(int treasuryId, int userId)
        {
            return RecordAndReturn<Response>("ITreasuriesDataContext.DeleteBankDate", new object[] { treasuryId, userId });
        }

        Response ITreasuriesDataContext.DeleteTreasury(int treasuryId, int userId)
        {
            return RecordAndReturn<Response>("ITreasuriesDataContext.DeleteTreasury", new object[] { treasuryId, userId });
        }

        Response ITreasuriesDataContext.DeleteDocument(int treasuryDocumentId, int userId)
        {
            return RecordAndReturn<Response>("ITreasuriesDataContext.DeleteDocument", new object[] { treasuryDocumentId, userId });
        }

        decimal ITreasuriesDataContext.GetAmount(int treasuryId)
        {
            return RecordAndReturn<decimal>("ITreasuriesDataContext.GetAmount", new object[] { treasuryId });
        }

        decimal ITreasuriesDataContext.GetDocumentsAmount(int treasuryId)
        {
            return RecordAndReturn<decimal>("ITreasuriesDataContext.GetDocumentsAmount", new object[] { treasuryId });
        }

        Response ITreasuriesDataContext.BoundDocument(PRE_TESORERIA_DOCUMENTO document)
        {
            return RecordAndReturn<Response>("ITreasuriesDataContext.BoundDocument", new object[] { document });
        }

        bool ITreasuriesDataContext.FoundTreasuryOracle(int treasuryId)
        {
            return RecordAndReturn<bool>("ITreasuriesDataContext.FoundTreasuryOracle", new object[] { treasuryId });
        }

        string ITreasuriesDataContext.GetRecognizedRightsOracle(int treasuryId)
        {
            return RecordAndReturn<string>("ITreasuriesDataContext.GetRecognizedRightsOracle", new object[] { treasuryId });
        }

        Response ITreasuriesDataContext.InsertTreasuryOracle(PRE_TESORERIA treasury)
        {
            return RecordAndReturn<Response>("ITreasuriesDataContext.InsertTreasuryOracle", new object[] { treasury });
        }

        Response ITreasuriesDataContext.UpdateTreasuryOracle(PRE_TESORERIA treasury)
        {
            return RecordAndReturn<Response>("ITreasuriesDataContext.UpdateTreasuryOracle", new object[] { treasury });
        }

        Response ITreasuriesDataContext.DeleteTreasuryOracle(int treasuryId)
        {
            return RecordAndReturn<Response>("ITreasuriesDataContext.DeleteTreasuryOracle", new object[] { treasuryId });
        }

        Response ITreasuriesDataContext.DeleteBankDateOracle(int treasuryId)
        {
            return RecordAndReturn<Response>("ITreasuriesDataContext.DeleteBankDateOracle", new object[] { treasuryId });
        }

        List<PRE_LINEA_TESORERIA> ITreasuryLinesDataContext.GetTreasuryLines()
        {
            return RecordAndReturn<List<PRE_LINEA_TESORERIA>>("ITreasuryLinesDataContext.GetTreasuryLines", new object[0]);
        }

        Response ITreasuryLinesDataContext.InsertTreasuryLine(PRE_LINEA_TESORERIA treasuryLine)
        {
            return RecordAndReturn<Response>("ITreasuryLinesDataContext.InsertTreasuryLine", new object[] { treasuryLine });
        }

        Response ITreasuryLinesDataContext.UpdateTreasuryLine(PRE_LINEA_TESORERIA treasuryLine)
        {
            return RecordAndReturn<Response>("ITreasuryLinesDataContext.UpdateTreasuryLine", new object[] { treasuryLine });
        }

        Response ITreasuryLinesDataContext.DeleteTreasuryLine(int treasuryLineId)
        {
            return RecordAndReturn<Response>("ITreasuryLinesDataContext.DeleteTreasuryLine", new object[] { treasuryLineId });
        }

        List<User> IUserDataContext.GetUsers()
        {
            return RecordAndReturn<List<User>>("IUserDataContext.GetUsers", new object[0]);
        }

        User IUserDataContext.Login(string login, string password)
        {
            return RecordAndReturn<User>("IUserDataContext.Login", new object[] { login, password });
        }

        Response IUserDataContext.InsertUser(User user)
        {
            return RecordAndReturn<Response>("IUserDataContext.InsertUser", new object[] { user });
        }

        Response IUserDataContext.UpdateUser(User user)
        {
            return RecordAndReturn<Response>("IUserDataContext.UpdateUser", new object[] { user });
        }

        Response IUserDataContext.UpdatePassword(User user)
        {
            return RecordAndReturn<Response>("IUserDataContext.UpdatePassword", new object[] { user });
        }

        Response IUserDataContext.UpdateStatus(int userId, bool obsolete)
        {
            return RecordAndReturn<Response>("IUserDataContext.UpdateStatus", new object[] { userId, obsolete });
        }

        Response IUserDataContext.DeleteUser(int userId)
        {
            return RecordAndReturn<Response>("IUserDataContext.DeleteUser", new object[] { userId });
        }

        User IUserDataContext.GetUserByCode(int userId)
        {
            return RecordAndReturn<User>("IUserDataContext.GetUserByCode", new object[] { userId });
        }

        User IUserDataContext.GetUserByEmail(string email)
        {
            return RecordAndReturn<User>("IUserDataContext.GetUserByEmail", new object[] { email });
        }

        User IUserDataContext.GetUserByUserName(string usu_login)
        {
            return RecordAndReturn<User>("IUserDataContext.GetUserByUserName", new object[] { usu_login });
        }

    }

}
