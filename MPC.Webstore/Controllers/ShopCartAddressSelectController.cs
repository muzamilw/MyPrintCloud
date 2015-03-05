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
        public ShopCartAddressSelectController(IWebstoreClaimsHelperService myClaimHelper, ICompanyService myCompanyService, IItemService IItemService, ITemplateService ITemplateService, IOrderService IOrderService,ICostCentreService ICostCentreService)
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
               
         
                List<CostCentre> deliveryCostCentersList = null;
                 List<Address> customerAddresses = new List<Address>();
                CompanyTerritory Territory = new CompanyTerritory();
                ShopCartAddressSelectViewModel AddressSelectModel = new ShopCartAddressSelectViewModel();
                ShoppingCart shopCart = null;
                CompanyContact superAdmin = null;
                if (!_myClaimHelper.isUserLoggedIn())
                {
                    // Annonymous user cann't view it.
                    Response.Redirect("/Login");

                }

                UserCookieManager.OrderId = OrderID;
                
                 
                MyCompanyDomainBaseResponse baseresponseOrg = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromOrganisation();
                MyCompanyDomainBaseResponse baseresponseComp = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();
                MyCompanyDomainBaseResponse baseresponseCurr = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCurrency();

                if (!string.IsNullOrEmpty(baseresponseCurr.Currency))
                    AddressSelectModel.Currency = baseresponseCurr.Currency;
                else
                    AddressSelectModel.Currency = string.Empty;

                AddressSelectModel.OrderId = OrderID;
                OrganisationID = baseresponseOrg.Organisation.OrganisationId;

                deliveryCostCentersList = GetDeliveryCostCenterList();

                shopCart = LoadShoppingCart(OrderID,AddressSelectModel);

                AddressSelectModel.shopcart = shopCart;

                BindGridView(shopCart,AddressSelectModel);

                BindCountriesDropDownData(baseresponseOrg,baseresponseComp,AddressSelectModel);
                
                if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                {
                  
                    
                  
                    //chkBoxDeliverySameAsBilling.Checked = true;

                    if(_myClaimHelper.loginContactID() > 0)
                    {
                        superAdmin =  _myCompanyService.GetCorporateAdmin(UserCookieManager.StoreId);
                    }
                 
                    // User is not the super admin.
                    if (superAdmin != null && _myClaimHelper.loginContactID() != superAdmin.ContactId)
                    {
                        
                        AddressSelectModel.HasAdminMessage = true;
                        if (superAdmin != null)
                        {
                            AddressSelectModel.AdminName = baseresponseComp.Company.Name;
                            
                        }
                        else
                        {
                            AddressSelectModel.AdminName = MPC.Webstore.Common.Constants.NotAvailiable;
                        }
                    }
                    AddressSelectModel.OrderId = OrderID;
                    //CompanyContact contactUser = _myCompanyService.GetContactByID(_myClaimHelper.loginContactID());// LoginUser;
                    //if (baseresponseComp.Company.isCalculateTaxByService == true)
                    //{
                    //    // btnConfirmOrder.Text = "Continue"; // continue to next page
                    //}
                    //else
                    //{
                    //    //if ((contactUser != null && (contactUser.ContactRoleID == Convert.ToInt32(Roles.Adminstrator) || contactUser.ContactRoleID == Convert.ToInt32(Roles.Manager))) || (contactUser.ContactRoleID == Convert.ToInt32(Roles.User) && contactUser.canUserPlaceOrderWithoutApproval == true))
                    //    //  btnConfirmOrder.Text = (string)GetGlobalResourceObject("MyResource", "btnConfrmSelectPLACEORDER");
                    //    //else
                    //    // btnConfirmOrder.Text = (string)GetGlobalResourceObject("MyResource", "btnSUBMITFORAPPROVAL");

                    //    if (contactUser.IsPayByPersonalCreditCard == true)
                    //    {
                    //        //ViewBag.divAdminMessage = false;
                    //        //if (SessionParameters.CustomerContact.canPlaceDirectOrder == true)
                    //        //{
                    //        //    // btnDirectDeposit.Visible = true;
                    //        //}
                    //        //btnConfirmOrder.Text = "Pay by Credit Card";//(string)GetGlobalResourceObject("MyResource", "btnConfrmSelectPLACEORDER");
                    //    }
                    //}
                }
                else
                {
                 
                    //if (SessionParameters.tbl_cmsDefaultSettings.isCalculateTaxByService == true)
                    //{
                    //    // btnConfirmOrder.Text = "Continue"; // continue to next page
                    //}
                    //else
                    //{
                    //    // btnConfirmOrder.Text = (string)GetGlobalResourceObject("MyResource", "btnConfrmSelectPLACEORDER");
                    //}
                }

                AddressSelectModel.LtrMessageToDisplay = false;
                //Addresses panel
                if (shopCart != null)
                {
                    BindDeliveryCostCenterDropDown(deliveryCostCentersList,OrderID,AddressSelectModel);

                    

                    //chkBoxDeliverySameAsBilling.Checked = true;

                    //string Mobile = _myCompanyService.GetContactMobile(_myClaimHelper.loginContactID()); //SessionParameters.CustomerContact.Mobile;
                    CompanyContact contact = _myCompanyService.GetContactByID(_myClaimHelper.loginContactID());

                    if (contact != null)
                        AddressSelectModel.ContactTel = contact.Mobile;
                    else
                        AddressSelectModel.ContactTel = "";
                   
                    // Bind Company Addresses
                    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                    {
                        if (baseresponseComp.Company.isStoreModePrivate == true)
                        {
                            // if role is admin
                            if (_myClaimHelper.loginContactRoleID() == (int)Roles.Adminstrator)
                                customerAddresses = _myCompanyService.GetAddressByCompanyID(UserCookieManager.StoreId);
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
                            customerAddresses = _myCompanyService.GetAddressByCompanyID(UserCookieManager.StoreId);
                        }
                    }
                    else
                    {
                        customerAddresses = _myCompanyService.GetContactCompanyAddressesList(_myClaimHelper.loginContactCompanyID());
                    }
                    if (customerAddresses != null && customerAddresses.Count > 0)
                    {
                        //ViewBag.listitem = new SelectList(customerAddresses.ToList(), "AddressId", "AddressName");
                        FillUpAddressDropDowns(customerAddresses,AddressSelectModel);

                        if (BillingID != 0 && ShippingID != 0)
                        {
                           
                                long ContactShippingID = 0;
                                Address billingAddress = null;
                                //Default Billing Address
                                if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
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
                               // ViewData["BillingAddress"] = billingAddress;
                                AddressSelectModel.BillingAddress = billingAddress;
                                AddressSelectModel.SelectedBillingCountry = billingAddress.CountryId ?? 0;
                                AddressSelectModel.SelectedBillingState = billingAddress.StateId ?? 0;
                                AddressSelectModel.SelectedBillingAddress = billingAddress.AddressId;
                               // SetBillingAddresControls(billingAddress);
                                if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                                {
                                    if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
                                    {
                                       AddressSelectModel.IsUserRole = true;
                                        if (baseresponseComp.Company.isStoreModePrivate == true)
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
                                   // SetEnabilityBillingAddressControls(billingAddress);
                                }
                                else
                                {
                                    AddressSelectModel.IsUserRole = false;
                                }

                                //Shipping Address

                                if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
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
                                if(shippingAddress == null)
                                {
                                    shippingAddress = customerAddresses.FirstOrDefault();
                                }

                                if (shippingAddress != null)
                                {
                                    
                                    AddressSelectModel.ShippingAddress = shippingAddress;
                                    AddressSelectModel.SelectedDeliveryState = shippingAddress.StateId ?? 0;
                                    AddressSelectModel.SelectedDeliveryCountry = shippingAddress.CountryId ?? 0;
                                    AddressSelectModel.SelectedDeliveryAddress = (int)shippingAddress.AddressId;
                                   // SetShippingAddresControls(shippingAddress);
                                  //  SetEnabilityShippingAddressControls(shippingAddress);
                                }
                               

                                if (billingAddress != null && shippingAddress != null && billingAddress.AddressId == shippingAddress.AddressId)
                                {
                                    
                                    //chkBoxDeliverySameAsBilling.Checked = true;
                                }
                            
                        }

                        else
                        {

                           
                                long ContactShippingID = 0;
                                Address billingAddress = null;
                                //Default Billing Address
                                if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
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
                                AddressSelectModel.SelectedBillingCountry = billingAddress.CountryId ?? 0;
                                AddressSelectModel.SelectedBillingState = billingAddress.StateId ?? 0;
                                AddressSelectModel.SelectedBillingAddress = billingAddress.AddressId;
                            //    SetBillingAddresControls(billingAddress);
                                if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                                {
                              //      SetEnabilityBillingAddressControls(billingAddress);
                                }

                                //Shipping Address

                                if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                                {
                                    if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
                                    {
                                        ContactShippingID = customerAddresses.Where(c => c.AddressId == contact.ShippingAddressId).Select(s => s.AddressId).FirstOrDefault();
                                    }
                                    else
                                    {
                                        ContactShippingID =   Convert.ToInt64(contact.ShippingAddressId);// ContactManager.GetContactShippingID(SessionParameters.ContactID);
                                    } 
                                }
                                else
                                {
                                    ContactShippingID = contact.AddressId;// ContactManager.GetContactShippingIDRetail(SessionParameters.ContactID);

                                }
                                Address shippingAddress = customerAddresses.Where(addr => addr.AddressId == ContactShippingID).FirstOrDefault();
                                
                                if(shippingAddress == null)
                                {
                                    shippingAddress = customerAddresses.FirstOrDefault();
                                }
                               // Is billing and Shipping are same ??
                                if (shippingAddress != null)
                                {
                                    
                                    AddressSelectModel.ShippingAddress = shippingAddress;
                                    AddressSelectModel.SelectedDeliveryState = shippingAddress.StateId ?? 0;
                                    AddressSelectModel.SelectedDeliveryCountry = shippingAddress.CountryId ?? 0;
                                    AddressSelectModel.SelectedDeliveryAddress = (int)shippingAddress.AddressId;
                            //        SetShippingAddresControls(shippingAddress);
                               //     SetEnabilityShippingAddressControls(shippingAddress);
                                }

                                if (billingAddress != null && shippingAddress != null && billingAddress.AddressId == shippingAddress.AddressId)
                                {
                                    //ltrlBillShippTo.Text = (string)GetGlobalResourceObject("MyResource", "lnkBillShipto");
                                 //   chkBoxDeliverySameAsBilling.Checked = true;
                                    AddressSelectModel.chkBoxDeliverySameAsBilling = "True";
                                }
                            

                        }

                    }
                }
              

              //  BaseMasterPage masterPage = MyBaseMasterPage;

                if (baseresponseComp.Company != null)
                {
                  AddressSelectModel.TaxLabel = baseresponseComp.Company.TaxLabel +  " :"; 
                }
                AddressSelectModel.chkBoxDeliverySameAsBilling = "True";
                AddressSelectModel.Warning = "warning"; // Utils.GetKeyValueFromResourceFile("lnkWarnMesg", UserCookieManager.StoreId) + " " + baseresponseOrg.Organisation.Country + "."; // (string)GetGlobalResourceObject("MyResource", "lnkWarnMesg") + " " + companySite.Country + ".";
              
              // ((BaseMasterPage)this.Page.Master).pageTitle = (string)GetGlobalResourceObject("MyResource", "lblPageTitleCheckout") + SessionParameters.CompanySite.CompanySiteName;
                
                return View("PartialViews/ShopCartAddressSelect",AddressSelectModel);
            }
            catch(Exception ex)
            {
                throw new MPCException(ex.ToString(), OrganisationID);
            }
            
        }

 
        private List<CostCentre> GetDeliveryCostCenterList()
        {
           if ( UserCookieManager.StoreMode ==  (int)StoreMode.Corp)
           { 
                return _ICostCenterService.GetCorporateDeliveryCostCentersList(_myClaimHelper.loginContactCompanyID());
           }
            else
           {
               return _ICostCenterService.GetCorporateDeliveryCostCentersList(UserCookieManager.StoreId);
           }
        }


        private ShoppingCart LoadShoppingCart(long orderID,ShopCartAddressSelectViewModel Model)
        {
            ShoppingCart shopCart = null;

          
            double _deliveryCost = 0;
            double _deliveryCostTaxVal = 0;
            shopCart = _IOrderService.GetShopCartOrderAndDetails(orderID, OrderStatus.ShoppingCart);
            

            if (shopCart != null)
            {
                Model.SelectedItemsAddonsList = shopCart.ItemsSelectedAddonsList;
                //ViewData["selectedItemsAddonsList"] = _selectedItemsAddonsList;
                //global values for all items
                CostCentre deliveryCostCenter = null;
                int deliverCostCenterID;
                _deliveryCost = shopCart.DeliveryCost;
              //  ViewBag.DeliveryCost = shopCart.DeliveryCost;
                Model.DeliveryCost = shopCart.DeliveryCost;
                _deliveryCostTaxVal = shopCart.DeliveryTaxValue;
              //  ViewBag.DeliveryCostTaxVal = _deliveryCostTaxVal;
                Model.DeliveryCostTaxVal = shopCart.DeliveryTaxValue;
                BillingID = shopCart.BillingAddressID;
                ShippingID = shopCart.ShippingAddressID;

            }
            return shopCart;
        }

        private void BindGridView(ShoppingCart shopCart,ShopCartAddressSelectViewModel model)
        {
            List<ProductItem> itemsList = null;

            if (shopCart != null)
            {
                itemsList = shopCart.CartItemsList;
                model.shopcart.CartItemsList = itemsList;
                if (itemsList != null && itemsList.Count > 0)
                {
                    BindGriViewWithProductItemList(itemsList,model);
                    return;
                }


            }

            // Empty shopping cart
            
          
        }
        private void BindGriViewWithProductItemList(List<ProductItem> itemsList,ShopCartAddressSelectViewModel model)
        {
           
            model.ProductItems = itemsList;
        }

        private void BindCountriesDropDownData(MyCompanyDomainBaseResponse basresponseOrg, MyCompanyDomainBaseResponse basresponseCom,ShopCartAddressSelectViewModel model)
        {
            List<Country> country = _IOrderService.PopulateBillingCountryDropDown();
            PopulateBillingCountryDropDown(country,model);
            PopulateShipperCountryDropDown(country, model);
            PopulateStateDropDown(model);

            if (UserCookieManager.StoreMode == (int)StoreMode.Retail)
            {


                model.PickUpAddress = basresponseOrg.Organisation.Address1 + " " + basresponseOrg.Organisation.Address2 + " " + basresponseOrg.Organisation.City + "," + basresponseOrg.Organisation.Country + "," + basresponseOrg.Organisation.State + " " + basresponseOrg.Organisation.ZipCode;

            }
            else
            {// corporate
                Address pickupAddress = _myCompanyService.GetAddressByID(basresponseCom.Company.PickupAddressId ?? 0);
                if (pickupAddress != null)
                {
                    model.PickUpAddress = pickupAddress.Address1 + " " + pickupAddress.Address2 + " " + pickupAddress.City + "," + pickupAddress.Country + "," + pickupAddress.State + " " + pickupAddress.PostCode;


                }
            }

         
        }
        private void PopulateBillingCountryDropDown( List<Country> Countries,ShopCartAddressSelectViewModel model)
        {
           
            model.BillingCountries = Countries;
            model.DDBillingCountries = new SelectList(Countries, "CountryId", "CountryName"); ;
          // select a country 
        }
        private void PopulateShipperCountryDropDown(List<Country> Countries,ShopCartAddressSelectViewModel model)
        {
           
            model.ShippingCountries = Countries;
            model.DDShippingCountries = new SelectList(Countries, "CountryId", "CountryName"); ;

        }

        private void PopulateStateDropDown(ShopCartAddressSelectViewModel model)
        {

            List<State> states = _IOrderService.GetStates();
            


            List<State> newState = new List<State>();
            
            foreach(var state in states)
            {
                State objState = new State();
                objState.StateId = state.StateId;
                objState.CountryId = state.CountryId;
                objState.StateName = state.StateName;
                objState.StateCode = state.StateCode;

                newState.Add(objState);

            }

            model.BillingStates = newState;

            model.ShippingStates = newState;

            model.DDBillingStates = new SelectList(states, "StateId", "StateName");

            model.DDShippingStates = new SelectList(states, "StateId", "StateName");


           
          
          
        }
        private void BindDeliveryCostCenterDropDown(List<CostCentre> costCenterList,long OrderID,ShopCartAddressSelectViewModel Model)
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
        

        private void FillUpAddressDropDowns(List<Address> addreses,ShopCartAddressSelectViewModel model)
        {
            if (addreses != null && addreses.Count > 0)
            {
               
                
            //	AddressSelectModel.ShippingAddresses = addreses;
              //  ViewData["ShipAddresses"] = addreses;
                //AddressSelectModel.BillingAddresses = addreses;
                List<Address> newaddress = new List<Address>();
               
                foreach (var address in addreses)
                {
                    //Address addressObj = new Address
                    //{ 
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
                        
                   // };

                    newaddress.Add(addressObj);
                }
                model.ShippingAddresses = newaddress;
                model.BillingAddresses = newaddress;
                //ViewBag.shippingAddress = newaddress;
                model.DDBillingAddresses = new SelectList(addreses, "AddressId", "AddressName");
                model.DDShippingAddresses = new SelectList(addreses, "AddressId", "AddressName");

                //ViewBag.listitemShipping = new SelectList(addreses, "AddressId", "AddressName");
             
                //ViewBag.listitemBilling = new SelectList(addreses, "AddressId", "AddressName");
                //SelectList list = new SelectList(addreses, "", "");
               // ViewBag.listitem = list;
            }
          
        }

        #endregion

        #region ConfirmOrder 
        [HttpPost]
        public ActionResult Index(ShopCartAddressSelectViewModel model)
        {
            try
            {
               
                    MyCompanyDomainBaseResponse baseresponseOrg = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromOrganisation();
                    MyCompanyDomainBaseResponse baseresponseComp = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();
                    List<Address> customerAddresses = new List<Address>();
                    CompanyTerritory Territory = new CompanyTerritory();
                    List<CostCentre> deliveryCostCentersList = null;
                    OrganisationID = baseresponseOrg.Organisation.OrganisationId;
                    //string addLine1 = Request.Form["txtAddressLine1"];
                    //string city = Request.Form["txtCity"];
                    //string PostCode = Request.Form["txtZipPostCode"];
                    int id = model.SelectedDeliveryAddress;
                    string addLine1 = model.ShippingAddress.Address1;
                    string city = model.ShippingAddress.City;
                    string PostCode = model.ShippingAddress.PostCode;
                    CompanyContact contact = _myCompanyService.GetContactByID(_myClaimHelper.loginContactID());

                    ShoppingCart shopCart = LoadShoppingCart(UserCookieManager.OrderId, model);

                    model.shopcart = shopCart;

                    BindGridView(shopCart, model);

                    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                    {
                        if (baseresponseComp.Company.isStoreModePrivate == true)
                        {
                            // if role is admin
                            if (_myClaimHelper.loginContactRoleID() == (int)Roles.Adminstrator)
                                customerAddresses = _myCompanyService.GetAddressByCompanyID(UserCookieManager.StoreId);
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
                            customerAddresses = _myCompanyService.GetAddressByCompanyID(UserCookieManager.StoreId);
                        }
                    }
                    else
                    {
                        customerAddresses = _myCompanyService.GetContactCompanyAddressesList(UserCookieManager.StoreId);
                    }
                    if (customerAddresses != null && customerAddresses.Count > 0)
                    {
                        //ViewBag.listitem = new SelectList(customerAddresses.ToList(), "AddressId", "AddressName");
                        FillUpAddressDropDowns(customerAddresses, model);

                        //if (BillingID != 0 && ShippingID != 0)
                        //{

                        //    long ContactShippingID = 0;
                        //    Address billingAddress = null;
                        //    //Default Billing Address
                        //    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                        //    {
                        //        if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
                        //        {
                        //            billingAddress = customerAddresses.Where(c => c.AddressId == BillingID).FirstOrDefault();
                        //        }
                        //        else
                        //        {
                        //            // billingAddress = 
                        //            long ContactBillingID = contact.AddressId;
                        //            billingAddress = customerAddresses.Where(c => c.AddressId == BillingID).FirstOrDefault();
                        //        }
                        //    }
                        //    else
                        //    {
                        //        billingAddress = customerAddresses.Where(c => c.AddressId == BillingID).FirstOrDefault();
                        //    }

                        //    if (billingAddress == null)
                        //    {
                        //        //set default address
                        //        billingAddress = customerAddresses.FirstOrDefault();
                        //    }
                        //    // ViewData["BillingAddress"] = billingAddress;
                        //    model.BillingAddress = billingAddress;
                        //    model.SelectedBillingCountry = billingAddress.CountryId ?? 0;
                        //    model.SelectedBillingState = billingAddress.StateId ?? 0;
                        //    model.SelectedBillingAddress = billingAddress.AddressId;
                        //    // SetBillingAddresControls(billingAddress);
                        //    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                        //    {
                        //        if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
                        //        {
                        //            model.IsUserRole = true;
                        //            if (baseresponseComp.Company.isStoreModePrivate == true)
                        //            {
                        //                model.isStoreModePrivate = true;
                        //            }
                        //            else
                        //            {
                        //                model.isStoreModePrivate = false;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            model.IsUserRole = false;
                        //        }
                        //        // SetEnabilityBillingAddressControls(billingAddress);
                        //    }
                        //    else
                        //    {
                        //        model.IsUserRole = false;
                        //    }

                        //    //Shipping Address

                        //    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                        //    {
                        //        if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
                        //        {
                        //            ContactShippingID = customerAddresses.Where(c => c.AddressId == contact.ShippingAddressId).Select(s => s.AddressId).FirstOrDefault();
                        //        }
                        //        else
                        //        {
                        //            ContactShippingID = Convert.ToInt64(contact.ShippingAddressId);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        ContactShippingID = contact.AddressId;

                        //    }
                        //    Address shippingAddress = customerAddresses.Where(addr => addr.AddressId == ShippingID).FirstOrDefault();
                        //    // Is billing and Shipping are same ??
                        //    if (shippingAddress != null)
                        //    {

                        //        model.ShippingAddress = shippingAddress;
                        //        model.SelectedDeliveryState = shippingAddress.StateId ?? 0;
                        //        model.SelectedDeliveryCountry = shippingAddress.CountryId ?? 0;
                        //        model.SelectedDeliveryAddress = (int)shippingAddress.AddressId;
                        //        // SetShippingAddresControls(shippingAddress);
                        //        //  SetEnabilityShippingAddressControls(shippingAddress);
                        //    }

                        //    if (billingAddress != null && shippingAddress != null && billingAddress.AddressId == shippingAddress.AddressId)
                        //    {

                        //        //chkBoxDeliverySameAsBilling.Checked = true;
                        //    }

                        //}

                        //else
                        //{


                        //    long ContactShippingID = 0;
                        //    Address billingAddress = null;
                        //    //Default Billing Address
                        //    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                        //    {
                        //        if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
                        //        {
                        //            billingAddress = customerAddresses.Where(c => c.AddressId == contact.AddressId).FirstOrDefault();
                        //        }
                        //        else
                        //        {
                        //            // billingAddress = 
                        //            long ContactBillingID = contact.AddressId;
                        //            billingAddress = customerAddresses.Where(c => c.AddressId == ContactBillingID).FirstOrDefault();
                        //        }
                        //    }
                        //    else
                        //    {
                        //        billingAddress = customerAddresses.Where(c => c.AddressId == contact.AddressId).FirstOrDefault();
                        //    }

                        //    if (billingAddress == null)
                        //    {
                        //        //set default address
                        //        billingAddress = customerAddresses.FirstOrDefault();
                        //    }

                        //    model.BillingAddress = billingAddress;
                        //    model.SelectedBillingCountry = billingAddress.CountryId ?? 0;
                        //    model.SelectedBillingState = billingAddress.StateId ?? 0;
                        //    model.SelectedBillingAddress = billingAddress.AddressId;
                        //    //    SetBillingAddresControls(billingAddress);
                        //    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                        //    {
                        //        //      SetEnabilityBillingAddressControls(billingAddress);
                        //    }

                        //    //Shipping Address

                        //    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                        //    {
                        //        if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
                        //        {
                        //            ContactShippingID = customerAddresses.Where(c => c.AddressId == contact.ShippingAddressId).Select(s => s.AddressId).FirstOrDefault();
                        //        }
                        //        else
                        //        {
                        //            ContactShippingID = Convert.ToInt64(contact.ShippingAddressId);// ContactManager.GetContactShippingID(SessionParameters.ContactID);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        ContactShippingID = contact.AddressId;// ContactManager.GetContactShippingIDRetail(SessionParameters.ContactID);

                        //    }
                        //    Address shippingAddress = customerAddresses.Where(addr => addr.AddressId == ContactShippingID).FirstOrDefault();
                        //    // Is billing and Shipping are same ??
                        //    if (shippingAddress != null)
                        //    {

                        //        model.ShippingAddress = shippingAddress;
                        //        model.SelectedDeliveryState = shippingAddress.StateId ?? 0;
                        //        model.SelectedDeliveryCountry = shippingAddress.CountryId ?? 0;
                        //        model.SelectedDeliveryAddress = (int)shippingAddress.AddressId;
                        //        //        SetShippingAddresControls(shippingAddress);
                        //        //     SetEnabilityShippingAddressControls(shippingAddress);
                        //    }

                        //    if (billingAddress != null && shippingAddress != null && billingAddress.AddressId == shippingAddress.AddressId)
                        //    {
                        //        //ltrlBillShippTo.Text = (string)GetGlobalResourceObject("MyResource", "lnkBillShipto");
                        //        //   chkBoxDeliverySameAsBilling.Checked = true;
                        //        model.chkBoxDeliverySameAsBilling = "True";
                        //    }


                        //}
                    }


                    model.DDBillingAddresses = new SelectList(model.BillingAddresses, "AddressId", "AddressName");
                    model.DDShippingAddresses = new SelectList(model.ShippingAddresses, "AddressId", "AddressName");

                    deliveryCostCentersList = GetDeliveryCostCenterList();
                    BindDeliveryCostCenterDropDown(deliveryCostCentersList,UserCookieManager.OrderId,model);

                    List<Country> country = _IOrderService.PopulateBillingCountryDropDown();
                    PopulateBillingCountryDropDown(country,model);
                    PopulateShipperCountryDropDown(country,model);

                   PopulateStateDropDown(model);
                   bool Result =   ConfirmOrder(1, addLine1, city, PostCode, baseresponseComp, model, baseresponseOrg);
                    if(Result)
                    {
                        Response.Redirect("/OrderConfirmation/" + UserCookieManager.OrderId);
                        return null;
                    }
                    else
                    {
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

        private bool ConfirmOrder(int modOverride, string AddLine1, string city, string PostCode, MyCompanyDomainBaseResponse baseresponseComp, ShopCartAddressSelectViewModel model, MyCompanyDomainBaseResponse baseresponseOrg)
        {

            bool isPageValid = true;
            //if (hfPageIsValid.Value == "0")
            //{
            //    isPageValid = false;
            //}
            if (isPageValid)
            {


                Double ServiceTaxRate = GetTAXRateFromService(AddLine1, city, PostCode, model);



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
                    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                    {
                        //yourRefNumber = Request.Form["txtYourRefNumber"];
                        yourRefNumber = model.RefNumber;

                    }
                    else
                    {
                        //yourRefNumber = Request.Form["txtYourRefNumberRetail"];
                        yourRefNumber = model.RefNumRetail;
                    }
                    if (!string.IsNullOrEmpty(yourRefNumber))
                    {
                        yourRefNumber = yourRefNumber.Trim();
                    }

                    //	specialTelNumber = Request.Form["txtInstContactTelNumber"];
                    specialTelNumber = model.ContactTel;
                    //notes = Request.Form["txtInstNotes"];
                    notes = model.Notes;
                    if (!string.IsNullOrEmpty(notes))
                        notes = notes.Trim();
                    //string total =   Request.Form["hfGrandTotal"];
                    double total = Convert.ToDouble(model.GrandTotal);
                    grandOrderTotal = total;

                    PrepareAddrssesToSave(out billingAdd, out deliveryAdd, baseresponseComp, model);

                    if (true)
                    {

                        try
                        {
                            if (UpdateDeliveryCostCenterInOrder(model, baseresponseOrg, baseresponseComp))
                            {
                                if (UserCookieManager.StoreMode == (int)StoreMode.Retail)
                                {
                                    if (baseresponseComp.Company.isCalculateTaxByService == true)
                                    {

                                        double TaxRate = GetTAXRateFromService(AddLine1, city, PostCode, model);

                                        //double TaxRate = 0.04;
                                        if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                                            _IOrderService.updateTaxInCloneItemForServic(UserCookieManager.OrderId, TaxRate, StoreMode.Corp);
                                        else
                                            _IOrderService.updateTaxInCloneItemForServic(UserCookieManager.OrderId, TaxRate, StoreMode.Retail);

                                    }
                                    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                                        result = _IOrderService.UpdateOrderWithDetailsToConfirmOrder(UserCookieManager.OrderId, _myClaimHelper.loginContactID(), OrderStatus.ShoppingCart, billingAdd, deliveryAdd, _IOrderService.UpdateORderGrandTotal(UserCookieManager.OrderId), yourRefNumber, specialTelNumber, notes, true, StoreMode.Corp);
                                    else
                                        result = _IOrderService.UpdateOrderWithDetailsToConfirmOrder(UserCookieManager.OrderId, _myClaimHelper.loginContactID(), OrderStatus.ShoppingCart, billingAdd, deliveryAdd, _IOrderService.UpdateORderGrandTotal(UserCookieManager.OrderId), yourRefNumber, specialTelNumber, notes, true, StoreMode.Retail);
                                }
                                else
                                {
                                    if (baseresponseComp.Company.isCalculateTaxByService == true)
                                    {
                                        try
                                        {
                                            double TaxRate = GetTAXRateFromService(AddLine1, city, PostCode, model);

                                            // double TaxRate = 0.04;
                                            if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                                                _IOrderService.updateTaxInCloneItemForServic(UserCookieManager.OrderId, TaxRate, StoreMode.Corp);
                                            else
                                                _IOrderService.updateTaxInCloneItemForServic(UserCookieManager.OrderId, TaxRate, StoreMode.Retail);
                                        }
                                        catch (Exception ex)
                                        {
                                            model.LtrMessageToDisplay = true;

                                            model.LtrMessage = "TAX Service Error";

                                        }
                                    }
                                    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                                        result = _IOrderService.UpdateOrderWithDetailsToConfirmOrder(UserCookieManager.OrderId, _myClaimHelper.loginContactID(), OrderStatus.ShoppingCart, billingAdd, deliveryAdd, _IOrderService.UpdateORderGrandTotal(UserCookieManager.OrderId), yourRefNumber, specialTelNumber, notes, true, StoreMode.Corp);
                                    else
                                        result = _IOrderService.UpdateOrderWithDetailsToConfirmOrder(UserCookieManager.OrderId, _myClaimHelper.loginContactID(), OrderStatus.ShoppingCart, billingAdd, deliveryAdd, _IOrderService.UpdateORderGrandTotal(UserCookieManager.OrderId), yourRefNumber, specialTelNumber, notes, true, StoreMode.Retail);
                                }

                              

                            }
                            return result;
                        }
                        catch (Exception ex)
                        {
                            model.LtrMessageToDisplay = true;
                            return false;

                            model.LtrMessage = "Error occurred while updating order in catch block.";

                            throw new MPCException(ex.ToString(), baseresponseOrg.Organisation.OrganisationId);


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

        private double GetTAXRateFromService(string Address, string City, string PostalCode,ShopCartAddressSelectViewModel model)
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

        protected void PrepareAddrssesToSave(out Address billingAdd, out Address deliveryAdd, MyCompanyDomainBaseResponse baseResponse, ShopCartAddressSelectViewModel model)
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


                if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                {
                    if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
                    {
                        if (baseResponse.Company.isStoreModePrivate == true)
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

                if(IsDefaultAddress == "true")
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

                    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                    {
                        if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
                        {
                            if (baseResponse.Company.isStoreModePrivate == true)
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
                    if(IsDefaultAddress == "true")
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

        private bool UpdateDeliveryCostCenterInOrder(ShopCartAddressSelectViewModel model,MyCompanyDomainBaseResponse BaseResponseOrganisation,MyCompanyDomainBaseResponse baseResponseCompany)
        {
            double Baseamount = 0;
            double SurchargeAmount = 0;
            double Taxamount = 0;
            double CostOfDelivery = 0;
            bool serviceResult = true;

           

            //string ShipPostCode = Request.Form["txtShipPostCode"];
            string ShipPostCode = model.ShippingAddress.PostCode;
            CostCentre SelecteddeliveryCostCenter = null;

            if (model.SelectedCostCentre != 0)
            {
                SelecteddeliveryCostCenter = _ICostCenterService.GetCostCentreByID(model.SelectedCostCentre);

                if (SelecteddeliveryCostCenter.CostCentreId > 0)
                {
                    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                    {
                        if (model.SelectedDeliveryCountry == 0 || model.SelectedDeliveryState == 0)
                        {
                            model.LtrMessageToDisplay = true;
                         
                            model.LtrMessage = "Please select country or state to countinue.";

                            serviceResult = false;
                        }

                        else
                        {
                            

                            if (!string.IsNullOrEmpty(ShipPostCode) && Convert.ToInt32(SelecteddeliveryCostCenter.DeliveryType) == Convert.ToInt32(DeliveryCarriers.Fedex))
                            {

                                serviceResult = GetFedexResponse(out Baseamount, out SurchargeAmount, out Taxamount, out CostOfDelivery,BaseResponseOrganisation,baseResponseCompany,model);

                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(ShipPostCode) && Convert.ToInt32(SelecteddeliveryCostCenter.DeliveryType) == Convert.ToInt32(DeliveryCarriers.Fedex))
                        {


                            if (model.SelectedDeliveryCountry == 0 || model.SelectedDeliveryState == 0)
                            {
                                model.LtrMessageToDisplay = true;
                                model.LtrMessage = "Please select country or state to countinue.";
               

                                serviceResult = false;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(ShipPostCode) && Convert.ToInt32(SelecteddeliveryCostCenter.DeliveryType) == Convert.ToInt32(DeliveryCarriers.Fedex))
                                {
                                    serviceResult = GetFedexResponse(out Baseamount, out SurchargeAmount, out Taxamount, out CostOfDelivery,BaseResponseOrganisation,baseResponseCompany,model);
                                }
                            }

                        }
                    }

                    if (serviceResult)
                    {
                        if (CostOfDelivery == 0)
                        {
                            CostOfDelivery = Convert.ToDouble(SelecteddeliveryCostCenter.SetupCost);
                        }

                        List<Item> DeliveryItemList = _IItemService.GetListOfDeliveryItemByOrderID(UserCookieManager.OrderId);


                        if (DeliveryItemList.Count > 1)
                        {
                            if (_IItemService.RemoveListOfDeliveryItemCostCenter(Convert.ToInt32(UserCookieManager.OrderId)))
                            {
                                AddNewDeliveryCostCentreToItem(SelecteddeliveryCostCenter, CostOfDelivery,baseResponseCompany);
                            }
                        }
                        else
                        {
                            AddNewDeliveryCostCentreToItem(SelecteddeliveryCostCenter, CostOfDelivery,baseResponseCompany);
                        }
                    }

                }
            }
            return serviceResult;
        }

        private void AddNewDeliveryCostCentreToItem(CostCentre SelecteddeliveryCostCenter, double costOfDelivery,MyCompanyDomainBaseResponse baseResponse)
        {
           

           double GetServiceTAX = Convert.ToDouble(Session["ServiceTaxRate"]);
            if (SelecteddeliveryCostCenter != null)
            {
                if (SelecteddeliveryCostCenter.CostCentreId > 0)
                {
                    if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                    {
                        _IItemService.AddUpdateItemFordeliveryCostCenter(UserCookieManager.OrderId, SelecteddeliveryCostCenter.CostCentreId, costOfDelivery, baseResponse.Company.CompanyId, SelecteddeliveryCostCenter.Name, StoreMode.Corp, baseResponse.Company.IsDeliveryTaxAble ?? false, baseResponse.Company.isCalculateTaxByService ?? false,GetServiceTAX,baseResponse.Company.TaxRate ?? 0);
                        
                        
                    }
                    else
                    {
                        _IItemService.AddUpdateItemFordeliveryCostCenter(UserCookieManager.OrderId, SelecteddeliveryCostCenter.CostCentreId, costOfDelivery, baseResponse.Company.CompanyId, SelecteddeliveryCostCenter.Name, StoreMode.Corp, baseResponse.Company.IsDeliveryTaxAble ?? false, baseResponse.Company.isCalculateTaxByService ?? false, GetServiceTAX, baseResponse.Company.TaxRate ?? 0);
                    }

                }
                SelecteddeliveryCostCenter.SetupCost = costOfDelivery;
                bool resultOfDilveryCostCenter = _IOrderService.SaveDilveryCostCenter(UserCookieManager.OrderId, SelecteddeliveryCostCenter);
            }
        }
        private bool GetFedexResponse(out double Baseamount, out double SurchargeAmount, out double Taxamount, out double NetFedexCharge,MyCompanyDomainBaseResponse baseResponseOrganisation,MyCompanyDomainBaseResponse baseResponseCompany,ShopCartAddressSelectViewModel model)
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

        private string FedexXML(MyCompanyDomainBaseResponse baseResponseOrg, ShopCartAddressSelectViewModel model, MyCompanyDomainBaseResponse baseResponseCompany)
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


            if (UserCookieManager.StoreMode == (int)StoreMode.Retail)
            {
                
                
                if (baseResponseOrg.Organisation.StateId != null && baseResponseOrg.Organisation.CountryId != null && baseResponseOrg.Organisation.ZipCode != null || baseResponseOrg.Organisation != null)
                {

                    SenderName = baseResponseOrg.Organisation.OrganisationName.ToString();
                    SenderCompany = baseResponseOrg.Organisation.OrganisationName.ToString();
                    SenderPhoneNo = baseResponseOrg.Organisation.Tel.ToString().Trim();
                    SenderCity = baseResponseOrg.Organisation.City.ToString();
                    SenderStateCode = _myCompanyService.GetStateCodeById(baseResponseOrg.Organisation.StateId ?? 0).ToString().Trim();
                    SenderCountryCode = _myCompanyService.GetCountryCodeById(baseResponseOrg.Organisation.CountryId ?? 0).ToString().Trim();
                    SenderPostalCode = baseResponseOrg.Organisation.ZipCode.ToString().Trim();
                    SenderAddressline = baseResponseOrg.Organisation.Address1;

                }
                else
                {
                    model.LtrMessageToDisplay = true;
                    model.LtrMessage = "Please Make Sure You Have Entered Correct Delivery PickUp Address";
                    


                }
            }
            else
            {

                
                string name = baseResponseCompany.Company.Name;

                Address pickUpAddress = _myCompanyService.GetAddressByID(baseResponseCompany.Company.PickupAddressId ?? 0);
                SenderName = baseResponseCompany.Company.Name;
                SenderCompany = baseResponseCompany.Company.Name;
                if(pickUpAddress != null)
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
            Estimate tblOrder = _IOrderService.GetOrderByID(UserCookieManager.OrderId);
            if (tblOrder != null)
            {
                List<Item> ClonedITem = _IItemService.GetItemsByOrderID(UserCookieManager.OrderId);

                if (ClonedITem != null)
                {



                    foreach (var item in ClonedITem)
                    {

                        if (Convert.ToInt32(item.ItemType) != Convert.ToInt32(ItemTypes.Delivery))
                        {
                            if (item.Qty1 > 0)
                            {
                                cartitems += GetItemXml(item, temp,baseResponseOrg.Organisation.OrganisationId) + Environment.NewLine;
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

        private string GetItemXml(Item item, int i,long OID)
        {

            string WeightUnit =_myCompanyService.SystemWeight(OID);
            string LengthUnit =  _myCompanyService.SystemLength(OID);
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


    }
}