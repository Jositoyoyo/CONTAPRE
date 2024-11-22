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

    public class ParametersDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<ParametersDataContext> Context = new Lazy<ParametersDataContext>(() => new ParametersDataContext());

        #endregion

        #region Public Properties

        public static ParametersDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<PRE_PARAMETROS> GetParameters()
        {
            try
            {
                var result = new List<PRE_PARAMETROS>();

                var cmd = Db.GetStoredProcCommand("USP_Parameters_GetAll");

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var parameter = new PRE_PARAMETROS
                                                {
                                                        PAR_CODIGO = this.DbInteger(reader["PAR_CODIGO"]),
                                                        PAR_DESCRIPCION = this.DbString(reader["PAR_DESCRIPCION"]),
                                                        PAR_VALOR = this.DbString(reader["PAR_VALOR"])
                                                };

                        result.Add(parameter);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response UpdateParameter(PRE_PARAMETROS parameters)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Parameters_Update");

                Db.AddInParameter(cmd, "@id", DbType.Int32, parameters.PAR_CODIGO);
                Db.AddInParameter(cmd, "@value", DbType.String, parameters.PAR_VALOR);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, parameters.USU_CODIGO);

                var code = ResponseCode.Invalid;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        switch (result)
                        {
                            case 0:
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

        #endregion
    }
}