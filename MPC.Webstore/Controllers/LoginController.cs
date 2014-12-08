using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Webstore.Common;
using MPC.Webstore.Models;

namespace MPC.Webstore.Controllers
{
    public class LoginController : Controller
    {
           #region Private

        private readonly ICompanyService _myCompanyService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public LoginController(ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            this._myCompanyService = myCompanyService;
        }

        #endregion
        // GET: Login
        public ActionResult Index(string FirstName,string LastName,string Email)
        {
           
            if (!string.IsNullOrEmpty(FirstName))
            {
                string returnUrl = string.Empty;
                //if (System.Web.HttpContext.Current.Request.UrlReferrer == null)
                //{
                //    returnUrl = "/Home/Index";
                //}
                //else
                //{
                //   // returnUrl = System.Web.HttpContext.Current.Request.UrlReferrer.Query.Split('=')[1];
                //    returnUrl = "/Home/Index";
                //}
                CompanyContact user = new CompanyContact();
                if (!string.IsNullOrEmpty(Email))
                {
                    user = _myCompanyService.GetContactByEmail(Email);
                }
                else
                {
                    user = _myCompanyService.GetContactByFirstName(FirstName);
                }
                if (user != null)
                {
                         return  VerifyUser(user);
                        //if (user.isArchived.HasValue && user.isArchived.Value == true)
                        //{
                        //    ModelState.AddModelError("", "Account is archived.");
                        //    return View("PartialViews/Login");
                        //}
                        //if (user.Company.IsDisabled == 1)
                        //{
                        //    ModelState.AddModelError("", "Your account is disabled. Please contact us for further information.");
                        //    return View("PartialViews/Login");
                        //}
                        //else
                        //{
                        //    SessionParameters.LoginCompany = user.Company;
                        //    SessionParameters.LoginContact = user;
                        //    RedirectToLocal(returnUrl);
                        //    return null;
                       // }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View("PartialViews/Login");
                    }
            }
            else
            {
                 return View("PartialViews/Login");
             }
        }

        [HttpPost]
        public ActionResult Index(AccountViewModel model)
        {
            string returnUrl = System.Web.HttpContext.Current.Request.UrlReferrer.Query.Split('=')[1];
            
            if (ModelState.IsValid)
            {
                CompanyContact user = _myCompanyService.GetContactUser(model.Email, model.Password);
                if (user != null)
                {
                    if (user.isArchived.HasValue && user.isArchived.Value == true)
                    {
                        ModelState.AddModelError("", "Account is archived.");
                        return View("PartialViews/Login");
                    }
                    if (user.Company.IsDisabled == 1)
                    {
                        ModelState.AddModelError("", "Your account is disabled. Please contact us for further information.");
                        return View("PartialViews/Login");
                    }
                    else
                    {
                        SessionParameters.LoginCompany = user.Company;
                        SessionParameters.LoginContact = user;
                        RedirectToLocal(returnUrl);
                        return null;
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View("PartialViews/Login");
                }
            }
            else
            {
                return View("PartialViews/Login");
            }
            
        }

        private ActionResult VerifyUser(CompanyContact user)
        {
            if (user.isArchived.HasValue && user.isArchived.Value == true)
            {
                ModelState.AddModelError("", "Account is archived.");
                return View("PartialViews/Login");
            }
            if (user.Company.IsDisabled == 1)
            {
                ModelState.AddModelError("", "Your account is disabled. Please contact us for further information.");
                return View("PartialViews/Login");
            }
            else
            {
                SessionParameters.LoginCompany = user.Company;
                SessionParameters.LoginContact = user;
                RedirectToLocal("");
                return null;
            }

        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                ControllerContext.HttpContext.Response.Redirect(returnUrl);
            }
            ControllerContext.HttpContext.Response.Redirect(Url.Action("Index", "Home", null, protocol: Request.Url.Scheme));
            return null;
        }
      
        //public ActionResult Login(string email, string password)
        //{

        //    if (ModelState.IsValid)
        //    {

        //    }
        //    else
        //    {
               
        //    }
        //   //return  "Invalid email or password.";
        //   //return Content("no");
        //    return View("PartialViews/Login");
        //    //return View("PartialViews/Login", "no");
        //   // return View("PartialViews/Login");
        //    // return RedirectToAction("Index", "Home", model);
        //   // return Redirect("/Login");
        //}
    }
}