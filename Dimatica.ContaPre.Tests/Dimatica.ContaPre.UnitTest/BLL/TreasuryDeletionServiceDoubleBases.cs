namespace Dimatica.ContaPre.UnitTest.BLL
{
    using System;
    using System.Collections.Generic;
    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    internal abstract class TreasuryServiceDoubleBase : ITreasuriesService
    {
        public virtual PRE_TESORERIA GetById(int treasuryId) { return default(PRE_TESORERIA); }
        public virtual List<PRE_TESORERIA> GetAllById(int treasuryId) { return default(List<PRE_TESORERIA>); }
        public virtual List<PRE_TESORERIA> GetDocuments(int? documentId, int? extraBudgetaryId, int? treasuryId) { return default(List<PRE_TESORERIA>); }
        public virtual List<PRE_DOCUMENTO_CONTABLE> GetDocumentsToBound(int? budgetYear, decimal? amount, int? providerCode, string checkNumber, int originCode) { return default(List<PRE_DOCUMENTO_CONTABLE>); }
        public virtual List<PRE_DOCUMENTO_CONTABLE> GetPaymentsRegister(int year, int? originCode, int? providerIncomes, int? providerSpends, string sinceDate, string untilDate) { return default(List<PRE_DOCUMENTO_CONTABLE>); }
        public virtual List<PRE_TESORERIA> GetTreasuryByFilters(int? exerciseYear, int? origingCode, decimal? importe, string sinceBankDate, string untilBankDate, int? payFormCode, string checkNumber, bool? treasuryHave, string description, string sinceDateEntry, string untilDateEntry, int? registerTypeCode, bool? isCanceled, bool? isBound, string pendingDate, int? sinceDocYear, int? untilDocYear, string order) { return default(List<PRE_TESORERIA>); }
        public virtual List<PRE_TESORERIA> GetAccountingBook(DateTime since, DateTime until) { return default(List<PRE_TESORERIA>); }
        public virtual List<PRE_TESORERIA> GetBankStatement(DateTime since, DateTime until) { return default(List<PRE_TESORERIA>); }
        public virtual List<PRE_TESORERIA> GetBlockListing(DateTime register) { return default(List<PRE_TESORERIA>); }
        public virtual List<PRE_TESORERIA> GetPaymentRecord(DateTime since, DateTime until) { return default(List<PRE_TESORERIA>); }
        public virtual List<PRE_DETALLE_HOJA_ARQUEO> GetTonnageSheetDetails(int tonnageSheetCode) { return default(List<PRE_DETALLE_HOJA_ARQUEO>); }
        public virtual int GetBoundDocumentsCount(int treasuryId) { return default(int); }
        public virtual Response InsertTreasury(PRE_TESORERIA treasury) { return default(Response); }
        public virtual Response UpdateTreasury(PRE_TESORERIA treasury) { return default(Response); }
        public virtual Response DeleteBankDate(int treasuryId, int userId) { return default(Response); }
        public virtual Response DeleteTreasury(int treasuryId, int userId) { return default(Response); }
        public virtual Response DeleteDocument(int treasuryDocumentId, int userId) { return default(Response); }
        public virtual decimal GetAmount(int treasuryId) { return default(decimal); }
        public virtual decimal GetDocumentsAmount(int treasuryId) { return default(decimal); }
        public virtual Response BoundDocument(PRE_TESORERIA_DOCUMENTO document) { return default(Response); }
        public virtual bool FoundTreasuryOracle(int treasuryId) { return default(bool); }
        public virtual string GetRecognizedRightsOracle(int treasuryId) { return default(string); }
        public virtual Response InsertTreasuryOracle(PRE_TESORERIA treasury) { return default(Response); }
        public virtual Response UpdateTreasuryOracle(PRE_TESORERIA treasury) { return default(Response); }
        public virtual Response DeleteTreasuryOracle(int treasuryId) { return default(Response); }
        public virtual Response DeleteBankDateOracle(int treasuryId) { return default(Response); }
    }

    internal abstract class AccountingDocumentsServiceDoubleBase : IAccountingDocumentsService
    {
        public virtual PRE_DOCUMENTO_CONTABLE GetById(int id) { return default(PRE_DOCUMENTO_CONTABLE); }
        public virtual List<PRE_DOCUMENTO_CONTABLE> GetByIdAndType(string type, int? accountingRecordId) { return default(List<PRE_DOCUMENTO_CONTABLE>); }
        public virtual Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> GetByIdReport(int id) { return default(Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>>); }
        public virtual Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> GetSpendRcById(int id) { return default(Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>>); }
        public virtual Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> GetSpendAdById(int id) { return default(Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>>); }
        public virtual Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> GetSpendOById(int id) { return default(Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>>); }
        public virtual Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> GetSpendPById(int id) { return default(Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>>); }
        public virtual Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>, List<PRE_EXPEDIENTE_CONTABLE>> GetSpendIncomeDiscountsById(int id) { return default(Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>, List<PRE_EXPEDIENTE_CONTABLE>>); }
        public virtual Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, PRE_DOCUMENTO_APLICACION, List<PRE_EXP_EXTRAPRE>> GetSpendPRecordById(int id) { return default(Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, PRE_DOCUMENTO_APLICACION, List<PRE_EXP_EXTRAPRE>>); }
        public virtual Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>, List<PRE_EXPEDIENTE_CONTABLE>> GetSpendORecordById(int id) { return default(Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>, List<PRE_EXPEDIENTE_CONTABLE>>); }
        public virtual Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, PRE_DOCUMENTO_APLICACION, List<PRE_EXPEDIENTE_CONTABLE>> GetSpendPRecordDiscountsById(int id) { return default(Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, PRE_DOCUMENTO_APLICACION, List<PRE_EXPEDIENTE_CONTABLE>>); }
        public virtual int GetNextTonnageSheetNumber(int year) { return default(int); }
        public virtual int GetNextTonnageSheetNumber50(int year) { return default(int); }
        public virtual int GetLastMiNumber(int year) { return default(int); }
        public virtual int CheckMiNumber(int year, int miNumber, int documentId, int id) { return default(int); }
        public virtual int CheckTonnageSheet(int tonnageSheetYear, int tonnageSheet, bool is50) { return default(int); }
        public virtual Response InsertAccountingDocument(PRE_DOCUMENTO_CONTABLE accountingDocument) { return default(Response); }
        public virtual Response UpdateAccountingDocument(PRE_DOCUMENTO_CONTABLE accountingDocument) { return default(Response); }
        public virtual Response DeleteAccountingDocument(int accountingId, int userId) { return default(Response); }
        public virtual Response UpdateTonnageSheet(int? tonnageSheet, int? tonnageSheetYear, int? tonnageSheet50, int? tonnageSheet50Year, int account, int userId) { return default(Response); }
        public virtual List<PRE_DOCUMENTO_APLICACION> GetApplicationsByDocumentId(int documentId) { return default(List<PRE_DOCUMENTO_APLICACION>); }
        public virtual List<PRE_DOCUMENTO_APLICACION> GetApplicationsByDocumentCacs(int documentId, string cacsCode) { return default(List<PRE_DOCUMENTO_APLICACION>); }
        public virtual Response InsertApplication(PRE_DOCUMENTO_APLICACION application) { return default(Response); }
        public virtual Response UpdateApplication(PRE_DOCUMENTO_APLICACION application) { return default(Response); }
        public virtual Response DeleteApplication(int id, int userId) { return default(Response); }
        public virtual Response UpdateIncomeDiscounts(PRE_DOCUMENTO_CONTABLE accountingDocument) { return default(Response); }
        public virtual Response UpdateDcDescription(PRE_DOCUMENTO_CONTABLE accountingDocument) { return default(Response); }
        public virtual Response UpdateRepair(PRE_DOCUMENTO_CONTABLE accountingDocument) { return default(Response); }
        public virtual List<PRE_FACTURA_COMPRA> GetPurchases(int accountingDocumentId) { return default(List<PRE_FACTURA_COMPRA>); }
        public virtual int GetPurchasesCount(int accountingDocumentId) { return default(int); }
        public virtual int GetIncomeDiscountsCount(int accountingDocumentId) { return default(int); }
        public virtual int GetDocumentsCount(int accountingDocumentId) { return default(int); }
        public virtual bool HaveAdPhase(int accountingDocumentId) { return default(bool); }
        public virtual bool HaveOPhase(int accountingDocumentId) { return default(bool); }
        public virtual bool HavePPhase(int accountingDocumentId) { return default(bool); }
        public virtual bool HaveExtraBudgetaries(int accountingDocumentId) { return default(bool); }
        public virtual bool HaveIncomeDiscounts(int accountingDocumentId) { return default(bool); }
        public virtual bool HaveRcPhase(int accountingDocumentId) { return default(bool); }
        public virtual PRE_EXPEDIENTE_CONTABLE GetAccountingRecord(int accountingDocumentId) { return default(PRE_EXPEDIENTE_CONTABLE); }
        public virtual int GetProviderDc(int accountingDocumentId) { return default(int); }
        public virtual Response UpdateBillConcept(int id, string concept, int userId) { return default(Response); }
        public virtual Response UpdateTransferNumber(int id, string checkNumber, int userId) { return default(Response); }
        public virtual Response UpdatePayBankOracle(DateTime? payBankDate, int billCode) { return default(Response); }
        public virtual Response UpdateHistoryOracle(string certificate, string description) { return default(Response); }
    }

}
