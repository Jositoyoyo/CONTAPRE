namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface ITonnageSheetsService
    {
        #region Public Methods

        PRE_DETALLE_HOJA_ARQUEO GetDetailByFilters(int tonnageSheetCode, int? fileNumber, int? lineNumber);

        Response InsertTonnageSheet(PRE_HOJA_ARQUEO tonnageSheet);

        Response UpdateTonnageSheet(PRE_HOJA_ARQUEO tonnageSheet);

        Response InsertTonnageSheetDetail(PRE_DETALLE_HOJA_ARQUEO tonnageSheetDetail);

        Response UpdateTonnageSheetDetail(int tonnageSheetDetailCode, bool check, int userId);

        PRE_HOJA_ARQUEO GetById(int id);

        List<PRE_HOJA_ARQUEO> GetRestrictedAccountStatement(int? exerciseYear, int? budgetYear, int accountId, string sinceDate, string untilDate);

        int GetTonnageSheetCode(int exerciseYear, int number, bool isFifty);

        List<PRE_HOJA_ARQUEO> GetTonnageSheetsDetails(int exerciseYear, int number, bool isFifty);

        List<PRE_HOJA_ARQUEO> GetTonnageSheetsDetailsWithoutSheet(int exerciseYear, int number, bool isFifty);

        List<PRE_HOJA_ARQUEO> GetByFilters(int? exerciseYear, int? accountingCode, int? sheetSinceNumber, int? sheetUntilNumber, string sheetDateSince, string sheetDateUntil, string order, string sense);

        List<PRE_DETALLE_HOJA_ARQUEO> GetDetailsById(int tonnageSheetId);

        Response DeleteDetail(int detailId, int userId);

        Response DeleteTonnageSheet(int id, int userId);

        #endregion
    }
}