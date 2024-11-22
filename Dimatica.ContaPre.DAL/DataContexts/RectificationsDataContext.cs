namespace Dimatica.ContaPre.DAL.DataContexts
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Dimatica.ContaPre.DAL.Models;
    using Dimatica.ContaPre.OL.Procedures;

    #endregion

    public class RectificationsDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<RectificationsDataContext> Context = new Lazy<RectificationsDataContext>(() => new RectificationsDataContext());

        #endregion

        #region Public Properties

        public static RectificationsDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<Rectification> GetRectifications(string sign, string type, int year)
        {
            try
            {
                var result = new List<Rectification>();

                var cmd = Db.GetStoredProcCommand("USP_Rectifications_Get");

                Db.AddInParameter(cmd, "@sign", DbType.String, sign);
                Db.AddInParameter(cmd, "@type", DbType.String, type);
                Db.AddInParameter(cmd, "@year", DbType.Int32, year);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var rectification = new Rectification
                                                    {
                                                            Origin = this.DbString(reader["Origin"]),
                                                            Description = this.DbString(reader["Description"]),
                                                            Code = this.DbInteger(reader["Code"]),
                                                            Concept = this.DbString(reader["Concept"]),
                                                            ProviderName = this.DbString(reader["ProviderName"]),
                                                            Amount = this.DbDecimal(reader["Amount"])
                                                    };

                        result.Add(rectification);
                    }
                }

                return result.OrderBy(r => r.Concept).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Rectification> GetRectificationsByCodes(string iCodes, string eCodes)
        {
            try
            {
                var result = new List<Rectification>();

                var cmd = Db.GetStoredProcCommand("USP_Rectifications_GetByCodes");

                if (!string.IsNullOrWhiteSpace(iCodes))
                {
                    Db.AddInParameter(cmd, "@iCodes", DbType.String, iCodes);
                }

                if (!string.IsNullOrWhiteSpace(eCodes))
                {
                    Db.AddInParameter(cmd, "@eCodes", DbType.String, eCodes);
                }

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var rectification = new Rectification
                                                    {
                                                            Code = this.DbInteger(reader["CODIGO"]),
                                                            Description = this.DbString(reader["DESCRIPCION"]),
                                                            Nature = this.DbString(reader["NATURALEZA"]),
                                                            Year = this.DbString(reader["ANO"]),
                                                            Origin = this.DbString(reader["SECCION"]),
                                                            Concept = this.DbString(reader["CONCEPTO"]),
                                                            Amount = this.DbDecimal(reader["IMPORTE"]),
                                                            Cta = this.DbString(reader["CTA"]),
                                                            ProviderName = this.DbString(reader["PROV_NIF"])
                                                    };

                        result.Add(rectification);
                    }
                }

                return result.OrderBy(r => r.Concept).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}