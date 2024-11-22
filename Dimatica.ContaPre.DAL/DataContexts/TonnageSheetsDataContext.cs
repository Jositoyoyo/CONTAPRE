namespace Dimatica.ContaPre.DAL.DataContexts
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Dimatica.ContaPre.DAL.Models;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class TonnageSheetsDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<TonnageSheetsDataContext> Context = new Lazy<TonnageSheetsDataContext>(() => new TonnageSheetsDataContext());

        #endregion

        #region Public Properties

        public static TonnageSheetsDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public PRE_DETALLE_HOJA_ARQUEO GetDetailByFilters(int tonnageSheetCode, int? fileNumber, int? lineNumber)
        {
            try
            {
                PRE_DETALLE_HOJA_ARQUEO result = null;

                var cmd = Db.GetStoredProcCommand("USP_TonnageSheets_GetDetailByFilters");

                Db.AddInParameter(cmd, "@tonnageSheetCode", DbType.Int32, tonnageSheetCode);

                if (fileNumber != null)
                {
                    Db.AddInParameter(cmd, "@fileNumber", DbType.Int32, fileNumber);
                }

                if (lineNumber != null)
                {
                    Db.AddInParameter(cmd, "@lineNumber", DbType.Int32, lineNumber);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = new PRE_DETALLE_HOJA_ARQUEO
                        {
                            DET_CODIGO = this.DbInteger(reader["DET_CODIGO"]),
                            HOJ_CODIGO = this.DbIntegerNullable(reader["HOJ_CODIGO"]),
                            DET_FECHA_APUNTE = this.DbDateNullable(reader["DET_FECHA_APUNTE"]),
                            DET_IMPORTE = this.DbDecimal(reader["DET_IMPORTE"]),
                            DOC_CODIGO = this.DbIntegerNullable(reader["DOC_CODIGO"]),
                            EXP_EXTRAP_CODIGO = this.DbIntegerNullable(reader["EXP_EXTRAP_CODIGO"]),
                            DET_NUMERO_EXPEDIENTE = this.DbIntegerNullable(reader["DET_NUMERO_EXPEDIENTE"]),
                            EXP_ANO_PRESUPUESTO = this.DbShort(reader["EXP_ANO_PRESUPUESTO"]),
                            LIN_NUMERO = this.DbIntegerNullable(reader["LIN_NUMERO"]),
                            MON_CODIGO = this.DbByteNullable(reader["MON_CODIGO"]),
                            DET_ASIGNADO = this.DbBooleanBit(reader["DET_ASIGNADO"]),
                            DET_ASIGNADO50 = this.DbBooleanBit(reader["DET_ASIGNADO50"])
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

        public Response InsertTonnageSheet(PRE_HOJA_ARQUEO tonnageSheet)
        {
            try
            {
                if (tonnageSheet?.HOJ_CODIGO == null)
                {
                    return new Response
                    {
                        ResponseCode = ResponseCode.Invalid
                    };
                }

                var cmd = Db.GetStoredProcCommand("USP_TonnageSheets_Insert");

                Db.AddInParameter(cmd, "@year", DbType.Int32, tonnageSheet.HOJ_ANO);
                Db.AddInParameter(cmd, "@sheetNumber", DbType.Int32, tonnageSheet.HOJ_NUMERO);
                Db.AddInParameter(cmd, "@isFifty", DbType.Boolean, tonnageSheet.HOJ_ARQUEO50);
                Db.AddInParameter(cmd, "@sheetDate", DbType.DateTime, tonnageSheet.HOJ_FECHA);

                if (tonnageSheet.CUE_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@restrictedAccount", DbType.Int32, tonnageSheet.CUE_CODIGO);
                }

                Db.AddInParameter(cmd, "@userId", DbType.Int32, tonnageSheet.USU_CODIGO);

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

        public Response UpdateTonnageSheet(PRE_HOJA_ARQUEO tonnageSheet)
        {
            try
            {
                if (tonnageSheet?.HOJ_CODIGO == null)
                {
                    return new Response
                    {
                        ResponseCode = ResponseCode.Invalid
                    };
                }

                var cmd = Db.GetStoredProcCommand("USP_TonnageSheets_Update");

                Db.AddInParameter(cmd, "@id", DbType.Int32, tonnageSheet.HOJ_CODIGO);
                Db.AddInParameter(cmd, "@sheetDate", DbType.DateTime, tonnageSheet.HOJ_FECHA);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, tonnageSheet.USU_CODIGO);

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

        public Response InsertTonnageSheetDetail(PRE_DETALLE_HOJA_ARQUEO tonnageSheetDetail)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_TonnageSheets_InsertDetail");

                Db.AddInParameter(cmd, "@sheetCode", DbType.Int32, tonnageSheetDetail.HOJ_CODIGO);
                Db.AddInParameter(cmd, "@date", DbType.DateTime, tonnageSheetDetail.DET_FECHA_APUNTE);
                Db.AddInParameter(cmd, "@amount", DbType.Decimal, tonnageSheetDetail.DET_IMPORTE);

                if (tonnageSheetDetail.DOC_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@documentCode", DbType.Int32, tonnageSheetDetail.DOC_CODIGO);
                }

                if (tonnageSheetDetail.EXP_EXTRAP_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@fileCode", DbType.Int32, tonnageSheetDetail.EXP_EXTRAP_CODIGO);
                }

                Db.AddInParameter(cmd, "@fileNumber", DbType.Int32, tonnageSheetDetail.DET_NUMERO_EXPEDIENTE);
                Db.AddInParameter(cmd, "@budgetYear", DbType.Int32, tonnageSheetDetail.EXP_ANO_PRESUPUESTO);
                Db.AddInParameter(cmd, "@lineNumber", DbType.Int32, tonnageSheetDetail.LIN_NUMERO);
                Db.AddInParameter(cmd, "@coinId", DbType.Int32, 1);
                Db.AddInParameter(cmd, "@check", DbType.Boolean, tonnageSheetDetail.MARCADO);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, tonnageSheetDetail.USU_CODIGO);

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

        public Response UpdateTonnageSheetDetail(int tonnageSheetDetailCode, bool check, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_TonnageSheets_UpdateDetail");

                Db.AddInParameter(cmd, "@id", DbType.Int32, tonnageSheetDetailCode);
                Db.AddInParameter(cmd, "@check", DbType.Boolean, check);
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

        public PRE_HOJA_ARQUEO GetById(int id)
        {
            try
            {
                PRE_HOJA_ARQUEO result = null;

                var cmd = Db.GetStoredProcCommand("USP_TonnageSheets_GetById");

                Db.AddInParameter(cmd, "@id", DbType.Int32, id);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = new PRE_HOJA_ARQUEO
                        {
                            HOJ_CODIGO = id,
                            HOJ_NUMERO = this.DbIntegerNullable(reader["HOJ_NUMERO"]),
                            HOJ_ANO = this.DbShortNullable(reader["HOJ_ANO"]),
                            HOJ_FECHA = this.DbDateNullable(reader["HOJ_FECHA"]),
                            HOJ_FECHA_SICAI = this.DbDateNullable(reader["HOJ_FECHA_SICAI"]),
                            HOJ_NUMERO_SICAI = this.DbIntegerNullable(reader["HOJ_NUMERO_SICAI"]),
                            HOJ_ARQUEO50 = this.DbBooleanBit(reader["HOJ_ARQUEO50"])
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

        public List<PRE_HOJA_ARQUEO> GetRestrictedAccountStatement(int? exerciseYear, int? budgetYear, int accountId, string sinceDate, string untilDate)
        {
            try
            {
                var result = new List<PRE_HOJA_ARQUEO>();

                var cmd = Db.GetStoredProcCommand("USP_Treasuries_GetRestrictedAccountStatement");

                if (exerciseYear != null)
                {
                    Db.AddInParameter(cmd, "@exerciseYear", DbType.Int32, exerciseYear);
                }

                if (budgetYear != null)
                {
                    Db.AddInParameter(cmd, "@budgetYear", DbType.Int32, budgetYear);
                }

                Db.AddInParameter(cmd, "@accountId", DbType.Int32, accountId);

                if (!string.IsNullOrWhiteSpace(sinceDate))
                {
                    Db.AddInParameter(cmd, "@sinceDate", DbType.String, sinceDate);
                }

                if (!string.IsNullOrWhiteSpace(untilDate))
                {
                    Db.AddInParameter(cmd, "@untilDate", DbType.String, untilDate);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var account = new PRE_HOJA_ARQUEO
                        {
                            DET_CODIGO = this.DbInteger(reader["DET_CODIGO"]),
                            DET_FECHA_APUNTE = this.DbDateNullable(reader["DET_FECHA_APUNTE"]),
                            DET_NUMERO_EXPEDIENTE = this.DbIntegerNullable(reader["DET_NUMERO_EXPEDIENTE"]),
                            DOC_DESCRIPCION = this.DbString(reader["DESCRIPCION"]),
                            DET_IMPORTE = this.DbDecimal(reader["DET_IMPORTE"]),
                            HOJ_NUMERO = this.DbIntegerNullable(reader["HOJ_NUMERO"]),
                            HOJ_FECHA = this.DbDateNullable(reader["HOJ_FECHA"]),
                            HOJ_ARQUEO50 = this.DbBooleanBit(reader["HOJ_ARQUEO50"])
                        };

                        result.Add(account);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetTonnageSheetCode(int exerciseYear, int number, bool isFifty)
        {
            try
            {
                var result = -1;

                var cmd = Db.GetStoredProcCommand("USP_TonnageSheets_GetCode");

                Db.AddInParameter(cmd, "@year", DbType.Int32, exerciseYear);
                Db.AddInParameter(cmd, "@number", DbType.Int32, number);
                Db.AddInParameter(cmd, "@is50", DbType.Boolean, isFifty);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = this.DbInteger(reader["HOJ_CODIGO"]);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_HOJA_ARQUEO> GetTonnageSheetsDetails(int exerciseYear, int number, bool isFifty)
        {
            try
            {
                var aux = new List<PRE_HOJA_ARQUEO>();

                var cmd = Db.GetStoredProcCommand("USP_TonnageSheets_GetDetails");
                Db.AddInParameter(cmd, "@year", DbType.Int32, exerciseYear);
                Db.AddInParameter(cmd, "@number", DbType.Int32, number);
                Db.AddInParameter(cmd, "@is50", DbType.Boolean, isFifty);

                using (IDataReader reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var tonnageSheet = new PRE_HOJA_ARQUEO
                        {
                            DET_CODIGO = this.DbIntegerNullable(reader["DOC_CODIGO"]),
                            EXP_EXTRAP_CODIGO = this.DbIntegerNullable(reader["EXP_EXTRAP_CODIGO"]),
                            DET_FECHA_APUNTE = this.DbDate(reader["Fecha"]),
                            DET_IMPORTE = this.DbDecimal(reader["Importe"]),
                            DET_NUMERO_EXPEDIENTE = this.DbInteger(reader["NUMERO"]),
                            CUE_CODIGO = this.DbInteger(reader["CUE_CODIGO"]),
                            CUE_DESCRIPCION = this.DbString(reader["CUENTA_RESTRINGIDA"]),
                            EXP_ANO_PRESUPUESTO = this.DbInteger(reader["ANO_PRESUPUESTO"]),
                            LIN_NUMERO = this.DbIntegerNullable(reader["LINEA_TESORERIA"]),
                            MARCADO = this.DbBooleanBit(reader["MARCADO"]),
                            TIPO = this.DbString(reader["TIPO"])
                        };

                        aux.Add(tonnageSheet);
                    }
                }

                var result = aux.Where(i => i.TIPO == "HA").ToList();

                foreach (var sheet in aux.Where(i => i.TIPO != "HA"))
                {
                    if (result.Any(i => i.LIN_NUMERO.Equals(sheet.LIN_NUMERO) && i.DET_NUMERO_EXPEDIENTE.Equals(sheet.DET_NUMERO_EXPEDIENTE)))
                    {
                        continue;
                    }

                    result.Add(sheet);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_HOJA_ARQUEO> GetTonnageSheetsDetailsWithoutSheet(int exerciseYear, int number, bool isFifty)
        {
            try
            {
                var result = new List<PRE_HOJA_ARQUEO>();

                var cmd = Db.GetStoredProcCommand("USP_TonnageSheets_GetDetailsWithoutSheet");
                Db.AddInParameter(cmd, "@year", DbType.Int32, exerciseYear);
                Db.AddInParameter(cmd, "@number", DbType.Int32, number);
                Db.AddInParameter(cmd, "@is50", DbType.Boolean, isFifty);

                using (IDataReader reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var tonnageSheet = new PRE_HOJA_ARQUEO
                        {
                            DET_CODIGO = this.DbIntegerNullable(reader["DOC_CODIGO"]),
                            EXP_EXTRAP_CODIGO = this.DbIntegerNullable(reader["EXP_EXTRAP_CODIGO"]),
                            DET_FECHA_APUNTE = this.DbDate(reader["Fecha"]),
                            DET_IMPORTE = this.DbDecimal(reader["Importe"]),
                            DET_NUMERO_EXPEDIENTE = this.DbInteger(reader["NUMERO"]),
                            CUE_CODIGO = this.DbInteger(reader["CUE_CODIGO"]),
                            CUE_DESCRIPCION = this.DbString(reader["CUENTA_RESTRINGIDA"]),
                            EXP_ANO_PRESUPUESTO = this.DbInteger(reader["ANO_PRESUPUESTO"]),
                            LIN_NUMERO = this.DbIntegerNullable(reader["LINEA_TESORERIA"]),
                            MARCADO = this.DbBooleanBit(reader["MARCADO"])
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

        public List<PRE_HOJA_ARQUEO> GetByFilters(int? exerciseYear, int? accountingCode, int? sheetSinceNumber, int? sheetUntilNumber, string sheetDateSince, string sheetDateUntil, string order, string sense)
        {
            try
            {
                var result = new List<PRE_HOJA_ARQUEO>();

                var cmd = Db.GetStoredProcCommand("USP_TonnageSheets_GetByFilters");

                if (exerciseYear != null)
                {
                    Db.AddInParameter(cmd, "@year", DbType.Int32, exerciseYear);
                }

                if (accountingCode != null)
                {
                    Db.AddInParameter(cmd, "@accountingCode", DbType.Int32, accountingCode);
                }

                if (sheetSinceNumber != null)
                {
                    Db.AddInParameter(cmd, "@sinceSheetNumber", DbType.Int32, sheetSinceNumber);
                }

                if (sheetUntilNumber != null)
                {
                    Db.AddInParameter(cmd, "@untilSheetNumber", DbType.Int32, sheetUntilNumber);
                }

                if (!string.IsNullOrWhiteSpace(sheetDateSince))
                {
                    Db.AddInParameter(cmd, "@sinceDate", DbType.String, sheetDateSince);
                }

                if (!string.IsNullOrWhiteSpace(sheetDateUntil))
                {
                    Db.AddInParameter(cmd, "@untilDate", DbType.String, sheetDateUntil);
                }

                using (IDataReader reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var sheet = new PRE_HOJA_ARQUEO
                        {
                            HOJ_NUMERO = this.DbIntegerNullable(reader["HOJ_NUMERO"]),
                            HOJ_ANO = this.DbShortNullable(reader["HOJ_ANO"]),
                            CUE_ORDINAL_PERCEPTOR = this.DbInteger(reader["CUE_ORDINAL_PERCEPTOR"]),
                            CUE_DESCRIPCION = this.DbString(reader["CUE_DESCRIPCION"]),
                            HOJ_FECHA = this.DbDateNullable(reader["HOJ_FECHA"]),
                            DET_IMPORTE = this.DbDecimalNullable(reader["SUMA_IMPORTE"]),
                            HOJ_CODIGO = this.DbInteger(reader["HOJ_CODIGO"]),
                            CUE_CODIGO = this.DbIntegerNullable(reader["CUE_CODIGO"]),
                            HOJ_ARQUEO50 = this.DbBooleanBit(reader["HOJ_ARQUEO50"])
                        };

                        result.Add(sheet);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_DETALLE_HOJA_ARQUEO> GetDetailsById(int tonnageSheetId)
        {
            try
            {
                var result = new List<PRE_DETALLE_HOJA_ARQUEO>();

                var cmd = Db.GetStoredProcCommand("USP_TonnageSheets_GetDetailsById");
                Db.AddInParameter(cmd, "@tonnageSheetCode", DbType.Int32, tonnageSheetId);

                using (IDataReader reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var sheet = new PRE_DETALLE_HOJA_ARQUEO
                        {
                            DET_CODIGO = this.DbInteger(reader["DET_CODIGO"]),
                            DET_FECHA_APUNTE = this.DbDateNullable(reader["DET_FECHA_APUNTE"]),
                            LIN_NUMERO = this.DbIntegerNullable(reader["LIN_NUMERO"]),
                            DET_NUMERO_EXPEDIENTE = this.DbIntegerNullable(reader["DET_NUMERO_EXPEDIENTE"]),
                            DET_IMPORTE = this.DbDecimal(reader["DET_IMPORTE"])
                        };

                        result.Add(sheet);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response DeleteDetail(int detailId, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_TonnageSheets_DeleteDetail");

                Db.AddInParameter(cmd, "@id", DbType.Int32, detailId);
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

        public Response DeleteTonnageSheet(int id, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_TonnageSheets_Delete");

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

        #endregion
    }
}