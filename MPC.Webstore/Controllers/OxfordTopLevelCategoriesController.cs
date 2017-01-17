using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class OxfordTopLevelCategoriesController : Controller
    {
        private readonly ICompanyService _myCompanyService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        
        private readonly ICampaignService _campaignService;
        private readonly IUserManagerService _UserManagerService;

        public OxfordTopLevelCategoriesController(ICompanyService _myCompanyService, IWebstoreClaimsHelperService _myClaimHelper, ICampaignService _campaignService, IUserManagerService _UserManagerService)
        {
            this._myCompanyService = _myCompanyService;
            this._myClaimHelper = _myClaimHelper;
            this._campaignService = _campaignService;
            this._UserManagerService = _UserManagerService;
        }
        // GET: OxfordTopLevelCategories
        public ActionResult Index()
        {
            GetRaReview();
            List<ProductCategory> lstParentCategories = new List<ProductCategory>();
            List<ProductCategory> AllRetailCat = new List<ProductCategory>();
            MPC.Models.DomainModels.Company model = null;
            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;
            //   MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();
            //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
            {
                Int64 roleid = _myClaimHelper.loginContactRoleID();

                if (_myClaimHelper.loginContactID() != 0 && roleid == Convert.ToInt32(Roles.Adminstrator))
                {
                    lstParentCategories = _myCompanyService.GetAllParentCorporateCatalog((int)_myClaimHelper.loginContactCompanyID());
                }
                else
                {
                    lstParentCategories = _myCompanyService.GetAllParentCorporateCatalogByTerritory((int)_myClaimHelper.loginContactCompanyID(), (int)_myClaimHelper.loginContactID());
                }
                ViewBag.AllRetailCat = _myCompanyService.GetAllCategories(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID).ToList();
            }
            else
            {

             //   List<ProductCategory> AllCategAllRetailCatroies = new List<ProductCategory>();
                //List<ProductCategory> ChildCategories = new List<ProductCategory>();
               // AllCategroies = _myCompanyService.GetAllCategories(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID);
                //SeablueToCategories
                ViewBag.lstParentCategories = _myCompanyService.GetStoreParentCategories(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID).OrderBy(i => i.DisplayOrder).ToList();
                //rptRetroPCats
                // ViewBag.AllRetailCat = _myCompanyService.GetAllRetailPublishedCat().Where(i => i.ParentCategoryId == null || i.ParentCategoryId == 0).OrderBy(g => g.DisplayOrder).ToList();
                ViewBag.AllRetailCat = _myCompanyService.GetAllCategories(UserCookieManager.WBStoreId, UserCookieManager.WEBOrganisationID).ToList();
            }
            if (StoreBaseResopnse.StoreDetaultAddress != null)
            {
                string State = _myCompanyService.GetStateNameById(StoreBaseResopnse.StoreDetaultAddress.StateId ?? 0);
                string Country = _myCompanyService.GetCountryNameById(StoreBaseResopnse.StoreDetaultAddress.CountryId ?? 0);
                ViewBag.companynameInnerText = StoreBaseResopnse.Company.Name;
                ViewBag.addressline1InnerHtml = StoreBaseResopnse.StoreDetaultAddress.Address1 + "<br/>" + StoreBaseResopnse.StoreDetaultAddress.Address2;
                ViewBag.cityandCodeInnerText = StoreBaseResopnse.StoreDetaultAddress.City + " " + StoreBaseResopnse.StoreDetaultAddress.PostCode;
                ViewBag.stateandCountryInnerText = State + " " + Country;
                ViewBag.telnoInnerText = StoreBaseResopnse.StoreDetaultAddress.Tel1;
                ViewBag.emailaddInnerText = StoreBaseResopnse.StoreDetaultAddress.Email;
            }
            
            return PartialView("PartialViews/OxfordTopLevelCategories");
        }

        public void GetRaReview()
        {
            RaveReview resultOfReviews = _myCompanyService.GetRaveReview(UserCookieManager.WBStoreId);
            if (resultOfReviews != null)
            {
                ViewBag.lblRaveReview = "<br /> " + resultOfReviews.Review + "<br /> Best regards,<br /> &nbsp;";
                ViewBag.lblReviewBy = resultOfReviews.ReviewBy;
            }
            else
            {
                ViewBag.lblRaveReview = "I used Company services for my business cards and I must tell that I am much&nbsp; pleased with the quality of printed cards their prompt and professional service. Good luck to your business.<br />Best regards,<br />&nbsp;";
                ViewBag.lblReviewBy = "Henry Roberts";
            }
        }

        [HttpPost]
        public JsonResult SubmitSubscribeData(string txtEmailbox)
        {
            string Message = string.Empty;
            try
            {
                NewsLetterSubscriber subscriber = _myCompanyService.GetSubscriber(txtEmailbox, UserCookieManager.WBStoreId);
                if (subscriber == null)
                {
                    //string CacheKeyName = "CompanyBaseResponse";
                    //ObjectCache cache = MemoryCache.Default;


                    //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
                    MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

                    string SubscriberEmail = "";
                    string subscriptionCode = Guid.NewGuid().ToString();

                    CampaignEmailParams CEP = new CampaignEmailParams();
                    CompanyContact Contact = null;
                    if (!string.IsNullOrEmpty(txtEmailbox))
                    {
                        Contact = _myCompanyService.GetContactByEmail(txtEmailbox, StoreBaseResopnse.Organisation.OrganisationId, UserCookieManager.WBStoreId);
                    }
                    Campaign SubscriptionCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.SubscriptionConfirmation, StoreBaseResopnse.Company.OrganisationId ?? 0, UserCookieManager.WBStoreId);
                    SystemUser EmailOFSM = _UserManagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);

                    subscriber = new NewsLetterSubscriber();
                    subscriber.Status = (int)SubscriberStatus.Pending;
                    subscriber.SubscribeDate = DateTime.Now;
                    subscriber.SubscriptionCode = subscriptionCode;
                    subscriber.Email = txtEmailbox;
                    subscriber.ContactCompanyID = Convert.ToInt32(UserCookieManager.WBStoreId);

                    CEP.OrganisationId = StoreBaseResopnse.Organisation.OrganisationId;

                    if (Contact != null)
                    {
                        subscriber.ContactId = Contact.ContactId;
                        CEP.ContactId = Contact.ContactId;
                        CEP.CompanyId = Contact.CompanyId;
                        CEP.SalesManagerContactID = Contact.ContactId;
                        CEP.StoreId = UserCookieManager.WBStoreId;

                    }
                    else
                    {
                        SubscriberEmail = txtEmailbox;
                        // should be greater than one to resolve variaables
                        CEP.SalesManagerContactID = 1;
                        CEP.StoreId = UserCookieManager.WBStoreId;

                    }
                    string basePath = Request.Url.ToString();
                    string path = basePath.Substring(0, basePath.LastIndexOf('/') + 1);
                    string subscriptionLink = path + "ConfirmSubscription.aspx?" + "SubscriptionCode=" + subscriptionCode;

                    CEP.SubscriberID = _myCompanyService.AddSubscriber(subscriber);
                    _campaignService.emailBodyGenerator(SubscriptionCampaign, CEP, Contact, StoreMode.Retail, (int)StoreBaseResopnse.Company.OrganisationId, "", "", SubscriberEmail, EmailOFSM.Email, "", "", null, "", null, "", "", subscriptionLink);
                    string sConfirmation = Utils.GetKeyValueFromResourceFile("ConfirmSubscriptionMesg", UserCookieManager.WBStoreId);
                    if (string.IsNullOrEmpty(sConfirmation))
                    {
                        Message = "To confirm your subscription please follow instructions which have been sent to provided email.";
                    }
                    else
                    {
                        Message = sConfirmation;
                    }



                }
                else
                {
                    string sConfirmation = Utils.GetKeyValueFromResourceFile("SubscriptionErrorMesg", UserCookieManager.WBStoreId);
                    if (string.IsNullOrEmpty(sConfirmation))
                    {
                        Message = "Someone is already subscribed with provided email. Please use a different email.";
                    }
                    else
                    {
                        Message = sConfirmation;
                    }
                }
            }
            catch (Exception ex)
            {
                Message = "Error in subscription. Please try again.";
                throw ex;

            }
            return Json(new { ErrorMessage = Message });
            //return PartialView("PartialViews/OxfordTopLevelCategories");

        }

       
    }
}