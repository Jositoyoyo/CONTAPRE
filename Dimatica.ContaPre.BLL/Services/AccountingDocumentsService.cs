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

    public class AccountingDocumentsService : IAccountingDocumentsService
    {
        #region IAccountingDocumentsService Members

        public PRE_DOCUMENTO_CONTABLE GetById(int id)
        {
            return AccountingDocumentsDataContext.Instance.GetById(id);
        } 

        public Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> GetByIdReport(int id)
        {
            return AccountingDocumentsDataContext.Instance.GetByIdReport(id);
        }

        public Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> GetSpendRcById(int id)
        {
            return AccountingDocumentsDataContext.Instance.GetSpendRcById(id);
        } 

        public Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> GetSpendAdById(int id)
        {
            return AccountingDocumentsDataContext.Instance.GetSpendAdById(id);
        } 

        public Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> GetSpendOById(int id)
        {
            return AccountingDocumentsDataContext.Instance.GetSpendOById(id);
        } 

        public Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> GetSpendPById(int id)
        {
            return AccountingDocumentsDataContext.Instance.GetSpendPById(id);
        } 

        public Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>, List<PRE_EXPEDIENTE_CONTABLE>> GetSpendIncomeDiscountsById(int id)
        {
            return AccountingDocumentsDataContext.Instance.GetSpendIncomeDiscountsById(id);
        } 

        public Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, PRE_DOCUMENTO_APLICACION, List<PRE_EXP_EXTRAPRE>> GetSpendPRecordById(int id)
        {
            return AccountingDocumentsDataContext.Instance.GetSpendPRecordById(id);
        } 

        public Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>, List<PRE_EXPEDIENTE_CONTABLE>> GetSpendORecordById(int id)
        {
            return AccountingDocumentsDataContext.Instance.GetSpendORecordById(id);
        } 

        public Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, PRE_DOCUMENTO_APLICACION, List<PRE_EXPEDIENTE_CONTABLE>> GetSpendPRecordDiscountsById(int id)
        {
            return AccountingDocumentsDataContext.Instance.GetSpendPRecordDiscountsById(id);
        } 
        
        public List<PRE_DOCUMENTO_CONTABLE> GetByIdAndType(string type, int? accountingRecordId)
        {
            return AccountingDocumentsDataContext.Instance.GetByIdAndType(type, accountingRecordId);
        }

        public int GetNextTonnageSheetNumber(int year)
        {
            return AccountingDocumentsDataContext.Instance.GetNextTonnageSheetNumber(year);
        }

        public int GetNextTonnageSheetNumber50(int year)
        {
            return AccountingDocumentsDataContext.Instance.GetNextTonnageSheetNumber50(year);
        }

        public int GetLastMiNumber(int year)
        {
            return AccountingDocumentsDataContext.Instance.GetLastMiNumber(year);
        }

        public int CheckMiNumber(int year, int miNumber, int documentId, int id)
        {
            return AccountingDocumentsDataContext.Instance.CheckMiNumber(year, miNumber, documentId, id);
        }

        public int CheckTonnageSheet(int tonnageSheetYear, int tonnageSheet, bool is50)
        {
            return AccountingDocumentsDataContext.Instance.CheckTonnageSheet(tonnageSheetYear, tonnageSheet, is50);
        }

        public Response InsertAccountingDocument(PRE_DOCUMENTO_CONTABLE accountingDocument)
        {
            return AccountingDocumentsDataContext.Instance.InsertAccountingDocument(accountingDocument);
        }

        public Response UpdateAccountingDocument(PRE_DOCUMENTO_CONTABLE accountingDocument)
        {
            return AccountingDocumentsDataContext.Instance.UpdateAccountingDocument(accountingDocument);
        }

        public Response DeleteAccountingDocument(int accountingId, int userId)
        {
            return AccountingDocumentsDataContext.Instance.DeleteAccountingDocument(accountingId, userId);
        }

        public Response UpdateTonnageSheet(int? tonnageSheet, int? tonnageSheetYear, int? tonnageSheet50, int? tonnageSheet50Year, int account, int userId)
        {
            return AccountingDocumentsDataContext.Instance.UpdateTonnageSheet(tonnageSheet, tonnageSheetYear, tonnageSheet50, tonnageSheet50Year, account, userId);
        }

        public List<PRE_DOCUMENTO_APLICACION> GetApplicationsByDocumentId(int documentId)
        {
            return AccountingDocumentsDataContext.Instance.GetApplicationsByDocumentId(documentId);
        }

        public List<PRE_DOCUMENTO_APLICACION> GetApplicationsByDocumentCacs(int documentId, string cacsCode)
        {
            return AccountingDocumentsDataContext.Instance.GetApplicationsByDocumentCacs(documentId, cacsCode);
        }

        public Response InsertApplication(PRE_DOCUMENTO_APLICACION application)
        {
            return AccountingDocumentsDataContext.Instance.InsertApplication(application);
        }

        public Response UpdateApplication(PRE_DOCUMENTO_APLICACION application)
        {
            return AccountingDocumentsDataContext.Instance.UpdateApplication(application);
        }

        public Response DeleteApplication(int id, int userId)
        {
            return AccountingDocumentsDataContext.Instance.DeleteApplication(id, userId);
        }

        public Response UpdateIncomeDiscounts(PRE_DOCUMENTO_CONTABLE accountingDocument)
        {
            return AccountingDocumentsDataContext.Instance.UpdateIncomeDiscounts(accountingDocument);
        }

        public Response UpdateDcDescription(PRE_DOCUMENTO_CONTABLE accountingDocument)
        {
            return AccountingDocumentsDataContext.Instance.UpdateDcDescription(accountingDocument);
        }

        public Response UpdateRepair(PRE_DOCUMENTO_CONTABLE accountingDocument)
        {
            return AccountingDocumentsDataContext.Instance.UpdateRepair(accountingDocument);
        }

        public List<PRE_FACTURA_COMPRA> GetPurchases(int accountingDocumentId)
        {
            return AccountingDocumentsDataContext.Instance.GetPurchases(accountingDocumentId);
        }

        public int GetPurchasesCount(int accountingDocumentId)
        {
            return AccountingDocumentsDataContext.Instance.GetPurchasesCount(accountingDocumentId);
        }

        public int GetIncomeDiscountsCount(int accountingDocumentId)
        {
            return AccountingDocumentsDataContext.Instance.GetIncomeDiscountsCount(accountingDocumentId);
        }

        public int GetDocumentsCount(int accountingDocumentId)
        {
            return AccountingDocumentsDataContext.Instance.GetDocumentsCount(accountingDocumentId);
        }

        public bool HaveAdPhase(int accountingDocumentId)
        {
            return AccountingDocumentsDataContext.Instance.HaveAdPhase(accountingDocumentId);
        }

        public bool HaveOPhase(int accountingDocumentId)
        {
            return AccountingDocumentsDataContext.Instance.HaveOPhase(accountingDocumentId);
        }

        public bool HavePPhase(int accountingDocumentId)
        {
            return AccountingDocumentsDataContext.Instance.HavePPhase(accountingDocumentId);
        }

        public bool HaveExtraBudgetaries(int accountingDocumentId)
        {
            return AccountingDocumentsDataContext.Instance.HaveExtraBudgetaries(accountingDocumentId);
        }

        public bool HaveIncomeDiscounts(int accountingDocumentId)
        {
            return AccountingDocumentsDataContext.Instance.HaveIncomeDiscounts(accountingDocumentId);
        }

        public bool HaveRcPhase(int accountingDocumentId)
        {
            return AccountingDocumentsDataContext.Instance.HaveRcPhase(accountingDocumentId);
        }

        public PRE_EXPEDIENTE_CONTABLE GetAccountingRecord(int accountingDocumentId)
        {
            return AccountingDocumentsDataContext.Instance.GetAccountingRecord(accountingDocumentId);
        }

        public int GetProviderDc(int accountingDocumentId)
        {
            return AccountingDocumentsDataContext.Instance.GetProviderDc(accountingDocumentId);
        }

        public Response UpdateBillConcept(int id, string concept, int userId)
        {
            return AccountingDocumentsDataContext.Instance.UpdateBillConcept(id, concept, userId);
        }

        public Response UpdateTransferNumber(int id, string checkNumber, int userId)
        {
            return AccountingDocumentsDataContext.Instance.UpdateTransferNumber(id, checkNumber, userId);
        }

        public Response UpdatePayBankOracle(DateTime? payBankDate, int billCode)
        {
            return AccountingDocumentsDataContext.Instance.UpdatePayBankOracle(payBankDate, billCode);
        }

        public Response UpdateHistoryOracle(string certificate, string description)
        {
            return AccountingDocumentsDataContext.Instance.UpdateHistoryOracle(certificate, description);
        }

        #endregion
    }
}