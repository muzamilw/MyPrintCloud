using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using MPC.Webstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class SignUpController : Controller
    {
        private readonly ICompanyService _myCompanyService;
  

       #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SignUpController(ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
        }

        #endregion

        // GET: SignUp
        public ActionResult Index()
        {
            return View("PartialViews/SignUp");
        }

        [HttpPost]
        public ActionResult Index(RegisterViewModel model)
        {
            string returnUrl = System.Web.HttpContext.Current.Request.UrlReferrer.Query.Split('=')[1];

            if (ModelState.IsValid)
            {
                if (_myCompanyService.GetContactByEmail(model.Email) != null)
                {
                   ViewBag.Message = string.Format("You indicated you are a new customer, but an account already exists with the e-mail {0}", model.Email);
                    return View();
                }
                //else if (hfisRegWithTwitter.Value == "1")
                //{
                //    if (_myCompanyService.GetContactByFirstName(model.FirstName) != null)
                //    {
                //        ViewBag.Message = string.Format("You indicated you are a new customer, but an account already exists with the e-mail {0}", txtEmail.Text);
                //        return View();
                //    }
                //    else
                //    {
                //        SetRegisterCustomer(model);

                //    }
                //}
                else
                {
                    SetRegisterCustomer(model);
                    return View();
                }
            }
            else
            {
                return View("PartialViews/Login");
            }

        }

        private void SetRegisterCustomer(RegisterViewModel model)
        {

            CompanyContact contact = new CompanyContact();
            Int64 CompanyID = 0;
            bool isContactCreate = false;
            contact.FirstName = model.FirstName;
            contact.LastName = model.LastName;
            contact.Email = model.Email;
            contact.Mobile = model.Phone;
            contact.Password = model.Password;

            if (SessionParameters.LoginCompany.IsCustomer == (int)StoreMode.Retail)
            {
               CompanyID =  _myCompanyService.CreateContact(contact, contact.FirstName + " " + contact.LastName, 0,(int)StoreMode.Retail,"");

                if (CompanyID > 0)
                {
                    SessionParameters.LoginCompany.CompanyId = contact.CompanyId;
                    SessionParameters.LoginContact.ContactId = contact.ContactId;
                    SessionParameters.LoginCompany = _myCompanyService.GetCompanyByCompanyID(contact.CompanyId);
                    SessionParameters.LoginContact = _myCompanyService.GetContactByID(contact.ContactId);

                   // Campaign RegistrationCampaign = emailmgr.GetCampaignRecordByEmailEvent((int)EmailEvents.Registration);
                    // work for email to sale manager
                    isContactCreate = true;

                }
                else
                {
                    isContactCreate = false;
                }

            }
            else
            {

            }

        }
    }
}