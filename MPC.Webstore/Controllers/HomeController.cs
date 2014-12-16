using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.Common;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.ResponseModels;
using MPC.Webstore.Models;
using DotNetOpenAuth.OAuth2;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Text;
using System.Security.Claims;
using ICompanyService = MPC.Interfaces.WebStoreServices.ICompanyService;

namespace MPC.Webstore.Controllers
{
    public class HomeController : Controller
    {
        #region Private

        private readonly ICompanyService _myCompanyService;

        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;

        #endregion


        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public HomeController(ICompanyService myCompanyService, IWebstoreClaimsHelperService webstoreAuthorizationChecker)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            if (webstoreAuthorizationChecker == null)
            {
                throw new ArgumentNullException("webstoreAuthorizationChecker");
            }
            this._myCompanyService = myCompanyService;
            this._webstoreAuthorizationChecker = webstoreAuthorizationChecker;
        }

        #endregion
       

       
        public ActionResult Index()
        {
            List<CmsSkinPageWidget> model = null;

            string pageRouteValue = (((System.Web.Routing.Route)(RouteData.Route))).Url.Split('{')[0];


            MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromWiget();

            model = GetWidgetsByPageName(baseResponse.SystemPages, pageRouteValue.Split('/')[0], baseResponse.CmsSkinPageWidgets);

            return View(model);
        }

        public List<CmsSkinPageWidget> GetWidgetsByPageName(List<CmsPage> pageList, string pageName, List<CmsSkinPageWidget> allPageWidgets)
        {
            if (!string.IsNullOrEmpty(pageName))
            {
                long pageId = pageList.Where(p => p.PageName == pageName).Select(id => id.PageId).FirstOrDefault();
                return allPageWidgets.Where(widget => widget.PageId == pageId).ToList();
            }
            else
            {
                return allPageWidgets.Where(widget => widget.PageId == 1).ToList();
            }
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Error()
        {

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult oAuth(int id,int isRegWithSM)
        {
            int isFacebook = id;
            if (isFacebook == 1)
            {
                DotNetOpenAuth.ApplicationBlock.FacebookClient client = new DotNetOpenAuth.ApplicationBlock.FacebookClient
                {
                    ClientIdentifier = "1421343758131537",
                    ClientCredentialApplicator = ClientCredentialApplicator.PostParameter("690d1f085e1cea24c61dbad3bdaa0b31"),
                };
                IAuthorizationState authorization = client.ProcessUserAuthorization();
                if (authorization == null)
                {
                    // Kick off authorization request
                    client.RequestUserAuthorization();
                }
                else
                {
                    string webResponse = "";
                    string email = "";
                    string firstname = "";
                    string lastname = "";
                    var request = System.Net.WebRequest.Create("https://graph.facebook.com/me?&grant_type=client_credentials&access_token=" + Uri.EscapeDataString(authorization.AccessToken) + "&scope=email");

                    using (var response = request.GetResponse())
                    {
                        using (var responseStream = response.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                            webResponse = reader.ReadToEnd();

                            // ShowMessage(graph.email);
                            var definition = new { id = "", email = "", first_name = "", gender = "", last_name = "", link = "", locale = "", name = "" };
                            var ResponseJon = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(webResponse, definition);
                            email = ResponseJon.email;
                            firstname = ResponseJon.first_name;
                            lastname = ResponseJon.last_name;
                        }
                    }
                    if (isRegWithSM == 1)
                    {
                        ViewBag.message = @"<script type='text/javascript' language='javascript'>window.close(); window.opener.location.href='/SignUp?Firstname=" + firstname + "&LastName=" + lastname + "&Email=" + email + "' </script>";

                        return View();
                    }
                    else
                    {
                        ViewBag.message = @"<script type='text/javascript' language='javascript'>window.close(); window.opener.location.href='/Login?Firstname=" + firstname + "&LastName=" + lastname + "&Email=" + email + "' </script>";
                        return View();
                    }

                }
            }
            else if (isFacebook == 0)
            {
                string requestToken = "";
                OAuthHelper oauthhelper = new OAuthHelper();

                requestToken = oauthhelper.GetRequestToken("GI61fP2n9JsLVWs5pHNiHCUvg", "6P71TMNHoVMC5RUDkqTqAJMFcvvZvtsSaEDgZtbXCTw572nvlw");

                if (string.IsNullOrEmpty(oauthhelper.oauth_error))
                    Response.Redirect(oauthhelper.GetAuthorizeUrl(requestToken));

            }
            else if (isFacebook == 2)
            {
                if (Request.QueryString["oauth_token"] != null && Request.QueryString["oauth_verifier"] != null)
                {
                    string oauth_token = Request.QueryString["oauth_token"];
                    string oauth_verifier = Request.QueryString["oauth_verifier"];

                    OAuthHelper oauthhelper = new OAuthHelper();

                    oauthhelper.GetUserTwAccessToken(oauth_token, oauth_verifier, "GI61fP2n9JsLVWs5pHNiHCUvg", "6P71TMNHoVMC5RUDkqTqAJMFcvvZvtsSaEDgZtbXCTw572nvlw");





                    if (string.IsNullOrEmpty(oauthhelper.oauth_error))
                    {
                        if (isRegWithSM == 1)
                        {
                            ViewBag.message = @"<script type='text/javascript' language='javascript'>window.close(); window.opener.location.href='/SignUp?Firstname=" + oauthhelper.screen_name + "' </script>";

                            return View();
                        }
                        else
                        {
                            ViewBag.message = @"<script type='text/javascript' language='javascript'>window.close(); window.opener.location.href='/Login?Firstname=" + oauthhelper.screen_name + "' </script>";
                            return View();
                        }

                    }
                }
            }
            return View();
        }
     
    }
}