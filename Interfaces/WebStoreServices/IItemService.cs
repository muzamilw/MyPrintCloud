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
        bool typeFourItemsStatus(long OrderID);
         double MMToPoint(double val);
         double PointToMM(double val);
         double InchtoPoint(double val);
          double PointToInch(double val);
        long TotalProductTypeFourItems(long OrderId);
        long OtherTheTypeFourItems(long OrderId);

        Item CloneItem(long Organisation, long OrderID, Asset GetAsset, string ImagePAth);
        GetCategoryProduct GetPublishedProductByItemID(int itemID);
        List<ItemStockOption> GetStockList(long ItemId, long CompanyId);
        Item GetItemById(long ItemId);
        Item GetItemByIdDesigner(long ItemId);


        Item CloneItem(long itemID, long RefItemID, long OrderID, long CustomerID, long TemplateID, long StockID, List<AddOnCostsCenter> SelectedAddOnsList, bool isSavedDesign, bool isCopyProduct, long objContactID, long OrganisationID, long StoreId, long PropertyId = 0, bool isUploadDesignMode = false, bool isSetTemplateIdToNull = false, double PdfTemplatewidth = 0.0, double PdfTemplateheight = 0.0,int SystemLengthUnit=0);
        List<ItemPriceMatrix> GetPriceMatrix(List<ItemPriceMatrix> tblRefItemsPriceMatrix, bool IsRanged, bool IsUserLoggedIn, long CompanyId, long OrganisationId);

        string specialCharactersEncoder(string value);

        ProductItem GetItemAndDetailsByItemID(long itemId);

        List<ProductMarketBriefQuestion> GetMarketingInquiryQuestionsByItemID(int itemID);

        List<ProductMarketBriefAnswer> GetMarketingInquiryAnswersByQID(int QID);

        void CopyAttachments(long ItemID, Item NewlyClonedItem, string OrderCode, bool CopyTemplate, DateTime OrderCreationDate, long OrganisationId, long CompanyId);
        ItemStockControl GetStockItem(long itemId);
        List<AddOnCostsCenter> GetStockOptionCostCentres(long itemId, long companyId);
        bool RemoveCloneItem(long itemID, out List<ArtWorkAttatchment> itemAttatchmetList, out Template clonedTemplateToRemove);

        ProductCategoriesView GetMappedCategory(string CatName, int CID);

        // get related items
        List<ProductItem> GetRelatedItemsList(long ItemId);

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
        bool UpdateCloneItemService(long clonedItemID, double orderedQuantity, double itemPrice, double addonsPrice, long stockItemID, List<AddOnCostsCenter> newlyAddedCostCenters, int Mode, long OrganisationId, double TaxRate, string ItemMode, bool isInculdeTax, long ItemstockOptionID, long StoreId, int CountOfUploads = 0, string QuestionQueue = "", string CostCentreQueue = "", string InputQueue = "");

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
        long PostLoginCustomerAndCardChanges(long OrderId, long CompanyId, long ContactId, long TemporaryCompanyId, long OrganisationId, double StoreTaxRate, long StoreId);

        List<usp_GetRealEstateProducts_Result> GetRealEstateProductsByCompanyID(long CompanyId);

        Item GetItemByOrderID(long OrderID);
        void GenerateThumbnailForPdf(string url, bool insertCuttingMargin, long ItemId);
        List<Item> GetItemsByOrderID(long OrderID);

        List<Item> GetListOfDeliveryItemByOrderID(long OID);
        string SaveDesignAttachments(long templateID, long itemID, long customerID, string DesignName, string caller, long organisationId);
        List<ItemAttachment> SaveArtworkAttachments(List<ItemAttachment> attachmentList);
        bool CreatAndSaveThumnail(Stream oImgstream, string sideThumbnailPath, string itemID);
        Item GetClonedItemById(long ItemId);
        PaymentGateway GetPaymentGatewayRecord(long CompanyId);
        long GetFirstItemIdByOrderId(long orderId);
        long AddPrePayment(PrePayment prePayment);

        bool RemoveListOfDeliveryItemCostCenter(long OrderId);

        bool AddUpdateItemFordeliveryCostCenter(long orderId, long DeliveryCostCenterId, double DeliveryCost, long customerID, string DeliveryName, StoreMode Mode, bool isDeliveryTaxable, bool IstaxONService, double GetServiceTAX, double TaxRate, long FreeShippingVoucherId, Organisation Organisation);

        Item GetItemByOrderItemID(long ItemID, long OrderID);

        int AddInquiryAndItems(Inquiry Inquiry, List<InquiryItem> InquiryItems);
        void AddInquiryAttachments(List<InquiryAttachment> InquiryAttachments);
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
          /// <summary>
        /// get id's of cost center except webstore cost cnetre 216 of first section of cloned item 
        /// </summary>
        /// <param name="StockOptionID"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        List<SectionCostcentre> GetClonedItemAddOnCostCentres(long ItemId, long OrganisationId);

        /// <summary>
        /// get cart items count 
        /// </summary>
        /// <returns></returns>
        long GetCartItemsCount(long ContactId, long TemporaryCustomerId, long CompanyId);

        List<CmsSkinPageWidget> GetStoreWidgets();

        List<SaveDesignView> GetSavedDesigns(long ContactID);

        void RemoveItemAttacmentPhysically(List<ArtWorkAttatchment> attatchmentList);
        /// <summary>
        /// get published featured, special and popular products
        /// </summary>
        /// <param name="productWidgetId"></param>
        /// <param name="CompanyId"></param>
        /// <param name="OrganisationId"></param>
        /// <returns></returns>
        List<Item> GetProductsWithDisplaySettings(ProductWidget productWidgetId, long CompanyId, long OrganisationId);

    

        /// <summary>
        /// get all parent categories and corresponding products of a category against a store
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="OrganisationId"></param>
        /// <returns></returns>
        List<ProductCategory> GetStoreParentCategories(long CompanyId, long OrganisationId);

        long getParentTemplateID(long itemId);

        string ProcessCorpOrderSkipDesignerMode(long WEBOrderId, int WEBStoreMode, long TemporaryCompanyId, long OrganisationId, long CompanyID, long ContactID, long itemID, long StoreId);
        /// <summary>
        /// get category name
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        string GetCategoryNameById(long CategoryId, long ItemId);
        /// <summary>
        /// get category ID
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        long GetCategoryIdByItemId(long ItemId);
          /// <summary>
        /// Load designer in case of print product 
        /// </summary>
        /// <param name="ItemId"></param>
        /// <param name="ModeOfStore"></param>
        /// <param name="OrderIdFromCookie"></param>
        /// <param name="ContactIdFromClaim"></param>
        /// <param name="CompanyIdFromClaim"></param>
        /// <param name="TemporaryRetailCompanyIdFromCookie"></param>
        /// <param name="OrganisationId"></param>
        /// <returns></returns>
        ItemCloneResult CloneItemAndLoadDesigner(long ItemId, StoreMode ModeOfStore, long OrderIdFromCookie, long ContactIdFromClaim, long CompanyIdFromClaim, long TemporaryRetailCompanyIdFromCookie, long OrganisationId, long StoreId, long PropertyId = 0, double PdfTemplatewidth = 0.0, double PdfTemplateheight = 0.0, int SystemLengthUnit = 0);
         /// <summary>
        /// delete single attachment record
        /// </summary>,
        /// <param name="ItemAttachmentId"></param>
        void DeleteItemAttachment(long ItemAttachmentId);
        long ReOrder(long ExistingOrderId, long loggedInContactID, double StatTaxVal, StoreMode mode, bool isIncludeTax, int TaxID, long OrganisationId, long StoreId);
        List<ItemVideo> GetProductVideos(long ItemID);
        List<ItemAttachment> GetItemAttactchments(long itemID);
        double GetDiscountAmountByVoucher(DiscountVoucher storeDiscountVoucher, double itemTotal, long ItemId, double OrderedQty, long? DiscountIdAlreadyApplied, double OrderTotal, double discountRateAlreadyAppliedOnProduct, ref long FreeShippingVoucherId, ref string voucherErrorMesg);
        bool ApplyDiscountOnCartProducts(DiscountVoucher storeDiscountVoucher, long OrderId, double StoreTaxRate, ref long FreeShippingVoucherId, ref string voucherErrorMesg);
        void SaveOrUpdateDiscountVoucher(DiscountVoucher storeDiscountVoucher);
        string ValidateDiscountVoucher(DiscountVoucher storeDiscountVoucher);//, ref string voucherErrorMesg
        DiscountVoucher GetDiscountVoucherByCouponCode(string DiscountCouponCode, long StoreId, long OrganisationId);
        void RollBackDiscountedItems(long OrderId, double StoreTaxRate, long StoreId, long OrganisationId, bool isDeliveryItem, long ContactId, long CompanyId);
        long ApplyStoreDefaultDiscountRateOnCartItems(long OrderId, long StoreId, long OrganisationId, double StoreTaxRate,ref long FreeShippingVoucherId);
        DiscountVoucher GetDiscountVoucherById(long DiscountVoucherId);
        void ApplyDiscountOnDeliveryItemAlreadyAddedToCart(DiscountVoucher storeDiscountVoucher, long OrderId, double StoreTaxRate);
        long IsStoreHaveFreeShippingDiscountVoucher(long StoreId, long OrganisationId, long OrderId);
        void UpdateOrderIdInItem(long itemId, long OrderId);
        DiscountVoucher GetFreeShippingDiscountVoucherByStoreId(long StoreId, long OrganisationId);
        DiscountVoucher GetOrderDiscountPercentageVoucherByStoreId(long StoreId, long OrganisationId);
        void RollBackSpecificDiscountedItemsByVoucherId(long OrderId, double StoreTaxRate, long StoreId, long OrganisationId, long DiscountVoucherId);
        void UpdateUploadFlagInItem(long ItemId, int? FlagValue);
        void SaveMarketingBriefHistory(MarketingBriefHistory model);
        List<TemplatePage> GetTemplatePagesByItemId(long clonedItemId);
        List<GetCategoryProduct> GetRetailFeaturedPublishedProducts();
        bool IsTerritoryUseUserFonts(long TerritoryId);
        bool IsTerritoryUseColors(long TerritoryId);
    }
}
