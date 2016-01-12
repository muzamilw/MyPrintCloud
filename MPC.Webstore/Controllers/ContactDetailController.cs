using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
namespace MPC.Webstore.Controllers
{
    public class ContactDetailController : Controller
    {
        // GET: ContactDetail
       private readonly ICompanyService _myCompanyService;
       private readonly IWebstoreClaimsHelperService _webstoreAuthorizationChecker;

       public ContactDetailController(ICompanyService myCompanyService,IWebstoreClaimsHelperService webstoreAuthorizationChecker)
       {

          this. _myCompanyService = myCompanyService;
          this._webstoreAuthorizationChecker = webstoreAuthorizationChecker;
       
       }
       public ActionResult Index()
        {
            long OrgaanisationId = UserCookieManager.WEBOrganisationID;

            int storeMode = UserCookieManager.WEBStoreMode;
           // long contactRoleID= _myCompanyService.GetContactByID(_webstoreAuthorizationChecker.loginContactID()).ContactRoleId;
            CompanyContact contact = _myCompanyService.GetContactByID(_webstoreAuthorizationChecker.loginContactID());
            if (UserCookieManager.WEBStoreMode ==(int)StoreMode.Retail)
            {
                Company Company = _myCompanyService.GetCompanyByCompanyID(_webstoreAuthorizationChecker.loginContactCompanyID());
                if (Company != null)
                {
                    ViewBag.CompanyName = Company.Name;
                }
            }
            if (contact != null)
            {
                return View("PartialViews/ContactDetail" , contact);
            }
            else
            {
                return View();
            }
        }
       [HttpPost]
       public ActionResult Index(CompanyContact Model, HttpPostedFileBase fuImageUpload,string MarketAndPromotion,string NewsLetterSubscription)
       {
               //bool result = false;
               //CompanyContact UpdateContact = new CompanyContact();
               //UpdateContact.FirstName = Model.FirstName;
               //UpdateContact.LastName = Model.LastName;
               //UpdateContact.Email = Model.Email;
               //UpdateContact.JobTitle = Model.JobTitle;
               //UpdateContact.HomeTel1 = Model.HomeTel1;
               //UpdateContact.Mobile = Model.Mobile;
               //UpdateContact.FAX = Model.FAX;
               //UpdateContact.quickWebsite = Model.quickWebsite;
               //UpdateContact.image = UpdateImage(fuImageUpload, Model);
               //UpdateContact.ContactId = _webstoreAuthorizationChecker.loginContactID();
               //if (MarketAndPromotion.Equals("true"))
               //{
               //    UpdateContact.IsEmailSubscription = true;
               //}
               //else
               //{
               //    UpdateContact.IsEmailSubscription = false;
               //}
               //if (NewsLetterSubscription.Equals("true"))
               //{
               //    UpdateContact.IsNewsLetterSubscription = true;
               //}
               //else
               //{
               //    UpdateContact.IsNewsLetterSubscription = false;
               //}

               //if (UserCookieManager.StoreMode == (int)StoreMode.Retail)
               //{

               //    Company Company = _myCompanyService.GetCompanyByCompanyID(_webstoreAuthorizationChecker.loginContactCompanyID());
               //    if (Company != null)
               //    {
               //        Company.Name = Request.Form["txtCompanyName"].ToString();
               //        Company.CompanyId = _webstoreAuthorizationChecker.loginContactCompanyID();
               //        result = _myCompanyService.UpdateCompanyName(Company);
               //    }
               //    result = _myCompanyService.UpdateCompanyContactForRetail(UpdateContact);
               //}
               //else
               //{
               //    UpdateContact.POBoxAddress = Model.POBoxAddress;
               //    UpdateContact.CorporateUnit = Model.CorporateUnit;
               //    UpdateContact.OfficeTradingName = Model.OfficeTradingName;
               //    UpdateContact.ContractorName = Model.ContractorName;
               //    UpdateContact.BPayCRN = Model.BPayCRN;
               //    UpdateContact.ABN = Model.ABN;
               //    UpdateContact.ACN = Model.ACN;
               //    UpdateContact.AdditionalField1 = Model.AdditionalField1;
               //    UpdateContact.AdditionalField2 = Model.AdditionalField2;
               //    UpdateContact.AdditionalField3 = Model.AdditionalField3;
               //    UpdateContact.AdditionalField4 = Model.AdditionalField4;
               //    UpdateContact.AdditionalField5 = Model.AdditionalField5;
               //    UpdateContact.ContactId = _webstoreAuthorizationChecker.loginContactID();
               //    result = _myCompanyService.UpdateCompanyContactForCorporate(UpdateContact);
               //}
               //if (result)
               //{
               //    ViewBag.Message = "Your profile updated successfully.";
               //}
               //else
               //{
               //    ViewBag.Message = "Sorry, no profile updated.";
               //}
               UserCookieManager.WEBContactFirstName = Model.FirstName;

               UserCookieManager.WEBContactLastName = Model.LastName;

              return View("PartialViews/ContactDetail", Model);
       }

        private string UpdateImage(HttpPostedFileBase Request, CompanyContact Model)
        {
            string ImagePath=string.Empty;
            CompanyContact contact = _myCompanyService.GetContactByID(_webstoreAuthorizationChecker.loginContactID());
            if (Request != null)
            {
                string folderPath = "/mpc_content/Assets" + "/" + UserCookieManager.WEBOrganisationID + "/" + UserCookieManager.WBStoreId + "/Contacts/" + contact.ContactId + "";
                string virtualFolderPth = string.Empty;

                virtualFolderPth = @Server.MapPath(folderPath);
                if (!System.IO.Directory.Exists(virtualFolderPth))
                {
                    System.IO.Directory.CreateDirectory(virtualFolderPth);
                }
                if (contact.image != null || contact.image != "")
                {
                    
                        RemovePreviousFile(contact.image);
                }
                var fileName = Path.GetFileName(Request.FileName);

                Request.SaveAs(virtualFolderPth + "/" + fileName);

                ImagePath = folderPath + "/" + fileName;
            }
            else
            {
                ImagePath = contact.image;
            }

            return ImagePath;
        }

        private void RemovePreviousFile(string previousFileToremove)
        {
            if (!string.IsNullOrEmpty(previousFileToremove))
            {
                if (System.IO.File.Exists(previousFileToremove))
                {
                    DeleteFile(previousFileToremove);
                }
            }
        }
        [HttpPost]
        public void Update(CompanyContact Model, HttpPostedFileBase fuImageUpload)
        {
               bool result = false;
               CompanyContact UpdateContact = new CompanyContact();
               UpdateContact.FirstName = Model.FirstName;
               UpdateContact.LastName = Model.LastName;
               UpdateContact.Email = Model.Email;
               UpdateContact.JobTitle = Model.JobTitle;
               UpdateContact.HomeTel1 = Model.HomeTel1;
               UpdateContact.Mobile = Model.Mobile;
               UpdateContact.FAX = Model.FAX;
               UpdateContact.quickWebsite = Model.quickWebsite;
               UpdateContact.image = UpdateImage(fuImageUpload, Model);
               UpdateContact.ContactId = _webstoreAuthorizationChecker.loginContactID();
         

               if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
               {

                   Company Company = _myCompanyService.GetCompanyByCompanyID(_webstoreAuthorizationChecker.loginContactCompanyID());
                   if (Company != null)
                   {
                       Company.Name = Request.Form["txtCompanyName"].ToString();
                       Company.CompanyId = _webstoreAuthorizationChecker.loginContactCompanyID();
                       result = _myCompanyService.UpdateCompanyName(Company);
                   }
                   result = _myCompanyService.UpdateCompanyContactForRetail(UpdateContact);
               }
               else
               {
                   UpdateContact.POBoxAddress = Model.POBoxAddress;
                   UpdateContact.CorporateUnit = Model.CorporateUnit;
                   UpdateContact.OfficeTradingName = Model.OfficeTradingName;
                   UpdateContact.ContractorName = Model.ContractorName;
                   UpdateContact.BPayCRN = Model.BPayCRN;
                   UpdateContact.ABN = Model.ABN;
                   UpdateContact.ACN = Model.ACN;
                   UpdateContact.AdditionalField1 = Model.AdditionalField1;
                   UpdateContact.AdditionalField2 = Model.AdditionalField2;
                   UpdateContact.AdditionalField3 = Model.AdditionalField3;
                   UpdateContact.AdditionalField4 = Model.AdditionalField4;
                   UpdateContact.AdditionalField5 = Model.AdditionalField5;
                   UpdateContact.ContactId = _webstoreAuthorizationChecker.loginContactID();
                   result = _myCompanyService.UpdateCompanyContactForCorporate(UpdateContact);
               }
               if (result)
               {
                   ViewBag.Message = "Your profile updated successfully.";
               }
               else
               {
                   ViewBag.Message = "Sorry, no profile updated.";
               }

             
        
        }
       
          public static void DeleteFile(string completePath)
          {
            try
            {
                if (System.IO.File.Exists(completePath))
                {
                    System.IO.File.Delete(completePath);
                }
            }
            catch (Exception ex)
            {
                  throw ex;
            }

           }
        }
    }
