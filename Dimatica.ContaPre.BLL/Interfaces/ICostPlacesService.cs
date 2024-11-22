namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface ICostPlacesService
    {
        #region Public Methods

        List<PRE_CENTRO_COSTE> GetCostPlacesToCombo();

        List<PRE_CENTRO_COSTE> GetCostPlacesOriginToCombo();

        Response GetIdByDescription(string description);

        #endregion
    }
}