namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class CostPlacesService : ICostPlacesService
    {
        #region ICostPlacesService Members

        public List<PRE_CENTRO_COSTE> GetCostPlacesToCombo()
        {
            return CostPlacesDataContext.Instance.GetCostPlacesToCombo();
        } 
        
        public List<PRE_CENTRO_COSTE> GetCostPlacesOriginToCombo()
        {
            return CostPlacesDataContext.Instance.GetCostPlacesOriginToCombo();
        }

        public Response GetIdByDescription(string description)
        {
            return CostPlacesDataContext.Instance.GetIdByDescription(description);
        }

        #endregion
    }
}