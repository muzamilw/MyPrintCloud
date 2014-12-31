using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
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

        Item CloneItem(int itemID, double CurrentTotal, int RefItemID, long OrderID, int CustomerID, double Quantity, int TemplateID, int StockID, List<AddOnCostsCenter> SelectedAddOnsList, bool isCorporate, bool isSavedDesign, bool isCopyProduct,  int ObjContactID, Company NewCustomer);
        Item GetItemById(long RefitemId);

        ProductItem GetItemAndDetailsByItemID(int itemId);

        List<ProductMarketBriefQuestion> GetMarketingInquiryQuestionsByItemID(int itemID);

        List<ProductMarketBriefAnswer> GetMarketingInquiryAnswersByQID(int QID);
    }
}
