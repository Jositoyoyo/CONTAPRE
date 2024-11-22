namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Procedures;

    #endregion

    public interface IRectificationsService
    {
        #region Public Methods

        List<Rectification> GetRectifications(string sign, string type, int year);

        List<Rectification> GetRectificationsByCodes(string iCodes, string eCodes);

        #endregion
    }
}