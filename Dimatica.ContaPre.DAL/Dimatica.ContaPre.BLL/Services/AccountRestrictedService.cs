namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class AccountRestrictedService : IAccountRestrictedService
    {
        #region IAccountRestrictedService Members

        public List<PRE_CUENTA_RESTRINGIDA> GetAccountsRestricted(string type)
        {
            return AccountRestrictedDataContext.Instance.GetAccountsRestricted(type);
        }

        public List<PRE_HOJA_ARQUEO> GetStatementAccounts(int? exerciseYear, int? budgetYear, int restrictedAccount, string sinceDate, string untilDate)
        {
            return AccountRestrictedDataContext.Instance.GetStatementAccounts(exerciseYear, budgetYear, restrictedAccount, sinceDate, untilDate);
        }

        public List<PRE_CUENTA_RESTRINGIDA> GetAccountsToCombo(string type)
        {
            return AccountRestrictedDataContext.Instance.GetAccountsToCombo(type);
        }

        public Response GetIdByDescription(string description, string type)
        {
            return AccountRestrictedDataContext.Instance.GetIdByDescription(description, type);
        }

        public Response InsertAccount(PRE_CUENTA_RESTRINGIDA account)
        {
            return AccountRestrictedDataContext.Instance.InsertAccount(account);
        }

        public Response UpdateAccount(PRE_CUENTA_RESTRINGIDA account)
        {
            return AccountRestrictedDataContext.Instance.UpdateAccount(account);
        }

        public Response DeleteAccount(int accountId)
        {
            return AccountRestrictedDataContext.Instance.DeleteAccount(accountId);
        }

        #endregion
    }
}