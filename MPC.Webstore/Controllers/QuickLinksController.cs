using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
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

            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);


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
                ViewData["PageCategory"] = oPageUpdateCategories.ToList();
                ViewData["CmsPage"] = StoreBaseResopnse.SecondaryPages;
                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                {
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
                            oAddress.Tel = Utils.GetKeyValueFromResourceFile("lblTelTxt", UserCookieManager.WBStoreId, "Tel:") + StoreBaseResopnse.Organisation.Tel;
                        }
                        if (!string.IsNullOrEmpty(StoreBaseResopnse.Organisation.Fax))
                        {
                            oAddress.Fax = Utils.GetKeyValueFromResourceFile("ltrlfaxx", UserCookieManager.WBStoreId, "Fax:") + StoreBaseResopnse.Organisation.Fax;
                        }
                        if (!string.IsNullOrEmpty(StoreBaseResopnse.Organisation.Email))
                        {
                            oAddress.Email = Utils.GetKeyValueFromResourceFile("ltrlllEmail", UserCookieManager.WBStoreId, "Email:") + StoreBaseResopnse.Organisation.Email;
                        }
                    }
                }
                else 
                {
                    if (StoreBaseResopnse.StoreDetaultAddress != null)
                    {
                        oAddress = new AddressViewModel();
                        oAddress.Address1 = StoreBaseResopnse.StoreDetaultAddress.Address1;
                        oAddress.Address2 = StoreBaseResopnse.StoreDetaultAddress.Address2;

                        oAddress.City = StoreBaseResopnse.StoreDetaultAddress.City;
                        oAddress.State = _myCompanyService.GetStateNameById(StoreBaseResopnse.StoreDetaultAddress.StateId ?? 0);
                        oAddress.Country = _myCompanyService.GetCountryNameById(StoreBaseResopnse.StoreDetaultAddress.CountryId ?? 0);
                        oAddress.ZipCode = StoreBaseResopnse.StoreDetaultAddress.PostCode;

                        if (!string.IsNullOrEmpty(StoreBaseResopnse.StoreDetaultAddress.Tel1))
                        {
                            oAddress.Tel = Utils.GetKeyValueFromResourceFile("lblTelTxt", UserCookieManager.WBStoreId, "Tel:") + StoreBaseResopnse.StoreDetaultAddress.Tel1;
                        }
                        if (!string.IsNullOrEmpty(StoreBaseResopnse.StoreDetaultAddress.Fax))
                        {
                            oAddress.Fax = Utils.GetKeyValueFromResourceFile("ltrlfaxx", UserCookieManager.WBStoreId, "Fax:") + StoreBaseResopnse.StoreDetaultAddress.Fax;
                        }
                        if (!string.IsNullOrEmpty(StoreBaseResopnse.StoreDetaultAddress.Email))
                        {
                            oAddress.Email = Utils.GetKeyValueFromResourceFile("ltrlllEmail", UserCookieManager.WBStoreId, "Email:") + StoreBaseResopnse.StoreDetaultAddress.Email;
                        }
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