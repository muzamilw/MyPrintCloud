﻿using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.WebStoreServices
{
    public interface IOrderService
    {
        int GetFirstItemIDByOrderId(int orderId);

        long ProcessPublicUserOrder(string orderTitle, long OrganisationId, int storeMode, long CompanyId, long ContactId, ref long TemporaryRetailCompanyId);

        long GetUserShopCartOrderID(int status);

        ShoppingCart GetShopCartOrderAndDetails(long orderID, OrderStatus Orderstatus);

        DiscountVoucher GetVoucherRecord(int VId);
        Estimate GetOrderByID(long orderId);
        bool UpdateOrderStatusAfterPrePayment(Estimate tblOrder, OrderStatus orderStatus, StoreMode mode);
        

        bool SetOrderCreationDateAndCode(long orderId);
        bool IsVoucherValid(string voucherCode);

        Estimate CheckDiscountApplied(int orderId);

        bool RollBackDiscountedItems(int orderId, double StateTax, StoreMode Mode);

        double SaveVoucherCodeAndRate(int orderId, string VCode);
        double PerformVoucherdiscountOnEachItem(int orderId, OrderStatus orderStatus, double StateTax, double VDiscountRate, StoreMode Mode);

        bool ResetOrderVoucherCode(int orderId);
         /// <summary>
        /// Get the OrderId by login User 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        long GetOrderIdByContactId(long contactId, long CompanyId);

        bool UpdateOrderWithDetails(long orderID, long loggedInContactID, double? orderTotal, int deliveryEstimatedCompletionTime, StoreMode isCorpFlow);

        bool IsOrderBelongToCorporate(long orderID, out long customerID);

        bool ValidateOrderForCorporateLogin(long orderID, bool isPlaceOrder, int IsCustomer, bool isWebAccess, out long CustomerID);
        /// <summary>
        /// Get order, items, addresses details by order id
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="BrokerID"></param>
        /// <returns></returns>
        OrderDetail GetOrderReceipt(long orderID);

        List<State> GetStates();

        List<Country> PopulateBillingCountryDropDown();

        Country GetCountryByID(long CountryID);

        void updateTaxInCloneItemForServic(long orderId, double TaxValue, StoreMode Mode);


        bool UpdateOrderWithDetailsToConfirmOrder(long orderID, long loggedInContactID, OrderStatus orderStatus, Address billingAdd, Address deliveryAdd, double grandOrderTotal,
                                             string yourReferenceNumber, string specialInsTel, string specialInsNotes, bool isCorpFlow, StoreMode CurrntStoreMde);

        double UpdateORderGrandTotal(long OrderID);

        bool SaveDilveryCostCenter(long orderId, CostCentre ChangedCostCenter);

       
        bool UpdateOrderAndCartStatus(long OrderID, OrderStatus orderStatus, StoreMode currentStoreMode);
        Estimate GetLastOrderByContactId(long ContactId);


        List<Order> GetOrdersListByContactID(long contactUserID, OrderStatus? orderStatus, string fromDate, string toDate, string orderRefNumber, int pageSize, int pageNumber);
        List<Order> GetOrdersListExceptPendingOrdersByContactID(long contactUserID, OrderStatus? orderStatus, string fromDate, string toDate, string orderRefNumber, int pageSize, int pageNumber);
        Order GetOrderAndDetails(long orderID);
        Address GetBillingAddress(long BillingAddressId);
        Address GetdeliveryAddress(long ShippingAddressId);
        long ReOrder(long ExistingOrderId, long loggedInContactID, double StatTaxVal, StoreMode mode, bool isIncludeTax, int TaxID);

    }
}
