using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.WebStoreServices
{
    public class OrderService : IOrderService
    {
        public readonly IOrderRepository _OrderRepository;
        private readonly IWebstoreClaimsHelperService _myClaimHelper;
        private readonly ICompanyService _myCompanyService;
        private readonly ICompanyContactRepository _myCompanyContact;
        private readonly IPrefixRepository _prefixRepository;
          #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public OrderService(IOrderRepository OrderRepository,IWebstoreClaimsHelperService myClaimHelper,ICompanyService myCompanyService,ICompanyContactRepository myCompanyContact,IPrefixRepository prefixRepository)
        {
            this._OrderRepository = OrderRepository;
            this._myClaimHelper = myClaimHelper;
            this._myCompanyService = myCompanyService;
            this._myCompanyContact = myCompanyContact;
            this._prefixRepository = prefixRepository;
        }


        #endregion

        public int GetFirstItemIDByOrderId(int orderId)
        {
            return _OrderRepository.GetFirstItemIDByOrderId(orderId);

        }

        // if user order cookie is null the we process the order
        public long ProcessPublicUserOrder(string orderTitle, long OrganisationId, int storeMode, long CompanyId)
        { // update but save this function changes 
            long dummyRetailCustomerId = 0;
            long orderID = 0;
            if (!IsUserLoggedIn())
            {
                if (!CheckCustomerCookie()) // need to update
                {
                    if (storeMode == 1) // retail
                    {
                        dummyRetailCustomerId = CreateCustomer();
                        long dummyContactId = _myCompanyContact.GetContactIdByCustomrID(dummyRetailCustomerId);
                        orderID = _OrderRepository.CreateNewOrder(dummyRetailCustomerId, dummyContactId, OrganisationId, orderTitle);
                    }
                    else  // corporate
                    {
                        // create dummy contact only in case of corporate
                        long dummyContactId = 0; //_myCompanyContact.GetContactIdByCustomrID(dummyRetailCustomerId);
                        orderID = _OrderRepository.CreateNewOrder(CompanyId, dummyContactId, OrganisationId, orderTitle);
                    }
                }
                else
                {
                   // user cookie is exists
                }
            }
            else
            {
                orderID = _OrderRepository.CreateNewOrder(_myClaimHelper.loginContactCompanyID(), _myClaimHelper.loginContactID(), OrganisationId, orderTitle);
                 
            }

            return orderID;
                 
        }

        public bool IsUserLoggedIn()
        {
            if (_myClaimHelper.loginContactID() > 0)
            {
                return true;
            }
            else
            {
                return false;

            }
        }
        public bool CheckCustomerCookie()
        {

            bool result = true;
            //HttpCookie customerCookie = null;


            //customerCookie = Request.Cookies[CUSTOMER_COOKIE];
            //if (customerCookie != null && !string.IsNullOrWhiteSpace(customerCookie.Value) && customerCookie.Value != "0")
            //    result = true;


            return result;
        }
        public int CreateCustomer()
        {
            int customerID = 0;
          
           
                customerID = _myCompanyService.CreateCustomer("Web Store Customer", true, false, ContactCompanyTypes.TemporaryCustomer, "");
                if (customerID > 0)
                    this.SetCustomerCookie(customerID); // sets the customer into the cookie
          

            return customerID;
        }
        public bool SetCustomerCookie(int customerID)
        {
            bool result = false;
            HttpCookie customerCookie = null;
     
                //customerCookie = new HttpCookie(CUSTOMER_COOKIE, customerID.ToString());
                //customerCookie.Expires = DateTime.Today.AddDays(365);
                //Response.Cookies.Add(customerCookie);
                //result = true;
           
          

            return result;
        }
        public long GetUserShopCartOrderID(int status)
        {
            return _OrderRepository.GetUserShopCartOrderID(status);
        }
        public ShoppingCart GetShopCartOrderAndDetails(long orderID, OrderStatus orderStatus)
        {
            return _OrderRepository.GetShopCartOrderAndDetails(orderID, orderStatus);
        }
        public DiscountVoucher GetVoucherRecord(int VId)
        {

            return _OrderRepository.GetVoucherRecord(VId); 
        }
        public Estimate GetOrderByID(long orderId)
        {
            try
            {
                return _OrderRepository.GetOrderByID(orderId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool IsVoucherValid(string voucherCode)
        {
            try
            {
                return _OrderRepository.IsVoucherValid(voucherCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Estimate CheckDiscountApplied(int orderId)
        {
            try
            {
                return _OrderRepository.CheckDiscountApplied(orderId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RollBackDiscountedItems(int orderId, double StateTax, StoreMode Mode)
        {
            try
            {
                return _OrderRepository.RollBackDiscountedItems(orderId,StateTax,Mode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public double SaveVoucherCodeAndRate(int orderId, string VCode)
        {
            try
            {
                return _OrderRepository.SaveVoucherCodeAndRate(orderId, VCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public double PerformVoucherdiscountOnEachItem(int orderId, OrderStatus orderStatus, double StateTax, double VDiscountRate,StoreMode Mode)
        {
            try
            {
                return _OrderRepository.PerformVoucherdiscountOnEachItem(orderId, orderStatus, StateTax, VDiscountRate, Mode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       public bool ResetOrderVoucherCode(int orderId)
       {
           try
           {
               return _OrderRepository.ResetOrderVoucherCode(orderId);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
