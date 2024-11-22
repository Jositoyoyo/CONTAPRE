namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class SingsService : ISingsService
    {
        #region Implementation of ISingsService

        public List<PRE_SENALAMIENTO> GetByFilters(int year, int? pointingNumber, string date, decimal? amount, int? fileNumber, string type)
        {
            return SingsDataContext.Instance.GetByFilters(year, pointingNumber, date, amount, fileNumber, type);
        }

        public PRE_SENALAMIENTO GetById(int id)
        {
            return SingsDataContext.Instance.GetById(id);
        }

        public List<PRE_SENALAMIENTO_DOCUMENTO> GetDocumentsByPointingId(int pointingId)
        {
            return SingsDataContext.Instance.GetDocumentsByPointingId(pointingId);
        }

        public List<PRE_SENALAMIENTO_DOCUMENTO> GetTransfersByPointingId(int pointingId, bool groupTransfers)
        {
            return SingsDataContext.Instance.GetTransfersByPointingId(pointingId, groupTransfers);
        }

        public List<PRE_DOCUMENTO_CONTABLE> GetPendingDocuments(int year, string documentType)
        {
            return SingsDataContext.Instance.GetPendingDocuments(year, documentType);
        }

        public Response InsertPointing(PRE_SENALAMIENTO pointing)
        {
            return SingsDataContext.Instance.InsertPointing(pointing);
        }

        public Response DeletePointing(int id, int userId)
        {
            return SingsDataContext.Instance.DeletePointing(id, userId);
        }

        public int GetNextNumber(int year)
        {
            return SingsDataContext.Instance.GetNextNumber(year);
        }

        public Response InsertDocumentSing(int pointingId, int? documentCode, int? fileCode, int userId)
        {
            return SingsDataContext.Instance.InsertDocumentSing(pointingId, documentCode, fileCode, userId);
        }

        public Response DeleteDocument(int id, int userId)
        {
            return SingsDataContext.Instance.DeleteDocument(id, userId);
        }

        #endregion
    }
}