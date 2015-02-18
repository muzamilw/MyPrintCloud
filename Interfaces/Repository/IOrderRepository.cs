using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface IOrderRepository : IBaseRepository<Estimate,long>
    {
        int GetFirstItemIDByOrderId(int orderId);

        long CreateNewOrder(long CompanyId, long ContactId, long OrganisationId, string orderTitle = null);

        long GetOrderID(long CustomerId, long ContactId, string orderTitle, long OrganisationId);

        long GetUserShopCartOrderID(int status);

        ShoppingCart GetShopCartOrderAndDetails(long orderID, OrderStatus orderStatus);

        DiscountVoucher GetVoucherRecord(int VId);

        Estimate GetOrderByID(long orderId);
        bool IsVoucherValid(string voucherCode);

        Estimate CheckDiscountApplied(int orderId);

        bool RollBackDiscountedItems(int orderId, double StateTax, StoreMode Mode);

        double SaveVoucherCodeAndRate(int orderId, string VCode);
        double PerformVoucherdiscountOnEachItem(int orderId, OrderStatus orderStatus, double StateTax, double VDiscountRate, StoreMode Mode);

        bool ResetOrderVoucherCode(int orderId);
        /// <summary>
        /// returns the order id of a logged in user if order exist in cart
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        long GetCartOrderId(long contactId, long CompanyId);

        bool UpdateOrderWithDetails(long orderID, long loggedInContactID, double? orderTotal, int deliveryEstimatedCompletionTime, StoreMode isCorpFlow);

        bool IsOrderBelongToCorporate(long orderID, out long customerID);
        /// <summary>
        /// Get order, items, addresses details by order id
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="BrokerID"></param>
        /// <returns></returns>
        OrderDetail GetOrderReceipt(long orderID);

        void updateTaxInCloneItemForServic(long orderId, double TaxValue, StoreMode Mode);


        bool UpdateOrderAndCartStatus(long OrderID, OrderStatus orderStatus, StoreMode currentStoreMode);
        bool UpdateOrderWithDetailsToConfirmOrder(long orderID, long loggedInContactID, OrderStatus orderStatus, Address billingAdd, Address deliveryAdd, double grandOrderTotal,
                                             string yourReferenceNumber, string specialInsTel, string specialInsNotes, bool isCorpFlow, StoreMode CurrntStoreMde, Estimate order, Prefix prefix);

        double UpdateORderGrandTotal(long OrderID);

        bool SaveDilveryCostCenter(long orderId, CostCentre ChangedCostCenter);
        Estimate GetLastOrderByContactID(long contactID);

        List<Order> GetOrdersListByContactID(long contactUserID, OrderStatus? orderStatus,string fromDate, string  toDate, string orderRefNumber, int pageSize, int pageNumber) ;
        List<Order> GetOrdersListExceptPendingOrdersByContactID(long contactUserID, OrderStatus? orderStatus, string fromDate, string toDate, string orderRefNumber, int pageSize, int pageNumber);
    }
}
