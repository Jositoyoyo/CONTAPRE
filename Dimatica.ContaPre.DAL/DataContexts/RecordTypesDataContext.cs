namespace Dimatica.ContaPre.DAL.DataContexts
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.DAL.Models;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class RecordTypesDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<RecordTypesDataContext> Context = new Lazy<RecordTypesDataContext>(() => new RecordTypesDataContext());

        #endregion

        #region Public Properties

        public static RecordTypesDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<PRE_TIPO_REGISTRO> GetRecordTypes()
        {
            try
            {
                var result = new List<PRE_TIPO_REGISTRO>();

                var cmd = Db.GetStoredProcCommand("USP_RecordTypes_Get");

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var record = new PRE_TIPO_REGISTRO
                                             {
                                                     TIPR_CODIGO = this.DbInteger(reader["TIPR_CODIGO"]),
                                                     TIPR_DESCRIPCION = this.DbString(reader["TIPR_DESCRIPCION"])
                                             };

                        result.Add(record);
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