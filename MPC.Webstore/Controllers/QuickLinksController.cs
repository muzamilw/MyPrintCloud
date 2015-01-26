using MPC.Interfaces.WebStoreServices;
using MPC.Webstore.Common;
using MPC.Webstore.ModelMappers;
using MPC.Webstore.Models;
using MPC.Webstore.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Controllers
{
    public class QuickLinksController : Controller
    {
         #region Private

        private readonly ICompanyService _myCompanyService;


        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public QuickLinksController(ICompanyService myCompanyService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
           
            this._myCompanyService = myCompanyService;
        }

        #endregion
      
        public ActionResult Index()
        {
            AddressViewModel oAddress = null;

            string CacheKeyName = "CompanyBaseResponse";
            ObjectCache cache = MemoryCache.Default;


            MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.StoreId];



            if (StoreBaseResopnse.Company.isDisplaySecondaryPages == true)
            {
                ViewBag.Display = "1";

                ViewData["PageCategory"] = StoreBaseResopnse.PageCategories;
                ViewData["CmsPage"] = StoreBaseResopnse.SecondaryPages;

                if (StoreBaseResopnse.Organisation != null)
                {
                    oAddress = new AddressViewModel();
                    oAddress.Address1 = StoreBaseResopnse.Organisation.Address1;
                    oAddress.Address2 = StoreBaseResopnse.Organisation.Address2;

                    oAddress.City = StoreBaseResopnse.Organisation.City;
                    oAddress.State = _myCompanyService.GetStateNameById(StoreBaseResopnse.Organisation.StateId ?? 0);
                    oAddress.Country = _myCompanyService.GetCountryNameById(StoreBaseResopnse.Organisation.CountryId ?? 0);
                    oAddress.ZipCode = StoreBaseResopnse.Organisation.ZipCode;

                    if (!string.IsNullOrEmpty(StoreBaseResopnse.Organisation.Tel))
                    {
                        oAddress.Tel = "Tel: " + StoreBaseResopnse.Organisation.Tel;
                    }
                    if (!string.IsNullOrEmpty(StoreBaseResopnse.Organisation.Fax))
                    {
                        oAddress.Fax = "Fax: " + StoreBaseResopnse.Organisation.Fax;
                    }
                    if (!string.IsNullOrEmpty(StoreBaseResopnse.Organisation.Email))
                    {
                        oAddress.Email = "Email: " + StoreBaseResopnse.Organisation.Email;
                    }
                }
            }
            else 
            {
                ViewBag.Display = "0";
            }
            
            return PartialView("PartialViews/QuickLinks", oAddress);
        }
    }
}