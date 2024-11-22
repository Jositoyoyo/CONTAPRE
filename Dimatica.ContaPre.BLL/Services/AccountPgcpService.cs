namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class AccountPgcpService : IAccountPgcpService
    {
        #region IAccountPgcpService Members

        public List<PRE_CUENTA_PGCP> GetAccountsToCombo()
        {
            return AccountPgcpDataContext.Instance.GetAccountsToCombo();
        }

        #endregion
    }
}