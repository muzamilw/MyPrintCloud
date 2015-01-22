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
        ShopCartAddressSelectViewModel AddressSelectModel = new ShopCartAddressSelectViewModel();
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
                MyCompanyDomainBaseResponse baseresponseCurr = _myCompanyService.GetStoreFromCache(UserCookieManager.StoreId).CreateFromCurrency();

                if (!string.IsNullOrEmpty(baseresponseCurr.Currency))
                    AddressSelectModel.Currency = baseresponseCurr.Currency;
                else
                    AddressSelectModel.Currency = string.Empty;


                OrganisationID = baseresponseOrg.Organisation.OrganisationId;

                deliveryCostCentersList = GetDeliveryCostCenterList();

                shopCart = LoadShoppingCart(OrderID);

                AddressSelectModel.shopcart = shopCart;

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

                AddressSelectModel.LtrMessageToDisplay = false;
                //Addresses panel
                if (shopCart != null)
                {
                    BindDeliveryCostCenterDropDown(deliveryCostCentersList);

                    ShowONDropDownExistingDeliveryName(OrderID);

                    //chkBoxDeliverySameAsBilling.Checked = true;

                    //string Mobile = _myCompanyService.GetContactMobile(_myClaimHelper.loginContactID()); //SessionParameters.CustomerContact.Mobile;
                    CompanyContact contact = _myCompanyService.GetContactByID(_myClaimHelper.loginContactID());
                    
                    AddressSelectModel.ContactTel = contact.Mobile;
                   
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
                        //ViewBag.listitem = new SelectList(customerAddresses.ToList(), "AddressId", "AddressName");
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
                               // ViewData["BillingAddress"] = billingAddress;
                                AddressSelectModel.BillingAddress = billingAddress;
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
                                if (shippingAddress != null)
                                {
                                    ViewData["ShippingAddress"] = shippingAddress;
                                    AddressSelectModel.ShippingAddress = shippingAddress;
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
                                AddressSelectModel.BillingAddress = billingAddress;
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
                                    AddressSelectModel.ShippingAddress = shippingAddress;
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
                  AddressSelectModel.TaxLabel = baseresponseComp.Company.TaxLabel +  " :"; 
                }
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
                AddressSelectModel.SelectedItemsAddonsList = shopCart.ItemsSelectedAddonsList;
                //ViewData["selectedItemsAddonsList"] = _selectedItemsAddonsList;
                //global values for all items
                CostCentre deliveryCostCenter = null;
                int deliverCostCenterID;
                _deliveryCost = shopCart.DeliveryCost;
              //  ViewBag.DeliveryCost = shopCart.DeliveryCost;
                AddressSelectModel.DeliveryCost = shopCart.DeliveryCost;
                _deliveryCostTaxVal = shopCart.DeliveryTaxValue;
              //  ViewBag.DeliveryCostTaxVal = _deliveryCostTaxVal;
                AddressSelectModel.DeliveryCostTaxVal = shopCart.DeliveryTaxValue;
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
                AddressSelectModel.shopcart.CartItemsList = itemsList;
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
           
            AddressSelectModel.ProductItems = itemsList;
        }

        private void BindCountriesDropDownData(MyCompanyDomainBaseResponse basresponseOrg, MyCompanyDomainBaseResponse basresponseCom)
        {
            List<Country> country = _IOrderService.PopulateBillingCountryDropDown();
            PopulateBillingCountryDropDown(country);
            PopulateShipperCountryDropDown(country);
            PopulateStateDropDown();

            if (UserCookieManager.StoreMode == (int)StoreMode.Retail)
            {


                AddressSelectModel.PickUpAddress = basresponseOrg.Organisation.Address1 + " " + basresponseOrg.Organisation.Address2 + " " + basresponseOrg.Organisation.City + "," + basresponseOrg.Organisation.Country + "," + basresponseOrg.Organisation.State + " " + basresponseOrg.Organisation.ZipCode;

            }
            else
            {// corporate
                Address pickupAddress = _myCompanyService.GetAddressByID(basresponseCom.Company.PickupAddressId ?? 0);
                if (pickupAddress != null)
                {
                    AddressSelectModel.PickUpAddress = pickupAddress.Address1 + " " + pickupAddress.Address2 + " " + pickupAddress.City + "," + pickupAddress.Country + "," + pickupAddress.State + " " + pickupAddress.PostCode;


                }
            }

         
        }
        private void PopulateBillingCountryDropDown( List<Country> Countries)
        {
           
            AddressSelectModel.Countries = Countries;
          // select a country 
        }
        private void PopulateShipperCountryDropDown(List<Country> Countries)
        {
           
            AddressSelectModel.Countries = Countries;
        }

        private void PopulateStateDropDown()
        {

            List<State> states = _IOrderService.GetStates();
            AddressSelectModel.States = states;
           
          
          
        }
        private void BindDeliveryCostCenterDropDown(List<CostCentre> costCenterList)
        {
            if (costCenterList != null && costCenterList.Count > 0)
            {
                AddressSelectModel.DeliveryCostCenters = costCenterList;
           

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
                
                AddressSelectModel.ShippingAddresses = addreses;
              //  ViewData["ShipAddresses"] = addreses;
                AddressSelectModel.BillingAddresses = addreses;
              
                ViewBag.listitemShipping = new SelectList(addreses, "AddressId", "AddressName");

                ViewBag.listitemBilling = new SelectList(addreses, "AddressId", "AddressName");
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


                OrganisationID = baseresponseOrg.Organisation.OrganisationId;
                string addLine1 = Request.Form["txtAddressLine1"];
                string city = Request.Form["txtCity"];
                string PostCode = Request.Form["txtZipPostCode"];
                ConfirmOrder(1,addLine1,city,PostCode,baseresponseComp,model);
                return View();
            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), OrganisationID);
            }
           
        }

        private void ConfirmOrder(int modOverride, string AddLine1, string city, string PostCode, MyCompanyDomainBaseResponse baseresponseComp, ShopCartAddressSelectViewModel model)
        {

            bool isPageValid = true;
            //if (hfPageIsValid.Value == "0")
            //{
            //    isPageValid = false;
            //}
            if (isPageValid)
            {


                Double ServiceTaxRate = GetTAXRateFromService(AddLine1, city, PostCode, model);

                Session["ServiceTaxRate"] = ServiceTaxRate;

                bool result = false;

                string voucherCode = string.Empty;
                double grandOrderTotal = 0;

                string yourRefNumber = null;
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
                        yourRefNumber = Request.Form["txtYourRefNumber"];
                       
                    }
                    else
                    {
                        yourRefNumber = Request.Form["txtYourRefNumberRetail"];
                    }
                    yourRefNumber = yourRefNumber.Trim();
                    specialTelNumber = Request.Form["txtInstContactTelNumber"];
                    notes = Request.Form["txtInstNotes"];
                    notes = notes.Trim();
                    string total =  Request.Form["hfGrandTotal"];
                    grandOrderTotal = Convert.ToDouble(total);

                 //   PrepareAddrssesToSave(out billingAdd, out deliveryAdd,baseresponseComp,model);

                    //if (true)
                    //{

                    //    try
                    //    {
                    //        if (UpdateDeliveryCostCenterInOrder())
                    //        {
                    //            if (UserCookieManager.StoreMode == (int)StoreMode.Retail)
                    //            {
                    //                if (SessionParameters.tbl_cmsDefaultSettings.isCalculateTaxByService == true)
                    //                {

                    //                    double TaxRate = GetTAXRateFromService(AddLine1, city, PostCode);

                    //                    //double TaxRate = 0.04;
                    //                    oMgr.updateTaxInCloneItemForServic(UserCookieManager.OrderId, TaxRate, SessionParameters.StoreMode);

                    //                }

                    //                result = OrderManager.UpdateOrderWithDetailsToConfirmOrder(PageParameters.OrderID, SessionParameters.CustomerContact.ContactID, OrderManager.OrderStatus.ShoppingCart, billingAdd, deliveryAdd, UpdateORderGrandTotal(PageParameters.OrderID), yourRefNumber, specialTelNumber, notes, true, SessionParameters.StoreMode, 0);
                    //            }
                    //            else if (SessionParameters.StoreMode == StoreMode.Broker)
                    //            {
                    //                if (SessionParameters.BrokerContactCompany.isCalculateTaxByService == true)
                    //                {
                    //                    try
                    //                    {
                    //                        double TaxRate = GetTAXRateFromService(txtAddressLine1.Value, txtCity.Value, txtZipPostCode.Value);

                    //                        //double TaxRate = 0.04;

                    //                        oMgr.updateTaxInCloneItemForServic(PageParameters.OrderID, TaxRate, SessionParameters.StoreMode);
                    //                    }
                    //                    catch (Exception ex)
                    //                    {
                    //                        MessgeToDisply.Visible = true;
                    //                        MessgeToDisply.Style.Add("border", "1px solid red");
                    //                        MessgeToDisply.Style.Add("font-size", "20px");
                    //                        MessgeToDisply.Style.Add("font-weight", "bold");
                    //                        MessgeToDisply.Style.Add("text-align", "left");
                    //                        MessgeToDisply.Style.Add("color", "red");
                    //                        MessgeToDisply.Style.Add("padding", "20px");

                    //                        ltrlMessge.Text = "TAX Service Error";

                    //                    }
                    //                }
                    //                result = OrderManager.UpdateOrderWithDetailsToConfirmOrder(PageParameters.OrderID, SessionParameters.CustomerContact.ContactID, OrderManager.OrderStatus.ShoppingCart, billingAdd, deliveryAdd, UpdateORderGrandTotal(PageParameters.OrderID), yourRefNumber, specialTelNumber, notes, true, SessionParameters.StoreMode, SessionParameters.BrokerContactCompany.ContactCompanyID);
                    //            }
                    //            else
                    //            {
                    //                if (SessionParameters.ContactCompany.isCalculateTaxByService == true)
                    //                {
                    //                    try
                    //                    {
                    //                        double TaxRate = GetTAXRateFromService(txtAddressLine1.Value, txtCity.Value, txtZipPostCode.Value);

                    //                        // double TaxRate = 0.04;
                    //                        oMgr.updateTaxInCloneItemForServic(PageParameters.OrderID, TaxRate, SessionParameters.StoreMode);
                    //                    }
                    //                    catch (Exception ex)
                    //                    {
                    //                        MessgeToDisply.Visible = true;
                    //                        MessgeToDisply.Style.Add("border", "1px solid red");
                    //                        MessgeToDisply.Style.Add("font-size", "20px");
                    //                        MessgeToDisply.Style.Add("font-weight", "bold");
                    //                        MessgeToDisply.Style.Add("text-align", "left");
                    //                        MessgeToDisply.Style.Add("color", "red");
                    //                        MessgeToDisply.Style.Add("padding", "20px");

                    //                        ltrlMessge.Text = "TAX Service Error";

                    //                    }
                    //                }
                    //                result = OrderManager.UpdateOrderWithDetailsToConfirmOrder(PageParameters.OrderID, SessionParameters.CustomerContact.ContactID, OrderManager.OrderStatus.ShoppingCart, billingAdd, deliveryAdd, UpdateORderGrandTotal(PageParameters.OrderID), yourRefNumber, specialTelNumber, notes, true, SessionParameters.StoreMode, 0);
                    //            }
                    //            if (result)
                    //            {

                    //                Response.Redirect("OrderConfirmation.aspx?OrderId=" + PageParameters.OrderID.ToString(), false);
                    //            }
                    //            else
                    //            {
                    //                MessgeToDisply.Visible = true;
                    //                MessgeToDisply.Style.Add("border", "1px solid red");
                    //                MessgeToDisply.Style.Add("font-size", "20px");
                    //                MessgeToDisply.Style.Add("font-weight", "bold");
                    //                MessgeToDisply.Style.Add("text-align", "left");
                    //                MessgeToDisply.Style.Add("color", "red");
                    //                MessgeToDisply.Style.Add("padding", "20px");
                    //                ltrlMessge.Text = "Error occurred while updating order.";

                    //            }
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        MessgeToDisply.Visible = true;
                    //        MessgeToDisply.Style.Add("border", "1px solid red");
                    //        MessgeToDisply.Style.Add("font-size", "20px");
                    //        MessgeToDisply.Style.Add("font-weight", "bold");
                    //        MessgeToDisply.Style.Add("text-align", "left");
                    //        MessgeToDisply.Style.Add("color", "red");
                    //        MessgeToDisply.Style.Add("padding", "20px");

                    //        ltrlMessge.Text = "Error occurred while updating order in catch block.";
                    //        throw ex;
                    //        LogError(ex);


                    //    }

                    //}

                    
                    }

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

        //protected void PrepareAddrssesToSave(out Address billingAdd, out Address deliveryAdd,MyCompanyDomainBaseResponse baseResponse,ShopCartAddressSelectViewModel model)
        //{
        //    billingAdd = null;

        //    deliveryAdd = new Address();

        //    deliveryAdd.AddressName = Request.Form["txtShipAddressName"]; 
        //    deliveryAdd.Address1 = Request.Form["txtShipAddLine1"]; 
        //    deliveryAdd.Address2 = Request.Form["txtShipAddressLine2"]; 
        //    deliveryAdd.City = Request.Form["txtShipAddCity"]; 
        //  //  deliveryAdd.State = Request.Form["txtShipAddLine1"]; ddShippingState.SelectedItem.Text.ToString();
        //    deliveryAdd.PostCode = Request.Form["txtShipPostCode"]; 
        //    deliveryAdd.Tel1 = Request.Form["txtShipContact"]; 
        //    long TerritoryID =  _myCompanyService.GetContactTerritoryID(_myClaimHelper.loginContactID());
        //  //  deliveryAdd.Country = Request.Form["txtShipAddLine1"]; ddShippingCountry.SelectedItem.Text.ToString();

        //    //if (ddShippingState.SelectedValue != "-1")
        //    //{

        //    //    deliveryAdd.StateId = Convert.ToInt32(ddShippingState.SelectedValue);
        //    //}
        //    //else
        //    //{
        //    //    deliveryAdd.StateId = 0;

        //    //}
        //    //if (ddShippingCountry.SelectedValue != "-1")
        //    //{

        //    //    deliveryAdd.CountryId = Convert.ToInt32(ddShippingCountry.SelectedValue);
        //    //}
        //    //else
        //    //{
        //    //    deliveryAdd.CountryId = 0;
        //    //}

        //    if (model.SelectedDeliveryAddress == 0) // New Delivery address
        //    {
        //        deliveryAdd.AddressId = 0; //Convert.ToInt32(txthdnDeliveryAddressID.Value);
        //        deliveryAdd.IsDefaultShippingAddress = false; //Convert.ToBoolean(txthdnDeliveryDefaultShippingAddress.Value);
        //        deliveryAdd.IsDefaultAddress = false; //Convert.ToBoolean(txthdnDeliveryDefaultAddress.Value);


        //        if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
        //        {
        //            if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
        //            {
        //                if (baseResponse.Company.isStoreModePrivate == true)
        //                    deliveryAdd.isPrivate = true;
        //                else
        //                    deliveryAdd.isPrivate = false;
        //                deliveryAdd.ContactId = _myClaimHelper.loginContactID();

        //            }
        //            else
        //            {
        //                deliveryAdd.isPrivate = false;
                      
        //                // if (SessionParameters.CustomerContact.ContactRoleID == (int)Roles.Manager)
        //                deliveryAdd.TerritoryId = TerritoryID;
        //            }

        //        }
        //    }
        //    else
        //    {
        //        deliveryAdd.AddressId = Convert.ToInt32(model.SelectedDeliveryAddress);  //Convert.ToInt32(txthdnDeliveryAddressID.Value);
               
        //        deliveryAdd.IsDefaultShippingAddress = model.ShippingAddress.IsDefaultShippingAddress;

        //        deliveryAdd.IsDefaultAddress = model.ShippingAddress.IsDefaultShippingAddress;
                
        //    }

        //    string hfchkBoxDeliverySameAsBilling = Request.Form["hfchkBoxDeliverySameAsBilling"];
        //    if (hfchkBoxDeliverySameAsBilling == "False")
        //    {
        //        //billing delivery

        //        billingAdd = new Address();

        //        billingAdd.AddressName = Request.Form["txtBillingName"];
        //        billingAdd.Address1 = Request.Form["txtAddressLine1"]; 
        //        billingAdd.Address2 = Request.Form["txtAddressLine2"]; 
        //        billingAdd.City = Request.Form["txtCity"];
        //        // billingAdd.State = txtState.Value;
        //        billingAdd.PostCode = Request.Form["txtZipPostCode"]; 
        //        billingAdd.Tel1 = Request.Form["txtContactNumber"];

        //        billingAdd.CountryId = Convert.ToInt32(BillingCountryDropDown.SelectedValue);
        //        billingAdd.Country = BillingCountryDropDown.SelectedItem.Text.ToString();
        //        if (ddBillingState.SelectedValue != string.Empty)
        //        {
        //            billingAdd.StateId = Convert.ToInt32(ddBillingState.SelectedValue);
        //        }

        //        billingAdd.State = ddBillingState.SelectedItem.Text.ToString();

        //        if (ddlBilling.SelectedValue == "0") // New Billing address
        //        {
        //            billingAdd.AddressId = 0;
        //            billingAdd.IsDefaultShippingAddress = false;
        //            billingAdd.IsDefaultAddress = false;

        //            if (UserCookieManager.StoreMode == (int)StoreMode.Corp)
        //            {
        //                if (_myClaimHelper.loginContactRoleID() == (int)Roles.User)
        //                {
        //                    if (baseResponse.Company.isStoreModePrivate == true)
        //                        billingAdd.isPrivate = true;
        //                    else
        //                        billingAdd.isPrivate = false;
        //                    billingAdd.ContactId = _myClaimHelper.loginContactID();

        //                }
        //                else
        //                {
        //                    billingAdd.isPrivate = false;
        //                    // if (SessionParameters.CustomerContact.ContactRoleID == (int)Roles.Manager)
        //                    billingAdd.TerritoryId = TerritoryID;

        //                }
        //            }
        //        }
        //        else
        //        {
        //            billingAdd.AddressId = Convert.ToInt32(ddlBilling.SelectedValue); //Convert.ToInt32(txthdnBillingAddressID.Value);

        //            billingAdd.IsDefaultShippingAddress = model.BillingAddress.IsDefaultShippingAddress;

        //            billingAdd.IsDefaultAddress = model.BillingAddress.IsDefaultShippingAddress;
        //        }
        //    }

        //}

        //private bool UpdateDeliveryCostCenterInOrder()
        //{
        //    double Baseamount = 0;
        //    double SurchargeAmount = 0;
        //    double Taxamount = 0;
        //    double CostOfDelivery = 0;
        //    bool serviceResult = true;

        //    OrderManager OderMgr = new OrderManager();
        //    ProductManager ProMgr = new ProductManager();



        //    Model.CostCenter SelecteddeliveryCostCenter = null;

        //    if (ddlDeliveyCostCenter.SelectedValue != "-1")
        //    {
        //        SelecteddeliveryCostCenter = GetDeliveryCostCenterByID(Convert.ToInt32(ddlDeliveyCostCenter.SelectedValue));

        //        if (SelecteddeliveryCostCenter.CostCenterID > 0)
        //        {
        //            if (SessionParameters.StoreMode == StoreMode.Broker)
        //            {
        //                if (ddShippingCountry.SelectedValue == "-1" || ddShippingState.SelectedValue == "-1")
        //                {
        //                    MessgeToDisply.Visible = true;
        //                    MessgeToDisply.Style.Add("border", "1px solid red");
        //                    MessgeToDisply.Style.Add("font-size", "20px");
        //                    MessgeToDisply.Style.Add("font-weight", "bold");
        //                    MessgeToDisply.Style.Add("text-align", "left");
        //                    MessgeToDisply.Style.Add("color", "red");
        //                    MessgeToDisply.Style.Add("padding", "20px");
        //                    ltrlMessge.Text = "Please select country or state to countinue.";

        //                    serviceResult = false;
        //                }
        //                else
        //                {
        //                    if (!string.IsNullOrEmpty(txtShipPostCode.Value) && (Convert.ToInt32(SelecteddeliveryCostCenter.DeliveryType) == Convert.ToInt32(DeliveryCarriers.Fedex)))
        //                    {
        //                        serviceResult = GetFedexResponse(out Baseamount, out SurchargeAmount, out Taxamount, out CostOfDelivery);

        //                    }
        //                }
        //            }
        //            else if (SessionParameters.StoreMode == StoreMode.Corp)
        //            {
        //                if (ddShippingCountry.SelectedValue == "-1" || ddShippingState.SelectedValue == "-1")
        //                {
        //                    MessgeToDisply.Visible = true;
        //                    MessgeToDisply.Style.Add("border", "1px solid red");
        //                    MessgeToDisply.Style.Add("font-size", "20px");
        //                    MessgeToDisply.Style.Add("font-weight", "bold");
        //                    MessgeToDisply.Style.Add("text-align", "left");
        //                    MessgeToDisply.Style.Add("color", "red");
        //                    MessgeToDisply.Style.Add("padding", "20px");
        //                    ltrlMessge.Text = "Please select country or state to countinue.";

        //                    serviceResult = false;
        //                }

        //                else
        //                {
        //                    if (!string.IsNullOrEmpty(txtShipPostCode.Value) && Convert.ToInt32(SelecteddeliveryCostCenter.DeliveryType) == Convert.ToInt32(DeliveryCarriers.Fedex))
        //                    {

        //                        serviceResult = GetFedexResponse(out Baseamount, out SurchargeAmount, out Taxamount, out CostOfDelivery);

        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (!string.IsNullOrEmpty(txtShipPostCode.Value) && Convert.ToInt32(SelecteddeliveryCostCenter.DeliveryType) == Convert.ToInt32(DeliveryCarriers.Fedex))
        //                {


        //                    if (ddShippingCountry.SelectedValue == "-1" || ddShippingState.SelectedValue == "-1")
        //                    {
        //                        MessgeToDisply.Visible = true;
        //                        MessgeToDisply.Style.Add("border", "1px solid red");
        //                        MessgeToDisply.Style.Add("font-size", "20px");
        //                        MessgeToDisply.Style.Add("font-weight", "bold");
        //                        MessgeToDisply.Style.Add("text-align", "left");
        //                        MessgeToDisply.Style.Add("color", "red");
        //                        MessgeToDisply.Style.Add("padding", "20px");
        //                        ltrlMessge.Text = "Please select country or state to countinue.";

        //                        serviceResult = false;
        //                    }
        //                    else
        //                    {
        //                        if (!string.IsNullOrEmpty(txtShipPostCode.Value) && Convert.ToInt32(SelecteddeliveryCostCenter.DeliveryType) == Convert.ToInt32(DeliveryCarriers.Fedex))
        //                        {
        //                            serviceResult = GetFedexResponse(out Baseamount, out SurchargeAmount, out Taxamount, out CostOfDelivery);
        //                        }
        //                    }

        //                }
        //            }

        //            if (serviceResult)
        //            {
        //                if (CostOfDelivery == 0)
        //                {
        //                    CostOfDelivery = Convert.ToDouble(SelecteddeliveryCostCenter.SetupCost);
        //                }

        //                List<tbl_items> DeliveryItemList = ProMgr.GetListOfDeliveryItemByOrderID(Convert.ToInt32(PageParameters.OrderID));


        //                if (DeliveryItemList.Count > 1)
        //                {
        //                    if (ProMgr.RemoveListOfDeliveryItemCostCenter(Convert.ToInt32(PageParameters.OrderID)))
        //                    {
        //                        AddNewDeliveryCostCentreToItem(SelecteddeliveryCostCenter, CostOfDelivery);
        //                    }
        //                }
        //                else
        //                {
        //                    AddNewDeliveryCostCentreToItem(SelecteddeliveryCostCenter, CostOfDelivery);
        //                }
        //            }

        //        }
        //    }
        //    return serviceResult;
        //}
        #endregion


    }
}