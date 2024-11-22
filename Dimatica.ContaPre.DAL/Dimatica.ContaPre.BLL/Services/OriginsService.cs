namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class OriginsService : IOriginsService
    {
        #region IOriginsService Members

        public List<PRE_ORIGEN> GetOrigins()
        {
            return OriginsDataContext.Instance.GetOrigins();
        }

        #endregion
    }
}