namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface ITreasuriesService
    {
        #region Public Methods

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

        #endregion
    }
}