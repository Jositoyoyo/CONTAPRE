namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface IProvidersService
    {
        #region Public Methods

        PRE_PROVEEDOR GetById(int providerId);

        List<PRE_PROVEEDOR> GetProviders();

        List<PRE_PROVEEDOR> GetProvidersToCombo();

        List<PRE_PROVEEDOR> GetBillProvidersToCombo();

        List<PRE_PROVEEDOR> GetProvidersByNameAndNif(string name, string nif);

        List<PRE_PROVEEDOR> FindProviders(string name, string nif);

        Response InsertProvider(PRE_PROVEEDOR provider);

        Response UpdateProvider(PRE_PROVEEDOR provider);

        Response DeleteProvider(int providerId);

        Response UpdateBranchDatas(int id, string ccCe, string ccCo, string ccDc, string ccNc, string branchName, string branchAddress, string branchLocation, int userId);

        List<PRE_PROVEEDOR> GetByYearHasSpend(int year);

        #endregion
    }
}