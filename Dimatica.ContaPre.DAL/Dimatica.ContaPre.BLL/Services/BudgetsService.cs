namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Procedures;

    #endregion

    public class BudgetsService : IBudgetsService
    {
        #region IBudgetsService Members

        public List<Year> GetYearsByType(string type)
        {
            return BudgetsDataContext.Instance.GetYearsByType(type);
        }

        public List<Budget> GetBudgetsByType(int year, string type)
        {
            return BudgetsDataContext.Instance.GetBudgetsByType(year, type);
        }

        public List<Budget> GetIncomeLevelCompliance(int year)
        {
            return BudgetsDataContext.Instance.GetIncomeLevelCompliance(year);
        }

        public List<Budget> GetSpendLevelCompliance(int year)
        {
            return BudgetsDataContext.Instance.GetSpendLevelCompliance(year);
        }

        public List<Budget> GetBudgetsToNewByType(string type)
        {
            return BudgetsDataContext.Instance.GetBudgetsToNewByType(type);
        }

        public int GetProgramCodeByYear(int year)
        {
            return BudgetsDataContext.Instance.GetProgramCodeByYear(year);
        }

        public Response InsertMany(int year, string type, List<Budget> budgets, int userId)
        {
            return BudgetsDataContext.Instance.InsertMany(year, type, budgets, userId);
        }

        public Response UpdateMany(string type, List<Budget> budgets, int userId)
        {
            return BudgetsDataContext.Instance.UpdateMany(type, budgets, userId);
        }

        public Response AddMany(int chapterId, int year, string type, int userId)
        {
            return BudgetsDataContext.Instance.AddMany(chapterId, year, type, userId);
        }

        public Response DeleteMany(int chapterId, int year, string type, int userId)
        {
            return BudgetsDataContext.Instance.DeleteMany(chapterId, year, type, userId);
        }

        public Response CloseBudget(int year, string type, int userId)
        {
            return BudgetsDataContext.Instance.CloseBudget(year, type, userId);
        }

        public Response DeleteBudget(int year, string type, int userId)
        {
            return BudgetsDataContext.Instance.DeleteBudget(year, type, userId);
        }

        public Response CheckApplicationAmount(string cacsCode, int year, string type)
        {
            return BudgetsDataContext.Instance.CheckApplicationAmount(cacsCode, year, type);
        }

        public Response CheckChapterApplicationAmount(string cacsCode, int year, string type)
        {
            return BudgetsDataContext.Instance.CheckChapterApplicationAmount(cacsCode, year, type);
        }

        public List<Budget> GetSpendProvisionalStatus(int year, DateTime date, bool withOutPending)
        {
            return BudgetsDataContext.Instance.GetSpendProvisionalStatus(year, date, withOutPending);
        }

        public List<Budget> GetSpendComplianceGrade(int year, DateTime since, DateTime until)
        {
            return BudgetsDataContext.Instance.GetSpendComplianceGrade(year, since, until);
        }

        public List<Budget> GetIncomeProvisionalStatus(int year, DateTime date, bool withOutPending)
        {
            return BudgetsDataContext.Instance.GetIncomeProvisionalStatus(year, date, withOutPending);
        }

        #endregion
    }
}