using System.Linq.Expressions;
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
        /// Get Items With Details
        /// </summary>
        List<ItemPriceMatrix> GetRetailProductsPriceMatrix(long CompanyID);
        List<ProductItem> GetAllRetailDisplayProductsQuickCalc(long CompanyID);
        Item GetItemWithDetails(long itemId);

        /// <summary>
        /// Eager load property
        /// </summary>
        void LoadProperty<T>(object entity, Expression<Func<T>> propertyExpression, bool isCollection = false);

        /// <summary>
        /// Check if Product Code is Duplicate
        /// </summary>
        bool IsDuplicateProductCode(string productCode, long? itemId, long? companyId);

        /// <summary>
        /// Get Items
        /// </summary>
        /// 
        ItemSearchResponse GetItems(ItemSearchRequestModel request);
        double GrossTotalCalculation(double netTotal, double stateTaxValue);
        double CalculatePercentage(double itemValue, double percentageValue);
        List<GetCategoryProduct> GetRetailOrCorpPublishedProducts(long ProductCategoryID);

        ItemStockOption GetFirstStockOptByItemID(long ItemId, long CompanyId);

        List<ItemPriceMatrix> GetPriceMatrixByItemID(int ItemId);
        Item CloneItem(long itemID, long RefItemID, long OrderID, long CustomerID, long TemplateID, long StockID, List<AddOnCostsCenter> SelectedAddOnsList, bool isSavedDesign, bool isCopyProduct, long objContactID, long OrganisationID);

        Item GetItemById(long RefitemId);
        Item GetItemByIdDesigner(long ItemId);
        Item GetItemByTemplateIdDesigner(long templateId);
        ProductItem GetItemAndDetailsByItemID(long itemId);

        List<ProductMarketBriefQuestion> GetMarketingInquiryQuestionsByItemID(int itemID);

        List<ProductMarketBriefAnswer> GetMarketingInquiryAnswersByQID(int QID);

        void AddAttachment(ItemAttachment AttachmentObject);

        bool RemoveCloneItem(long itemID, out List<ArtWorkAttatchment> itemAttatchmetList, out Template clonedTemplateToRemove);

        bool UpdateCloneItem(long clonedItemID, double orderedQuantity, double itemPrice, double addonsPrice, long stockItemID, List<AddOnCostsCenter> newlyAddedCostCenters, int Mode, long OrganisationId, double TaxRate, string ItemMode, bool isInculdeTax, int CountOfUploads = 0, string QuestionQueue = "", string CostCentreQueue = "", string InputQueue = "");

        List<ProductItem> GetRelatedItemsList();

        Item GetItemByOrderAndItemID(long ItemID, long OrderID);

        double FindMinimumPriceOfProduct(long itemID);

        Item GetClonedItemByOrderId(long OrderId, long ReferenceItemId);

        List<ProductItem> GetRelatedItemsByItemID(long ItemID);

        List<ItemImage> getItemImagesByItemID(long ItemID);

        int GetDefaultSectionPriceFlag();

        double GetMinimumProductValue(long itemId);

        long UpdateTemporaryCustomerOrderWithRealCustomer(long TemporaryCustomerID, long realCustomerID, long realContactID, long replacedOrderdID,long OrganisationId ,out List<ArtWorkAttatchment> orderAllItemsAttatchmentsListToBeRemoved, out List<Template> clonedTemplateToRemoveList);

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

        List<usp_GetRealEstateProducts_Result> GetRealEstateProductsByCompanyID(long CompanyId);
         bool RemoveListOfDeliveryItemCostCenter(long OrderId);

         bool AddUpdateItemFordeliveryCostCenter(long orderId, long DeliveryCostCenterId, double DeliveryCost, long customerID, string DeliveryName, StoreMode Mode, bool isDeliveryTaxable, bool IstaxONService, double GetServiceTAX, double TaxRate);
       
         Item GetItemByOrderItemID(long ItemID, long OrderID);

         void VariablesResolve(long ItemID, long ProductId, long objContactID);
         /// <summary>
         /// checks if the order is dummy, this function return true if the order is dummy and needs to replaced with real order else false
         /// </summary>
         /// <param name="orderId"></param>
        /// <param name="customerId"></param>
        /// <param name="contactId"></param>
        /// <returns></returns>
 
        bool isTemporaryOrder(long orderId, long customerId, long contactId);
        /// <summary>
        /// get item section
        /// </summary>
        /// <param name="ItemId"></param>
        /// <returns></returns>
        ItemSection GetItemFirstSectionByItemId(long ItemId);
        /// <summary>
        /// update quantity in item's first section
        /// </summary>
        /// <param name="ItemSectionId"></param>
        /// <param name="Quantity"></param>
        /// <returns></returns>
        ItemSection UpdateItemFirstSectionByItemId(long ItemId, int Quantity);
        Item CloneReOrderItem(long orderID, long ExistingItemId, long loggedInContactID, string order_code, long OrganisationId);


        /// <summary>
        /// Get Items By Company Id
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        IEnumerable<Item> GetItemsByCompanyId(ItemSearchRequestModel requestModel);
        /// <summary>
        /// get cart items count 
        /// </summary>
        /// <returns></returns>
        long GetCartItemsCount(long ContactId, long TemporaryCustomerId, long CompanyId);

        List<CmsSkinPageWidget> GetStoreWidgets();

        List<SaveDesignView> GetSavedDesigns(long ContactID);

        void RemoveItemAttacmentPhysically(List<ArtWorkAttatchment> attatchmentList);

        /// <summary>
        /// get all published products against a store
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="OrganisationId"></param>
        /// <returns></returns>
        List<Item> GetProductsList(long CompanyId, long OrganisationId);

       
        /// <summary>
        /// get all parent categories and corresponding products of a category against a store
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="OrganisationId"></param>
        /// <returns></returns>
        List<ProductCategory> GetStoreParentCategories(long CompanyId, long OrganisationId);

        Item GetItemByItemID(long itemId);

        void DeleteItemBySP(long ItemID);

        long getParentTemplateID(long itemId);

        bool UpdateItem(long itemID, long? templateID);

        List<Item> GetItemsWithAttachmentsByOrderID(long OrderID);

        Item GetItemWithSections(long itemID);
        /// <summary>
        /// Gets the attchment list
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        List<ItemAttachment> GetItemAttactchments(long itemID);

        /// <summary>
        /// returns the item , item section and section cost centre
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        Item GetActualItemToClone(long itemID);

        T Clone<T>(T source);
    }

}
