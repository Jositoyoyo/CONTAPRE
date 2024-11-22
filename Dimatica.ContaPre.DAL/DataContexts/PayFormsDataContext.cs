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

    public class PayFormsDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<PayFormsDataContext> Context = new Lazy<PayFormsDataContext>(() => new PayFormsDataContext());

        #endregion

        #region Public Properties

        public static PayFormsDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<PRE_FORMA_PAGO> GetPayForms()
        {
            try
            {
                var result = new List<PRE_FORMA_PAGO>();

                var cmd = Db.GetStoredProcCommand("USP_PayForms_GetAll");

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var payForm = new PRE_FORMA_PAGO
                                              {
                                                      FOR_CODIGO = this.DbByte(reader["FOR_CODIGO"]),
                                                      FOR_CODIGO_AUX = this.DbInteger(reader["FOR_CODIGO"]),
                                                      FOR_DESCRIPCION = this.DbString(reader["FOR_DESCRIPCION"]),
                                              };

                        result.Add(payForm);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response InsertPayForm(PRE_FORMA_PAGO payForm)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_PayForms_Insert");

                Db.AddInParameter(cmd, "@payForm", DbType.String, payForm.FOR_DESCRIPCION);

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

        public Response UpdatePayForm(PRE_FORMA_PAGO payForm)
        {
            try
            {
                if (payForm?.FOR_CODIGO == null)
                {
                    return new Response
                                   {
                                           ResponseCode = ResponseCode.Invalid
                                   };
                }

                var cmd = Db.GetStoredProcCommand("USP_PayForms_Update");

                Db.AddInParameter(cmd, "@id", DbType.Int32, payForm.FOR_CODIGO);
                Db.AddInParameter(cmd, "@payForm", DbType.String, payForm.FOR_DESCRIPCION);

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

        public Response DeletePayForm(byte payFormId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_PayForms_Delete");

                Db.AddInParameter(cmd, "@id", DbType.Int32, payFormId);

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