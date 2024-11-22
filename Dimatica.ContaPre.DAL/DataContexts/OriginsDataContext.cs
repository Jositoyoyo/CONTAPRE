namespace Dimatica.ContaPre.DAL.DataContexts
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.DAL.Models;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class OriginsDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<OriginsDataContext> Context = new Lazy<OriginsDataContext>(() => new OriginsDataContext());

        #endregion

        #region Public Properties

        public static OriginsDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<PRE_ORIGEN> GetOrigins()
        {
            try
            {
                var result = new List<PRE_ORIGEN>();

                var cmd = Db.GetStoredProcCommand("USP_Origins_Get");

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var origin = new PRE_ORIGEN
                                             {
                                                     ORI_CODIGO = this.DbByte(reader["ORI_CODIGO"]),
                                                     ORI_CODIGO_AUX = this.DbInteger(reader["ORI_CODIGO"]),
                                                     ORI_DESCRIPCION = this.DbString(reader["ORI_DESCRIPCION"]),
                                             };

                        result.Add(origin);
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