﻿using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Webstore.ResponseModels;
using MPC.Webstore.ModelMappers;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using System.Text;
using MPC.Webstore.Models;
using MPC.Webstore.Common;
using System.Runtime.Caching;

namespace MPC.Webstore.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly ICompanyService _myCompanyService;
        private readonly ICampaignService _campaignService;
        private readonly IUserManagerService _UserManagerService;

        private CompanyContact curUser = new CompanyContact();
        private int Counter;
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ForgotPasswordController(ICompanyService myCompanyService, ICampaignService campaignService, IUserManagerService userManagerService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
            this._campaignService = campaignService;
            this._UserManagerService = userManagerService;

        }

        #endregion

        // GET: ForgotPassword
        public ActionResult Index()
        {

            return View("PartialViews/ForgotPassword");
        }

        [HttpPost]
        public ActionResult Index(ForgotPasswordViewModel model)
        {


            if (!string.IsNullOrEmpty(model.Email))
            {
                string CacheKeyName = "CompanyBaseResponse";
                ObjectCache cache = MemoryCache.Default;

                MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.StoreId];
                if (StoreBaseResopnse.Company.IsCustomer == (int)StoreMode.Corp)
                {
                    curUser = _myCompanyService.GetContactByEmailAndMode(model.Email, Convert.ToInt32(CustomerTypes.Corporate), StoreBaseResopnse.Company.CompanyId);
                }
                else
                {
                    curUser = _myCompanyService.GetContactByEmail(model.Email, StoreBaseResopnse.Organisation.OrganisationId);
                }
                SendEmail(model.Email, StoreBaseResopnse);
                StoreBaseResopnse = null;
            }


            return View("PartialViews/ForgotPassword");
        }

        private void SendEmail(string Email, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse companyDomain)
        {

          
            if (curUser != null)
            {
                  CampaignEmailParams objtCEP = new CampaignEmailParams();

                    objtCEP.CompanySiteID = companyDomain.Company.OrganisationId ?? 0;

                Counter = 1;
                string autoGeneratedPss = RandomString(4);
                string encryptedPass = _myCompanyService.GeneratePasswordHash(autoGeneratedPss);
                _myCompanyService.UpdateUserPassword((int)curUser.ContactId, encryptedPass);
                objtCEP.ContactId = curUser.ContactId;
                Campaign ForgotPasswordCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.ForgotPassword);
                CompanyContact CustomerEmailAcc = _myCompanyService.GetContactByEmail(Email, companyDomain.Organisation.OrganisationId);
                bool result = false;
                Address CompanyDefaultAddress = _myCompanyService.GetDefaultAddressByStoreID(UserCookieManager.StoreId);
                if (CompanyDefaultAddress != null)
                {
                    objtCEP.AddressID = CompanyDefaultAddress.AddressId;
                }
                else 
                {
                    objtCEP.AddressID = 0;
                }
                objtCEP.SalesManagerContactID = CustomerEmailAcc.ContactId;
                objtCEP.StoreID = UserCookieManager.StoreId;
                if (companyDomain.Company.IsCustomer == (int)StoreMode.Corp)
                {
                    objtCEP.CompanyId = (int)companyDomain.Company.CompanyId;
                  
                    SystemUser EmailOFSM = _UserManagerService.GetSalesManagerDataByID(companyDomain.Company.SalesAndOrderManagerId1.Value);
                    result = _campaignService.emailBodyGenerator(ForgotPasswordCampaign, objtCEP, CustomerEmailAcc, StoreMode.Corp, (int)companyDomain.Organisation.OrganisationId, autoGeneratedPss, "", "", EmailOFSM.Email, "", "", null, "");
                }
                else
                {
                    objtCEP.CompanyId = (int)companyDomain.Company.CompanyId;
                   

                    SystemUser EmailOFSM = _UserManagerService.GetSalesManagerDataByID(companyDomain.Company.SalesAndOrderManagerId1.Value);
                    result = _campaignService.emailBodyGenerator(ForgotPasswordCampaign, objtCEP, CustomerEmailAcc, StoreMode.Retail, (int)companyDomain.Company.OrganisationId, autoGeneratedPss, "", "", EmailOFSM.Email, "", "", null, "");
                }

                if (result)
                {
                    ViewBag.Message = Utils.GetKeyValueFromResourceFile("hfPsswordSent", UserCookieManager.StoreId); //"Your new password is sent to given e-mail address. Please check your email";
                   
                }
                else
                {
                    ViewBag.Message = Utils.GetKeyValueFromResourceFile("hfEmailNotSent", UserCookieManager.StoreId); //"Email not sent. please try again!";
                
                }

            }
            else
            {
              
                ViewBag.Message = Utils.GetKeyValueFromResourceFile("hfEmailNorRegistered", UserCookieManager.StoreId); //"Your e-mail address is not registered.";
                Counter = 0;
            }

        }

        public string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            string password = builder.ToString().ToLower();
            return password;
        }
    }
}