using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Webstore.Common;
using MPC.Webstore.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Webstore.ModelMappers;

namespace MPC.Webstore.Controllers
{
    public class ReceiptController : Controller
    {
        private readonly IOrderService _OrderService;
        private readonly ICompanyService _myCompanyService;

        public ReceiptController(IOrderService OrderService, ICompanyService myCompanyService)
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
         }
        // GET: Receipt
        public ActionResult Index(string OrderId)
        {
            MyCompanyDomainBaseResponse baseResponseOrganisation = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromOrganisation();

           OrderDetail order =  _OrderService.GetOrderReceipt(Convert.ToInt64(OrderId));

           ViewBag.Organisation = baseResponseOrganisation.Organisation;

            return View();
        }

        
    }
}