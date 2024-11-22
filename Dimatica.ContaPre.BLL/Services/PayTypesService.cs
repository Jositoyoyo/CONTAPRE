namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class PayTypesService : IPayTypesService
    {
        #region IPayTypesService Members

        public List<PRE_TIPO_PAGO> GetPayTypes()
        {
            return PayTypesDataContext.Instance.GetPayTypes();
        }

        public Response InsertPayType(PRE_TIPO_PAGO payType)
        {
            return PayTypesDataContext.Instance.InsertPayType(payType);
        }

        public Response UpdatePayType(PRE_TIPO_PAGO payType)
        {
            return PayTypesDataContext.Instance.UpdatePayType(payType);
        }

        public Response DeletePayType(byte payTypeId)
        {
            return PayTypesDataContext.Instance.DeletePayType(payTypeId);
        }

        #endregion
    }
}