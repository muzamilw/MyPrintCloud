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

            // gets cached page categories and secondary pages of company id
            MyCompanyDomainBaseResponse basePageResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromSecondaryPages();

            // gets the organisation to set the default address detail in footer 
            MyCompanyDomainBaseResponse baseOrganisationResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromOrganisation();

            // gets the company detail to check display secondary pages or not
            MyCompanyDomainBaseResponse baseCompanyResponse = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();

            if (baseCompanyResponse.Company.isDisplaySecondaryPages == true)
            {
                ViewBag.Display = "1";

                ViewData["PageCategory"] = basePageResponse.PageCategories;
                ViewData["CmsPage"] = basePageResponse.SecondaryPages;

                if (baseOrganisationResponse.Organisation != null)
                {
                    oAddress = new AddressViewModel();
                    oAddress.Address1 = baseOrganisationResponse.Organisation.Address1;
                    oAddress.Address2 = baseOrganisationResponse.Organisation.Address2;

                    oAddress.City = baseOrganisationResponse.Organisation.City;
                    oAddress.State = _myCompanyService.GetStateNameById(baseOrganisationResponse.Organisation.StateId ?? 0);
                    oAddress.Country = _myCompanyService.GetCountryNameById(baseOrganisationResponse.Organisation.CountryId ?? 0);
                    oAddress.ZipCode = baseOrganisationResponse.Organisation.ZipCode;

                    if (!string.IsNullOrEmpty(baseOrganisationResponse.Organisation.Tel))
                    {
                        oAddress.Tel = "Tel: " + baseOrganisationResponse.Organisation.Tel;
                    }
                    if (!string.IsNullOrEmpty(baseOrganisationResponse.Organisation.Fax))
                    {
                        oAddress.Fax = "Fax: " + baseOrganisationResponse.Organisation.Fax;
                    }
                    if (!string.IsNullOrEmpty(baseOrganisationResponse.Organisation.Email))
                    {
                        oAddress.Email = "Email: " + baseOrganisationResponse.Organisation.Email;
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