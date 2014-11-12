using System.Security.Claims;
using System.Web;
using System.Web.Http;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Security;
using MPC.WebBase.Mvc;
using Thinktecture.IdentityModel.Tokens.Http;

namespace MPC.WebApi
{
    public class SecurityConfig

    {
        /// <summary>
        /// Unity Container
        /// </summary>
        private static IUnityContainer UnityContainer { get; set; }

        #region Private

        /// <summary>
        /// Check Authentication
        /// </summary>
        private static bool CheckAuthentication(string userName, string password)
        {
            IWebApiAuthenticationChecker authenticationChecker = UnityContainer.Resolve<IWebApiAuthenticationChecker>();
            if (authenticationChecker.Check(userName, password))
            {
                ClaimsIdentity identity = new ClaimsIdentity(userName);
                HttpContext.Current.User = new ClaimsPrincipal(identity);
                return true;
            }
            return false;
        }
        #endregion

        /// <summary>
        /// Configure security
        /// </summary>
        public static void ConfigureGlobal(HttpConfiguration globalConfig, IUnityContainer container)
        {
            UnityContainer = container;
            globalConfig.MessageHandlers.Add(new AuthenticationHandler(CreateConfiguration()));
            globalConfig.Filters.Add(new SecurityExceptionFilter());
        }

        /// <summary>
        /// Create the configuration
        /// </summary>
        private static AuthenticationConfiguration CreateConfiguration()
        {
            AuthenticationConfiguration config = new AuthenticationConfiguration();
            config.AddBasicAuthentication(CheckAuthentication);
            return config;
        }
    }
}