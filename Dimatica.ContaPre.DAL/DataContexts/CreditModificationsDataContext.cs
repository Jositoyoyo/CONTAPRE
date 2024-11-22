namespace Dimatica.ContaPre.DAL.DataContexts
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Data;

    using Dimatica.ContaPre.DAL.Models;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.OL.Procedures;

    #endregion

    public class CreditModificationsDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<CreditModificationsDataContext> Context = new Lazy<CreditModificationsDataContext>(() => new CreditModificationsDataContext());

        #endregion

        #region Public Properties

        public static CreditModificationsDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<Year> GetYears()
        {
            try
            {
                var result = new List<Year>();

                var cmd = Db.GetStoredProcCommand("USP_CreditModifications_GetYears");

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var year = new Year
                                           {
                                                   Value = this.DbString(reader["MOD_ANO_PRESUPUESTO"])
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

        public List<PRE_MODIFICACION_CREDITO> GetByFilters(int year, int? since, int? until)
        {
            try
            {
                var result = new List<PRE_MODIFICACION_CREDITO>();

                var cmd = Db.GetStoredProcCommand("USP_CreditModifications_GetByFilters");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                Db.AddInParameter(cmd, "@sinceOrder", DbType.Int32, since);
                Db.AddInParameter(cmd, "@untilOrder", DbType.Int32, until);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var creditModification = new PRE_MODIFICACION_CREDITO
                                                         {
                                                                 MOD_CODIGO = this.DbInteger(reader["MOD_CODIGO"]),
                                                                 MOD_ANO_PRESUPUESTO = this.DbShort(reader["MOD_ANO_PRESUPUESTO"]),
                                                                 MOD_NUMERO_ORDEN = this.DbInteger(reader["MOD_NUMERO_ORDEN"]),
                                                                 MOD_DESCRIPCION = this.DbString(reader["MOD_DESCRIPCION"]),
                                                                 MOD_EJECUTADA = this.DbBooleanBit(reader["MOD_EJECUTADA"]),
                                                                 TIPM_CODIGO_I = this.DbIntegerNullable(reader["TIPM_NUMERO_I"]),
                                                                 TIPM_CODIGO_G = this.DbIntegerNullable(reader["TIPM_NUMERO_G"]),
                                                                 TIPM_DESCRIPCION_I = this.DbString(reader["TIPM_DESCRIPCION_I"]),
                                                                 TIPM_DESCRIPCION_G = this.DbString(reader["TIPM_DESCRIPCION_G"])
                                                         };

                        result.Add(creditModification);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PRE_MODIFICACION_CREDITO GetById(int creditModificationId)
        {
            try
            {
                PRE_MODIFICACION_CREDITO result = null;

                var cmd = Db.GetStoredProcCommand("USP_CreditModifications_GetById");

                Db.AddInParameter(cmd, "@id", DbType.Int32, creditModificationId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = new PRE_MODIFICACION_CREDITO
                                         {
                                                 MOD_CODIGO = creditModificationId,
                                                 MOD_ANO_PRESUPUESTO = this.DbShort(reader["MOD_ANO_PRESUPUESTO"]),
                                                 MOD_NUMERO_ORDEN = this.DbInteger(reader["MOD_NUMERO_ORDEN"]),
                                                 EA_CODIGO = this.DbIntegerNullable(reader["EA_CODIGO"]),
                                                 EXP_CODIGO = this.DbIntegerNullable(reader["EXP_CODIGO"]),
                                                 MON_CODIGO = this.DbByteNullable(reader["MON_CODIGO"]),
                                                 MOD_FECHA_PROPUESTA = this.DbDate(reader["MOD_FECHA_PROPUESTA"]),
                                                 MOD_FECHA_ASIENTO_DIARIO = this.DbDateNullable(reader["MOD_FECHA_ASIENTO_DIARIO"]),
                                                 MOD_DESCRIPCION = this.DbString(reader["MOD_DESCRIPCION"]),
                                                 MOD_EJECUTADA = this.DbBooleanBit(reader["MOD_EJECUTADA"]),
                                                 MOD_CREADO_EXPEDIENTE = this.DbBooleanBit(reader["MOD_CREADO_EXPEDIENTE"]),
                                                 TIPM_CODIGO_I = this.DbIntegerNullable(reader["TIPM_CODIGO_I"]),
                                                 TIPM_CODIGO_G = this.DbIntegerNullable(reader["TIPM_CODIGO_G"]),
                                                 MOD_FECHA_MODIFICACION = this.DbDate(reader["MOD_FECHA_MODIFICACION"])
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

        public int GetNextOrderByYear(int year)
        {
            try
            {
                var result = 1;

                var cmd = Db.GetStoredProcCommand("USP_CreditModifications_GetNextOrderByYear");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = this.DbInteger(reader["NextOrder"]);

                        break;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response InsertCreditModification(PRE_MODIFICACION_CREDITO creditModification)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_CreditModifications_Insert");

                Db.AddInParameter(cmd, "@year", DbType.Int32, creditModification.MOD_ANO_PRESUPUESTO);
                Db.AddInParameter(cmd, "@order", DbType.Int32, creditModification.MOD_NUMERO_ORDEN);
                Db.AddInParameter(cmd, "@coinId", DbType.Int32, 1);
                Db.AddInParameter(cmd, "@proposalDate", DbType.DateTime, creditModification.MOD_FECHA_PROPUESTA);
                Db.AddInParameter(cmd, "@efectiveDate", DbType.DateTime, creditModification.MOD_FECHA_ASIENTO_DIARIO);
                Db.AddInParameter(cmd, "@description", DbType.String, creditModification.MOD_DESCRIPCION);
                Db.AddInParameter(cmd, "@execute", DbType.Boolean, false);
                Db.AddInParameter(cmd, "@fileCreated", DbType.Boolean, false);
                Db.AddInParameter(cmd, "@modificationTypeIncomeId", DbType.Int32, creditModification.TIPM_CODIGO_I);
                Db.AddInParameter(cmd, "@modificationTypeSpendId", DbType.Int32, creditModification.TIPM_CODIGO_G);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, creditModification.USU_CODIGO);

                var code = ResponseCode.Invalid;
                var id = 0;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        if (result == 0)
                        {
                            code = ResponseCode.Found;
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

        public Response UpdateCreditModification(PRE_MODIFICACION_CREDITO creditModification)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_CreditModifications_Update");

                Db.AddInParameter(cmd, "@id", DbType.Int32, creditModification.MOD_CODIGO);
                Db.AddInParameter(cmd, "@year", DbType.Int32, creditModification.MOD_ANO_PRESUPUESTO);
                Db.AddInParameter(cmd, "@order", DbType.Int32, creditModification.MOD_NUMERO_ORDEN);
                Db.AddInParameter(cmd, "@coinId", DbType.Int32, 1);
                Db.AddInParameter(cmd, "@proposalDate", DbType.DateTime, creditModification.MOD_FECHA_PROPUESTA);
                Db.AddInParameter(cmd, "@efectiveDate", DbType.DateTime, creditModification.MOD_FECHA_ASIENTO_DIARIO);
                Db.AddInParameter(cmd, "@description", DbType.String, creditModification.MOD_DESCRIPCION);
                Db.AddInParameter(cmd, "@execute", DbType.Boolean, false);
                Db.AddInParameter(cmd, "@fileCreated", DbType.Boolean, false);
                Db.AddInParameter(cmd, "@modificationTypeIncomeId", DbType.Int32, creditModification.TIPM_CODIGO_I);
                Db.AddInParameter(cmd, "@modificationTypeSpendId", DbType.Int32, creditModification.TIPM_CODIGO_G);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, creditModification.USU_CODIGO);

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

        public Response GenerateRecordsCreditModification(PRE_MODIFICACION_CREDITO creditModification)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_CreditModifications_GenerateRecords");

                Db.AddInParameter(cmd, "@id", DbType.Int32, creditModification.MOD_CODIGO);
                Db.AddInParameter(cmd, "@year", DbType.Int32, creditModification.MOD_ANO_PRESUPUESTO);
                Db.AddInParameter(cmd, "@coinId", DbType.Int32, 1);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, creditModification.USU_CODIGO);

                var code = ResponseCode.Invalid;
                var message = string.Empty;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        switch (result)
                        {
                            case 0:
                                code = ResponseCode.Invalid;
                                message = "ha ocurrido un error inesperado.";

                                break;
                            case 1:
                                code = ResponseCode.Ok;

                                break;
                            case 2:
                                code = ResponseCode.Invalid;
                                message = "el centro de coste en cuestión no ha sido encontrado.";

                                break;
                            case 3:
                                code = ResponseCode.Invalid;
                                message = "la cuenta restringida de ingreso no ha sido encontrada.";

                                break;
                            case 4:
                                code = ResponseCode.Invalid;
                                message = "la cuenta restringida de gasto no ha sido encontrada.";

                                break;
                            case 5:
                                code = ResponseCode.Invalid;
                                message = "la procendencia del expediente en cuestión no ha sido encontrada.";

                                break;
                            case 6:
                                code = ResponseCode.Invalid;
                                message = "no se ha podido insertar el expediente administrativo.";

                                break;
                            case 7:
                                code = ResponseCode.Invalid;
                                message = "la procendencia de gasto no ha sido encontrada.";

                                break;
                            case 8:
                                code = ResponseCode.Invalid;
                                message = "el tipo de documento de ingreso positivo no ha sido encontrado.";

                                break;
                            case 9:
                                code = ResponseCode.Invalid;
                                message = "el tipo de documento de ingreso negativo no ha sido encontrado.";

                                break;
                            case 10:
                                code = ResponseCode.Invalid;
                                message = "el tipo de documento de gasto positivo no ha sido encontrado.";

                                break;
                            case 11:
                                code = ResponseCode.Invalid;
                                message = "el tipo de documento de gasto negativo no ha sido encontrado.";

                                break;
                        }
                    }
                }

                return new Response
                               {
                                       ResponseCode = code,
                                       ResponseMethod = message
                               };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response ExecuteCreditModification(int creditModificationId, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_CreditModifications_Execute");

                Db.AddInParameter(cmd, "@id", DbType.Int32, creditModificationId);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, userId);

                var code = ResponseCode.Invalid;
                var value = 0;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        if (result == 1)
                        {
                            code = ResponseCode.Ok;
                        }

                        value = result;
                    }
                }

                return new Response
                               {
                                       ResponseCode = code,
                                       ResponseMethod = value
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