namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface IBillPurchasesService
    {
        #region Public Methods

        List<PRE_FACTURA_COMPRA> GetBillPurchases(int? exerciseYear, int? administrativeId, string billNumber, string billDate, decimal? billAmount, string roDate, int? providerId, decimal? roAmount, string empty);

        List<PRE_FACTURA_COMPRA> GetByDocument(int documentId);

        List<PRE_FACTURA_COMPRA> GetByAccountingRecord(int accountingRecordId);

        Response DeleteByAccountingRecord(int accountingRecordId);

        Response DeleteBillPurchase(int id);

        Response UpdateDocument(int accountingId, string date, int providerId, int documentId, int userId);

        decimal GetDocumentRetentionAmount(int documentId);

        decimal GetDocumentBoeAmount(int documentId);

        #endregion
    }
}