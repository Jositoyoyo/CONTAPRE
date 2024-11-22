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

    public class ProvenancesDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<ProvenancesDataContext> Context = new Lazy<ProvenancesDataContext>(() => new ProvenancesDataContext());

        #endregion

        #region Public Properties

        public static ProvenancesDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<PRE_PROCEDENCIA> GetProvenances(string type)
        {
            try
            {
                var result = new List<PRE_PROCEDENCIA>();

                var cmd = Db.GetStoredProcCommand("USP_Provenances_GetByType");

                Db.AddInParameter(cmd, "@type", DbType.String, type);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var provenance = new PRE_PROCEDENCIA
                                                 {
                                                         PROC_CODIGO = this.DbInteger(reader["PROC_CODIGO"]),
                                                         PROC_DESCRIPCION = this.DbString(reader["PROC_DESCRIPCION"])
                                                 };

                        result.Add(provenance);
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
                var cmd = Db.GetStoredProcCommand("USP_Provenances_GetIdByDesc");

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

        public Response InsertProvenance(PRE_PROCEDENCIA provenance)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Provenances_Insert");

                Db.AddInParameter(cmd, "@description", DbType.String, provenance.PROC_DESCRIPCION);
                Db.AddInParameter(cmd, "@type", DbType.String, provenance.PROC_I_G);

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

        public Response UpdateProvenance(PRE_PROCEDENCIA provenance)
        {
            try
            {
                if (provenance?.PROC_CODIGO == null)
                {
                    return new Response
                                   {
                                           ResponseCode = ResponseCode.Invalid
                                   };
                }

                var cmd = Db.GetStoredProcCommand("USP_Provenances_Update");

                Db.AddInParameter(cmd, "@id", DbType.Int32, provenance.PROC_CODIGO);
                Db.AddInParameter(cmd, "@description", DbType.String, provenance.PROC_DESCRIPCION);
                Db.AddInParameter(cmd, "@type", DbType.String, provenance.PROC_I_G);

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

        public Response DeleteProvenance(int provenanceId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Provenances_Delete");

                Db.AddInParameter(cmd, "@id", DbType.Int32, provenanceId);

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