namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface IPayTypesService
    {
        #region Public Methods

        List<PRE_TIPO_PAGO> GetPayTypes();

        Response InsertPayType(PRE_TIPO_PAGO payType);

        Response UpdatePayType(PRE_TIPO_PAGO payType);

        Response DeletePayType(byte payTypeId);

        #endregion
    }
}