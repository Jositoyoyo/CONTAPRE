namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface ITreasuryLinesService
    {
        #region Public Methods

        List<PRE_LINEA_TESORERIA> GetTreasuryLines();

        Response InsertTreasuryLine(PRE_LINEA_TESORERIA treasuryLine);

        Response UpdateTreasuryLine(PRE_LINEA_TESORERIA treasuryLine);

        Response DeleteTreasuryLine(int treasuryLineId);

        #endregion
    }
}