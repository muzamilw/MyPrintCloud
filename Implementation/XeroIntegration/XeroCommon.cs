using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xero.Api.Example.Applications.Public;
using Xero.Api.Infrastructure.Interfaces;

namespace MPC.Implementation.XeroIntegration
{
    public static class XeroCommon
    {
        public static PublicMvcAuthenticator Authenticator { get; set; }
        public static ITokenStore TokenStore { get; set; }
    }
}
