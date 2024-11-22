namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class ContractTypesService : IContractTypesService
    {
        #region IContractTypesService Members

        public List<PRE_TIPO_CONTRATO> GetContractTypes()
        {
            return ContractTypesDataContext.Instance.GetContractTypes();
        }

        #endregion
    }
}