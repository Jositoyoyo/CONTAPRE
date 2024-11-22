namespace Dimatica.ContaPre.DAL.DataContexts
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    using Dimatica.ContaPre.DAL.Models;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Procedures;

    #endregion

    public class BudgetsDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<BudgetsDataContext> Context = new Lazy<BudgetsDataContext>(() => new BudgetsDataContext());

        #endregion

        #region Public Properties

        public static BudgetsDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<Year> GetYearsByType(string type)
        {
            try
            {
                var result = new List<Year>();

                var cmd = Db.GetStoredProcCommand("USP_Budgets_GetYearsByType");

                Db.AddInParameter(cmd, "@type", DbType.String, type);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var year = new Year
                                           {
                                                   Value = this.DbString(reader["PRE_ANO"])
                                           };

                        result.Add(year);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Budget> GetBudgetsByType(int year, string type)
        {
            try
            {
                var result = new List<Budget>();

                var cmd = Db.GetStoredProcCommand("USP_Budgets_GetByType");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                Db.AddInParameter(cmd, "@type", DbType.String, type);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var budget = new Budget
                                             {
                                                     BudgetId = this.DbInteger(reader["PRE_CODIGO"]),
                                                     ChapterId = this.DbIntegerNullable(reader["CAP_CODIGO"]),
                                                     Chapter = this.DbString(reader["CAP_NUMERO"]),
                                                     ArticleId = this.DbIntegerNullable(reader["ART_CODIGO"]),
                                                     Article = this.DbString(reader["ART_NUMERO"]),
                                                     ConceptId = this.DbIntegerNullable(reader["CON_CODIGO"]),
                                                     Concept = this.DbString(reader["CON_NUMERO"]),
                                                     SubConceptId = this.DbIntegerNullable(reader["SUB_CODIGO"]),
                                                     SubConcept = this.DbString(reader["SUB_NUMERO"]),
                                                     Description = this.DbString(reader["DESCRIPCION"]),
                                                     Level = this.DbString(reader["APP_LEVEL"]),
                                                     NotBinding = this.DbBooleanBit(reader["PRE_NO_VINCULANTE"]),
                                                     Amount = this.DbDecimal(reader["PRE_IMPORTE"]),
                                                     UpdateAmount = this.DbDecimal(reader["PRE_IMPORTE_MODIFICACIONES"]),
                                                     FinalAmount = this.DbDecimal(reader["PRE_IMPORTE_DEFINITIVO"]),
                                                     ProgramId = this.DbIntegerNullable(reader["PRO_CODIGO"]),
                                                     Program = this.DbString(reader["PRO_NUMERO"]),
                                                     IsClose = this.DbBooleanBit(reader["PRE_CERRADO"]),
                                                     Type = this.DbString(reader["PRE_I_G"])
                                             };

                        result.Add(budget);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Budget> GetIncomeLevelCompliance(int year)
        {
            try
            {
                var result = new List<Budget>();

                var cmd = Db.GetStoredProcCommand("USP_Budgets_IncomeLevelCompliance");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var budget = new Budget
                                             {
                                                     ChapterId = this.DbIntegerNullable(reader["CAPITULO"]),
                                                     ArticleId = this.DbIntegerNullable(reader["ARTICULO"]),
                                                     ConceptId = this.DbIntegerNullable(reader["CONCEPTO"]),
                                                     SubConceptId = this.DbIntegerNullable(reader["SUBCONCEPTO"]),
                                                     ApplicationNumber = this.DbString(reader["APLICACION"]),
                                                     Description = this.DbString(reader["NOMBRE_APLICACION"]),
                                                     Amount = this.DbDecimal(reader["IMPORTE_DEFINITIVO"]),
                                                     DrAmount = this.DbDecimal(reader["SUMA_DR"]),
                                                     MiAmount = this.DbDecimal(reader["SUMA_MI"]),
                                                     Year = year
                                             };

                        budget.Pending = budget.UpdateAmount - budget.FinalAmount;

                        if (budget.ArticleId != null && budget.ConceptId != null && budget.SubConceptId != null)
                        {
                            budget.Level = "SUB";
                        }
                        else
                        {
                            if (budget.ArticleId != null && budget.ConceptId != null)
                            {
                                budget.Level = "CON";
                            }
                            else
                            {
                                budget.Level = budget.ArticleId != null ? "ART" : "CAP";
                            }
                        }

                        result.Add(budget);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Budget> GetSpendLevelCompliance(int year)
        {
            try
            {
                var result = new List<Budget>();

                var cmd = Db.GetStoredProcCommand("USP_Budgets_SpendLevelCompliance");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var budget = new Budget
                                             {
                                                     ChapterId = this.DbIntegerNullable(reader["CAPITULO"]),
                                                     ArticleId = this.DbIntegerNullable(reader["ARTICULO"]),
                                                     ConceptId = this.DbIntegerNullable(reader["CONCEPTO"]),
                                                     SubConceptId = this.DbIntegerNullable(reader["SUBCONCEPTO"]),
                                                     ApplicationNumber = this.DbString(reader["APLICACION"]),
                                                     Description = this.DbString(reader["NOMBRE_APLICACION"]),
                                                     Amount = this.DbDecimal(reader["IMPORTE_EJECUTIVO"]),
                                                     RcAmount = this.DbDecimal(reader["SUMA_RC"]),
                                                     AdAmount = this.DbDecimal(reader["SUMA_AD"]),
                                                     OAmount = this.DbDecimal(reader["SUMA_O"]),
                                                     PAmount = this.DbDecimal(reader["SUMA_P"]),
                                                     Year = year
                                             };

                        budget.Pending = budget.UpdateAmount - budget.FinalAmount;

                        if (budget.ArticleId != null && budget.ConceptId != null && budget.SubConceptId != null)
                        {
                            budget.Level = "SUB";
                        }
                        else
                        {
                            if (budget.ArticleId != null && budget.ConceptId != null)
                            {
                                budget.Level = "CON";
                            }
                            else
                            {
                                budget.Level = budget.ArticleId != null ? "ART" : "CAP";
                            }
                        }

                        result.Add(budget);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Budget> GetBudgetsToNewByType(string type)
        {
            try
            {
                var result = new List<Budget>();

                var cmd = Db.GetStoredProcCommand("USP_Budgets_GetToNewByType");

                Db.AddInParameter(cmd, "@type", DbType.String, type);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var budget = new Budget
                                             {
                                                     ChapterId = this.DbIntegerNullable(reader["CAP_CODIGO"]),
                                                     Chapter = this.DbString(reader["CAP_NUMERO"]),
                                                     ArticleId = this.DbIntegerNullable(reader["ART_CODIGO"]),
                                                     Article = this.DbString(reader["ART_NUMERO"]),
                                                     ConceptId = this.DbIntegerNullable(reader["CON_CODIGO"]),
                                                     Concept = this.DbString(reader["CON_NUMERO"]),
                                                     SubConceptId = this.DbIntegerNullable(reader["SUB_CODIGO"]),
                                                     SubConcept = this.DbString(reader["SUB_NUMERO"]),
                                                     Description = this.DbString(reader["DESCRIPCION"]),
                                                     Level = this.DbString(reader["NIVEL"]),
                                                     NotBinding = this.DbBooleanBit(reader["NO_VINCULANTE"]),
                                                     Amount = 0,
                                                     ProgramId = this.DbIntegerNullable(reader["PRO_CODIGO"])
                                             };

                        result.Add(budget);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetProgramCodeByYear(int year)
        {
            try
            {
                var result = 0;

                var cmd = Db.GetStoredProcCommand("USP_Budgets_GetProgramCode");

                Db.AddInParameter(cmd, "@year", DbType.String, year);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = this.DbInteger(reader["Result"]);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response InsertMany(int year, string type, List<Budget> budgets, int userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(type) || budgets.Any() == false)
                {
                    return new Response
                                   {
                                           ResponseCode = ResponseCode.Invalid
                                   };
                }

                var cmd = Db.GetStoredProcCommand("USP_Budgets_InsertMany");

                Db.AddInParameter(cmd, "@type", DbType.String, type);
                Db.AddInParameter(cmd, "@year", DbType.Int32, year);

                var sourceBudgets = new DataTable();
                sourceBudgets.Columns.Add("BudgetId", typeof(int));
                sourceBudgets.Columns.Add("ChapterId", typeof(int));
                sourceBudgets.Columns.Add("ArticleId", typeof(int));
                sourceBudgets.Columns.Add("ConceptId", typeof(int));
                sourceBudgets.Columns.Add("SubConceptId", typeof(int));
                sourceBudgets.Columns.Add("Year", typeof(int));
                sourceBudgets.Columns.Add("Type", typeof(string));
                sourceBudgets.Columns.Add("NotBinding", typeof(bool));
                sourceBudgets.Columns.Add("IsClose", typeof(bool));
                sourceBudgets.Columns.Add("Amount", typeof(decimal));
                sourceBudgets.Columns.Add("UpdateAmount", typeof(decimal));
                sourceBudgets.Columns.Add("ProgramId", typeof(int));
                sourceBudgets.Columns.Add("CoinId", typeof(int));
                sourceBudgets.Columns.Add("Hidden", typeof(bool));
                sourceBudgets.Columns.Add("Date", typeof(DateTime));
                sourceBudgets.Columns.Add("UserId", typeof(int));

                foreach (var budget in budgets)
                {
                    var dr = sourceBudgets.NewRow();
                    dr[0] = 0;

                    if (budget.ChapterId != null)
                    {
                        dr[1] = budget.ChapterId;
                    }

                    if (budget.ArticleId != null)
                    {
                        dr[2] = budget.ArticleId;
                    }

                    if (budget.ConceptId != null)
                    {
                        dr[3] = budget.ConceptId;
                    }

                    if (budget.SubConceptId != null)
                    {
                        dr[4] = budget.SubConceptId;
                    }

                    dr[5] = year;
                    dr[6] = type;
                    dr[7] = budget.NotBinding;
                    dr[8] = false;
                    dr[9] = budget.Amount;
                    dr[10] = 0;

                    if (budget.ProgramId != null)
                    {
                        dr[11] = budget.ProgramId;
                    }

                    dr[12] = 1;
                    dr[13] = false;
                    dr[14] = DateTime.Now;
                    dr[15] = userId;

                    sourceBudgets.Rows.Add(dr);
                }

                cmd.Parameters.Add(
                                   new SqlParameter
                                           {
                                                   ParameterName = "@budgets",
                                                   Value = sourceBudgets,
                                                   SqlDbType = SqlDbType.Structured
                                           });

                var code = ResponseCode.Invalid;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        switch (result)
                        {
                            case 0:
                                code = ResponseCode.Found;

                                break;
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

        public Response UpdateMany(string type, List<Budget> budgets, int userId)
        {
            try
            {
                if (budgets.Any() == false)
                {
                    return new Response
                                   {
                                           ResponseCode = ResponseCode.Invalid
                                   };
                }

                var cmd = Db.GetStoredProcCommand("USP_Budgets_UpdateMany");

                var sourceBudgets = new DataTable();
                sourceBudgets.Columns.Add("BudgetId", typeof(int));
                sourceBudgets.Columns.Add("ChapterId", typeof(int));
                sourceBudgets.Columns.Add("ArticleId", typeof(int));
                sourceBudgets.Columns.Add("ConceptId", typeof(int));
                sourceBudgets.Columns.Add("SubConceptId", typeof(int));
                sourceBudgets.Columns.Add("Year", typeof(int));
                sourceBudgets.Columns.Add("Type", typeof(string));
                sourceBudgets.Columns.Add("NotBinding", typeof(bool));
                sourceBudgets.Columns.Add("IsClose", typeof(bool));
                sourceBudgets.Columns.Add("Amount", typeof(decimal));
                sourceBudgets.Columns.Add("UpdateAmount", typeof(decimal));
                sourceBudgets.Columns.Add("ProgramId", typeof(int));
                sourceBudgets.Columns.Add("CoinId", typeof(int));
                sourceBudgets.Columns.Add("Hidden", typeof(bool));
                sourceBudgets.Columns.Add("Date", typeof(DateTime));
                sourceBudgets.Columns.Add("UserId", typeof(int));

                foreach (var budget in budgets)
                {
                    var dr = sourceBudgets.NewRow();
                    dr[0] = budget.BudgetId;
                    dr[6] = type;
                    dr[7] = budget.NotBinding;
                    dr[10] = budget.UpdateAmount;

                    if (budget.ProgramId != null)
                    {
                        dr[11] = budget.ProgramId;
                    }

                    dr[12] = 1;
                    dr[15] = userId;

                    sourceBudgets.Rows.Add(dr);
                }

                cmd.Parameters.Add(
                                   new SqlParameter
                                           {
                                                   ParameterName = "@budgets",
                                                   Value = sourceBudgets,
                                                   SqlDbType = SqlDbType.Structured
                                           });

                var code = ResponseCode.Invalid;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        switch (result)
                        {
                            case 2:
                                code = ResponseCode.NotFound;

                                break;
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

        public Response AddMany(int chapterId, int year, string type, int userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(type))
                {
                    return new Response
                                   {
                                           ResponseCode = ResponseCode.Invalid
                                   };
                }

                var cmd = Db.GetStoredProcCommand("USP_Budgets_AddMany");

                Db.AddInParameter(cmd, "@type", DbType.String, type);
                Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                Db.AddInParameter(cmd, "@cap_code", DbType.Int32, chapterId);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, userId);

                var code = ResponseCode.Invalid;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        switch (result)
                        {
                            case 0:
                                code = ResponseCode.Found;

                                break;
                            case 1:
                                code = ResponseCode.Ok;

                                break;
                            case 2:
                                code = ResponseCode.NotFound;

                                break;
                            case 3:
                                code = ResponseCode.IsClose;

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

        public Response DeleteMany(int chapterId, int year, string type, int userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(type))
                {
                    return new Response
                                   {
                                           ResponseCode = ResponseCode.Invalid
                                   };
                }

                var cmd = Db.GetStoredProcCommand("USP_Budgets_DeleteMany");

                Db.AddInParameter(cmd, "@type", DbType.String, type);
                Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                Db.AddInParameter(cmd, "@cap_code", DbType.Int32, chapterId);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, userId);

                var code = ResponseCode.Invalid;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        switch (result)
                        {
                            case 0:
                                code = ResponseCode.NotFoundChild;

                                break;
                            case 1:
                                code = ResponseCode.Ok;

                                break;
                            case 2:
                                code = ResponseCode.NotFound;

                                break;
                            case 3:
                                code = ResponseCode.IsClose;

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

        public Response CloseBudget(int year, string type, int userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(type))
                {
                    return new Response
                                   {
                                           ResponseCode = ResponseCode.Invalid
                                   };
                }

                var cmd = Db.GetStoredProcCommand("USP_Budgets_Close");

                Db.AddInParameter(cmd, "@type", DbType.String, type);
                Db.AddInParameter(cmd, "@year", DbType.Int32, year);
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

        public Response DeleteBudget(int year, string type, int userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(type))
                {
                    return new Response
                                   {
                                           ResponseCode = ResponseCode.Invalid
                                   };
                }

                var cmd = Db.GetStoredProcCommand("USP_Budgets_Delete");

                Db.AddInParameter(cmd, "@type", DbType.String, type);
                Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, userId);

                var code = ResponseCode.Invalid;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        switch (result)
                        {
                            case 0:
                                code = ResponseCode.Found;

                                break;
                            case 1:
                                code = ResponseCode.Ok;

                                break;
                            case 2:
                                code = ResponseCode.NotFound;

                                break;
                            case 3:
                                code = ResponseCode.IsClose;

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

        public Response CheckApplicationAmount(string cacsCode, int year, string type)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Budgets_CheckApplicationAmount");

                Db.AddInParameter(cmd, "@cacsCode", DbType.String, cacsCode);
                Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                Db.AddInParameter(cmd, "@type", DbType.String, type);

                var code = ResponseCode.Invalid;
                var result = new Tuple<decimal, decimal>(0, 0);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var budget = this.DbDecimal(reader["SALDO_PRESUPUESTADO"]);
                        var credit = this.DbDecimal(reader["SUMA_CREDITO_RETENIDO"]);

                        code = ResponseCode.Ok;
                        result = new Tuple<decimal, decimal>(budget, credit);
                    }
                }

                return new Response
                               {
                                       ResponseCode = code,
                                       ResponseMethod = result
                               };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response CheckChapterApplicationAmount(string cacsCode, int year, string type)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Budgets_CheckChapterApplicationAmount");

                Db.AddInParameter(cmd, "@cacsCode", DbType.String, cacsCode);
                Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                Db.AddInParameter(cmd, "@type", DbType.String, type);

                var code = ResponseCode.Invalid;
                var result = new Tuple<decimal, decimal>(0, 0);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var budget = this.DbDecimal(reader["SALDO_PRESUPUESTADO"]);
                        var credit = this.DbDecimal(reader["SUMA_CREDITO_RETENIDO"]);

                        code = ResponseCode.Ok;
                        result = new Tuple<decimal, decimal>(budget, credit);
                    }
                }

                return new Response
                               {
                                       ResponseCode = code,
                                       ResponseMethod = result
                               };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Budget> GetSpendProvisionalStatus(int year, DateTime date, bool withOutPending)
        {
            try
            {
                var result = new List<Budget>();

                var cmd = Db.GetStoredProcCommand("USP_Budgets_GetProvisionalStatusExercise");

                Db.AddInParameter(cmd, "@type", DbType.String, "G");
                Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                Db.AddInParameter(cmd, "@date", DbType.Date, date);
                Db.AddInParameter(cmd, "@withOutPending", DbType.Boolean, withOutPending);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var budget = new Budget
                                             {
                                                     ApplicationNumber = this.DbString(reader["APLICACION"]),
                                                     Amount = this.DbDecimal(reader["PRESUPUESTO_INICIAL"]),
                                                     UpdateAmount = this.DbDecimal(reader["MODIFICACION"]),
                                                     FinalAmount = this.DbDecimal(reader["DEFINITIVO"]),
                                                     AdAmount = this.DbDecimal(reader["TOTAL_AD"]),
                                                     PAmount = this.DbDecimal(reader["TOTAL_P"]),
                                                     ChapterId = this.DbIntegerNullable(reader["CAPITULO"]),
                                                     ArticleId = this.DbIntegerNullable(reader["ARTICULO"]),
                                                     ConceptId = this.DbIntegerNullable(reader["CONCEPTO"]),
                                                     SubConceptId = this.DbIntegerNullable(reader["SUBCONCEPTO"]),
                                             };

                        if (budget.ArticleId != null && budget.ConceptId != null && budget.SubConceptId != null)
                        {
                            budget.Level = "SUB";
                        }
                        else
                        {
                            if (budget.ArticleId != null && budget.ConceptId != null)
                            {
                                budget.Level = "CON";
                            }
                            else
                            {
                                budget.Level = budget.ArticleId != null ? "ART" : "CAP";
                            }
                        }

                        result.Add(budget);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Budget> GetSpendComplianceGrade(int year, DateTime since, DateTime until)
        {
            try
            {
                var result = new List<Budget>();

                var cmd = Db.GetStoredProcCommand("USP_Budgets_GetComplianceGrade");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                Db.AddInParameter(cmd, "@since", DbType.DateTime, since);
                Db.AddInParameter(cmd, "@until", DbType.DateTime, until);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var budget = new Budget
                                             {
                                                     ApplicationNumber = this.DbString(reader["APLICACION"]),
                                                     ApplicationName = this.DbString(reader["NOMBRE_APLICACION"]),
                                                     FinalAmount = this.DbDecimal(reader["IMPORTE_DEFINITIVO"]),
                                                     RcAmount = this.DbDecimal(reader["SUMA_RC"]),
                                                     RcBalanceAmount = this.DbDecimal(reader["SALDO_RC"]),
                                                     AdAmount = this.DbDecimal(reader["SUMA_AD"]),
                                                     OAmount = this.DbDecimal(reader["SUMA_O"]),
                                                     PAmount = this.DbDecimal(reader["SUMA_P"]),
                                                     Amount = this.DbDecimal(reader["SALDO"]),
                                             };

                        result.Add(budget);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Budget> GetIncomeProvisionalStatus(int year, DateTime date, bool withOutPending)
        {
            try  
            {
                var result = new List<Budget>();

                var cmd = Db.GetStoredProcCommand("USP_Budgets_GetProvisionalStatusExercise");

                Db.AddInParameter(cmd, "@type", DbType.String, "I");
                Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                Db.AddInParameter(cmd, "@date", DbType.Date, date);
                Db.AddInParameter(cmd, "@withOutPending", DbType.Boolean, withOutPending);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var budget = new Budget
                        {
                            ApplicationNumber = this.DbString(reader["APLICACION"]),
                            Amount = this.DbDecimal(reader["PRESUPUESTO_INICIAL"]),
                            UpdateAmount = this.DbDecimal(reader["MODIFICACION"]),
                            FinalAmount = this.DbDecimal(reader["DEFINITIVO"]),
                            DrAmount = this.DbDecimal(reader["TOTAL_DR"]),
                            MiAmount = this.DbDecimal(reader["TOTAL_MI"]),
                            ChapterId = this.DbIntegerNullable(reader["CAPITULO"]),
                            ArticleId = this.DbIntegerNullable(reader["ARTICULO"]),
                            ConceptId = this.DbIntegerNullable(reader["CONCEPTO"]),
                            SubConceptId = this.DbIntegerNullable(reader["SUBCONCEPTO"]),
                        };

                        if (budget.ArticleId != null && budget.ConceptId != null && budget.SubConceptId != null)
                        {
                            budget.Level = "SUB";
                        }
                        else
                        {
                            if (budget.ArticleId != null && budget.ConceptId != null)
                            {
                                budget.Level = "CON";
                            }
                            else
                            {
                                budget.Level = budget.ArticleId != null ? "ART" : "CAP";
                            }
                        }

                        result.Add(budget);
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