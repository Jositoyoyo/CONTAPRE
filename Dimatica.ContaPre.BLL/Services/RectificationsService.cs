namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Procedures;

    #endregion

    public class RectificationsService : IRectificationsService
    {
        #region IRectificationsService Members

        public List<Rectification> GetRectifications(string sign, string type, int year)
        {
            return RectificationsDataContext.Instance.GetRectifications(sign, type, year);
        }

        public List<Rectification> GetRectificationsByCodes(string iCodes, string eCodes)
        {
            return RectificationsDataContext.Instance.GetRectificationsByCodes(iCodes, eCodes);
        }

        #endregion
    }
}