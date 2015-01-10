using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.WebStoreServices
{
    public interface IItemService
    {
        List<ItemStockOption> GetStockList(long ItemId, long CompanyId);
        Item GetItemById(long ItemId);

        Item CloneItem(long itemID, long RefItemID, long OrderID, long CustomerID, int TemplateID, long StockID, List<AddOnCostsCenter> SelectedAddOnsList, bool isSavedDesign, bool isCopyProduct, long objContactID);
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

        //long CloneItem(long itemID, long RefItemID, long OrderID, long CustomerID, int TemplateID, long StockID, List<AddOnCostsCenter> SelectedAddOnsList, bool isSavedDesign, bool isCopyProduct, long objContactID);
    
        bool UpdateCloneItemService(long clonedItemID, double orderedQuantity, double itemPrice, double addonsPrice, long stockItemID, List<AddOnCostsCenter> newlyAddedCostCenters, int Mode, long OrganisationId, double TaxRate, int CountOfUploads = 0);
            /// <summary>
            /// In Webstore when ordering an item in case of upload design this function checks if the same item exists in the order already.
            /// </summary>
            /// <param name="OrderId"></param>
            /// <param name="ReferenceItemId"></param>
            /// <returns></returns>
        Item GetExisitingClonedItemInOrder(long OrderId, long ReferenceItemId);
    }
}
