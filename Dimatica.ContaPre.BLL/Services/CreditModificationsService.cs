namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.OL.Procedures;

    #endregion

    public class CreditModificationsService : ICreditModificationsService
    {
        #region ICreditModificationsService Members

        public List<Year> GetYears()
        {
            return CreditModificationsDataContext.Instance.GetYears();
        }

        public List<PRE_MODIFICACION_CREDITO> GetByFilters(int year, int? since, int? until)
        {
            return CreditModificationsDataContext.Instance.GetByFilters(year, since, until);
        }

        public PRE_MODIFICACION_CREDITO GetById(int creditModificationId)
        {
            return CreditModificationsDataContext.Instance.GetById(creditModificationId);
        }

        public int GetNextOrderByYear(int year)
        {
            return CreditModificationsDataContext.Instance.GetNextOrderByYear(year);
        }

        public Response InsertCreditModification(PRE_MODIFICACION_CREDITO creditModification)
        {
            return CreditModificationsDataContext.Instance.InsertCreditModification(creditModification);
        }

        public Response UpdateCreditModification(PRE_MODIFICACION_CREDITO creditModification)
        {
            return CreditModificationsDataContext.Instance.UpdateCreditModification(creditModification);
        }

        public Response GenerateRecordsCreditModification(PRE_MODIFICACION_CREDITO creditModification)
        {
            return CreditModificationsDataContext.Instance.GenerateRecordsCreditModification(creditModification);
        }

        public Response ExecuteCreditModification(int creditModificationId, int userId)
        {
            return CreditModificationsDataContext.Instance.ExecuteCreditModification(creditModificationId, userId);
        }

        #endregion
    }
}