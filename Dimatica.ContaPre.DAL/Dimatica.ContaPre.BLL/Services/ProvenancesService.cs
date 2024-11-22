namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class ProvenancesService : IProvenancesService
    {
        #region IProvenancesService Members

        public List<PRE_PROCEDENCIA> GetProvenances(string type)
        {
            return ProvenancesDataContext.Instance.GetProvenances(type);
        }

        public Response GetIdByDescription(string description, string type)
        {
            return ProvenancesDataContext.Instance.GetIdByDescription(description, type);
        }

        public Response InsertProvenance(PRE_PROCEDENCIA provenance)
        {
            return ProvenancesDataContext.Instance.InsertProvenance(provenance);
        }

        public Response UpdateProvenance(PRE_PROCEDENCIA provenance)
        {
            return ProvenancesDataContext.Instance.UpdateProvenance(provenance);
        }

        public Response DeleteProvenance(int provenanceId)
        {
            return ProvenancesDataContext.Instance.DeleteProvenance(provenanceId);
        }

        #endregion
    }
}