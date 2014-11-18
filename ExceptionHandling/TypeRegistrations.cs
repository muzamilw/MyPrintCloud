using Microsoft.Practices.Unity;
using MPC.ExceptionHandling.Logger;
using MPC.Interfaces.Logger;

namespace MPC.ExceptionHandling
{
    /// <summary>
    /// Type Registration for Exception Handling
    /// </summary>
    public static class TypeRegistrations
    {
        /// <summary>
        /// Register Types Exception Handling module
        /// </summary>
        public static void RegisterType(IUnityContainer unityContainer)
        {            
            unityContainer.RegisterType<IMPCLogger, MPCLogger>();
        }
    }
}
