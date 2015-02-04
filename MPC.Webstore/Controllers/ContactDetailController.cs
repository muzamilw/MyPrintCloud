using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        

    }
}