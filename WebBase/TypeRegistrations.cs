using System.Web.Mvc;
using MPC.WebBase.UnityConfiguration;
using Microsoft.Practices.Unity;
using MPC.WebBase.Mvc;
using MPC.WebBase.UnityConfiguration;

namespace MPC.WebBase
{
    public static class TypeRegistrations
    {
        public static void RegisterTypes(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType(typeof(IControllerActivator), typeof(UnityControllerActivator));
           // unityContainer.RegisterType<IExceptionFilter, LogExceptionFilterAttribute>();
           // unityContainer.RegisterType<System.Web.Http.Filters.IExceptionFilter, LogExceptionFilterAttribute>();
        }
    }
}
