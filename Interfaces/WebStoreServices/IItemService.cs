using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;
using System.IO;

namespace MPC.Interfaces.WebStoreServices
{
    public interface IItemService
    {
        List<ItemStockOption> GetStockList(long ItemId, long CompanyId);
        Item GetItemById(long ItemId);

        Item CloneItem(long itemID, long RefItemID, long OrderID, long CustomerID, long TemplateID, long StockID, List<AddOnCostsCenter> SelectedAddOnsList, bool isSavedDesign, bool isCopyProduct, long objContactID, long OrganisationID);
        List<ItemPriceMatrix> GetPriceMatrix(List<ItemPriceMatrix> tblRefItemsPriceMatrix, bool IsRanged, bool IsUserLoggedIn, long CompanyId);

        string specialCharactersEncoder(string value);

        ProductItem GetItemAndDetailsByItemID(long itemId);

        List<ProductMarketBriefQuestion> GetMarketingInquiryQuestionsByItemID(int itemID);

        List<ProductMarketBriefAnswer> GetMarketingInquiryAnswersByQID(int QID);

        void CopyAttachments(int itemID, Item NewItem, string OrderCode, bool CopyTemplate, DateTime OrderCreationDate);
        ItemStockControl GetStockItem(long itemId);
        List<AddOnCostsCenter> GetStockOptionCostCentres(long itemId, long companyId);
        bool RemoveCloneItem(long itemID, out List<ArtWorkAttatchment> itemAttatchmetList, out Template clonedTemplateToRemove);

        ProductCategoriesView GetMappedCategory(string CatName, int CID);

        // get related items
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

        /// <summary>
        /// get name of parent categogry by ItemID
        /// </summaery>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        string GetImmidiateParentCategory(long ItemID,out string CurrentProductCategoryName);
        /// <summary>
        /// get list of stock options by item and company id
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="companyID"></param>
        /// <returns></returns>
        List<ItemStockOption> GetAllStockListByItemID(long ItemID, long companyID);
        /// <summary>
        /// get cost center list according to stock option id
        /// </summary>
        /// <param name="StockOptionID"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        List<string> GetProductItemAddOnCostCentres(long StockOptionID, long CompanyID);

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

        List<ItemAttachment> GetArtwork(long ItemId);

        Item GetExisitingClonedItemInOrder(long OrderId, long ReferenceItemId);
        bool UpdateCloneItemService(long clonedItemID, double orderedQuantity, double itemPrice, double addonsPrice, long stockItemID, List<AddOnCostsCenter> newlyAddedCostCenters, int Mode, long OrganisationId, double TaxRate, int CountOfUploads = 0);

        FavoriteDesign GetFavContactDesign(long templateID, long contactID);
        /// <summary>
        /// Update Temporary Customer order
        /// </summary>
        /// <param name="TemporaryCustomerID"></param>
        /// <param name="realCustomerID"></param>
        /// <param name="realContactID"></param>
        /// <param name="replacedOrderdID"></param>
        /// <param name="orderAllItemsAttatchmentsListToBeRemoved"></param>
        /// <param name="clonedTemplateToRemoveList"></param>
        /// <returns></returns>
        long PostLoginCustomerAndCardChanges(long OrderId, long CompanyId, long ContactId, long TemporaryCompanyId, long OrganisationId);

        List<usp_GetRealEstateProducts_Result> GetRealEstateProductsByCompanyID(long CompanyId);

        Item GetItemByOrderID(long OrderID);
        void GenerateThumbnailForPdf(string url, bool insertCuttingMargin);
        List<Item> GetItemsByOrderID(long OrderID);

        List<Item> GetListOfDeliveryItemByOrderID(long OID);
        string SaveDesignAttachments(long templateID, long itemID, long customerID, string DesignName, string caller, long organisationId);
        List<ItemAttachment> SaveArtworkAttachments(List<ItemAttachment> attachmentList);
        bool CreatAndSaveThumnail(Stream oImgstream, string sideThumbnailPath);
        Item GetClonedItemById(long ItemId);
        PaymentGateway GetPaymentGatewayRecord(long CompanyId);
        long GetFirstItemIdByOrderId(long orderId);
        long AddPrePayment(PrePayment prePayment);

        bool RemoveListOfDeliveryItemCostCenter(long OrderId);

        bool AddUpdateItemFordeliveryCostCenter(long orderId, long DeliveryCostCenterId, double DeliveryCost, long customerID, string DeliveryName, StoreMode Mode, bool isDeliveryTaxable, bool IstaxONService, double GetServiceTAX, double TaxRate);

    }
}
