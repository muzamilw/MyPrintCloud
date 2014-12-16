using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.ResponseModels;
using MPC.Webstore.Common;
using System.Web;
namespace MPC.Webstore.Controllers
{
    
    public class DomainController : Controller
    {
         #region Private

        private readonly ICompanyService _myCompanyService;


        #endregion


        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public DomainController(ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
          
            this._myCompanyService = myCompanyService;
        }

        #endregion

        // GET: Domain
        public void Index()
        {

            string url = HttpContext.Request.Url.ToString();
            if (!string.IsNullOrEmpty(url))
            {
                url = url.Substring(7);
            }
            long storeId = _myCompanyService.GetStoreIdFromDomain(url);
            if (storeId == 0)
            {
                Response.Redirect("/Error");
                
                //return RedirectToAction("Error", "Home"); //Response.Redirect("Home/Error");
                //throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            else
            {
                MyCompanyDomainBaseResponse baseResponse = _myCompanyService.GetStoreFromCache(storeId).CreateFromCompany();

                if (baseResponse.Company != null)
                {
                    UserCookieManager.StoreId = baseResponse.Company.CompanyId;

                    // set global language of store

                    string languageName = _myCompanyService.GetUiCulture(Convert.ToInt64(baseResponse.Company.OrganisationId));

                    CultureInfo ci = null;

                    if (string.IsNullOrEmpty(languageName))
                    {
                        languageName = "en-US";
                    }

                    ci = new CultureInfo(languageName);

                    Thread.CurrentThread.CurrentUICulture = ci;
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);

                    if (baseResponse.Company.IsCustomer == 3)// corporate customer
                    {
                        Response.Redirect("/Login");
                    }
                }
                else
                {
                    RedirectToAction("Error", "Home");
                }
            }
            Response.Redirect("/Home/Index");
           // return RedirectToAction("Index", "Home");
          //  return View();
        }
    }
}