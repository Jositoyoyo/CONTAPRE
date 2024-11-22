namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class ParametersService : IParametersService
    {
        #region IParametersService Members

        public List<PRE_PARAMETROS> GetParameters()
        {
            return ParametersDataContext.Instance.GetParameters();
        }

        public Response UpdateParameter(PRE_PARAMETROS parameters)
        {
            return ParametersDataContext.Instance.UpdateParameter(parameters);
        }

        #endregion
    }
}