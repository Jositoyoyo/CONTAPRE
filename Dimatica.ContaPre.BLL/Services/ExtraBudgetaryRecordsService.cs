namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class ExtraBudgetaryRecordsService : IExtraBudgetaryRecordsService
    {
        #region IExtraBudgetaryRecordsService Members

        public List<PRE_EXP_EXTRAPRE> GetByDocumentId(int accountingDocumentId, int type)
        {
            return ExtraBudgetaryRecordsDataContext.Instance.GetByDocumentId(accountingDocumentId, type);
        }

        public List<PRE_EXP_EXTRAPRE> GetByFilters(int? year, int? extraBudgetaryType, int? extraBudgetaryApplication, bool? isBound, string sinceDate, string untilDate, int? fileNumberSince, int? fileNumberUntil, int? providerCode)
        {
            return ExtraBudgetaryRecordsDataContext.Instance.GetByFilters(year, extraBudgetaryType, extraBudgetaryApplication, isBound, sinceDate, untilDate, fileNumberSince, fileNumberUntil, providerCode);
        }

        public List<PRE_EXP_EXTRAPRE> GetByBoundId(int extraBudgetaryId)
        {
            return ExtraBudgetaryRecordsDataContext.Instance.GetByBoundId(extraBudgetaryId);
        }

        public Tuple<PRE_PARAMETROS, PRE_EXP_EXTRAPRE> GetMi(int extraBudgetaryId)
        {
            return ExtraBudgetaryRecordsDataContext.Instance.GetMi(extraBudgetaryId);
        }

        public Tuple<PRE_EXP_EXTRAPRE, PRE_PARAMETROS, List<PRE_EXP_EXTRAPRE>> GetPmp(int extraBudgetaryId)
        {
            return ExtraBudgetaryRecordsDataContext.Instance.GetPmp(extraBudgetaryId);
        }

        public List<PRE_EXP_EXTRAPRE> GetDebitAndCredit(int extraBudgetaryId, DateTime since, DateTime until)
        {
            return ExtraBudgetaryRecordsDataContext.Instance.GetDebitAndCredit(extraBudgetaryId, since, until);
        }

        public PRE_EXP_EXTRAPRE GetById(int extraBudgetaryId)
        {
            return ExtraBudgetaryRecordsDataContext.Instance.GetById(extraBudgetaryId);
        }

        public Response InsertExtraBudgetary(PRE_EXP_EXTRAPRE extraBudgetary)
        {
            return ExtraBudgetaryRecordsDataContext.Instance.InsertExtraBudgetary(extraBudgetary);
        }

        public Response UpdateExtraBudgetaryAll(PRE_EXP_EXTRAPRE extraBudgetary)
        {
            return ExtraBudgetaryRecordsDataContext.Instance.UpdateExtraBudgetaryAll(extraBudgetary);
        }

        public Response UpdateExtraBudgetary(PRE_EXP_EXTRAPRE extraBudgetary)
        {
            return ExtraBudgetaryRecordsDataContext.Instance.UpdateExtraBudgetary(extraBudgetary);
        }

        public Response DeleteExtraBudgetary(int extraBudgetaryId, int userId)
        {
            return ExtraBudgetaryRecordsDataContext.Instance.DeleteExtraBudgetary(extraBudgetaryId, userId);
        }

        public Response DeleteExtraBudgetaryDiscount(int extraBudgetaryId)
        {
            return ExtraBudgetaryRecordsDataContext.Instance.DeleteExtraBudgetaryDiscount(extraBudgetaryId);
        }

        public decimal GetSumAmount(int accountingId)
        {
            return ExtraBudgetaryRecordsDataContext.Instance.GetSumAmount(accountingId);
        }

        public decimal GetSumBoundAmount(int extraBudgetaryId)
        {
            return ExtraBudgetaryRecordsDataContext.Instance.GetSumBoundAmount(extraBudgetaryId);
        }

        public Response UpdateRepair(PRE_DOCUMENTO_CONTABLE accountingDocument)
        {
            return ExtraBudgetaryRecordsDataContext.Instance.UpdateRepair(accountingDocument);
        }

        #endregion
    }
}