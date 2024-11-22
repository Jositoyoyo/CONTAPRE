namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface ICreditModificationBudgetsService
    {
        #region Public Methods

        List<PRE_MODIF_CREDITO_PRESUPUESTO> GetByCreditModification(int creditModificationId, string type);

        Response InsertCreditModificationBudget(PRE_MODIF_CREDITO_PRESUPUESTO creditModificationBudget, int year);

        Response UpdateCreditModificationBudget(PRE_MODIF_CREDITO_PRESUPUESTO creditModificationBudget, int year);

        Response DeleteCreditModificationBudget(int budgetId, int creditModificationId, string type, int userId);

        #endregion
    }
}