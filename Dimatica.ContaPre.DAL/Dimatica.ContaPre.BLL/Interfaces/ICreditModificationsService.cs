namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.OL.Procedures;

    #endregion

    public interface ICreditModificationsService
    {
        #region Public Methods

        List<Year> GetYears();

        List<PRE_MODIFICACION_CREDITO> GetByFilters(int year, int? since, int? until);

        PRE_MODIFICACION_CREDITO GetById(int creditModificationId);

        int GetNextOrderByYear(int year);

        Response InsertCreditModification(PRE_MODIFICACION_CREDITO creditModification);

        Response UpdateCreditModification(PRE_MODIFICACION_CREDITO creditModification);

        Response GenerateRecordsCreditModification(PRE_MODIFICACION_CREDITO creditModification);

        Response ExecuteCreditModification(int creditModificationId, int userId);

        #endregion
    }
}