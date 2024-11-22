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

    public class ExtraBudgetaryApplicationsDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<ExtraBudgetaryApplicationsDataContext> Context = new Lazy<ExtraBudgetaryApplicationsDataContext>(() => new ExtraBudgetaryApplicationsDataContext());

        #endregion

        #region Public Properties

        public static ExtraBudgetaryApplicationsDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<PRE_EXTRAPRESUPUESTARIA> GetExtraBudgetaryApplications()
        {
            try
            {
                var result = new List<PRE_EXTRAPRESUPUESTARIA>();

                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryApplications_GetAll");

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var application = new PRE_EXTRAPRESUPUESTARIA
                                                  {
                                                          EXTRAPRE_CODIGO = this.DbInteger(reader["EXTRAPRE_CODIGO"]),
                                                          EXTRAPRE_NUMERO = this.DbIntegerNullable(reader["EXTRAPRE_NUMERO"]),
                                                          EXTRAPRE_DESCRIPCION = this.DbString(reader["EXTRAPRE_DESCRIPCION"]),
                                                          CUEP_CODIGO = this.DbIntegerNullable(reader["CUEP_CODIGO"]),
                                                          CUEP_NUMERO = this.DbString(reader["CUEP_NUMERO"])
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

        public List<PRE_EXTRAPRESUPUESTARIA> GetExtraBudgetaryApplicationsToCombo()
        {
            try
            {
                var result = new List<PRE_EXTRAPRESUPUESTARIA>();

                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryApplications_GetToCombo");

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var application = new PRE_EXTRAPRESUPUESTARIA
                                                  {
                                                          EXTRAPRE_CODIGO = this.DbInteger(reader["EXTRAPRE_CODIGO"]),
                                                          EXTRAPRE_NUMERO = this.DbIntegerNullable(reader["EXTRAPRE_NUMERO"]),
                                                          EXTRAPRE_DESCRIPCION = this.DbString(reader["EXTRAPRE_DESCRIPCION"])
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

        public List<PRE_TIPO_EXTRAP> GetExtraBudgetaryTypes()
        {
            try
            {
                var result = new List<PRE_TIPO_EXTRAP>();

                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryApplications_GetTypes");

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var application = new PRE_TIPO_EXTRAP
                                                  {
                                                          TIP_EXTRAP_CODIGO = this.DbByte(reader["TIP_EXTRAP_CODIGO"]),
                                                          TIP_EXTRAP_CODIGO_AUX = this.DbInteger(reader["TIP_EXTRAP_CODIGO"]),
                                                          TIP_EXTRAP_DESCRIPCION = this.DbString(reader["TIP_EXTRAP_DESCRIPCION"])
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

        public Response InsertExtraBudgetaryApplication(PRE_EXTRAPRESUPUESTARIA application)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryApplications_Insert");

                Db.AddInParameter(cmd, "@number", DbType.Int32, application.EXTRAPRE_NUMERO);
                Db.AddInParameter(cmd, "@description", DbType.String, application.EXTRAPRE_DESCRIPCION);
                Db.AddInParameter(cmd, "@accountId", DbType.Int32, application.CUEP_CODIGO);

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

        public Response UpdateExtraBudgetaryApplication(PRE_EXTRAPRESUPUESTARIA application)
        {
            try
            {
                if (application?.EXTRAPRE_CODIGO == null)
                {
                    return new Response
                                   {
                                           ResponseCode = ResponseCode.Invalid
                                   };
                }

                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryApplications_Update");

                Db.AddInParameter(cmd, "@id", DbType.Int32, application.EXTRAPRE_CODIGO);
                Db.AddInParameter(cmd, "@number", DbType.Int32, application.EXTRAPRE_NUMERO);
                Db.AddInParameter(cmd, "@description", DbType.String, application.EXTRAPRE_DESCRIPCION);
                Db.AddInParameter(cmd, "@accountId", DbType.Int32, application.CUEP_CODIGO);

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

        public Response DeleteExtraBudgetaryApplication(int applicationId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryApplications_Delete");

                Db.AddInParameter(cmd, "@id", DbType.Int32, applicationId);

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

        public int GetNextNumber(int year)
        {
            try
            {
                var result = 0;

                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryRecords_GetNextNumber");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = this.DbInteger(reader["NextNumber"]);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_EXP_EXTRAPRE> GetStatus(int extraBudgetaryApplication, int year)
        {
            try
            {
                var result = new List<PRE_EXP_EXTRAPRE>();

                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryApplications_GetStatus");

                Db.AddInParameter(cmd, "@extraBudgetaryApplication", DbType.Int32, extraBudgetaryApplication);
                Db.AddInParameter(cmd, "@year", DbType.Int32, year);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var application = new PRE_EXP_EXTRAPRE
                                                  {
                                                          EXP_EXTRAP_IMPORTE = this.DbDecimal(reader["suma"]),
                                                          EXTRAPRE_DESCRIPCION = this.DbString(reader["descripcion"]),
                                                          EXTRAPRE_NOMBRE = this.DbString(reader["nombre"])
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

        public PRE_EXP_EXTRAPRE GetInfo(int extraBudgetaryApplication)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryApplications_GetInfo");

                Db.AddInParameter(cmd, "@extraBudgetaryApplication", DbType.Int32, extraBudgetaryApplication);

                PRE_EXP_EXTRAPRE application = null;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        application = new PRE_EXP_EXTRAPRE
                                              {
                                                      EXTRAPRE_NUMERO = this.DbInteger(reader["EXTRAPRE_NUMERO"]),
                                                      EXTRAPRE_DESCRIPCION = this.DbString(reader["EXTRAPRE_DESCRIPCION"])
                                              };
                    }
                }

                return application;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}