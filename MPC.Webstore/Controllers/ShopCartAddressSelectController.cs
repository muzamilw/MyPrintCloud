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
        double _deliveryCostTaxVal = 0;
        double _deliveryCost = 0;
        long BillingID = 0;
        long ShippingID = 0;
        private List<AddOnCostsCenter> _selectedItemsAddonsList = null;
        private List<Address> customerAddresses = new List<Address>();
        private CompanyTerritory Territory = new CompanyTerritory();
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

        // GET: ShopCartAddressSelect
        public ActionResult Index(long OrderID)
        {
            try
            {
                ShoppingCart shopCart = null;
                CompanyContact superAdmin = null;
                if (!_myClaimHelper.isUserLoggedIn())
                {
                    // Annonymous user cann't view it.
                    RedirectToAction("Index", "Home");

                }


                List<CostCentre> deliveryCostCentersList = null;
                 
                MyCompanyDomainBaseResponse baseresponseOrg = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromOrganisation();
                MyCompanyDomainBaseResponse baseresponseComp = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCompany();


                OrganisationID = baseresponseOrg.Organisation.OrganisationId;

                deliveryCostCentersList = GetDeliveryCostCenterList();

                shopCart = LoadShoppingCart(OrderID);

                BindGridView(shopCart);

                BindCountriesDropDownData(baseresponseOrg,baseresponseComp);

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
                        ViewBag.divAdminMessage = true;

                        if (superAdmin != null)
                        {
                            ViewBag.lblSupAdminName = baseresponseComp.Company.Name;
                        }
                        else
                        {
                            ViewBag.lblSupAdminName = MPC.Webstore.Common.Constants.NotAvailiable;
                        }
                    }

                    CompanyContact contactUser = _myCompanyService.GetContactByID(_myClaimHelper.loginContactID());// LoginUser;
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

                    //    if (baseresponseComp.Company.IsPayByPersonalCreditCard == true)
                    //    {
                    //        ViewBag.divAdminMessage = false;
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

                //Addresses panel
                if (shopCart != null)
                {
                    BindDeliveryCostCenterDropDown(deliveryCostCentersList);

                    ShowONDropDownExistingDeliveryName(OrderID);

                    //chkBoxDeliverySameAsBilling.Checked = true;

                    //string Mobile = _myCompanyService.GetContactMobile(_myClaimHelper.loginContactID()); //SessionParameters.CustomerContact.Mobile;
                    CompanyContact contact = _myCompanyService.GetContactByID(_myClaimHelper.loginContactID());
                    
                    ViewBag.txtInstContactTelNumber = contact.Mobile;
                    ViewBag.lblContactNo = contact.Mobile;
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
                        customerAddresses = _myCompanyService.GetContactCompanyAddressesList(UserCookieManager.StoreId);
                    }
                    if (customerAddresses != null && customerAddresses.Count > 0)
                    {
                        FillUpAddressDropDowns(customerAddresses);

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
                                ViewData["BillingAddress"] = billingAddress;
                               // SetBillingAddresControls(billingAddress);
                                if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
                                {
                                    if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
                                    {
                                        ViewBag.IsUserRole = true;
                                        if (baseresponseComp.Company.isStoreModePrivate == true)
                                        {
                                            ViewBag.isStoreModePrivate = true;
                                        }
                                        else
                                        {
                                            ViewBag.isStoreModePrivate = false;
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.IsUserRole = false;
                                    }
                                   // SetEnabilityBillingAddressControls(billingAddress);
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
                                if (shippingAddress != null)
                                {
                                    ViewData["ShippingAddress"] = shippingAddress;
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
                                ViewData["BillingAddress"] = billingAddress;
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
                                // Is billing and Shipping are same ??
                                if (shippingAddress != null)
                                {
                                    ViewData["ShippingAddress"] = shippingAddress;
                            //        SetShippingAddresControls(shippingAddress);
                               //     SetEnabilityShippingAddressControls(shippingAddress);
                                }

                                if (billingAddress != null && shippingAddress != null && billingAddress.AddressId == shippingAddress.AddressId)
                                {
                                    //ltrlBillShippTo.Text = (string)GetGlobalResourceObject("MyResource", "lnkBillShipto");
                                 //   chkBoxDeliverySameAsBilling.Checked = true;
                                }
                            

                        }

                    }
                }
              

              //  BaseMasterPage masterPage = MyBaseMasterPage;

                if (baseresponseComp.Company != null)
                {
                   ViewBag.lblTaxLabel = baseresponseComp.Company.TaxLabel +  " :"; 
                }
                ViewBag.ltrlWarnnigMEsg = Utils.GetKeyValueFromResourceFile("lnkWarnMesg", UserCookieManager.StoreId) + " " + baseresponseOrg.Organisation.Country + "."; // (string)GetGlobalResourceObject("MyResource", "lnkWarnMesg") + " " + companySite.Country + ".";
              
              // ((BaseMasterPage)this.Page.Master).pageTitle = (string)GetGlobalResourceObject("MyResource", "lblPageTitleCheckout") + SessionParameters.CompanySite.CompanySiteName;
                
                return View();
            }
            catch(Exception ex)
            {
                throw new MPCException(ex.ToString(), OrganisationID);
            }
            
        }

     

 
        private List<CostCentre> GetDeliveryCostCenterList()
        {
            if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
            {
                return _ICostCenterService.GetCorporateDeliveryCostCentersList(_myClaimHelper.loginContactCompanyID());
            }
            else
            {
                return _ICostCenterService.GetDeliveryCostCentersList();
            }
        }


        private ShoppingCart LoadShoppingCart(long orderID)
        {
            ShoppingCart shopCart = null;
           
            shopCart = _IOrderService.GetShopCartOrderAndDetails(orderID, OrderStatus.ShoppingCart);
            

            if (shopCart != null)
            {
                _selectedItemsAddonsList = shopCart.ItemsSelectedAddonsList;
                ViewData["selectedItemsAddonsList"] = _selectedItemsAddonsList;
                //global values for all items
                CostCentre deliveryCostCenter = null;
                int deliverCostCenterID;
                _deliveryCost = shopCart.DeliveryCost;
                ViewBag.DeliveryCost = shopCart.DeliveryCost;

                _deliveryCostTaxVal = shopCart.DeliveryTaxValue;
                ViewBag.DeliveryCostTaxVal = _deliveryCostTaxVal;
                BillingID = shopCart.BillingAddressID;
                ShippingID = shopCart.ShippingAddressID;

            }
            return shopCart;
        }

        private void BindGridView(ShoppingCart shopCart)
        {
            List<ProductItem> itemsList = null;

            if (shopCart != null)
            {
                itemsList = shopCart.CartItemsList;
                if (itemsList != null && itemsList.Count > 0)
                {
                    BindGriViewWithProductItemList(itemsList);
                    return;
                }


            }

            // Empty shopping cart
            
          
        }
        private void BindGriViewWithProductItemList(List<ProductItem> itemsList)
        {
            ViewData["GrdViewShopCart"] = itemsList;

        }

        private void BindCountriesDropDownData(MyCompanyDomainBaseResponse basresponseOrg, MyCompanyDomainBaseResponse basresponseCom)
        {
            List<Country> country = _IOrderService.PopulateBillingCountryDropDown();
            PopulateBillingCountryDropDown(country);
            PopulateShipperCountryDropDown(country);
            PopulateStateDropDown();

            if (UserCookieManager.StoreMode == (int)StoreMode.Retail)
            {


                ViewBag.txtPickUpAddressDetail = basresponseOrg.Organisation.Address1 + " " + basresponseOrg.Organisation.Address2 + " " + basresponseOrg.Organisation.City + "," + basresponseOrg.Organisation.Country + "," + basresponseOrg.Organisation.State + " " + basresponseOrg.Organisation.ZipCode;

            }
            else
            {// corporate
                Address pickupAddress = _myCompanyService.GetAddressByID(basresponseCom.Company.PickupAddressId ?? 0);
                if (pickupAddress != null)
                {
                    ViewBag.txtPickUpAddressDetail = pickupAddress.Address1 + " " + pickupAddress.Address2 + " " + pickupAddress.City + "," + pickupAddress.Country + "," + pickupAddress.State + " " + pickupAddress.PostCode;


                }
            }

         
        }
        private void PopulateBillingCountryDropDown( List<Country> Countries)
        {
            ViewData["Countries"] = Countries;
          // select a country 
        }
        private void PopulateShipperCountryDropDown(List<Country> Countries)
        {
            ViewData["ShipperCountries"] = Countries;
           
        }

        private void PopulateStateDropDown()
        {

            List<State> states = _IOrderService.GetStates();
            ViewData["BillingStates"] = states;
            ViewData["ShippingStates"] = states;
          
          
        }
        private void BindDeliveryCostCenterDropDown(List<CostCentre> costCenterList)
        {
            if (costCenterList != null && costCenterList.Count > 0)
            {
                ViewData["ddlDeliveyCostCenter"] = costCenterList;
           

            }
        }
        private void ShowONDropDownExistingDeliveryName(long orderID) // need to be discussed
        {


            Item Record = _IItemService.GetItemByOrderID(orderID);
            ViewBag.Item = Record;
           

        }

        private void FillUpAddressDropDowns(List<Address> addreses)
        {
            if (addreses != null && addreses.Count > 0)
            {
                ViewData["ddlBilling"] = addreses;
                ViewData["ddlDelivery"] = addreses;

               
            }
        }

    }
}