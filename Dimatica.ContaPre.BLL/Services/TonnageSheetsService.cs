namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class TonnageSheetsService : ITonnageSheetsService
    {
        #region ITonnageSheetsService Members

        public PRE_DETALLE_HOJA_ARQUEO GetDetailByFilters(int tonnageSheetCode, int? fileNumber, int? lineNumber)
        {
            return TonnageSheetsDataContext.Instance.GetDetailByFilters(tonnageSheetCode, fileNumber, lineNumber);
        }

        public Response InsertTonnageSheet(PRE_HOJA_ARQUEO tonnageSheet)
        {
            return TonnageSheetsDataContext.Instance.InsertTonnageSheet(tonnageSheet);
        }

        public Response UpdateTonnageSheet(PRE_HOJA_ARQUEO tonnageSheet)
        {
            return TonnageSheetsDataContext.Instance.UpdateTonnageSheet(tonnageSheet);
        }

        public Response InsertTonnageSheetDetail(PRE_DETALLE_HOJA_ARQUEO tonnageSheetDetail)
        {
            return TonnageSheetsDataContext.Instance.InsertTonnageSheetDetail(tonnageSheetDetail);
        }

        public Response UpdateTonnageSheetDetail(int tonnageSheetDetailCode, bool check, int userId)
        {
            return TonnageSheetsDataContext.Instance.UpdateTonnageSheetDetail(tonnageSheetDetailCode, check, userId);
        }

        public PRE_HOJA_ARQUEO GetById(int id)
        {
            return TonnageSheetsDataContext.Instance.GetById(id);
        }

        public List<PRE_HOJA_ARQUEO> GetRestrictedAccountStatement(int? exerciseYear, int? budgetYear, int accountId, string sinceDate, string untilDate)
        {
            return TonnageSheetsDataContext.Instance.GetRestrictedAccountStatement(exerciseYear, budgetYear, accountId, sinceDate, untilDate);
        }

        public int GetTonnageSheetCode(int exerciseYear, int number, bool isFifty)
        {
            return TonnageSheetsDataContext.Instance.GetTonnageSheetCode(exerciseYear, number, isFifty);
        }

        public List<PRE_HOJA_ARQUEO> GetTonnageSheetsDetails(int exerciseYear, int number, bool isFifty)
        {
            return TonnageSheetsDataContext.Instance.GetTonnageSheetsDetails(exerciseYear, number, isFifty);
        }

        public List<PRE_HOJA_ARQUEO> GetTonnageSheetsDetailsWithoutSheet(int exerciseYear, int number, bool isFifty)
        {
            return TonnageSheetsDataContext.Instance.GetTonnageSheetsDetailsWithoutSheet(exerciseYear, number, isFifty);
        }

        public List<PRE_HOJA_ARQUEO> GetByFilters(int? exerciseYear, int? accountingCode, int? sheetSinceNumber, int? sheetUntilNumber, string sheetDateSince, string sheetDateUntil, string order, string sense)
        {
            return TonnageSheetsDataContext.Instance.GetByFilters(exerciseYear, accountingCode, sheetSinceNumber, sheetUntilNumber, sheetDateSince, sheetDateUntil, order, sense);
        }

        public List<PRE_DETALLE_HOJA_ARQUEO> GetDetailsById(int tonnageSheetId)
        {
            return TonnageSheetsDataContext.Instance.GetDetailsById(tonnageSheetId);
        }

        public Response DeleteDetail(int detailId, int userId)
        {
            return TonnageSheetsDataContext.Instance.DeleteDetail(detailId, userId);
        }

        public Response DeleteTonnageSheet(int id, int userId)
        {
            return TonnageSheetsDataContext.Instance.DeleteTonnageSheet(id, userId);
        }

        #endregion
    }
}