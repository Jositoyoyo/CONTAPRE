namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class TreasuriesService : ITreasuriesService
    {
        #region ITreasuriesService Members

        public PRE_TESORERIA GetById(int treasuryId)
        {
            return TreasuriesDataContext.Instance.GetById(treasuryId);
        }

        public List<PRE_TESORERIA> GetAllById(int treasuryId)
        {
            return TreasuriesDataContext.Instance.GetAllById(treasuryId);
        }

        public List<PRE_TESORERIA> GetDocuments(int? documentId, int? extraBudgetaryId, int? treasuryId)
        {
            return TreasuriesDataContext.Instance.GetDocuments(documentId, extraBudgetaryId, treasuryId);
        }

        public List<PRE_DOCUMENTO_CONTABLE> GetDocumentsToBound(int? budgetYear, decimal? amount, int? providerCode, string checkNumber, int originCode)
        {
            return TreasuriesDataContext.Instance.GetDocumentsToBound(budgetYear, amount, providerCode, checkNumber, originCode);
        }

        public List<PRE_DOCUMENTO_CONTABLE> GetPaymentsRegister(int year, int? originCode, int? providerIncomes, int? providerSpends, string sinceDate, string untilDate)
        {
            return TreasuriesDataContext.Instance.GetPaymentsRegister(year, originCode, providerIncomes, providerSpends, sinceDate, untilDate);
        }

        public List<PRE_TESORERIA> GetTreasuryByFilters(int? exerciseYear, int? origingCode, decimal? importe, string sinceBankDate, string untilBankDate, int? payFormCode, string checkNumber, bool? treasuryHave, string description, string sinceDateEntry, string untilDateEntry, int? registerTypeCode, bool? isCanceled, bool? isBound, string pendingDate, int? sinceDocYear, int? untilDocYear, string order)
        {
            return TreasuriesDataContext.Instance.GetTreasuryByFilters(exerciseYear, origingCode, importe, sinceBankDate, untilBankDate, payFormCode, checkNumber, treasuryHave, description, sinceDateEntry, untilDateEntry, registerTypeCode, isCanceled, isBound, pendingDate, sinceDocYear, untilDocYear, order);
        }

        public List<PRE_TESORERIA> GetAccountingBook(DateTime since, DateTime until)
        {
            return TreasuriesDataContext.Instance.GetAccountingBook(since, until);
        }

        public List<PRE_TESORERIA> GetBankStatement(DateTime since, DateTime until)
        {
            return TreasuriesDataContext.Instance.GetBankStatement(since, until);
        }

        public List<PRE_TESORERIA> GetBlockListing(DateTime register)
        {
            return TreasuriesDataContext.Instance.GetBlockListing(register);
        }

        public List<PRE_TESORERIA> GetPaymentRecord(DateTime since, DateTime until)
        {
            return TreasuriesDataContext.Instance.GetPaymentRecord(since, until);
        }

        public List<PRE_DETALLE_HOJA_ARQUEO> GetTonnageSheetDetails(int tonnageSheetCode)
        {
            return TreasuriesDataContext.Instance.GetTonnageSheetDetails(tonnageSheetCode);
        }

        public int GetBoundDocumentsCount(int treasuryId)
        {
            return TreasuriesDataContext.Instance.GetBoundDocumentsCount(treasuryId);
        }

        public Response InsertTreasury(PRE_TESORERIA treasury)
        {
            return TreasuriesDataContext.Instance.InsertTreasury(treasury);
        }

        public Response UpdateTreasury(PRE_TESORERIA treasury)
        {
            return TreasuriesDataContext.Instance.UpdateTreasury(treasury);
        }

        public Response DeleteBankDate(int treasuryId, int userId)
        {
            return TreasuriesDataContext.Instance.DeleteBankDate(treasuryId, userId);
        }

        public Response DeleteTreasury(int treasuryId, int userId)
        {
            return TreasuriesDataContext.Instance.DeleteTreasury(treasuryId, userId);
        }

        public Response DeleteDocument(int treasuryDocumentId, int userId)
        {
            return TreasuriesDataContext.Instance.DeleteDocument(treasuryDocumentId, userId);
        }

        public decimal GetAmount(int treasuryId)
        {
            return TreasuriesDataContext.Instance.GetAmount(treasuryId);
        }

        public decimal GetDocumentsAmount(int treasuryId)
        {
            return TreasuriesDataContext.Instance.GetDocumentsAmount(treasuryId);
        }

        public Response BoundDocument(PRE_TESORERIA_DOCUMENTO document)
        {
            return TreasuriesDataContext.Instance.BoundDocument(document);
        }

        public bool FoundTreasuryOracle(int treasuryId)
        {
            return TreasuriesDataContext.Instance.FoundTreasuryOracle(treasuryId);
        }

        public string GetRecognizedRightsOracle(int treasuryId)
        {
            return TreasuriesDataContext.Instance.GetRecognizedRightsOracle(treasuryId);
        }

        public Response InsertTreasuryOracle(PRE_TESORERIA treasury)
        {
            return TreasuriesDataContext.Instance.InsertTreasuryOracle(treasury);
        }

        public Response UpdateTreasuryOracle(PRE_TESORERIA treasury)
        {
            return TreasuriesDataContext.Instance.UpdateTreasuryOracle(treasury);
        }

        public Response DeleteTreasuryOracle(int treasuryId)
        {
            return TreasuriesDataContext.Instance.DeleteTreasuryOracle(treasuryId);
        }

        public Response DeleteBankDateOracle(int treasuryId)
        {
            return TreasuriesDataContext.Instance.DeleteBankDateOracle(treasuryId);
        }

        #endregion
    }
}