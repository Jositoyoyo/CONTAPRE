namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface IPayFormsService
    {
        #region Public Methods

        List<PRE_FORMA_PAGO> GetPayForms();

        Response InsertPayForm(PRE_FORMA_PAGO payForm);

        Response UpdatePayForm(PRE_FORMA_PAGO payForm);

        Response DeletePayForm(byte payFormId);

        #endregion
    }
}