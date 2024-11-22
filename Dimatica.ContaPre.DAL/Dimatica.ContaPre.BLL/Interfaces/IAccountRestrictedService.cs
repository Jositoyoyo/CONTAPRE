namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface IAccountRestrictedService
    {
        #region Public Methods

        List<PRE_CUENTA_RESTRINGIDA> GetAccountsRestricted(string type);

        List<PRE_HOJA_ARQUEO> GetStatementAccounts(int? exerciseYear, int? budgetYear, int restrictedAccount, string sinceDate, string untilDate);

        List<PRE_CUENTA_RESTRINGIDA> GetAccountsToCombo(string type);

        Response GetIdByDescription(string description, string type);

        Response InsertAccount(PRE_CUENTA_RESTRINGIDA account);

        Response UpdateAccount(PRE_CUENTA_RESTRINGIDA account);

        Response DeleteAccount(int accountId);

        #endregion
    }
}