namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class RecordTypesService : IRecordTypesService
    {
        #region IRecordTypesService Members

        public List<PRE_TIPO_REGISTRO> GetRecordTypes()
        {
            return RecordTypesDataContext.Instance.GetRecordTypes();
        }

        #endregion
    }
}