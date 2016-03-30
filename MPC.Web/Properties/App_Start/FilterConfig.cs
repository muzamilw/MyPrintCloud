using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace MPC.MIS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters, IUnityContainer container)
        {
            filters.Add(container.Resolve<IExceptionFilter>());
            filters.Add(container.Resolve<System.Web.Http.Filters.IExceptionFilter>());
            
            filters.Add(new HandleErrorAttribute());
        }
    }
}
