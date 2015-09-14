using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Webstore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPC.Webstore.Areas.OrderReceipt.Controllers
{
    public class OrderReceiptController : Controller
    {
           #region Private

        private readonly ICompanyService _myCompanyService;
        private readonly IOrderService _OrderService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public OrderReceiptController(ICompanyService myCompanyService, IOrderService OrderService)
        {

            this._myCompanyService = myCompanyService;
            this._OrderService = OrderService;

        }

        #endregion
        // GET: OrderReceipt/OrderReceipt

        [AllowAnonymous]
        public ActionResult Index(string OrderId, string StoreId, string IsPrintReceipt)
        {
            MPC.Models.DomainModels.Company oCompany = _myCompanyService.GetStoreReceiptPage(Convert.ToInt64(StoreId));

            if (oCompany != null)
            {
                MPC.Models.DomainModels.Organisation oOrganisation = _myCompanyService.GetOrganisatonById(Convert.ToInt64(oCompany.OrganisationId));

                if (oCompany.ShowPrices == true)
                {

                    ViewBag.IsShowPrices = true;

                }
                else
                {

                    ViewBag.IsShowPrices = false;

                }


                ViewBag.TaxLabel = oCompany.TaxLabel;

                ViewBag.Company = oCompany;

                AddressViewModel oStoreDefaultAddress = null;
              //  Address StoreAddress = _myCompanyService.GetDefaultAddressByStoreID(Convert.ToInt64(StoreId));

                ViewBag.OrganisationLogo = "";
                ViewBag.OrganisationName = "";
                if (oCompany.isWhiteLabel == false)
                {
                    oStoreDefaultAddress = null;
                }
                else
                {
                    if (oOrganisation != null)
                    {
                        ViewBag.OrganisationLogo = oOrganisation.MISLogo;
                        ViewBag.OrganisationName = oOrganisation.OrganisationName;
                        oStoreDefaultAddress = new AddressViewModel();
                        oStoreDefaultAddress.Address1 = oOrganisation.Address1;
                        oStoreDefaultAddress.Address2 = oOrganisation.Address2;

                        oStoreDefaultAddress.City = oOrganisation.City;
                        oStoreDefaultAddress.State = _myCompanyService.GetStateNameById(oOrganisation.StateId ?? 0);
                        oStoreDefaultAddress.Country = _myCompanyService.GetCountryNameById(oOrganisation.CountryId ?? 0);
                        oStoreDefaultAddress.ZipCode = oOrganisation.ZipCode;

                        if (!string.IsNullOrEmpty(oOrganisation.Tel))
                        {
                            oStoreDefaultAddress.Tel = oOrganisation.Tel;
                        }
                    }
                }

                ViewBag.oStoreDefaultAddress = oStoreDefaultAddress;

                string currency = "";

                if (oOrganisation != null)
                {
                    currency = _myCompanyService.GetCurrencySymbolById(Convert.ToInt64(oOrganisation.CurrencyId));
                }

                if (!string.IsNullOrEmpty(currency))
                {
                    ViewBag.Currency = currency;
                }
                else
                {
                    ViewBag.Currency = "";
                }


            }

            OrderDetail order = _OrderService.GetOrderReceipt(Convert.ToInt64(OrderId));

            ViewBag.StoreId = StoreId;

            if (IsPrintReceipt == "1")
            {
                ViewBag.Print = "<script type='text/javascript'>function MyPrint() {window.print();}</script>";
            }
            else
            {
                ViewBag.Print = "";
            }

            if (oCompany.IsCustomer == (int)CustomerTypes.Corporate)
            {
                if (order != null && order.CompanyContact != null)
                {
                    if (order.CompanyContact.IsPricingshown == true)
                    {
                        ViewBag.IsShowPrices = true;
                    }
                    else
                    {
                        ViewBag.IsShowPrices = false;
                    }
                }
            }

            return View(order);
        }
    }
}