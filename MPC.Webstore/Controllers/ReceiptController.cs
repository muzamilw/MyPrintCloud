﻿using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Webstore.Common;
using MPC.Webstore.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Webstore.ModelMappers;
using System.Runtime.Caching;
using MPC.Webstore.Models;
using System.Net;
using System.IO;
using MPC.Models.ResponseModels;

namespace MPC.Webstore.Controllers
{
    public class ReceiptController : Controller
    {
        private readonly IOrderService _OrderService;
        private readonly ICompanyService _myCompanyService; 
        private readonly IWebstoreClaimsHelperService _myClaimHelper;

        public ReceiptController(IOrderService OrderService, ICompanyService myCompanyService, IWebstoreClaimsHelperService myClaimHelper)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            if (OrderService == null)
            {
                throw new ArgumentNullException("OrderService");
            }
            this._myCompanyService = myCompanyService;
            this._OrderService = OrderService;
            this._myClaimHelper = myClaimHelper;
        }
        // GET: Receipt
        public ActionResult Index(string OrderId)
        {
            UserCookieManager.WEBOrderId = 0;

            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;

            ViewBag.OrderId = OrderId;
            //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];

            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);


            ViewBag.IsShowPrices = _myCompanyService.ShowPricesOnStore(UserCookieManager.WEBStoreMode, StoreBaseResopnse.Company.ShowPrices ?? false, _myClaimHelper.loginContactID(), UserCookieManager.ShowPriceOnWebstore);


            if (!string.IsNullOrEmpty(StoreBaseResopnse.Currency))
            {
                ViewBag.Currency = StoreBaseResopnse.Currency;
            }
            else
            {
                ViewBag.Currency = "";
            }

            ViewBag.TaxLabel = StoreBaseResopnse.Company.TaxLabel;
            OrderDetail order = _OrderService.GetOrderReceipt(Convert.ToInt64(OrderId));

            ViewBag.Company = StoreBaseResopnse.Company;
            ViewBag.IsDigitalDownloadOrder = order.IsDownloadOrder;
           
            AddressViewModel oStoreDefaultAddress = null;

            if (StoreBaseResopnse.Company.isWhiteLabel == false || StoreBaseResopnse.Company.isWhiteLabel == null)
            {
                oStoreDefaultAddress = null;
            }
            else 
            {
                oStoreDefaultAddress = new AddressViewModel();
                //if (StoreBaseResopnse.StoreDetaultAddress != null)
                //{
                    
                //    oStoreDefaultAddress.Address1 = StoreBaseResopnse.StoreDetaultAddress.Address1;
                //    oStoreDefaultAddress.Address2 = StoreBaseResopnse.StoreDetaultAddress.Address2;

                //    oStoreDefaultAddress.City = StoreBaseResopnse.StoreDetaultAddress.City;
                //    oStoreDefaultAddress.State = _myCompanyService.GetStateNameById(StoreBaseResopnse.StoreDetaultAddress.StateId ?? 0);
                //    oStoreDefaultAddress.Country = _myCompanyService.GetCountryNameById(StoreBaseResopnse.StoreDetaultAddress.CountryId ?? 0);
                //    oStoreDefaultAddress.ZipCode = StoreBaseResopnse.StoreDetaultAddress.PostCode;

                //    if (!string.IsNullOrEmpty(StoreBaseResopnse.StoreDetaultAddress.Tel1))
                //    {
                //        oStoreDefaultAddress.Tel = StoreBaseResopnse.StoreDetaultAddress.Tel1;
                //    }
                //}
                ViewBag.OrganisationLogo = "";
                ViewBag.OrganisationName = "";
                ViewBag.OrgVATRegNumber = "";
                if (StoreBaseResopnse.Organisation != null)
                {
                    ViewBag.OrganisationLogo = StoreBaseResopnse.Organisation.MISLogo;
                    ViewBag.OrgVATRegNumber = StoreBaseResopnse.Organisation.TaxRegistrationNo;
                    ViewBag.OrganisationName = StoreBaseResopnse.Organisation.OrganisationName;
                    oStoreDefaultAddress.Address1 = StoreBaseResopnse.Organisation.Address1;
                    oStoreDefaultAddress.Address2 = StoreBaseResopnse.Organisation.Address2;

                    oStoreDefaultAddress.City = StoreBaseResopnse.Organisation.City;
                    oStoreDefaultAddress.State = _myCompanyService.GetStateNameById(StoreBaseResopnse.Organisation.StateId ?? 0);
                    oStoreDefaultAddress.Country = _myCompanyService.GetCountryNameById(StoreBaseResopnse.Organisation.CountryId ?? 0);
                    oStoreDefaultAddress.ZipCode = StoreBaseResopnse.Organisation.ZipCode;

                    if (!string.IsNullOrEmpty(StoreBaseResopnse.Organisation.Tel))
                    {
                        oStoreDefaultAddress.Tel = StoreBaseResopnse.Organisation.Tel;
                    }
                }
            }
            ViewBag.oStoreDefaultAddress = oStoreDefaultAddress;


            
            ViewBag.StoreId = StoreBaseResopnse.Company.CompanyId;
         
            return View("PartialViews/Receipt", order);
        }
    }
}