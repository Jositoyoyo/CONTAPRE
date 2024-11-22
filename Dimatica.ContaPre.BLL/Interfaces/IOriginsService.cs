namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface IOriginsService
    {
        #region Public Methods

        List<PRE_ORIGEN> GetOrigins();

        #endregion
    }
}