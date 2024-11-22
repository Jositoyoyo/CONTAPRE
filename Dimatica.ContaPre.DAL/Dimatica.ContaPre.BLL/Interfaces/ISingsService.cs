namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface ISingsService
    {
        #region Public Methods

        List<PRE_SENALAMIENTO> GetByFilters(int year, int? pointingNumber, string date, decimal? amount, int? fileNumber, string type);

        PRE_SENALAMIENTO GetById(int id);

        List<PRE_SENALAMIENTO_DOCUMENTO> GetDocumentsByPointingId(int pointingId);

        List<PRE_SENALAMIENTO_DOCUMENTO> GetTransfersByPointingId(int pointingId, bool groupTransfers);

        List<PRE_DOCUMENTO_CONTABLE> GetPendingDocuments(int year, string documentType);

        Response InsertPointing(PRE_SENALAMIENTO pointing);

        Response DeletePointing(int id, int userId);

        int GetNextNumber(int year);

        Response InsertDocumentSing(int pointingId, int? documentCode, int? fileCode, int userId);

        Response DeleteDocument(int id, int userId);

        #endregion
    }
}