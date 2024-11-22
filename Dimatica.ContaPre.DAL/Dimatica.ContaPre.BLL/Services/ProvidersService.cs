namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class ProvidersService : IProvidersService
    {
        #region IProvidersService Members

        public PRE_PROVEEDOR GetById(int providerId)
        {
            return ProvidersDataContext.Instance.GetById(providerId);
        }

        public List<PRE_PROVEEDOR> GetProviders()
        {
            return ProvidersDataContext.Instance.GetProviders();
        }

        public List<PRE_PROVEEDOR> GetProvidersToCombo()
        {
            return ProvidersDataContext.Instance.GetProvidersToCombo();
        }

        public List<PRE_PROVEEDOR> GetBillProvidersToCombo()
        {
            return ProvidersDataContext.Instance.GetBillProvidersToCombo();
        }

        public List<PRE_PROVEEDOR> GetProvidersByNameAndNif(string name, string nif)
        {
            return ProvidersDataContext.Instance.GetProvidersByNameAndNif(name, nif);
        }

        public List<PRE_PROVEEDOR> FindProviders(string name, string nif)
        {
            return ProvidersDataContext.Instance.FindProviders(name, nif);
        }

        public Response InsertProvider(PRE_PROVEEDOR provider)
        {
            return ProvidersDataContext.Instance.InsertProvider(provider);
        }

        public Response UpdateProvider(PRE_PROVEEDOR provider)
        {
            return ProvidersDataContext.Instance.UpdateProvider(provider);
        }

        public Response DeleteProvider(int providerId)
        {
            return ProvidersDataContext.Instance.DeleteProvider(providerId);
        }

        public Response UpdateBranchDatas(int id, string ccCe, string ccCo, string ccDc, string ccNc, string branchName, string branchAddress, string branchLocation, int userId)
        {
            return ProvidersDataContext.Instance.UpdateBranchDatas(id, ccCe, ccCo, ccDc, ccNc, branchName, branchAddress, branchLocation, userId);
        }

        public List<PRE_PROVEEDOR> GetByYearHasSpend(int year)
        {
            return ProvidersDataContext.Instance.GetByYearHasSpend(year);
        }

        #endregion
    }
}