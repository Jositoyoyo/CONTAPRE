namespace Dimatica.ContaPre.DAL.DataContexts
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Dimatica.ContaPre.DAL.Models;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Procedures;

    #endregion

    public class BudgetApplicationsDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<BudgetApplicationsDataContext> Context = new Lazy<BudgetApplicationsDataContext>(() => new BudgetApplicationsDataContext());

        #endregion

        #region Fields

        //private object creditModificationId;

        #endregion

        #region Public Properties

        public static BudgetApplicationsDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<BudgetApplication> GetNumbersByTypeByYear(string type, int year)
        {
            try
            {
                var result = new List<BudgetApplication>();

                var cmd = Db.GetStoredProcCommand("USP_BudgetApplications_GetNumbers");

                Db.AddInParameter(cmd, "@type", DbType.String, type);
                Db.AddInParameter(cmd, "@year", DbType.String, year);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var cacsCode = this.DbString(reader["CACS_CODIGO"]);

                        if (result.Any(a => a.CacsCode.Equals(cacsCode)))
                        {
                            continue;
                        }

                        var application = new BudgetApplication
                                                  {
                                                          // BudgetId = this.DbInteger(reader["PRE_CODIGO"]),
                                                          ChapterId = this.DbIntegerNullable(reader["CAP_CODIGO"]),
                                                          ArticleId = this.DbIntegerNullable(reader["ART_CODIGO"]),
                                                          ConceptId = this.DbIntegerNullable(reader["CON_CODIGO"]),
                                                          SubConceptId = this.DbIntegerNullable(reader["SUB_CODIGO"]),
                                                          CacsCode = cacsCode,
                                                          CacsNumber = this.DbString(reader["CACS_NUMERO"]),
                                                          Description = this.DbString(reader["DESCRIPCION"])
                                                  };

                        result.Add(application);
                    }
                }

                return result.OrderBy(a => a.CacsNumber).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BudgetApplication> GetChaptersNumbersByTypeByYear(string type, int year)
        {
            try
            {
                var result = new List<BudgetApplication>();

                var cmd = Db.GetStoredProcCommand("USP_BudgetApplications_GetChaptersNumbers");

                Db.AddInParameter(cmd, "@type", DbType.String, type);
                Db.AddInParameter(cmd, "@year", DbType.String, year);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var cacsCode = this.DbString(reader["CACS_CODIGO"]);

                        if (result.Any(a => a.CacsCode.Equals(cacsCode)))
                        {
                            continue;
                        }

                        var application = new BudgetApplication
                                                  {
                                                          // BudgetId = this.DbInteger(reader["PRE_CODIGO"]),
                                                          ChapterId = this.DbIntegerNullable(reader["CAP_CODIGO"]),
                                                          ArticleId = this.DbIntegerNullable(reader["ART_CODIGO"]),
                                                          ConceptId = this.DbIntegerNullable(reader["CON_CODIGO"]),
                                                          SubConceptId = this.DbIntegerNullable(reader["SUB_CODIGO"]),
                                                          CacsCode = cacsCode,
                                                          CacsNumber = this.DbString(reader["CACS_NUMERO"]),
                                                          Description = this.DbString(reader["DESCRIPCION"])
                                                  };

                        result.Add(application);
                    }
                }

                return result.OrderBy(a => a.CacsNumber).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Response GetApplicationInfo(string cacsCode, int year, string type)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_BudgetApplications_GetInfo");

                Db.AddInParameter(cmd, "@cacsCode", DbType.String, cacsCode);
                Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                Db.AddInParameter(cmd, "@type", DbType.String, type);

                var code = ResponseCode.Invalid;
                var result = new Tuple<string, string, decimal, decimal, decimal>(string.Empty, string.Empty, 0, 0, 0);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var applicationNumber = this.DbString(reader["NUMERO_APLICACION"]);
                        var applicationName = this.DbString(reader["NOMBRE_APLICACION"]);
                        var initialBudget = this.DbDecimal(reader["PRESUP_INICIAL"]);
                        var modification = this.DbDecimal(reader["MODIFICACIONES"]);
                        var finalBudget = this.DbDecimal(reader["PRESUP_DEFINITIVO"]);

                        code = ResponseCode.Ok;
                        result = new Tuple<string, string, decimal, decimal, decimal>(applicationNumber, applicationName, initialBudget, modification, finalBudget);
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

        #endregion
    }
}