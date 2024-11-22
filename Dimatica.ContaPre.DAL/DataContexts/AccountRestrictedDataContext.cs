namespace Dimatica.ContaPre.DAL.DataContexts
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Data;

    using Dimatica.ContaPre.DAL.Models;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class AccountRestrictedDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<AccountRestrictedDataContext> Context = new Lazy<AccountRestrictedDataContext>(() => new AccountRestrictedDataContext());

        #endregion

        #region Public Properties

        public static AccountRestrictedDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<PRE_CUENTA_RESTRINGIDA> GetAccountsRestricted(string type)
        {
            try
            {
                var result = new List<PRE_CUENTA_RESTRINGIDA>();

                var cmd = Db.GetStoredProcCommand("USP_AccountsRestricted_GetByType");

                if (!string.IsNullOrWhiteSpace(type))
                {
                    Db.AddInParameter(cmd, "@type", DbType.String, type);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var application = new PRE_CUENTA_RESTRINGIDA
                        {
                            CUE_CODIGO = this.DbInteger(reader["CUE_CODIGO"]),
                            CUE_ORDINAL_PERCEPTOR = this.DbIntegerNullable(reader["CUE_ORDINAL_PERCEPTOR"]),
                            CUE_DESCRIPCION = this.DbString(reader["CUE_DESCRIPCION"]),
                            CUE_ENTIDAD = this.DbString(reader["CUE_ENTIDAD"]),
                            CUE_CC = this.DbString(reader["CUE_CC"]),
                            CUE_DIRECCION = this.DbString(reader["CUE_DIRECCION"]),
                            CEN_CODIGO = this.DbIntegerNullable(reader["CEN_CODIGO"]),
                            CEN_DESCRIPCION = this.DbString(reader["CEN_DESCRIPCION"]),
                        };

                        result.Add(application);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_HOJA_ARQUEO> GetStatementAccounts(int? exerciseYear, int? budgetYear, int restrictedAccount, string sinceDate, string untilDate)
        {
            try
            {
                var result = new List<PRE_HOJA_ARQUEO>();

                var cmd = Db.GetStoredProcCommand("USP_AccountsRestricted_GetStatementAccounts");

                if (exerciseYear != null)
                {
                    Db.AddInParameter(cmd, "@exerciseYear", DbType.Int32, exerciseYear);
                }

                if (budgetYear != null)
                {
                    Db.AddInParameter(cmd, "@budgetYear", DbType.Int32, budgetYear);
                }

                Db.AddInParameter(cmd, "@restrictedAccount", DbType.Int32, restrictedAccount);

                if (!string.IsNullOrWhiteSpace(sinceDate))
                {
                    Db.AddInParameter(cmd, "@since", DbType.String, sinceDate);
                }

                if (!string.IsNullOrWhiteSpace(untilDate))
                {
                    Db.AddInParameter(cmd, "@until", DbType.String, untilDate);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var application = new PRE_HOJA_ARQUEO
                        {
                            DET_CODIGO = this.DbIntegerNullable(reader["DET_CODIGO"]),
                            DET_FECHA_APUNTE = this.DbDateNullable(reader["DET_FECHA_APUNTE"]),
                            DET_NUMERO_EXPEDIENTE = this.DbIntegerNullable(reader["DET_NUMERO_EXPEDIENTE"]),
                            DOC_DESCRIPCION = this.DbString(reader["DESCRIPCION"]),
                            DET_IMPORTE = this.DbDecimal(reader["DET_IMPORTE"]),
                            HOJ_NUMERO = this.DbIntegerNullable(reader["HOJ_NUMERO"]),
                            HOJ_FECHA = this.DbDateNullable(reader["HOJ_FECHA"]),
                            HOJ_ARQUEO50 = this.DbBooleanBit(reader["HOJ_ARQUEO50"]),
                            HOJ_ANO = this.DbShortNullable(reader["ANO_HOJA"])
                        };

                        result.Add(application);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_CUENTA_RESTRINGIDA> GetAccountsToCombo(string type)
        {
            try
            {
                var result = new List<PRE_CUENTA_RESTRINGIDA>();

                var cmd = Db.GetStoredProcCommand("USP_AccountsRestricted_GetToCombo");

                Db.AddInParameter(cmd, "@type", DbType.String, type);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var application = new PRE_CUENTA_RESTRINGIDA
                        {
                            CUE_CODIGO = this.DbInteger(reader["CUE_CODIGO"]),
                            CUE_ORDINAL_PERCEPTOR = this.DbIntegerNullable(reader["CUE_ORDINAL_PERCEPTOR"]),
                            CUE_ENTIDAD = this.DbString(reader["CUE_ENTIDAD"]),
                        };

                        result.Add(application);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response GetIdByDescription(string description, string type)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AccountsRestricted_GetCodeByDesc");

                Db.AddInParameter(cmd, "@description", DbType.String, description);
                Db.AddInParameter(cmd, "@type", DbType.String, type);

                var code = ResponseCode.Invalid;
                var id = 0;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        if (result == 0)
                        {
                            code = ResponseCode.NotFound;
                        }
                        else
                        {
                            code = ResponseCode.Ok;
                            id = result;
                        }
                    }
                }

                return new Response
                {
                    ResponseCode = code,
                    ResponseMethod = id
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response InsertAccount(PRE_CUENTA_RESTRINGIDA account)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AccountsRestricted_Insert");

                Db.AddInParameter(cmd, "@type", DbType.String, account.CUE_I_G);
                Db.AddInParameter(cmd, "@order", DbType.Int32, account.CUE_ORDINAL_PERCEPTOR);
                Db.AddInParameter(cmd, "@description", DbType.String, account.CUE_DESCRIPCION);
                Db.AddInParameter(cmd, "@entity", DbType.String, account.CUE_ENTIDAD);
                Db.AddInParameter(cmd, "@account", DbType.String, account.CUE_CC);
                Db.AddInParameter(cmd, "@address", DbType.String, account.CUE_DIRECCION);
                Db.AddInParameter(cmd, "@placeId", DbType.Int32, account.CEN_CODIGO);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, account.USU_CODIGO);

                var code = ResponseCode.Invalid;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        switch (result)
                        {
                            case 1:
                                code = ResponseCode.Ok;

                                break;
                        }
                    }
                }

                return new Response
                {
                    ResponseCode = code
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response UpdateAccount(PRE_CUENTA_RESTRINGIDA account)
        {
            try
            {
                if (account?.CUE_CODIGO == null)
                {
                    return new Response
                    {
                        ResponseCode = ResponseCode.Invalid
                    };
                }

                var cmd = Db.GetStoredProcCommand("USP_AccountsRestricted_Update");

                Db.AddInParameter(cmd, "@id", DbType.Int32, account.CUE_CODIGO);
                Db.AddInParameter(cmd, "@type", DbType.String, account.CUE_I_G);
                Db.AddInParameter(cmd, "@order", DbType.Int32, account.CUE_ORDINAL_PERCEPTOR);
                Db.AddInParameter(cmd, "@description", DbType.String, account.CUE_DESCRIPCION);
                Db.AddInParameter(cmd, "@entity", DbType.String, account.CUE_ENTIDAD);
                Db.AddInParameter(cmd, "@account", DbType.String, account.CUE_CC);
                Db.AddInParameter(cmd, "@address", DbType.String, account.CUE_DIRECCION);
                Db.AddInParameter(cmd, "@placeId", DbType.Int32, account.CEN_CODIGO);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, account.USU_CODIGO);

                var code = ResponseCode.Invalid;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        switch (result)
                        {
                            case 1:
                                code = ResponseCode.Ok;

                                break;
                            case 2:
                                code = ResponseCode.NotFound;

                                break;
                        }
                    }
                }

                return new Response
                {
                    ResponseCode = code
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response DeleteAccount(int accountId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AccountsRestricted_Delete");

                Db.AddInParameter(cmd, "@id", DbType.Int32, accountId);

                var code = ResponseCode.Invalid;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        switch (result)
                        {
                            case 1:
                                code = ResponseCode.Ok;

                                break;
                            case 2:
                                code = ResponseCode.NotFound;

                                break;
                            case 3:
                                code = ResponseCode.Found;

                                break;
                        }
                    }
                }

                return new Response
                {
                    ResponseCode = code
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}