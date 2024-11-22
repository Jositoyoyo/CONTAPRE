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

    public class PayTypesDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<PayTypesDataContext> Context = new Lazy<PayTypesDataContext>(() => new PayTypesDataContext());

        #endregion

        #region Public Properties

        public static PayTypesDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<PRE_TIPO_PAGO> GetPayTypes()
        {
            try
            {
                var result = new List<PRE_TIPO_PAGO>();

                var cmd = Db.GetStoredProcCommand("USP_PayTypes_GetAll");

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var payType = new PRE_TIPO_PAGO
                                              {
                                                      TIPP_CODIGO = this.DbByte(reader["TIPP_CODIGO"]),
                                                      TIPP_CODIGO_AUX = this.DbInteger(reader["TIPP_CODIGO"]),
                                                      TIPP_DESCRIPCION = this.DbString(reader["TIPP_DESCRIPCION"]),
                                              };

                        result.Add(payType);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response InsertPayType(PRE_TIPO_PAGO payType)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_PayTypes_Insert");

                Db.AddInParameter(cmd, "@payType", DbType.String, payType.TIPP_DESCRIPCION);

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

        public Response UpdatePayType(PRE_TIPO_PAGO payType)
        {
            try
            {
                if (payType?.TIPP_CODIGO == null)
                {
                    return new Response
                                   {
                                           ResponseCode = ResponseCode.Invalid
                                   };
                }

                var cmd = Db.GetStoredProcCommand("USP_PayTypes_Update");

                Db.AddInParameter(cmd, "@id", DbType.Int32, payType.TIPP_CODIGO);
                Db.AddInParameter(cmd, "@payType", DbType.String, payType.TIPP_DESCRIPCION);

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

        public Response DeletePayType(byte payTypeId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_PayTypes_Delete");

                Db.AddInParameter(cmd, "@id", DbType.Int32, payTypeId);

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