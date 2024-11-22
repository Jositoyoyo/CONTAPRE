namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.OL.Procedures;

    #endregion

    public class AccountingRecordsService : IAccountingRecordsService
    {
        #region IAccountingRecordsService Members

        public List<PRE_EXPEDIENTE_CONTABLE> GetSpends(int? year, int? recordNumberYear, int? provenanceCode, int? providerCode, string providerNif, bool? multiYear, string order, string orderSent)
        {
            return AccountingRecordsDataContext.Instance.GetSpends(year, recordNumberYear, provenanceCode, providerCode, providerNif, multiYear, order, orderSent);
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetSpendsReports(int? year, int? recordNumberYear, int? provenanceCode, int? providerCode, string providerNif, bool? multiYear)
        {
            return AccountingRecordsDataContext.Instance.GetSpendsReports(year, recordNumberYear, provenanceCode, providerCode, providerNif, multiYear);
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetSpendsWithAppAmount(int? year, int? chapterCode, int? articleCode, int? conceptCode, int? subConceptCode, decimal? sinceAmount, decimal? untilAmount, bool? isBound, bool? isSquare, int? providerCode, string providerNif, int? documentTypeCode, int? docNumber, string order, string sense)
        {
            return AccountingRecordsDataContext.Instance.GetSpendsWithAppAmount(year, chapterCode, articleCode, conceptCode, subConceptCode, sinceAmount, untilAmount, isBound, isSquare, providerCode, providerNif, documentTypeCode, docNumber, order, sense);
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetIncomes(int year, string description, bool? isSquare, int? providerCode, int? operationYear, string type, int? docNumber, string order)
        {
            return AccountingRecordsDataContext.Instance.GetIncomes(year, description, isSquare, providerCode, operationYear, type, docNumber, order);
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetIncomesWithAppAmount(int? year, string description, bool? isSquare, int? providerCode, string sinceDate, string untilDate, decimal? sinceAmount, decimal? untilAmount, int? chapterCode, int? articleCode, int? conceptCode, int? subConceptCode, bool? isBound, int? operationYear, string type, int? docNumber, string order, string sense)
        {
            return AccountingRecordsDataContext.Instance.GetIncomesWithAppAmount(year, description, isSquare, providerCode, sinceDate, untilDate, sinceAmount, untilAmount, chapterCode, articleCode, conceptCode, subConceptCode, isBound, operationYear, type, docNumber, order, sense);
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetByAdministrativeRecord(int administrativeRecordId)
        {
            return AccountingRecordsDataContext.Instance.GetByAdministrativeRecord(administrativeRecordId);
        }

        public PRE_EXPEDIENTE_CONTABLE GetById(int id)
        {
            return AccountingRecordsDataContext.Instance.GetById(id);
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetIncomesDr(int? budgetYear, int? exerciseYear, int? groupNumber, int? documentCode)
        {
            return AccountingRecordsDataContext.Instance.GetIncomesDr(budgetYear, exerciseYear, groupNumber, documentCode);
        }

        public Tuple<List<PRE_EXPEDIENTE_CONTABLE>, PRE_PARAMETROS> GetIncomesDrReport(int? budgetYear, int? exerciseYear, int? groupNumber, int? documentCode)
        {
            return AccountingRecordsDataContext.Instance.GetIncomesDrReport(budgetYear, exerciseYear, groupNumber, documentCode);
        }

        public Tuple<PRE_DOCUMENTO_CONTABLE, List<PRE_DOCUMENTO_APLICACION>, PRE_PARAMETROS> GetSpendCertificate(int documentCode)
        {
            return AccountingRecordsDataContext.Instance.GetSpendCertificate(documentCode);
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetIncomesAnnexedDr(int? budgetYear, int? exerciseYear, int? groupNumber, int? documentCode)
        {
            return AccountingRecordsDataContext.Instance.GetIncomesAnnexedDr(budgetYear, exerciseYear, groupNumber, documentCode);
        }

        public List<PRE_FACTURA_COMPRA> GetPurchaseBills(int accountingId, int documentId)
        {
            return AccountingRecordsDataContext.Instance.GetPurchaseBills(accountingId, documentId);
        }

        public List<PRE_PROVEEDOR> GetProviders(int accountingId)
        {
            return AccountingRecordsDataContext.Instance.GetProviders(accountingId);
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetMultiYears(int accountingId)
        {
            return AccountingRecordsDataContext.Instance.GetMultiYears(accountingId);
        }

        public List<PRE_FACTURA_COMPRA> GetProvidersPurchaseBills(int accountingId, string date, int providerId)
        {
            return AccountingRecordsDataContext.Instance.GetProvidersPurchaseBills(accountingId, date, providerId);
        }

        public decimal GetDrAmount(int id)
        {
            return AccountingRecordsDataContext.Instance.GetDrAmount(id);
        }

        public decimal GetMiAmount(int id)
        {
            return AccountingRecordsDataContext.Instance.GetMiAmount(id);
        }

        public decimal GetRcAmount(int accountingId, bool isPositive, int? documentId)
        {
            return AccountingRecordsDataContext.Instance.GetRcAmount(accountingId, isPositive, documentId);
        }

        public decimal GetAdAmount(int accountingId, bool isPositive, int? documentId)
        {
            return AccountingRecordsDataContext.Instance.GetAdAmount(accountingId, isPositive, documentId);
        }

        public decimal GetOAmount(int accountingId, bool isPositive, int? documentId)
        {
            return AccountingRecordsDataContext.Instance.GetOAmount(accountingId, isPositive, documentId);
        }

        public decimal GetPAmount(int accountingId, bool isPositive, int? documentId)
        {
            return AccountingRecordsDataContext.Instance.GetPAmount(accountingId, isPositive, documentId);
        }

        public decimal GetDiscountIEcAmount(int accountingId)
        {
            return AccountingRecordsDataContext.Instance.GetDiscountIEcAmount(accountingId);
        }

        public decimal GetApplicationsAmount(int documentId)
        {
            return AccountingRecordsDataContext.Instance.GetApplicationsAmount(documentId);
        }

        public decimal GetBillsAmount(int documentId)
        {
            return AccountingRecordsDataContext.Instance.GetBillsAmount(documentId);
        }

        public int GetLastYearNumber(int year, string type)
        {
            return AccountingRecordsDataContext.Instance.GetLastYearNumber(year, type);
        }

        public int GetLastDrNumber(int year)
        {
            return AccountingRecordsDataContext.Instance.GetLastDrNumber(year);
        }

        public List<Year> GetYears(string type)
        {
            return AccountingRecordsDataContext.Instance.GetYears(type);
        }

        public Response InsertAccountingRecord(PRE_EXPEDIENTE_CONTABLE accountingRecord)
        {
            return AccountingRecordsDataContext.Instance.InsertAccountingRecord(accountingRecord);
        }

        public Response UpdateAccountingRecord(PRE_EXPEDIENTE_CONTABLE accountingRecord)
        {
            return AccountingRecordsDataContext.Instance.UpdateAccountingRecord(accountingRecord);
        }

        public Response UpdateAccountingRecordSquare(int id, bool square, int userId)
        {
            return AccountingRecordsDataContext.Instance.UpdateAccountingRecordSquare(id, square, userId);
        }

        public Response DeleteAccountingRecord(int id, int userId)
        {
            return AccountingRecordsDataContext.Instance.DeleteAccountingRecord(id, userId);
        }

        public Response GroupDr(int groupNumber, int userId, List<int> documents)
        {
            return AccountingRecordsDataContext.Instance.GroupDr(groupNumber, userId, documents);
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetSpendsByConcept(int year, string cacsCode, DateTime? since, DateTime? until)
        {
            return AccountingRecordsDataContext.Instance.GetSpendsByConcept(year, cacsCode, since, until);
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetIncomesByConcept(int year, string cacsCode, string since, string until)
        {
            return AccountingRecordsDataContext.Instance.GetIncomesByConcept(year, cacsCode, since, until);
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetIncomesByConceptDrMi(int year, string cacsCode, DateTime? since, DateTime? until)
        {
            return AccountingRecordsDataContext.Instance.GetIncomesByConceptDrMi(year, cacsCode, since, until);
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetIncomesByPlace(int year, int? place, int since, int until)
        {
            return AccountingRecordsDataContext.Instance.GetIncomesByPlace(year, place, since, until);
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetSpendsByPlace(int year, int? place, int since, int until, string cacsCode)
        {
            return AccountingRecordsDataContext.Instance.GetSpendsByPlace(year, place, since, until, cacsCode);
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetIncomesDrAgreement(DateTime since, DateTime until)
        {
            return AccountingRecordsDataContext.Instance.GetIncomesDrAgreement(since, until);
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetIncomesPendingRightsRecognized(DateTime date)
        {
            return AccountingRecordsDataContext.Instance.GetIncomesPendingRightsRecognized(date);
        }

        #endregion
    }
}