namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface IAccountPgcpService
    {
        #region Public Methods

        List<PRE_CUENTA_PGCP> GetAccountsToCombo();

        #endregion
    }
}