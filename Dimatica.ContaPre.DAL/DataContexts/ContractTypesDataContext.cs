namespace Dimatica.ContaPre.DAL.DataContexts
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.DAL.Models;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class ContractTypesDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<ContractTypesDataContext> Context = new Lazy<ContractTypesDataContext>(() => new ContractTypesDataContext());

        #endregion

        #region Public Properties

        public static ContractTypesDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<PRE_TIPO_CONTRATO> GetContractTypes()
        {
            try
            {
                var result = new List<PRE_TIPO_CONTRATO>();

                var cmd = Db.GetStoredProcCommand("USP_ContractTypes_Get");

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var type = new PRE_TIPO_CONTRATO
                                           {
                                                   TIPC_CODIGO = this.DbByte(reader["TIPC_CODIGO"]),
                                                   TIPC_CODIGO_AUX = this.DbInteger(reader["TIPC_CODIGO"]),
                                                   TIPC_DESCRIPCION = this.DbString(reader["TIPC_DESCRIPCION"])
                                           };

                        result.Add(type);
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