namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface ICreditModificationTypesService
    {
        #region Public Methods

        List<PRE_TIPO_MODIFICACION_CREDITO> GetCreditModificationTypes(string type);

        Response InsertCreditModificationType(PRE_TIPO_MODIFICACION_CREDITO creditModificationType);

        Response UpdateCreditModificationType(PRE_TIPO_MODIFICACION_CREDITO creditModificationType);

        Response DeleteCreditModificationType(int creditModificationTypeId);

        #endregion
    }
}