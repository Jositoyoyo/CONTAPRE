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

    public class CostPlacesDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<CostPlacesDataContext> Context = new Lazy<CostPlacesDataContext>(() => new CostPlacesDataContext());

        #endregion

        #region Public Properties

        public static CostPlacesDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<PRE_CENTRO_COSTE> GetCostPlacesToCombo()
        {
            try
            {
                var result = new List<PRE_CENTRO_COSTE>();

                var cmd = Db.GetStoredProcCommand("USP_CostPlaces_GetToCombo");

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var place = new PRE_CENTRO_COSTE
                        {
                            CEN_CODIGO = this.DbInteger(reader["CEN_CODIGO"]),
                            CEN_DESCRIPCION = this.DbString(reader["CEN_DESCRIPCION"])
                        };

                        result.Add(place);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_CENTRO_COSTE> GetCostPlacesOriginToCombo()
        {
            try
            {
                var result = new List<PRE_CENTRO_COSTE>();

                var cmd = Db.GetStoredProcCommand("USP_CostPlaces_GetOriginToCombo");

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var place = new PRE_CENTRO_COSTE
                        {
                            CEN_CODIGO = this.DbInteger(reader["CEN_CODIGO"]),
                            CEN_DESCRIPCION = this.DbString(reader["CEN_DESCRIPCION"]),
                            CEN_AREA_ORIGEN = this.DbIntegerNullable(reader["CEN_AREA_ORIGEN"])
                        };

                        result.Add(place);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response GetIdByDescription(string description)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_CostPlaces_GetCodeByDesc");

                Db.AddInParameter(cmd, "@description", DbType.String, description);

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

        #endregion
    }
}