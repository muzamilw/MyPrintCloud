using Microsoft.Practices.Unity;
using MPC.ExceptionHandling.Logger;
using MPC.Implementation.Services;
using MPC.Interfaces.IServices;
using MPC.Interfaces.Logger;


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
            unityContainer.RegisterType<IMPCLogger, MPCLogger>();
            unityContainer.RegisterType<IPaperSheetService, PaperSheetService>();
        }
    }
}