namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Procedures;

    #endregion

    public interface IBudgetsService
    {
        #region Public Methods

        List<Year> GetYearsByType(string type);

        List<Budget> GetBudgetsByType(int year, string type);

        List<Budget> GetIncomeLevelCompliance(int year);

        List<Budget> GetSpendLevelCompliance(int year);

        List<Budget> GetBudgetsToNewByType(string type);

        int GetProgramCodeByYear(int year);

        Response InsertMany(int year, string type, List<Budget> budgets, int userId);

        Response UpdateMany(string type, List<Budget> budgets, int userId);

        Response AddMany(int chapterId, int year, string type, int userId);

        Response DeleteMany(int chapterId, int year, string type, int userId);

        Response CloseBudget(int year, string type, int userId);

        Response DeleteBudget(int year, string type, int userId);

        Response CheckApplicationAmount(string cacsCode, int year, string type);

        Response CheckChapterApplicationAmount(string cacsCode, int year, string type);

        List<Budget> GetSpendProvisionalStatus(int year, DateTime date, bool withOutPending);

        List<Budget> GetSpendComplianceGrade(int year, DateTime since, DateTime until);

        List<Budget> GetIncomeProvisionalStatus(int year, DateTime date, bool withOutPending);

        #endregion
    }
}