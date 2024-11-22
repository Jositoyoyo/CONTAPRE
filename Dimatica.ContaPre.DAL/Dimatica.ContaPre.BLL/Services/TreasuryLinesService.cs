namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class TreasuryLinesService : ITreasuryLinesService
    {
        #region ITreasuryLinesService Members

        public List<PRE_LINEA_TESORERIA> GetTreasuryLines()
        {
            return TreasuryLinesDataContext.Instance.GetTreasuryLines();
        }

        public Response InsertTreasuryLine(PRE_LINEA_TESORERIA treasuryLine)
        {
            return TreasuryLinesDataContext.Instance.InsertTreasuryLine(treasuryLine);
        }

        public Response UpdateTreasuryLine(PRE_LINEA_TESORERIA treasuryLine)
        {
            return TreasuryLinesDataContext.Instance.UpdateTreasuryLine(treasuryLine);
        }

        public Response DeleteTreasuryLine(int treasuryLineId)
        {
            return TreasuryLinesDataContext.Instance.DeleteTreasuryLine(treasuryLineId);
        }

        #endregion
    }
}