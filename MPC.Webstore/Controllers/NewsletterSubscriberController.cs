﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using MPC.Webstore.Models;
using MPC.Models.Common;
using System.Runtime.Caching;

namespace MPC.Webstore.Controllers
{
    public class NewsletterSubscriberController : Controller
    {
        #region Private

        private readonly ICompanyService _myCompanyService;
        private readonly ICampaignService _campaignService;
        private readonly IUserManagerService _UserManagerService;

        #endregion

           #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public NewsletterSubscriberController(ICompanyService myCompanyService, ICampaignService campaignService, IUserManagerService userManagerService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._campaignService = campaignService;
            this._myCompanyService = myCompanyService;
            this._UserManagerService = userManagerService;
           
        }

        #endregion
        // GET: NewsletterSubscriber
        public ActionResult Index()
        {
            return PartialView("PartialViews/NewsletterSubscriber");
         
        }

        [HttpPost]
        public ActionResult Index(string txtEmailbox)
        {
            try
            {


                 NewsLetterSubscriber subscriber = _myCompanyService.GetSubscriber(txtEmailbox, UserCookieManager.StoreId);

                
                if (subscriber == null)
                {
                    string CacheKeyName = "CompanyBaseResponse";
                    ObjectCache cache = MemoryCache.Default;


                    MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.StoreId];

                    string SubscriberEmail = "";
                    string subscriptionCode = Guid.NewGuid().ToString();

                    CampaignEmailParams CEP = new CampaignEmailParams();
                    CompanyContact Contact = null;
                    if (!string.IsNullOrEmpty(txtEmailbox))
                    {
                        Contact = _myCompanyService.GetContactByEmail(txtEmailbox, StoreBaseResopnse.Organisation.OrganisationId);
                    }
                    Campaign SubscriptionCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.SubscriptionConfirmation);
                    SystemUser EmailOFSM = _UserManagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);

                    subscriber = new NewsLetterSubscriber();
                    subscriber.Status = (int)SubscriberStatus.Pending;
                    subscriber.SubscribeDate = DateTime.Now;
                    subscriber.SubscriptionCode = subscriptionCode;
                    subscriber.Email = txtEmailbox;
                    subscriber.ContactCompanyID = Convert.ToInt32(UserCookieManager.StoreId);

                    CEP.CompanySiteID = StoreBaseResopnse.Organisation.OrganisationId;

                    if (Contact != null)
                    {
                        subscriber.ContactId = Contact.ContactId;
                        CEP.ContactId = Contact.ContactId;
                        CEP.CompanyId = Contact.CompanyId;
                        CEP.SalesManagerContactID = Contact.ContactId;
                        CEP.StoreID = UserCookieManager.StoreId;

                    }
                    else
                    {
                        SubscriberEmail = txtEmailbox;
                        // should be greater than one to resolve variaables
                        CEP.SalesManagerContactID = 1;
                        CEP.StoreID = UserCookieManager.StoreId;


                    }
                    string basePath = Request.Url.ToString();
                    string path = basePath.Substring(0, basePath.LastIndexOf('/') + 1);
                    string subscriptionLink = path + "ConfirmSubscription.aspx?" + "SubscriptionCode=" + subscriptionCode;

                    CEP.SubscriberID = _myCompanyService.AddSubscriber(subscriber);
                    _campaignService.emailBodyGenerator(SubscriptionCampaign, CEP, Contact, StoreMode.Retail, (int)StoreBaseResopnse.Company.OrganisationId, "", "", SubscriberEmail, EmailOFSM.Email, "", "", null, "", null, "", "", subscriptionLink);
                    string sConfirmation = Utils.GetKeyValueFromResourceFile("ConfirmSubscriptionMesg", UserCookieManager.StoreId);
                    if (string.IsNullOrEmpty(sConfirmation))
                    {
                        ViewBag.Message = "To confirm your subscription please follow instructions which have been sent to provided email.";
                    }
                    else
                    {
                        ViewBag.Message = sConfirmation;
                    }



                }
                else
                {
                    string sConfirmation = Utils.GetKeyValueFromResourceFile("SubscriptionErrorMesg", UserCookieManager.StoreId);
                    if (string.IsNullOrEmpty(sConfirmation))
                    {
                        ViewBag.Message = "Someone is already subscribed with provided email. Please use a different email.";
                    }
                    else
                    {
                        ViewBag.Message = sConfirmation;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error in subscription. Please try again.";
                throw ex;

            }
            return PartialView("PartialViews/NewsletterSubscriber");
         
        }
    }
}