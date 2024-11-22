namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class ExtraBudgetaryApplicationsService : IExtraBudgetaryApplicationsService
    {
        #region IExtraBudgetaryApplicationsService Members

        public List<PRE_EXTRAPRESUPUESTARIA> GetExtraBudgetaryApplications()
        {
            return ExtraBudgetaryApplicationsDataContext.Instance.GetExtraBudgetaryApplications();
        }

        public List<PRE_EXTRAPRESUPUESTARIA> GetExtraBudgetaryApplicationsToCombo()
        {
            return ExtraBudgetaryApplicationsDataContext.Instance.GetExtraBudgetaryApplicationsToCombo();
        }

        public List<PRE_TIPO_EXTRAP> GetExtraBudgetaryTypes()
        {
            return ExtraBudgetaryApplicationsDataContext.Instance.GetExtraBudgetaryTypes();
        }

        public Response InsertExtraBudgetaryApplication(PRE_EXTRAPRESUPUESTARIA application)
        {
            return ExtraBudgetaryApplicationsDataContext.Instance.InsertExtraBudgetaryApplication(application);
        }

        public Response UpdateExtraBudgetaryApplication(PRE_EXTRAPRESUPUESTARIA application)
        {
            return ExtraBudgetaryApplicationsDataContext.Instance.UpdateExtraBudgetaryApplication(application);
        }

        public Response DeleteExtraBudgetaryApplication(int applicationId)
        {
            return ExtraBudgetaryApplicationsDataContext.Instance.DeleteExtraBudgetaryApplication(applicationId);
        }

        public int GetNextNumber(int year)
        {
            return ExtraBudgetaryApplicationsDataContext.Instance.GetNextNumber(year);
        }

        public List<PRE_EXP_EXTRAPRE> GetStatus(int extraBudgetaryApplication, int year)
        {
            return ExtraBudgetaryApplicationsDataContext.Instance.GetStatus(extraBudgetaryApplication, year);
        }

        public PRE_EXP_EXTRAPRE GetInfo(int extraBudgetaryApplication)
        {
            return ExtraBudgetaryApplicationsDataContext.Instance.GetInfo(extraBudgetaryApplication);
        }

        #endregion
    }
}