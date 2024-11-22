namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface IDocumentTypesService
    {
        #region Public Methods

        List<PRE_TIPO_DOCUMENTO> GetDocumentTypes(string type);

        List<PRE_TIPO_DOCUMENTO> GetDocumentTypesToCombo(string type);

        List<PRE_TIPO_DOCUMENTO> GetDocumentTypesDrPhase();

        Response InsertDocumentType(PRE_TIPO_DOCUMENTO documentType);

        Response UpdateDocumentType(PRE_TIPO_DOCUMENTO documentType);

        Response DeleteDocumentType(int documentTypeId);

        #endregion
    }
}