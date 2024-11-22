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

    public class CreditModificationTypesDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<CreditModificationTypesDataContext> Context = new Lazy<CreditModificationTypesDataContext>(() => new CreditModificationTypesDataContext());

        #endregion

        #region Public Properties

        public static CreditModificationTypesDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<PRE_TIPO_MODIFICACION_CREDITO> GetCreditModificationTypes(string type)
        {
            try
            {
                var result = new List<PRE_TIPO_MODIFICACION_CREDITO>();

                var cmd = Db.GetStoredProcCommand("USP_CreditModificationTypes_GetByType");

                Db.AddInParameter(cmd, "@type", DbType.String, type);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var creditModificationType = new PRE_TIPO_MODIFICACION_CREDITO
                                                             {
                                                                     TIPM_CODIGO = this.DbInteger(reader["TIPM_CODIGO"]),
                                                                     TIPM_NUMERO = this.DbInteger(reader["TIPM_NUMERO"]),
                                                                     TIPM_DESCRIPCION = this.DbString(reader["TIPM_DESCRIPCION"])
                                                             };

                        result.Add(creditModificationType);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response InsertCreditModificationType(PRE_TIPO_MODIFICACION_CREDITO creditModificationType)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_CreditModificationTypes_Insert");

                Db.AddInParameter(cmd, "@number", DbType.Int32, creditModificationType.TIPM_NUMERO);
                Db.AddInParameter(cmd, "@description", DbType.String, creditModificationType.TIPM_DESCRIPCION);
                Db.AddInParameter(cmd, "@type", DbType.String, creditModificationType.TIPM_I_G);

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

        public Response UpdateCreditModificationType(PRE_TIPO_MODIFICACION_CREDITO creditModificationType)
        {
            try
            {
                if (creditModificationType?.TIPM_CODIGO == null)
                {
                    return new Response
                                   {
                                           ResponseCode = ResponseCode.Invalid
                                   };
                }

                var cmd = Db.GetStoredProcCommand("USP_CreditModificationTypes_Update");

                Db.AddInParameter(cmd, "@id", DbType.Int32, creditModificationType.TIPM_CODIGO);
                Db.AddInParameter(cmd, "@number", DbType.Int32, creditModificationType.TIPM_NUMERO);
                Db.AddInParameter(cmd, "@description", DbType.String, creditModificationType.TIPM_DESCRIPCION);
                Db.AddInParameter(cmd, "@type", DbType.String, creditModificationType.TIPM_I_G);

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

        public Response DeleteCreditModificationType(int creditModificationTypeId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_CreditModificationTypes_Delete");

                Db.AddInParameter(cmd, "@id", DbType.Int32, creditModificationTypeId);

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