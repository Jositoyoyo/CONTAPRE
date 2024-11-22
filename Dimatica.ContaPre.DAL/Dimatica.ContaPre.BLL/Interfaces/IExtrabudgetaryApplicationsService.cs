namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface IExtraBudgetaryApplicationsService
    {
        #region Public Methods

        List<PRE_EXTRAPRESUPUESTARIA> GetExtraBudgetaryApplications();

        List<PRE_EXTRAPRESUPUESTARIA> GetExtraBudgetaryApplicationsToCombo();

        List<PRE_TIPO_EXTRAP> GetExtraBudgetaryTypes();

        Response InsertExtraBudgetaryApplication(PRE_EXTRAPRESUPUESTARIA application);

        Response UpdateExtraBudgetaryApplication(PRE_EXTRAPRESUPUESTARIA application);

        Response DeleteExtraBudgetaryApplication(int applicationId);

        int GetNextNumber(int year);

        List<PRE_EXP_EXTRAPRE> GetStatus(int extraBudgetaryApplication, int year);

        PRE_EXP_EXTRAPRE GetInfo(int extraBudgetaryApplication);

        #endregion
    }
}