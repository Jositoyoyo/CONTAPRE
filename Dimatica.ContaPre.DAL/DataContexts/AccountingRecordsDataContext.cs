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
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.OL.Procedures;

    #endregion

    public class AccountingRecordsDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<AccountingRecordsDataContext> Context = new Lazy<AccountingRecordsDataContext>(() => new AccountingRecordsDataContext());

        #endregion

        #region Public Properties

        public static AccountingRecordsDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<PRE_EXPEDIENTE_CONTABLE> GetSpends(int? year, int? recordNumberYear, int? provenanceCode, int? providerCode, string providerNif, bool? multiYear, string order, string orderSent)
        {
            try
            {
                var result = new List<PRE_EXPEDIENTE_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetSpends");

                if (year != null)
                {
                    Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                }

                if (recordNumberYear != null)
                {
                    Db.AddInParameter(cmd, "@recordNumberYear", DbType.Int32, recordNumberYear);
                }

                if (provenanceCode != null)
                {
                    Db.AddInParameter(cmd, "@provenanceCode", DbType.Int32, provenanceCode);
                }

                if (providerCode != null)
                {
                    Db.AddInParameter(cmd, "@providerCode", DbType.Int32, providerCode);
                }

                if (!string.IsNullOrWhiteSpace(providerNif))
                {
                    Db.AddInParameter(cmd, "@providerNif", DbType.String, providerNif);
                }

                if (multiYear != null)
                {
                    Db.AddInParameter(cmd, "@multiYear", DbType.Boolean, multiYear);
                }

                if (!string.IsNullOrWhiteSpace(order))
                {
                    Db.AddInParameter(cmd, "@order", DbType.String, order);
                }

                if (!string.IsNullOrWhiteSpace(orderSent))
                {
                    Db.AddInParameter(cmd, "@orderSent", DbType.String, orderSent);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var spend = new PRE_EXPEDIENTE_CONTABLE
                        {
                            EXP_CODIGO = this.DbInteger(reader["EXP_CODIGO"]),
                            EXP_ANO_PRESUPUESTO = this.DbShort(reader["ANO_PTO"]),
                            EXP_NUM_EXP_CONTABLE_ANUAL = this.DbIntegerNullable(reader["NUM_EC"]),
                            PROV_NOMBRE = this.DbString(reader["INTERESADO"]),
                            PROC_DESCRIPCION = this.DbString(reader["PROCEDENCIA"]),
                            EXP_PLURIANUAL = this.DbBooleanBit(reader["PLURIANUAL"]),
                            EXP_CUADRADO = this.DbBooleanBit(reader["CUADRADO"]),
                            EA_DESCRIPCION = this.DbString(reader["EA_DESCRIPCION"])
                        };

                        result.Add(spend);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetSpendsReports(int? year, int? recordNumberYear, int? provenanceCode, int? providerCode, string providerNif, bool? multiYear)
        {
            try
            {
                var result = new List<PRE_EXPEDIENTE_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetSpendsReports");

                if (year != null)
                {
                    Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                }

                if (recordNumberYear != null)
                {
                    Db.AddInParameter(cmd, "@recordNumberYear", DbType.Int32, recordNumberYear);
                }

                if (provenanceCode != null)
                {
                    Db.AddInParameter(cmd, "@provenanceCode", DbType.Int32, provenanceCode);
                }

                if (providerCode != null)
                {
                    Db.AddInParameter(cmd, "@providerCode", DbType.Int32, providerCode);
                }

                if (!string.IsNullOrWhiteSpace(providerNif))
                {
                    Db.AddInParameter(cmd, "@providerNif", DbType.String, providerNif);
                }

                if (multiYear != null)
                {
                    Db.AddInParameter(cmd, "@multiYear", DbType.Boolean, multiYear);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var spend = new PRE_EXPEDIENTE_CONTABLE
                        {
                            EXP_CODIGO = this.DbInteger(reader["EXP_CODIGO"]),
                            EXP_ANO_PRESUPUESTO = this.DbShort(reader["ANO_PTO"]),
                            EXP_NUM_EXP_CONTABLE_ANUAL = this.DbIntegerNullable(reader["NUM_EC"]),
                            PROV_NOMBRE = this.DbString(reader["INTERESADO"]),
                            PROC_DESCRIPCION = this.DbString(reader["PROCEDENCIA"]),
                            DOC_DESCRIPCION = this.DbString(reader["DESCRIPCION"]),
                            IMPORTE = this.DbDecimal(reader["IMPORTE"]),
                            FASE_RC = this.DbBooleanBit(reader["FASE_RC"]),
                            FASE_P = this.DbBooleanBit(reader["FASE_P"]),
                            TIPD_POSITIVO = this.DbBooleanBit(reader["POSITIVO"]),
                        };

                        result.Add(spend);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetSpendsWithAppAmount(int? year, int? chapterCode, int? articleCode, int? conceptCode, int? subConceptCode, decimal? sinceAmount, decimal? untilAmount, bool? isBound, bool? isSquare, int? providerCode, string providerNif, int? documentTypeCode, int? docNumber, string order, string sense)
        {
            try
            {
                var result = new List<PRE_EXPEDIENTE_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetSpendsWithAppAmount");

                if (year != null)
                {
                    Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                }

                if (chapterCode != null)
                {
                    Db.AddInParameter(cmd, "@chapterCode", DbType.Int32, chapterCode);
                }

                if (articleCode != null)
                {
                    Db.AddInParameter(cmd, "@articleCode", DbType.Int32, articleCode);
                }

                if (conceptCode != null)
                {
                    Db.AddInParameter(cmd, "@conceptCode", DbType.Int32, conceptCode);
                }

                if (subConceptCode != null)
                {
                    Db.AddInParameter(cmd, "@subConceptCode", DbType.Int32, subConceptCode);
                }

                if (sinceAmount != null)
                {
                    Db.AddInParameter(cmd, "@sinceAmount", DbType.Decimal, sinceAmount);
                }

                if (untilAmount != null)
                {
                    Db.AddInParameter(cmd, "@untilAmount", DbType.Decimal, untilAmount);
                }

                if (isBound != null)
                {
                    Db.AddInParameter(cmd, "@isBound", DbType.Boolean, isBound);
                }

                if (isSquare != null)
                {
                    Db.AddInParameter(cmd, "@isSquare", DbType.Boolean, isSquare);
                }

                if (providerCode != null)
                {
                    Db.AddInParameter(cmd, "@providerCode", DbType.Int32, providerCode);
                }

                if (!string.IsNullOrWhiteSpace(providerNif))
                {
                    Db.AddInParameter(cmd, "@providerNif", DbType.String, providerNif);
                }

                if (documentTypeCode != null)
                {
                    Db.AddInParameter(cmd, "@documentTypeCode", DbType.Int32, documentTypeCode);
                }

                if (docNumber != null)
                {
                    Db.AddInParameter(cmd, "@docNumber", DbType.Int32, docNumber);
                }

                if (!string.IsNullOrWhiteSpace(order))
                {
                    Db.AddInParameter(cmd, "@order", DbType.String, order);
                }

                if (!string.IsNullOrWhiteSpace(sense))
                {
                    Db.AddInParameter(cmd, "@sense", DbType.String, sense);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var incomes = new PRE_EXPEDIENTE_CONTABLE
                        {
                            EXP_I_G = this.DbString(reader["EXP_I_G"]),
                            EXP_CODIGO = this.DbInteger(reader["EXP_CODIGO"]),
                            EXP_NUM_EXP_CONTABLE_ANUAL = this.DbIntegerNullable(reader["EXP_NUMERO"]),
                            EXP_ANO_PRESUPUESTO = this.DbShort(reader["ANO_PTO"]),
                            EA_DESCRIPCION = this.DbString(reader["DESCRIPCION"]),
                            PROV_NOMBRE = this.DbString(reader["TERCERO"]),
                            TIPO_DOC = this.DbString(reader["TIPO_DOC"]),
                            IMPORTE = this.DbDecimal(reader["IMPORTE"]),
                            CACS_NUMERO = this.DbString(reader["CACS_NUMERO"]),
                            DOC_ENLAZADO_TESORERIA = this.DbBooleanBit(reader["ENLAZADO"]),
                            EXP_CUADRADO = this.DbBooleanBit(reader["CUADRADO"])
                        };

                        result.Add(incomes);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetIncomes(int year, string description, bool? isSquare, int? providerCode, int? operationYear, string type, int? docNumber, string order)
        {
            try
            {
                var result = new List<PRE_EXPEDIENTE_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetIncomes");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);

                if (!string.IsNullOrWhiteSpace(description))
                {
                    Db.AddInParameter(cmd, "@description", DbType.String, description);
                }

                if (isSquare != null)
                {
                    Db.AddInParameter(cmd, "@square", DbType.Boolean, isSquare);
                }

                if (providerCode != null)
                {
                    Db.AddInParameter(cmd, "@providerCode", DbType.Int32, providerCode);
                }

                if (operationYear != null)
                {
                    Db.AddInParameter(cmd, "@operationYear", DbType.Int32, operationYear);
                }

                if (!string.IsNullOrWhiteSpace(type))
                {
                    Db.AddInParameter(cmd, "@typeCode", DbType.Int32, type);
                }

                if (docNumber != null)
                {
                    Db.AddInParameter(cmd, "@docNumber", DbType.Int32, docNumber);
                }

                if (!string.IsNullOrWhiteSpace(order))
                {
                    Db.AddInParameter(cmd, "@order", DbType.String, order);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var incomes = new PRE_EXPEDIENTE_CONTABLE
                        {
                            EXP_I_G = this.DbString(reader["EXP_I_G"]),
                            EXP_CODIGO = this.DbInteger(reader["EXP_CODIGO"]),
                            EXP_NUMERO = this.DbIntegerNullable(reader["EXP_NUMERO"]),
                            DOC_AGRUPADO_DR_I = this.DbBooleanBit(reader["DOC_AGRUPADO_DR_I"]),
                            EXP_ANO_PRESUPUESTO = this.DbShort(reader["ANO_PTO"]),
                            DOC_DESCRIPCION = this.DbString(reader["DOC_DESCRIPCION"]),
                            EXP_DESCRIPCION = this.DbString(reader["EXP_DESCRIPCION"]),
                            PROV_NOMBRE = this.DbString(reader["TERCERO"]),
                            TIPO_DOC = this.DbString(reader["TIPO_DOC"]),
                            DOC_ENLAZADO_TESORERIA = this.DbBooleanBit(reader["ENLAZADO"]),
                            HOJ_NUMERO = this.DbIntegerNullable(reader["HOJ_NUMERO"]),
                            EJERCICIO = this.DbInteger(reader["EJERCICIO"]),
                            EXP_CUADRADO = this.DbBooleanBit(reader["CUADRADO"])
                        };

                        result.Add(incomes);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetIncomesWithAppAmount(int? year, string description, bool? isSquare, int? providerCode, string sinceDate, string untilDate, decimal? sinceAmount, decimal? untilAmount, int? chapterCode, int? articleCode, int? conceptCode, int? subConceptCode, bool? isBound, int? operationYear, string type, int? docNumber, string order, string sense)
        {
            try
            {
                var result = new List<PRE_EXPEDIENTE_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetIncomesWithAppAmount");

                if (year != null)
                {
                    Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    Db.AddInParameter(cmd, "@description", DbType.String, description);
                }

                if (isSquare != null)
                {
                    Db.AddInParameter(cmd, "@square", DbType.Boolean, isSquare);
                }

                if (providerCode != null)
                {
                    Db.AddInParameter(cmd, "@providerCode", DbType.Int32, providerCode);
                }

                if (!string.IsNullOrWhiteSpace(sinceDate))
                {
                    Db.AddInParameter(cmd, "@sinceDate", DbType.String, sinceDate);
                }

                if (!string.IsNullOrWhiteSpace(untilDate))
                {
                    Db.AddInParameter(cmd, "@untilDate", DbType.String, untilDate);
                }

                if (sinceAmount != null)
                {
                    Db.AddInParameter(cmd, "@sinceAmount", DbType.Decimal, sinceAmount);
                }

                if (untilAmount != null)
                {
                    Db.AddInParameter(cmd, "@untilAmount", DbType.Decimal, untilAmount);
                }

                if (chapterCode != null)
                {
                    Db.AddInParameter(cmd, "@chapterCode", DbType.Int32, chapterCode);
                }

                if (articleCode != null)
                {
                    Db.AddInParameter(cmd, "@articleCode", DbType.Int32, articleCode);
                }

                if (conceptCode != null)
                {
                    Db.AddInParameter(cmd, "@conceptCode", DbType.Int32, conceptCode);
                }

                if (subConceptCode != null)
                {
                    Db.AddInParameter(cmd, "@subConceptCode", DbType.Int32, subConceptCode);
                }

                if (isBound != null)
                {
                    Db.AddInParameter(cmd, "@isBound", DbType.Boolean, isBound);
                }

                if (operationYear != null)
                {
                    Db.AddInParameter(cmd, "@operationYear", DbType.Int32, operationYear);
                }

                if (!string.IsNullOrWhiteSpace(type))
                {
                    Db.AddInParameter(cmd, "@type", DbType.String, type);
                }

                if (docNumber != null)
                {
                    Db.AddInParameter(cmd, "@docNumber", DbType.Int32, docNumber);
                }

                if (!string.IsNullOrWhiteSpace(order))
                {
                    Db.AddInParameter(cmd, "@order", DbType.String, order);
                }

                if (!string.IsNullOrWhiteSpace(sense))
                {
                    Db.AddInParameter(cmd, "@sense", DbType.String, sense);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var incomes = new PRE_EXPEDIENTE_CONTABLE
                        {
                            EXP_I_G = this.DbString(reader["EXP_I_G"]),
                            EXP_CODIGO = this.DbInteger(reader["EXP_CODIGO"]),
                            EXP_NUMERO = this.DbIntegerNullable(reader["EXP_NUMERO"]),
                            EXP_ANO_PRESUPUESTO = this.DbShort(reader["ANO_PTO"]),
                            EXP_DESCRIPCION = this.DbString(reader["EXP_DESCRIPCION"]),
                            PROV_NOMBRE = this.DbString(reader["TERCERO"]),
                            TIPO_DOC = this.DbString(reader["TIPO_DOC"]),
                            IMPORTE = this.DbDecimal(reader["IMPORTE"]),
                            DOC_AGRUPADO_DR_I = this.DbBooleanBit(reader["DOC_AGRUPADO_DR_I"]),
                            CACS_NUMERO = this.DbString(reader["CACS_NUMERO"]),
                            DOC_ENLAZADO_TESORERIA = this.DbBooleanBit(reader["ENLAZADO"]),
                            HOJ_NUMERO = this.DbIntegerNullable(reader["HOJ_NUMERO"]),
                            EJERCICIO = this.DbInteger(reader["ANO_EJERCICIO"]),
                            EXP_CUADRADO = this.DbBooleanBit(reader["CUADRADO"])
                        };

                        result.Add(incomes);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetByAdministrativeRecord(int administrativeRecordId)
        {
            try
            {
                var result = new List<PRE_EXPEDIENTE_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetByEACode");

                Db.AddInParameter(cmd, "@eaCode", DbType.Int32, administrativeRecordId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var incomes = new PRE_EXPEDIENTE_CONTABLE
                        {
                            EXP_CODIGO = this.DbInteger(reader["EXP_CODIGO"]),
                            PRO_CODIGO = this.DbByteNullable(reader["PRO_CODIGO"]),
                            PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                            EXP_ANO_PRESUPUESTO = this.DbShort(reader["EXP_ANO_PRESUPUESTO"]),
                            EXP_NUM_EXP_CONTABLE_ANUAL = this.DbIntegerNullable(reader["EXP_NUM_EXP_CONTABLE_ANUAL"])
                        };

                        result.Add(incomes);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PRE_EXPEDIENTE_CONTABLE GetById(int id)
        {
            try
            {
                PRE_EXPEDIENTE_CONTABLE result = null;

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetById");

                Db.AddInParameter(cmd, "@id", DbType.Int32, id);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = new PRE_EXPEDIENTE_CONTABLE
                        {
                            EXP_CODIGO = id,
                            EA_CODIGO = this.DbIntegerNullable(reader["EA_CODIGO"]),
                            PRO_CODIGO = this.DbByteNullable(reader["PRO_CODIGO"]),
                            EXP_DESCRIPCION = this.DbString(reader["DESCRIPCION"]),
                            EXP_PLURIANUAL = this.DbBooleanBit(reader["EXP_PLURIANUAL"]),
                            MON_CODIGO = this.DbByteNullable(reader["MON_CODIGO"]),
                            EXP_CUADRADO = this.DbBooleanBit(reader["EXP_CUADRADO"]),
                            EXP_ANO_PRESUPUESTO = this.DbShort(reader["EXP_ANO_PRESUPUESTO"]),
                            EXP_NUM_EXP_CONTABLE_ANUAL = this.DbIntegerNullable(reader["EXP_NUM_EXP_CONTABLE_ANUAL"]),
                            PROV_CODIGO = this.DbIntegerNullable(reader["PROV_CODIGO"]),
                            PROC_CODIGO = this.DbIntegerNullable(reader["PROC_CODIGO"]),
                            CEN_CODIGO = this.DbIntegerNullable(reader["CEN_CODIGO"]),
                            CUE_CODIGO = this.DbIntegerNullable(reader["CUE_CODIGO"]),
                            EXP_FECHA_MODIFICACION = this.DbDate(reader["EXP_FECHA_MODIFICACION"]),
                            USU_CODIGO = this.DbInteger(reader["USU_CODIGO"]),
                            PRO_NUMERO = this.DbString(reader["PRO_NUMERO"]),
                            EA_NUMERO = this.DbIntegerNullable(reader["EA_NUMERO"]),
                            EA_ANO_EJERCICIO = this.DbShortNullable(reader["EA_ANO_EJERCICIO"]),
                            PROC_DESCRIPCION = this.DbString(reader["PROC_DESCRIPCION"])
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

        public List<PRE_EXPEDIENTE_CONTABLE> GetIncomesDr(int? budgetYear, int? exerciseYear, int? groupNumber, int? documentCode)
        {
            try
            {
                var result = new List<PRE_EXPEDIENTE_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetDr");

                if (budgetYear != null)
                {
                    Db.AddInParameter(cmd, "@budgetYear", DbType.Int32, budgetYear);
                }

                if (exerciseYear != null)
                {
                    Db.AddInParameter(cmd, "@exerciseYear", DbType.Int32, exerciseYear);
                }

                if (groupNumber != null)
                {
                    Db.AddInParameter(cmd, "@groupNumber", DbType.Int32, groupNumber);
                }

                if (documentCode != null)
                {
                    Db.AddInParameter(cmd, "@documentCode", DbType.Int32, documentCode);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var incomes = new PRE_EXPEDIENTE_CONTABLE();
                        incomes.TIPD_NOMBRE_CORTO = this.DbString(reader["TIPD_NOMBRE_CORTO"]);
                        incomes.PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]);
                        incomes.CACS_NUMERO = this.DbString(reader["CACS_NUMERO"]);
                        incomes.DOCA_IMPORTE = this.DbDecimal(reader["DOCA_IMPORTE"]);
                        incomes.DOC_FECHA_MOVIMIENTO_I = this.DbDateNullable(reader["DOC_FECHA_MOVIMIENTO_I"]);
                        incomes.DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]);
                        incomes.EXP_CODIGO = this.DbInteger(reader["EXP_CODIGO"]);
                        incomes.EXP_DESCRIPCION = this.DbString(reader["EXP_DESCRIPCION"]);
                        incomes.DOC_NUMERO_MOVIMIENTO_I = this.DbIntegerNullable(reader["DOC_NUMERO_MOVIMIENTO_I"]);
                        incomes.DOC_AGRUPADO_DR_I = this.DbBooleanBit(reader["DOC_AGRUPADO_DR_I"]);

                        result.Add(incomes);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<List<PRE_EXPEDIENTE_CONTABLE>, PRE_PARAMETROS> GetIncomesDrReport(int? budgetYear, int? exerciseYear, int? groupNumber, int? documentCode)
        {
            try
            {
                var result = new List<PRE_EXPEDIENTE_CONTABLE>();
                PRE_PARAMETROS parameter = null;

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetDrReport");

                if (budgetYear != null)
                {
                    Db.AddInParameter(cmd, "@budgetYear", DbType.Int32, budgetYear);
                }

                if (exerciseYear != null)
                {
                    Db.AddInParameter(cmd, "@exerciseYear", DbType.Int32, exerciseYear);
                }

                if (groupNumber != null)
                {
                    Db.AddInParameter(cmd, "@groupNumber", DbType.Int32, groupNumber);
                }

                if (documentCode != null)
                {
                    Db.AddInParameter(cmd, "@documentCode", DbType.Int32, documentCode);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var incomes = new PRE_EXPEDIENTE_CONTABLE
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            TIPD_CLAVE = this.DbIntegerNullable(reader["TIPD_CLAVE"]),
                            TIPD_NOMBRE_CORTO = this.DbString(reader["TIPD_NOMBRE_CORTO"]),
                            TIPD_POSITIVO = this.DbBooleanBit(reader["TIPD_POSITIVO"]),
                            PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                            CACS_NUMERO = this.DbString(reader["CACS_NUMERO"]),
                            DOCA_IMPORTE = this.DbDecimal(reader["DOCA_IMPORTE"]),
                            DOC_FECHA_MOVIMIENTO_I = this.DbDateNullable(reader["DOC_FECHA_MOVIMIENTO_I"]),
                            DOC_DESCRIPCION = this.DbString(reader["DOC_DESCRIPCION"]),
                            EXP_DESCRIPCION = this.DbString(reader["EXP_DESCRIPCION"]),
                            DOC_NUMERO_MOVIMIENTO_I = this.DbIntegerNullable(reader["DOC_NUMERO_MOVIMIENTO_I"]),
                            DOC_AGRUPADO_DR_I = this.DbBooleanBit(reader["DOC_AGRUPADO_DR_I"]),
                            EXP_ANO_PRESUPUESTO = this.DbShortNullable(reader["EXP_ANO_PRESUPUESTO"]),
                            DOCA_ANO_PRESUPUESTO = this.DbIntegerNullable(reader["DOCA_ANO_PRESUPUESTO"]),
                            DOCA_CODIGO = this.DbIntegerNullable(reader["DOCA_CODIGO"]),
                            DOC_FECHA_PROPUESTA = this.DbDateNullable(reader["DOC_FECHA_PROPUESTA"]),
                            DOC_FECHA_ASIENTO_DIARIO = this.DbDateNullable(reader["DOC_FECHA_ASIENTO_DIARIO"]),
                        };

                        result.Add(incomes);
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        parameter = new PRE_PARAMETROS
                        {
                            CODIGO_MINISTERIO = this.DbString(reader["CODIGO_MINISTERIO"]),
                            CODIGO_ORGANISMO = this.DbString(reader["CODIGO_ORGANISMO"]),
                            NOMBRE_ORGANISMO = this.DbString(reader["NOMBRE_ORGANISMO"]),
                        };
                    }
                }

                return new Tuple<List<PRE_EXPEDIENTE_CONTABLE>, PRE_PARAMETROS>(result, parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<PRE_DOCUMENTO_CONTABLE, List<PRE_DOCUMENTO_APLICACION>, PRE_PARAMETROS> GetSpendCertificate(int documentCode)
        {
            try
            {
                PRE_DOCUMENTO_CONTABLE document = null;
                var result = new List<PRE_DOCUMENTO_APLICACION>();
                PRE_PARAMETROS parameter = null;

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetSpendCertificate");

                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, documentCode);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        document = new PRE_DOCUMENTO_CONTABLE
                        {
                            DOC_FECHA_PROPUESTA = this.DbDateNullable(reader["DOC_FECHA_PROPUESTA"]),
                            PRO_NUMERO = this.DbString(reader["PRO_NUMERO"]),
                            EXP_NUM_EXP_CONTABLE_ANUAL = this.DbIntegerNullable(reader["EXP_NUM_EXP_CONTABLE_ANUAL"]),
                            DOC_DESCRIPCION = this.DbString(reader["DOC_DESCRIPCION"]),
                            TIPD_POSITIVO = this.DbBooleanBit(reader["TIPD_POSITIVO"]),
                        };
                    }

                    reader.NextResult();
                    
                    while (reader.Read())
                    {
                        var incomes = new PRE_DOCUMENTO_APLICACION
                        {
                                CACS_NUMERO = this.DbString(reader["CACS_NUMERO"]),
                                DOCA_IMPORTE = this.DbDecimal(reader["DOCA_IMPORTE"])
                        };

                        result.Add(incomes);
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

                return new Tuple<PRE_DOCUMENTO_CONTABLE, List<PRE_DOCUMENTO_APLICACION>, PRE_PARAMETROS>(document, result, parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetIncomesAnnexedDr(int? budgetYear, int? exerciseYear, int? groupNumber, int? documentCode)
        {
            try
            {
                var result = new List<PRE_EXPEDIENTE_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_AnnexedDr");

                if (budgetYear != null)
                {
                    Db.AddInParameter(cmd, "@budgetYear", DbType.Int32, budgetYear);
                }

                if (exerciseYear != null)
                {
                    Db.AddInParameter(cmd, "@exerciseYear", DbType.Int32, exerciseYear);
                }

                if (groupNumber != null)
                {
                    Db.AddInParameter(cmd, "@groupNumber", DbType.Int32, groupNumber);
                }

                if (documentCode != null)
                {
                    Db.AddInParameter(cmd, "@documentCode", DbType.Int32, documentCode);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var incomes = new PRE_EXPEDIENTE_CONTABLE
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            TIPD_POSITIVO = this.DbBooleanBit(reader["TIPD_POSITIVO"]),
                            PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                            CACS_NUMERO = this.DbString(reader["CACS_NUMERO"]),
                            DOCA_IMPORTE = this.DbDecimal(reader["DOCA_IMPORTE"]),
                            EXP_DESCRIPCION = this.DbString(reader["EXP_DESCRIPCION"]),
                            DOC_NUMERO_MOVIMIENTO_I = this.DbIntegerNullable(reader["DOC_NUMERO_MOVIMIENTO_I"]),
                            DOC_AGRUPADO_DR_I = this.DbBooleanBit(reader["DOC_AGRUPADO_DR_I"]),
                            DOCA_CODIGO = this.DbIntegerNullable(reader["DOCA_CODIGO"])
                        };

                        result.Add(incomes);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_FACTURA_COMPRA> GetPurchaseBills(int accountingId, int documentId)
        {
            try
            {
                var result = new List<PRE_FACTURA_COMPRA>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetPurchaseBills");

                Db.AddInParameter(cmd, "@accountingId", DbType.String, accountingId.ToString());
                Db.AddInParameter(cmd, "@documentId", DbType.String, documentId.ToString());

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var bill = new PRE_FACTURA_COMPRA
                        {
                            FA_FIRMA_RO = this.DbDateNullable(reader["fa_firma_ro"]),
                            PROV_COD_PROVEEDOR = this.DbIntegerNullable(reader["prov_cod_proveedor"]),
                            PROV_NOMBRE = this.DbString(reader["prov_nombre"]),
                            PROV_NIF = this.DbString(reader["prov_nif"]),
                            FA_IMPORTE_INTEGRO = this.DbDecimal(reader["importe"]),
                            DOC_CODIGO = this.DbInteger(reader["doc_codigo"])
                        };

                        result.Add(bill);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_PROVEEDOR> GetProviders(int accountingId)
        {
            try
            {
                var result = new List<PRE_PROVEEDOR>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetProviders");

                Db.AddInParameter(cmd, "@accountingId", DbType.String, accountingId.ToString());

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var provider = new PRE_PROVEEDOR
                        {
                            PROV_CODIGO = this.DbInteger(reader["PROV_CODIGO"]),
                            PROV_NIF = this.DbString(reader["PROV_NIF"]),
                            PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"])
                        };

                        result.Add(provider);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetMultiYears(int accountingId)
        {
            try
            {
                var result = new List<PRE_EXPEDIENTE_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetMultiYears");

                Db.AddInParameter(cmd, "@accountingId", DbType.String, accountingId.ToString());

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var document = new PRE_EXPEDIENTE_CONTABLE
                        {
                            EA_NUMERO = this.DbIntegerNullable(reader["EA_NUMERO"]),
                            EXP_ANO_PRESUPUESTO = this.DbShortNullable(reader["EXP_ANO_PRESUPUESTO"]),
                            EXP_NUM_EXP_CONTABLE_ANUAL = this.DbShortNullable(reader["EXP_NUM_EXP_CONTABLE_ANUAL"])
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

        public List<PRE_FACTURA_COMPRA> GetProvidersPurchaseBills(int accountingId, string date, int providerId)
        {
            try
            {
                var result = new List<PRE_FACTURA_COMPRA>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetProviderPurchaseBills");

                Db.AddInParameter(cmd, "@accountingId", DbType.String, accountingId.ToString());
                Db.AddInParameter(cmd, "@date", DbType.String, date);
                Db.AddInParameter(cmd, "@providerCode", DbType.Int32, providerId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var bill = new PRE_FACTURA_COMPRA
                        {
                            FA_CODIGO = this.DbInteger(reader["fa_codigo"]),
                            EA_CODIGO = this.DbIntegerNullable(reader["ea_codigo"]),
                            EXP_CODIGO = this.DbIntegerNullable(reader["exp_codigo"]),
                            ANU_COD_ANUALIDAD = this.DbIntegerNullable(reader["anu_cod_anualidad"]),
                            LOTE_COD_LOTE = this.DbIntegerNullable(reader["lote_cod_lote"]),
                            PROV_COD_PROVEEDOR = this.DbIntegerNullable(reader["prov_cod_proveedor"]),
                            FA_NUM_FACTURA = this.DbString(reader["fa_num_factura"]),
                            FA_FECHA_FACTURA = this.DbDateNullable(reader["fecha_factura"]),
                            FA_IMPORTE_INTEGRO = this.DbDecimal(reader["fa_importe_integro"]),
                            FA_IMPORTE_BOE = this.DbDecimal(reader["fa_importe_boe"]),
                            FA_IMPORTE_GARANTIA = this.DbDecimal(reader["fa_importe_garantia"]),
                            PROV_NOMBRE = this.DbString(reader["prov_nombre"]),
                            PROV_NIF = this.DbString(reader["prov_nif"]),
                            FA_BASE_IMPONIBLE = this.DbDecimal(reader["fa_base_imponible"]),
                            FA_IMPORTE_RETENCION = this.DbDecimal(reader["fa_importe_retencion"]),
                            FA_IMPORTE_IVA = this.DbDecimal(reader["fa_importe_iva"]),
                            FA_FIRMA_RO = this.DbDateNullable(reader["fa_firma_ro"]),
                            DOC_CODIGO = this.DbInteger(reader["doc_codigo"]),
                            MARCADO = this.DbBooleanBit(reader["marcado"]),
                            CODFACTURAGEI = this.DbIntegerNullable(reader["codFacturaGEI"]),
                            NCERTIFICADO = this.DbString(reader["ncertificado"]),
                            APP_PRESUP = this.DbString(reader["app_presup"])
                        };

                        result.Add(bill);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal GetDrAmount(int id)
        {
            try
            {
                decimal result = 0;

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_SumDrAmount");

                Db.AddInParameter(cmd, "@id", DbType.Int32, id);

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

        public decimal GetMiAmount(int id)
        {
            try
            {
                decimal result = 0;

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_SumMiAmount");

                Db.AddInParameter(cmd, "@id", DbType.Int32, id);

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

        public decimal GetRcAmount(int accountingId, bool isPositive, int? documentId)
        {
            try
            {
                decimal result = 0;

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_SumRcAmount");

                Db.AddInParameter(cmd, "@accountingId", DbType.Int32, accountingId);
                Db.AddInParameter(cmd, "@isPositive", DbType.Boolean, isPositive);

                if (documentId != null)
                {
                    Db.AddInParameter(cmd, "@documentId", DbType.Int32, documentId);
                }

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

        public decimal GetAdAmount(int accountingId, bool isPositive, int? documentId)
        {
            try
            {
                decimal result = 0;

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_SumAdAmount");

                Db.AddInParameter(cmd, "@accountingId", DbType.Int32, accountingId);
                Db.AddInParameter(cmd, "@isPositive", DbType.Boolean, isPositive);

                if (documentId != null)
                {
                    Db.AddInParameter(cmd, "@documentId", DbType.Int32, documentId);
                }

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

        public decimal GetOAmount(int accountingId, bool isPositive, int? documentId)
        {
            try
            {
                decimal result = 0;

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_SumOAmount");

                Db.AddInParameter(cmd, "@accountingId", DbType.Int32, accountingId);
                Db.AddInParameter(cmd, "@isPositive", DbType.Boolean, isPositive);

                if (documentId != null)
                {
                    Db.AddInParameter(cmd, "@documentId", DbType.Int32, documentId);
                }

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

        public decimal GetPAmount(int accountingId, bool isPositive, int? documentId)
        {
            try
            {
                decimal result = 0;

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_SumPAmount");

                Db.AddInParameter(cmd, "@accountingId", DbType.Int32, accountingId);
                Db.AddInParameter(cmd, "@isPositive", DbType.Boolean, isPositive);

                if (documentId != null)
                {
                    Db.AddInParameter(cmd, "@documentId", DbType.Int32, documentId);
                }

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

        public decimal GetDiscountIEcAmount(int accountingId)
        {
            try
            {
                decimal result = 0;

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_SumDiscounts_I_EC");

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

        public decimal GetApplicationsAmount(int documentId)
        {
            try
            {
                decimal result = 0;

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_SumApplicationsAmount");

                Db.AddInParameter(cmd, "@documentId", DbType.Int32, documentId);

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

        public decimal GetBillsAmount(int documentId)
        {
            try
            {
                decimal result = 0;

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_SumBillsAmount");

                Db.AddInParameter(cmd, "@documentId", DbType.Int32, documentId);

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

        public int GetLastYearNumber(int year, string type)
        {
            try
            {
                var result = 0;

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetNextYearNumber");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                Db.AddInParameter(cmd, "@type", DbType.String, type);

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

        public int GetLastDrNumber(int year)
        {
            try
            {
                var result = 0;

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetNextYearNumberDr");

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

        public List<Year> GetYears(string type)
        {
            try
            {
                var result = new List<Year>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetYears");

                Db.AddInParameter(cmd, "@type", DbType.String, type);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var year = new Year
                        {
                            Value = this.DbString(reader["EXP_ANO_PRESUPUESTO"])
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

        public Response InsertAccountingRecord(PRE_EXPEDIENTE_CONTABLE accountingRecord)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_Insert");

                if (accountingRecord.EA_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@eaCode", DbType.Int32, accountingRecord.EA_CODIGO);
                }

                Db.AddInParameter(cmd, "@type", DbType.String, accountingRecord.EXP_I_G);

                if (accountingRecord.PRO_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@programCode", DbType.Int32, accountingRecord.PRO_CODIGO);
                }

                Db.AddInParameter(cmd, "@description", DbType.String, accountingRecord.EXP_DESCRIPCION);
                Db.AddInParameter(cmd, "@multiYear", DbType.Boolean, accountingRecord.EXP_PLURIANUAL);
                Db.AddInParameter(cmd, "@coinCode", DbType.Int32, 1);
                Db.AddInParameter(cmd, "@square", DbType.Boolean, accountingRecord.EXP_CUADRADO);
                Db.AddInParameter(cmd, "@year", DbType.Int32, accountingRecord.EXP_ANO_PRESUPUESTO);

                if (accountingRecord.EXP_NUM_EXP_CONTABLE_ANUAL != null)
                {
                    Db.AddInParameter(cmd, "@recordNumberYear", DbType.Int32, accountingRecord.EXP_NUM_EXP_CONTABLE_ANUAL);
                }

                if (accountingRecord.PROV_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@providerCode", DbType.Int32, accountingRecord.PROV_CODIGO);
                }

                Db.AddInParameter(cmd, "@provenanceCode", DbType.Int32, accountingRecord.PROC_CODIGO);

                if (accountingRecord.CEN_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@costPlaceCode", DbType.Int32, accountingRecord.CEN_CODIGO);
                }

                if (accountingRecord.CUE_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@accountCode", DbType.Int32, accountingRecord.CUE_CODIGO);
                }

                if (accountingRecord.DOC_CODIGO_G_DESCUENTO_I != null)
                {
                    Db.AddInParameter(cmd, "@spendDiscountCode", DbType.Int32, accountingRecord.DOC_CODIGO_G_DESCUENTO_I);
                }

                Db.AddInParameter(cmd, "@userId", DbType.Int32, accountingRecord.USU_CODIGO);

                if (accountingRecord.DRConvenioId != null)
                {
                    Db.AddInParameter(cmd, "@covenantsCode", DbType.Int32, accountingRecord.DRConvenioId);
                }

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

        public Response UpdateAccountingRecord(PRE_EXPEDIENTE_CONTABLE accountingRecord)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_Update");

                Db.AddInParameter(cmd, "@id", DbType.String, accountingRecord.EXP_CODIGO);
                Db.AddInParameter(cmd, "@type", DbType.String, accountingRecord.EXP_I_G);

                if (accountingRecord.PRO_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@programCode", DbType.Int32, accountingRecord.PRO_CODIGO);
                }

                Db.AddInParameter(cmd, "@description", DbType.String, accountingRecord.EXP_DESCRIPCION);
                Db.AddInParameter(cmd, "@multiYear", DbType.Boolean, accountingRecord.EXP_PLURIANUAL);
                Db.AddInParameter(cmd, "@coinCode", DbType.Int32, 1);
                Db.AddInParameter(cmd, "@square", DbType.Boolean, accountingRecord.EXP_CUADRADO);
                Db.AddInParameter(cmd, "@year", DbType.Int32, accountingRecord.EXP_ANO_PRESUPUESTO);

                if (accountingRecord.EXP_NUM_EXP_CONTABLE_ANUAL != null)
                {
                    Db.AddInParameter(cmd, "@recordNumberYear", DbType.Int32, accountingRecord.EXP_NUM_EXP_CONTABLE_ANUAL);
                }

                if (accountingRecord.PROV_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@providerCode", DbType.Int32, accountingRecord.PROV_CODIGO);
                }

                Db.AddInParameter(cmd, "@provenanceCode", DbType.Int32, accountingRecord.PROC_CODIGO);
                Db.AddInParameter(cmd, "@costPlaceCode", DbType.Int32, accountingRecord.CEN_CODIGO);

                if (accountingRecord.CUE_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@accountCode", DbType.Int32, accountingRecord.CUE_CODIGO);
                }

                Db.AddInParameter(cmd, "@userId", DbType.Int32, accountingRecord.USU_CODIGO);

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
                    ResponseCode = code,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response UpdateAccountingRecordSquare(int id, bool square, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_UpdateSquare");

                Db.AddInParameter(cmd, "@id", DbType.Int32, id);
                Db.AddInParameter(cmd, "@square", DbType.Boolean, square);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, userId);

                var code = ResponseCode.Invalid;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        if (result == 1)
                        {
                            code = ResponseCode.Ok;
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

        public Response DeleteAccountingRecord(int id, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_Delete");

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

        public Response GroupDr(int groupNumber, int userId, List<int> documents)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GroupDr");

                Db.AddInParameter(cmd, "@groupNumber", DbType.Int32, groupNumber);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, userId);

                var sourceDocuments = new DataTable();
                sourceDocuments.Columns.Add("DocumentId", typeof(int));

                foreach (var document in documents)
                {
                    var dr = sourceDocuments.NewRow();
                    dr[0] = document;

                    sourceDocuments.Rows.Add(dr);
                }

                cmd.Parameters.Add(
                                   new SqlParameter
                                   {
                                       ParameterName = "@documents",
                                       Value = sourceDocuments,
                                       SqlDbType = SqlDbType.Structured
                                   });

                var code = ResponseCode.Invalid;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        if (result == 1)
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

        public List<PRE_EXPEDIENTE_CONTABLE> GetSpendsByConcept(int year, string cacsCode, DateTime? since, DateTime? until)
        {
            try
            {
                var result = new List<PRE_EXPEDIENTE_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetSpendsByConcept");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                Db.AddInParameter(cmd, "@cacsCode", DbType.String, cacsCode);

                if (since != null)
                {
                    Db.AddInParameter(cmd, "@since", DbType.DateTime, since);
                }

                if (until != null)
                {
                    Db.AddInParameter(cmd, "@until", DbType.DateTime, until);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var spend = new PRE_EXPEDIENTE_CONTABLE
                        {
                            DOC_FECHA_MOVIMIENTO_I = this.DbDate(reader["FECHA"]),
                            EXP_NUM_EXP_CONTABLE_ANUAL = this.DbIntegerNullable(reader["NUM_EXPEDIENTE"]),
                            TIPO_DOC = this.DbString(reader["TIPO_DOC"]),
                            PROV_NOMBRE = this.DbString(reader["INTERESADO"]),
                            APLICACION = this.DbString(reader["APLICACION"]),
                            MODIF_CREDITO = this.DbDecimal(reader["MODIF_CREDITO"]),
                            AUTORIZADO = this.DbDecimal(reader["AUTORIZADO"]),
                            OP = this.DbDecimal(reader["OP"]),
                            NO_VINCULANTE = this.DbBooleanBit(reader["NO_VINCULANTE"]),
                            NUMERO_APLICACION = this.DbString(reader["NUMERO_APLICACION"]),
                            NOMBRE_APLICACION = this.DbString(reader["NOMBRE_APLICACION"]),
                            IMPORTE = this.DbDecimal(reader["PRESUP_INICIAL"])
                        };

                        result.Add(spend);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetIncomesByConcept(int year, string cacsCode, string since, string until)
        {
            try
            {
                var result = new List<PRE_EXPEDIENTE_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetIncomesByConcept");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                Db.AddInParameter(cmd, "@cacsCode", DbType.String, cacsCode);

                if (!string.IsNullOrWhiteSpace(since))
                {
                    Db.AddInParameter(cmd, "@since", DbType.String, since);
                }

                if (!string.IsNullOrWhiteSpace(until))
                {
                    Db.AddInParameter(cmd, "@until", DbType.String, until);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var spend = new PRE_EXPEDIENTE_CONTABLE
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            EXP_FECHA_MODIFICACION = this.DbDate(reader["FECHA"]),
                            DOC_FECHA_MOVIMIENTO_I = this.DbDate(reader["DOC_FECHA_MOVIMIENTO_I"]),
                            DOC_NUMERO_MOVIMIENTO_I = this.DbIntegerNullable(reader["NUM_MOVIMIENTO"]),
                            TIPO_DOC = this.DbString(reader["TIPO_DOC"]),
                            PROV_NOMBRE = this.DbString(reader["TERCERO"]),
                            EXP_DESCRIPCION = this.DbString(reader["DESCRIPCION_EXPEDIENTE"]),
                            DOC_DESCRIPCION = this.DbString(reader["DESCRIPCION_DOCUMENTO"]),
                            APLICACION = this.DbString(reader["APLICACION"]),
                            Mp = this.DbDecimal(reader["MP"]),
                            DrAmount = this.DbDecimal(reader["DR"]),
                            MiAmount = this.DbDecimal(reader["MI"]),
                            NUMERO_APLICACION = this.DbString(reader["NUMERO_APLICACION"]),
                            NOMBRE_APLICACION = this.DbString(reader["NOMBRE_APLICACION"]),
                            IMPORTE = this.DbDecimal(reader["PRESUP_INICIAL"])
                        };

                        result.Add(spend);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetIncomesByConceptDrMi(int year, string cacsCode, DateTime? since, DateTime? until)
        {
            try
            {
                var result = new List<PRE_EXPEDIENTE_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetIncomesByConceptDrMi");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                Db.AddInParameter(cmd, "@cacsCode", DbType.String, cacsCode);

                if (since != null)
                {
                    Db.AddInParameter(cmd, "@since", DbType.DateTime, since);
                }

                if (until != null)
                {
                    Db.AddInParameter(cmd, "@until", DbType.DateTime, until);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var spend = new PRE_EXPEDIENTE_CONTABLE
                        {
                            PROV_NOMBRE = this.DbString(reader["TERCERO"]),
                            DOC_FECHA_MOVIMIENTO_I = this.DbDate(reader["FECHA"]),
                            DOC_NUMERO_MOVIMIENTO_I = this.DbIntegerNullable(reader["NUM_MOVIMIENTO"]),
                            TIPO_DOC = this.DbString(reader["TIPO_DOC"]),
                            DOC_DESCRIPCION = this.DbString(reader["DESCRIPCION"]),
                            DrAmount = this.DbDecimal(reader["DR"]),
                            MiAmount = this.DbDecimal(reader["MI"]),
                            NUMERO_APLICACION = this.DbString(reader["NUMERO_APLICACION"]),
                            NOMBRE_APLICACION = this.DbString(reader["NOMBRE_APLICACION"]),
                            IMPORTE = this.DbDecimal(reader["PRESUP_INICIAL"]),
                            MODIF_CREDITO = this.DbDecimal(reader["MODIFICACIONES"]),
                            AUTORIZADO = this.DbDecimal(reader["PRESUP_DEFINITIVO"])
                        };

                        result.Add(spend);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetIncomesByPlace(int year, int? place, int since, int until)
        {
            try
            {
                var result = new List<PRE_EXPEDIENTE_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetIncomesByPlace");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);

                if (place != null)
                {
                    Db.AddInParameter(cmd, "@place", DbType.Int32, place);
                }

                Db.AddInParameter(cmd, "@since", DbType.Int32, since);
                Db.AddInParameter(cmd, "@until", DbType.Int32, until);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var spend = new PRE_EXPEDIENTE_CONTABLE
                        {
                            MES = this.DbInteger(reader["MES"]),
                            DOC_FECHA_MOVIMIENTO_I = this.DbDate(reader["DOC_FECHA_MOVIMIENTO_I"]),
                            DOC_DESCRIPCION = this.DbString(reader["SEDE"]),
                            PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                            IMPORTE = this.DbDecimal(reader["DOCA_IMPORTE"]),
                            TIPO_DOC = this.DbString(reader["TIPO_DOC"]),
                            MiAmount = this.DbDecimal(reader["TIPD_FASE_MI_I"]),
                            TIPD_POSITIVO = this.DbBooleanBit(reader["TIPD_POSITIVO"])
                        };

                        result.Add(spend);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetSpendsByPlace(int year, int? place, int since, int until, string cacsCode)
        {
            try
            {
                var result = new List<PRE_EXPEDIENTE_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetSpendsByPlace");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);

                if (place != null)
                {
                    Db.AddInParameter(cmd, "@place", DbType.Int32, place);
                }

                Db.AddInParameter(cmd, "@since", DbType.Int32, since);
                Db.AddInParameter(cmd, "@until", DbType.Int32, until);

                if (!string.IsNullOrWhiteSpace(cacsCode))
                {
                    Db.AddInParameter(cmd, "@cacsCode", DbType.String, cacsCode);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var spend = new PRE_EXPEDIENTE_CONTABLE
                        {
                            MES = this.DbInteger(reader["MES"]),
                            CEN_CODIGO = this.DbIntegerNullable(reader["CEN_CODIGO"]),
                            DOC_DESCRIPCION = this.DbString(reader["SEDE"]),
                            PROV_NOMBRE = this.DbString(reader["PROCEDENCIA"]),
                            IMPORTE = this.DbDecimal(reader["DOCA_IMPORTE"]),
                            TIPD_POSITIVO = this.DbBooleanBit(reader["TIPD_POSITIVO"])
                        };

                        result.Add(spend);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetIncomesDrAgreement(DateTime since, DateTime until)
        {
            try
            {
                var result = new List<PRE_EXPEDIENTE_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetIncomesDrAgreement");

                Db.AddInParameter(cmd, "@since", DbType.Date, since);
                Db.AddInParameter(cmd, "@until", DbType.Date, until);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var income = new PRE_EXPEDIENTE_CONTABLE
                        {
                            PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                            DOC_AGRUPADO_DR_I = this.DbBooleanBit(reader["FASE_DR"]),
                            DOC_AGRUPADO_MI_I = this.DbBooleanBit(reader["FASE_MI"]),
                            TIPD_POSITIVO = this.DbBooleanBit(reader["POSITIVO"]),
                            IMPORTE = this.DbDecimal(reader["IMPORTE"])
                        };

                        if (!income.TIPD_POSITIVO)
                        {
                            income.IMPORTE *= -1;
                        }

                        var type = string.Empty;

                        if (income.DOC_AGRUPADO_DR_I && !income.DOC_AGRUPADO_MI_I)
                        {
                            type = "SOLAMENTE RECONOCIDOS";
                        }

                        if (!income.DOC_AGRUPADO_DR_I && income.DOC_AGRUPADO_MI_I)
                        {
                            type = "INGRESADOS";
                        }

                        if (income.DOC_AGRUPADO_DR_I && income.DOC_AGRUPADO_MI_I)
                        {
                            type = "RECONOCIDOS E INGRESADOS";
                        }

                        income.REPORT_TYPE = type;

                        if (result.Any(i => i.REPORT_TYPE.Equals(income.REPORT_TYPE) && i.PROV_NOMBRE.Equals(income.PROV_NOMBRE)))
                        {
                            result.FirstOrDefault(i => i.REPORT_TYPE.Equals(income.REPORT_TYPE) && i.PROV_NOMBRE.Equals(income.PROV_NOMBRE)).IMPORTE += income.IMPORTE;
                        }
                        else
                        {
                            result.Add(income);
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_EXPEDIENTE_CONTABLE> GetIncomesPendingRightsRecognized(DateTime date)
        {
            try
            {
                var result = new List<PRE_EXPEDIENTE_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_GetIncomesPendingRightsRecognized");

                Db.AddInParameter(cmd, "@until", DbType.Date, date);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var income = new PRE_EXPEDIENTE_CONTABLE
                        {
                            EXP_ANO_PRESUPUESTO = this.DbShort(reader["ANO_PRESUPUESTO"]),
                            NUMERO_APLICACION = this.DbString(reader["NUMERO_APLICACION"]),
                            APLICACION = this.DbString(reader["APLICACION"]),
                            EXP_CODIGO = this.DbInteger(reader["EXP_CODIGO"]),
                            PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                            DOC_NUMERO_MOVIMIENTO_I = this.DbIntegerNullable(reader["DOC_NUMERO_MOVIMIENTO_I"]),
                            DOC_FECHA_MOVIMIENTO_I = this.DbDateNullable(reader["DOC_FECHA_MOVIMIENTO_I"]),
                            DOCA_IMPORTE = this.DbDecimal(reader["DOCA_IMPORTE"]),
                            TIPD_CODIGO = this.DbInteger(reader["TIPD_CODIGO"]),
                            TIPD_NOMBRE_CORTO = this.DbString(reader["TIPD_NOMBRE_CORTO"]),
                            TIPD_POSITIVO = this.DbBooleanBit(reader["TIPD_POSITIVO"]),
                            TIPD_FASE_DR_I = this.DbBooleanBit(reader["TIPD_FASE_DR_I"]),
                            TIPD_FASE_MI_I = this.DbBooleanBit(reader["TIPD_FASE_MI_I"]),
                        };

                        if (!income.TIPD_POSITIVO)
                        {
                            income.DOCA_IMPORTE *= -1;
                        }

                        result.Add(income);
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