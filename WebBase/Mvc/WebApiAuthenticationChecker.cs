using System;

namespace MPC.WebBase.Mvc
{
    /// <summary>
    /// Web Api Authentication Checket that checks the WebApiUser authentication
    /// </summary>
    public sealed class WebApiAuthenticationChecker : IWebApiAuthenticationChecker
    {
        #region Private
        private readonly IWebApiAuthenticationService webApiAuthenticationService;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="webApiAuthenticationService"></param>
        public WebApiAuthenticationChecker(IWebApiAuthenticationService webApiAuthenticationService)
        {
            if (webApiAuthenticationService == null)
            {
                throw new ArgumentNullException("webApiAuthenticationService");
            }
            this.webApiAuthenticationService = webApiAuthenticationService;
        }
        #endregion
        #region Public
        /// <summary>
        /// Check Authentication
        /// </summary>
        public bool Check(string userName, string password)
        {
            return webApiAuthenticationService.IsValidWebApiUser(userName, password);
        }
        #endregion
    }
}
