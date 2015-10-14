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
using System.Web.UI.WebControls;
using MPC.Webstore.ViewModels;
using System.Configuration;
using System.Net;
using System.IO;
using System.Xml;
using System.Text;
using System.Runtime.Caching;
using MPC.Models.ResponseModels;

namespace MPC.Webstore.Controllers
{
    public class ShopCartAddressSelectController : Controller
    {
        private readonly ICompanyService _myCompanyService;
        private readonly IItemService _IItemService;
        private readonly ITemplateService _ITemplateService;
        private readonly IOrderService _IOrderService;
        private readonly ICostCentreService _ICostCenterService;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private long OrganisationID = 0;
        long BillingID = 0;
        long ShippingID = 0;
        public ShopCartAddressSelectController(IWebstoreClaimsHelperService myClaimHelper, ICompanyService myCompanyService, IItemService IItemService, ITemplateService ITemplateService, IOrderService IOrderService, ICostCentreService ICostCentreService)
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
            if (ICostCentreService == null)
            {
                throw new ArgumentNullException("ICostCentreService");
            }

            this._myClaimHelper = myClaimHelper;
            this._myCompanyService = myCompanyService;
            this._IItemService = IItemService;
            this._ITemplateService = ITemplateService;
            this._IOrderService = IOrderService;
            this._ICostCenterService = ICostCentreService;

        }

        #region LoadOperations
        // GET: ShopCartAddressSelect
        public ActionResult Index(long OrderID)
        {
            try
            {

                ShopCartAddressSelectViewModel AddressSelectModel = new ShopCartAddressSelectViewModel();
                LoadPageData(AddressSelectModel, OrderID);
                return View("PartialViews/ShopCartAddressSelect", AddressSelectModel);
            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), OrganisationID);
            }

        }



        private void LoadPageData(ShopCartAddressSelectViewModel AddressSelectModel, long OrderID) 
        {
            List<CostCentre> deliveryCostCentersList = null;
            List<Address> customerAddresses = new List<Address>();
            CompanyTerritory Territory = new CompanyTerritory();
            
            ShoppingCart shopCart = null;
            CompanyContact superAdmin = null;
            if (!_myClaimHelper.isUserLoggedIn())
            {
                // Annonymous user cann't view it.
                Response.Redirect("/Login");

            }

            UserCookieManager.WEBOrderId = OrderID;

            //string CacheKeyName = "CompanyBaseResponse";
            //ObjectCache cache = MemoryCache.Default;

            //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
            MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);


            Organisation baseresponseOrg = StoreBaseResopnse.Organisation;
            Company baseresponseComp = StoreBaseResopnse.Company;
           
            if (!string.IsNullOrEmpty(StoreBaseResopnse.Currency))
                AddressSelectModel.Currency = StoreBaseResopnse.Currency;
            else
                AddressSelectModel.Currency = string.Empty;

            AddressSelectModel.OrderId = OrderID;
            OrganisationID = baseresponseOrg.OrganisationId;

            deliveryCostCentersList = GetDeliveryCostCenterList();

            shopCart = LoadShoppingCart(OrderID, AddressSelectModel);

            AddressSelectModel.shopcart = shopCart;

            BindGridView(shopCart, AddressSelectModel);

            AddressSelectModel.calcultedValuesOfCart = CartModel(shopCart);
            BindCountriesDropDownData(baseresponseOrg, baseresponseComp, AddressSelectModel);

            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
            {

                if (_myClaimHelper.loginContactID() > 0)
                {
                    superAdmin = _myCompanyService.GetCorporateAdmin(UserCookieManager.WBStoreId);
                }

                // User is not the super admin.
                if (superAdmin != null && _myClaimHelper.loginContactID() != superAdmin.ContactId)
                {

                    AddressSelectModel.HasAdminMessage = true;
                    if (superAdmin != null)
                    {
                        AddressSelectModel.AdminName = baseresponseComp.Name;

                    }
                    else
                    {
                        AddressSelectModel.AdminName = MPC.Webstore.Common.Constants.NotAvailiable;
                    }
                }
                AddressSelectModel.OrderId = OrderID;

            }

            AddressSelectModel.LtrMessageToDisplay = false;
            //Addresses panel
            if (shopCart != null)
            {
                BindDeliveryCostCenterDropDown(deliveryCostCentersList, OrderID, AddressSelectModel);

                CompanyContact contact = _myCompanyService.GetContactByID(_myClaimHelper.loginContactID());

                if (contact != null)
                    AddressSelectModel.ContactTel = contact.Mobile;
                else
                    AddressSelectModel.ContactTel = "";

                // Bind Company Addresses
                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                {
                    if (baseresponseComp.isStoreModePrivate == true)
                    {
                        // if role is admin
                        if (_myClaimHelper.loginContactRoleID() == (int)Roles.Adminstrator)
                            customerAddresses = _myCompanyService.GetAddressByCompanyID(UserCookieManager.WBStoreId);
                        // if role is manager
                        else if (_myClaimHelper.loginContactRoleID() == (int)Roles.Manager)
                        {
                            // get territory of manager

                            Territory = _myCompanyService.GetTerritoryById(contact.TerritoryId ?? 0);

                            if (Territory != null)
                            {
                                List<CompanyContact> ContactTerritoriesIDs = new List<CompanyContact>();
                                List<Address> Manageraddresses = new List<Address>();
                                List<int> AddressIDs = new List<int>();
                                int BillingAddressID = 0;
                                int ShippingAddressID = 0;

                                customerAddresses = _myCompanyService.GetAddressesByTerritoryID(Territory.TerritoryId);


                            }
                        }// if role is user
                        else if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
                        {

                            // get addresses of contact where isprivate is true
                            customerAddresses = _myCompanyService.GetAdressesByContactID(_myClaimHelper.loginContactID());
                            if (contact.TerritoryId != null)
                            {
                                List<int> TerritoryDefaultAddress = new List<int>();
                                // get territory of contact
                                CompanyTerritory ContactTerritory = _myCompanyService.GetTerritoryById(contact.TerritoryId ?? 0);
                                if (ContactTerritory != null)
                                {
                                    List<Address> addresses = _myCompanyService.GetBillingAndShippingAddresses(ContactTerritory.TerritoryId);

                                    if (addresses != null)
                                    {

                                        foreach (Address address in addresses)
                                        {
                                            customerAddresses.Add(address);
                                        }
                                    }
                                }
                            }

                        }
                    }
                    else
                    {
                        customerAddresses = _myCompanyService.GetAddressByCompanyID(UserCookieManager.WBStoreId);
                    }
                }
                else
                {
                    customerAddresses = _myCompanyService.GetContactCompanyAddressesList(_myClaimHelper.loginContactCompanyID());
                }
                if (customerAddresses != null && customerAddresses.Count > 0)
                {

                    FillUpAddressDropDowns(customerAddresses, AddressSelectModel);

                    if (BillingID != 0 && ShippingID != 0)
                    {

                        long ContactShippingID = 0;
                        Address billingAddress = null;
                        //Default Billing Address
                        if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                        {
                            if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
                            {
                                billingAddress = customerAddresses.Where(c => c.AddressId == BillingID).FirstOrDefault();
                            }
                            else
                            {
                                // billingAddress = 
                                long ContactBillingID = contact.AddressId;
                                billingAddress = customerAddresses.Where(c => c.AddressId == BillingID).FirstOrDefault();
                            }
                        }
                        else
                        {
                            billingAddress = customerAddresses.Where(c => c.AddressId == BillingID).FirstOrDefault();
                        }

                        if (billingAddress == null)
                        {
                            //set default address
                            billingAddress = customerAddresses.FirstOrDefault();
                        }

                        AddressSelectModel.BillingAddress = billingAddress;
                        //AddressSelectModel.SelectedBillingCountry = billingAddress.CountryId ?? 0;
                        //AddressSelectModel.SelectedBillingState = billingAddress.StateId ?? 0;
                        RebindBillingStatesDD(AddressSelectModel, billingAddress);
                        AddressSelectModel.SelectedBillingAddress = billingAddress.AddressId;

                        if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                        {
                            if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
                            {
                                AddressSelectModel.IsUserRole = true;
                                if (baseresponseComp.isStoreModePrivate == true)
                                {
                                    AddressSelectModel.isStoreModePrivate = true;
                                }
                                else
                                {
                                    AddressSelectModel.isStoreModePrivate = false;
                                }
                            }
                            else
                            {
                                AddressSelectModel.IsUserRole = false;
                            }
                        }
                        else
                        {
                            AddressSelectModel.IsUserRole = false;
                        }

                        //Shipping Address

                        if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                        {
                            if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
                            {
                                ContactShippingID = customerAddresses.Where(c => c.AddressId == contact.ShippingAddressId).Select(s => s.AddressId).FirstOrDefault();
                            }
                            else
                            {
                                ContactShippingID = Convert.ToInt64(contact.ShippingAddressId);
                            }
                        }
                        else
                        {
                            ContactShippingID = contact.AddressId;

                        }
                        Address shippingAddress = customerAddresses.Where(addr => addr.AddressId == ShippingID).FirstOrDefault();
                        // Is billing and Shipping are same ??
                        if (shippingAddress == null)
                        {
                            shippingAddress = customerAddresses.FirstOrDefault();
                        }

                        if (shippingAddress != null)
                        {
                            AddressSelectModel.ShippingAddress = shippingAddress;
                            //AddressSelectModel.SelectedDeliveryState = shippingAddress.StateId ?? 0;
                            //AddressSelectModel.SelectedDeliveryCountry = shippingAddress.CountryId ?? 0;
                            RebindShippingStatesDD(AddressSelectModel, shippingAddress);
                            AddressSelectModel.SelectedDeliveryAddress = (int)shippingAddress.AddressId;
                        }
                    }
                    else
                    {

                        long ContactShippingID = 0;
                        Address billingAddress = null;
                        //Default Billing Address
                        if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                        {
                            if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
                            {
                                billingAddress = customerAddresses.Where(c => c.AddressId == contact.AddressId).FirstOrDefault();
                            }
                            else
                            {
                                // billingAddress = 
                                long ContactBillingID = contact.AddressId;
                                billingAddress = customerAddresses.Where(c => c.AddressId == ContactBillingID).FirstOrDefault();
                            }
                        }
                        else
                        {
                            billingAddress = customerAddresses.Where(c => c.AddressId == contact.AddressId).FirstOrDefault();
                        }

                        if (billingAddress == null)
                        {
                            //set default address
                            billingAddress = customerAddresses.FirstOrDefault();
                        }

                        AddressSelectModel.BillingAddress = billingAddress;
                        RebindBillingStatesDD(AddressSelectModel, billingAddress);
                        //AddressSelectModel.SelectedBillingCountry = billingAddress.CountryId ?? 0;
                        //AddressSelectModel.SelectedBillingState = billingAddress.StateId ?? 0;
                        AddressSelectModel.SelectedBillingAddress = billingAddress.AddressId;

                        //Shipping Address

                        if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                        {
                            if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
                            {
                                ContactShippingID = customerAddresses.Where(c => c.AddressId == contact.ShippingAddressId).Select(s => s.AddressId).FirstOrDefault();
                            }
                            else
                            {
                                ContactShippingID = Convert.ToInt64(contact.ShippingAddressId);// ContactManager.GetContactShippingID(SessionParameters.ContactID);
                            }
                        }
                        else
                        {
                            ContactShippingID = contact.AddressId;// ContactManager.GetContactShippingIDRetail(SessionParameters.ContactID);

                        }
                        Address shippingAddress = customerAddresses.Where(addr => addr.AddressId == ContactShippingID).FirstOrDefault();

                        if (shippingAddress == null)
                        {
                            shippingAddress = customerAddresses.FirstOrDefault();
                        }
                        // Is billing and Shipping are same ??
                        if (shippingAddress != null)
                        {

                            AddressSelectModel.ShippingAddress = shippingAddress;
                            //AddressSelectModel.SelectedDeliveryState = shippingAddress.StateId ?? 0;
                            //AddressSelectModel.SelectedDeliveryCountry = shippingAddress.CountryId ?? 0;
                            RebindShippingStatesDD(AddressSelectModel, shippingAddress);
                            AddressSelectModel.SelectedDeliveryAddress = (int)shippingAddress.AddressId;
                        }

                        if (billingAddress != null && shippingAddress != null && billingAddress.AddressId == shippingAddress.AddressId)
                        {

                            AddressSelectModel.chkBoxDeliverySameAsBilling = "True";
                        }
                    }

                }
            }

            if (baseresponseComp != null)
            {
                AddressSelectModel.TaxLabel = baseresponseComp.TaxLabel + " :";
                if (baseresponseComp.PONumberRequired == true)
                {
                    ViewBag.isPoRequired = 1;
                }
                else 
                {
                    ViewBag.isPoRequired = 0;
                }
            }
            else
            {
                ViewBag.isPoRequired = 0;
            }
            AddressSelectModel.chkBoxDeliverySameAsBilling = "True";
            AddressSelectModel.Warning = "warning"; // Utils.GetKeyValueFromResourceFile("lnkWarnMesg", UserCookieManager.StoreId) + " " + baseresponseOrg.Organisation.Country + "."; // (string)GetGlobalResourceObject("MyResource", "lnkWarnMesg") + " " + companySite.Country + ".";

            ViewBag.IsShowPrices = _myCompanyService.ShowPricesOnStore(UserCookieManager.WEBStoreMode, baseresponseComp.ShowPrices ?? false, _myClaimHelper.loginContactID(), UserCookieManager.ShowPriceOnWebstore);
            

        }

        private CalculatedCartValues CartModel(ShoppingCart shopcart) 
        {
            double _priceTotal = 0;
            double VatTotal = 0;
            double _DiscountAmountTotal = 0;
            CalculatedCartValues oValues = new CalculatedCartValues();
            foreach (ProductItem itm in shopcart.CartItemsList)
            {
                _priceTotal += Convert.ToDouble(itm.Qty1BaseCharge1 ?? 0);
                _DiscountAmountTotal += itm.DiscountedAmount ?? 0;
                VatTotal += itm.Qty1Tax1Value ?? 0;
            }

            oValues.SubTotal =  Convert.ToString(_priceTotal);
            oValues.DiscountAmount =  Convert.ToString(_DiscountAmountTotal);
            if (shopcart.DeliveryCost > 0)
            {
                oValues.DeliveryCost = shopcart.DeliveryCost.ToString();
            }
            else
            {

                oValues.DeliveryCost = Convert.ToString(0);
            }

            if (shopcart.DeliveryTaxValue > 0)
            {
                oValues.Tax = Convert.ToString(shopcart.DeliveryTaxValue + VatTotal);
            }
            else
            {
                oValues.Tax =  Convert.ToString(VatTotal);
            }

            if (shopcart.DeliveryCost > 0)
            {
                oValues.GrandTotal =  Convert.ToString(_priceTotal + (shopcart.DeliveryTaxValue + VatTotal) + shopcart.DeliveryCost);
                
            }
            else
            {
                oValues.GrandTotal =  Convert.ToString(_priceTotal + VatTotal);
            }
            return oValues;
        }

        private List<CostCentre> GetDeliveryCostCenterList()
        {
            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
            {
                return _ICostCenterService.GetCorporateDeliveryCostCentersList(_myClaimHelper.loginContactCompanyID());
            }
            else
            {
                return _ICostCenterService.GetCorporateDeliveryCostCentersList(UserCookieManager.WBStoreId);
            }
        }


        private ShoppingCart LoadShoppingCart(long orderID, ShopCartAddressSelectViewModel Model)
        {
            ShoppingCart shopCart = null;


            double _deliveryCost = 0;
            double _deliveryCostTaxVal = 0;
            shopCart = _IOrderService.GetShopCartOrderAndDetails(orderID, OrderStatus.ShoppingCart);


            if (shopCart != null)
            {
                Model.SelectedItemsAddonsList = shopCart.ItemsSelectedAddonsList;

                //global values for all items
                CostCentre deliveryCostCenter = null;
                int deliverCostCenterID;
                _deliveryCost = shopCart.DeliveryCost;

                Model.DeliveryCost = shopCart.DeliveryCost;
                _deliveryCostTaxVal = shopCart.DeliveryTaxValue;

                Model.DeliveryCostTaxVal = shopCart.DeliveryTaxValue;
                BillingID = shopCart.BillingAddressID;
                ShippingID = shopCart.ShippingAddressID;

            }
            return shopCart;
        }

        private void BindGridView(ShoppingCart shopCart, ShopCartAddressSelectViewModel model)
        {
            List<ProductItem> itemsList = null;

            if (shopCart != null)
            {
                itemsList = shopCart.CartItemsList;
                model.shopcart.CartItemsList = itemsList;
                model.SelectedDeliveryCostCentreId = shopCart.DeliveryCostCenterID;
                model.DeliveryDiscountVoucherID = shopCart.DeliveryDiscountVoucherID;
                if (itemsList != null && itemsList.Count > 0)
                {
                    BindGriViewWithProductItemList(itemsList, model);
                    return;
                }


            }


        }
        private void BindGriViewWithProductItemList(List<ProductItem> itemsList, ShopCartAddressSelectViewModel model)
        {

            model.ProductItems = itemsList;
        }

        private void BindCountriesDropDownData(Organisation basresponseOrg, Company basresponseCom, ShopCartAddressSelectViewModel model)
        {
            List<Country> country = _IOrderService.PopulateBillingCountryDropDown();
            PopulateBillingCountryDropDown(country, model);
            PopulateShipperCountryDropDown(country, model);
            PopulateStateDropDown(model);

            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
            {


                model.PickUpAddress = basresponseOrg.Address1 + " " + basresponseOrg.Address2 + " " + basresponseOrg.City + "," + basresponseOrg.Country + "," + basresponseOrg.State + " " + basresponseOrg.ZipCode;

            }
            else
            {// corporate
                Address pickupAddress = _myCompanyService.GetAddressByID(basresponseCom.PickupAddressId ?? 0);
                if (pickupAddress != null)
                {
                    model.PickUpAddress = pickupAddress.Address1 + " " + pickupAddress.Address2 + " " + pickupAddress.City + "," + pickupAddress.Country + "," + pickupAddress.State + " " + pickupAddress.PostCode;


                }
            }


        }
        private void PopulateBillingCountryDropDown(List<Country> Countries, ShopCartAddressSelectViewModel model)
        {

            model.BillingCountries = Countries;
            model.DDBillingCountries = new SelectList(Countries, "CountryId", "CountryName"); ;
            // select a country 
        }
        private void PopulateShipperCountryDropDown(List<Country> Countries, ShopCartAddressSelectViewModel model)
        {

            model.ShippingCountries = Countries;
            model.DDShippingCountries = new SelectList(Countries, "CountryId", "CountryName"); ;

        }

        private void PopulateStateDropDown(ShopCartAddressSelectViewModel model)
        {

            List<State> states = _IOrderService.GetStates();



            List<State> newState = new List<State>();

            foreach (var state in states)
            {
                State objState = new State();
                objState.StateId = state.StateId;
                objState.CountryId = state.CountryId;
                objState.StateName = (state.StateName == null || state.StateName == "") ? "N/A" : state.StateName;
                objState.StateCode = (state.StateCode == null || state.StateCode == "") ? "N/A" : state.StateCode;

                newState.Add(objState);

            }

            model.BillingStates = newState;

            model.ShippingStates = newState;

            model.DDBillingStates = new SelectList(states, "StateId", "StateName");

            model.DDShippingStates = new SelectList(states, "StateId", "StateName");





        }
        private void BindDeliveryCostCenterDropDown(List<CostCentre> costCenterList, long OrderID, ShopCartAddressSelectViewModel Model)
        {
            if (costCenterList != null)
            {
                Model.DeliveryCostCenters = costCenterList;
                Model.DDDeliveryCostCenters = new SelectList(costCenterList, "CostCentreId", "Name");

                Item Record = _IItemService.GetItemByOrderID(OrderID);
                if (Record != null)
                {
                    foreach (var cost in costCenterList)
                    {
                        if (cost.Name == Record.ProductName)
                        {
                            Model.SelectedCostCentre = cost.CostCentreId;
                        }
                        else
                        {
                            Model.SelectedCostCentre = 0;

                        }
                    }
                }
                else
                {
                    Model.SelectedCostCentre = 0;
                }



            }

        }


        private void FillUpAddressDropDowns(List<Address> addreses, ShopCartAddressSelectViewModel model)
        {
            if (addreses != null && addreses.Count > 0)
            {
                List<Address> newaddress = new List<Address>();

                foreach (var address in addreses)
                {

                    Address addressObj = new Address();
                    addressObj.AddressId = address.AddressId;
                    addressObj.AddressName = address.AddressName;
                    addressObj.City = address.City;
                    addressObj.Address1 = address.Address1;
                    addressObj.Address2 = address.Address2;
                    addressObj.PostCode = address.PostCode;
                    addressObj.Tel1 = address.Tel1;
                    addressObj.IsDefaultShippingAddress = address.IsDefaultShippingAddress;
                    addressObj.IsDefaultAddress = address.IsDefaultAddress;
                    addressObj.StateId = address.StateId;
                    if (address.State != null)
                        addressObj.Tel2 = address.State.StateName; // because of circullar reference error in json 
                    else
                        addressObj.Tel2 = "";
                    if (address.Country != null)
                        addressObj.FAO = address.Country.CountryName; // because of circullar reference error in json 
                    else
                        addressObj.FAO = "";
                    addressObj.CountryId = address.CountryId;

                    newaddress.Add(addressObj);
                }
                model.ShippingAddresses = newaddress;
                model.BillingAddresses = newaddress;

                model.DDBillingAddresses = new SelectList(addreses, "AddressId", "AddressName");
                model.DDShippingAddresses = new SelectList(addreses, "AddressId", "AddressName");

            }

        }

        #endregion

        #region ConfirmOrder
        [HttpPost]
        public ActionResult Index(ShopCartAddressSelectViewModel model)
        {
            try
            {
                //string CacheKeyName = "CompanyBaseResponse";
                //ObjectCache cache = MemoryCache.Default;

                //MPC.Models.ResponseModels.MyCompanyDomainBaseReponse StoreBaseResopnse = (cache.Get(CacheKeyName) as Dictionary<long, MPC.Models.ResponseModels.MyCompanyDomainBaseReponse>)[UserCookieManager.WBStoreId];
                MyCompanyDomainBaseReponse StoreBaseResopnse = _myCompanyService.GetStoreCachedObject(UserCookieManager.WBStoreId);

                OrganisationID = StoreBaseResopnse.Organisation.OrganisationId;

                //int id = model.SelectedDeliveryAddress;
                string addLine1 = model.ShippingAddress.Address1;
                string city = model.ShippingAddress.City;
                string PostCode = model.ShippingAddress.PostCode;
                CompanyContact contact = _myCompanyService.GetContactByID(_myClaimHelper.loginContactID());

                bool Result = ConfirmOrder(1, addLine1, city, PostCode, StoreBaseResopnse.Company, model, StoreBaseResopnse.Organisation);
                if (Result)
                {
                    Response.Redirect("/OrderConfirmation/" + UserCookieManager.WEBOrderId);
                    return null;
                }
                else
                {
                    if (StoreBaseResopnse.Company.ShowPrices ?? true)
                    {
                        ViewBag.IsShowPrices = true;
                        //do nothing because pricing are already visible.
                    }
                    else
                    {
                        ViewBag.IsShowPrices = false;
                        //  cntRightPricing1.Visible = false;
                    }
                    List<Address> customerAddresses = new List<Address>();
                    CompanyTerritory Territory = new CompanyTerritory();
                    List<CostCentre> deliveryCostCentersList = null;
                    ShoppingCart shopCart = LoadShoppingCart(UserCookieManager.WEBOrderId, model);

                    model.shopcart = shopCart;

                    BindGridView(shopCart, model);

                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                    {
                        if (StoreBaseResopnse.Company.isStoreModePrivate == true)
                        {
                            // if role is admin
                            if (_myClaimHelper.loginContactRoleID() == (int)Roles.Adminstrator)
                                customerAddresses = _myCompanyService.GetAddressByCompanyID(UserCookieManager.WBStoreId);
                            // if role is manager
                            else if (_myClaimHelper.loginContactRoleID() == (int)Roles.Manager)
                            {
                                // get territory of manager

                                Territory = _myCompanyService.GetTerritoryById(contact.TerritoryId ?? 0);

                                if (Territory != null)
                                {
                                    List<CompanyContact> ContactTerritoriesIDs = new List<CompanyContact>();
                                    List<Address> Manageraddresses = new List<Address>();
                                    List<int> AddressIDs = new List<int>();
                                    int BillingAddressID = 0;
                                    int ShippingAddressID = 0;

                                    customerAddresses = _myCompanyService.GetAddressesByTerritoryID(Territory.TerritoryId);


                                }
                            }// if role is user
                            else if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
                            {

                                // get addresses of contact where isprivate is true
                                customerAddresses = _myCompanyService.GetAdressesByContactID(_myClaimHelper.loginContactID());
                                if (contact.TerritoryId != null)
                                {
                                    List<int> TerritoryDefaultAddress = new List<int>();
                                    // get territory of contact
                                    CompanyTerritory ContactTerritory = _myCompanyService.GetTerritoryById(contact.TerritoryId ?? 0);
                                    if (ContactTerritory != null)
                                    {
                                        List<Address> addresses = _myCompanyService.GetBillingAndShippingAddresses(ContactTerritory.TerritoryId);

                                        if (addresses != null)
                                        {

                                            foreach (Address address in addresses)
                                            {
                                                customerAddresses.Add(address);
                                            }
                                        }
                                    }
                                }

                            }
                        }
                        else
                        {
                            customerAddresses = _myCompanyService.GetAddressByCompanyID(UserCookieManager.WBStoreId);
                        }
                    }
                    else
                    {
                        customerAddresses = _myCompanyService.GetContactCompanyAddressesList(UserCookieManager.WBStoreId);
                    }
                    if (customerAddresses != null && customerAddresses.Count > 0)
                    {
                        //ViewBag.listitem = new SelectList(customerAddresses.ToList(), "AddressId", "AddressName");
                        FillUpAddressDropDowns(customerAddresses, model);

                    }


                    model.DDBillingAddresses = new SelectList(model.BillingAddresses, "AddressId", "AddressName");
                    model.DDShippingAddresses = new SelectList(model.ShippingAddresses, "AddressId", "AddressName");

                    deliveryCostCentersList = GetDeliveryCostCenterList();
                    BindDeliveryCostCenterDropDown(deliveryCostCentersList, UserCookieManager.WEBOrderId, model);

                    List<Country> country = _IOrderService.PopulateBillingCountryDropDown();
                    PopulateBillingCountryDropDown(country, model);
                    PopulateShipperCountryDropDown(country, model);

                    PopulateStateDropDown(model);
                    model.LtrMessageToDisplay = true;
                    model.LtrMessage = "Error occurred while updating order.";

                    return View("PartialViews/ShopCartAddressSelect", model);
                }


            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), OrganisationID);
            }

        }

        private bool ConfirmOrder(int modOverride, string AddLine1, string city, string PostCode, Company baseresponseComp, ShopCartAddressSelectViewModel model, Organisation baseresponseOrg)
        {

            bool isPageValid = true;

            if (isPageValid)
            {

                //Double ServiceTaxRate = GetTAXRateFromService(AddLine1, city, PostCode, model);

                bool result = false;

                string voucherCode = string.Empty;
                double grandOrderTotal = 0;

                string yourRefNumber = string.Empty;
                string notes = null;
                string specialTelNumber = null;
                Address billingAdd = null;
                Address deliveryAdd = null;

                //  OrderManager oMgr = new OrderManager();
                CompanyContact user = _myCompanyService.GetContactByID(_myClaimHelper.loginContactID());
                if (user != null)
                {
                    if (!string.IsNullOrEmpty(model.RefNumber))
                    {

                        yourRefNumber = model.RefNumber;

                    }
                    else
                    {

                        yourRefNumber = model.RefNumRetail;
                    }
                    if (!string.IsNullOrEmpty(yourRefNumber))
                    {
                        yourRefNumber = yourRefNumber.Trim();
                    }


                    specialTelNumber = model.ContactTel;

                    notes = model.Notes;
                    if (!string.IsNullOrEmpty(notes))
                        notes = notes.Trim();

                    double total = Convert.ToDouble(model.GrandTotal);
                    grandOrderTotal = total;

                    PrepareAddrssesToSave(out billingAdd, out deliveryAdd, baseresponseComp, model);

                    if (true)
                    {

                        try
                        {
                            if(true)//if (UpdateDeliveryCostCenterInOrder(model, baseresponseOrg, baseresponseComp))
                            {
                                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
                                {
                                    if (baseresponseComp.isCalculateTaxByService == true)
                                    {

                                        double TaxRate = GetTAXRateFromService(AddLine1, city, PostCode, model);

                                        //double TaxRate = 0.04;
                                        if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                                            _IOrderService.updateTaxInCloneItemForServic(UserCookieManager.WEBOrderId, TaxRate, StoreMode.Corp);
                                        else
                                            _IOrderService.updateTaxInCloneItemForServic(UserCookieManager.WEBOrderId, TaxRate, StoreMode.Retail);

                                    }
                                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                                        result = _IOrderService.UpdateOrderWithDetailsToConfirmOrder(UserCookieManager.WEBOrderId, _myClaimHelper.loginContactID(), OrderStatus.ShoppingCart, billingAdd, deliveryAdd, _IOrderService.UpdateORderGrandTotal(UserCookieManager.WEBOrderId), yourRefNumber, specialTelNumber, notes, true, StoreMode.Corp);
                                    else
                                        result = _IOrderService.UpdateOrderWithDetailsToConfirmOrder(UserCookieManager.WEBOrderId, _myClaimHelper.loginContactID(), OrderStatus.ShoppingCart, billingAdd, deliveryAdd, _IOrderService.UpdateORderGrandTotal(UserCookieManager.WEBOrderId), yourRefNumber, specialTelNumber, notes, true, StoreMode.Retail);
                                }
                                else
                                {
                                    if (baseresponseComp.isCalculateTaxByService == true)
                                    {
                                        try
                                        {
                                            double TaxRate = GetTAXRateFromService(AddLine1, city, PostCode, model);

                                            // double TaxRate = 0.04;
                                            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                                                _IOrderService.updateTaxInCloneItemForServic(UserCookieManager.WEBOrderId, TaxRate, StoreMode.Corp);
                                            else
                                                _IOrderService.updateTaxInCloneItemForServic(UserCookieManager.WEBOrderId, TaxRate, StoreMode.Retail);
                                        }
                                        catch (Exception ex)
                                        {
                                            model.LtrMessageToDisplay = true;

                                            model.LtrMessage = "TAX Service Error";

                                        }
                                    }
                                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                                        result = _IOrderService.UpdateOrderWithDetailsToConfirmOrder(UserCookieManager.WEBOrderId, _myClaimHelper.loginContactID(), OrderStatus.ShoppingCart, billingAdd, deliveryAdd, _IOrderService.UpdateORderGrandTotal(UserCookieManager.WEBOrderId), yourRefNumber, specialTelNumber, notes, true, StoreMode.Corp);
                                    else
                                        result = _IOrderService.UpdateOrderWithDetailsToConfirmOrder(UserCookieManager.WEBOrderId, _myClaimHelper.loginContactID(), OrderStatus.ShoppingCart, billingAdd, deliveryAdd, _IOrderService.UpdateORderGrandTotal(UserCookieManager.WEBOrderId), yourRefNumber, specialTelNumber, notes, true, StoreMode.Retail);
                                }



                            }
                            return result;
                        }
                        catch (Exception ex)
                        {
                            model.LtrMessageToDisplay = true;
                            return false;

                            model.LtrMessage = "Error occurred while updating order in catch block.";

                            throw new MPCException(ex.ToString(), baseresponseOrg.OrganisationId);


                        }

                    }
                    else
                    {
                        return false;
                    }


                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }

        }

        private double GetTAXRateFromService(string Address, string City, string PostalCode, ShopCartAddressSelectViewModel model)
        {

            string sServiceObjectURL = ConfigurationSettings.AppSettings["TaxServiceUrl"].ToString();
            string sLicenseKey = ConfigurationSettings.AppSettings["ServiceLicenceKey"].ToString();
            double StateTax = 0;
            //string sPostalCode = "00501";//From User Input into the delivery address i.e. hardcode is NewYark zip code
            try
            {
                Uri sParameterURl = new Uri(string.Format("{0}/GetTaxInfoByZip_V2?PostalCode={1}&TaxType=sales&LicenseKey={2}", sServiceObjectURL, PostalCode, sLicenseKey));
                WebRequest request = WebRequest.Create(sParameterURl);
                string sServices = string.Empty;

                using (WebResponse response = request.GetResponse())
                {

                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream);
                        sServices = reader.ReadToEnd();
                        var xmlDoc2 = new XmlDocument();
                        xmlDoc2.LoadXml(sServices);
                        XmlNodeList statusNode = xmlDoc2.GetElementsByTagName("TaxInfo");
                        if (statusNode[0] != null)
                        {
                            USTaxInfo TaxInfo = new USTaxInfo();
                            TaxInfo.TotalTaxRate = Convert.ToDecimal(statusNode[0].ChildNodes[6].InnerText);
                            TaxInfo.CityRate = Convert.ToDecimal(statusNode[0].ChildNodes[8].InnerText);

                            TaxInfo.StateName = statusNode[0].ChildNodes[4].InnerText;

                            StateTax = Convert.ToDouble(TaxInfo.CityRate);
                        }
                        stream.Close();
                        reader.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                model.LtrMessageToDisplay = true;

                model.LtrMessage = "Tax Service Licence Key Expired";
            }
            return StateTax;
            //Tax calculation code ends here.
        }

        protected void PrepareAddrssesToSave(out Address billingAdd, out Address deliveryAdd, Company baseResponse, ShopCartAddressSelectViewModel model)
        {
            billingAdd = null;

            deliveryAdd = new Address();

            //deliveryAdd.AddressName = Request.Form["txtShipAddressName"];
            //deliveryAdd.Address1 = Request.Form["txtShipAddLine1"];
            //deliveryAdd.Address2 = Request.Form["txtShipAddressLine2"];
            //deliveryAdd.City = Request.Form["txtShipAddCity"];
            deliveryAdd.AddressName = model.ShippingAddress.AddressName;
            deliveryAdd.Address1 = model.ShippingAddress.Address1;
            deliveryAdd.Address2 = model.ShippingAddress.Address2;
            deliveryAdd.City = model.ShippingAddress.City;
            //  deliveryAdd.State = Request.Form["txtShipAddLine1"]; ddShippingState.SelectedItem.Text.ToString();
            //deliveryAdd.PostCode = Request.Form["txtShipPostCode"];
            //deliveryAdd.Tel1 = Request.Form["txtShipContact"];

            deliveryAdd.PostCode = model.ShippingAddress.PostCode;
            deliveryAdd.Tel1 = model.ShippingAddress.Tel1;
            deliveryAdd.StateId = model.SelectedDeliveryState;
            deliveryAdd.CountryId = model.SelectedDeliveryCountry;
            long TerritoryID = _myCompanyService.GetContactTerritoryID(_myClaimHelper.loginContactID());
            //  deliveryAdd.Country = Request.Form["txtShipAddLine1"]; ddShippingCountry.SelectedItem.Text.ToString();



            if (model.SelectedDeliveryAddress == 0) // New Delivery address
            {
                deliveryAdd.AddressId = 0; //Convert.ToInt32(txthdnDeliveryAddressID.Value);
                deliveryAdd.IsDefaultShippingAddress = false; //Convert.ToBoolean(txthdnDeliveryDefaultShippingAddress.Value);
                deliveryAdd.IsDefaultAddress = false; //Convert.ToBoolean(txthdnDeliveryDefaultAddress.Value);


                if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                {
                    if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
                    {
                        if (baseResponse.isStoreModePrivate == true)
                            deliveryAdd.isPrivate = true;
                        else
                            deliveryAdd.isPrivate = false;
                        deliveryAdd.ContactId = _myClaimHelper.loginContactID();

                    }
                    else
                    {
                        deliveryAdd.isPrivate = false;

                        // if (SessionParameters.CustomerContact.ContactRoleID == (int)Roles.Manager)
                        deliveryAdd.TerritoryId = TerritoryID;
                    }

                }
            }
            else
            {
                string IsDefaultAddress = Request.Form["txthdnDeliveryDefaultAddress"];
                deliveryAdd.AddressId = Convert.ToInt32(model.SelectedDeliveryAddress);  //Convert.ToInt32(txthdnDeliveryAddressID.Value);

                deliveryAdd.IsDefaultShippingAddress = model.ShippingAddress.IsDefaultShippingAddress;

                if (IsDefaultAddress == "true")
                {
                    deliveryAdd.IsDefaultAddress = true;
                }
                else
                {
                    deliveryAdd.IsDefaultAddress = false;
                }


            }

            //string hfchkBoxDeliverySameAsBilling = Request.Form["hfchkBoxDeliverySameAsBilling"];
            string hfchkBoxDeliverySameAsBilling = Request.Form["isSameBillingAddress"];
            // string hfchkBoxDeliverySameAsBilling = model.chkBoxDeliverySameAsBilling;
            if (hfchkBoxDeliverySameAsBilling == "False")
            {
                //billing delivery

                billingAdd = new Address();

                //billingAdd.AddressName = Request.Form["txtBillingName"];
                //billingAdd.Address1 = Request.Form["txtAddressLine1"];
                //billingAdd.Address2 = Request.Form["txtAddressLine2"];
                //billingAdd.City = Request.Form["txtCity"];

                billingAdd.AddressName = model.BillingAddress.AddressName;
                billingAdd.Address1 = model.BillingAddress.Address1;
                billingAdd.Address2 = model.BillingAddress.Address2;
                billingAdd.City = model.BillingAddress.City;

                // billingAdd.State = txtState.Value;
                //billingAdd.PostCode = Request.Form["txtZipPostCode"];
                //billingAdd.Tel1 = Request.Form["txtContactNumber"];

                billingAdd.PostCode = model.BillingAddress.PostCode;
                billingAdd.Tel1 = model.BillingAddress.Tel1;


                billingAdd.CountryId = model.SelectedBillingCountry;
                //billingAdd.Country = BillingCountryDropDown.SelectedItem.Text.ToString();

                billingAdd.StateId = model.SelectedBillingState;


                //billingAdd.State = ddBillingState.SelectedItem.Text.ToString();

                if (model.SelectedBillingAddress == 0) // New Billing address
                {
                    billingAdd.AddressId = 0;
                    billingAdd.IsDefaultShippingAddress = false;
                    billingAdd.IsDefaultAddress = false;

                    if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
                    {
                        if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
                        {
                            if (baseResponse.isStoreModePrivate == true)
                                billingAdd.isPrivate = true;
                            else
                                billingAdd.isPrivate = false;
                            billingAdd.ContactId = _myClaimHelper.loginContactID();

                        }
                        else
                        {
                            billingAdd.isPrivate = false;
                            // if (SessionParameters.CustomerContact.ContactRoleID == (int)Roles.Manager)
                            billingAdd.TerritoryId = TerritoryID;

                        }
                    }
                }
                else
                {
                    billingAdd.AddressId = model.SelectedBillingAddress; //Convert.ToInt32(txthdnBillingAddressID.Value);

                    billingAdd.IsDefaultShippingAddress = model.BillingAddress.IsDefaultShippingAddress;

                    string IsDefaultAddress = Request.Form["txthdnBillingDefaultAddress"];
                    if (IsDefaultAddress == "true")
                    {
                        billingAdd.IsDefaultAddress = true;
                    }
                    else
                    {
                        billingAdd.IsDefaultAddress = false;
                    }

                }
            }

        }

        //private bool UpdateDeliveryCostCenterInOrder(ShopCartAddressSelectViewModel model, Organisation BaseResponseOrganisation, Company baseResponseCompany)
        //{
            //double Baseamount = 0;
            //double SurchargeAmount = 0;
            //double Taxamount = 0;
            //double CostOfDelivery = 0;
            //bool serviceResult = true;



            //string ShipPostCode = model.ShippingAddress.PostCode;
            //CostCentre SelecteddeliveryCostCenter = null;

            //if (model.SelectedCostCentre != 0)
            //{
            //    SelecteddeliveryCostCenter = _ICostCenterService.GetCostCentreByID(model.SelectedCostCentre);

            //    if (SelecteddeliveryCostCenter.CostCentreId > 0)
            //    {
            //        if (UserCookieManager.WEBStoreMode == (int)StoreMode.Corp)
            //        {
            //            if (model.SelectedDeliveryCountry == 0 || model.SelectedDeliveryState == 0)
            //            {
            //                model.LtrMessageToDisplay = true;

            //                model.LtrMessage = "Please select country or state to countinue.";

            //                serviceResult = false;
            //            }

            //            else
            //            {


            //                if (!string.IsNullOrEmpty(ShipPostCode) && Convert.ToInt32(SelecteddeliveryCostCenter.DeliveryType) == Convert.ToInt32(DeliveryCarriers.Fedex))
            //                {

            //                    serviceResult = GetFedexResponse(out Baseamount, out SurchargeAmount, out Taxamount, out CostOfDelivery, BaseResponseOrganisation, baseResponseCompany, model);

            //                }
            //            }
            //        }
            //        else
            //        {
            //            if (!string.IsNullOrEmpty(ShipPostCode) && Convert.ToInt32(SelecteddeliveryCostCenter.DeliveryType) == Convert.ToInt32(DeliveryCarriers.Fedex))
            //            {


            //                if (model.SelectedDeliveryCountry == 0 || model.SelectedDeliveryState == 0)
            //                {
            //                    model.LtrMessageToDisplay = true;
            //                    model.LtrMessage = "Please select country or state to countinue.";


            //                    serviceResult = false;
            //                }
            //                else
            //                {
            //                    if (!string.IsNullOrEmpty(ShipPostCode) && Convert.ToInt32(SelecteddeliveryCostCenter.DeliveryType) == Convert.ToInt32(DeliveryCarriers.Fedex))
            //                    {
            //                        serviceResult = GetFedexResponse(out Baseamount, out SurchargeAmount, out Taxamount, out CostOfDelivery, BaseResponseOrganisation, baseResponseCompany, model);
            //                    }
            //                }

            //            }
            //        }

            //        if (serviceResult)
            //        {
            //            if (CostOfDelivery == 0)
            //            {
            //                CostOfDelivery = Convert.ToDouble(SelecteddeliveryCostCenter.DeliveryCharges);
            //            }

            //            List<Item> DeliveryItemList = _IItemService.GetListOfDeliveryItemByOrderID(UserCookieManager.WEBOrderId);


            //            if (DeliveryItemList.Count > 1)
            //            {
            //                if (_IItemService.RemoveListOfDeliveryItemCostCenter(Convert.ToInt32(UserCookieManager.WEBOrderId)))
            //                {
            //                    AddNewDeliveryCostCentreToItem(SelecteddeliveryCostCenter, CostOfDelivery, baseResponseCompany);
            //                }
            //            }
            //            else
            //            {
            //                AddNewDeliveryCostCentreToItem(SelecteddeliveryCostCenter, CostOfDelivery, baseResponseCompany);
            //            }
            //        }

            //    }
            //}
            //return serviceResult;
        //}

     
        private bool GetFedexResponse(out double Baseamount, out double SurchargeAmount, out double Taxamount, out double NetFedexCharge, Organisation baseResponseOrganisation, Company baseResponseCompany, ShopCartAddressSelectViewModel model)
        {
            Baseamount = 0;
            SurchargeAmount = 0;
            Taxamount = 0;
            NetFedexCharge = 0;
            try
            {
                string apiURL = "https://wsbeta.fedex.com:443/web-services/rate";
                WebRequest request = null;
                WebResponse response = null;
                request = WebRequest.Create(apiURL);
                // Set the Method property of the request to POST.
                request.Method = "POST";

                string xmlFormate = FedexXML(baseResponseOrganisation, model, baseResponseCompany);

                byte[] byteArray = Encoding.UTF8.GetBytes(xmlFormate);

                //POST /xml HTTP/1.0 Referrer: YourCompanyNameGoesHere Host: ws.fedex.com Port: 443 Accept: image/gif, image/jpeg, image/pjpeg, text/plain, text/html, */* Content-Type: text/xml Content-length: %d

                request.ContentType = "text/xml; encoding='utf-8'";

                request.ContentLength = byteArray.Length;

                Stream dataStream = request.GetRequestStream();

                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                response = request.GetResponse();

                dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);

                string responseFromServer = reader.ReadToEnd();
                if (!string.IsNullOrEmpty(responseFromServer))
                {
                    var xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(responseFromServer);

                    try
                    {
                        string CurrencySymbol = ConfigurationSettings.AppSettings["CurrencyCode"].ToString();
                        XmlNodeList TotalBaseCharge = xmlDoc.GetElementsByTagName("TotalBaseCharge");
                        XmlNodeList TotalFreightDiscounts = xmlDoc.GetElementsByTagName("TotalFreightDiscounts");
                        XmlNodeList TotalNetFreight = xmlDoc.GetElementsByTagName("TotalNetFreight");
                        XmlNodeList TotalSurcharges = xmlDoc.GetElementsByTagName("TotalSurcharges");
                        XmlNodeList TotalTaxes = xmlDoc.GetElementsByTagName("TotalTaxes");
                        XmlNodeList TotalNetFedExCharge = xmlDoc.GetElementsByTagName("TotalNetFedExCharge");
                        XmlNodeList Message = xmlDoc.GetElementsByTagName("Message");
                        XmlNodeList Code = xmlDoc.GetElementsByTagName("Code");
                        if (TotalBaseCharge.Count > 0 && TotalFreightDiscounts.Count > 0 && TotalNetFreight.Count > 0 && TotalSurcharges.Count > 0 && TotalNetFedExCharge.Count > 0 && TotalTaxes.Count > 0)
                        {

                            Baseamount = Convert.ToDouble(TotalBaseCharge.Item(1).InnerText.Replace(CurrencySymbol, ""));

                            SurchargeAmount = Convert.ToDouble(TotalSurcharges.Item(1).InnerText.Replace(CurrencySymbol, ""));
                            Taxamount = Convert.ToDouble(TotalTaxes.Item(1).InnerText.Replace(CurrencySymbol, ""));

                            NetFedexCharge = Convert.ToDouble(TotalNetFedExCharge.Item(1).InnerText.Replace(CurrencySymbol, ""));
                            return true;
                        }
                        else
                        {
                            model.LtrMessageToDisplay = true;

                            string CodeNumber = Code.Item(0).InnerText;
                            if (CodeNumber.Equals("868"))
                            {
                                model.LtrMessage = "FedEx Service is not Available for the Pickup Or Shipping Address";
                            }
                            else
                            {
                                model.LtrMessage = Message.Item(0).InnerText;

                            }

                            return false;

                        }
                    }
                    catch (Exception ex)
                    {

                        model.LtrMessageToDisplay = true;
                        model.LtrMessage = "Error occurred while getting response from fedex service";
                        return false;
                        throw ex;
                    }

                }
                else
                {
                    model.LtrMessageToDisplay = true;
                    model.LtrMessage = "Service response is null";
                    return false;
                }
            }
            catch (Exception ex)
            {
                model.LtrMessageToDisplay = true;
                model.LtrMessage = "There is some problem with the data. Please Contact Your Support Team.";


                return false;
            }

        }

        private string FedexXML(Organisation baseResponseOrg, ShopCartAddressSelectViewModel model, Company baseResponseCompany)
        {
            string SenderName = string.Empty;
            string SenderCompany = string.Empty;
            string SenderPhoneNo = string.Empty;
            string SenderCity = string.Empty;
            string SenderStateCode = string.Empty;
            string SenderPostalCode = string.Empty;
            string SenderCountryCode = string.Empty;
            string SenderAddressline = string.Empty;
            string Currency = string.Empty;


            CompanyContact contact = _myCompanyService.GetContactByID(_myClaimHelper.loginContactID());
            Address address = _myCompanyService.GetAddressByID(contact.AddressId);

            string RecipientName = contact.FirstName;
            string RecipientCoumpnay = string.Empty;
            //string RecipientPhoneNo = Request.Form["txtShipContact"];
            //string RecipientCity = Request.Form["txtShipAddCity"];
            //string Addressline1 = Request.Form["txtShipAddLine1"];

            string RecipientPhoneNo = model.ShippingAddress.Tel1;
            string RecipientCity = model.ShippingAddress.City;
            string Addressline1 = model.ShippingAddress.Address1;

            string RecipientProvinceCode = _myCompanyService.GetStateCodeById(model.SelectedDeliveryState).ToString().Trim();
            string RecipientPostcode = Request.Form["txtShipPostCode"];
            string CountryCode = _myCompanyService.GetCountryCodeById(model.SelectedDeliveryCountry).ToString().Trim();
           

            if (UserCookieManager.WEBStoreMode == (int)StoreMode.Retail)
            {


                if (baseResponseOrg.StateId != null && baseResponseOrg.CountryId != null && baseResponseOrg.ZipCode != null || baseResponseOrg != null)
                {

                    SenderName = baseResponseOrg.OrganisationName.ToString();
                    SenderCompany = baseResponseOrg.OrganisationName.ToString();
                    SenderPhoneNo = baseResponseOrg.Tel.ToString().Trim();
                    SenderCity = baseResponseOrg.City.ToString();
                    SenderStateCode = _myCompanyService.GetStateCodeById(baseResponseOrg.StateId ?? 0).ToString().Trim();
                    SenderCountryCode = _myCompanyService.GetCountryCodeById(baseResponseOrg.CountryId ?? 0).ToString().Trim();
                    SenderPostalCode = baseResponseOrg.ZipCode.ToString().Trim();
                    SenderAddressline = baseResponseOrg.Address1;

                }
                else
                {
                    model.LtrMessageToDisplay = true;
                    model.LtrMessage = "Please Make Sure You Have Entered Correct Delivery PickUp Address";



                }
            }
            else
            {


                string name = baseResponseCompany.Name;

                Address pickUpAddress = _myCompanyService.GetAddressByID(baseResponseCompany.PickupAddressId ?? 0);
                SenderName = baseResponseCompany.Name;
                SenderCompany = baseResponseCompany.Name;
                if (pickUpAddress != null)
                {
                    SenderPhoneNo = pickUpAddress.Tel1;
                    SenderCity = pickUpAddress.City;
                    SenderStateCode = _myCompanyService.GetStateCodeById(Convert.ToInt32(pickUpAddress.StateId));
                    SenderCountryCode = _myCompanyService.GetCountryCodeById(Convert.ToInt32(pickUpAddress.CountryId));
                    SenderPostalCode = pickUpAddress.PostCode;
                    SenderAddressline = pickUpAddress.Address1;

                }

            }


            string cartitems = "";
            string itemscount = "";

            int temp = 1;
            int Counter = 0;
            Estimate tblOrder = _IOrderService.GetOrderByID(UserCookieManager.WEBOrderId);
            if (tblOrder != null)
            {
                List<Item> ClonedITem = _IItemService.GetItemsByOrderID(UserCookieManager.WEBOrderId);

                if (ClonedITem != null)
                {



                    foreach (var item in ClonedITem)
                    {

                        if (Convert.ToInt32(item.ItemType) != Convert.ToInt32(ItemTypes.Delivery))
                        {
                            if (item.Qty1 > 0)
                            {
                                cartitems += GetItemXml(item, temp, baseResponseOrg.OrganisationId) + Environment.NewLine;
                            }

                            temp++;
                        }
                        else
                        {
                            Counter = (ClonedITem.Count) - 1;

                        }

                    }

                }
                if (Counter > 0)
                {
                    itemscount = Counter.ToString();
                }
                else
                {
                    itemscount = ClonedITem.Count.ToString();
                }
            }

            Currency = ConfigurationSettings.AppSettings["CurrencyCode"].ToString();
            string sKey = ConfigurationSettings.AppSettings["FedexKey"].ToString(); // Take it from webconfig.
            string sPassword = ConfigurationSettings.AppSettings["Fpassword"].ToString();
            string sXML = @"<?xml version=""1.0"" encoding=""UTF-8""?>

                        <SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ns1=""http://fedex.com/ws/rate/v13"">
                        <SOAP-ENV:Body>
                        <ns1:RateRequest> <ns1:WebAuthenticationDetail> 
                        <ns1:UserCredential> 
                        <ns1:Key>#Key</ns1:Key> 
                        <ns1:Password>#password</ns1:Password> 
                        </ns1:UserCredential></ns1:WebAuthenticationDetail> 
                        <ns1:ClientDetail> 
                        <ns1:AccountNumber>510087941</ns1:AccountNumber> 
                        <ns1:MeterNumber>118658025</ns1:MeterNumber> 
                        </ns1:ClientDetail> 

                        <ns1:TransactionDetail>
                        <ns1:CustomerTransactionId>***Rate Request using VC#***</ns1:CustomerTransactionId>
                        </ns1:TransactionDetail>
                        <ns1:Version><ns1:ServiceId>crs</ns1:ServiceId>
                        <ns1:Major>13</ns1:Major>
                        <ns1:Intermediate>0</ns1:Intermediate>
                        <ns1:Minor>0</ns1:Minor>
                        </ns1:Version>
                        <ns1:ReturnTransitAndCommit>true</ns1:ReturnTransitAndCommit>
                        <ns1:RequestedShipment> 
                        <ns1:DropoffType>REGULAR_PICKUP</ns1:DropoffType> 
                        <ns1:ServiceType>INTERNATIONAL_ECONOMY</ns1:ServiceType> 
                        <ns1:PackagingType>YOUR_PACKAGING</ns1:PackagingType> 

                        <ns1:TotalInsuredValue><ns1:Currency>#Currency</ns1:Currency></ns1:TotalInsuredValue>


 
                        <ns1:Shipper><ns1:Contact><ns1:PersonName>#SenderName</ns1:PersonName>
                        <ns1:CompanyName>#SenderCompany</ns1:CompanyName>
                        <ns1:PhoneNumber>#SenderPhoneNo</ns1:PhoneNumber></ns1:Contact>
                        <ns1:Address>
                        <ns1:StreetLines>#SenderAddressline</ns1:StreetLines>

                        <ns1:City>#SenderCity</ns1:City>

                        <ns1:StateOrProvinceCode>#SenderStateCode</ns1:StateOrProvinceCode> 

                        <ns1:PostalCode>#SenderPostalCode</ns1:PostalCode><ns1:CountryCode>#SenderCountryCode</ns1:CountryCode>

                        </ns1:Address>
                        </ns1:Shipper> 



                        <ns1:Recipient><ns1:Contact><ns1:PersonName>#RecipientName</ns1:PersonName>
                        <ns1:CompanyName>#RecipientCoumpnay</ns1:CompanyName>
                        <ns1:PhoneNumber></ns1:PhoneNumber>
                        </ns1:Contact><ns1:Address><ns1:StreetLines>#Addressline1</ns1:StreetLines><ns1:City>RecipientCity</ns1:City>

                        <ns1:StateOrProvinceCode>#RecipientProvinceCode</ns1:StateOrProvinceCode> 
                        <ns1:PostalCode>#RecipientPostcode</ns1:PostalCode> 
                        <ns1:CountryCode>#CountryCode</ns1:CountryCode>
                        <ns1:Residential>false</ns1:Residential>
                        </ns1:Address>
                        </ns1:Recipient>
                        <ns1:ShippingChargesPayment><ns1:PaymentType>SENDER</ns1:PaymentType><ns1:Payor> 
                        <ns1:ResponsibleParty> 
                        <ns1:AccountNumber>510087941</ns1:AccountNumber> 
                        </ns1:ResponsibleParty></ns1:Payor></ns1:ShippingChargesPayment> 
                        <ns1:RateRequestTypes>ACCOUNT</ns1:RateRequestTypes>
                        <ns1:PackageCount>#itemscount</ns1:PackageCount>
                        <#items>
                        </ns1:RequestedShipment>
                        </ns1:RateRequest>
                        </SOAP-ENV:Body>
                        </SOAP-ENV:Envelope>";

            sXML = sXML.Replace("#Key", sKey);
            sXML = sXML.Replace("#password", sPassword);
            sXML = sXML.Replace("#RecipientName", RecipientName);
            sXML = sXML.Replace("#RecipientCoumpnay", RecipientCoumpnay);
            sXML = sXML.Replace("#RecipientPhoneNo", RecipientPhoneNo);
            sXML = sXML.Replace("#RecipientCity", RecipientCity);
            sXML = sXML.Replace("#Addressline1", Addressline1);
            sXML = sXML.Replace("#RecipientProvinceCode", RecipientProvinceCode);
            sXML = sXML.Replace("#RecipientPostcode", RecipientPostcode);
            sXML = sXML.Replace("#CountryCode", CountryCode);
            sXML = sXML.Replace("<#items>", cartitems);
            sXML = sXML.Replace("#itemscount", itemscount);


            sXML = sXML.Replace("#SenderName", SenderName);
            sXML = sXML.Replace("#SenderCompany", SenderCompany);
            sXML = sXML.Replace("#SenderAddressline", SenderAddressline);
            sXML = sXML.Replace("#SenderPhoneNo", SenderPhoneNo);
            sXML = sXML.Replace("#SenderCity", SenderCity);
            sXML = sXML.Replace("#SenderStateCode", SenderStateCode);
            sXML = sXML.Replace("#SenderCountryCode", SenderCountryCode);
            sXML = sXML.Replace("#SenderPostalCode", SenderPostalCode);

            sXML = sXML.Replace("#Currency", Currency);

            return sXML;
        }

        private string GetItemXml(Item item, int i, long OID)
        {

            string WeightUnit = _myCompanyService.SystemWeight(OID);
            string LengthUnit = _myCompanyService.SystemLength(OID);
            string sWeight = string.Empty;
            string sHeight = string.Empty;
            string sWidth = string.Empty;
            string sLength = string.Empty;
            string str = string.Empty;
            if (WeightUnit.Equals("GSM"))
            {
                sWeight = Convert.ToString(Math.Round(Convert.ToDouble((WeightConversion(Convert.ToDouble(item.ItemWeight)) * item.Qty1)), 2));
                WeightUnit = "KG";
            }
            else
                sWeight = Convert.ToString(Math.Round(Convert.ToDouble(item.ItemWeight * item.Qty1)));
            if (LengthUnit.Equals("MM"))
            {
                sHeight = Convert.ToString(Math.Ceiling(Conversion(Convert.ToDouble(item.ItemHeight))));
                sWidth = Convert.ToString(Math.Ceiling(Conversion(Convert.ToDouble(item.ItemWidth))));
                sLength = Convert.ToString(Math.Ceiling(Conversion(Convert.ToDouble(item.ItemLength))));
                LengthUnit = "CM";
            }
            else
            {
                sHeight = Convert.ToString(Math.Ceiling(Convert.ToDouble(item.ItemHeight)));
                sWidth = Convert.ToString(Math.Ceiling(Convert.ToDouble(item.ItemWidth)));
                sLength = Convert.ToString(Math.Ceiling(Convert.ToDouble(item.ItemLength)));
            }



            str = @"<ns1:RequestedPackageLineItems>
                  <ns1:SequenceNumber>#i</ns1:SequenceNumber> 
                 <ns1:GroupPackageCount>1</ns1:GroupPackageCount> 
                 <ns1:Weight><ns1:Units>#shipweightunit</ns1:Units><ns1:Value>#weight</ns1:Value></ns1:Weight> 
                 <ns1:Dimensions> <ns1:Length>#length</ns1:Length> <ns1:Width>#width</ns1:Width> 
                 <ns1:Height>#height</ns1:Height> 
                 <ns1:Units>#shiplengthtunit</ns1:Units> 
                 </ns1:Dimensions> 
                 </ns1:RequestedPackageLineItems>";

            //str = str.Replace("#height", Convert.ToString(Math.Ceiling(Convert.ToDouble(item.ItemHeight))));
            //str = str.Replace("#width", Convert.ToString(Math.Ceiling(Convert.ToDouble(item.ItemWidth))));
            //str = str.Replace("#length", Convert.ToString(Math.Ceiling(Convert.ToDouble(item.ItemLength))));
            //str = str.Replace("#weight", Convert.ToString(Math.Round(Convert.ToDouble(item.ItemWeight * item.Qty1))));

            str = str.Replace("#height", sHeight);
            str = str.Replace("#width", sWidth);
            str = str.Replace("#length", sLength);
            str = str.Replace("#weight", sWeight);
            str = str.Replace("#shipweightunit", WeightUnit);
            str = str.Replace("#shiplengthtunit", LengthUnit);
            str = str.Replace("#i", Convert.ToString(i));
            return str;
        }
        private double Conversion(double Milimeter)
        {
            double Cenitmeter = Milimeter / 10;
            return Cenitmeter;

        }
        private double WeightConversion(double Grams)
        {
            double Kg = Grams / 1000;
            return Kg;
        }


        #endregion


   
        [HttpPost]
        public ActionResult BuildSecondDropDownLists(int id)
        {
            var result = _IOrderService.GetStates();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private void RebindBillingStatesDD(ShopCartAddressSelectViewModel Model, Address BillingAddress)
        {
            if (BillingAddress.CountryId != null && BillingAddress.CountryId > 0)
            {
                List<State> BillingStates = _myCompanyService.GetCountryStates(BillingAddress.CountryId ?? 0);
                List<State> newState = new List<State>();

                foreach (var state in BillingStates)
                {
                    State objState = new State();
                    objState.StateId = state.StateId;
                    objState.CountryId = state.CountryId;
                    objState.StateName = (state.StateName == null || state.StateName == "") ? "N/A" : state.StateName;
                    objState.StateCode = (state.StateCode == null || state.StateCode == "") ? "N/A" : state.StateCode;
                    newState.Add(objState);
                }
                
                Model.DDBillingStates = new SelectList(newState, "StateId", "StateName");
            }
            Model.SelectedBillingCountry = BillingAddress.CountryId ?? 0;
            Model.SelectedBillingState = BillingAddress.StateId ?? 0;
        }
        private void RebindShippingStatesDD(ShopCartAddressSelectViewModel Model, Address ShippingAddress)
        {
            if (ShippingAddress.CountryId != null && ShippingAddress.CountryId > 0)
            {
                List<State> BillingStates = _myCompanyService.GetCountryStates(ShippingAddress.CountryId ?? 0);
                List<State> newState = new List<State>();

                foreach (var state in BillingStates)
                {
                    State objState = new State();
                    objState.StateId = state.StateId;
                    objState.CountryId = state.CountryId;
                    objState.StateName = (state.StateName == null || state.StateName == "") ? "N/A" : state.StateName;
                    objState.StateCode = (state.StateCode == null || state.StateCode == "") ? "N/A" : state.StateCode;
                    newState.Add(objState);
                }
                
                Model.DDShippingStates = new SelectList(newState, "StateId", "StateName");
            }
            Model.SelectedDeliveryCountry = ShippingAddress.CountryId ?? 0;
            Model.SelectedDeliveryState = ShippingAddress.StateId ?? 0;
        }
    }
}