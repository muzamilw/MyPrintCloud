using MPC.ExceptionHandling;
using MPC.Interfaces.WebStoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Models.DomainModels;
using MPC.Webstore.ResponseModels;
using MPC.Webstore.Common;
using MPC.Webstore.ModelMappers;
using MPC.Models.Common;

namespace MPC.Webstore.Controllers
{
    public class ShopCartAddressSelectController : Controller
    {
        private readonly ICompanyService _myCompanyService;
        private readonly IItemService _IItemService;
        private readonly ITemplateService _ITemplateService;
        private readonly IOrderService _IOrderService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private long OrganisationID = 0;
        public ShopCartAddressSelectController(IWebstoreClaimsHelperService myClaimHelper, ICompanyService myCompanyService, IItemService IItemService, ITemplateService ITemplateService, IOrderService IOrderService)
        {
            if (myCompanyService == null)
            {
                throw new ArgumentNullException("myCompanyService");
            }
            if (myClaimHelper == null)
            {
                throw new ArgumentNullException("myClaimHelper");
            }
            if (IItemService == null)
            {
                throw new ArgumentNullException("IItemService");
            }
            if (ITemplateService == null)
            {
                throw new ArgumentNullException("ITemplateService");
            }
            if (IOrderService == null)
            {
                throw new ArgumentNullException("IOrderService");
            }
    
            this._myClaimHelper = myClaimHelper;
            this._myCompanyService = myCompanyService;
            this._IItemService = IItemService;
            this._ITemplateService = ITemplateService;
            this._IOrderService = IOrderService;
        
        }

        // GET: ShopCartAddressSelect
        public ActionResult Index(long OrderID)
        {
            try
            {
                List<CostCentre> deliveryCostCentersList = null;
                MyCompanyDomainBaseResponse baseresponseOrg = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromOrganisation();

                OrganisationID = baseresponseOrg.Organisation.OrganisationId;

                //deliveryCostCentersList = GetDeliveryCostCenterList();
                if (!_myClaimHelper.isUserLoggedIn())
                {
                    // Annonymous user cann't view it.
                    RedirectToAction("Index", "Home");

                }



                return View();
            }
            catch(Exception ex)
            {
                throw new MPCException(ex.ToString(), OrganisationID);
            }
            
        }
        //private List<CostCentre> GetDeliveryCostCenterList()
        //{
        //    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
        //    {
        //        return OrderManager.GetCorporateDeliveryCostCentersList(SessionParameters.ContactCompany.ContactCompanyID);
        //    }
        //    else
        //    {
        //        return OrderManager.GetDeliveryCostCentersList();
        //    }
        //}
    }
}