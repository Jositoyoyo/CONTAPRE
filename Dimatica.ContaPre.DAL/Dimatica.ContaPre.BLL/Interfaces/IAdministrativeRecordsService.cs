namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface IAdministrativeRecordsService
    {
        #region Public Methods

        List<PRE_EXPEDIENTE_ADMINISTRATIVO> GetSpends(int? exerciseYear, int? recordNumber, int? provenanceId, string description);

        PRE_EXPEDIENTE_ADMINISTRATIVO GetById(int administrativeRecordId);

        Response InsertAdministrativeRecord(PRE_EXPEDIENTE_ADMINISTRATIVO administrativeRecord);

        Response UpdateAdministrativeRecord(PRE_EXPEDIENTE_ADMINISTRATIVO administrativeRecord);

        Response DeleteAdministrativeRecord(int id, int userId);

        int GetNextOrder(int year, int provenanceId);

        #endregion
    }
}