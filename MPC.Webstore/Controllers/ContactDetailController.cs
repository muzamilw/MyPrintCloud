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
            
            CompanyContact contact = _myCompanyService.GetContactByID(_webstoreAuthorizationChecker.loginContactID());
            Company Company = _myCompanyService.GetCompanyByCompanyID(_webstoreAuthorizationChecker.loginContactCompanyID());
            if (Company != null)
            {
                ViewBag.CompanyName = Company.Name;
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

           var value = Model.IsEmailSubscription;
           if (UserCookieManager.StoreMode == (int)StoreMode.Retail)
           {
               CompanyContact UpdateContact = new CompanyContact();
               UpdateContact.FirstName = Model.FirstName;
               UpdateContact.LastName = Model.LastName;
               UpdateContact.Email = Model.Email;
               UpdateContact.JobTitle = Model.JobTitle;
               UpdateContact.HomeTel1 = Model.HomeTel1;
               UpdateContact.Mobile = Model.Mobile;
               UpdateContact.FAX = Model.FAX;
               UpdateContact.quickWebsite = Model.quickWebsite;
               UpdateContact.image = UpdateImage(fuImageUpload,Model);
               if (MarketAndPromotion.Equals("true"))
               {
                   UpdateContact.IsEmailSubscription = true;
               }
               else
               {
                   UpdateContact.IsEmailSubscription = false;
               }
               if (NewsLetterSubscription.Equals("true"))
               {
                   UpdateContact.IsNewsLetterSubscription = true;
               }
               else
               {
                   UpdateContact.IsNewsLetterSubscription = false;
               }

               UpdateContact.ContactId = _webstoreAuthorizationChecker.loginContactID();
               Company Company = _myCompanyService.GetCompanyByCompanyID(_webstoreAuthorizationChecker.loginContactCompanyID());
               if (Company != null)
               {
                   Company.Name = Request.Form["txtCompanyName"].ToString();
                   _myCompanyService.UpdateCompany(Company);
               }
               _myCompanyService.UpdateCompanyContactForRetail(UpdateContact);
           }
           else
           {
               CompanyContact uPdateContact = new CompanyContact();
               uPdateContact.FirstName = Model.FirstName;
               uPdateContact.LastName = Model.LastName;
               uPdateContact.Email = Model.Email;
               uPdateContact.JobTitle = Model.JobTitle;
               uPdateContact.HomeTel1 = Model.HomeTel1;
               uPdateContact.Mobile = Model.Mobile;
               uPdateContact.FAX = Model.FAX;
               uPdateContact.quickWebsite = Model.quickWebsite;
               uPdateContact.image = UpdateImage(fuImageUpload,Model);
               if (MarketAndPromotion.Equals("true"))
               {
                   uPdateContact.IsEmailSubscription = true;
               }
               else
               {
                   uPdateContact.IsEmailSubscription = false;
               }

               if (NewsLetterSubscription.Equals("true"))
               {
                   uPdateContact.IsNewsLetterSubscription = true;
               }

               else
               {

                   uPdateContact.IsNewsLetterSubscription = false;
               }
               uPdateContact.POBoxAddress = Model.POBoxAddress;
               uPdateContact.CorporateUnit = Model.CorporateUnit;
               uPdateContact.OfficeTradingName = Model.OfficeTradingName;
               uPdateContact.ContractorName = Model.ContractorName;
               uPdateContact.BPayCRN = Model.BPayCRN;
               uPdateContact.ABN = Model.ABN;
               uPdateContact.ACN = Model.ACN;
               uPdateContact.AdditionalField1 = Model.AdditionalField1;
               uPdateContact.AdditionalField2 = Model.AdditionalField2;
               uPdateContact.AdditionalField3 = Model.AdditionalField3;
               uPdateContact.AdditionalField4 = Model.AdditionalField4;
               uPdateContact.AdditionalField5 = Model.AdditionalField5;
               uPdateContact.ContactId = _webstoreAuthorizationChecker.loginContactID();
               _myCompanyService.UpdateCompanyContactForCorporate(uPdateContact);
           }
           return View();
       }

        private string UpdateImage(HttpPostedFileBase Request, CompanyContact Model)
        {
            string ImagePath=string.Empty;
            CompanyContact contact = _myCompanyService.GetContactByID(_webstoreAuthorizationChecker.loginContactID());
            if (Request != null)
            {
                string folderPath = "/mpc_content/Assets" + "/" + UserCookieManager.OrganisationID + "/" + UserCookieManager.StoreId + "/Contacts/" + contact.ContactId + "";
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
                        
                //string fullPath = Request.MapPath("~/Images/Cakes/" + photoName);
                //if (System.IO.File.Exists(fullPath))
                //{
                //    System.IO.File.Delete(fullPath);
                //}

               // string completePath=System.Web.HttpContext.Current.Server.MapPath(previousFileToremove);

              //  var CompletePath = Request.MapPath(previousFileToremove);
                

                if (System.IO.File.Exists(previousFileToremove))
                {
                    DeleteFile(previousFileToremove);
                }
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
