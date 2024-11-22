namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class ProgramsService : IProgramsService
    {
        #region IProgramsService Members

        public List<PRE_PROGRAMA> GetPrograms()
        {
            return ProgramsDataContext.Instance.GetPrograms();
        }

        public Response InsertProgram(PRE_PROGRAMA program)
        {
            return ProgramsDataContext.Instance.InsertProgram(program);
        }

        public Response UpdateProgram(PRE_PROGRAMA program)
        {
            return ProgramsDataContext.Instance.UpdateProgram(program);
        }

        public Response DeleteProgram(byte programId)
        {
            return ProgramsDataContext.Instance.DeleteProgram(programId);
        }

        #endregion
    }
}