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

    public class CreditModificationBudgetsDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<CreditModificationBudgetsDataContext> Context = new Lazy<CreditModificationBudgetsDataContext>(() => new CreditModificationBudgetsDataContext());

        #endregion

        #region Public Properties

        public static CreditModificationBudgetsDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<PRE_MODIF_CREDITO_PRESUPUESTO> GetByCreditModification(int creditModificationId, string type)
        {
            try
            {
                var result = new List<PRE_MODIF_CREDITO_PRESUPUESTO>();

                var cmd = Db.GetStoredProcCommand("USP_CreditModificationBudgets_GetByType");

                Db.AddInParameter(cmd, "@creditModificationId", DbType.Int32, creditModificationId);
                Db.AddInParameter(cmd, "@type", DbType.String, type);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var creditModification = new PRE_MODIF_CREDITO_PRESUPUESTO
                                                         {
                                                                 PRE_CODIGO = this.DbInteger(reader["PRE_CODIGO"]),
                                                                 MOD_CODIGO = this.DbInteger(reader["MOD_CODIGO"]),
                                                                 MODP_IMPORTE = this.DbDecimal(reader["MODP_IMPORTE"]),
                                                                 MODP_POSITIVO = this.DbBooleanBit(reader["MODP_POSITIVO"]),
                                                                 MODP_I_G = this.DbString(reader["MODP_I_G"]),
                                                                 CACS_NUMERO = this.DbString(reader["CACS_NUMERO"]),
                                                                 CACS_CODIGO = this.DbString(reader["CACS_CODIGO"])
                                                         };

                        result.Add(creditModification);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response InsertCreditModificationBudget(PRE_MODIF_CREDITO_PRESUPUESTO creditModificationBudget, int year)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_CreditModificationBudgets_Insert");

                Db.AddInParameter(cmd, "@cacs_code", DbType.String, creditModificationBudget.CACS_CODIGO);
                Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                Db.AddInParameter(cmd, "@type", DbType.String, creditModificationBudget.MODP_I_G);
                Db.AddInParameter(cmd, "@creditModificationId", DbType.Int32, creditModificationBudget.MOD_CODIGO);
                Db.AddInParameter(cmd, "@amount", DbType.Decimal, creditModificationBudget.MODP_IMPORTE);
                Db.AddInParameter(cmd, "@isPositive", DbType.Boolean, creditModificationBudget.MODP_POSITIVO);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, creditModificationBudget.USU_CODIGO);

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
                            case 0:
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

        public Response UpdateCreditModificationBudget(PRE_MODIF_CREDITO_PRESUPUESTO creditModificationBudget, int year)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_CreditModificationBudgets_Update");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                Db.AddInParameter(cmd, "@type", DbType.String, creditModificationBudget.MODP_I_G);
                Db.AddInParameter(cmd, "@budgetId", DbType.Int32, creditModificationBudget.PRE_CODIGO);
                Db.AddInParameter(cmd, "@creditModificationId", DbType.Int32, creditModificationBudget.MOD_CODIGO);
                Db.AddInParameter(cmd, "@amount", DbType.Decimal, creditModificationBudget.MODP_IMPORTE);
                Db.AddInParameter(cmd, "@isPositive", DbType.Boolean, creditModificationBudget.MODP_POSITIVO);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, creditModificationBudget.USU_CODIGO);

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
                            case 0:
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

        public Response DeleteCreditModificationBudget(int budgetId, int creditModificationId, string type, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_CreditModificationBudgets_Delete");

                Db.AddInParameter(cmd, "@budgetId", DbType.Int32, budgetId);
                Db.AddInParameter(cmd, "@creditModificationId", DbType.Int32, creditModificationId);
                Db.AddInParameter(cmd, "@type", DbType.String, type);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, userId);

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

        #endregion
    }
}