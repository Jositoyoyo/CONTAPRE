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

    public class ProvidersDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<ProvidersDataContext> Context = new Lazy<ProvidersDataContext>(() => new ProvidersDataContext());

        #endregion

        #region Public Properties

        public static ProvidersDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public PRE_PROVEEDOR GetById(int providerId)
        {
            try
            {
                PRE_PROVEEDOR result = null;

                var cmd = Db.GetStoredProcCommand("USP_Providers_GetById");

                Db.AddInParameter(cmd, "@id", DbType.Int32, providerId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = new PRE_PROVEEDOR
                        {
                            PROV_CODIGO = this.DbInteger(reader["PROV_CODIGO"]),
                            PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                            PROV_NIF = this.DbString(reader["PROV_NIF"]),
                            PROV_DIRECCION = this.DbString(reader["PROV_DIRECCION"]),
                            PROV_CODIGO_POSTAL = this.DbString(reader["PROV_CODIGO_POSTAL"]),
                            PROV_POBLACION = this.DbString(reader["PROV_POBLACION"]),
                            PROV_TELEFONO = this.DbString(reader["PROV_TELEFONO"]),
                            PROV_PERSONA_CONTACTO = this.DbString(reader["PROV_PERSONA_CONTACTO"]),
                            ProvID = this.DbByteNullable(reader["ProvID"]),
                            PROV_CC_CE = this.DbString(reader["PROV_CC_CE"]),
                            PROV_CC_CO = this.DbString(reader["PROV_CC_CO"]),
                            PROV_CC_DC = this.DbString(reader["PROV_CC_DC"]),
                            PROV_CC_NC = this.DbString(reader["PROV_CC_NC"]),
                            PROV_NOMBRE_SUCURSAL = this.DbString(reader["PROV_NOMBRE_SUCURSAL"]),
                            PROV_DIR_SUCURSAL = this.DbString(reader["PROV_DIR_SUCURSAL"]),
                            PROV_CP_SUCURSAL = this.DbString(reader["PROV_CP_SUCURSAL"]),
                            PROV_POBLACION_SUCURSAL = this.DbString(reader["PROV_POBLACION_SUCURSAL"]),
                            PROV_INTER_JUDICIAL = this.DbBooleanBit(reader["PROV_INTER_JUDICIAL"]),
                            PROV_COD_PROVEEDOR = this.DbDecimal(reader["PROV_COD_PROVEEDOR"]),
                            CodPaisID = this.DbIntegerNullable(reader["CodPaisID"]),
                            PROV_FECHA_MODIFICACION = this.DbDateNullable(reader["PROV_FECHA_MODIFICACION"]),
                            USU_CODIGO = this.DbInteger(reader["USU_CODIGO"]),
                            prov_iban = this.DbString(reader["prov_iban"]),
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

        public List<PRE_PROVEEDOR> GetProviders()
        {
            try
            {
                var result = new List<PRE_PROVEEDOR>();

                var cmd = Db.GetStoredProcCommand("USP_Providers_GetAll");

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var provider = new PRE_PROVEEDOR
                        {
                            PROV_CODIGO = this.DbInteger(reader["PROV_CODIGO"]),
                            PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                            PROV_NIF = this.DbString(reader["PROV_NIF"]),
                            PROV_DIRECCION = this.DbString(reader["PROV_DIRECCION"]),
                            PROV_POBLACION = this.DbString(reader["PROV_POBLACION"]),
                            PROV_CODIGO_POSTAL = this.DbString(reader["PROV_CODIGO_POSTAL"]),
                            PROV_TELEFONO = this.DbString(reader["PROV_TELEFONO"]),
                            PROV_PERSONA_CONTACTO = this.DbString(reader["PROV_PERSONA_CONTACTO"]),
                            ProvID = this.DbByteNullable(reader["ProvId"]),
                            ProvName = this.DbString(reader["Provincia"]),
                            PROV_CC_CE = this.DbString(reader["PROV_CC_CE"]),
                            PROV_CC_CO = this.DbString(reader["PROV_CC_CO"]),
                            PROV_CC_DC = this.DbString(reader["PROV_CC_DC"]),
                            PROV_CC_NC = this.DbString(reader["PROV_CC_NC"]),
                            PROV_NOMBRE_SUCURSAL = this.DbString(reader["PROV_NOMBRE_SUCURSAL"]),
                            PROV_DIR_SUCURSAL = this.DbString(reader["PROV_DIR_SUCURSAL"]),
                            PROV_CP_SUCURSAL = this.DbString(reader["PROV_CP_SUCURSAL"]),
                            PROV_POBLACION_SUCURSAL = this.DbString(reader["PROV_POBLACION_SUCURSAL"])
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

        public List<PRE_PROVEEDOR> GetProvidersToCombo()
        {
            try
            {
                var result = new List<PRE_PROVEEDOR>();

                var cmd = Db.GetStoredProcCommand("USP_Providers_GetToCombo");

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var provider = new PRE_PROVEEDOR
                        {
                            PROV_CODIGO = this.DbInteger(reader["PROV_CODIGO"]),
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

        public List<PRE_PROVEEDOR> GetBillProvidersToCombo()
        {
            try
            {
                var result = new List<PRE_PROVEEDOR>();

                var cmd = Db.GetStoredProcCommand("USP_Providers_GetInBillToCombo");

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var provider = new PRE_PROVEEDOR
                        {
                            PROV_CODIGO = this.DbInteger(reader["PROV_CODIGO"]),
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

        public List<PRE_PROVEEDOR> GetProvidersByNameAndNif(string name, string nif)
        {
            try
            {
                var result = new List<PRE_PROVEEDOR>();

                var cmd = Db.GetStoredProcCommand("USP_Providers_Verify");

                Db.AddInParameter(cmd, "@name", DbType.String, name);
                Db.AddInParameter(cmd, "@nif", DbType.String, nif);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var provider = new PRE_PROVEEDOR
                        {
                            PROV_CODIGO = this.DbInteger(reader["PROV_CODIGO"]),
                            PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                            PROV_NIF = this.DbString(reader["PROV_NIF"]),
                            PROV_DIRECCION = this.DbString(reader["PROV_DIRECCION"]),
                            PROV_POBLACION = this.DbString(reader["PROV_POBLACION"]),
                            PROV_TELEFONO = this.DbString(reader["PROV_TELEFONO"]),
                            PROV_CODIGO_POSTAL = this.DbString(reader["PROV_CODIGO_POSTAL"]),
                            PROV_PERSONA_CONTACTO = this.DbString(reader["PROV_PERSONA_CONTACTO"]),
                            ProvName = this.DbString(reader["Provincia"])
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

        public List<PRE_PROVEEDOR> FindProviders(string name, string nif)
        {
            try
            {
                var result = new List<PRE_PROVEEDOR>();

                var cmd = Db.GetStoredProcCommand("USP_Providers_Find");

                if (!string.IsNullOrWhiteSpace(name))
                {
                    Db.AddInParameter(cmd, "@name", DbType.String, name);
                }

                if (!string.IsNullOrWhiteSpace(nif))
                {
                    Db.AddInParameter(cmd, "@nif", DbType.String, nif);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var provider = new PRE_PROVEEDOR
                        {
                            PROV_CODIGO = this.DbInteger(reader["PROV_CODIGO"]),
                            PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                            PROV_NIF = this.DbString(reader["PROV_NIF"]),
                            PROV_DIRECCION = this.DbString(reader["PROV_DIRECCION"]),
                            PROV_POBLACION = this.DbString(reader["PROV_POBLACION"]),
                            PROV_CODIGO_POSTAL = this.DbString(reader["PROV_CODIGO_POSTAL"]),
                            PROV_TELEFONO = this.DbString(reader["PROV_TELEFONO"]),
                            PROV_PERSONA_CONTACTO = this.DbString(reader["PROV_PERSONA_CONTACTO"]),
                            ProvID = this.DbByteNullable(reader["ProvId"]),
                            ProvName = this.DbString(reader["Provincia"]),
                            PROV_CC_CE = this.DbString(reader["PROV_CC_CE"]),
                            PROV_CC_CO = this.DbString(reader["PROV_CC_CO"]),
                            PROV_CC_DC = this.DbString(reader["PROV_CC_DC"]),
                            PROV_CC_NC = this.DbString(reader["PROV_CC_NC"]),
                            PROV_NOMBRE_SUCURSAL = this.DbString(reader["PROV_NOMBRE_SUCURSAL"]),
                            PROV_DIR_SUCURSAL = this.DbString(reader["PROV_DIR_SUCURSAL"]),
                            PROV_CP_SUCURSAL = this.DbString(reader["PROV_CP_SUCURSAL"]),
                            PROV_POBLACION_SUCURSAL = this.DbString(reader["PROV_POBLACION_SUCURSAL"])
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

        public Response InsertProvider(PRE_PROVEEDOR provider)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Providers_Insert");

                Db.AddInParameter(cmd, "@name", DbType.String, provider.PROV_NOMBRE);
                Db.AddInParameter(cmd, "@nif", DbType.String, provider.PROV_NIF);
                Db.AddInParameter(cmd, "@address", DbType.String, provider.PROV_DIRECCION);
                Db.AddInParameter(cmd, "@cp", DbType.String, provider.PROV_CODIGO_POSTAL);
                Db.AddInParameter(cmd, "@population", DbType.String, provider.PROV_POBLACION);
                Db.AddInParameter(cmd, "@phone", DbType.String, provider.PROV_TELEFONO);
                Db.AddInParameter(cmd, "@person", DbType.String, provider.PROV_PERSONA_CONTACTO);
                Db.AddInParameter(cmd, "@provinceId", DbType.Int32, provider.ProvID);
                Db.AddInParameter(cmd, "@countryId", DbType.Int32, provider.CodPaisID);
                Db.AddInParameter(cmd, "@cc_ce", DbType.String, provider.PROV_CC_CE);
                Db.AddInParameter(cmd, "@cc_co", DbType.String, provider.PROV_CC_CO);
                Db.AddInParameter(cmd, "@cc_dc", DbType.String, provider.PROV_CC_DC);
                Db.AddInParameter(cmd, "@cc_nc", DbType.String, provider.PROV_CC_NC);
                Db.AddInParameter(cmd, "@branchName", DbType.String, provider.PROV_NOMBRE_SUCURSAL);
                Db.AddInParameter(cmd, "@branchAddress", DbType.String, provider.PROV_DIR_SUCURSAL);
                Db.AddInParameter(cmd, "@branchCp", DbType.String, provider.PROV_CP_SUCURSAL);
                Db.AddInParameter(cmd, "@branchPopulation", DbType.String, provider.PROV_POBLACION_SUCURSAL);
                Db.AddInParameter(cmd, "@iban", DbType.String, provider.prov_iban);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, provider.USU_CODIGO);

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

        public Response UpdateProvider(PRE_PROVEEDOR provider)
        {
            try
            {
                if (provider?.PROV_CODIGO == null)
                {
                    return new Response
                    {
                        ResponseCode = ResponseCode.Invalid
                    };
                }

                var cmd = Db.GetStoredProcCommand("USP_Providers_Update");

                Db.AddInParameter(cmd, "@id", DbType.Int32, provider.PROV_CODIGO);
                Db.AddInParameter(cmd, "@name", DbType.String, provider.PROV_NOMBRE);
                Db.AddInParameter(cmd, "@nif", DbType.String, provider.PROV_NIF);
                Db.AddInParameter(cmd, "@address", DbType.String, provider.PROV_DIRECCION);
                Db.AddInParameter(cmd, "@cp", DbType.String, provider.PROV_CODIGO_POSTAL);
                Db.AddInParameter(cmd, "@population", DbType.String, provider.PROV_POBLACION);
                Db.AddInParameter(cmd, "@phone", DbType.String, provider.PROV_TELEFONO);
                Db.AddInParameter(cmd, "@person", DbType.String, provider.PROV_PERSONA_CONTACTO);
                Db.AddInParameter(cmd, "@provinceId", DbType.Int32, provider.ProvID);
                Db.AddInParameter(cmd, "@countryId", DbType.Int32, provider.CodPaisID);
                Db.AddInParameter(cmd, "@cc_ce", DbType.String, provider.PROV_CC_CE);
                Db.AddInParameter(cmd, "@cc_co", DbType.String, provider.PROV_CC_CO);
                Db.AddInParameter(cmd, "@cc_dc", DbType.String, provider.PROV_CC_DC);
                Db.AddInParameter(cmd, "@cc_nc", DbType.String, provider.PROV_CC_NC);
                Db.AddInParameter(cmd, "@branchName", DbType.String, provider.PROV_NOMBRE_SUCURSAL);
                Db.AddInParameter(cmd, "@branchAddress", DbType.String, provider.PROV_DIR_SUCURSAL);
                Db.AddInParameter(cmd, "@branchCp", DbType.String, provider.PROV_CP_SUCURSAL);
                Db.AddInParameter(cmd, "@branchPopulation", DbType.String, provider.PROV_POBLACION_SUCURSAL);
                Db.AddInParameter(cmd, "@iban", DbType.String, provider.prov_iban);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, provider.USU_CODIGO);

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

        public Response DeleteProvider(int providerId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Providers_Delete");

                Db.AddInParameter(cmd, "@id", DbType.Int32, providerId);

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

        public Response UpdateBranchDatas(int id, string ccCe, string ccCo, string ccDc, string ccNc, string branchName, string branchAddress, string branchLocation, int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Providers_UpdateBranchDatas");

                Db.AddInParameter(cmd, "@id", DbType.Int32, id);
                Db.AddInParameter(cmd, "@ccCe", DbType.String, ccCe);
                Db.AddInParameter(cmd, "@ccCo", DbType.String, ccCo);
                Db.AddInParameter(cmd, "@ccDc", DbType.String, ccDc);
                Db.AddInParameter(cmd, "@ccNc", DbType.String, ccNc);
                Db.AddInParameter(cmd, "@branchName", DbType.String, branchName);
                Db.AddInParameter(cmd, "@branchAddress", DbType.String, branchAddress);
                Db.AddInParameter(cmd, "@branchLocation", DbType.String, branchLocation);
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

        public List<PRE_PROVEEDOR> GetByYearHasSpend(int year)
        {
            try
            {
                var result = new List<PRE_PROVEEDOR>();

                var cmd = Db.GetStoredProcCommand("USP_Providers_GetByYearSpend");

                Db.AddInParameter(cmd, "@year", DbType.Int32, year);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var provider = new PRE_PROVEEDOR
                        {
                            PROV_NOMBRE = this.DbString(reader["PROV_NOMBRE"]),
                            PROV_NIF = this.DbString(reader["PROV_NIF"])
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

        #endregion
    }
}