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
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        public BubbleQuickLinksController(ICompanyService myCompanyService, ICampaignService _myCompainservice,
            IUserManagerService _usermanagerService
            , IWebstoreClaimsHelperService myClaimHelper)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
            this._myCompainservice = _myCompainservice;
            this._usermanagerService = _usermanagerService;
            this._myClaimHelper = myClaimHelper;
        }

        public ActionResult Index()
        {

            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);
            if (_myClaimHelper.loginContactID() > 0)
            {
                ViewBag.IsLogin = 1;
            }
            else 
            {
                ViewBag.IsLogin = 0;
            }
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
            if (StoreBaseResopnse.Company.isDisplaySecondaryPages == true)
            {
                ViewBag.Display = "1";

                List<PageCategory> oPageCategories = StoreBaseResopnse.PageCategories.ToList();
                List<PageCategory> oPageUpdateCategories = new List<PageCategory>();
                foreach (PageCategory opageC in oPageCategories)
                {
                    if (StoreBaseResopnse.SecondaryPages != null && StoreBaseResopnse.SecondaryPages.Where(p => p.CategoryId == opageC.CategoryId).ToList().Count() > 0)
                    {
                        oPageUpdateCategories.Add(opageC);
                    }
                }
                if (oPageCategories != null && oPageCategories.Count() > 1)
                {
                    ViewData["PageCategory"] = oPageUpdateCategories.Take(1).ToList();
                }
                else
                {
                    ViewData["PageCategory"] = oPageUpdateCategories.ToList();
                }

                ViewData["CmsPage"] = StoreBaseResopnse.SecondaryPages;
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
                if (_myClaimHelper.loginContactID() > 0)
                {
                    ViewBag.IsLogin = 1;
                }
                else
                {
                    ViewBag.IsLogin = 0;
                }
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
                if (StoreBaseResopnse.Company.isDisplaySecondaryPages == true)
                {
                    ViewBag.Display = "1";

                    List<PageCategory> oPageCategories = StoreBaseResopnse.PageCategories.ToList();
                    List<PageCategory> oPageUpdateCategories = new List<PageCategory>();
                    foreach (PageCategory opageC in oPageCategories)
                    {
                        if (StoreBaseResopnse.SecondaryPages != null && StoreBaseResopnse.SecondaryPages.Where(p => p.CategoryId == opageC.CategoryId).ToList().Count() > 0)
                        {
                            oPageUpdateCategories.Add(opageC);
                        }
                    }
                    if (oPageCategories != null && oPageCategories.Count() > 1)
                    {
                        ViewData["PageCategory"] = oPageUpdateCategories.Take(1).ToList();
                    }
                    else
                    {
                        ViewData["PageCategory"] = oPageUpdateCategories.ToList();
                    }

                    ViewData["CmsPage"] = StoreBaseResopnse.SecondaryPages;
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