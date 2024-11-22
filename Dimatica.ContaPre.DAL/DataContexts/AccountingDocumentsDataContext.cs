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

    public class AccountingDocumentsDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<AccountingDocumentsDataContext> Context = new Lazy<AccountingDocumentsDataContext>(() => new AccountingDocumentsDataContext());

        #endregion

        #region Public Properties

        public static AccountingDocumentsDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods



        public PRE_DOCUMENTO_CONTABLE GetById(int id)
        {
            try
            {
                PRE_DOCUMENTO_CONTABLE result = null;

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetById");

                Db.AddInParameter(cmd, "@id", DbType.Int32, id);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = new PRE_DOCUMENTO_CONTABLE
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            EXP_CODIGO = this.DbIntegerNullable(reader["EXP_CODIGO"]),
                            DOC_I_G = this.DbString(reader["DOC_I_G"]),
                            DOC_FECHA_PROPUESTA = this.DbDateNullable(reader["DOC_FECHA_PROPUESTA"]),
                            DOC_FECHA_ASIENTO_DIARIO = this.DbDateNullable(reader["DOC_FECHA_ASIENTO_DIARIO"]),
                            DOC_NUMERO_CHEQUE = this.DbString(reader["DOC_NUMERO_CHEQUE"]),
                            DOC_DESCRIPCION = this.DbString(reader["DOC_DESCRIPCION"]),
                            DOC_NUMERO_MOVIMIENTO_I = this.DbIntegerNullable(reader["DOC_NUMERO_MOVIMIENTO_I"]),
                            DOC_FECHA_MOVIMIENTO_I = this.DbDateNullable(reader["DOC_FECHA_MOVIMIENTO_I"]),
                            TIPD_CODIGO = this.DbIntegerNullable(reader["TIPD_CODIGO"]),
                            FOR_CODIGO = this.DbByteNullable(reader["FOR_CODIGO"]),
                            CUE_CODIGO = this.DbIntegerNullable(reader["CUE_CODIGO"]),
                            TIPP_CODIGO = this.DbByteNullable(reader["TIPP_CODIGO"]),
                            DOC_ENLAZADO_TESORERIA = this.DbBooleanBit(reader["DOC_ENLAZADO_TESORERIA"]),
                            TES_CODIGO = this.DbIntegerNullable(reader["TES_CODIGO"]),
                            HOJ_NUMERO = this.DbIntegerNullable(reader["HOJ_NUMERO"]),
                            HOJ_NUMERO50 = this.DbIntegerNullable(reader["HOJ_NUMERO50"]),
                            ANO_HOJA = this.DbShortNullable(reader["ANO_HOJA"]),
                            ANO_HOJA50 = this.DbByteNullable(reader["ANO_HOJA50"]),
                            DOC_FECHA_MODIFICACION = this.DbDateNullable(reader["DOC_FECHA_MODIFICACION"]),
                            USU_CODIGO = this.DbInteger(reader["USU_CODIGO"]),
                            SEN_CODIGO = this.DbIntegerNullable(reader["SEN_CODIGO"]),
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

        public Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> GetByIdReport(int id)
        {
            try
            {
                PRE_DOCUMENTO_CONTABLE accountinDocument = null;
                PRE_PARAMETROS parameter = null;
                var applications = new List<PRE_DOCUMENTO_APLICACION>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetByIdReport");

                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, id);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        accountinDocument = new PRE_DOCUMENTO_CONTABLE
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            DOC_FECHA_PROPUESTA = this.DbDateNullable(reader["DOC_FECHA_PROPUESTA"]),
                            DOC_FECHA_ASIENTO_DIARIO = this.DbDateNullable(reader["DOC_FECHA_ASIENTO_DIARIO"]),
                            DOC_NUMERO_CHEQUE = this.DbString(reader["DOC_NUMERO_CHEQUE"]),
                            DOC_DESCRIPCION = this.DbString(reader["DOC_DESCRIPCION"]),
                            DOC_NUMERO_MOVIMIENTO_I = this.DbIntegerNullable(reader["DOC_NUMERO_MOVIMIENTO_I"]),
                            DOC_FECHA_MOVIMIENTO_I = this.DbDateNullable(reader["DOC_FECHA_MOVIMIENTO_I"]),
                            CUE_ORDINAL_PERCEPTOR = this.DbIntegerNullable(reader["CUE_ORDINAL_PERCEPTOR"]),
                            TIPP_CODIGO = this.DbByteNullable(reader["TIPP_CODIGO"]),
                            FOR_CODIGO = this.DbByteNullable(reader["FOR_CODIGO"]),
                            TIPD_POSITIVO = this.DbBooleanBit(reader["TIPD_POSITIVO"]),
                            TIPD_CLAVE = this.DbIntegerNullable(reader["TIPD_CLAVE"]),
                            TIPD_NOMBRE_CORTO = this.DbString(reader["TIPD_NOMBRE_CORTO"]),
                            CEN_CODIGO = this.DbIntegerNullable(reader["CEN_CODIGO"]),
                            CEN_DESCRIPCION = this.DbString(reader["CEN_DESCRIPCION"]),
                            CEN_AREA_ORIGEN = this.DbString(reader["CEN_AREA_ORIGEN"]),
                            PROV_NIF = this.DbString(reader["PROV_NIF"]),
                            PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                            PROV_DIRECCION = this.DbString(reader["PROV_DIRECCION"]),
                            PROV_POBLACION = this.DbString(reader["PROV_POBLACION"]),
                            PROV_CODIGO_POSTAL = this.DbString(reader["PROV_CODIGO_POSTAL"]),
                            HOJ_NUMERO = this.DbIntegerNullable(reader["HOJ_NUMERO"]),
                            PRO_NUMERO = this.DbString(reader["PRO_NUMERO"]),
                            SEN_NUMERO = this.DbIntegerNullable(reader["SEN_NUMERO"]),
                            EXP_ANO_PRESUPUESTO = this.DbShortNullable(reader["EXP_ANO_PRESUPUESTO"])
                        };
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        var application = new PRE_DOCUMENTO_APLICACION
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            CACS_NUMERO = this.DbString(reader["CACS_NUMERO"]),
                            CUEP_NUMERO = this.DbString(reader["CUEP_NUMERO"]),
                            DOCA_IMPORTE = this.DbDecimal(reader["DOCA_IMPORTE"]),
                            DOCA_ANO_PRESUPUESTO = this.DbShortNullable(reader["DOCA_ANO_PRESUPUESTO"])
                        };

                        applications.Add(application);
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

                return new Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>>(accountinDocument, parameter, applications);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> GetSpendRcById(int id)
        {
            try
            {
                PRE_DOCUMENTO_CONTABLE accountinDocument = null;
                PRE_PARAMETROS parameter = null;
                var applications = new List<PRE_DOCUMENTO_APLICACION>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetSpendRCById");

                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, id);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        accountinDocument = new PRE_DOCUMENTO_CONTABLE
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            DOC_FECHA_PROPUESTA = this.DbDateNullable(reader["DOC_FECHA_PROPUESTA"]),
                            DOC_FECHA_ASIENTO_DIARIO = this.DbDateNullable(reader["DOC_FECHA_ASIENTO_DIARIO"]),
                            DOC_DESCRIPCION = this.DbString(reader["DOC_DESCRIPCION"]),
                            EXP_NUM_EXP_CONTABLE_ANUAL = this.DbIntegerNullable(reader["EXP_NUM_EXP_CONTABLE_ANUAL"]),
                            TIPD_POSITIVO = this.DbBooleanBit(reader["TIPD_POSITIVO"]),
                            TIPD_CLAVE = this.DbIntegerNullable(reader["TIPD_CLAVE"]),
                            TIPD_NOMBRE_CORTO = this.DbString(reader["TIPD_NOMBRE_CORTO"]),
                            TIPD_DESCRIPCION = this.DbString(reader["TIPD_DESCRIPCION"]),
                            TIPD_FASE_RC_G = this.DbBooleanBit(reader["TIPD_FASE_RC_G"]),
                            TIPD_FASE_AD_G = this.DbBooleanBit(reader["TIPD_FASE_AD_G"]),
                            TIPD_FASE_O_G = this.DbBooleanBit(reader["TIPD_FASE_O_G"]),
                            TIPD_FASE_P_G = this.DbBooleanBit(reader["TIPD_FASE_P_G"]),
                            EXP_ANO_PRESUPUESTO = this.DbShortNullable(reader["EXP_ANO_PRESUPUESTO"]),
                            PRO_NUMERO = this.DbString(reader["PRO_NUMERO"])
                        };
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        var application = new PRE_DOCUMENTO_APLICACION
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            CACS_NUMERO = this.DbString(reader["CACS_NUMERO"]),
                            CUEP_NUMERO = this.DbString(reader["CUEP_NUMERO"]),
                            DOCA_IMPORTE = this.DbDecimal(reader["DOCA_IMPORTE"])
                        };

                        applications.Add(application);
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

                return new Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>>(accountinDocument, parameter, applications);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> GetSpendAdById(int id)
        {
            try
            {
                PRE_DOCUMENTO_CONTABLE accountinDocument = null;
                PRE_PARAMETROS parameter = null;
                var applications = new List<PRE_DOCUMENTO_APLICACION>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetSpendADById");

                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, id);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        accountinDocument = new PRE_DOCUMENTO_CONTABLE
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            DOC_FECHA_PROPUESTA = this.DbDateNullable(reader["DOC_FECHA_PROPUESTA"]),
                            DOC_FECHA_ASIENTO_DIARIO = this.DbDateNullable(reader["DOC_FECHA_ASIENTO_DIARIO"]),
                            DOC_DESCRIPCION = this.DbString(reader["DOC_DESCRIPCION"]),
                            EXP_NUM_EXP_CONTABLE_ANUAL = this.DbIntegerNullable(reader["EXP_NUM_EXP_CONTABLE_ANUAL"]),
                            TIPD_POSITIVO = this.DbBooleanBit(reader["TIPD_POSITIVO"]),
                            TIPD_CLAVE = this.DbIntegerNullable(reader["TIPD_CLAVE"]),
                            TIPD_NOMBRE_CORTO = this.DbString(reader["TIPD_NOMBRE_CORTO"]),
                            TIPD_DESCRIPCION = this.DbString(reader["TIPD_DESCRIPCION"]),
                            TIPD_FASE_RC_G = this.DbBooleanBit(reader["TIPD_FASE_RC_G"]),
                            TIPD_FASE_AD_G = this.DbBooleanBit(reader["TIPD_FASE_AD_G"]),
                            TIPD_FASE_O_G = this.DbBooleanBit(reader["TIPD_FASE_O_G"]),
                            TIPD_FASE_P_G = this.DbBooleanBit(reader["TIPD_FASE_P_G"]),
                            EXP_ANO_PRESUPUESTO = this.DbShortNullable(reader["EXP_ANO_PRESUPUESTO"]),
                            PRO_NUMERO = this.DbString(reader["PRO_NUMERO"]),
                            PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                            PROV_NIF = this.DbString(reader["PROV_NIF"])
                        };
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        var application = new PRE_DOCUMENTO_APLICACION
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            CACS_NUMERO = this.DbString(reader["CACS_NUMERO"]),
                            CUEP_NUMERO = this.DbString(reader["CUEP_NUMERO"]),
                            DOCA_IMPORTE = this.DbDecimal(reader["DOCA_IMPORTE"])
                        };

                        applications.Add(application);
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

                return new Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>>(accountinDocument, parameter, applications);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> GetSpendOById(int id)
        {
            try
            {
                PRE_DOCUMENTO_CONTABLE accountinDocument = null;
                PRE_PARAMETROS parameter = null;
                var applications = new List<PRE_DOCUMENTO_APLICACION>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetSpendOById");

                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, id);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        accountinDocument = new PRE_DOCUMENTO_CONTABLE
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            DOC_FECHA_PROPUESTA = this.DbDateNullable(reader["DOC_FECHA_PROPUESTA"]),
                            DOC_FECHA_ASIENTO_DIARIO = this.DbDateNullable(reader["DOC_FECHA_ASIENTO_DIARIO"]),
                            DOC_DESCRIPCION = this.DbString(reader["DOC_DESCRIPCION"]),
                            EXP_NUM_EXP_CONTABLE_ANUAL = this.DbIntegerNullable(reader["EXP_NUM_EXP_CONTABLE_ANUAL"]),
                            CUE_ORDINAL_PERCEPTOR = this.DbIntegerNullable(reader["CUE_ORDINAL_PERCEPTOR"]),
                            TIPP_CODIGO = this.DbByteNullable(reader["TIPP_CODIGO"]),
                            FOR_CODIGO = this.DbByteNullable(reader["FOR_CODIGO"]),
                            TIPD_POSITIVO = this.DbBooleanBit(reader["TIPD_POSITIVO"]),
                            TIPD_CLAVE = this.DbIntegerNullable(reader["TIPD_CLAVE"]),
                            TIPD_NOMBRE_CORTO = this.DbString(reader["TIPD_NOMBRE_CORTO"]),
                            TIPD_DESCRIPCION = this.DbString(reader["TIPD_DESCRIPCION"]),
                            CEN_CODIGO = this.DbIntegerNullable(reader["CEN_CODIGO"]),
                            CEN_DESCRIPCION = this.DbString(reader["CEN_DESCRIPCION"]),
                            CEN_AREA_ORIGEN = this.DbString(reader["CEN_AREA_ORIGEN"]),
                            PROV_NIF = this.DbString(reader["PROV_NIF"]),
                            PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                            EXP_ANO_PRESUPUESTO = this.DbShortNullable(reader["EXP_ANO_PRESUPUESTO"]),
                            PRO_NUMERO = this.DbString(reader["PRO_NUMERO"]),
                            SEN_NUMERO = this.DbIntegerNullable(reader["SEN_NUMERO"]),
                        };
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        var application = new PRE_DOCUMENTO_APLICACION
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            CACS_NUMERO = this.DbString(reader["CACS_NUMERO"]),
                            CUEP_NUMERO = this.DbString(reader["CUEP_NUMERO"]),
                            DOCA_IMPORTE = this.DbDecimal(reader["DOCA_IMPORTE"]),
                            DOCA_ANO_PRESUPUESTO = this.DbShortNullable(reader["DOCA_ANO_PRESUPUESTO"])
                        };

                        applications.Add(application);
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

                return new Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>>(accountinDocument, parameter, applications);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>> GetSpendPById(int id)
        {
            try
            {
                PRE_DOCUMENTO_CONTABLE accountinDocument = null;
                PRE_PARAMETROS parameter = null;
                var applications = new List<PRE_DOCUMENTO_APLICACION>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetSpendPById");

                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, id);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        accountinDocument = new PRE_DOCUMENTO_CONTABLE
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            DOC_FECHA_PROPUESTA = this.DbDateNullable(reader["DOC_FECHA_PROPUESTA"]),
                            DOC_FECHA_ASIENTO_DIARIO = this.DbDateNullable(reader["DOC_FECHA_ASIENTO_DIARIO"]),
                            DOC_NUMERO_CHEQUE = this.DbString(reader["DOC_NUMERO_CHEQUE"]),
                            DOC_DESCRIPCION = this.DbString(reader["DOC_DESCRIPCION"]),
                            EXP_NUM_EXP_CONTABLE_ANUAL = this.DbIntegerNullable(reader["EXP_NUM_EXP_CONTABLE_ANUAL"]),
                            CUE_ORDINAL_PERCEPTOR = this.DbIntegerNullable(reader["CUE_ORDINAL_PERCEPTOR"]),
                            TIPP_CODIGO = this.DbByteNullable(reader["TIPP_CODIGO"]),
                            FOR_CODIGO = this.DbByteNullable(reader["FOR_CODIGO"]),
                            TIPD_POSITIVO = this.DbBooleanBit(reader["TIPD_POSITIVO"]),
                            TIPD_CLAVE = this.DbIntegerNullable(reader["TIPD_CLAVE"]),
                            TIPD_NOMBRE_CORTO = this.DbString(reader["TIPD_NOMBRE_CORTO"]),
                            TIPD_DESCRIPCION = this.DbString(reader["TIPD_DESCRIPCION"]),
                            CEN_CODIGO = this.DbIntegerNullable(reader["CEN_CODIGO"]),
                            CEN_DESCRIPCION = this.DbString(reader["CEN_DESCRIPCION"]),
                            CEN_AREA_ORIGEN = this.DbString(reader["CEN_AREA_ORIGEN"]),
                            PROV_NIF = this.DbString(reader["PROV_NIF"]),
                            PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                            EXP_ANO_PRESUPUESTO = this.DbShortNullable(reader["EXP_ANO_PRESUPUESTO"]),
                            PRO_NUMERO = this.DbString(reader["PRO_NUMERO"]),
                            SEN_NUMERO = this.DbIntegerNullable(reader["SEN_NUMERO"]),
                        };
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        var application = new PRE_DOCUMENTO_APLICACION
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            CACS_NUMERO = this.DbString(reader["CACS_NUMERO"]),
                            CUEP_NUMERO = this.DbString(reader["CUEP_NUMERO"]),
                            DOCA_IMPORTE = this.DbDecimal(reader["DOCA_IMPORTE"]),
                            DOCA_ANO_PRESUPUESTO = this.DbShortNullable(reader["DOCA_ANO_PRESUPUESTO"])
                        };

                        applications.Add(application);
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

                return new Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>>(accountinDocument, parameter, applications);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>, List<PRE_EXPEDIENTE_CONTABLE>> GetSpendIncomeDiscountsById(int id)
        {
            try
            {
                PRE_DOCUMENTO_CONTABLE accountinDocument = null;
                PRE_PARAMETROS parameter = null;
                var applications = new List<PRE_DOCUMENTO_APLICACION>();
                var discounts = new List<PRE_EXPEDIENTE_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetSpendIncomeDiscountsById");

                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, id);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        accountinDocument = new PRE_DOCUMENTO_CONTABLE
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            DOC_FECHA_PROPUESTA = this.DbDateNullable(reader["DOC_FECHA_PROPUESTA"]),
                            DOC_FECHA_ASIENTO_DIARIO = this.DbDateNullable(reader["DOC_FECHA_ASIENTO_DIARIO"]),
                            DOC_NUMERO_CHEQUE = this.DbString(reader["DOC_NUMERO_CHEQUE"]),
                            DOC_DESCRIPCION = this.DbString(reader["DOC_DESCRIPCION"]),
                            EXP_NUM_EXP_CONTABLE_ANUAL = this.DbIntegerNullable(reader["EXP_NUM_EXP_CONTABLE_ANUAL"]),
                            CUE_ORDINAL_PERCEPTOR = this.DbIntegerNullable(reader["CUE_ORDINAL_PERCEPTOR"]),
                            TIPP_CODIGO = this.DbByteNullable(reader["TIPP_CODIGO"]),
                            FOR_CODIGO = this.DbByteNullable(reader["FOR_CODIGO"]),
                            TIPD_POSITIVO = this.DbBooleanBit(reader["TIPD_POSITIVO"]),
                            TIPD_CLAVE = this.DbIntegerNullable(reader["TIPD_CLAVE"]),
                            TIPD_NOMBRE_CORTO = this.DbString(reader["TIPD_NOMBRE_CORTO"]),
                            TIPD_DESCRIPCION = this.DbString(reader["TIPD_DESCRIPCION"]),
                            CEN_CODIGO = this.DbIntegerNullable(reader["CEN_CODIGO"]),
                            CEN_DESCRIPCION = this.DbString(reader["CEN_DESCRIPCION"]),
                            CEN_AREA_ORIGEN = this.DbString(reader["CEN_AREA_ORIGEN"]),
                            PROV_NIF = this.DbString(reader["PROV_NIF"]),
                            PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                            EXP_ANO_PRESUPUESTO = this.DbShortNullable(reader["EXP_ANO_PRESUPUESTO"]),
                            PRO_NUMERO = this.DbString(reader["PRO_NUMERO"])
                        };
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        var application = new PRE_DOCUMENTO_APLICACION
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            CACS_NUMERO = this.DbString(reader["CACS_NUMERO"]),
                            CUEP_NUMERO = this.DbString(reader["CUEP_NUMERO"]),
                            DOCA_IMPORTE = this.DbDecimal(reader["DOCA_IMPORTE"]),
                            DOCA_ANO_PRESUPUESTO = this.DbShortNullable(reader["DOCA_ANO_PRESUPUESTO"])
                        };

                        applications.Add(application);
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        var discount = new PRE_EXPEDIENTE_CONTABLE
                        {
                            EXP_DESCRIPCION = this.DbString(reader["EXP_DESCRIPCION"]),
                            PRE_CODIGO = this.DbIntegerNullable(reader["PRE_CODIGO"]),
                            DOCA_IMPORTE = this.DbDecimal(reader["DOCA_IMPORTE"]),
                            CUEP_CODIGO = this.DbIntegerNullable(reader["CUEP_CODIGO"]),
                            CACS_NUMERO = this.DbString(reader["CACS_NUMERO"]),
                            DOC_CODIGO_G_DESCUENTO_I = this.DbInteger(reader["DOC_CODIGO_G_DESCUENTO_I"])
                        };

                        discounts.Add(discount);
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

                return new Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>, List<PRE_EXPEDIENTE_CONTABLE>>(accountinDocument, parameter, applications, discounts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, PRE_DOCUMENTO_APLICACION, List<PRE_EXPEDIENTE_CONTABLE>> GetSpendPRecordDiscountsById(int id)
        {
            try
            {
                PRE_DOCUMENTO_CONTABLE accountinDocument = null;
                PRE_PARAMETROS parameter = null;
                PRE_DOCUMENTO_APLICACION application = null;
                var discounts = new List<PRE_EXPEDIENTE_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetSpendPRecordDiscountsById");

                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, id);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        accountinDocument = new PRE_DOCUMENTO_CONTABLE
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            DOC_FECHA_PROPUESTA = this.DbDateNullable(reader["DOC_FECHA_PROPUESTA"]),
                            DOC_FECHA_ASIENTO_DIARIO = this.DbDateNullable(reader["DOC_FECHA_ASIENTO_DIARIO"]),
                            DOC_NUMERO_CHEQUE = this.DbString(reader["DOC_NUMERO_CHEQUE"]),
                            DOC_DESCRIPCION = this.DbString(reader["DOC_DESCRIPCION"]),
                            EXP_NUM_EXP_CONTABLE_ANUAL = this.DbIntegerNullable(reader["EXP_NUM_EXP_CONTABLE_ANUAL"]),
                            CUE_ORDINAL_PERCEPTOR = this.DbIntegerNullable(reader["CUE_ORDINAL_PERCEPTOR"]),
                            TIPP_CODIGO = this.DbByteNullable(reader["TIPP_CODIGO"]),
                            FOR_CODIGO = this.DbByteNullable(reader["FOR_CODIGO"]),
                            TIPD_POSITIVO = this.DbBooleanBit(reader["TIPD_POSITIVO"]),
                            TIPD_CLAVE = this.DbIntegerNullable(reader["TIPD_CLAVE"]),
                            TIPD_NOMBRE_CORTO = this.DbString(reader["TIPD_NOMBRE_CORTO"]),
                            TIPD_DESCRIPCION = this.DbString(reader["TIPD_DESCRIPCION"]),
                            CEN_CODIGO = this.DbIntegerNullable(reader["CEN_CODIGO"]),
                            CEN_DESCRIPCION = this.DbString(reader["CEN_DESCRIPCION"]),
                            CEN_AREA_ORIGEN = this.DbString(reader["CEN_AREA_ORIGEN"]),
                            PROV_NIF = this.DbString(reader["PROV_NIF"]),
                            PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                            EXP_ANO_PRESUPUESTO = this.DbShortNullable(reader["EXP_ANO_PRESUPUESTO"]),
                            PRO_NUMERO = this.DbString(reader["PRO_NUMERO"]),
                            SEN_NUMERO = this.DbIntegerNullable(reader["SEN_NUMERO"])
                        };
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        application = new PRE_DOCUMENTO_APLICACION
                        {
                            CACS_NUMERO = this.DbString(reader["CACS_NUMERO"]),
                            CUEP_NUMERO = this.DbString(reader["CUEP_NUMERO"]),
                            DOCA_ANO_PRESUPUESTO = this.DbShortNullable(reader["DOCA_ANO_PRESUPUESTO"]),
                            DOCA_IMPORTE = this.DbDecimal(reader["DOCA_IMPORTE"]),
                            NUM_APLICACIONES = this.DbInteger(reader["NUM_APLICACIONES"])
                        };

                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        var discount = new PRE_EXPEDIENTE_CONTABLE
                        {
                            NUMERO_APLICACION = this.DbString(reader["NUMERO_APLICACION"]),
                            NOMBRE_APLICACION = this.DbString(reader["NOMBRE_APLICACION"]),
                            IMPORTE_DESCUENTO = this.DbDecimal(reader["IMPORTE_DESCUENTO"]),
                            CUENTA_PGCP = this.DbString(reader["CUENTA_PGCP"])
                        };

                        discounts.Add(discount);
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

                return new Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, PRE_DOCUMENTO_APLICACION, List<PRE_EXPEDIENTE_CONTABLE>>(accountinDocument, parameter, application, discounts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, PRE_DOCUMENTO_APLICACION, List<PRE_EXP_EXTRAPRE>> GetSpendPRecordById(int id)
        {
            try
            {
                PRE_DOCUMENTO_CONTABLE accountinDocument = null;
                PRE_PARAMETROS parameter = null;
                PRE_DOCUMENTO_APLICACION application = null;
                var files = new List<PRE_EXP_EXTRAPRE>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetSpendPRecordById");

                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, id);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        accountinDocument = new PRE_DOCUMENTO_CONTABLE
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            DOC_FECHA_PROPUESTA = this.DbDateNullable(reader["DOC_FECHA_PROPUESTA"]),
                            DOC_FECHA_ASIENTO_DIARIO = this.DbDateNullable(reader["DOC_FECHA_ASIENTO_DIARIO"]),
                            DOC_NUMERO_CHEQUE = this.DbString(reader["DOC_NUMERO_CHEQUE"]),
                            DOC_DESCRIPCION = this.DbString(reader["DOC_DESCRIPCION"]),
                            EXP_NUM_EXP_CONTABLE_ANUAL = this.DbIntegerNullable(reader["EXP_NUM_EXP_CONTABLE_ANUAL"]),
                            CUE_ORDINAL_PERCEPTOR = this.DbIntegerNullable(reader["CUE_ORDINAL_PERCEPTOR"]),
                            TIPP_CODIGO = this.DbByteNullable(reader["TIPP_CODIGO"]),
                            FOR_CODIGO = this.DbByteNullable(reader["FOR_CODIGO"]),
                            TIPD_POSITIVO = this.DbBooleanBit(reader["TIPD_POSITIVO"]),
                            TIPD_CLAVE = this.DbIntegerNullable(reader["TIPD_CLAVE"]),
                            TIPD_NOMBRE_CORTO = this.DbString(reader["TIPD_NOMBRE_CORTO"]),
                            TIPD_DESCRIPCION = this.DbString(reader["TIPD_DESCRIPCION"]),
                            CEN_CODIGO = this.DbIntegerNullable(reader["CEN_CODIGO"]),
                            CEN_DESCRIPCION = this.DbString(reader["CEN_DESCRIPCION"]),
                            CEN_AREA_ORIGEN = this.DbString(reader["CEN_AREA_ORIGEN"]),
                            PROV_NIF = this.DbString(reader["PROV_NIF"]),
                            PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                            EXP_ANO_PRESUPUESTO = this.DbShortNullable(reader["EXP_ANO_PRESUPUESTO"]),
                            PRO_NUMERO = this.DbString(reader["PRO_NUMERO"]),
                            SEN_NUMERO = this.DbIntegerNullable(reader["SEN_NUMERO"])
                        };
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        application = new PRE_DOCUMENTO_APLICACION
                        {
                            CACS_NUMERO = this.DbString(reader["CACS_NUMERO"]),
                            CUEP_NUMERO = this.DbString(reader["CUEP_NUMERO"]),
                            DOCA_ANO_PRESUPUESTO = this.DbShortNullable(reader["DOCA_ANO_PRESUPUESTO"]),
                            DOCA_IMPORTE = this.DbDecimal(reader["DOCA_IMPORTE"]),
                            NUM_APLICACIONES = this.DbInteger(reader["NUM_APLICACIONES"])
                        };
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        var file = new PRE_EXP_EXTRAPRE
                        {
                            EXTRAPRE_NUMERO = this.DbIntegerNullable(reader["EXTRAPRE_NUMERO"]),
                            EXTRAPRE_DESCRIPCION = this.DbString(reader["EXTRAPRE_DESCRIPCION"]),
                            EXP_EXTRAP_IMPORTE = this.DbDecimal(reader["EXP_EXTRAP_IMPORTE"])
                        };

                        files.Add(file);
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

                return new Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, PRE_DOCUMENTO_APLICACION, List<PRE_EXP_EXTRAPRE>>(accountinDocument, parameter, application, files);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>, List<PRE_EXPEDIENTE_CONTABLE>> GetSpendORecordById(int id)
        {
            try
            {
                PRE_DOCUMENTO_CONTABLE accountinDocument = null;
                PRE_PARAMETROS parameter = null;
                var applications = new List<PRE_DOCUMENTO_APLICACION>();
                var discounts = new List<PRE_EXPEDIENTE_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetSpendORecordById");

                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, id);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        accountinDocument = new PRE_DOCUMENTO_CONTABLE
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            DOC_FECHA_PROPUESTA = this.DbDateNullable(reader["DOC_FECHA_PROPUESTA"]),
                            DOC_FECHA_ASIENTO_DIARIO = this.DbDateNullable(reader["DOC_FECHA_ASIENTO_DIARIO"]),
                            DOC_DESCRIPCION = this.DbString(reader["DOC_DESCRIPCION"]),
                            EXP_NUM_EXP_CONTABLE_ANUAL = this.DbIntegerNullable(reader["EXP_NUM_EXP_CONTABLE_ANUAL"]),
                            CUE_ORDINAL_PERCEPTOR = this.DbIntegerNullable(reader["CUE_ORDINAL_PERCEPTOR"]),
                            TIPP_CODIGO = this.DbByteNullable(reader["TIPP_CODIGO"]),
                            FOR_CODIGO = this.DbByteNullable(reader["FOR_CODIGO"]),
                            TIPD_POSITIVO = this.DbBooleanBit(reader["TIPD_POSITIVO"]),
                            TIPD_CLAVE = this.DbIntegerNullable(reader["TIPD_CLAVE"]),
                            TIPD_NOMBRE_CORTO = this.DbString(reader["TIPD_NOMBRE_CORTO"]),
                            TIPD_DESCRIPCION = this.DbString(reader["TIPD_DESCRIPCION"]),
                            CEN_CODIGO = this.DbIntegerNullable(reader["CEN_CODIGO"]),
                            CEN_DESCRIPCION = this.DbString(reader["CEN_DESCRIPCION"]),
                            CEN_AREA_ORIGEN = this.DbString(reader["CEN_AREA_ORIGEN"]),
                            PROV_NIF = this.DbString(reader["PROV_NIF"]),
                            PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                            EXP_ANO_PRESUPUESTO = this.DbShortNullable(reader["EXP_ANO_PRESUPUESTO"]),
                            PRO_NUMERO = this.DbString(reader["PRO_NUMERO"]),
                            SEN_NUMERO = this.DbIntegerNullable(reader["SEN_NUMERO"])
                        };
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        var application = new PRE_DOCUMENTO_APLICACION
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            CACS_NUMERO = this.DbString(reader["CACS_NUMERO"]),
                            CUEP_NUMERO = this.DbString(reader["CUEP_NUMERO"]),
                            DOCA_IMPORTE = this.DbDecimal(reader["DOCA_IMPORTE"]),
                            DOCA_ANO_PRESUPUESTO = this.DbShortNullable(reader["DOCA_ANO_PRESUPUESTO"])
                        };

                        applications.Add(application);
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        var discount = new PRE_EXPEDIENTE_CONTABLE
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            EXTRAPRE_NUMERO = this.DbIntegerNullable(reader["EXTRAPRE_NUMERO"]),
                            EXTRAPRE_DESCRIPCION = this.DbString(reader["EXTRAPRE_DESCRIPCION"]),
                            EXP_EXTRAP_IMPORTE = this.DbDecimal(reader["EXP_EXTRAP_IMPORTE"])
                        };

                        discounts.Add(discount);
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

                return new Tuple<PRE_DOCUMENTO_CONTABLE, PRE_PARAMETROS, List<PRE_DOCUMENTO_APLICACION>, List<PRE_EXPEDIENTE_CONTABLE>>(accountinDocument, parameter, applications, discounts);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_DOCUMENTO_CONTABLE> GetByIdAndType(string type, int? accountingRecordId)
        {
            try
            {
                var result = new List<PRE_DOCUMENTO_CONTABLE>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetByIdAndType");

                Db.AddInParameter(cmd, "@type", DbType.String, type);

                if (accountingRecordId != null)
                {
                    Db.AddInParameter(cmd, "@accountingRecordId", DbType.Int32, accountingRecordId);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var document = new PRE_DOCUMENTO_CONTABLE
                        {
                            DOC_CODIGO = this.DbInteger(reader["DOC_CODIGO"]),
                            DOC_I_G = this.DbString(reader["DOC_I_G"]),
                            DOC_FECHA_PROPUESTA = this.DbDateNullable(reader["DOC_FECHA_PROPUESTA"]),
                            DOC_FECHA_ASIENTO_DIARIO = this.DbDateNullable(reader["DOC_FECHA_ASIENTO_DIARIO"]),
                            DOC_NUMERO_CHEQUE = this.DbString(reader["DOC_NUMERO_CHEQUE"]),
                            DOC_DESCRIPCION = this.DbString(reader["DOC_DESCRIPCION"]),
                            SEN_NUMERO = this.DbIntegerNullable(reader["SEN_NUMERO"]),
                            SEN_CODIGO = this.DbIntegerNullable(reader["SEN_CODIGO"]),
                            DOC_NUMERO_MOVIMIENTO_I = this.DbIntegerNullable(reader["DOC_NUMERO_MOVIMIENTO_I"]),
                            DOC_FECHA_MOVIMIENTO_I = this.DbDateNullable(reader["DOC_FECHA_MOVIMIENTO_I"]),
                            EXP_CODIGO = this.DbIntegerNullable(reader["EXP_CODIGO"]),
                            TIPO_DOC = this.DbString(reader["TIPO_DOC"]),
                            TIPD_CODIGO = this.DbIntegerNullable(reader["TIPD_CODIGO"]),
                            FOR_CODIGO = this.DbByteNullable(reader["FOR_CODIGO"]),
                            ORDINAL_PAGADOR = this.DbString(reader["ORDINAL_PAGADOR"]),
                            CUE_CODIGO = this.DbIntegerNullable(reader["CUE_CODIGO"]),
                            TIPP_CODIGO = this.DbByteNullable(reader["TIPP_CODIGO"]),
                            DOC_ENLAZADO_TESORERIA = this.DbBooleanBit(reader["DOC_ENLAZADO_TESORERIA"]),
                            TES_CODIGO = this.DbIntegerNullable(reader["TES_CODIGO"]),
                            HOJ_NUMERO = this.DbIntegerNullable(reader["HOJ_NUMERO"]),
                            HOJ_NUMERO50 = this.DbIntegerNullable(reader["HOJ_NUMERO50"]),
                            ANO_HOJA = this.DbShortNullable(reader["ANO_HOJA"]),
                            ANO_HOJA50 = this.DbShortNullable(reader["ANO_HOJA50"]),
                            TIPP_DESCRIPCION = this.DbString(reader["TIPP_DESCRIPCION"]),
                            FOR_DESCRIPCION = this.DbString(reader["FOR_DESCRIPCION"])
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

        public int GetNextTonnageSheetNumber(int year)
        {
            int nextNumber = 0;

            var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetNextTonnageSheetNumber");

            Db.AddInParameter(cmd, "@year", DbType.Int32, year);

            using (var reader = Db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    nextNumber = this.DbInteger(reader["Result"]);
                }
            }
            

            return nextNumber;
        }

        public int GetNextTonnageSheetNumber50(int year)
        {
            int nextNumber = 0;

            var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetNextTonnageSheetNumber50");

            Db.AddInParameter(cmd, "@year", DbType.Int32, year);

            using (var reader = Db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    nextNumber = this.DbInteger(reader["Result"]);
                }
            }


            return nextNumber;
        }

        public int GetLastMiNumber(int year)
        {
            try
            {
                var result = 0;

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetLastMi");

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

        public int CheckMiNumber(int year, int miNumber, int documentId, int id)
        {
            try
            {
                var result = 0;

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_CheckMiNumber");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);
                Db.AddInParameter(cmd, "@operationNumber", DbType.Int32, miNumber);
                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, documentId);
                Db.AddInParameter(cmd, "@id", DbType.Int32, id);

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

        public int CheckTonnageSheet(int tonnageSheetYear, int tonnageSheet, bool is50)
        {
            try
            {
                var result = 0;

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_CheckTonnageSheet");

                Db.AddInParameter(cmd, "@tonnageSheetYear", DbType.Int32, tonnageSheetYear);
                Db.AddInParameter(cmd, "@tonnageSheet", DbType.Int32, tonnageSheet);
                Db.AddInParameter(cmd, "@is50", DbType.Boolean, is50);

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

        public Response InsertAccountingDocument(PRE_DOCUMENTO_CONTABLE accountingDocument)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_Insert");

                Db.AddInParameter(cmd, "@i_g", DbType.String, accountingDocument.DOC_I_G);
                Db.AddInParameter(cmd, "@fecha_propuesta", DbType.DateTime, accountingDocument.DOC_FECHA_PROPUESTA);
                Db.AddInParameter(cmd, "@fecha_asiento", DbType.DateTime, accountingDocument.DOC_FECHA_ASIENTO_DIARIO);

                if (!string.IsNullOrWhiteSpace(accountingDocument.DOC_NUMERO_CHEQUE))
                {
                    Db.AddInParameter(cmd, "@numero_cheque", DbType.String, accountingDocument.DOC_NUMERO_CHEQUE);
                }

                Db.AddInParameter(cmd, "@descripcion", DbType.String, accountingDocument.DOC_DESCRIPCION);

                if (accountingDocument.DOC_NUMERO_MOVIMIENTO_I != null)
                {
                    Db.AddInParameter(cmd, "@numero_movimiento_i", DbType.Int32, accountingDocument.DOC_NUMERO_MOVIMIENTO_I);
                }

                if (accountingDocument.DOC_FECHA_MOVIMIENTO_I != null)
                {
                    Db.AddInParameter(cmd, "@fecha_movimiento_i", DbType.DateTime, accountingDocument.DOC_FECHA_MOVIMIENTO_I);
                }

                Db.AddInParameter(cmd, "@exp_codigo", DbType.Int32, accountingDocument.EXP_CODIGO);
                Db.AddInParameter(cmd, "@tipd_codigo", DbType.Int32, accountingDocument.TIPD_CODIGO);

                if (accountingDocument.FOR_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@for_codigo", DbType.Int32, accountingDocument.FOR_CODIGO);
                }

                Db.AddInParameter(cmd, "@cue_codigo", DbType.Int32, accountingDocument.CUE_CODIGO);

                if (accountingDocument.TIPP_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@tipp_codigo", DbType.Int32, accountingDocument.TIPP_CODIGO);
                }

                Db.AddInParameter(cmd, "@enlazado_tesoreria", DbType.Boolean, accountingDocument.DOC_ENLAZADO_TESORERIA);

                if (accountingDocument.TES_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@tes_codigo", DbType.Int32, accountingDocument.TES_CODIGO);
                }

                Db.AddInParameter(cmd, "@hoj_numero", DbType.Int32, accountingDocument.HOJ_NUMERO);
                Db.AddInParameter(cmd, "@hoj_numero50", DbType.Int32, accountingDocument.HOJ_NUMERO50);
                Db.AddInParameter(cmd, "@ano_hoja", DbType.Int32, accountingDocument.ANO_HOJA);
                Db.AddInParameter(cmd, "@ano_hoja50", DbType.Int32, accountingDocument.ANO_HOJA50);

                if (!string.IsNullOrWhiteSpace(accountingDocument.DOC_FACTURA))
                {
                    Db.AddInParameter(cmd, "@numeros_facturas", DbType.String, accountingDocument.DOC_FACTURA);
                }

                if (accountingDocument.DOC_CODIGO_G_DESCUENTO_I != null)
                {
                    Db.AddInParameter(cmd, "@doc_codigo_g_descuento_i", DbType.Int32, accountingDocument.DOC_CODIGO_G_DESCUENTO_I);
                }

                Db.AddInParameter(cmd, "@usu_codigo", DbType.Int32, accountingDocument.USU_CODIGO);

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

        public Response UpdateAccountingDocument(PRE_DOCUMENTO_CONTABLE accountingDocument)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_Update");

                Db.AddInParameter(cmd, "@id", DbType.Int32, accountingDocument.DOC_CODIGO);
                Db.AddInParameter(cmd, "@i_g", DbType.String, accountingDocument.DOC_I_G);
                Db.AddInParameter(cmd, "@fecha_propuesta", DbType.DateTime, accountingDocument.DOC_FECHA_PROPUESTA);
                Db.AddInParameter(cmd, "@fecha_asiento", DbType.DateTime, accountingDocument.DOC_FECHA_ASIENTO_DIARIO);
                Db.AddInParameter(cmd, "@numero_cheque", DbType.String, accountingDocument.DOC_NUMERO_CHEQUE);
                Db.AddInParameter(cmd, "@descripcion", DbType.String, accountingDocument.DOC_DESCRIPCION);
                Db.AddInParameter(cmd, "@numero_movimiento_i", DbType.Int32, accountingDocument.DOC_NUMERO_MOVIMIENTO_I);
                Db.AddInParameter(cmd, "@fecha_movimiento_i", DbType.DateTime, accountingDocument.DOC_FECHA_MOVIMIENTO_I);
                Db.AddInParameter(cmd, "@exp_codigo", DbType.Int32, accountingDocument.EXP_CODIGO);
                Db.AddInParameter(cmd, "@tipd_codigo", DbType.Int32, accountingDocument.TIPD_CODIGO);

                if (accountingDocument.FOR_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@for_codigo", DbType.Int32, accountingDocument.FOR_CODIGO);
                }

                Db.AddInParameter(cmd, "@cue_codigo", DbType.Int32, accountingDocument.CUE_CODIGO);

                if (accountingDocument.TIPP_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@tipp_codigo", DbType.Int32, accountingDocument.TIPP_CODIGO);
                }

                Db.AddInParameter(cmd, "@enlazado_tesoreria", DbType.Boolean, accountingDocument.DOC_ENLAZADO_TESORERIA);

                if (accountingDocument.TES_CODIGO != null)
                {
                    Db.AddInParameter(cmd, "@tes_codigo", DbType.Int32, accountingDocument.TES_CODIGO);
                }

                Db.AddInParameter(cmd, "@hoj_numero", DbType.Int32, accountingDocument.HOJ_NUMERO);
                Db.AddInParameter(cmd, "@hoj_numero50", DbType.Int32, accountingDocument.HOJ_NUMERO50);
                Db.AddInParameter(cmd, "@ano_hoja", DbType.Int32, accountingDocument.ANO_HOJA);
                Db.AddInParameter(cmd, "@ano_hoja50", DbType.Int32, accountingDocument.ANO_HOJA50);
                Db.AddInParameter(cmd, "@usu_codigo", DbType.Int32, accountingDocument.USU_CODIGO);

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

        public Response DeleteAccountingDocument(int accountingId, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_Delete");

                Db.AddInParameter(cmd, "@id", DbType.Int32, accountingId);
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
                    ResponseCode = code
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response UpdateTonnageSheet(int? tonnageSheet, int? tonnageSheetYear, int? tonnageSheet50, int? tonnageSheet50Year, int account, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_UpdateTonnageSheet");

                if (tonnageSheet != null)
                {
                    Db.AddInParameter(cmd, "@tonnageSheet", DbType.Int32, tonnageSheet);
                }

                if (tonnageSheetYear != null)
                {
                    Db.AddInParameter(cmd, "@tonnageSheetYear", DbType.Int32, tonnageSheetYear);
                }

                if (tonnageSheet50 != null)
                {
                    Db.AddInParameter(cmd, "@tonnageSheet50", DbType.Int32, tonnageSheet50);
                }

                if (tonnageSheet50Year != null)
                {
                    Db.AddInParameter(cmd, "@tonnageSheetYear50", DbType.Int32, tonnageSheet50Year);
                }

                Db.AddInParameter(cmd, "@accountCode", DbType.Int32, account);
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
                    ResponseCode = code
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_DOCUMENTO_APLICACION> GetApplicationsByDocumentId(int documentId)
        {
            try
            {
                var result = new List<PRE_DOCUMENTO_APLICACION>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetApplications");

                Db.AddInParameter(cmd, "@id", DbType.Int32, documentId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var application = new PRE_DOCUMENTO_APLICACION
                        {
                            DOCA_CODIGO = this.DbInteger(reader["DOCA_CODIGO"]),
                            CACS_NUMERO = this.DbString(reader["CACS_NUMERO"]),
                            CUEP_CODIGO = this.DbIntegerNullable(reader["CUEP_CODIGO"]),
                            CUEP_NUMERO = this.DbString(reader["CUEP_NUMERO"]),
                            DOCA_IMPORTE = this.DbDecimal(reader["DOCA_IMPORTE"]),
                            DOC_CODIGO = this.DbIntegerNullable(reader["DOC_CODIGO"]),
                            CACS_CODIGO = this.DbString(reader["CACS_CODIGO"])
                        };

                        result.Add(application);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_DOCUMENTO_APLICACION> GetApplicationsByDocumentCacs(int documentId, string cacsCode)
        {
            try
            {
                var result = new List<PRE_DOCUMENTO_APLICACION>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetApplicationsCacs");

                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, documentId);
                Db.AddInParameter(cmd, "@cacsNumber", DbType.String, cacsCode);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var application = new PRE_DOCUMENTO_APLICACION
                        {
                            DOCA_CODIGO = this.DbInteger(reader["DOCA_CODIGO"]),
                            CACS_NUMERO = this.DbString(reader["cacs_numero"]),
                            CUEP_NUMERO = this.DbString(reader["CUEP_NUMERO"]),
                            DOCA_IMPORTE = this.DbDecimal(reader["DOCA_IMPORTE"]),
                            DOC_CODIGO = this.DbIntegerNullable(reader["DOC_CODIGO"]),
                            CACS_CODIGO = this.DbString(reader["CACS_CODIGO"])
                        };

                        result.Add(application);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response InsertApplication(PRE_DOCUMENTO_APLICACION application)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_InsertApplication");

                Db.AddInParameter(cmd, "@cacsCode", DbType.String, application.CACS_CODIGO);
                Db.AddInParameter(cmd, "@amount", DbType.Decimal, application.DOCA_IMPORTE);
                Db.AddInParameter(cmd, "@year", DbType.Int32, application.DOCA_ANO_PRESUPUESTO);
                Db.AddInParameter(cmd, "@type", DbType.String, application.DOCA_I_G);
                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, application.DOC_CODIGO);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, application.USU_CODIGO);

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

        public Response UpdateApplication(PRE_DOCUMENTO_APLICACION application)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_UpdateApplication");

                Db.AddInParameter(cmd, "@id", DbType.Int32, application.DOCA_CODIGO);
                Db.AddInParameter(cmd, "@cacsCode", DbType.String, application.CACS_CODIGO);
                Db.AddInParameter(cmd, "@amount", DbType.Decimal, application.DOCA_IMPORTE);
                Db.AddInParameter(cmd, "@year", DbType.Int32, application.DOCA_ANO_PRESUPUESTO);
                Db.AddInParameter(cmd, "@type", DbType.String, application.DOCA_I_G);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, application.USU_CODIGO);

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

        public Response DeleteApplication(int id, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_DeleteApplication");

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
                    ResponseCode = code
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response UpdateIncomeDiscounts(PRE_DOCUMENTO_CONTABLE accountingDocument)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_UpdateIncomeDiscounts");

                Db.AddInParameter(cmd, "@id", DbType.Int32, accountingDocument.DOC_CODIGO);
                Db.AddInParameter(cmd, "@proposalDate", DbType.DateTime, accountingDocument.DOC_FECHA_PROPUESTA);
                Db.AddInParameter(cmd, "@effectiveDate", DbType.DateTime, accountingDocument.DOC_FECHA_ASIENTO_DIARIO);
                Db.AddInParameter(cmd, "@incomeDate", DbType.DateTime, accountingDocument.DOC_FECHA_MOVIMIENTO_I);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, accountingDocument.USU_CODIGO);

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

        public Response UpdateDcDescription(PRE_DOCUMENTO_CONTABLE accountingDocument)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_UpdateDcDescription");

                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, accountingDocument.DOC_CODIGO);
                Db.AddInParameter(cmd, "@description", DbType.String, accountingDocument.DOC_DESCRIPCION);
                Db.AddInParameter(cmd, "@billDocument", DbType.String, accountingDocument.DOC_FACTURA);
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

        public Response UpdateRepair(PRE_DOCUMENTO_CONTABLE accountingDocument)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_UpdateRepair");

                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, accountingDocument.DOC_CODIGO);
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

        public List<PRE_FACTURA_COMPRA> GetPurchases(int accountingDocumentId)
        {
            try
            {
                var result = new List<PRE_FACTURA_COMPRA>();

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetPurchases");

                Db.AddInParameter(cmd, "@accountingDocument", DbType.Int32, accountingDocumentId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var purchase = new PRE_FACTURA_COMPRA
                        {
                            FA_CODIGO = this.DbInteger(reader["FA_CODIGO"]),
                            EA_CODIGO = this.DbIntegerNullable(reader["EA_CODIGO"]),
                            EXP_CODIGO = this.DbIntegerNullable(reader["EXP_CODIGO"]),
                            ANU_COD_ANUALIDAD = this.DbIntegerNullable(reader["ANU_COD_ANUALIDAD"]),
                            LOTE_COD_LOTE = this.DbIntegerNullable(reader["LOTE_COD_LOTE"]),
                            PROV_COD_PROVEEDOR = this.DbIntegerNullable(reader["PROV_COD_PROVEEDOR"]),
                            FA_NUM_FACTURA = this.DbString(reader["FA_NUM_FACTURA"]),
                            FA_FECHA_FACTURA = this.DbDateNullable(reader["FA_FECHA_FACTURA"]),
                            FA_IMPORTE_INTEGRO = this.DbDecimal(reader["FA_IMPORTE_INTEGRO"]),
                            FA_IMPORTE_BOE = this.DbDecimal(reader["FA_IMPORTE_BOE"]),
                            FA_IMPORTE_GARANTIA = this.DbDecimal(reader["FA_IMPORTE_GARANTIA"]),
                            PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                            PROV_NIF = this.DbString(reader["PROV_NIF"]),
                            FA_FIRMA_RO = this.DbDateNullable(reader["FA_FIRMA_RO"]),
                            DOC_CODIGO = this.DbIntegerNullable(reader["DOC_CODIGO"]),
                            MARCADO = this.DbBooleanBit(reader["MARCADO"]),
                            CODFACTURAGEI = this.DbIntegerNullable(reader["CODFACTURAGEI"]),
                            NCERTIFICADO = this.DbString(reader["NCERTIFICADO"]),
                            APP_PRESUP = this.DbString(reader["APP_PRESUP"])
                        };

                        result.Add(purchase);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetPurchasesCount(int accountingDocumentId)
        {
            try
            {
                var result = 0;

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetPurchasesCount");

                Db.AddInParameter(cmd, "@accountingDocument", DbType.Int32, accountingDocumentId);

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

        public int GetIncomeDiscountsCount(int accountingDocumentId)
        {
            try
            {
                var result = 0;

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetIncomeDiscountsCount");

                Db.AddInParameter(cmd, "@accountingDocument", DbType.Int32, accountingDocumentId);

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

        public int GetDocumentsCount(int accountingDocumentId)
        {
            try
            {
                var result = 0;

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetCounts");

                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, accountingDocumentId);

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

        public bool HaveAdPhase(int accountingDocumentId)
        {
            try
            {
                var result = false;

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_HaveAdPhase");

                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, accountingDocumentId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = this.DbBooleanBit(reader["Result"]);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool HaveOPhase(int accountingDocumentId)
        {
            try
            {
                var result = false;

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_HaveOPhase");

                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, accountingDocumentId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = this.DbBooleanBit(reader["Result"]);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool HavePPhase(int accountingDocumentId)
        {
            try
            {
                var result = false;

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_HavePPhase");

                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, accountingDocumentId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = this.DbBooleanBit(reader["Result"]);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool HaveExtraBudgetaries(int accountingDocumentId)
        {
            try
            {
                var result = 0;

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_HaveExtraBudgetaries");

                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, accountingDocumentId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = this.DbInteger(reader["Result"]);
                    }
                }

                return result != 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool HaveIncomeDiscounts(int accountingDocumentId)
        {
            try
            {
                var result = 0;

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetIncomeDiscountsCount");

                Db.AddInParameter(cmd, "@accountingDocument", DbType.Int32, accountingDocumentId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = this.DbInteger(reader["Result"]);
                    }
                }

                return result != 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool HaveRcPhase(int accountingDocumentId)
        {
            try
            {
                var result = false;

                var cmd = Db.GetStoredProcCommand("USP_AccountingRecords_HaveRcPhase");

                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, accountingDocumentId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = this.DbBooleanBit(reader["Result"]);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PRE_EXPEDIENTE_CONTABLE GetAccountingRecord(int accountingDocumentId)
        {
            try
            {
                PRE_EXPEDIENTE_CONTABLE result = null;

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetIncomeDiscount");

                Db.AddInParameter(cmd, "@accountingDocument", DbType.Int32, accountingDocumentId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = new PRE_EXPEDIENTE_CONTABLE
                        {
                            EXP_CODIGO = this.DbInteger(reader["EXP_CODIGO"]),
                            EXP_ANO_PRESUPUESTO = this.DbShort(reader["EXP_ANO_PRESUPUESTO"]),
                            TIPD_CLAVE = this.DbIntegerNullable(reader["TIPD_CLAVE"]),
                            TIPD_NOMBRE_CORTO = this.DbString(reader["TIPD_NOMBRE_CORTO"]),
                            PROC_CODIGO = this.DbInteger(reader["PROC_CODIGO"]),
                            CEN_CODIGO = this.DbInteger(reader["CEN_CODIGO"]),
                            PROV_CODIGO = this.DbInteger(reader["PROV_CODIGO"]),
                            DOC_CODIGO_G_DESCUENTO_I = this.DbInteger(reader["DOC_CODIGO_G_DESCUENTO_I"])
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

        public int GetProviderDc(int accountingDocumentId)
        {
            try
            {
                var result = 0;

                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_GetProviderDc");

                Db.AddInParameter(cmd, "@accountingDocument", DbType.Int32, accountingDocumentId);

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

        public Response UpdateBillConcept(int id, string concept, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_UpdateBillConcept");

                Db.AddInParameter(cmd, "@id", DbType.Int32, id);
                Db.AddInParameter(cmd, "@concept", DbType.String, concept);
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

        public Response UpdateTransferNumber(int id, string checkNumber, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_AccountingDocuments_UpdateTransferNumber");

                Db.AddInParameter(cmd, "@id", DbType.Int32, id);
                Db.AddInParameter(cmd, "@checkNumber", DbType.String, checkNumber);
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

        public Response UpdatePayBankOracle(DateTime? payBankDate, int billCode)
        {
            try
            {
                var connection = new OracleConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["oracleConnectionString"].ConnectionString;

                connection.Open();

                var cmd = connection.CreateCommand();

                var bank = payBankDate == null ? "null" : $"'{((DateTime)payBankDate).ToString("d", new CultureInfo("es-ES"))}'";

                var sql = $"update cyc_factura set fact_fecha_pago = {bank} where fact_cod_factura = {billCode}";

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

        public Response UpdateHistoryOracle(string certificate, string description)
        {
            try
            {
                var connection = new OracleConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["oracleConnectionString"].ConnectionString;

                connection.Open();

                var cmd = connection.CreateCommand();

                var text = $"[Contabilidad Presupuestaria: {DateTime.Now.ToString("d", new CultureInfo("es-ES"))}] {description}";

                var sql = "insert into tgst_jrnregfac (cod_factura, fec_modif, usu_modif, mod_en_ge, flgest) " +
                        $"select rf.cod_factura, sysdate, 'usuConta', '{text}', 'U' from tgst_regfac rf where rf.num_certif = '{certificate}'";

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