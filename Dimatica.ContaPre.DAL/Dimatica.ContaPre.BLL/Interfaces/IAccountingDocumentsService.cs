namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface IAccountingDocumentsService
    {
        #region Public Methods

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

        #endregion
    }
}