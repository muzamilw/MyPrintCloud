using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using MPC.Webstore.Common;
using MPC.Webstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class BubbleQuickLinksController : Controller
    {
        // GET: BubbleQuickLinks


        private readonly ICompanyService _myCompanyService;
        private readonly ICampaignService _myCompainservice;
        private readonly IUserManagerService _usermanagerService;

        public BubbleQuickLinksController(ICompanyService myCompanyService, ICampaignService _myCompainservice, IUserManagerService _usermanagerService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
            this._myCompainservice = _myCompainservice;
            this._usermanagerService = _usermanagerService;
        }

        public ActionResult Index()
        {

            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            SetDefaultAddress(StoreBaseResopnse);
            if (StoreBaseResopnse.SecondaryPages != null)
            {
                if (StoreBaseResopnse.SecondaryPages.Where(p => p.PageTitle.Contains("Terms & Conditions") && p.isUserDefined == true && p.isEnabled == true).Count() > 0)
                {
                    ViewBag.TermAndCondition = StoreBaseResopnse.SecondaryPages.Where(p => p.PageTitle.Contains("Terms & Conditions") && p.isUserDefined == true && p.isEnabled == true).FirstOrDefault();
                }

                if (StoreBaseResopnse.SecondaryPages.Where(p => p.PageTitle.Contains("Privacy Policy") && p.isUserDefined == true && p.isEnabled == true).Count() > 0)
                {
                    ViewBag.PrivacyPolicy = StoreBaseResopnse.SecondaryPages.Where(p => p.PageTitle.Contains("Privacy Policy") && p.isUserDefined == true && p.isEnabled == true).FirstOrDefault();
                }
            }

            return PartialView("PartialViews/BubbleQuickLinks", StoreBaseResopnse.Company);
        }

        private void SetDefaultAddress(MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse)
        {

            AddressViewModel oAddress = null;

            if (StoreBaseResopnse.StoreDetaultAddress != null)
            {
                oAddress = new AddressViewModel();
                oAddress.AddressName = StoreBaseResopnse.StoreDetaultAddress.AddressName;
                oAddress.Address1 = StoreBaseResopnse.StoreDetaultAddress.Address1;
                oAddress.Address2 = StoreBaseResopnse.StoreDetaultAddress.Address2;

                oAddress.City = StoreBaseResopnse.StoreDetaultAddress.City;
                oAddress.State = _myCompanyService.GetStateNameById(StoreBaseResopnse.StoreDetaultAddress.StateId ?? 0);
                oAddress.Country = _myCompanyService.GetCountryNameById(StoreBaseResopnse.StoreDetaultAddress.CountryId ?? 0);
                oAddress.ZipCode = StoreBaseResopnse.StoreDetaultAddress.PostCode;

                if (!string.IsNullOrEmpty(StoreBaseResopnse.StoreDetaultAddress.Tel1))
                {
                    oAddress.Tel = "Tel: " + StoreBaseResopnse.StoreDetaultAddress.Tel1;
                }
                if (!string.IsNullOrEmpty(StoreBaseResopnse.StoreDetaultAddress.Fax))
                {
                    oAddress.Fax = "Fax: " + StoreBaseResopnse.StoreDetaultAddress.Fax;
                }
                if (!string.IsNullOrEmpty(StoreBaseResopnse.StoreDetaultAddress.Email))
                {
                    oAddress.Email = StoreBaseResopnse.StoreDetaultAddress.Email;
                }
                ViewBag.Address = oAddress;
            }
        

        }
        [HttpPost]
        public ActionResult Index(string name, string email, string comment)
        {
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);
            try
            {
                string smtpUser = null;
                string smtpserver = null;
                string smtpPassword = null;
                string fromName = null;
                string fromEmail = null;
                string Message=null;
                string MesgBody = "";
                if (StoreBaseResopnse.Organisation != null)
                {
                    //organisationResponse
                    smtpUser = StoreBaseResopnse.Organisation.SmtpUserName == null ? "" : StoreBaseResopnse.Organisation.SmtpUserName;
                    smtpserver = StoreBaseResopnse.Organisation.SmtpServer;
                    smtpPassword = StoreBaseResopnse.Organisation.SmtpPassword;
                    fromName = StoreBaseResopnse.Organisation.OrganisationName;
                    fromEmail = StoreBaseResopnse.Organisation.Email;
                }
                SystemUser EmailOFSM = _usermanagerService.GetSalesManagerDataByID(StoreBaseResopnse.Company.SalesAndOrderManagerId1.Value);
                if (EmailOFSM != null)
                {

                    MesgBody += "Dear " + EmailOFSM.FullName + ",<br>";
                    MesgBody += "An enquiry has been submitted to you with the details:<br>";
                    MesgBody += "Name: " + name + "<br>";
                    MesgBody += "Company Name: " + StoreBaseResopnse.Company.Name + "<br>";

                    MesgBody += "Email: " + email + "<br>";

                    MesgBody += "Message: " + comment + "<br>";

                  //  bool result = EmailManager.AddMsgToTblQueue(salesManager.Email, "", salesManager.FullName, MesgBody, fromName, fromEmail, smtpUser, smtpPassword, smtpserver, ddlEnqiryNature.SelectedItem.Text + " Contact enquiry from " + StoreName, null, 0);
                    bool result = _myCompainservice.AddMsgToTblQueue(EmailOFSM.Email, "", EmailOFSM.FullName, MesgBody, fromName, fromEmail, smtpUser, smtpPassword, smtpserver, Message, null, 0);
                    if (result)
                    {
                        //txtName.Text = "";
                        //txtCompany.Text = "";
                        //txtEnquiry.Text = "";
                        //txtEmail.Text = "";
                       
                    }
                    else
                    {
                       
                    }
                 
                }
                else
                {
                    throw new Exception("Critcal Error, Store Sales Manager record not available.", null);
                }
                
            }
            catch (Exception ex)
            {
                
            }

            MyCompanyDomainBaseReponse StoreBaseResopnse1 = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

            SetDefaultAddress(StoreBaseResopnse1);
            if (StoreBaseResopnse.SecondaryPages != null)
            {
                if (StoreBaseResopnse.SecondaryPages.Where(p => p.PageTitle.Contains("Terms & Conditions") && p.isUserDefined == true && p.isEnabled == true).Count() > 0)
                {
                    ViewBag.TermAndCondition = StoreBaseResopnse.SecondaryPages.Where(p => p.PageTitle.Contains("Terms & Conditions") && p.isUserDefined == true && p.isEnabled == true).FirstOrDefault();
                }

                if (StoreBaseResopnse.SecondaryPages.Where(p => p.PageTitle.Contains("Privacy Policy") && p.isUserDefined == true && p.isEnabled == true).Count() > 0)
                {
                    ViewBag.PrivacyPolicy = StoreBaseResopnse.SecondaryPages.Where(p => p.PageTitle.Contains("Privacy Policy") && p.isUserDefined == true && p.isEnabled == true).FirstOrDefault();
                }
            }

            return PartialView("PartialViews/BubbleQuickLinks", StoreBaseResopnse.Company);
        }
    }
}