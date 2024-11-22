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

    public class DocumentTypesDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<DocumentTypesDataContext> Context = new Lazy<DocumentTypesDataContext>(() => new DocumentTypesDataContext());

        #endregion

        #region Public Properties

        public static DocumentTypesDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<PRE_TIPO_DOCUMENTO> GetDocumentTypes(string type)
        {
            try
            {
                var result = new List<PRE_TIPO_DOCUMENTO>();

                var cmd = Db.GetStoredProcCommand("USP_DocumentTypes_GetByType");

                Db.AddInParameter(cmd, "@type", DbType.String, type);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var document = new PRE_TIPO_DOCUMENTO
                        {
                            TIPD_CODIGO = this.DbInteger(reader["TIPD_CODIGO"]),
                            TIPD_CLAVE = this.DbIntegerNullable(reader["TIPD_CLAVE"]),
                            TIPD_NOMBRE_CORTO = this.DbString(reader["TIPD_NOMBRE_CORTO"]),
                            TIPD_DESCRIPCION = this.DbString(reader["TIPD_DESCRIPCION"]),
                            TIPD_POSITIVO = this.DbBooleanBit(reader["TIPD_POSITIVO"]),
                            TIPD_FASE_RC_G = this.DbBooleanBit(reader["TIPD_FASE_RC_G"]),
                            TIPD_FASE_AD_G = this.DbBooleanBit(reader["TIPD_FASE_AD_G"]),
                            TIPD_FASE_O_G = this.DbBooleanBit(reader["TIPD_FASE_O_G"]),
                            TIPD_FASE_P_G = this.DbBooleanBit(reader["TIPD_FASE_P_G"]),
                            TIPD_FASE_DR_I = this.DbBooleanBit(reader["TIPD_FASE_DR_I"]),
                            TIPD_FASE_MI_I = this.DbBooleanBit(reader["TIPD_FASE_MI_I"]),
                            TIPD_FASE_MIsinDR_I = this.DbBooleanBit(reader["TIPD_FASE_MIsinDR_I"])
                        };

                        result.Add(document);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_TIPO_DOCUMENTO> GetDocumentTypesToCombo(string type)
        {
            try
            {
                var result = new List<PRE_TIPO_DOCUMENTO>();

                var cmd = Db.GetStoredProcCommand("USP_DocumentTypes_GetToComboByType");

                Db.AddInParameter(cmd, "@type", DbType.String, type);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var document = new PRE_TIPO_DOCUMENTO
                        {
                            TIPD_CODIGO = this.DbInteger(reader["TIPD_CODIGO"]),
                            TIPD_NOMBRE_CORTO = this.DbString(reader["TIPD_NOMBRE_CORTO"]),
                            TIPD_DESCRIPCION = this.DbString(reader["TIPD_NOMBRE_CORTO"]),
                            TIPD_CLAVE = this.DbIntegerNullable(reader["TIPD_CLAVE"]),
                            TIPD_POSITIVO = this.DbBooleanBit(reader["TIPD_POSITIVO"]),
                        };

                        result.Add(document);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_TIPO_DOCUMENTO> GetDocumentTypesDrPhase()
        {
            try
            {
                var result = new List<PRE_TIPO_DOCUMENTO>();

                var cmd = Db.GetStoredProcCommand("USP_DocumentTypes_GetDrPhase");

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var document = new PRE_TIPO_DOCUMENTO
                        {
                            TIPD_CODIGO = this.DbInteger(reader["TIPD_CODIGO"]),
                            TIPD_CLAVE = this.DbInteger(reader["TIPD_CLAVE"]),
                            TIPD_NOMBRE_CORTO = this.DbString(reader["TIPD_NOMBRE_CORTO"]),
                            TIPD_DESCRIPCION = this.DbString(reader["TIPD_DESCRIPCION"]),
                            TIPD_POSITIVO = this.DbBooleanBit(reader["TIPD_POSITIVO"]),
                            TIPD_I_G = this.DbString(reader["TIPD_I_G"]),
                            TIPD_FASE_DR_I = this.DbBooleanBit(reader["TIPD_FASE_DR_I"])
                        };

                        result.Add(document);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response InsertDocumentType(PRE_TIPO_DOCUMENTO documentType)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_DocumentTypes_Insert");

                Db.AddInParameter(cmd, "@key", DbType.Int32, documentType.TIPD_CLAVE);
                Db.AddInParameter(cmd, "@shortName", DbType.String, documentType.TIPD_NOMBRE_CORTO);
                Db.AddInParameter(cmd, "@description", DbType.String, documentType.TIPD_DESCRIPCION);
                Db.AddInParameter(cmd, "@isPositive", DbType.Boolean, documentType.TIPD_POSITIVO);
                Db.AddInParameter(cmd, "@type", DbType.String, documentType.TIPD_I_G);
                Db.AddInParameter(cmd, "@rcSpend", DbType.Boolean, documentType.TIPD_FASE_RC_G);
                Db.AddInParameter(cmd, "@adSpend", DbType.Boolean, documentType.TIPD_FASE_AD_G);
                Db.AddInParameter(cmd, "@oSpend", DbType.Boolean, documentType.TIPD_FASE_O_G);
                Db.AddInParameter(cmd, "@pSpend", DbType.Boolean, documentType.TIPD_FASE_P_G);
                Db.AddInParameter(cmd, "@drEntry", DbType.Boolean, documentType.TIPD_FASE_DR_I);
                Db.AddInParameter(cmd, "@miEntry", DbType.Boolean, documentType.TIPD_FASE_MI_I);
                Db.AddInParameter(cmd, "@miWithOutDrEntry", DbType.Boolean, documentType.TIPD_FASE_MIsinDR_I);

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

        public Response UpdateDocumentType(PRE_TIPO_DOCUMENTO documentType)
        {
            try
            {
                if (documentType?.TIPD_CODIGO == null)
                {
                    return new Response
                    {
                        ResponseCode = ResponseCode.Invalid
                    };
                }

                var cmd = Db.GetStoredProcCommand("USP_DocumentTypes_Update");

                Db.AddInParameter(cmd, "@id", DbType.Int32, documentType.TIPD_CODIGO);
                Db.AddInParameter(cmd, "@key", DbType.Int32, documentType.TIPD_CLAVE);
                Db.AddInParameter(cmd, "@shortName", DbType.String, documentType.TIPD_NOMBRE_CORTO);
                Db.AddInParameter(cmd, "@description", DbType.String, documentType.TIPD_DESCRIPCION);
                Db.AddInParameter(cmd, "@isPositive", DbType.Boolean, documentType.TIPD_POSITIVO);
                Db.AddInParameter(cmd, "@type", DbType.String, documentType.TIPD_I_G);
                Db.AddInParameter(cmd, "@rcSpend", DbType.Boolean, documentType.TIPD_FASE_RC_G);
                Db.AddInParameter(cmd, "@adSpend", DbType.Boolean, documentType.TIPD_FASE_AD_G);
                Db.AddInParameter(cmd, "@oSpend", DbType.Boolean, documentType.TIPD_FASE_O_G);
                Db.AddInParameter(cmd, "@pSpend", DbType.Boolean, documentType.TIPD_FASE_P_G);
                Db.AddInParameter(cmd, "@drEntry", DbType.Boolean, documentType.TIPD_FASE_DR_I);
                Db.AddInParameter(cmd, "@miEntry", DbType.Boolean, documentType.TIPD_FASE_MI_I);
                Db.AddInParameter(cmd, "@miWithOutDrEntry", DbType.Boolean, documentType.TIPD_FASE_MIsinDR_I);

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

        public Response DeleteDocumentType(int documentTypeId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_DocumentTypes_Delete");

                Db.AddInParameter(cmd, "@id", DbType.Int32, documentTypeId);

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