namespace Dimatica.ContaPre.DAL.DataContexts
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.DAL.Models;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class AccountPgcpDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<AccountPgcpDataContext> Context = new Lazy<AccountPgcpDataContext>(() => new AccountPgcpDataContext());

        #endregion

        #region Public Properties

        public static AccountPgcpDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<PRE_CUENTA_PGCP> GetAccountsToCombo()
        {
            try
            {
                var result = new List<PRE_CUENTA_PGCP>();

                var cmd = Db.GetStoredProcCommand("USP_AccountsPGCP_GetToCombo");

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var account = new PRE_CUENTA_PGCP
                                              {
                                                      CUEP_CODIGO = this.DbInteger(reader["CUEP_CODIGO"]),
                                                      CUEP_NUMERO = this.DbString(reader["CUEP_NUMERO"]),
                                                      CUEP_DESCRIPCION = this.DbString(reader["CUEP_DESCRIPCION"])
                                              };

                        result.Add(account);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}