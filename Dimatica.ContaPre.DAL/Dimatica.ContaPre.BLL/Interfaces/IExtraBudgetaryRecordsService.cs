namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface IExtraBudgetaryRecordsService
    {
        #region Public Methods

        List<PRE_EXP_EXTRAPRE> GetByDocumentId(int accountingDocumentId, int type);

        List<PRE_EXP_EXTRAPRE> GetByFilters(int? year, int? extraBudgetaryType, int? extraBudgetaryApplication, bool? isBound, string sinceDate, string untilDate, int? fileNumberSince, int? fileNumberUntil, int? providerCode);

        List<PRE_EXP_EXTRAPRE> GetByBoundId(int extraBudgetaryId);

        Tuple<PRE_PARAMETROS, PRE_EXP_EXTRAPRE> GetMi(int extraBudgetaryId);

        Tuple<PRE_EXP_EXTRAPRE, PRE_PARAMETROS, List<PRE_EXP_EXTRAPRE>> GetPmp(int extraBudgetaryId);

        List<PRE_EXP_EXTRAPRE> GetDebitAndCredit(int extraBudgetaryId, DateTime since, DateTime until);

        PRE_EXP_EXTRAPRE GetById(int extraBudgetaryId);

        Response InsertExtraBudgetary(PRE_EXP_EXTRAPRE extraBudgetary);

        Response UpdateExtraBudgetaryAll(PRE_EXP_EXTRAPRE extraBudgetary);

        Response UpdateExtraBudgetary(PRE_EXP_EXTRAPRE extraBudgetary);

        Response DeleteExtraBudgetary(int extraBudgetaryId, int userId);

        Response DeleteExtraBudgetaryDiscount(int extraBudgetaryId);

        decimal GetSumAmount(int accountingId);

        decimal GetSumBoundAmount(int extraBudgetaryId);

        Response UpdateRepair(PRE_DOCUMENTO_CONTABLE accountingDocument);

        #endregion
    }
}