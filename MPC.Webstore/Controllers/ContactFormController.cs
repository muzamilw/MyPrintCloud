using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using MPC.Webstore.Areas.WebstoreApi.Models;
using MPC.Webstore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class ContactFormController : Controller
    {
       
        // GET: ContactForm
        public ActionResult Index()
        {
            
            return PartialView("PartialViews/ContactForm");
        }

        
    }
}