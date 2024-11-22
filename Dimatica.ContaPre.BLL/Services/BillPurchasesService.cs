namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class BillPurchasesService : IBillPurchasesService
    {
        #region IBillPurchasesService Members

        public List<PRE_FACTURA_COMPRA> GetBillPurchases(int? exerciseYear, int? administrativeId, string billNumber, string billDate, decimal? billAmount, string roDate, int? providerId, decimal? roAmount, string order)
        {
            return BillPurchasesDataContext.Instance.GetBillPurchases(exerciseYear, administrativeId, billNumber, billDate, billAmount, roDate, providerId, roAmount, order);
        }

        public List<PRE_FACTURA_COMPRA> GetByDocument(int documentId)
        {
            return BillPurchasesDataContext.Instance.GetByDocument(documentId);
        }

        public List<PRE_FACTURA_COMPRA> GetByAccountingRecord(int accountingRecordId)
        {
            return BillPurchasesDataContext.Instance.GetByAccountingRecord(accountingRecordId);
        }

        public Response DeleteByAccountingRecord(int accountingRecordId)
        {
            return BillPurchasesDataContext.Instance.DeleteByAccountingRecord(accountingRecordId);
        }

        public Response DeleteBillPurchase(int id)
        {
            return BillPurchasesDataContext.Instance.DeleteBillPurchase(id);
        }

        public Response UpdateDocument(int accountingId, string date, int providerId, int documentId, int userId)
        {
            return BillPurchasesDataContext.Instance.UpdateDocument(accountingId, date, providerId, documentId, userId);
        }

        public decimal GetDocumentRetentionAmount(int documentId)
        {
            return BillPurchasesDataContext.Instance.GetDocumentRetentionAmount(documentId);
        }

        public decimal GetDocumentBoeAmount(int documentId)
        {
            return BillPurchasesDataContext.Instance.GetDocumentBoeAmount(documentId);
        }

        #endregion
    }
}