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
using MPC.Webstore.Models;
using DotNetOpenAuth.OAuth2;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Text;
using System.Security.Claims;
using ICompanyService = MPC.Interfaces.WebStoreServices.ICompanyService;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using System.Runtime.Caching;
using WebSupergoo.ABCpdf8;
using System.Globalization;
using MPC.Models.ResponseModels;
using System.Net.Http;
using System.Net.Http.Headers;


namespace MPC.Webstore.Controllers
{
    public class HomeController : Controller
    {
        #region Private

        private readonly ICompanyService _myCompanyService;

        private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;

        private ICostCentreService _CostCentreService;

        private readonly IOrderService _OrderService;

        private readonly IItemService _ItemService;
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
        public HomeController(ICompanyService myCompanyService, IWebstoreClaimsHelperService webstoreAuthorizationChecker, ICostCentreService CostCentreService
            , IOrderService OrderService, IItemService ItemService)
        //: base(myCompanyService, webstoreAuthorizationChecker)
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
            if (OrderService == null)
            {
                throw new ArgumentNullException("OrderService");
            }
            this._CostCentreService = CostCentreService;
            this._myCompanyService = myCompanyService;
            this._webstoreAuthorizationChecker = webstoreAuthorizationChecker;
            this._OrderService = OrderService;
            this._ItemService = ItemService;

        }

        #endregion



        public ActionResult Index()
        {
            try
            {
                SetUserClaim(UserCookieManager.WEBOrganisationID);

                // dirty trick to set cookies after auto login
                if (UserCookieManager.PerformAutoLogin == true)
                {
                    UserCookieManager.WEBContactFirstName = UserCookieManager.WEBContactFirstName;
                    UserCookieManager.WEBContactLastName = UserCookieManager.WEBContactLastName;
                    UserCookieManager.ContactCanEditProfile = UserCookieManager.ContactCanEditProfile;
                    UserCookieManager.ShowPriceOnWebstore = UserCookieManager.ShowPriceOnWebstore;
                    UserCookieManager.WEBEmail = UserCookieManager.WEBEmail;
                    UserCookieManager.PerformAutoLogin = false;
                }
                List<MPC.Models.DomainModels.CmsSkinPageWidget> model = null;

                //iqra to fix the route of error page, consult khurram if required to get it propper.
                if (UserCookieManager.WBStoreId > 0)
                {

                    model = GetStoreAndUpdatePageSettings(model, UserCookieManager.WBStoreId);
                   
                }
                else
                {
                  
                        TempData["ErrorMessage"] = "The Domain does not exist. Please enter valid url to proceed.";
                        return RedirectToAction("Error");
                 
                
                }


                ViewBag.StyleSheet = "/mpc_content/Assets/" + UserCookieManager.WEBOrganisationID + "/" + UserCookieManager.WBStoreId + "/Site.css";
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "" + ex + "";
                return RedirectToAction("Error", "Home");

            }
        }

        private List<MPC.Models.DomainModels.CmsSkinPageWidget> GetStoreAndUpdatePageSettings(List<MPC.Models.DomainModels.CmsSkinPageWidget> model, long StoreId)
        {
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(StoreId);

            if (StoreBaseResopnse != null)
            {
                string pageRouteValue = (((System.Web.Routing.Route)(RouteData.Route))).Url.Split('{')[0];
                if (!_webstoreAuthorizationChecker.isUserLoggedIn())
                {
                    if ((StoreBaseResopnse.Company.IsCustomer == (int)StoreMode.Corp && _webstoreAuthorizationChecker.loginContactID() == 0 && (pageRouteValue != "Login/" && pageRouteValue != "SignUp/" && pageRouteValue != "ForgotPassword/")))
                    {
                        Response.Redirect("/Login");
                    }
                }
                else if (_webstoreAuthorizationChecker.isUserLoggedIn() && pageRouteValue.Split('/')[0] == "Login" && StoreBaseResopnse.Company.IsCustomer == (int)StoreMode.Corp)
                {
                    Response.Redirect("/");

                }

                model = GetWidgetsByPageName(StoreBaseResopnse.SystemPages, pageRouteValue.Split('/')[0], StoreBaseResopnse.CmsSkinPageWidgets, StoreBaseResopnse.StoreDetaultAddress, StoreBaseResopnse);

            }
            else
            {
                TempData["ErrorMessage"] = "There is some problem while performing the operation.";
                RedirectToAction("Error");
            }
            return model;
        }

        public List<MPC.Models.DomainModels.CmsSkinPageWidget> GetWidgetsByPageName(List<MPC.Models.Common.CmsPageModel> pageList, string pageName, List<MPC.Models.DomainModels.CmsSkinPageWidget> allPageWidgets, MPC.Models.DomainModels.Address DefaultAddress, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse CompanyObject)
        {
            if (!string.IsNullOrEmpty(pageName))
            {
                MPC.Models.Common.CmsPageModel Page = pageList.Where(p => p.PageName == pageName).FirstOrDefault();
                if (Page.isUserDefined == true)
                {
                    ViewBag.pageName = Page.PageTitle.Replace(" ", "-");

                }
                else
                {
                    ViewBag.pageName = Page.PageName;
                }

                if (Page.PageName == "ProductDetail" || Page.PageName == "Category" || Page.PageName == "ProductOptions")
                {
                    ViewBag.MetaTitle = TempData["MetaTitle"];
                    TempData.Keep("MetaTitle");
                    ViewBag.MetaKeywords = TempData["MetaKeywords"];
                    TempData.Keep("MetaKeywords");
                    ViewBag.MetaDescription = TempData["MetaDescription"];
                    TempData.Keep("MetaDescription");
                    ViewBag.WebMasterTag = CompanyObject.Company.WebMasterTag;
                    ViewBag.WebAnalyticCode = CompanyObject.Company.WebAnalyticCode;
                }
                else
                {
                    SetPageMEtaTitle(Page, DefaultAddress, CompanyObject);
                }
                TempData["systemPageId"] = Page.PageId;
                return allPageWidgets.Where(widget => widget.PageId == Page.PageId).OrderBy(s => s.Sequence).ToList();
            }
            else        //this is default page being fired.
            {
                ViewBag.IsHome = true;
                MPC.Models.Common.CmsPageModel Page = pageList.Where(p => p.PageName.ToLower() == "home").FirstOrDefault();
                if (Page.isUserDefined == true)
                {
                    ViewBag.pageName = Page.PageTitle.Replace(" ", "-");

                }
                else
                {
                    ViewBag.pageName = Page.PageName;
                }

                SetPageMEtaTitle(Page, DefaultAddress, CompanyObject);
                TempData["systemPageId"] = Page.PageId;
                return allPageWidgets.Where(widget => widget.PageId == Page.PageId).OrderBy(s => s.Sequence).ToList();
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
        private void SetPageMEtaTitle(MPC.Models.Common.CmsPageModel oPage, MPC.Models.DomainModels.Address DefaultAddress, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse CompanyObject)
        {
            string[] MetaTags = _myCompanyService.CreatePageMetaTags(oPage.PageTitle == null ? "" : oPage.PageTitle, oPage.Meta_DescriptionContent == null ? "" : oPage.Meta_DescriptionContent, oPage.Meta_KeywordContent == null ? "" : oPage.Meta_KeywordContent, CompanyObject.Company.Name, DefaultAddress);

            TempData.Remove("MetaTitle");
            TempData["MetaTitle"] = MetaTags[0];
            TempData.Remove("MetaKeywords");
            TempData["MetaKeywords"] = MetaTags[1];
            TempData.Remove("MetaDescription");
            TempData["MetaDescription"] = MetaTags[2];

            ViewBag.WebMasterTag = CompanyObject.Company.WebMasterTag;
            ViewBag.WebAnalyticCode = CompanyObject.Company.WebAnalyticCode;
        }


        public ActionResult Compile()
        {
            // _CostCentreService.SaveCostCentre(335, 1, "Test");

            return Content("Cost Centre compiled");
        }

        public ActionResult About(string mode)
        {
            
            //if (mode == "compile")
            //{
            //   _CostCentreService.SaveCostCentre(Convert.ToInt32(mode), 1, "Test");

            return Content("Cost Centre compiled");
            //}
            //else
            //{
            //    AppDomain _AppDomain = null;

            //    try
            //    {

            //        string OrganizationName = "Test";
            //        AppDomainSetup _AppDomainSetup = new AppDomainSetup();


            //        object _oLocalObject;
            //        ICostCentreLoader _oRemoteObject;

            //        object[] _CostCentreParamsArray = new object[12];

            //        _AppDomainSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
            //        _AppDomainSetup.PrivateBinPath = Path.GetDirectoryName((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath);


            //        _AppDomain = AppDomain.CreateDomain("CostCentresDomain", null, _AppDomainSetup);
            //        //Me._AppDomain.InitializeLifetimeService()

            //        List<CostCentreQueueItem> CostCentreQueue = new List<CostCentreQueueItem>();


            //        //Me._CostCentreLaoderFactory = CType(Me._AppDomain.CreateInstance(Common.g_GlobalData.AppSettings.ApplicationStartupPath + "\Infinity.Model.dll", "Infinity.Model.CostCentres.CostCentreLoaderFactory").Unwrap(), Model.CostCentres.CostCentreLoaderFactory)
            //        CostCentreLoaderFactory _CostCentreLaoderFactory = (CostCentreLoaderFactory)_AppDomain.CreateInstance("MPC.Interfaces", "MPC.Interfaces.WebStoreServices.CostCentreLoaderFactory").Unwrap();
            //        _CostCentreLaoderFactory.InitializeLifetimeService();


            //        //_CostCentreParamsArray(0) = Common.g_GlobalData;
            //        //GlobalData
            //        _CostCentreParamsArray[1] = CostCentreExecutionMode.PromptMode;
            //        //this mode will load the questionqueue
            //        _CostCentreParamsArray[2] = new List<QuestionQueueItem>();
            //        //QuestionQueue / Execution Queue
            //        _CostCentreParamsArray[3] = CostCentreQueue;
            //        //CostCentreQueue
            //        _CostCentreParamsArray[4] = 1;
            //        //MultipleQuantities
            //        _CostCentreParamsArray[5] = 1;
            //        //CurrentQuantity
            //        _CostCentreParamsArray[6] = new List<StockQueueItem>();
            //        //StockQueue
            //        _CostCentreParamsArray[7] = new List<InputQueueItem>();
            //        //InputQueue
            //        _CostCentreParamsArray[8] = new ItemSection(); //this._CurrentItemDTO.ItemSection(this._CurrentCostCentreIndex);
            //        _CostCentreParamsArray[9] = 1;


            //        CostCentre oCostCentre = _CostCentreService.GetCostCentreByID(335);

            //        CostCentreQueue.Add(new CostCentreQueueItem(oCostCentre.CostCentreId, oCostCentre.Name, 1, oCostCentre.CodeFileName, null, oCostCentre.SetupSpoilage, oCostCentre.RunningSpoilage));



            //        _oLocalObject = _CostCentreLaoderFactory.Create(ControllerContext.HttpContext.Server.MapPath("/") + "\\ccAssembly\\" + OrganizationName + "UserCostCentres.dll", "UserCostCentres." + oCostCentre.CodeFileName, null);
            //        _oRemoteObject = (ICostCentreLoader)_oLocalObject;

            //        CostCentreCostResult oResult = _oRemoteObject.returnCost(ref _CostCentreParamsArray);


            //        ViewBag.result = oResult.TotalCost.ToString();

            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //    finally
            //    {
            //        AppDomain.Unload(_AppDomain);
            //    }

            //    return View();
            //}
        }

        public ActionResult Error(string Message)
        {

            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.InvalidUrl = TempData["InvalidUrl"];
            return View();

        }
        public ActionResult NotFound(string Message)
        {

            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();

        }
        public ActionResult oAuth(int Provider, int isRegistrationProcess, long StoreId, string ReturnUrl)
        {

            MPC.Models.DomainModels.Company oCompany = _myCompanyService.GetCompanyByCompanyID(StoreId);

            if (Provider == 1) // fb
            {
                DotNetOpenAuth.ApplicationBlock.FacebookClient client = new DotNetOpenAuth.ApplicationBlock.FacebookClient
                {
                    ClientIdentifier = oCompany.facebookAppId,
                    ClientCredentialApplicator = ClientCredentialApplicator.PostParameter(oCompany.facebookAppKey),
                    
                };
                IEnumerable<string> scops = new List<string>() { "email"};
               // scops
              

                IAuthorizationState authorization = client.ProcessUserAuthorization();
                if (authorization == null)
                {
                    // Kick off authorization request
                    //client.PrepareRequestUserAuthorization(scops);
                    client.RequestUserAuthorization(scops);
                }
                else
                {
                    string webResponse = "";
                    string email = "";
                    string firstname = "";
                    string lastname = "";
                    string providerId = "";
                    string callBackLink = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + "/oAuth/1/" + isRegistrationProcess + "/" + UserCookieManager.WBStoreId + "/Social";

                    var request = System.Net.WebRequest.Create("https://graph.facebook.com/me?&grant_type=client_credentials&access_token=" + Uri.EscapeDataString(authorization.AccessToken) + "&scope=email,publish_actions");
                  
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
                           providerId = ResponseJon.id;
                       }
                   }

                    if (isRegistrationProcess == 1)
                    {
                        TempData["SocialProviderId"] = providerId;
                        ViewBag.message = @"<script type='text/javascript' language='javascript'>window.close(); window.opener.location.href='/SignUp?Firstname=" + firstname + "&LastName=" + lastname + "&Email=" + email + "&provider=fb&ReturnURL=" + ReturnUrl + "' </script>";

                        return View();
                    }
                    else
                    {
                        CompanyContact user = null;
                        if (string.IsNullOrEmpty(email))
                        {
                            string returnUrl = string.Empty;

                            

                            if (!string.IsNullOrEmpty(firstname) && !string.IsNullOrEmpty(lastname))
                            {
                                user = _myCompanyService.GetContactByFirstName(firstname + " " + lastname, UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID, UserCookieManager.WEBStoreMode, providerId);
                            }
                        }
                        else 
                        {
                            user = _myCompanyService.GetContactByFirstName(firstname + " " + lastname, UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID, UserCookieManager.WEBStoreMode, providerId);
                        }
                        if (user != null)
                        {
                            ViewBag.message = VerifyUser(user);
                            
                        }
                        else
                        {
                            ViewBag.message = @"<script type='text/javascript' language='javascript'>window.close(); window.opener.location.href='/Login?Message=Invalid login attempt.' </script>";
                        }
                        
                        return View();
                    }

                }
            }
            else if (Provider == 0)// twitter
            {
                string requestToken = "";
                string callBackLink = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + "/oAuth/2/" + isRegistrationProcess + "/" + UserCookieManager.WBStoreId + "/Social";
                OAuthHelper oauthhelper = new OAuthHelper();


                requestToken = oauthhelper.GetRequestToken(oCompany.twitterAppId, oCompany.twitterAppKey, callBackLink);
               
                if (string.IsNullOrEmpty(oauthhelper.oauth_error))
                    Response.Redirect(oauthhelper.GetAuthorizeUrl(requestToken));
               
            }
            else if (Provider == 2)
            {
                if (Request.QueryString["oauth_token"] != null && Request.QueryString["oauth_verifier"] != null)
                {
                    string oauth_token = Request.QueryString["oauth_token"];
                    string oauth_verifier = Request.QueryString["oauth_verifier"];

                    OAuthHelper oauthhelper = new OAuthHelper();

                    oauthhelper.GetUserTwAccessToken(oauth_token, oauth_verifier, oCompany.twitterAppId, oCompany.twitterAppKey);
                   

                    if (string.IsNullOrEmpty(oauthhelper.oauth_error))
                    {
                        if (isRegistrationProcess == 1)
                        {
                            TempData["SocialProviderId"] = oauthhelper.user_id;
                            ViewBag.message = @"<script type='text/javascript' language='javascript'>window.close(); window.opener.location.href='/SignUp?Firstname=" + oauthhelper.screen_name + "&provider=tw&ReturnURL=" + ReturnUrl + "' </script>";

                            return View();
                        }
                        else
                        {
                           
                            CompanyContact user = null;
                            if (!string.IsNullOrEmpty(oauthhelper.screen_name))
                            {
                                user = _myCompanyService.GetContactByFirstName(oauthhelper.screen_name, UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID, UserCookieManager.WEBStoreMode, oauthhelper.user_id);
                                
                            }
                            if (user != null)
                            {
                                ViewBag.message = VerifyUser(user);

                            }
                            else
                            {
                                ViewBag.message = @"<script type='text/javascript' language='javascript'>window.close(); window.opener.location.href='/Login?Message=Invalid login attempt.' </script>";
                            }
                            return View();
                        }

                    }
                }
            }
            return View();
        }

        private string VerifyUser(CompanyContact user)
        {
            string returnString = "";
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

                if (user.isArchived.HasValue && user.isArchived.Value == true)
                {
                    returnString = @"<script type='text/javascript' language='javascript'>window.close(); window.opener.location.href='/Login?Message=" + Utils.GetKeyValueFromResourceFile("DefaultAddress", UserCookieManager.WBStoreId) + "' </script>";
                   
                }
                else if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp && user.isWebAccess == false)
                {
                    returnString = @"<script type='text/javascript' language='javascript'>window.close(); window.opener.location.href='/Login?Message=" + Utils.GetKeyValueFromResourceFile("AccountHasNoWebAccess", UserCookieManager.WBStoreId) + "' </script>";
                   
                }
                else
                {

                    UserCookieManager.isRegisterClaims = 1;
                    UserCookieManager.WEBContactFirstName = user.FirstName;
                    UserCookieManager.WEBContactLastName = user.LastName == null ? "" : user.LastName;
                    UserCookieManager.ContactCanEditProfile = user.CanUserEditProfile ?? false;
                    UserCookieManager.ShowPriceOnWebstore = user.IsPricingshown ?? false;
                    UserCookieManager.CanPlaceOrder = user.isPlaceOrder ?? false;
                    UserCookieManager.WEBEmail = user.Email;

                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
                    {
                        long Orderid = _ItemService.PostLoginCustomerAndCardChanges(UserCookieManager.WEBOrderId, user.CompanyId, user.ContactId, UserCookieManager.TemporaryCompanyId, UserCookieManager.WEBOrganisationID, Convert.ToDouble(StoreBaseResopnse.Company.TaxRate), UserCookieManager.WBStoreId);

                        if (Orderid > 0)
                        {
                            UserCookieManager.TemporaryCompanyId = 0;
                             ControllerContext.HttpContext.Response.RedirectToRoute("ShopCart", new { OrderId = Orderid });
                             returnString = @"<script type='text/javascript' language='javascript'>window.close(); window.opener.location.href='/ShopCart?OrderId=" + Orderid + "' </script>";
                   
              
                        }
                    }

                    returnString = @"<script type='text/javascript' language='javascript'>window.close(); window.opener.location.href='/' </script>";
                   
                }

                return returnString;
        }

        [HttpGet]
        public ActionResult FBAuthentication()
        {
            MPC.Models.DomainModels.Company oCompany = _myCompanyService.GetCompanyByCompanyID(UserCookieManager.WBStoreId);

            DotNetOpenAuth.ApplicationBlock.FacebookClient client = new DotNetOpenAuth.ApplicationBlock.FacebookClient
            {
                ClientIdentifier = oCompany.facebookAppId,
                ClientCredentialApplicator = ClientCredentialApplicator.PostParameter(oCompany.facebookAppKey),
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


                Response.Redirect("/Login?Firstname=" + firstname + "&LastName=" + lastname + "&Email=" + email);

            }
            return null;
        }
        private void SetUserClaim(long OrganisationID)
        {
            if (UserCookieManager.isRegisterClaims == 1)
            {
                // login 
                MPC.Models.DomainModels.CompanyContact loginUser = null;
                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                {
                    loginUser = _myCompanyService.GetCorporateContactForAutoLogin(UserCookieManager.WEBEmail, OrganisationID, UserCookieManager.WBStoreId);
                }
                else
                {
                    loginUser = _myCompanyService.GetContactByEmail(UserCookieManager.WEBEmail, OrganisationID, UserCookieManager.WBStoreId);
                }


                if (loginUser != null)
                {

                    //UserCookieManager.WEBContactFirstName = loginUser.FirstName;
                    //UserCookieManager.WEBContactLastName = loginUser.LastName == null ? "" : loginUser.LastName;
                    //UserCookieManager.ContactCanEditProfile = loginUser.CanUserEditProfile ?? false;
                    //UserCookieManager.ShowPriceOnWebstore = loginUser.IsPricingshown ?? true;
                    //UserCookieManager.WEBEmail = loginUser.Email;

                    ClaimsIdentity identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);

                    ClaimsSecurityService.AddSignInClaimsToIdentity(loginUser.ContactId, loginUser.CompanyId, loginUser.ContactRoleId ?? 0, loginUser.TerritoryId ?? 0, identity);

                    var claimsPriciple = new ClaimsPrincipal(identity);
                    // Make sure the Principal's are in sync
                    HttpContext.User = claimsPriciple;

                    Thread.CurrentPrincipal = HttpContext.User;

                    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);
                }


                UserCookieManager.isRegisterClaims = 0;
            }
            else if (UserCookieManager.isRegisterClaims == 2)
            {
                //signout
                //AuthenticationManager.Signout(DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationManager.SignOut();
                UserCookieManager.isRegisterClaims = 0;

                /// explicitly set claim to null 

                ClaimsIdentity identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);

                ClaimsSecurityService.AddSignInClaimsToIdentity(0, 0, 0, 0, identity);

                var claimsPriciple = new ClaimsPrincipal(identity);
                // Make sure the Principal's are in sync
                HttpContext.User = claimsPriciple;

                Thread.CurrentPrincipal = HttpContext.User;

                AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);

                UserCookieManager.isRegisterClaims = 0;

                //Manually remove the authentication cooke it is not getting removed automatically 

                if (Request.Cookies[".AspNet.ApplicationCookie"] != null)
                {
                    Response.Cookies.Remove(".AspNet.ApplicationCookie");
                }
                // clear session cookie (not necessary for your current problem)
                HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
                cookie2.Expires = DateTime.Now.AddYears(-1);
                Response.Cookies.Add(cookie2);


                // if we redirect response to other page all the changes to cookies are lost and user will remain login 

                //if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                //{
                //    Response.Redirect("/Login");
                //}
                //else
                //{
                //   // Response.Redirect("/");

                //}
            }
        }

        //[AllowAnonymous]
        //public ActionResult ReceiptPlain(string OrderId, string StoreId, string IsPrintReceipt)
        //{

        //    MPC.Models.DomainModels.Company oCompany = _myCompanyService.GetStoreReceiptPage(Convert.ToInt64(StoreId));

        //    if (oCompany != null)
        //    {
        //        MPC.Models.DomainModels.Organisation oOrganisation = _myCompanyService.GetOrganisatonById(Convert.ToInt64(oCompany.OrganisationId));

        //        if (oCompany.ShowPrices == true)
        //        {

        //            ViewBag.IsShowPrices = true;

        //        }
        //        else
        //        {

        //            ViewBag.IsShowPrices = false;

        //        }


        //        ViewBag.TaxLabel = oCompany.TaxLabel;

        //        ViewBag.Company = oCompany;

        //        AddressViewModel oStoreDefaultAddress = null;
        //        Address StoreAddress = _myCompanyService.GetDefaultAddressByStoreID(Convert.ToInt64(StoreId));


        //        if (oCompany.isWhiteLabel == false)
        //        {
        //            oStoreDefaultAddress = null;
        //        }
        //        else
        //        {
        //            if (StoreAddress != null)
        //            {
        //                oStoreDefaultAddress = new AddressViewModel();
        //                oStoreDefaultAddress.Address1 = StoreAddress.Address1;
        //                oStoreDefaultAddress.Address2 = StoreAddress.Address2;

        //                oStoreDefaultAddress.City = StoreAddress.City;
        //                oStoreDefaultAddress.State = _myCompanyService.GetStateNameById(StoreAddress.StateId ?? 0);
        //                oStoreDefaultAddress.Country = _myCompanyService.GetCountryNameById(StoreAddress.CountryId ?? 0);
        //                oStoreDefaultAddress.ZipCode = StoreAddress.PostCode;

        //                if (!string.IsNullOrEmpty(StoreAddress.Tel1))
        //                {
        //                    oStoreDefaultAddress.Tel = StoreAddress.Tel1;
        //                }
        //            }
        //        }

        //        ViewBag.oStoreDefaultAddress = oStoreDefaultAddress;

        //        string currency = "";

        //        //if (oOrganisation != null)
        //        //{
        //        //   currency = _currencyRepository.GetCurrencySymbolById(Convert.ToInt64(oOrganisation.CurrencyId));
        //        //}

        //        if (!string.IsNullOrEmpty(currency))
        //        {
        //            ViewBag.Currency = currency;
        //        }
        //        else
        //        {
        //            ViewBag.Currency = "";
        //        }


        //    }

        //    OrderDetail order = _OrderService.GetOrderReceipt(Convert.ToInt64(OrderId));

        //    ViewBag.StoreId = StoreId;

        //    if (IsPrintReceipt == "1")
        //    {
        //        ViewBag.Print = "<script type='text/javascript'>function MyPrint() {window.print();}</script>";
        //    }
        //    else
        //    {
        //        ViewBag.Print = "";
        //    }

        //    if (oCompany.IsCustomer == (int)CustomerTypes.Corporate)
        //    {
        //        if (order != null && order.CompanyContact != null)
        //        {
        //            if (order.CompanyContact.IsPricingshown == true)
        //            {
        //                ViewBag.IsShowPrices = true;
        //            }
        //            else
        //            {
        //                ViewBag.IsShowPrices = false;
        //            }
        //        }
        //    }

        //    return View(order);
        //}

        public ActionResult AutoLoginOrRegister(string C, string F, string L, string E, string CC)
        {

            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(E, "^[A-Za-z0-9](([_\\.\\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\\.\\-]?[a-zA-Z0-9]+)*)\\.([A-Za-z]{2,})$"))
                {
                    if (!string.IsNullOrEmpty(C))
                    {
                        long OrganisationId = UserCookieManager.WEBOrganisationID;
                        if (OrganisationId == 0)
                        {
                            OrganisationId = _myCompanyService.GetOrganisationIdByRequestUrl(Convert.ToString(HttpContext.Request.Url.DnsSafeHost));
                            if (OrganisationId == 0)
                            {
                                return RedirectToAction("Error", "Home", new { Message = "Please enter valid URl." });
                            }
                            else 
                            {
                                UserCookieManager.WEBOrganisationID = OrganisationId;
                            }
                        }
                        MPC.Models.DomainModels.Company oCompany = _myCompanyService.isValidWebAccessCode(C, OrganisationId);

                        if (oCompany != null)
                        {
                            CompanyContact oContact = _myCompanyService.GetOrCreateContact(oCompany, E, F, L, C);
                            if (oContact == null && oCompany.isAllowRegistrationFromWeb == false)
                            {
                                TempData["ErrorMessage"] = "You are not allowed to register.";
                                return RedirectToAction("Error", "Home");
                            }
                            else
                            {
                                string CacheKeyName = "CompanyBaseResponse";
                                ObjectCache cache = MemoryCache.Default;
                                MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = null;
                                if ((cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>) != null && (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>).ContainsKey(oCompany.CompanyId))
                                {
                                    StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[oCompany.CompanyId];
                                }
                                else
                                {
                                    StoreBaseResopnse = _myCompanyService.GetStoreFromCache(oCompany.CompanyId);
                                }

                                if (StoreBaseResopnse.Company != null)
                                {
                                    // set company cookie
                                    UserCookieManager.WBStoreId = StoreBaseResopnse.Company.CompanyId;
                                    UserCookieManager.WEBStoreMode = StoreBaseResopnse.Company.IsCustomer;
                                    UserCookieManager.isIncludeTax = StoreBaseResopnse.Company.isIncludeVAT ?? false;
                                    UserCookieManager.TaxRate = StoreBaseResopnse.Company.TaxRate ?? 0;

                                    // set user cookies
                                    UserCookieManager.isRegisterClaims = 1;
                                    UserCookieManager.WEBContactFirstName = oContact.FirstName;
                                    UserCookieManager.WEBContactLastName = oContact.LastName == null ? "" : oContact.LastName;
                                    UserCookieManager.ContactCanEditProfile = oContact.CanUserEditProfile ?? false;
                                    UserCookieManager.ShowPriceOnWebstore = oContact.IsPricingshown ?? true;
                                    UserCookieManager.WEBEmail = oContact.Email;
                                    //Response.Cookies["WEBFirstName"].Value = oContact.FirstName;
                                    string languageName = _myCompanyService.GetUiCulture(Convert.ToInt64(StoreBaseResopnse.Company.OrganisationId));

                                    CultureInfo ci = null;

                                    if (string.IsNullOrEmpty(languageName))
                                    {
                                        languageName = "en-US";
                                    }

                                    ci = new CultureInfo(languageName);

                                    Thread.CurrentThread.CurrentUICulture = ci;
                                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
                                    // ViewBag.ResponseRedirectUrl = "/";
                                    // var action = new HomeController().Index();
                                    //return null;// View();
                                    UserCookieManager.PerformAutoLogin = true;
                                    ControllerContext.HttpContext.Response.Redirect("/");
                                    return null;
                                    //SetUserClaim(UserCookieManager.WEBOrganisationID);
                                    //List<MPC.Models.DomainModels.CmsSkinPageWidget> model = null;
                                    //ViewBag.StyleSheet = "/mpc_content/Assets/" + UserCookieManager.WEBOrganisationID + "/" + oCompany.CompanyId + "/Site.css";
                                    //model = GetWidgetsByPageName(StoreBaseResopnse.SystemPages, "", StoreBaseResopnse.CmsSkinPageWidgets, StoreBaseResopnse.StoreDetaultAddress, StoreBaseResopnse.Company.Name);
                                    // return View("Index", model);
                                }
                                else
                                {
                                    TempData["ErrorMessage"] = "Please try again.";
                                    return RedirectToAction("Error", "Home");
                                }
                            }
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Your Web Access Code is invalid.";
                            return RedirectToAction("Error", "Home");
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Please enter Web Access Code to proceed.";
                        return RedirectToAction("Error", "Home");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Please enter valid email address to proceed.";
                    return RedirectToAction("Error", "Home");
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }

}