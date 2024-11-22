namespace Dimatica.ContaPre.DAL.DataContexts
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Globalization;

    using Dimatica.ContaPre.DAL.Models;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    using Oracle.ManagedDataAccess.Client;

    #endregion

    public class TreasuriesDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<TreasuriesDataContext> Context = new Lazy<TreasuriesDataContext>(() => new TreasuriesDataContext());

        #endregion

        #region Public Properties

        public static TreasuriesDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public PRE_TESORERIA GetById(int treasuryId)
        {
            try
            {
                PRE_TESORERIA result = null;

                var cmd = Db.GetStoredProcCommand("USP_Treasuries_GetById");
                Db.AddInParameter(cmd, "@id", DbType.Int32, treasuryId);

                using (IDataReader reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = new PRE_TESORERIA
                                         {
                                                 TES_CODIGO = this.DbInteger(reader["TES_CODIGO"]),
                                                 TES_FECHA_APUNTE = this.DbDateNullable(reader["TES_FECHA_APUNTE"]),
                                                 TES_TOTAL_IMPORTE_LIQUIDO = this.DbDecimal(reader["TES_TOTAL_IMPORTE_LIQUIDO"]),
                                                 TES_ANO_PRESUPUESTO = this.DbShortNullable(reader["TES_ANO_PRESUPUESTO"]),
                                                 CUE_CODIGO = this.DbIntegerNullable(reader["CUE_CODIGO"]),
                                                 ORI_CODIGO = this.DbByteNullable(reader["ORI_CODIGO"]),
                                                 TES_DESCRIPCION = this.DbString(reader["TES_DESCRIPCION"]),
                                                 TES_NUMERO_CHEQUE = this.DbString(reader["TES_NUMERO_CHEQUE"]),
                                                 TES_FECHA_BANCO = this.DbDateNullable(reader["TES_FECHA_BANCO"]),
                                                 FOR_CODIGO = this.DbByteNullable(reader["FOR_CODIGO"]),
                                                 TIPR_CODIGO = this.DbIntegerNullable(reader["TIPR_CODIGO"]),
                                                 TES_MARCA_0_1_255 = this.DbByteNullable(reader["TES_MARCA_0_1_255"]),
                                                 TES_ANULADO = this.DbBooleanBit(reader["TES_ANULADO"]),
                                                 TES_HABER = this.DbBooleanBit(reader["TES_HABER"]),
                                                 TES_APLICACION = this.DbString(reader["TES_APLICACION"]),
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

        public List<PRE_TESORERIA> GetAllById(int treasuryId)
        {
            try
            {
                var result = new List<PRE_TESORERIA>();

                var cmd = Db.GetStoredProcCommand("USP_Treasuries_GetAllById");
                Db.AddInParameter(cmd, "@id", DbType.Int32, treasuryId);

                using (IDataReader reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var treasury = new PRE_TESORERIA
                                               {
                                                       TES_CODIGO = this.DbInteger(reader["TES_CODIGO"]),
                                                       TES_APLICACION = this.DbString(reader["TES_APLICACION"]),
                                                       TES_ANO_PRESUPUESTO = this.DbShortNullable(reader["TES_ANO_PRESUPUESTO"]),
                                                       TES_FECHA_BANCO = this.DbDateNullable(reader["TES_FECHA_BANCO"]),
                                                       TES_FECHA_APUNTE = this.DbDateNullable(reader["TES_FECHA_APUNTE"]),
                                                       TES_TOTAL_IMPORTE_LIQUIDO = this.DbDecimal(reader["TES_TOTAL_IMPORTE_LIQUIDO"]),
                                                       TES_NUMERO_CHEQUE = this.DbString(reader["TES_NUMERO_CHEQUE"]),
                                                       TES_DESCRIPCION = this.DbString(reader["TES_DESCRIPCION"])
                                               };

                        result.Add(treasury);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_TESORERIA> GetDocuments(int? documentId, int? extraBudgetaryId, int? treasuryId)
        {
            try
            {
                var result = new List<PRE_TESORERIA>();

                var cmd = Db.GetStoredProcCommand("USP_Treasuries_GetDocuments");

                if (documentId != null)
                {
                    Db.AddInParameter(cmd, "@doc_codigo", DbType.Int32, documentId);
                }

                if (extraBudgetaryId != null)
                {
                    Db.AddInParameter(cmd, "@exp_extrap_codigo", DbType.Int32, extraBudgetaryId);
                }

                if (treasuryId != null)
                {
                    Db.AddInParameter(cmd, "@tes_codigo", DbType.Int32, treasuryId);
                }

                using (IDataReader reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var treasury = new PRE_TESORERIA
                                               {
                                                       TESD_CODIGO = this.DbInteger(reader["TESD_CODIGO"]),
                                                       TESD_ANO_PRESUPUESTO = this.DbInteger(reader["TESD_ANO_PRESUPUESTO"]),
                                                       TESD_DOCUMENTO = this.DbString(reader["TESD_DOCUMENTO"]),
                                                       TESD_NUMERO_EXPEDIENTE = this.DbIntegerNullable(reader["TESD_NUMERO_EXPEDIENTE"]),
                                                       TESD_IMPORTE_LIQUIDO = this.DbDecimal(reader["TESD_IMPORTE_LIQUIDO"]),
                                                       TESD_NUMERO_CHEQUE = this.DbString(reader["TESD_NUMERO_CHEQUE"]),
                                                       TESD_DESCRIPCION = this.DbString(reader["TESD_DESCRIPCION"]),
                                                       TES_CODIGO = this.DbInteger(reader["TES_CODIGO"]),
                                                       DOC_CODIGO = this.DbIntegerNullable(reader["DOC_CODIGO"]),
                                                       TES_APLICACION = this.DbString(reader["TES_APLICACION"]),
                                                       TES_FECHA_BANCO = this.DbDateNullable(reader["TES_FECHA_BANCO"]),
                                                       TES_FECHA_APUNTE = this.DbDateNullable(reader["TES_FECHA_APUNTE"])
                                               };

                        result.Add(treasury);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_DOCUMENTO_CONTABLE> GetDocumentsToBound(int? budgetYear, decimal? amount, int? providerCode, string checkNumber, int originCode)
        {
            try
            {
                var result = new List<PRE_DOCUMENTO_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_Treasuries_GetDocumentsToBound");

                if (budgetYear != null)
                {
                    Db.AddInParameter(cmd, "@budgetYear", DbType.Int32, budgetYear);
                }

                if (amount != null)
                {
                    Db.AddInParameter(cmd, "@amount", DbType.Decimal, amount);
                }

                if (providerCode != null)
                {
                    Db.AddInParameter(cmd, "@providerCode", DbType.Int32, providerCode);
                }

                if (!string.IsNullOrWhiteSpace(checkNumber))
                {
                    Db.AddInParameter(cmd, "@checkNumber", DbType.String, checkNumber);
                }

                Db.AddInParameter(cmd, "@originCode", DbType.Int32, originCode);

                using (IDataReader reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var document = new PRE_DOCUMENTO_CONTABLE
                                               {
                                                       DOC_CODIGO = this.DbInteger(reader["CODIGO_DOCUMENTO"]),
                                                       ANO_PRESUPUESTO = this.DbInteger(reader["ANO_PRESUPUESTO"]),
                                                       NUMERO_EXPEDIENTE = this.DbIntegerNullable(reader["NUMERO_EXPEDIENTE"]),
                                                       PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                                                       DOCUMENTO_APLICACION = this.DbString(reader["DOCUMENTO_APLICACION"]),
                                                       DOC_NUMERO_CHEQUE = this.DbString(reader["NUMERO_CHEQUE"]),
                                                       DOC_ENLAZADO_TESORERIA = this.DbBooleanBit(reader["ENLAZADO_TESORERIA"]),
                                                       LIQUIDO = this.DbDecimal(reader["LIQUIDO"]),
                                                       ORIGEN = this.DbString(reader["ORIGEN"])
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

        public List<PRE_DOCUMENTO_CONTABLE> GetPaymentsRegister(int year, int? originCode, int? providerIncomes, int? providerSpends, string sinceDate, string untilDate)
        {
            try
            {
                var result = new List<PRE_DOCUMENTO_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_Treasuries_GetPaymentsRegister");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);

                if (originCode != null)
                {
                    Db.AddInParameter(cmd, "@originCode", DbType.Int32, originCode);
                }

                if (providerIncomes != null)
                {
                    Db.AddInParameter(cmd, "@providerIncomes", DbType.Int32, providerIncomes);
                }

                if (providerSpends != null)
                {
                    Db.AddInParameter(cmd, "@providerSpends", DbType.Int32, providerSpends);
                }

                if (!string.IsNullOrWhiteSpace(sinceDate))
                {
                    Db.AddInParameter(cmd, "@sinceDate", DbType.String, sinceDate);
                }

                if (!string.IsNullOrWhiteSpace(untilDate))
                {
                    Db.AddInParameter(cmd, "@untilDate", DbType.String, untilDate);
                }

                using (IDataReader reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var document = new PRE_DOCUMENTO_CONTABLE
                                               {
                                                       EXP_CODIGO = this.DbInteger(reader["CODIGO_EXPEDIENTE"]),
                                                       NUMERO_EXPEDIENTE = this.DbIntegerNullable(reader["NUMERO_EXPEDIENTE"]),
                                                       PROV_NOMBRE = this.DbString(reader["TERCERO_INTERESADO"]),
                                                       LIQUIDO = this.DbDecimal(reader["TOTAL_IMPORTE"]),
                                                       DOC_ENLAZADO_TESORERIA = this.DbBooleanBit(reader["ENLAZADO_TESORERIA"]),
                                                       CODIGO_DOCUMENTO = this.DbIntegerNullable(reader["CODIGO_DOCUMENTO"]),
                                                       CODIGO_EXP_EXTRAP = this.DbIntegerNullable(reader["CODIGO_EXP_EXTRAP"]),
                                                       DOC_NUMERO_CHEQUE = this.DbString(reader["NUMERO_CHEQUE"]),
                                                       DOC_FECHA_PROPUESTA = this.DbDateNullable(reader["FECHA_PROPUESTA"]),
                                                       PROV_CODIGO = this.DbIntegerNullable(reader["PROV_CODIGO"]),
                                                       DOCUMENTO_APLICACION = this.DbString(reader["APLICACION_DOCUMENTO"]),
                                                       ORIGEN = this.DbString(reader["ORIGEN"]),
                                                       PROCEDENCIA = this.DbString(reader["PROCEDENCIA"])
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

        public List<PRE_TESORERIA> GetTreasuryByFilters(int? exerciseYear, int? origingCode, decimal? importe, string sinceBankDate, string untilBankDate, int? payFormCode, string checkNumber, bool? treasuryHave, string description, string sinceDateEntry, string untilDateEntry, int? registerTypeCode, bool? isCanceled, bool? isBound, string pendingDate, int? sinceDocYear, int? untilDocYear, string order)
        {
            try
            {
                var result = new List<PRE_TESORERIA>();

                var cmd = Db.GetStoredProcCommand("USP_Treasuries_GetByFilters");

                if (exerciseYear != null)
                {
                    Db.AddInParameter(cmd, "@year", DbType.Int32, exerciseYear);
                }

                if (origingCode != null)
                {
                    Db.AddInParameter(cmd, "@originCode", DbType.Int32, origingCode);
                }

                if (importe != null)
                {
                    Db.AddInParameter(cmd, "@treasuryAmount", DbType.Decimal, importe);
                }

                if (!string.IsNullOrWhiteSpace(sinceBankDate))
                {
                    Db.AddInParameter(cmd, "@sinceBankDate", DbType.String, sinceBankDate);
                }

                if (!string.IsNullOrWhiteSpace(untilBankDate))
                {
                    Db.AddInParameter(cmd, "@untilBankDate", DbType.String, untilBankDate);
                }

                if (payFormCode != null)
                {
                    Db.AddInParameter(cmd, "@payFormCode", DbType.Int32, payFormCode);
                }

                if (!string.IsNullOrWhiteSpace(checkNumber))
                {
                    Db.AddInParameter(cmd, "@checkNumber", DbType.String, checkNumber);
                }

                if (treasuryHave != null)
                {
                    Db.AddInParameter(cmd, "@treasuryHave", DbType.Boolean, treasuryHave);
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    Db.AddInParameter(cmd, "@description", DbType.String, description);
                }

                if (!string.IsNullOrWhiteSpace(sinceDateEntry))
                {
                    Db.AddInParameter(cmd, "@sinceEntryDate", DbType.String, sinceDateEntry);
                }

                if (!string.IsNullOrWhiteSpace(untilDateEntry))
                {
                    Db.AddInParameter(cmd, "@untilEntryDate", DbType.String, untilDateEntry);
                }

                if (registerTypeCode != null)
                {
                    Db.AddInParameter(cmd, "@registerTypeCode", DbType.Int32, registerTypeCode);
                }

                if (isCanceled != null)
                {
                    Db.AddInParameter(cmd, "@isCanceled", DbType.Boolean, isCanceled);
                }

                if (isBound != null)
                {
                    Db.AddInParameter(cmd, "@isBound", DbType.Boolean, isBound);
                }

                if (!string.IsNullOrWhiteSpace(pendingDate))
                {
                    Db.AddInParameter(cmd, "@pendingDate", DbType.String, pendingDate);
                }

                if (sinceDocYear != null)
                {
                    Db.AddInParameter(cmd, "@sinceYear", DbType.Int32, sinceDocYear);
                }

                if (untilDocYear != null)
                {
                    Db.AddInParameter(cmd, "@untilYear", DbType.Int32, untilDocYear);
                }

                if (!string.IsNullOrWhiteSpace(order))
                {
                    Db.AddInParameter(cmd, "@order", DbType.String, order);
                }

                using (IDataReader reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var treasury = new PRE_TESORERIA
                                               {
                                                       TES_CODIGO = this.DbInteger(reader["TES_CODIGO"]),
                                                       TES_ANO_PRESUPUESTO = this.DbShort(reader["TES_ANO_PRESUPUESTO"]),
                                                       TES_FECHA_BANCO = this.DbDateNullable(reader["TES_FECHA_BANCO"]),
                                                       TES_FECHA_APUNTE = this.DbDateNullable(reader["TES_FECHA_APUNTE"]),
                                                       TES_TOTAL_IMPORTE_LIQUIDO = this.DbDecimal(reader["TES_TOTAL_IMPORTE_LIQUIDO"]),
                                                       TES_HABER = this.DbBooleanBit(reader["TES_HABER"]),
                                                       ORI_DESCRIPCION = this.DbString(reader["ORI_DESCRIPCION"]),
                                                       TES_NUMERO_CHEQUE = this.DbString(reader["TES_NUMERO_CHEQUE"]),
                                                       TES_DESCRIPCION = this.DbString(reader["TES_DESCRIPCION"]),
                                                       TES_APLICACION = this.DbString(reader["TES_APLICACION"]),
                                                       TES_MARCA_0_1_255 = this.DbByteNullable(reader["TES_MARCA_0_1_255"])
                                               };

                        result.Add(treasury);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_TESORERIA> GetAccountingBook(DateTime since, DateTime until)
        {
            try
            {
                var result = new List<PRE_TESORERIA>();

                var cmd = Db.GetStoredProcCommand("USP_Treasuries_GetAccountingBook");

                Db.AddInParameter(cmd, "@since", DbType.Date, since);
                Db.AddInParameter(cmd, "@until", DbType.Date, until);

                using (IDataReader reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var treasury = new PRE_TESORERIA
                                               {
                                                       TES_FECHA_APUNTE = this.DbDateNullable(reader["FECHA"]),
                                                       TES_NUMERO_CHEQUE = this.DbString(reader["NUMERO_CHEQUE"]),
                                                       TES_DESCRIPCION = this.DbString(reader["DETALLE"]),
                                                       TES_APLICACION = this.DbString(reader["APLICACION"]),
                                                       TES_FECHA_BANCO = this.DbDateNullable(reader["FECHA_COBRO"]),
                                                       TES_TOTAL_IMPORTE_LIQUIDO = this.DbDecimal(reader["IMPORTE"]),
                                                       TES_HABER = this.DbBooleanBit(reader["HABER"])
                                               };

                        result.Add(treasury);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_TESORERIA> GetBankStatement(DateTime since, DateTime until)
        {
            try
            {
                var result = new List<PRE_TESORERIA>();

                var cmd = Db.GetStoredProcCommand("USP_Treasuries_GetBankStatement");

                Db.AddInParameter(cmd, "@since", DbType.Date, since);
                Db.AddInParameter(cmd, "@until", DbType.Date, until);

                using (IDataReader reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var treasury = new PRE_TESORERIA
                                               {
                                                       TES_FECHA_APUNTE = this.DbDateNullable(reader["FECHA"]),
                                                       TES_NUMERO_CHEQUE = this.DbString(reader["NUMERO_CHEQUE"]),
                                                       TES_DESCRIPCION = this.DbString(reader["DETALLE"]),
                                                       TES_APLICACION = this.DbString(reader["APLICACION"]),
                                                       TES_FECHA_BANCO = this.DbDateNullable(reader["FECHA_COBRO"]),
                                                       TES_TOTAL_IMPORTE_LIQUIDO = this.DbDecimal(reader["IMPORTE"]),
                                                       TES_HABER = this.DbBooleanBit(reader["HABER"])
                                               };

                        result.Add(treasury);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_TESORERIA> GetBlockListing(DateTime register)
        {
            try
            {
                var result = new List<PRE_TESORERIA>();

                var cmd = Db.GetStoredProcCommand("USP_Treasuries_GetBlockListing");

                Db.AddInParameter(cmd, "@register", DbType.Date, register);

                using (IDataReader reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var treasury = new PRE_TESORERIA
                                               {
                                                       TES_FECHA_APUNTE = this.DbDateNullable(reader["TES_FECHA_APUNTE"]),
                                                       TES_TOTAL_IMPORTE_LIQUIDO = this.DbDecimal(reader["TES_TOTAL_IMPORTE_LIQUIDO"]),
                                                       TES_FECHA_BANCO = this.DbDateNullable(reader["TES_FECHA_BANCO"]),
                                                       TES_ANULADO = this.DbBooleanBit(reader["TES_ANULADO"]),
                                                       TES_HABER = this.DbBooleanBit(reader["TES_HABER"])
                                               };

                        result.Add(treasury);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_TESORERIA> GetPaymentRecord(DateTime since, DateTime until)
        {
            try
            {
                var result = new List<PRE_TESORERIA>();

                var cmd = Db.GetStoredProcCommand("USP_Treasuries_GetPaymentRecord");

                Db.AddInParameter(cmd, "@since", DbType.DateTime, since);
                Db.AddInParameter(cmd, "@until", DbType.DateTime, until);

                using (IDataReader reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var treasury = new PRE_TESORERIA
                                               {
                                                       TES_FECHA_APUNTE = this.DbDateNullable(reader["FECHA"]),
                                                       DOC_CODIGO = this.DbIntegerNullable(reader["CODIGO_DOCUMENTO"]),
                                                       EXP_NUM_EXP_CONTABLE_ANUAL = this.DbIntegerNullable(reader["NUMERO_DOCUMENTO"]),
                                                       TES_APLICACION = this.DbString(reader["APLICACION"]),
                                                       PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                                                       TES_TOTAL_IMPORTE_LIQUIDO = this.DbDecimal(reader["INTEGRO"]),
                                                       IRPF = this.DbDecimal(reader["IRPF"]),
                                                       SEGURIDAD_SOCIAL = this.DbDecimal(reader["SEGURIDAD_SOCIAL"]),
                                                       BOE = this.DbDecimal(reader["BOE"]),
                                                       D_PASIVOS = this.DbDecimal(reader["D_PASIVOS"]),
                                                       MUFACE = this.DbDecimal(reader["MUFACE"]),
                                                       ANTICIPO_HABERES = this.DbDecimal(reader["ANTICIPO_HABERES"]),
                                                       INTERESES_ANTICIPOS = this.DbDecimal(reader["INTERESES_ANTICIPOS"]),
                                                       TESD_IMPORTE_LIQUIDO = this.DbDecimal(reader["LIQUIDO"]),
                                                       TES_NUMERO_CHEQUE = this.DbString(reader["NUMERO_CHEQUE"])
                                               };

                        result.Add(treasury);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_DETALLE_HOJA_ARQUEO> GetTonnageSheetDetails(int tonnageSheetCode)
        {
            try
            {
                var result = new List<PRE_DETALLE_HOJA_ARQUEO>();

                var cmd = Db.GetStoredProcCommand("USP_Treasuries_GetTonnageSheetDetails");

                Db.AddInParameter(cmd, "@tonnageSheetCode", DbType.Int32, tonnageSheetCode);

                using (IDataReader reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var tonnageSheet = new PRE_DETALLE_HOJA_ARQUEO
                                                   {
                                                           ORDINAL_BANCARIO = this.DbString(reader["ORDINAL_BANCARIO"]),
                                                           DET_NUMERO_EXPEDIENTE = this.DbIntegerNullable(reader["DET_NUMERO_EXPEDIENTE"]),
                                                           DET_IMPORTE = this.DbDecimal(reader["DET_IMPORTE"]),
                                                           LIN_NUMERO = this.DbIntegerNullable(reader["LIN_NUMERO"]),
                                                           LIN_ORIGEN_DESCRIPCION = this.DbString(reader["LIN_ORIGEN_DESCRIPCION"]),
                                                           LIN_DESCRIPCION = this.DbString(reader["LIN_DESCRIPCION"])
                                                   };

                        result.Add(tonnageSheet);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetBoundDocumentsCount(int treasuryId)
        {
            try
            {
                var result = 0;

                var cmd = Db.GetStoredProcCommand("USP_Treasuries_GetBoundDocumentsCount");

                Db.AddInParameter(cmd, "@treasuryId", DbType.Int32, treasuryId);

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

        public Response InsertTreasury(PRE_TESORERIA treasury)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Treasuries_Insert");

                Db.AddInParameter(cmd, "@originCode", DbType.Int32, treasury.ORI_CODIGO);
                Db.AddInParameter(cmd, "@year", DbType.Int32, treasury.TES_ANO_PRESUPUESTO);

                if (treasury.TIPR_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@registerType", DbType.Int32, treasury.TIPR_CODIGO);
                }

                Db.AddInParameter(cmd, "@entryDate", DbType.DateTime, treasury.TES_FECHA_APUNTE);

                if (treasury.TES_FECHA_BANCO != null)
                {
                    Db.AddInParameter(cmd, "@bankDate", DbType.DateTime, treasury.TES_FECHA_BANCO);
                }

                Db.AddInParameter(cmd, "@application", DbType.String, treasury.TES_APLICACION);

                if (treasury.FOR_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@payType", DbType.Int32, treasury.FOR_CODIGO);
                }

                Db.AddInParameter(cmd, "@checkNumber", DbType.String, treasury.TES_NUMERO_CHEQUE);

                if (treasury.CUE_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@restrictedAccount", DbType.Int32, treasury.CUE_CODIGO);
                }

                Db.AddInParameter(cmd, "@amount", DbType.Decimal, treasury.TES_TOTAL_IMPORTE_LIQUIDO);
                Db.AddInParameter(cmd, "@treasuryHave", DbType.Boolean, treasury.TES_HABER);
                Db.AddInParameter(cmd, "@description", DbType.String, treasury.TES_DESCRIPCION);
                Db.AddInParameter(cmd, "@cancelled", DbType.Boolean, treasury.TES_ANULADO);
                Db.AddInParameter(cmd, "@finish", DbType.Int32, treasury.TES_MARCA_0_1_255);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, treasury.USU_CODIGO);

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

        public Response UpdateTreasury(PRE_TESORERIA treasury)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Treasuries_Update");

                Db.AddInParameter(cmd, "@id", DbType.Int32, treasury.TES_CODIGO);
                Db.AddInParameter(cmd, "@originCode", DbType.Int32, treasury.ORI_CODIGO);
                Db.AddInParameter(cmd, "@year", DbType.Int32, treasury.TES_ANO_PRESUPUESTO);

                if (treasury.TIPR_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@registerType", DbType.Int32, treasury.TIPR_CODIGO);
                }

                Db.AddInParameter(cmd, "@entryDate", DbType.DateTime, treasury.TES_FECHA_APUNTE);

                if (treasury.TES_FECHA_BANCO != null)
                {
                    Db.AddInParameter(cmd, "@bankDate", DbType.DateTime, treasury.TES_FECHA_BANCO);
                }

                Db.AddInParameter(cmd, "@application", DbType.String, treasury.TES_APLICACION);

                if (treasury.FOR_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@payType", DbType.Int32, treasury.FOR_CODIGO);
                }

                Db.AddInParameter(cmd, "@checkNumber", DbType.String, treasury.TES_NUMERO_CHEQUE);

                if (treasury.CUE_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@restrictedAccount", DbType.Int32, treasury.CUE_CODIGO);
                }

                Db.AddInParameter(cmd, "@amount", DbType.Decimal, treasury.TES_TOTAL_IMPORTE_LIQUIDO);
                Db.AddInParameter(cmd, "@treasuryHave", DbType.Boolean, treasury.TES_HABER);
                Db.AddInParameter(cmd, "@description", DbType.String, treasury.TES_DESCRIPCION);
                Db.AddInParameter(cmd, "@cancelled", DbType.Boolean, treasury.TES_ANULADO);
                Db.AddInParameter(cmd, "@finish", DbType.Int32, treasury.TES_MARCA_0_1_255);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, treasury.USU_CODIGO);

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

        public Response DeleteBankDate(int treasuryId, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Treasuries_DeleteBankDate");

                Db.AddInParameter(cmd, "@id", DbType.Int32, treasuryId);
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

        public Response DeleteTreasury(int treasuryId, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Treasuries_Delete");

                Db.AddInParameter(cmd, "@treasuryId", DbType.Int32, treasuryId);
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

        public Response DeleteDocument(int treasuryDocumentId, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Treasuries_DeleteDocument");

                Db.AddInParameter(cmd, "@treasuryDocumentId", DbType.Int32, treasuryDocumentId);
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

        public decimal GetAmount(int treasuryId)
        {
            try
            {
                decimal result = 0;

                var cmd = Db.GetStoredProcCommand("USP_Treasuries_SumAmount");

                Db.AddInParameter(cmd, "@treasuryId", DbType.Int32, treasuryId);

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

        public decimal GetDocumentsAmount(int treasuryId)
        {
            try
            {
                decimal result = 0;

                var cmd = Db.GetStoredProcCommand("USP_Treasuries_SumDocumentsAmount");

                Db.AddInParameter(cmd, "@treasuryId", DbType.Int32, treasuryId);

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

        public Response BoundDocument(PRE_TESORERIA_DOCUMENTO document)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Treasuries_BoundDocument");

                Db.AddInParameter(cmd, "@treasuryId", DbType.Int32, document.TES_CODIGO);

                if (document.DOC_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@documentId", DbType.Int32, document.DOC_CODIGO);
                }

                if (document.EXP_EXTRAP_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@extraBudgetaryId", DbType.Int32, document.EXP_EXTRAP_CODIGO);
                }

                Db.AddInParameter(cmd, "@amount", DbType.Decimal, document.TESD_IMPORTE_LIQUIDO);
                Db.AddInParameter(cmd, "@budgetYear", DbType.Int32, document.TESD_ANO_PRESUPUESTO);
                Db.AddInParameter(cmd, "@checkNumber", DbType.String, document.TESD_NUMERO_CHEQUE);
                Db.AddInParameter(cmd, "@originCode", DbType.Int32, document.ORI_CODIGO);
                Db.AddInParameter(cmd, "@document", DbType.String, document.TESD_DOCUMENTO);
                Db.AddInParameter(cmd, "@application", DbType.String, document.TESD_APLICACION);
                Db.AddInParameter(cmd, "@description", DbType.String, document.TESD_DESCRIPCION);

                if (document.TESD_NUMERO_EXPEDIENTE != null)
                {
                    Db.AddInParameter(cmd, "@fileNumber", DbType.Int32, document.TESD_NUMERO_EXPEDIENTE);
                }

                Db.AddInParameter(cmd, "@finish", DbType.Boolean, document.Finish);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, document.USU_CODIGO);

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

        public bool FoundTreasuryOracle(int treasuryId)
        {
            try
            {
                var connection = new OracleConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["oracleConnectionString"].ConnectionString;

                connection.Open();

                var cmd = connection.CreateCommand();

                var sql = $"select count(*) as Result from tcvn_ingresos_conta where tes_codigo = {treasuryId}";

                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                var result = 0;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = this.DbInteger(reader["Result"]);
                    }
                }

                connection.Close();

                return result != 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetRecognizedRightsOracle(int treasuryId)
        {
            try
            {
                var connection = new OracleConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["oracleConnectionString"].ConnectionString;

                connection.Open();

                var cmd = connection.CreateCommand();

                var sql = $"select dercodnum from vcvn_ing_dr_contabilidad where tes_codigo = {treasuryId}";

                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                var result = string.Empty;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = string.IsNullOrWhiteSpace(result) ? reader.GetString(0) : $"{result}#{reader.GetString(0)}";
                    }
                }

                connection.Close();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response InsertTreasuryOracle(PRE_TESORERIA treasury)
        {
            try
            {
                var connection = new OracleConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["oracleConnectionString"].ConnectionString;

                connection.Open();

                var cmd = connection.CreateCommand();

                var bank = treasury.TES_FECHA_BANCO == null ? "null" : $"'{((DateTime)treasury.TES_FECHA_BANCO).ToString("d", new CultureInfo("es-ES"))}'";

                var sql = "insert into tcvn_ingresos_conta (tes_ano_presupuesto, tes_fecha_banco, tes_descripcion, tes_total_importe_liquido, tes_fecha_apunte, tes_numero_cheque, tes_codigo) " + $"values ({treasury.TES_ANO_PRESUPUESTO}, {bank}, '{treasury.TES_DESCRIPCION}', {((decimal)treasury.TES_TOTAL_IMPORTE_LIQUIDO).ToString().Replace(",", ".")}, '{((DateTime)treasury.TES_FECHA_APUNTE).ToString("d", new CultureInfo("es-ES"))}', '{treasury.TES_NUMERO_CHEQUE}', {treasury.TES_CODIGO})";

                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                var insert = cmd.ExecuteNonQuery();

                connection.Close();

                var code = insert > 0 ? ResponseCode.Ok : ResponseCode.Invalid;

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

        public Response UpdateTreasuryOracle(PRE_TESORERIA treasury)
        {
            try
            {
                var connection = new OracleConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["oracleConnectionString"].ConnectionString;

                connection.Open();

                var cmd = connection.CreateCommand();

                var sql = "update tcvn_ingresos_conta set tes_codigo = tes_codigo";

                if (treasury.TES_ANO_PRESUPUESTO != null)
                {
                    sql = $"{sql}, tes_ano_presupuesto = {treasury.TES_ANO_PRESUPUESTO}";
                }

                sql = treasury.TES_FECHA_BANCO != null ? $"{sql}, tes_fecha_banco = '{((DateTime)treasury.TES_FECHA_BANCO).ToString("d", new CultureInfo("es-ES"))}'" : $"{sql}, tes_fecha_banco = null";

                if (!string.IsNullOrWhiteSpace(treasury.TES_DESCRIPCION))
                {
                    sql = $"{sql}, tes_descripcion = '{treasury.TES_DESCRIPCION}'";
                }

                if (treasury.TES_TOTAL_IMPORTE_LIQUIDO != null)
                {
                    sql = $"{sql}, tes_total_importe_liquido = {((decimal)treasury.TES_TOTAL_IMPORTE_LIQUIDO).ToString().Replace(",", ".")}";
                }

                if (treasury.TES_FECHA_BANCO != null)
                {
                    sql = $"{sql}, tes_fecha_apunte = '{((DateTime)treasury.TES_FECHA_APUNTE).ToString("d", new CultureInfo("es-ES"))}'";
                }

                if (!string.IsNullOrWhiteSpace(treasury.TES_NUMERO_CHEQUE))
                {
                    sql = $"{sql}, tes_numero_cheque = '{treasury.TES_NUMERO_CHEQUE}'";
                }

                sql = $"{sql} where tes_codigo = {treasury.TES_CODIGO}";

                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                var update = cmd.ExecuteNonQuery();

                connection.Close();

                var code = update > 0 ? ResponseCode.Ok : ResponseCode.Invalid;

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

        public Response DeleteTreasuryOracle(int treasuryId)
        {
            try
            {
                var connection = new OracleConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["oracleConnectionString"].ConnectionString;

                connection.Open();

                var cmd = connection.CreateCommand();

                var sql = $"delete from tcvn_ingresos_conta where tes_codigo = {treasuryId}";

                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                var update = cmd.ExecuteNonQuery();

                connection.Close();

                var code = update > 0 ? ResponseCode.Ok : ResponseCode.Invalid;

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

        public Response DeleteBankDateOracle(int treasuryId)
        {
            try
            {
                var connection = new OracleConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["oracleConnectionString"].ConnectionString;

                connection.Open();

                var cmd = connection.CreateCommand();

                var sql = "update tcvn_ingresos_conta set tes_codigo = tes_codigo, tes_fecha_banco = null";

                sql = $"{sql} where tes_codigo = {treasuryId}";

                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                var update = cmd.ExecuteNonQuery();

                connection.Close();

                var code = update > 0 ? ResponseCode.Ok : ResponseCode.Invalid;

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