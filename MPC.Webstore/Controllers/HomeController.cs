using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.ResponseModels;
using System.Runtime.Caching;
using MPC.Webstore.Models;
using DotNetOpenAuth.OAuth2;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DotNetOpenAuth.ApplicationBlock.Facebook;
using System.IO;
using System.Text;
using System.Security.Claims;

using MPC.Webstore.Common;

namespace MPC.Webstore.Controllers
{
    public class HomeController : Controller
    {
         #region Private

        private readonly ICompanyService _myCompanyService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public HomeController(ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
        }

        #endregion
        public ActionResult Index()
        {
            List<CmsSkinPageWidget> model = null;
            string CacheKeyName = "CompanyBaseResponse";
            string val = ((System.Web.Routing.Route) (RouteData.Route)).Url; //RouteData.Values["controller"].ToString();
            val = val.Split('/')[0];
            ObjectCache cache = MemoryCache.Default;

            MyCompanyDomainBaseResponse responseObject = cache.Get(CacheKeyName) as MyCompanyDomainBaseResponse;

            if (responseObject == null)
            {
                long storeId = Convert.ToInt64(Session["storeId"]);
                MyCompanyDomainBaseResponse response = _myCompanyService.GetBaseData(storeId).CreateFrom();
                 
                CacheItemPolicy policy = null;
                CacheEntryRemovedCallback callback = null;

                policy = new CacheItemPolicy();
                policy.Priority = CacheItemPriority.NotRemovable;
                policy.SlidingExpiration =
                    TimeSpan.FromMinutes(5);
                policy.RemovedCallback = callback;
                cache.Set(CacheKeyName, response, policy);
                model = response.CmsSkinPageWidgets.ToList();
                if (val == "Login")
                {
                    model = response.CmsSkinPageWidgets.Where(p => p.PageId == 59).ToList();
                }
                else
                {
                    model = response.CmsSkinPageWidgets.Where(p => p.PageId == 1).ToList();
                }
            }
            else
            {

                if (val == "Login")
                {
                    model = responseObject.CmsSkinPageWidgets.Where(p => p.PageId == 59).ToList();
                }
                else
                {
                    model = responseObject.CmsSkinPageWidgets.Where(p => p.PageId == 1).ToList();
                }
            }
            return View(model);
        }

        public ActionResult About()
        {
           
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult oAuth(int id)
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
                    
                  //  return RedirectToAction("Index", "Login", new { Firstname = firstname, LastName = lastname, Email = email });
                   // ViewBag.redirectUrl = "/Login/Index?Firstname=" + firstname + "&LastName=" + lastname + "&Email=" + email + "&auth=1";
                    string ff = "/Login/Index?Firstname=" + firstname + "&LastName=" + lastname + "&Email=" + email;
                  //  this.JavaScript("callMe("+ ff + ")");
                    return JavaScript("callMe(" + ff + ")");
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
                        return RedirectToAction("Index", "Login", new { Firstname = oauthhelper.screen_name });
                        //redirectUrl.Value = "/Login.aspx?Fname=" + oauthhelper.screen_name + "&auth=2";
                        //hfisPostback.Value = "1";

                    }
                }
            }
            return View();
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                ControllerContext.HttpContext.Response.Redirect(returnUrl);
            }
            ControllerContext.HttpContext.Response.Redirect(Url.Action("Index", "Home", null, protocol: Request.Url.Scheme));
            return null;
        }
        //private const string XsrfKey = "XsrfId";

        //private IAuthenticationManager AuthenticationManager
        //{
        //    get
        //    {

        //        return HttpContext.GetOwinContext().Authentication;
        //    }
        //}
        //private class ChallengeResult : HttpUnauthorizedResult
        //{
        //    public ChallengeResult(string provider, string redirectUri)
        //        : this(provider, redirectUri, null)
        //    {
        //    }

        //    public ChallengeResult(string provider, string redirectUri, string userId)
        //    {
        //        LoginProvider = provider;
        //        RedirectUri = redirectUri;
        //        UserId = userId;
        //    }

        //    public string LoginProvider { get; set; }
        //    public string RedirectUri { get; set; }
        //    public string UserId { get; set; }

        //    public override void ExecuteResult(ControllerContext context)
        //    {
        //        var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
        //        if (UserId != null)
        //        {
        //            properties.Dictionary[XsrfKey] = UserId;
        //        }
        //        // context.HttpContext.GetOwinContext()
        //        context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        //    }
        //}
    }
}