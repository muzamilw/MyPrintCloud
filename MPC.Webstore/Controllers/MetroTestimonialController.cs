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
    public class MetroTestimonialController : Controller
    {
        private readonly ICompanyService _MyCompanyService;
        private readonly ICampaignService _campaignService;
        private readonly IUserManagerService _usermanagerService;
        // GET: MetroTestimonial
        public MetroTestimonialController(ICompanyService _MyCompanyService, ICampaignService _campaignService, IUserManagerService _usermanagerService)
        {
            this._MyCompanyService = _MyCompanyService;
            this._campaignService = _campaignService;
            this._usermanagerService = _usermanagerService;
        }
        public ActionResult Index()
        {
            string best = Utils.GetKeyValueFromResourceFile("ltrlbestregard", UserCookieManager.WBStoreId, "");

            Messages();
            RaveReview resultOfReviews = _MyCompanyService.GetRaveReview();
            if (resultOfReviews != null)
            {
                ViewBag.lblRaveReview = "<br /> " + resultOfReviews.Review + "<br />,<br /> &nbsp;";
               ViewBag.lblReviewBy = resultOfReviews.ReviewBy;
            }
            else
            {
                ViewBag.lblRaveReview = "I used Company services for my business cards and I must tell that I am much&nbsp; pleased with the quality of printed cards their prompt and professional service. Good luck to your business.<br />Best regards,<br />&nbsp;";
                ViewBag.lblReviewBy = "Henry Roberts";
            }

            return PartialView("PartialViews/MetroTestimonial");
        }
        
        [HttpPost]
        public ActionResult Index(string txtEmailbox)
        {
            try
            {
                Rebind();
                NewsLetterSubscriber subscriber = _MyCompanyService.GetSubscriber(txtEmailbox, UserCookieManager.WBStoreId);
                if (subscriber == null)
                {
                    //string CacheKeyName = "CompanyBaseResponse";
                    //ObjectCache cache = MemoryCache.Default;

                    //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
                    MyCompanyDomainBaseReponse StoreBaseResopnse = _MyCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

                    string SubscriberEmail = "";
                    string subscriptionCode = Guid.NewGuid().ToString();

                    CampaignEmailParams CEP = new CampaignEmailParams();
                    CompanyContact Contact = null;
                    if (!string.IsNullOrEmpty(txtEmailbox))
                    {
                        Contact = _MyCompanyService.GetContactByEmail(txtEmailbox, StoreBaseResopnse.Organisation.OrganisationId, UserCookieManager.WBStoreId);
                    }
                    Campaign SubscriptionCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.SubscriptionConfirmation, StoreBaseResopnse.Company.OrganisationId ?? 0, UserCookieManager.WBStoreId);
                    SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);

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

                    CEP.SubscriberID = _MyCompanyService.AddSubscriber(subscriber);
                    _campaignService.emailBodyGenerator(SubscriptionCampaign, CEP, Contact, StoreMode.Retail, (int)StoreBaseResopnse.Company.OrganisationId, "", "", SubscriberEmail, EmailOFSM.Email, "", "", null, "", null, "", "", subscriptionLink);
                    string sConfirmation = Utils.GetKeyValueFromResourceFile("ConfirmSubscriptionMesg", UserCookieManager.WBStoreId);
                    if (string.IsNullOrEmpty(sConfirmation))
                    {
                        ViewBag.ErrorMessage = "To confirm your subscription please follow instructions which have been sent to provided email.";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = sConfirmation;
                    }



                }
                else
                {
                    string sConfirmation = Utils.GetKeyValueFromResourceFile("SubscriptionErrorMesg", UserCookieManager.WBStoreId);
                    if (string.IsNullOrEmpty(sConfirmation))
                    {
                        ViewBag.ErrorMessage = "Someone is already subscribed with provided email. Please use a different email.";
                    }
                    else
                    {
                        ViewBag.ErrorMessage=sConfirmation;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage="Error in subscription. Please try again.";
                throw ex;

            }
            return PartialView("PartialViews/MetroTestimonial");
        }

        private void Messages()
        {
            ViewBag.SendBtntext=Utils.GetKeyValueFromResourceFile("btnSend", UserCookieManager.WBStoreId);
            string sDesc = Utils.GetKeyValueFromResourceFile("NewsLetterDescription", UserCookieManager.WBStoreId);
            
            if (!string.IsNullOrEmpty(sDesc))
              ViewBag.Nwsdesc = sDesc;
            else
            {
                ViewBag.Nwsdesc = "Sign up to our newsletters, and you’ll get a wealth of business tips, inspirational ideas, exclusive special offers and discounts";
            }

            sDesc = Utils.GetKeyValueFromResourceFile("SubScribeText", UserCookieManager.WBStoreId);
            if (!string.IsNullOrEmpty(sDesc))
               ViewBag.lblOurNews = sDesc;
            else
            {
                ViewBag.lblOurNews = "SUBSCRIBE To Our Newsletter";
            }

        
        }

        private void Rebind()
        {
            Messages();
            RaveReview resultOfReviews = _MyCompanyService.GetRaveReview();
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
        public JsonResult SubmitTestimonialData(string txtEmailbox)
        {
            string Message = string.Empty;
            try
            {
                Rebind();
                NewsLetterSubscriber subscriber = _MyCompanyService.GetSubscriber(txtEmailbox, UserCookieManager.WBStoreId);
                if (subscriber == null)
                {
                    //string CacheKeyName = "CompanyBaseResponse";
                    //ObjectCache cache = MemoryCache.Default;

                    //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
                    MyCompanyDomainBaseReponse StoreBaseResopnse = _MyCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

                    string SubscriberEmail = "";
                    string subscriptionCode = Guid.NewGuid().ToString();

                    CampaignEmailParams CEP = new CampaignEmailParams();
                    CompanyContact Contact = null;
                    if (!string.IsNullOrEmpty(txtEmailbox))
                    {
                        Contact = _MyCompanyService.GetContactByEmail(txtEmailbox, StoreBaseResopnse.Organisation.OrganisationId, UserCookieManager.WBStoreId);
                    }
                    Campaign SubscriptionCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.SubscriptionConfirmation, StoreBaseResopnse.Company.OrganisationId ?? 0, UserCookieManager.WBStoreId);
                    SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);

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

                    CEP.SubscriberID = _MyCompanyService.AddSubscriber(subscriber);
                    _campaignService.emailBodyGenerator(SubscriptionCampaign, CEP, Contact, StoreMode.Retail, (int)StoreBaseResopnse.Company.OrganisationId, "", "", SubscriberEmail, EmailOFSM.Email, "", "", null, "", null, "", "", subscriptionLink);
                    string sConfirmation = Utils.GetKeyValueFromResourceFile("ConfirmSubscriptionMesg", UserCookieManager.WBStoreId);
                    if (string.IsNullOrEmpty(sConfirmation))
                    {
                        Message= "To confirm your subscription please follow instructions which have been sent to provided email.";
                    }
                    else
                    {
                        Message= sConfirmation;
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
        }

    }
}