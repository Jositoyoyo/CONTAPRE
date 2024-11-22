namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class CreditModificationTypesService : ICreditModificationTypesService
    {
        #region ICreditModificationTypesService Members

        public List<PRE_TIPO_MODIFICACION_CREDITO> GetCreditModificationTypes(string type)
        {
            return CreditModificationTypesDataContext.Instance.GetCreditModificationTypes(type);
        }

        public Response InsertCreditModificationType(PRE_TIPO_MODIFICACION_CREDITO creditModificationType)
        {
            return CreditModificationTypesDataContext.Instance.InsertCreditModificationType(creditModificationType);
        }

        public Response UpdateCreditModificationType(PRE_TIPO_MODIFICACION_CREDITO creditModificationType)
        {
            return CreditModificationTypesDataContext.Instance.UpdateCreditModificationType(creditModificationType);
        }

        public Response DeleteCreditModificationType(int creditModificationTypeId)
        {
            return CreditModificationTypesDataContext.Instance.DeleteCreditModificationType(creditModificationTypeId);
        }

        #endregion
    }
}