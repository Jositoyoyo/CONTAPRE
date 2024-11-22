namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface IParametersService
    {
        #region Public Methods

        List<PRE_PARAMETROS> GetParameters();

        Response UpdateParameter(PRE_PARAMETROS parameters);

        #endregion
    } 
}
