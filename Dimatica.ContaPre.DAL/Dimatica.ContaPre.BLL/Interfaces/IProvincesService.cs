namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface IProvincesService
    {
        #region Public Methods

        List<Provincia> GetProvincesToCombo();

        #endregion
    }
}