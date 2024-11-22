namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.OL.Procedures;

    #endregion

    public interface IAccountingRecordsService
    {
        #region Public Methods

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

        #endregion
    }
}