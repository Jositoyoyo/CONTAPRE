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

    public class BillPurchasesDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<BillPurchasesDataContext> Context = new Lazy<BillPurchasesDataContext>(() => new BillPurchasesDataContext());

        #endregion

        #region Public Properties

        public static BillPurchasesDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<PRE_FACTURA_COMPRA> GetBillPurchases(int? exerciseYear, int? administrativeId, string billNumber, string billDate, decimal? billAmount, string roDate, int? providerId, decimal? roAmount, string order)
        {
            try
            {
                var result = new List<PRE_FACTURA_COMPRA>();

                var cmd = Db.GetStoredProcCommand("USP_BillPurchases_Get");

                if (exerciseYear != null)
                {
                    Db.AddInParameter(cmd, "@exerciseYear", DbType.Int32, exerciseYear);
                }

                if (administrativeId != null)
                {
                    Db.AddInParameter(cmd, "@eaCode", DbType.Int32, administrativeId);
                }

                if (!string.IsNullOrWhiteSpace(billNumber))
                {
                    Db.AddInParameter(cmd, "@billNumber", DbType.String, billNumber);
                }

                if (!string.IsNullOrWhiteSpace(billDate))
                {
                    Db.AddInParameter(cmd, "@billDate", DbType.String, billDate);
                }

                if (!string.IsNullOrWhiteSpace(roDate))
                {
                    Db.AddInParameter(cmd, "@roDate", DbType.String, roDate);
                }

                if (billAmount != null)
                {
                    Db.AddInParameter(cmd, "@billAmount", DbType.Decimal, billAmount);
                }

                if (providerId != null)
                {
                    Db.AddInParameter(cmd, "@providerId", DbType.Int32, providerId);
                }

                if (roAmount != null)
                {
                    Db.AddInParameter(cmd, "@roAmount", DbType.Decimal, roAmount);
                }

                if (!string.IsNullOrWhiteSpace(order))
                {
                    Db.AddInParameter(cmd, "@order", DbType.String, order);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var bill = new PRE_FACTURA_COMPRA
                                           {
                                                   FA_CODIGO = this.DbInteger(reader["FA_CODIGO"]),
                                                   EA_CODIGO = this.DbIntegerNullable(reader["EA_CODIGO"]),
                                                   EXP_CODIGO = this.DbIntegerNullable(reader["EXP_CODIGO"]),
                                                   ANU_COD_ANUALIDAD = this.DbIntegerNullable(reader["ANU_COD_ANUALIDAD"]),
                                                   LOTE_COD_LOTE = this.DbIntegerNullable(reader["LOTE_COD_LOTE"]),
                                                   PROV_COD_PROVEEDOR = this.DbIntegerNullable(reader["prov_cod_proveedor"]),
                                                   PROV_NOMBRE = this.DbString(reader["prov_nombre"]),
                                                   PROV_NIF = this.DbString(reader["prov_nif"]),
                                                   FA_FIRMA_RO = this.DbDateNullable(reader["fecha_ro"]),
                                                   FA_FECHA_FACTURA = this.DbDateNullable(reader["fecha_factura"]),
                                                   NCERTIFICADO = this.DbString(reader["NCERTIFICADO"]),
                                                   FA_NUM_FACTURA = this.DbString(reader["FA_NUM_FACTURA"]),
                                                   FA_IMPORTE_INTEGRO = this.DbDecimal(reader["FA_IMPORTE_INTEGRO"]),
                                                   FA_IMPORTE_BOE = this.DbDecimal(reader["FA_IMPORTE_BOE"]),
                                                   FA_IMPORTE_GARANTIA = this.DbDecimal(reader["FA_IMPORTE_GARANTIA"]),
                                                   DOC_CODIGO = this.DbIntegerNullable(reader["DOC_CODIGO"]),
                                                   FA_BASE_IMPONIBLE = this.DbDecimal(reader["FA_BASE_IMPONIBLE"]),
                                                   FA_IVA = this.DbDecimal(reader["FA_IVA"]),
                                                   FA_RETENCION = this.DbDecimal(reader["FA_RETENCION"]),
                                                   FA_IMPORTE_IVA = this.DbDecimal(reader["FA_IMPORTE_IVA"]),
                                                   FA_IMPORTE_RETENCION = this.DbDecimal(reader["FA_IMPORTE_RETENCION"]),
                                                   CODFACTURAGEI = this.DbIntegerNullable(reader["CODFACTURAGEI"]),
                                                   EXP_NUM_EXP_CONTABLE_ANUAL = this.DbIntegerNullable(reader["exp_num_exp_contable_anual"]),
                                                   EXP_ANO_PRESUPUESTO = this.DbShortNullable(reader["NroEC"]),
                                                   FA_IMPORTE_RO = this.DbDecimal(reader["importe_ro"])
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

        public List<PRE_FACTURA_COMPRA> GetByDocument(int documentId)
        {
            try
            {
                var result = new List<PRE_FACTURA_COMPRA>();

                var cmd = Db.GetStoredProcCommand("USP_BillPurchases_GetByDocument");

                Db.AddInParameter(cmd, "@documentCode", DbType.String, documentId.ToString());

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var bill = new PRE_FACTURA_COMPRA
                                           {
                                                   APP_PRESUP = this.DbString(reader["app_presup"]),
                                                   FA_IMPORTE_INTEGRO = this.DbDecimal(reader["total_importe"])
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

        public List<PRE_FACTURA_COMPRA> GetByAccountingRecord(int accountingRecordId)
        {
            try
            {
                var result = new List<PRE_FACTURA_COMPRA>();

                var cmd = Db.GetStoredProcCommand("USP_BillPurchases_GetByAccountingRecord");

                Db.AddInParameter(cmd, "@accountingRecordCode", DbType.String, accountingRecordId.ToString());

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var bill = new PRE_FACTURA_COMPRA
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

        public Response DeleteByAccountingRecord(int accountingRecordId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_BillPurchases_DeleteByAccountingRecord");

                Db.AddInParameter(cmd, "@accountingRecordCode", DbType.Int32, accountingRecordId);

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
        public Response DeleteBillPurchase(int id)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_BillPurchases_Delete");

                Db.AddInParameter(cmd, "@id", DbType.Int32, id);

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

        public Response UpdateDocument(int accountingId, string date, int providerId, int documentId, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_BillPurchases_UpdateDocument");

                Db.AddInParameter(cmd, "@accountingId", DbType.Int32, accountingId);
                Db.AddInParameter(cmd, "@date", DbType.String, date);
                Db.AddInParameter(cmd, "@providerCode", DbType.Int32, providerId);
                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, documentId);
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

        public decimal GetDocumentRetentionAmount(int documentId)
        {
            try
            {
                decimal result = 0;

                var cmd = Db.GetStoredProcCommand("USP_BillPurchases_SumDocumentRetention");

                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, documentId);

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

        public decimal GetDocumentBoeAmount(int documentId)
        {
            try
            {
                decimal result = 0;

                var cmd = Db.GetStoredProcCommand("USP_BillPurchases_SumDocumentBoe");

                Db.AddInParameter(cmd, "@documentCode", DbType.Int32, documentId);

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

        #endregion
    }
}