namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Procedures;

    #endregion

    public interface IBudgetApplicationsService
    {
        #region Public Methods

        List<BudgetApplication> GetNumbersByTypeByYear(string type, int year);

        List<BudgetApplication> GetChaptersNumbersByTypeByYear(string type, int year);

        Response GetApplicationInfo(string cacsCode, int year, string type);

        #endregion
    }
}