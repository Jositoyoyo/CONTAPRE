namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class CreditModificationBudgetsService : ICreditModificationBudgetsService
    {
        #region ICreditModificationBudgetsService Members

        public List<PRE_MODIF_CREDITO_PRESUPUESTO> GetByCreditModification(int creditModificationId, string type)
        {
            return CreditModificationBudgetsDataContext.Instance.GetByCreditModification(creditModificationId, type);
        }

        public Response InsertCreditModificationBudget(PRE_MODIF_CREDITO_PRESUPUESTO creditModificationBudget, int year)
        {
            return CreditModificationBudgetsDataContext.Instance.InsertCreditModificationBudget(creditModificationBudget, year);
        }

        public Response UpdateCreditModificationBudget(PRE_MODIF_CREDITO_PRESUPUESTO creditModificationBudget, int year)
        {
            return CreditModificationBudgetsDataContext.Instance.UpdateCreditModificationBudget(creditModificationBudget, year);
        }

        public Response DeleteCreditModificationBudget(int budgetId, int creditModificationId, string type, int userId)
        {
            return CreditModificationBudgetsDataContext.Instance.DeleteCreditModificationBudget(budgetId, creditModificationId, type, userId);
        }

        #endregion
    }
}