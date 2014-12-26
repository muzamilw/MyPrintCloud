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

        public int ProcessPublicUserOrder(string orderTitle,Organisation org)
        {
            int customerID = 0;
            long orderID = 0;
            if (!IsUserLoggedIn())
            {
                if (!CheckCustomerCookie()) // need to update
                {
                    customerID = CreateCustomer();
                    int CID = _myCompanyContact.GetContactIdByCustomrID(customerID);
                    Company company = _myCompanyService.GetCompanyByCompanyID(customerID);
                    Prefix prefix = _prefixRepository.GetDefaultPrefix();
                    orderID = _OrderRepository.CreateNewOrder(customerID, CID, company, org, prefix, orderTitle);
                    //Here Ofcourse for new Customer There shall not be an order exists so we need to create one
                }
                else
                {
                    customerID = (int)_myClaimHelper.loginContactCompanyID(); //dummy customer
                    Company tblCustomer = _myCompanyService.GetCompanyByCompanyID((Int64)customerID);
                    if (tblCustomer == null)
                        customerID = this.CreateCustomer();
                     Prefix prefix = _prefixRepository.GetDefaultPrefix();
                    int CID = _myCompanyContact.GetContactIdByCustomrID(customerID);


                    // start from here

                    orderID = _OrderRepository.GetOrderID(customerID, CID, orderTitle, tblCustomer, org, prefix);
                }
            }
            else
            {
                //user is Loggged in
                //Then get customer
                 Company tblCustomer = _myCompanyService.GetCompanyByCompanyID((Int64)customerID);
                
                 Prefix prefix = _prefixRepository.GetDefaultPrefix();
                customerID = (int)_myClaimHelper.loginContactCompanyID();
                int contactID = (int)_myClaimHelper.loginContactID();
                // When user is logged in then we have the contact id why to get order by customer id.
                orderID = _OrderRepository.GetOrderID(customerID, contactID, orderTitle, tblCustomer,org,prefix);
              //  customerID = SessionParameters.CustomerID;
            }

            return (int)orderID;
                 
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


    }
}
