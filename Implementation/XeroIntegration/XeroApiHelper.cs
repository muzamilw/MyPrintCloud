using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Interfaces.Common;
using Xero.Api.Core;
using Xero.Api.Example.Applications.Public;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Serialization;

namespace MPC.Implementation.XeroIntegration
{
    public class ApplicationSettings
    {
        public string BaseApiUrl { get; set; }
        public Consumer Consumer { get; set; }
        public object Authenticator { get; set; }
    }
    public class XeroApiHelper
    {
        private static ApplicationSettings _applicationSettings;

        public XeroApiHelper()
        {
        
            // Refer to README.md for details
            //var callbackUrl = "http://4f0bd7c4.ngrok.io/mis/api/XeroInvoice";
            //var memoryStore = new MemoryAccessTokenStore();
            //var requestTokenStore = new MemoryRequestTokenStore();
            //var baseApiUrl = "https://api.xero.com";

            //var publicAppConsumerKey = "CHJCKMBPAVT9EYGBJRQ6G9LJMQGYWK";
            //var publicAppConsumerSecret = "Q6FR9HBFBR6XQMMGXKAXYRXX3XAJCB";

            //// Public Application Settings
            //var publicConsumer = new Consumer(publicAppConsumerKey, publicAppConsumerSecret);

            //var publicAuthenticator = new PublicMvcAuthenticator(baseApiUrl, baseApiUrl, callbackUrl, memoryStore,
            //    publicConsumer, requestTokenStore);

            //var publicApplicationSettings = new ApplicationSettings
            //{
            //    BaseApiUrl = baseApiUrl,
            //    Consumer = publicConsumer,
            //    Authenticator = publicAuthenticator
            //};

            
            //_applicationSettings = publicApplicationSettings;
            
        }

        public static ApiUser User()
        {
            return new ApiUser { Name = Environment.MachineName };
        }

        public static IConsumer Consumer()
        {
            return _applicationSettings.Consumer;
        }

        public static IMvcAuthenticator MvcAuthenticator()
        {
            if (_applicationSettings != null)
                return (IMvcAuthenticator) _applicationSettings.Authenticator;
            else
                return null;
        }

        public static IXeroCoreApi CoreApi()
        {
            if (_applicationSettings.Authenticator is IAuthenticator)
            {
                return new XeroCoreApi(_applicationSettings.BaseApiUrl, _applicationSettings.Authenticator as IAuthenticator,
                    _applicationSettings.Consumer, User(), new DefaultMapper(), new DefaultMapper());
            }

            return null;
        }

        public static ApplicationSettings GetApplicationSettings(Consumer consumer, PublicMvcAuthenticator authenticator)
        {
            var publicApplicationSettings = new ApplicationSettings
            {
                BaseApiUrl = "https://api.xero.com",
                Consumer = consumer,
                Authenticator = authenticator
            };
            _applicationSettings = publicApplicationSettings;
            return publicApplicationSettings;
        }
        
    }
}
