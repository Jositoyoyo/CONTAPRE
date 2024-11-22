namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface IProgramsService
    {
        #region Public Methods

        List<PRE_PROGRAMA> GetPrograms();

        Response InsertProgram(PRE_PROGRAMA program);

        Response UpdateProgram(PRE_PROGRAMA program);

        Response DeleteProgram(byte programId);

        #endregion
    }
}