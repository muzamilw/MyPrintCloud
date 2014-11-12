using Microsoft.Practices.Unity;
using MPC.Interfaces.Security;
using MPC.WebBase.Mvc;

namespace MPC.WebApi
{
    /// <summary>
    /// Register types WebApi Project
    /// </summary>
    public static class TypeRegistration
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterType<IWebApiAuthenticationChecker, WebApiAuthenticationChecker>();
        }
    }
}