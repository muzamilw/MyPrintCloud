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
using MPC.Models.Common;
using MPC.Interfaces.Common;
using System.Reflection;
using MPC.Models.DomainModels;
using MPC.WebBase.UnityConfiguration;
using System.Runtime.Caching;



namespace MPC.Webstore.Controllers
{
    public class HomeController : Controller
    {
        #region Private

        private readonly ICompanyService _myCompanyService;

        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;

        private ICostCentreService _CostCentreService;

        #endregion
        [Dependency]
        public IWebstoreClaimsSecurityService ClaimsSecurityService { get; set; }




        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public HomeController(ICompanyService myCompanyService, IWebstoreClaimsHelperService webstoreAuthorizationChecker, ICostCentreService CostCentreService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            if (webstoreAuthorizationChecker == null)
            {
                throw new ArgumentNullException("webstoreAuthorizationChecker");
            }
            if (CostCentreService == null)
            {
                throw new ArgumentNullException("CostCentreService");
            }
            this._CostCentreService = CostCentreService;
            this._myCompanyService = myCompanyService;
            this._webstoreAuthorizationChecker = webstoreAuthorizationChecker;

        }

        #endregion



        public ActionResult Index()
        {
            SetUserClaim(UserCookieManager.OrganisationID);


            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;
            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.StoreId];
            ViewBag.StyleSheet = "/mpc_content/Assets/" + UserCookieManager.OrganisationID + "/" + UserCookieManager.StoreId + "/Site.css";  

           

            List<MPC.Models.DomainModels.CmsSkinPageWidget> model = null;

            string pageRouteValue = (((System.Web.Routing.Route)(RouteData.Route))).Url.Split('{')[0];

         


            model = GetWidgetsByPageName(StoreBaseResopnse.SystemPages, pageRouteValue.Split('/')[0], StoreBaseResopnse.CmsSkinPageWidgets, StoreBaseResopnse.StoreDetaultAddress, StoreBaseResopnse.Company.Name);

            StoreBaseResopnse = null;
            return View(model);
        }

        public List<MPC.Models.DomainModels.CmsSkinPageWidget> GetWidgetsByPageName(List<MPC.Models.Common.CmsPageModel> pageList, string pageName, List<MPC.Models.DomainModels.CmsSkinPageWidget> allPageWidgets, MPC.Models.DomainModels.Address DefaultAddress, string CompanyName)
        {
            if (!string.IsNullOrEmpty(pageName))
            {
                MPC.Models.Common.CmsPageModel Page = pageList.Where(p => p.PageName == pageName).FirstOrDefault();
                
                SetPageMEtaTitle(Page, DefaultAddress, CompanyName);
                
                return allPageWidgets.Where(widget => widget.PageId == Page.PageId).OrderBy(s => s.Sequence).ToList();
            }
            else
            {
                return allPageWidgets.Where(widget => widget.PageId == 1).OrderBy(s => s.Sequence).ToList();
            }
        }
                /// <summary>
        /// Binds the SEO tags to the page, page tags are in the laypout view
        /// </summary>
        /// <param name="CatName"></param>
        /// <param name="CatDes"></param>
        /// <param name="Keywords"></param>
        /// <param name="Title"></param>
        /// <param name="baseResponse"></param>
        private void SetPageMEtaTitle(MPC.Models.Common.CmsPageModel oPage, MPC.Models.DomainModels.Address DefaultAddress, string CompanyName)
        {
            string[] MetaTags = _myCompanyService.CreatePageMetaTags(oPage.PageTitle == null ? "" : oPage.PageTitle, oPage.Meta_DescriptionContent == null ? "" : oPage.Meta_DescriptionContent, oPage.Meta_KeywordContent == null ? "" : oPage.Meta_KeywordContent, StoreMode.Retail, CompanyName, DefaultAddress);

            ViewBag.MetaTitle = MetaTags[0];
            ViewBag.MetaKeywords = MetaTags[1];
            ViewBag.MetaDescription = MetaTags[2];
        }


        public ActionResult Compile()
        {
            _CostCentreService.SaveCostCentre(335, 1, "Test");

            return Content("Cost Centre compiled");
        }

        public ActionResult About(string mode)
        {
            if (mode == "compile")
            {
                _CostCentreService.SaveCostCentre(335, 1, "Test");

                return Content("Cost Centre compiled");
            }
            else
            {
                AppDomain _AppDomain = null;

                try
                {

                    string OrganizationName = "Test";
                    AppDomainSetup _AppDomainSetup = new AppDomainSetup();


                    object _oLocalObject;
                    ICostCentreLoader _oRemoteObject;

                    object[] _CostCentreParamsArray = new object[12];

                    _AppDomainSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
                    _AppDomainSetup.PrivateBinPath = Path.GetDirectoryName((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath);


                    _AppDomain = AppDomain.CreateDomain("CostCentresDomain", null, _AppDomainSetup);
                    //Me._AppDomain.InitializeLifetimeService()

                    List<CostCentreQueueItem> CostCentreQueue = new List<CostCentreQueueItem>();


                    //Me._CostCentreLaoderFactory = CType(Me._AppDomain.CreateInstance(Common.g_GlobalData.AppSettings.ApplicationStartupPath + "\Infinity.Model.dll", "Infinity.Model.CostCentres.CostCentreLoaderFactory").Unwrap(), Model.CostCentres.CostCentreLoaderFactory)
                    CostCentreLoaderFactory _CostCentreLaoderFactory = (CostCentreLoaderFactory)_AppDomain.CreateInstance("MPC.Interfaces", "MPC.Interfaces.WebStoreServices.CostCentreLoaderFactory").Unwrap();
                    _CostCentreLaoderFactory.InitializeLifetimeService();


                    //_CostCentreParamsArray(0) = Common.g_GlobalData;
                    //GlobalData
                    _CostCentreParamsArray[1] = CostCentreExecutionMode.PromptMode;
                    //this mode will load the questionqueue
                    _CostCentreParamsArray[2] = new List<QuestionQueueItem>();
                    //QuestionQueue / Execution Queue
                    _CostCentreParamsArray[3] = CostCentreQueue;
                    //CostCentreQueue
                    _CostCentreParamsArray[4] = 1;
                    //MultipleQuantities
                    _CostCentreParamsArray[5] = 1;
                    //CurrentQuantity
                    _CostCentreParamsArray[6] = new List<StockQueueItem>();
                    //StockQueue
                    _CostCentreParamsArray[7] = new List<InputQueueItem>();
                    //InputQueue
                    _CostCentreParamsArray[8] = new ItemSection(); //this._CurrentItemDTO.ItemSection(this._CurrentCostCentreIndex);
                    _CostCentreParamsArray[9] = 1;


                    CostCentre oCostCentre = _CostCentreService.GetCostCentreByID(335);

                    CostCentreQueue.Add(new CostCentreQueueItem(oCostCentre.CostCentreId, oCostCentre.Name, 1, oCostCentre.CodeFileName, null, oCostCentre.SetupSpoilage, oCostCentre.RunningSpoilage));



                    _oLocalObject = _CostCentreLaoderFactory.Create(ControllerContext.HttpContext.Server.MapPath("/") + "\\ccAssembly\\" + OrganizationName + "UserCostCentres.dll", "UserCostCentres." + oCostCentre.CodeFileName, null);
                    _oRemoteObject = (ICostCentreLoader)_oLocalObject;

                    CostCentreCostResult oResult = _oRemoteObject.returnCost(ref _CostCentreParamsArray);


                    ViewBag.result = oResult.TotalCost.ToString();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    AppDomain.Unload(_AppDomain);
                }

                return View();
            }
        }

        public ActionResult Error()
        {

            return View();
        }

        public ActionResult oAuth(int id, int isRegWithSM, string MarketBriefReturnURL)
        {
            //int isFacebook = id;
            //if (isFacebook == 1)
            //{
            //    DotNetOpenAuth.ApplicationBlock.FacebookClient client = new DotNetOpenAuth.ApplicationBlock.FacebookClient
            //    {
            //        ClientIdentifier = "1421343758131537",
            //        ClientCredentialApplicator = ClientCredentialApplicator.PostParameter("690d1f085e1cea24c61dbad3bdaa0b31"),
            //    };
            //    IAuthorizationState authorization = client.ProcessUserAuthorization();
            //    if (authorization == null)
            //    {
            //        // Kick off authorization request
            //        client.RequestUserAuthorization();
            //    }
            //    else
            //    {
            //        string webResponse = "";
            //        string email = "";
            //        string firstname = "";
            //        string lastname = "";
            //        var request = System.Net.WebRequest.Create("https://graph.facebook.com/me?&grant_type=client_credentials&access_token=" + Uri.EscapeDataString(authorization.AccessToken) + "&scope=email");

            //        using (var response = request.GetResponse())
            //        {
            //            using (var responseStream = response.GetResponseStream())
            //            {
            //                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
            //                webResponse = reader.ReadToEnd();

            //                // ShowMessage(graph.email);
            //                var definition = new { id = "", email = "", first_name = "", gender = "", last_name = "", link = "", locale = "", name = "" };
            //                var ResponseJon = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(webResponse, definition);
            //                email = ResponseJon.email;
            //                firstname = ResponseJon.first_name;
            //                lastname = ResponseJon.last_name;
            //            }
            //        }

            //        if (isRegWithSM == 1)
            //        {
            //            ViewBag.message = @"<script type='text/javascript' language='javascript'>window.close(); window.opener.location.href='/SignUp?Firstname=" + firstname + "&LastName=" + lastname + "&Email=" + email + "&ReturnURL=" + MarketBriefReturnURL + "' </script>";

            //            return View();
            //        }
            //        else
            //        {
            //            ViewBag.message = @"<script type='text/javascript' language='javascript'>window.close(); window.opener.location.href='/Login?Firstname=" + firstname + "&LastName=" + lastname + "&Email=" + email + "&ReturnURL=" + MarketBriefReturnURL + "' </script>";
            //            return View();
            //        }

            //    }
            //}
            //else if (isFacebook == 0)
            //{
            //    string requestToken = "";
            //    OAuthHelper oauthhelper = new OAuthHelper();

            //    requestToken = oauthhelper.GetRequestToken("GI61fP2n9JsLVWs5pHNiHCUvg", "6P71TMNHoVMC5RUDkqTqAJMFcvvZvtsSaEDgZtbXCTw572nvlw");

            //    if (string.IsNullOrEmpty(oauthhelper.oauth_error))
            //        Response.Redirect(oauthhelper.GetAuthorizeUrl(requestToken));

            //}
            //else if (isFacebook == 2)
            //{
            //    if (Request.QueryString["oauth_token"] != null && Request.QueryString["oauth_verifier"] != null)
            //    {
            //        string oauth_token = Request.QueryString["oauth_token"];
            //        string oauth_verifier = Request.QueryString["oauth_verifier"];

            //        OAuthHelper oauthhelper = new OAuthHelper();

            //        oauthhelper.GetUserTwAccessToken(oauth_token, oauth_verifier, "GI61fP2n9JsLVWs5pHNiHCUvg", "6P71TMNHoVMC5RUDkqTqAJMFcvvZvtsSaEDgZtbXCTw572nvlw");





            //        if (string.IsNullOrEmpty(oauthhelper.oauth_error))
            //        {
            //            if (isRegWithSM == 1)
            //            {
            //                ViewBag.message = @"<script type='text/javascript' language='javascript'>window.close(); window.opener.location.href='/SignUp?Firstname=" + oauthhelper.screen_name + "&ReturnURL=" + MarketBriefReturnURL + "' </script>";

            //                return View();
            //            }
            //            else
            //            {
            //                ViewBag.message = @"<script type='text/javascript' language='javascript'>window.close(); window.opener.location.href='/Login?Firstname=" + oauthhelper.screen_name + "&ReturnURL=" + MarketBriefReturnURL + "' </script>";
            //                return View();
            //            }

            //        }
            //    }
            //}
            return View();
        }

        private void SetUserClaim(long OrganisationID)
        {
            if (UserCookieManager.isRegisterClaims == 1)
            {
                // login 

                MPC.Models.DomainModels.CompanyContact loginUser = _myCompanyService.GetContactByEmail(UserCookieManager.Email,OrganisationID);

                ClaimsIdentity identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);

                ClaimsSecurityService.AddSignInClaimsToIdentity(loginUser.ContactId, loginUser.CompanyId, loginUser.ContactRoleId ?? 0, loginUser.TerritoryId ?? 0, identity);

                var claimsPriciple = new ClaimsPrincipal(identity);
                // Make sure the Principal's are in sync
                HttpContext.User = claimsPriciple;

                Thread.CurrentPrincipal = HttpContext.User;

                AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);

                UserCookieManager.isRegisterClaims = 0;
            }
            else if (UserCookieManager.isRegisterClaims == 2)
            {
                //signout
                AuthenticationManager.SignOut();
                UserCookieManager.isRegisterClaims = 0;
                if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                {
                    Response.Redirect("/Login");
                }
                else
                {
                    Response.Redirect("/");
                }
            }
        }


    }

}