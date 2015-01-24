using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Item Repository 
    /// </summary>
    public interface IItemRepository : IBaseRepository<Item, long>
    {
        /// <summary>
        /// Get Items
        /// </summary>
        ItemSearchResponse GetItems(ItemSearchRequestModel request);

        List<GetCategoryProduct> GetRetailOrCorpPublishedProducts(int ProductCategoryID);

        ItemStockOption GetFirstStockOptByItemID(int ItemId, int CompanyId);

        List<ItemPriceMatrix> GetPriceMatrixByItemID(int ItemId);
        Item CloneItem(long itemID, long RefItemID, long OrderID, long CustomerID, long TemplateID, long StockID, List<AddOnCostsCenter> SelectedAddOnsList, bool isSavedDesign, bool isCopyProduct, long objContactID);

        Item GetItemById(long RefitemId);

        ProductItem GetItemAndDetailsByItemID(long itemId);

        List<ProductMarketBriefQuestion> GetMarketingInquiryQuestionsByItemID(int itemID);

        List<ProductMarketBriefAnswer> GetMarketingInquiryAnswersByQID(int QID);

        void CopyAttachments(int itemID, Item NewItem, string OrderCode, bool CopyTemplate, DateTime OrderCreationDate);

        bool RemoveCloneItem(long itemID, out List<ArtWorkAttatchment> itemAttatchmetList, out Template clonedTemplateToRemove);

        bool UpdateCloneItem(long clonedItemID, double orderedQuantity, double itemPrice, double addonsPrice, long stockItemID, List<AddOnCostsCenter> newlyAddedCostCenters, int Mode, long OrganisationId, double TaxRate, int CountOfUploads = 0);

        List<ProductItem> GetRelatedItemsList();

        Item GetItemByOrderAndItemID(long ItemID, long OrderID);

        double FindMinimumPriceOfProduct(long itemID);

        Item GetClonedItemByOrderId(long OrderId, long ReferenceItemId);

        List<ProductItem> GetRelatedItemsByItemID(long ItemID);

        List<ItemImage> getItemImagesByItemID(long ItemID);

        int GetDefaultSectionPriceFlag();

        double GetMinimumProductValue(long itemId);

        long UpdateTemporaryCustomerOrderWithRealCustomer(long TemporaryCustomerID, long realCustomerID, long realContactID, long replacedOrderdID, out List<ArtWorkAttatchment> orderAllItemsAttatchmentsListToBeRemoved, out List<Template> clonedTemplateToRemoveList);

        /// <summary>
        /// Get Items For Widgets 
        /// </summary>
        List<Item> GetItemsForWidgets();
         Item GetItemByOrderID(long OrderID);
          List<Item> GetItemsByOrderID(long OrderID);
         string SaveDesignAttachments(long templateID, long itemID, long customerID, string DesignName, string caller, long organisationId);
         Item GetClonedItemById(long itemId);
         long GetFirstItemIdByOrderId(long orderId);

         List<Item> GetListOfDeliveryItemByOrderID(long OID);

         bool RemoveListOfDeliveryItemCostCenter(long OrderId);

         bool AddUpdateItemFordeliveryCostCenter(long orderId, long DeliveryCostCenterId, double DeliveryCost, long customerID, string DeliveryName, StoreMode Mode, bool isDeliveryTaxable, bool IstaxONService, double GetServiceTAX, double TaxRate);
    }
}
