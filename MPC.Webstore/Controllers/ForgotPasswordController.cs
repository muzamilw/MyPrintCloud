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
            string Email = model.Email;

            long storeId = Convert.ToInt64(Session["storeId"]);

            MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();

            if (baseResponse.Company.IsCustomer == (int)StoreMode.Corp)
            {
                curUser =  _myCompanyService.GetContactByEmailAndMode(Email, Convert.ToInt32(CustomerTypes.Corporate), (int)baseResponse.Company.CompanyId);
            }
            else
            {
                curUser = _myCompanyService.GetContactByEmail(Email);
            }
            SendEmail(Email,baseResponse); 
            return View("PartialViews/ForgotPassword");
        }

        private void SendEmail(string Email, MyCompanyDomainBaseResponse companyDomain)
        {
          
            CampaignEmailParams objtCEP = new CampaignEmailParams();
            objtCEP.CompanySiteID = 1;
             
                if (curUser != null)
                {
                   
                    Counter = 1;
                    string autoGeneratedPss = RandomString(4);
                    string encryptedPass =  _myCompanyService.GeneratePasswordHash(autoGeneratedPss);
                    _myCompanyService.UpdateUserPassword((int)curUser.ContactId, encryptedPass);
                    objtCEP.ContactId = (int)curUser.ContactId;
                    Campaign ForgotPasswordCampaign = _campaignService.GetCampaignRecordByEmailEvent((int)Events.ForgotPassword);
                    CompanyContact CustomerEmailAcc = _myCompanyService.GetContactByEmail(Email);
                    bool result = false;
                   
                    objtCEP.SalesManagerContactID = CustomerEmailAcc.ContactId;
                    objtCEP.StoreID = (int)companyDomain.Company.OrganisationId;
                    if (companyDomain.Company.IsCustomer == (int)StoreMode.Corp)
                    {
                         objtCEP.CompanyId = (int)companyDomain.Company.CompanyId;
                         objtCEP.AddressID = companyDomain.Company.CompanyId;
                        SystemUser EmailOFSM = _UserManagerService.GetSalesManagerDataByID(Convert.ToInt32(companyDomain.Organisation.OrganisationId));
                          result =  _campaignService.emailBodyGenerator(ForgotPasswordCampaign, null, objtCEP, CustomerEmailAcc, StoreMode.Corp , (int)companyDomain.Organisation.OrganisationId, autoGeneratedPss, "", "", EmailOFSM.Email, "", "", null, "");
                     }
                    else
                    {
                         objtCEP.CompanyId = (int)companyDomain.Company.CompanyId;
                         objtCEP.AddressID = companyDomain.Company.CompanyId;

                        SystemUser EmailOFSM = _UserManagerService.GetSalesManagerDataByID(Convert.ToInt32(companyDomain.Company.OrganisationId));
                          result =  _campaignService.emailBodyGenerator(ForgotPasswordCampaign, null, objtCEP, CustomerEmailAcc, StoreMode.Retail , (int)companyDomain.Company.OrganisationId, autoGeneratedPss, "", "", EmailOFSM.Email, "", "", null, "");
                    }
                   
                    if (result)
                    {

                        ViewBag.Message = "Your new password is sent to given e-mail address. Please check your email"; 
                        // lblMessage.Text = "Your new password is sent to given e-mail address. Please check your email";
                       // lblMessage.Text = (string)GetGlobalResourceObject("MyResource", "hfPsswordSent");
                    }
                    else
                    {
                        ViewBag.Message = "Email not sent. please try again!"; 
                      //  lblMessage.Visible = true;
                        //lblMessage.Text = "Email not sent. please try again!";
                       // lblMessage.Text = (string)GetGlobalResourceObject("MyResource", "hfEmailNotSent");
                    }
                  
                }
                else
                {
                    //txtEmail.Visible = true;
                    //lblEmail.Visible = false;
                    ////lblMessage.Text = "Your e-mail address is not registered.";
                    //lblMessage.Text = (string)GetGlobalResourceObject("MyResource", "hfEmailNorRegistered");
                    ViewBag.Message = "Your e-mail address is not registered."; 
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