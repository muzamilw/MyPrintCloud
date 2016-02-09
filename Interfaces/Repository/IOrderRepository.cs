using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    public interface IOrderRepository : IBaseRepository<Estimate, long>
    {
        void UpdateOrderForDel(Estimate Order);
        double? GetOrderTotalById(long OrderId);
        long ApproveOrRejectOrder(long orderID, long loggedInContactID, OrderStatus orderStatus, Guid OrdermangerID, string BrokerPO = "");
        List<Order> GetPendingApprovelOrdersList(long contactUserID, bool isApprover);
        List<Order> GetAllCorpOrders(long ContactCompany, OrderStatus? orderStatus, string fromDate, string toDate, string orderRefNumber, bool IsManager, long TerritoryId);


        string FormatDateValue(DateTime? dateTimeValue);
        ShoppingCart ExtractShoppingCartForOrder(Estimate tblEstimate);
        bool ApplyCurrentTax(List<Item> ClonedITem, double TaxValue, int TaxID);
        string GetAttachmentFileName(string ProductCode, string OrderCode, string ItemCode, string SideCode, string VirtualFolderPath, string extension, DateTime OrderCreationDate);
        string GetTemplateAttachmentFileName(string ProductCode, string OrderCode, string ItemCode, string SideCode, string VirtualFolderPath, string extension, DateTime CreationDate);
        bool RollBackDiscountedItemsWithdbContext(List<Item> clonedItems, double StateTax);
        int GetFirstItemIDByOrderId(long orderId);

        long CreateNewOrder(long CompanyId, long ContactId, long OrganisationId, string orderTitle = null);

        long GetOrderID(long CustomerId, long ContactId, string orderTitle, long OrganisationId);

        long GetUserShopCartOrderID(int status);

        ShoppingCart GetShopCartOrderAndDetails(long orderID, OrderStatus orderStatus);

        DiscountVoucher GetVoucherRecord(int VId);

        Estimate GetOrderByID(long orderId);
        bool SetOrderCreationDateAndCode(long orderId, long OrganisationId);
        //bool IsVoucherValid(string voucherCode);
        bool UpdateOrderStatusAfterPrePayment(Estimate tblOrder, OrderStatus orderStatus, StoreMode mode);
        void updateStockAndSendNotification(long ItemId, long StockID, StoreMode Mode, long companyId, int orderedQty, long contactId, long orderedItemid, long OrderId, List<Guid> MgrIds, Organisation org);
        //Estimate CheckDiscountApplied(int orderId);

        //bool RollBackDiscountedItems(int orderId, double StateTax, StoreMode Mode);

        //double SaveVoucherCodeAndRate(int orderId, string VCode);
        //double PerformVoucherdiscountOnEachItem(int orderId, OrderStatus orderStatus, double StateTax, double VDiscountRate, StoreMode Mode);

        //bool ResetOrderVoucherCode(int orderId);
        /// <summary>
        /// returns the order id of a logged in user if order exist in cart
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        long GetCartOrderId(long contactId, long CompanyId);

        bool UpdateOrderWithDetails(long orderID, long loggedInContactID, double? orderTotal, int deliveryEstimatedCompletionTime, StoreMode isCorpFlow, CompanyContact Contact);

        bool IsOrderBelongToCorporate(long orderID, out long customerID);
        /// <summary>
        /// Get order, items, addresses details by order id
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="BrokerID"></param>
        /// <returns></returns>
        OrderDetail GetOrderReceipt(long orderID);

        void updateTaxInCloneItemForServic(long orderId, double TaxValue, StoreMode Mode);
        void DeleteCart(long CompanyID);
        void DeleteOrderBySP(long OrderID);
        bool UpdateOrderAndCartStatus(long OrderID, OrderStatus orderStatus, StoreMode currentStoreMode, Organisation Org, List<Guid> ManagerIds, long StoreId);
        bool UpdateOrderWithDetailsToConfirmOrder(long orderID, long loggedInContactID, OrderStatus orderStatus, Address billingAdd, Address deliveryAdd, double grandOrderTotal,
                                             string yourReferenceNumber, string specialInsTel, string specialInsNotes, bool isCorpFlow, StoreMode CurrntStoreMde, Estimate order, Prefix prefix);

        double UpdateORderGrandTotal(long OrderID);

        bool SaveDilveryCostCenter(long orderId, CostCentre ChangedCostCenter);
        Estimate GetLastOrderByContactID(long contactID);

        List<Order> GetOrdersListByContactID(long contactUserID, OrderStatus? orderStatus, string fromDate, string toDate, string orderRefNumber, int pageSize, int pageNumber);
        List<Order> GetOrdersListExceptPendingOrdersByContactID(long contactUserID, OrderStatus? orderStatus, string fromDate, string toDate, string orderRefNumber, int pageSize, int pageNumber);

        Order GetOrderAndDetails(long orderID);
        Address GetAddress(long AddressId);
        void GenerateThumbnailForPdf(byte[] PDFFile, string sideThumbnailPath, bool insertCuttingMargin);
        bool CreatAndSaveThumnail(Stream oImgstream, string sideThumbnailPath);
        void CopyAttachments(long itemID, Item NewItem, string OrderCode, bool CopyTemplate, DateTime OrderCreationDate);
        long ReOrder(long ExistingOrderId, long loggedInContactID, double StatTaxVal, StoreMode mode, bool isIncludeTax, int TaxID, long OrganisationId);
        Estimate GetShoppingCartOrderByContactID(long contactID, OrderStatus orderStatus);

        /// <summary>
        /// gets cart order by company id
        /// </summary>
        /// <param name="ContactId"></param>
        /// <param name="TemporaryCustomerId"></param>
        /// <returns></returns>
        long GetOrderIdByCompanyId(long CompanyId, OrderStatus orderStatus);

        Estimate GetOrderByOrderID(long OrderID);

        // List<Item> GetItemsByOrderID(long orderID);

        List<Estimate> GetCartOrdersByCompanyID(long CompanyID);
        void DeleteOrder(long orderId);

        //  string GenerateOrderArtworkArchive(int OrderID, string sZipName);

        /// <summary>
        /// Get Estimates For Item Job Status
        /// </summary>
        IEnumerable<Estimate> GetEstimatesForItemJobStatus();
        /// <summary>
        /// check cookie order is the real login customer order
        /// </summary>
        bool IsRealCustomerOrder(long orderId, long contactId, long companyId);

        List<Item> GetOrderItems(long OrderId);
        bool CreateUploadYourArtWork(long itemID, long customerID, List<ArtWorkAttatchment> yourDesignList);

        Estimate GetOrderByOrderCode(string Code);

        Estimate GetOrderByIdforXml(long RecordID);

       // void regeneratePDFs(long productID, long OrganisationID, bool printCuttingMargins, bool isMultipageProduct, bool drawBleedArea, double bleedAreaSize);

        /// <summary>
        /// Search Estimates For Live Jobs
        /// </summary>
        LiveJobsSearchResponse GetEstimatesForLiveJobs(LiveJobsRequestModel request);
        
          /// <summary>
        /// This function return the delivery item also
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        List<Item> GetAllOrderItems(long OrderId);
        long GetStoreIdByOrderId(long OrderId);

        bool UpdateItemAttachmentPath(List<Item> items);
        List<Item> GetOrderItemsIncludingDelivery(long OrderId, int OrderStatus);
        List<long> GetOrdersForBillingCycle(DateTime billingDate, bool isDirectOrder);
        bool IsExtradOrderForBillingCycle(DateTime billingDate, bool isDirectOrder, int licensedCount, long orderId, long organisationId);

    }
}


   
