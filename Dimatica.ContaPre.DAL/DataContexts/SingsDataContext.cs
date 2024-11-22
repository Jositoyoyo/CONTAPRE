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

    public class SingsDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<SingsDataContext> Context = new Lazy<SingsDataContext>(() => new SingsDataContext());

        #endregion

        #region Public Properties

        public static SingsDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<PRE_SENALAMIENTO> GetByFilters(int year, int? pointingNumber, string date, decimal? amount, int? fileNumber, string type)
        {
            try
            {
                var result = new List<PRE_SENALAMIENTO>();

                var cmd = Db.GetStoredProcCommand("USP_Sings_GetByFilters");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);

                if (pointingNumber != null)
                {
                    Db.AddInParameter(cmd, "@pointingNumber", DbType.Int32, pointingNumber);
                }

                if (!string.IsNullOrWhiteSpace(date))
                {
                    Db.AddInParameter(cmd, "@pointingDate", DbType.String, date);
                }

                if (amount != null)
                {
                    Db.AddInParameter(cmd, "@pointingAmount", DbType.Decimal, amount);
                }

                if (fileNumber != null)
                {
                    Db.AddInParameter(cmd, "@fileNumber", DbType.Int32, fileNumber);
                }

                if (!string.IsNullOrWhiteSpace(type))
                {
                    Db.AddInParameter(cmd, "@documentType", DbType.String, type);
                }

                using (IDataReader reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var sing = new PRE_SENALAMIENTO
                        {
                            SEN_CODIGO = this.DbInteger(reader["SEN_CODIGO"]),
                            SEN_ANO = this.DbShortNullable(reader["SEN_ANO"]),
                            SEN_NUMERO = this.DbIntegerNullable(reader["SEN_NUMERO"]),
                            SEN_TOTAL_LIQUIDO = this.DbDecimal(reader["SEN_TOTAL_LIQUIDO"]),
                            SEN_FECHA = this.DbDateNullable(reader["SEN_FECHA"]),
                            NUMERO_EXPEDIENTE = this.DbIntegerNullable(reader["NUMERO_EXPEDIENTE"]),
                            NUMERO_DOCUMENTOS = this.DbInteger(reader["NUMERO_DOCUMENTOS"])
                        };

                        result.Add(sing);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PRE_SENALAMIENTO GetById(int id)
        {
            try
            {
                PRE_SENALAMIENTO result = null;

                var cmd = Db.GetStoredProcCommand("USP_Sings_GetById");

                Db.AddInParameter(cmd, "@id", DbType.Int32, id);

                using (IDataReader reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = new PRE_SENALAMIENTO
                        {
                            SEN_CODIGO = this.DbInteger(reader["SEN_CODIGO"]),
                            SEN_ANO = this.DbShortNullable(reader["SEN_ANO"]),
                            SEN_NUMERO = this.DbIntegerNullable(reader["SEN_NUMERO"]),
                            SEN_TOTAL_LIQUIDO = this.DbDecimal(reader["SEN_TOTAL_LIQUIDO"]),
                            SEN_FECHA = this.DbDateNullable(reader["SEN_FECHA"])
                        };
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_SENALAMIENTO_DOCUMENTO> GetDocumentsByPointingId(int pointingId)
        {
            try
            {
                var result = new List<PRE_SENALAMIENTO_DOCUMENTO>();

                var cmd = Db.GetStoredProcCommand("USP_Sings_GetDocumentsById");

                Db.AddInParameter(cmd, "@pointingId", DbType.Int32, pointingId);

                using (IDataReader reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var document = new PRE_SENALAMIENTO_DOCUMENTO
                        {
                            SEND_CODIGO = this.DbInteger(reader["SEND_CODIGO"]),
                            DOC_CODIGO = this.DbIntegerNullable(reader["DOC_CODIGO"]),
                            EXP_EXTRAP_CODIGO = this.DbIntegerNullable(reader["EXP_EXTRAP_CODIGO"]),
                            NUMERO_DOCUMENTO = this.DbIntegerNullable(reader["NUMERO_DOCUMENTO"]),
                            TIPO_DOCUMENTO = this.DbString(reader["TIPO_DOCUMENTO"]),
                            CONCEPTO = this.DbString(reader["CONCEPTO"]),
                            PERCEPTOR = this.DbString(reader["PERCEPTOR"]),
                            IMPORTE_INTEGRO = this.DbDecimal(reader["IMPORTE_INTEGRO"]),
                            IRPF = this.DbDecimal(reader["IRPF"]),
                            SEGURIDAD_SOCIAL = this.DbDecimal(reader["SEGURIDAD_SOCIAL"]),
                            BOE = this.DbDecimal(reader["BOE"]),
                            D_PASIVOS = this.DbDecimal(reader["D_PASIVOS"]),
                            MUFACE = this.DbDecimal(reader["MUFACE"]),
                            ANTICIPO_HABERES = this.DbDecimal(reader["ANTICIPO_HABERES"]),
                            INTERESES_ANTICIPOS = this.DbDecimal(reader["INTERESES_ANTICIPOS"]),
                            IMPORTE_LIQUIDO = this.DbDecimal(reader["IMPORTE_LIQUIDO"]),
                            NUMERO_CHEQUE = this.DbString(reader["NUMERO_CHEQUE"]),
                            SEN_FECHA = this.DbDateNullable(reader["SEN_FECHA"])
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

        public List<PRE_SENALAMIENTO_DOCUMENTO> GetTransfersByPointingId(int pointingId, bool groupTransfers)
        {
            try
            {
                var result = new List<PRE_SENALAMIENTO_DOCUMENTO>();

                var cmd = Db.GetStoredProcCommand("USP_Sings_GetTransfersById");

                Db.AddInParameter(cmd, "@pointingId", DbType.Int32, pointingId);
                Db.AddInParameter(cmd, "@groupTransfers", DbType.Boolean, groupTransfers);

                using (IDataReader reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var document = new PRE_SENALAMIENTO_DOCUMENTO
                        {
                            SEND_CODIGO_AUX = this.DbIntegerNullable(reader["SEND_CODIGO"]),
                            DOC_CODIGO_AUX = this.DbString(reader["DOC_CODIGO"]),
                            EXP_EXTRAP_CODIGO = this.DbIntegerNullable(reader["EXP_EXTRAP_CODIGO"]),
                            PERCEPTOR = this.DbString(reader["PERCEPTOR"]),
                            IMPORTE_LIQUIDO = this.DbDecimal(reader["IMPORTE_LIQUIDO"]),
                            NUMERO_CHEQUE = this.DbString(reader["NUMERO_CHEQUE"]),
                            PROV_CC_CE = this.DbString(reader["PROV_CC_CE"]),
                            PROV_CC_CO = this.DbString(reader["PROV_CC_CO"]),
                            PROV_CC_DC = this.DbString(reader["PROV_CC_DC"]),
                            PROV_CC_NC = this.DbString(reader["PROV_CC_NC"]),
                            PROV_IBAN = this.DbString(reader["PROV_IBAN"]),
                            PROV_NOMBRE_SUCURSAL = this.DbString(reader["PROV_NOMBRE_SUCURSAL"]),
                            PROV_DIR_SUCURSAL = this.DbString(reader["PROV_DIR_SUCURSAL"]),
                            PROV_CP_SUCURSAL = this.DbString(reader["PROV_CP_SUCURSAL"]),
                            PROV_POBLACION_SUCURSAL = this.DbString(reader["PROV_POBLACION_SUCURSAL"]),
                            DOC_FACTURA = this.DbString(reader["FACTURA"]),
                            PROV_CODIGO = this.DbIntegerNullable(reader["PROV_CODIGO"])
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

        public List<PRE_DOCUMENTO_CONTABLE> GetPendingDocuments(int year, string documentType)
        {
            try
            {
                var result = new List<PRE_DOCUMENTO_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_Sings_GetPendingDocuments");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);

                if (!string.IsNullOrWhiteSpace(documentType))
                {
                    Db.AddInParameter(cmd, "@type", DbType.String, documentType);
                }

                using (IDataReader reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var document = new PRE_DOCUMENTO_CONTABLE
                        {
                            DOC_CODIGO_AUX = this.DbIntegerNullable(reader["DOC_CODIGO"]),
                            DOC_FECHA_ASIENTO_DIARIO = this.DbDateNullable(reader["DOC_FECHA_ASIENTO_DIARIO"]),
                            EXP_EXTRAP_CODIGO = this.DbIntegerNullable(reader["EXP_EXTRAP_CODIGO"]),
                            NUMERO_DOCUMENTO = this.DbIntegerNullable(reader["NUMERO_DOCUMENTO"]),
                            TIPO_DOCUMENTO = this.DbString(reader["TIPO_DOCUMENTO"]),
                            CONCEPTO = this.DbString(reader["CONCEPTO"]),
                            PROV_NOMBRE = this.DbString(reader["PERCEPTOR"]),
                            LIQUIDO = this.DbDecimal(reader["IMPORTE_LIQUIDO"]),
                            DOC_NUMERO_CHEQUE = this.DbString(reader["NUMERO_CHEQUE"]),
                            doc_reparado = this.DbBooleanBit(reader["DOC_REPARADO"]),
                            tiene_irpf = this.DbString(reader["tiene_irpf"]),
                            Select = false,
                            Repair = this.DbBooleanBit(reader["DOC_REPARADO"])
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

        public Response InsertPointing(PRE_SENALAMIENTO pointing)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Sings_Insert");

                Db.AddInParameter(cmd, "@singYear", DbType.Int32, pointing.SEN_ANO);
                Db.AddInParameter(cmd, "@singNumber", DbType.Int32, pointing.SEN_NUMERO);
                Db.AddInParameter(cmd, "@singDate", DbType.DateTime, pointing.SEN_FECHA);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, pointing.USU_CODIGO);

                var code = ResponseCode.Invalid;
                var id = 0;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        if (result != 0)
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

        public Response DeletePointing(int id, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Sings_Delete");

                Db.AddInParameter(cmd, "@id", DbType.Int32, id);
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
                    ResponseCode = code,
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

                var cmd = Db.GetStoredProcCommand("USP_Sings_GetNextNumber");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);

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

        public Response InsertDocumentSing(int pointingId, int? documentCode, int? fileCode, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Sings_InsertDocument");

                Db.AddInParameter(cmd, "@singId", DbType.Int32, pointingId);

                if (documentCode != null)
                {
                    Db.AddInParameter(cmd, "@documentCode", DbType.Int32, documentCode);
                }

                if (fileCode != null)
                {
                    Db.AddInParameter(cmd, "@fileCode", DbType.Int32, fileCode);
                }

                Db.AddInParameter(cmd, "@userId", DbType.Int32, userId);

                var code = ResponseCode.Invalid;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        if (result != 0)
                        {
                            code = ResponseCode.Ok;
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

        public Response DeleteDocument(int id, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Sings_DeleteDocument");

                Db.AddInParameter(cmd, "@id", DbType.Int32, id);
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
                    ResponseCode = code,
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