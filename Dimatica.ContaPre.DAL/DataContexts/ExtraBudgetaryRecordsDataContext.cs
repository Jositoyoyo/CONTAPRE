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

    public class ExtraBudgetaryRecordsDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<ExtraBudgetaryRecordsDataContext> Context = new Lazy<ExtraBudgetaryRecordsDataContext>(() => new ExtraBudgetaryRecordsDataContext());

        #endregion

        #region Public Properties

        public static ExtraBudgetaryRecordsDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<PRE_EXP_EXTRAPRE> GetByDocumentId(int accountingDocumentId, int type)
        {
            try
            {
                var result = new List<PRE_EXP_EXTRAPRE>();

                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryRecords_Get");

                Db.AddInParameter(cmd, "@accountingDocument", DbType.Int32, accountingDocumentId);
                Db.AddInParameter(cmd, "@type", DbType.Int32, type);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var extraBudgetary = new PRE_EXP_EXTRAPRE
                        {
                            EXP_EXTRAP_CODIGO = this.DbInteger(reader["EXP_EXTRAP_CODIGO"]),
                            EXP_CODIGO = this.DbIntegerNullable(reader["EXP_CODIGO"]),
                            EXTRAPRE_CODIGO = this.DbInteger(reader["EXTRAPRE_CODIGO"]),
                            EXP_EXTRAP_ANO_PRESUPUESTO = this.DbShortNullable(reader["EXP_EXTRAP_ANO_PRESUPUESTO"]),
                            TIP_EXTRAP_CODIGO = this.DbByteNullable(reader["TIP_EXTRAP_CODIGO"]),
                            DOC_CODIGO = this.DbIntegerNullable(reader["DOC_CODIGO"]),
                            EXP_EXTRAP_IMPORTE = this.DbDecimal(reader["EXP_EXTRAP_IMPORTE"]),
                            EXP_EXTRAP_FECHA = this.DbDateNullable(reader["EXP_EXTRAP_FECHA"]),
                            EXP_EXTRAP_TEXTO = this.DbString(reader["EXP_EXTRAP_TEXTO"]),
                            EXTRAPRE_NUMERO = this.DbIntegerNullable(reader["EXTRAPRE_NUMERO"]),
                            EXTRAPRE_DESCRIPCION = this.DbString(reader["EXTRAPRE_DESCRIPCION"])
                        };

                        result.Add(extraBudgetary);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_EXP_EXTRAPRE> GetByFilters(int? year, int? extraBudgetaryType, int? extraBudgetaryApplication, bool? isBound, string sinceDate, string untilDate, int? fileNumberSince, int? fileNumberUntil, int? providerCode)
        {
            try
            {
                var result = new List<PRE_EXP_EXTRAPRE>();

                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryRecords_GetByFilters");

                if (year != null)
                {
                    Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                }

                if (extraBudgetaryType != null)
                {
                    Db.AddInParameter(cmd, "@typeCode", DbType.Int32, extraBudgetaryType);
                }

                if (extraBudgetaryApplication != null)
                {
                    Db.AddInParameter(cmd, "@extraBudgetaryCode", DbType.Int32, extraBudgetaryApplication);
                }

                if (!string.IsNullOrWhiteSpace(sinceDate))
                {
                    Db.AddInParameter(cmd, "@sinceDate", DbType.String, sinceDate);
                }

                if (!string.IsNullOrWhiteSpace(untilDate))
                {
                    Db.AddInParameter(cmd, "@untilDate", DbType.String, untilDate);
                }

                if (fileNumberSince != null)
                {
                    Db.AddInParameter(cmd, "@sinceFileCode", DbType.Int32, fileNumberSince);
                }

                if (fileNumberUntil != null)
                {
                    Db.AddInParameter(cmd, "@untilFileCode", DbType.Int32, fileNumberUntil);
                }

                if (isBound != null)
                {
                    Db.AddInParameter(cmd, "@isBound", DbType.Boolean, isBound);
                }

                if (providerCode != null)
                {
                    Db.AddInParameter(cmd, "@providerCode", DbType.Int32, providerCode);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var extraBudgetary = new PRE_EXP_EXTRAPRE
                        {
                            EXP_EXTRAP_CODIGO = this.DbInteger(reader["EXP_EXTRAP_CODIGO"]),
                            EXP_CODIGO = this.DbIntegerNullable(reader["EXP_CODIGO"]),
                            EXP_EXTRAP_ANO_PRESUPUESTO = this.DbShortNullable(reader["ANO"]),
                            EXTRAPRE_CODIGO = this.DbInteger(reader["EXTRAPRE_CODIGO"]),
                            TIPO_DOC = this.DbString(reader["TIPO_DOC"]),
                            EXP_EXTRAP_IMPORTE = this.DbDecimal(reader["EXP_EXTRAP_IMPORTE"]),
                            EXP_EXTRAP_FECHA = this.DbDateNullable(reader["EXP_EXTRAP_FECHA"]),
                            INTERESADO = this.DbString(reader["INTERESADO"]),
                            TERCERO = this.DbString(reader["TERCERO"]),
                            EXP_NUM_EXP_CONTABLE_ANUAL_LABEL = this.DbString(reader["EXP_NUM_EXP_CONTABLE_ANUAL"]),
                            ORDINAL_PAGADOR = this.DbString(reader["ORDINAL_PAGADOR"]),
                            EXP_EXTRAP_NUMERO = this.DbIntegerNullable(reader["EXP_EXTRAP_NUMERO"]),
                            EXTRAPRE_NUMERO = this.DbIntegerNullable(reader["EXTRAPRE_NUMERO"]),
                            EXTRAPRE_DESCRIPCION = this.DbString(reader["EXTRAPRE_DESCRIPCION"]),
                            NUM_ORDINAL_PAGADOR = this.DbIntegerNullable(reader["NUM_ORDINAL_PAGADOR"])
                        };

                        result.Add(extraBudgetary);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_EXP_EXTRAPRE> GetByBoundId(int extraBudgetaryId)
        {
            try
            {
                var result = new List<PRE_EXP_EXTRAPRE>();

                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryRecords_GetByBoundId");

                Db.AddInParameter(cmd, "@extraBudgetaryId", DbType.Int32, extraBudgetaryId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var extraBudgetary = new PRE_EXP_EXTRAPRE
                        {
                            EXP_EXTRAP_CODIGO = this.DbInteger(reader["EXP_EXTRAP_CODIGO"]),
                            EXTRAPRE_CODIGO = this.DbInteger(reader["EXTRAPRE_CODIGO"]),
                            EXP_EXTRAP_ANO_PRESUPUESTO = this.DbShortNullable(reader["EXP_EXTRAP_ANO_PRESUPUESTO"]),
                            TIP_EXTRAP_CODIGO = this.DbByteNullable(reader["TIP_EXTRAP_CODIGO"]),
                            EXP_EXTRAP_IMPORTE = this.DbDecimal(reader["EXP_EXTRAP_IMPORTE"]),
                            EXP_EXTRAP_FECHA = this.DbDateNullable(reader["EXP_EXTRAP_FECHA"]),
                            EXP_EXTRAP_TEXTO = this.DbString(reader["EXP_EXTRAP_TEXTO"]),
                            EXTRAPRE_DESCRIPCION = this.DbString(reader["EXTRAPRE_NUMERO_DESCRIPCION"])
                        };

                        result.Add(extraBudgetary);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<PRE_PARAMETROS, PRE_EXP_EXTRAPRE> GetMi(int extraBudgetaryId)
        {
            try
            {
                PRE_EXP_EXTRAPRE extraBudgetary = null;
                PRE_PARAMETROS parameter = null;
                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryRecords_GetMI");

                Db.AddInParameter(cmd, "@extraBudgetaryCode", DbType.Int32, extraBudgetaryId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        extraBudgetary = new PRE_EXP_EXTRAPRE
                        {
                            EXP_EXTRAP_CODIGO = this.DbInteger(reader["EXP_EXTRAP_CODIGO"]),
                            EXP_CODIGO = this.DbIntegerNullable(reader["EXP_CODIGO"]),
                            EXTRAPRE_CODIGO = this.DbInteger(reader["EXTRAPRE_CODIGO"]),
                            EXP_EXTRAP_ANO_PRESUPUESTO = this.DbShortNullable(reader["EXP_EXTRAP_ANO_PRESUPUESTO"]),
                            TIP_EXTRAP_CODIGO = this.DbByteNullable(reader["TIP_EXTRAP_CODIGO"]),
                            EXP_EXTRAP_FECHA = this.DbDateNullable(reader["EXP_EXTRAP_FECHA"]),
                            EXP_EXTRAP_IMPORTE = this.DbDecimal(reader["EXP_EXTRAP_IMPORTE"]),
                            PROV_CODIGO_PROVEEDOR = this.DbIntegerNullable(reader["PROV_CODIGO_PROVEEDOR"]),
                            CUE_CODIGO = this.DbIntegerNullable(reader["CUE_CODIGO"]),
                            TIPP_CODIGO = this.DbByteNullable(reader["TIPP_CODIGO"]),
                            FOR_CODIGO = this.DbByteNullable(reader["FOR_CODIGO"]),
                            CUE_CODIGO_PAGADOR = this.DbIntegerNullable(reader["CUE_CODIGO_PAGADOR"]),
                            EXP_EXTRAP_TEXTO = this.DbString(reader["EXP_EXTRAP_TEXTO"]),
                            EXP_EXTRAP_NUMERO = this.DbIntegerNullable(reader["EXP_EXTRAP_NUMERO"]),
                            PROV_CODIGO_TERCERO = this.DbIntegerNullable(reader["PROV_CODIGO_TERCERO"]),
                            EXP_EXTRAP_PAGADO = this.DbBooleanBit(reader["EXP_EXTRAP_PAGADO"]),
                            EXP_EXTRAP_NUMERO_PMP = this.DbIntegerNullable(reader["EXP_EXTRAP_NUMERO_PMP"]),
                            EXP_EXTRAP_NUMERO_CHEQUE = this.DbString(reader["EXP_EXTRAP_NUMERO_CHEQUE"]),
                            CUEP_NUMERO = this.DbString(reader["CUEP_NUMERO"]),
                            EXP_NUM_EXP_CONTABLE_ANUAL = this.DbIntegerNullable(reader["EXP_NUM_EXP_CONTABLE_ANUAL"]),
                            EXTRAPRE_NUMERO = this.DbIntegerNullable(reader["EXTRAPRE_NUMERO"]),
                            EXTRAPRE_DESCRIPCION = this.DbString(reader["EXTRAPRE_DESCRIPCION"]),
                            PROV_NIF = this.DbString(reader["PROV_NIF"]),
                            INTERESADO = this.DbString(reader["PROV_NOMBRE"]),
                            CUE_ORDINAL_PERCEPTOR = this.DbIntegerNullable(reader["CUE_ORDINAL_PERCEPTOR"]),
                            //SEN_NUMERO = this.DbIntegerNullable(reader["SEN_NUMERO"])
                            HOJ_NUMERO = this.DbIntegerNullable(reader["HOJ_NUMERO"])
                        };
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        parameter = new PRE_PARAMETROS
                        {
                            CODIGO_MINISTERIO = this.DbString(reader["CODIGO_MINISTERIO"]),
                            CODIGO_ORGANISMO = this.DbString(reader["CODIGO_ORGANISMO"]),
                            NOMBRE_ORGANISMO = this.DbString(reader["NOMBRE_ORGANISMO"]),
                            MINISTERIO = this.DbString(reader["MINISTERIO"]),
                        };
                    }

                }

                return new Tuple<PRE_PARAMETROS, PRE_EXP_EXTRAPRE>(parameter, extraBudgetary);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<PRE_EXP_EXTRAPRE, PRE_PARAMETROS, List<PRE_EXP_EXTRAPRE>> GetPmp(int extraBudgetaryId)
        {
            try
            {
                PRE_EXP_EXTRAPRE extraBudgetary = null;
                PRE_PARAMETROS parameter = null;
                var discounts = new List<PRE_EXP_EXTRAPRE>();

                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryRecords_GetPmp");

                Db.AddInParameter(cmd, "@extraBudgetaryCode", DbType.Int32, extraBudgetaryId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        extraBudgetary = new PRE_EXP_EXTRAPRE
                        {
                            EXP_EXTRAP_CODIGO = this.DbInteger(reader["EXP_EXTRAP_CODIGO"]),
                            EXP_CODIGO = this.DbIntegerNullable(reader["EXP_CODIGO"]),
                            EXTRAPRE_CODIGO = this.DbInteger(reader["EXTRAPRE_CODIGO"]),
                            EXP_EXTRAP_ANO_PRESUPUESTO = this.DbShortNullable(reader["EXP_EXTRAP_ANO_PRESUPUESTO"]),
                            TIP_EXTRAP_CODIGO = this.DbByteNullable(reader["TIP_EXTRAP_CODIGO"]),
                            EXP_EXTRAP_FECHA = this.DbDateNullable(reader["EXP_EXTRAP_FECHA"]),
                            EXP_EXTRAP_IMPORTE = this.DbDecimal(reader["EXP_EXTRAP_IMPORTE"]),
                            PROV_CODIGO_PROVEEDOR = this.DbIntegerNullable(reader["PROV_CODIGO_PROVEEDOR"]),
                            CUE_CODIGO = this.DbIntegerNullable(reader["CUE_CODIGO"]),
                            TIPP_CODIGO = this.DbByteNullable(reader["TIPP_CODIGO"]),
                            FOR_CODIGO = this.DbByteNullable(reader["FOR_CODIGO"]),
                            CUE_CODIGO_PAGADOR = this.DbIntegerNullable(reader["CUE_CODIGO_PAGADOR"]),
                            EXP_EXTRAP_TEXTO = this.DbString(reader["EXP_EXTRAP_TEXTO"]),
                            EXP_EXTRAP_NUMERO = this.DbIntegerNullable(reader["EXP_EXTRAP_NUMERO"]),
                            PROV_CODIGO_TERCERO = this.DbIntegerNullable(reader["PROV_CODIGO_TERCERO"]),
                            EXP_EXTRAP_PAGADO = this.DbBooleanBit(reader["EXP_EXTRAP_PAGADO"]),
                            EXP_EXTRAP_NUMERO_PMP = this.DbIntegerNullable(reader["EXP_EXTRAP_NUMERO_PMP"]),
                            EXP_EXTRAP_NUMERO_CHEQUE = this.DbString(reader["EXP_EXTRAP_NUMERO_CHEQUE"]),
                            CUEP_NUMERO = this.DbString(reader["CUEP_NUMERO"]),
                            EXP_NUM_EXP_CONTABLE_ANUAL = this.DbIntegerNullable(reader["EXP_NUM_EXP_CONTABLE_ANUAL"]),
                            EXTRAPRE_NUMERO = this.DbIntegerNullable(reader["EXTRAPRE_NUMERO"]),
                            EXTRAPRE_DESCRIPCION = this.DbString(reader["EXTRAPRE_DESCRIPCION"]),
                            PROV_NIF = this.DbString(reader["PROV_NIF"]),
                            INTERESADO = this.DbString(reader["PROV_NOMBRE"]),
                            CUE_ORDINAL_PERCEPTOR = this.DbIntegerNullable(reader["CUE_ORDINAL_PERCEPTOR"]),
                            SEN_NUMERO = this.DbIntegerNullable(reader["SEN_NUMERO"])
                        };
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        parameter = new PRE_PARAMETROS
                        {
                            CODIGO_MINISTERIO = this.DbString(reader["CODIGO_MINISTERIO"]),
                            CODIGO_ORGANISMO = this.DbString(reader["CODIGO_ORGANISMO"]),
                            NOMBRE_ORGANISMO = this.DbString(reader["NOMBRE_ORGANISMO"]),
                            MINISTERIO = this.DbString(reader["MINISTERIO"]),
                        };
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        var discount = new PRE_EXP_EXTRAPRE
                        {
                            CODIGO_DESCUENTO = this.DbString(reader["codigo_descuento"]),
                            EXTRAPRE_DESCRIPCION = this.DbString(reader["descripcion_descuento"]),
                            EXP_EXTRAP_IMPORTE = this.DbDecimal(reader["importe_descuento"]),
                            CUEP_NUMERO = this.DbString(reader["cta_pgca"])
                        };

                        discounts.Add(discount);
                    }
                }

                return new Tuple<PRE_EXP_EXTRAPRE, PRE_PARAMETROS, List<PRE_EXP_EXTRAPRE>>(extraBudgetary, parameter, discounts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_EXP_EXTRAPRE> GetDebitAndCredit(int extraBudgetaryId, DateTime since, DateTime until)
        {
            try
            {
                var result = new List<PRE_EXP_EXTRAPRE>();

                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryRecords_GetDebitAndCredit");

                Db.AddInParameter(cmd, "@extraBudgetaryApplication", DbType.Int32, extraBudgetaryId);
                Db.AddInParameter(cmd, "@since", DbType.Date, since);
                Db.AddInParameter(cmd, "@until", DbType.Date, until);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var extraBudgetary = new PRE_EXP_EXTRAPRE
                        {
                            EXP_EXTRAP_FECHA = this.DbDateNullable(reader["FECHA_EE"]),
                            EXP_EXTRAP_NUMERO = this.DbIntegerNullable(reader["NUMERO_EE"]),
                            EXTRAPRE_DESCRIPCION = this.DbString(reader["TIPO_DOC"]),
                            TIP_EXTRAP_CODIGO = this.DbByteNullable(reader["CODIGO_TIPO_DOC"]),
                            EXP_EXTRAP_TEXTO = this.DbString(reader["TEXTO"]),
                            TERCERO = this.DbString(reader["PROVEEDOR"]),
                            EXP_EXTRAP_NUMERO_CHEQUE = this.DbString(reader["NUMERO_EC_ANUAL"]),
                            EXP_EXTRAP_IMPORTE = this.DbDecimal(reader["IMPORTE"])
                        };

                        result.Add(extraBudgetary);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PRE_EXP_EXTRAPRE GetById(int extraBudgetaryId)
        {
            try
            {
                PRE_EXP_EXTRAPRE result = null;

                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryRecords_GetById");

                Db.AddInParameter(cmd, "@extraBudgetaryId", DbType.Int32, extraBudgetaryId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = new PRE_EXP_EXTRAPRE
                        {
                            EXP_EXTRAP_CODIGO = this.DbInteger(reader["EXP_EXTRAP_CODIGO"]),
                            EXP_CODIGO = this.DbIntegerNullable(reader["EXP_CODIGO"]),
                            EXTRAPRE_CODIGO = this.DbInteger(reader["EXTRAPRE_CODIGO"]),
                            EXP_EXTRAP_ANO_PRESUPUESTO = this.DbShortNullable(reader["EXP_EXTRAP_ANO_PRESUPUESTO"]),
                            TIP_EXTRAP_CODIGO = this.DbByteNullable(reader["TIP_EXTRAP_CODIGO"]),
                            EXP_EXTRAP_FECHA = this.DbDateNullable(reader["EXP_EXTRAP_FECHA"]),
                            EXP_EXTRAP_IMPORTE = this.DbDecimal(reader["EXP_EXTRAP_IMPORTE"]),
                            PROV_CODIGO_PROVEEDOR = this.DbIntegerNullable(reader["PROV_CODIGO_PROVEEDOR"]),
                            CUE_CODIGO = this.DbIntegerNullable(reader["CUE_CODIGO"]),
                            TIPP_CODIGO = this.DbByteNullable(reader["TIPP_CODIGO"]),
                            FOR_CODIGO = this.DbByteNullable(reader["FOR_CODIGO"]),
                            CUE_CODIGO_PAGADOR = this.DbIntegerNullable(reader["CUE_CODIGO_PAGADOR"]),
                            EXP_EXTRAP_TEXTO = this.DbString(reader["EXP_EXTRAP_TEXTO"]),
                            EXP_EXTRAP_NUMERO = this.DbIntegerNullable(reader["EXP_EXTRAP_NUMERO"]),
                            PROV_CODIGO_TERCERO = this.DbIntegerNullable(reader["PROV_CODIGO_TERCERO"]),
                            HOJ_NUMERO = this.DbIntegerNullable(reader["HOJ_NUMERO"]),
                            HOJ_NUMERO50 = this.DbIntegerNullable(reader["HOJ_NUMERO50"]),
                            ANO_HOJA = this.DbShortNullable(reader["ANO_HOJA"]),
                            ANO_HOJA50 = this.DbShortNullable(reader["ANO_HOJA50"]),
                            EXP_ENLAZADO_TESORERIA = this.DbBooleanBit(reader["EXP_ENLAZADO_TESORERIA"]),
                            TES_CODIGO = this.DbIntegerNullable(reader["TES_CODIGO"]),
                            EXP_EXTRAP_NUMERO_CHEQUE = this.DbString(reader["EXP_EXTRAP_NUMERO_CHEQUE"]),
                            EXP_NUM_EXP_CONTABLE_ANUAL = this.DbIntegerNullable(reader["EXP_NUM_EXP_CONTABLE_ANUAL"]),
                            CUEP_NUMERO = this.DbString(reader["CUEP_NUMERO"]),
                            SEN_NUMERO = this.DbIntegerNullable(reader["SEN_NUMERO"]),
                            EXP_NUM_EXP_EXTRAPRE = this.DbIntegerNullable(reader["EXP_NUM_EXP_EXTRAPRE"])
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

        public Response InsertExtraBudgetary(PRE_EXP_EXTRAPRE extraBudgetary)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryRecords_Insert");

                if (extraBudgetary.EXP_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@exp_codigo", DbType.Int32, extraBudgetary.EXP_CODIGO);
                }

                Db.AddInParameter(cmd, "@extrapre_codigo", DbType.Int32, extraBudgetary.EXTRAPRE_CODIGO);
                Db.AddInParameter(cmd, "@exp_extrap_ano_presupuesto", DbType.Int32, extraBudgetary.EXP_EXTRAP_ANO_PRESUPUESTO);
                Db.AddInParameter(cmd, "@tip_extrap_codigo", DbType.Int32, extraBudgetary.TIP_EXTRAP_CODIGO);

                if (extraBudgetary.DOC_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@doc_codigo", DbType.Int32, extraBudgetary.DOC_CODIGO);
                }

                Db.AddInParameter(cmd, "@exp_extrap_fecha", DbType.DateTime, extraBudgetary.EXP_EXTRAP_FECHA);
                Db.AddInParameter(cmd, "@exp_extrap_importe", DbType.Decimal, extraBudgetary.EXP_EXTRAP_IMPORTE);

                if (extraBudgetary.PROV_CODIGO_PROVEEDOR != null)
                {
                    Db.AddInParameter(cmd, "@prov_codigo_proveedor", DbType.Int32, extraBudgetary.PROV_CODIGO_PROVEEDOR);
                }

                if (extraBudgetary.CUE_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@cue_codigo", DbType.Int32, extraBudgetary.CUE_CODIGO);
                }

                if (extraBudgetary.TIPP_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@tipp_codigo", DbType.Int32, extraBudgetary.TIPP_CODIGO);
                }

                if (extraBudgetary.FOR_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@for_codigo", DbType.Int32, extraBudgetary.FOR_CODIGO);
                }

                if (extraBudgetary.CUE_CODIGO_PAGADOR != null)
                {
                    Db.AddInParameter(cmd, "@cue_codigo_pagador", DbType.Int32, extraBudgetary.CUE_CODIGO_PAGADOR);
                }

                if (!string.IsNullOrWhiteSpace(extraBudgetary.EXP_EXTRAP_TEXTO))
                {
                    Db.AddInParameter(cmd, "@exp_extrap_texto", DbType.String, extraBudgetary.EXP_EXTRAP_TEXTO);
                }

                Db.AddInParameter(cmd, "@exp_extrap_numero", DbType.Int32, extraBudgetary.EXP_EXTRAP_NUMERO);

                if (extraBudgetary.PROV_CODIGO_TERCERO != null)
                {
                    Db.AddInParameter(cmd, "@prov_codigo_tercero", DbType.Int32, extraBudgetary.PROV_CODIGO_TERCERO);
                }

                if (extraBudgetary.HOJ_NUMERO != null)
                {
                    Db.AddInParameter(cmd, "@hoj_numero", DbType.Int32, extraBudgetary.HOJ_NUMERO);
                }

                if (extraBudgetary.HOJ_NUMERO50 != null)
                {
                    Db.AddInParameter(cmd, "@hoj_numero50", DbType.Int32, extraBudgetary.HOJ_NUMERO50);
                }

                if (extraBudgetary.ANO_HOJA != null)
                {
                    Db.AddInParameter(cmd, "@ano_hoja", DbType.Int32, extraBudgetary.ANO_HOJA);
                }

                if (extraBudgetary.ANO_HOJA50 != null)
                {
                    Db.AddInParameter(cmd, "@ano_hoja50", DbType.Int32, extraBudgetary.ANO_HOJA50);
                }

                Db.AddInParameter(cmd, "@mon_codigo", DbType.Int32, 1);
                Db.AddInParameter(cmd, "@exp_enlazado_tesoreria", DbType.Boolean, extraBudgetary.EXP_ENLAZADO_TESORERIA);

                if (extraBudgetary.TES_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@tes_codigo", DbType.Int32, extraBudgetary.TES_CODIGO);
                }

                if (extraBudgetary.EXP_EXTRAP_PAGADO != null)
                {
                    Db.AddInParameter(cmd, "@exp_extrap_pagado", DbType.Boolean, extraBudgetary.EXP_EXTRAP_PAGADO);
                }

                if (extraBudgetary.EXP_EXTRAP_NUMERO_PMP != null)
                {
                    Db.AddInParameter(cmd, "@exp_extrap_numero_pmp", DbType.Int32, extraBudgetary.EXP_EXTRAP_NUMERO_PMP);
                }

                if (!string.IsNullOrWhiteSpace(extraBudgetary.EXP_EXTRAP_NUMERO_CHEQUE))
                {
                    Db.AddInParameter(cmd, "@exp_extrap_numero_cheque", DbType.String, extraBudgetary.EXP_EXTRAP_NUMERO_CHEQUE);
                }

                if (!string.IsNullOrWhiteSpace(extraBudgetary.CUEP_NUMERO))
                {
                    Db.AddInParameter(cmd, "@cuep_numero", DbType.String, extraBudgetary.CUEP_NUMERO);
                }

                if (extraBudgetary.EXP_NUM_EXP_CONTABLE_ANUAL != null)
                {
                    Db.AddInParameter(cmd, "@exp_num_exp_contable_anual", DbType.Int32, extraBudgetary.EXP_NUM_EXP_CONTABLE_ANUAL);
                }

                if (extraBudgetary.EXP_NUM_EXP_EXTRAPRE != null)
                {
                    Db.AddInParameter(cmd, "@exp_num_exp_extrapresup", DbType.Int32, extraBudgetary.EXP_NUM_EXP_EXTRAPRE);
                }

                if (extraBudgetary.EXP_EXTRAP_CODIGO_ENLAZADO != null)
                {
                    Db.AddInParameter(cmd, "@exp_extrap_codigo_enlazado", DbType.Int32, extraBudgetary.EXP_EXTRAP_CODIGO_ENLAZADO);
                }

                Db.AddInParameter(cmd, "@codigoUsuario", DbType.Int32, extraBudgetary.USU_CODIGO);

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

        public Response UpdateExtraBudgetaryAll(PRE_EXP_EXTRAPRE extraBudgetary)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryRecords_UpdateAll");

                Db.AddInParameter(cmd, "@exp_extrap_codigo", DbType.Int32, extraBudgetary.EXP_EXTRAP_CODIGO);

                if (extraBudgetary.EXP_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@exp_codigo", DbType.Int32, extraBudgetary.EXP_CODIGO);
                }

                Db.AddInParameter(cmd, "@extrapre_codigo", DbType.Int32, extraBudgetary.EXTRAPRE_CODIGO);
                Db.AddInParameter(cmd, "@exp_extrap_ano_presupuesto", DbType.Int32, extraBudgetary.EXP_EXTRAP_ANO_PRESUPUESTO);
                Db.AddInParameter(cmd, "@tip_extrap_codigo", DbType.Int32, extraBudgetary.TIP_EXTRAP_CODIGO);

                if (extraBudgetary.DOC_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@doc_codigo", DbType.Int32, extraBudgetary.DOC_CODIGO);
                }

                Db.AddInParameter(cmd, "@exp_extrap_fecha", DbType.DateTime, extraBudgetary.EXP_EXTRAP_FECHA);
                Db.AddInParameter(cmd, "@exp_extrap_importe", DbType.Decimal, extraBudgetary.EXP_EXTRAP_IMPORTE);

                if (extraBudgetary.PROV_CODIGO_PROVEEDOR != null)
                {
                    Db.AddInParameter(cmd, "@prov_codigo_proveedor", DbType.Int32, extraBudgetary.PROV_CODIGO_PROVEEDOR);
                }

                if (extraBudgetary.CUE_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@cue_codigo", DbType.Int32, extraBudgetary.CUE_CODIGO);
                }

                if (extraBudgetary.TIPP_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@tipp_codigo", DbType.Int32, extraBudgetary.TIPP_CODIGO);
                }

                if (extraBudgetary.FOR_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@for_codigo", DbType.Int32, extraBudgetary.FOR_CODIGO);
                }

                if (extraBudgetary.CUE_CODIGO_PAGADOR != null)
                {
                    Db.AddInParameter(cmd, "@cue_codigo_pagador", DbType.Int32, extraBudgetary.CUE_CODIGO_PAGADOR);
                }

                if (!string.IsNullOrWhiteSpace(extraBudgetary.EXP_EXTRAP_TEXTO))
                {
                    Db.AddInParameter(cmd, "@exp_extrap_texto", DbType.String, extraBudgetary.EXP_EXTRAP_TEXTO);
                }

                Db.AddInParameter(cmd, "@exp_extrap_numero", DbType.Int32, extraBudgetary.EXP_EXTRAP_NUMERO);

                if (extraBudgetary.PROV_CODIGO_TERCERO != null)
                {
                    Db.AddInParameter(cmd, "@prov_codigo_tercero", DbType.Int32, extraBudgetary.PROV_CODIGO_TERCERO);
                }

                if (extraBudgetary.HOJ_NUMERO != null)
                {
                    Db.AddInParameter(cmd, "@hoj_numero", DbType.Int32, extraBudgetary.HOJ_NUMERO);
                }

                if (extraBudgetary.HOJ_NUMERO50 != null)
                {
                    Db.AddInParameter(cmd, "@hoj_numero50", DbType.Int32, extraBudgetary.HOJ_NUMERO50);
                }

                if (extraBudgetary.ANO_HOJA != null)
                {
                    Db.AddInParameter(cmd, "@ano_hoja", DbType.Int32, extraBudgetary.ANO_HOJA);
                }

                if (extraBudgetary.ANO_HOJA50 != null)
                {
                    Db.AddInParameter(cmd, "@ano_hoja50", DbType.Int32, extraBudgetary.ANO_HOJA50);
                }

                Db.AddInParameter(cmd, "@mon_codigo", DbType.Int32, 1);
                Db.AddInParameter(cmd, "@exp_enlazado_tesoreria", DbType.Boolean, extraBudgetary.EXP_ENLAZADO_TESORERIA);

                if (extraBudgetary.TES_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@tes_codigo", DbType.Int32, extraBudgetary.TES_CODIGO);
                }

                if (extraBudgetary.EXP_EXTRAP_PAGADO != null)
                {
                    Db.AddInParameter(cmd, "@exp_extrap_pagado", DbType.Boolean, extraBudgetary.EXP_EXTRAP_PAGADO);
                }

                if (extraBudgetary.EXP_EXTRAP_NUMERO_PMP != null)
                {
                    Db.AddInParameter(cmd, "@exp_extrap_numero_pmp", DbType.Int32, extraBudgetary.EXP_EXTRAP_NUMERO_PMP);
                }

                if (!string.IsNullOrWhiteSpace(extraBudgetary.EXP_EXTRAP_NUMERO_CHEQUE))
                {
                    Db.AddInParameter(cmd, "@exp_extrap_numero_cheque", DbType.String, extraBudgetary.EXP_EXTRAP_NUMERO_CHEQUE);
                }

                if (!string.IsNullOrWhiteSpace(extraBudgetary.CUEP_NUMERO))
                {
                    Db.AddInParameter(cmd, "@cuep_numero", DbType.String, extraBudgetary.CUEP_NUMERO);
                }

                if (extraBudgetary.EXP_NUM_EXP_CONTABLE_ANUAL != null)
                {
                    Db.AddInParameter(cmd, "@exp_num_exp_contable_anual", DbType.Int32, extraBudgetary.EXP_NUM_EXP_CONTABLE_ANUAL);
                }

                if (extraBudgetary.EXP_NUM_EXP_EXTRAPRE != null)
                {
                    Db.AddInParameter(cmd, "@exp_num_exp_extrapresup", DbType.Int32, extraBudgetary.EXP_NUM_EXP_EXTRAPRE);
                }

                if (extraBudgetary.EXP_EXTRAP_CODIGO_ENLAZADO != null)
                {
                    Db.AddInParameter(cmd, "@exp_extrap_codigo_enlazado", DbType.Int32, extraBudgetary.EXP_EXTRAP_CODIGO_ENLAZADO);
                }

                Db.AddInParameter(cmd, "@codigoUsuario", DbType.Int32, extraBudgetary.USU_CODIGO);

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

        public Response UpdateExtraBudgetary(PRE_EXP_EXTRAPRE extraBudgetary)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryRecords_Update");

                Db.AddInParameter(cmd, "@exp_extrap_codigo", DbType.Int32, extraBudgetary.EXP_EXTRAP_CODIGO);
                Db.AddInParameter(cmd, "@exp_codigo", DbType.Int32, extraBudgetary.EXP_CODIGO);
                Db.AddInParameter(cmd, "@extrapre_codigo", DbType.Int32, extraBudgetary.EXTRAPRE_CODIGO);
                Db.AddInParameter(cmd, "@exp_extrap_fecha", DbType.DateTime, extraBudgetary.EXP_EXTRAP_FECHA);
                Db.AddInParameter(cmd, "@exp_extrap_importe", DbType.Decimal, extraBudgetary.EXP_EXTRAP_IMPORTE);

                Db.AddInParameter(cmd, "@codigoUsuario", DbType.Int32, extraBudgetary.USU_CODIGO);

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

        public Response DeleteExtraBudgetary(int extraBudgetaryId, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryRecords_Delete");

                Db.AddInParameter(cmd, "@exp_extrap_codigo", DbType.Int32, extraBudgetaryId);
                Db.AddInParameter(cmd, "@usu_codigo", DbType.Int32, userId);

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

        public Response DeleteExtraBudgetaryDiscount(int extraBudgetaryId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryRecords_DeleteDiscount");

                Db.AddInParameter(cmd, "@exp_extrap_codigo", DbType.Int32, extraBudgetaryId);

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

        public decimal GetSumAmount(int accountingId)
        {
            try
            {
                decimal result = 0;

                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryRecords_SumAmount");

                Db.AddInParameter(cmd, "@accountingId", DbType.Int32, accountingId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = this.DbDecimal(reader["Result"]);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal GetSumBoundAmount(int extraBudgetaryId)
        {
            try
            {
                decimal result = 0;

                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryRecords_SumBoundAmount");

                Db.AddInParameter(cmd, "@extraBudgetaryId", DbType.Int32, extraBudgetaryId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = this.DbDecimal(reader["Result"]);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response UpdateRepair(PRE_DOCUMENTO_CONTABLE accountingDocument)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_ExtraBudgetaryRecords_UpdateRepair");

                Db.AddInParameter(cmd, "@extraBudgetaryCode", DbType.Int32, accountingDocument.EXP_EXTRAP_CODIGO);
                Db.AddInParameter(cmd, "@repair", DbType.Boolean, accountingDocument.Repair);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, accountingDocument.USU_CODIGO);

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

        #endregion
    }
}