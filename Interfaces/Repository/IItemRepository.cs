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

        Item CloneItem(int itemID, double CurrentTotal, int RefItemID, long OrderID, int CustomerID, double Quantity, int TemplateID, int StockID, List<AddOnCostsCenter> SelectedAddOnsList, bool isSavedDesign, bool isCopyProduct,  long ObjContactID, Company NewCustomer);
        Item GetItemById(long RefitemId);

        ProductItem GetItemAndDetailsByItemID(long itemId);

        List<ProductMarketBriefQuestion> GetMarketingInquiryQuestionsByItemID(int itemID);

        List<ProductMarketBriefAnswer> GetMarketingInquiryAnswersByQID(int QID);

        void CopyAttachments(int itemID, Item NewItem, string OrderCode, bool CopyTemplate, DateTime OrderCreationDate);

        bool RemoveCloneItem(long itemID, out List<ArtWorkAttatchment> itemAttatchmetList, out Template clonedTemplateToRemove);

        // get related Items
        List<ProductItem> GetRelatedItemsList();
        /// <summary>
        /// get an item according to usercookiemanager.orderid or itemid 
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        Item GetItemByOrderAndItemID(long ItemID, long OrderID);

        /// <summary>
        /// to find the minimun price of specific Product by itemid
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        double FindMinimumPriceOfProduct(long itemID);

        List<ProductItem> GetRelatedItemsByItemID(long ItemID);
        /// <summary>
        /// get itemimages base on item ID
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        List<ItemImage> getItemImagesByItemID(long ItemID);
         /// <summary>
        /// get default section price flag
        /// </summary>
        /// <returns></returns>
        int GetDefaultSectionPriceFlag();


    }
}
