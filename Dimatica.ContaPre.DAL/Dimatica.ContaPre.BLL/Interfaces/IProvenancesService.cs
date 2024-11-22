namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface IProvenancesService
    {
        #region Public Methods

        List<PRE_PROCEDENCIA> GetProvenances(string type);

        Response GetIdByDescription(string description, string type);

        Response InsertProvenance(PRE_PROCEDENCIA provenance);

        Response UpdateProvenance(PRE_PROCEDENCIA provenance);

        Response DeleteProvenance(int provenanceId);

        #endregion
    }
}