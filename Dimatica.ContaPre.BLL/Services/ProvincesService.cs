namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class ProvincesService : IProvincesService
    {
        #region IProvincesService Members

        public List<Provincia> GetProvincesToCombo()
        {
            return ProvincesDataContext.Instance.GetProvincesToCombo();
        }

        #endregion
    }
}