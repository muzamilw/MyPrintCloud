using Microsoft.Practices.Unity;
using MPC.ExceptionHandling.Logger;
using MPC.Implementation.MISServices;
using MPC.Implementation.WebStoreServices;
using MPC.Interfaces.Logger;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.WebStoreServices;


namespace MPC.Implementation
{
    /// <summary>
    /// Type Registration for Implemention 
    /// </summary>
    public static class TypeRegistrations
    {
        /// <summary>
        /// Register Types for Implementation
        /// </summary>
        public static void RegisterType(IUnityContainer unityContainer)
        {
            Repository.TypeRegistrations.RegisterType(unityContainer);
            unityContainer.RegisterType<IMyOrganizationService, MyOrganizationService>();
            unityContainer.RegisterType<IMyCompanyDomainService, MyCompanyDomainService>();
            unityContainer.RegisterType<ICompanyService, CompanyService>();
            unityContainer.RegisterType<IMPCLogger, MPCLogger>();
            unityContainer.RegisterType<IPaperSheetService, PaperSheetService>();
            unityContainer.RegisterType<IAuthorizationChecker, AuthorizationChecker>();
            unityContainer.RegisterType<ICmsSkinPageWidgetService, CmsSkinPageWidgetService>();
            unityContainer.RegisterType<ICompanyBannerService, CompanyBannerService>();
            unityContainer.RegisterType<IClaimsSecurityService, ClaimsSecurityService>();
            unityContainer.RegisterType<IInventoryService, InventoryService>();
            unityContainer.RegisterType<IStockCategoryService, StockCategoryService>();
        }
    }
}