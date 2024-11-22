namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Procedures;

    #endregion

    public class BudgetApplicationsService : IBudgetApplicationsService
    {
        #region IBudgetApplicationsService Members

        public List<BudgetApplication> GetNumbersByTypeByYear(string type, int year)
        {
            return BudgetApplicationsDataContext.Instance.GetNumbersByTypeByYear(type, year);
        }

        public List<BudgetApplication> GetChaptersNumbersByTypeByYear(string type, int year)
        {
            return BudgetApplicationsDataContext.Instance.GetChaptersNumbersByTypeByYear(type, year);
        }

        public Response GetApplicationInfo(string cacsCode, int year, string type)
        {
            return BudgetApplicationsDataContext.Instance.GetApplicationInfo(cacsCode, year, type);
        }

        #endregion
    }
}