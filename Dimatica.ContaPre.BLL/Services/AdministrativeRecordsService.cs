namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class AdministrativeRecordsService : IAdministrativeRecordsService
    {
        #region IAdministrativeRecordsService Members

        public List<PRE_EXPEDIENTE_ADMINISTRATIVO> GetSpends(int? exerciseYear, int? recordNumber, int? provenanceId, string description)
        {
            return AdministrativeRecordsDataContext.Instance.GetSpends(exerciseYear, recordNumber, provenanceId, description);
        }

        public PRE_EXPEDIENTE_ADMINISTRATIVO GetById(int administrativeRecordId)
        {
            return AdministrativeRecordsDataContext.Instance.GetById(administrativeRecordId);
        }

        public Response InsertAdministrativeRecord(PRE_EXPEDIENTE_ADMINISTRATIVO administrativeRecord)
        {
            return AdministrativeRecordsDataContext.Instance.InsertAdministrativeRecord(administrativeRecord);
        }

        public Response UpdateAdministrativeRecord(PRE_EXPEDIENTE_ADMINISTRATIVO administrativeRecord)
        {
            return AdministrativeRecordsDataContext.Instance.UpdateAdministrativeRecord(administrativeRecord);
        }

        public Response DeleteAdministrativeRecord(int id, int userId)
        {
            return AdministrativeRecordsDataContext.Instance.DeleteAdministrativeRecord(id, userId);
        }

        public int GetNextOrder(int year, int provenanceId)
        {
            return AdministrativeRecordsDataContext.Instance.GetNextOrder(year, provenanceId);
        }

        #endregion
    }
}