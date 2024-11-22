namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface IRecordTypesService
    {
        #region Public Methods

        List<PRE_TIPO_REGISTRO> GetRecordTypes();

        #endregion
    }
}