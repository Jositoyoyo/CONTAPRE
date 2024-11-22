namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class PayFormsService : IPayFormsService
    {
        #region IPayFormsService Members

        public List<PRE_FORMA_PAGO> GetPayForms()
        {
            return PayFormsDataContext.Instance.GetPayForms();
        }

        public Response InsertPayForm(PRE_FORMA_PAGO payForm)
        {
            return PayFormsDataContext.Instance.InsertPayForm(payForm);
        }

        public Response UpdatePayForm(PRE_FORMA_PAGO payForm)
        {
            return PayFormsDataContext.Instance.UpdatePayForm(payForm);
        }

        public Response DeletePayForm(byte payFormId)
        {
            return PayFormsDataContext.Instance.DeletePayForm(payFormId);
        }

        #endregion
    }
}