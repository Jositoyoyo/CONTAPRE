namespace Dimatica.ContaPre.DAL.DataContexts
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.DAL.Models;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class ProvincesDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<ProvincesDataContext> Context = new Lazy<ProvincesDataContext>(() => new ProvincesDataContext());

        #endregion

        #region Public Properties

        public static ProvincesDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<Provincia> GetProvincesToCombo()
        {
            try
            {
                var result = new List<Provincia>();

                var cmd = Db.GetStoredProcCommand("USP_Provinces_GetToCombo");

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var province = new Provincia
                        {
                            ProvID = this.DbByte(reader["ProvID"]),
                            CodProvincia = this.DbByte(reader["CodProvincia"]),
                            Provincia1 = this.DbString(reader["Provincia"]),
                            ProvIdInt = this.DbInteger(reader["ProvID"])
                        };

                        result.Add(province);
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