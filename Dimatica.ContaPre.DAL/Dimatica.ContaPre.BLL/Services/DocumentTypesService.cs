namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class DocumentTypesService : IDocumentTypesService
    {
        #region IDocumentTypesService Members

        public List<PRE_TIPO_DOCUMENTO> GetDocumentTypes(string type)
        {
            return DocumentTypesDataContext.Instance.GetDocumentTypes(type);
        }   
        
        public List<PRE_TIPO_DOCUMENTO> GetDocumentTypesToCombo(string type)
        {
            return DocumentTypesDataContext.Instance.GetDocumentTypesToCombo(type);
        }    
        
        public List<PRE_TIPO_DOCUMENTO> GetDocumentTypesDrPhase()
        {
            return DocumentTypesDataContext.Instance.GetDocumentTypesDrPhase();
        }

        public Response InsertDocumentType(PRE_TIPO_DOCUMENTO documentType)
        {
            return DocumentTypesDataContext.Instance.InsertDocumentType(documentType);
        }

        public Response UpdateDocumentType(PRE_TIPO_DOCUMENTO documentType)
        {
            return DocumentTypesDataContext.Instance.UpdateDocumentType(documentType);
        }

        public Response DeleteDocumentType(int documentTypeId)
        {
            return DocumentTypesDataContext.Instance.DeleteDocumentType(documentTypeId);
        }

        #endregion
    }
}