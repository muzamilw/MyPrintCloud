﻿using MPC.Models.Common;
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
        Item CloneItem(long itemID, long RefItemID, long OrderID, long CustomerID, int TemplateID, long StockID, List<AddOnCostsCenter> SelectedAddOnsList, bool isSavedDesign, bool isCopyProduct, long objContactID);

        Item GetItemById(long RefitemId);

        ProductItem GetItemAndDetailsByItemID(int itemId);

        List<ProductMarketBriefQuestion> GetMarketingInquiryQuestionsByItemID(int itemID);

        List<ProductMarketBriefAnswer> GetMarketingInquiryAnswersByQID(int QID);

        void CopyAttachments(int itemID, Item NewItem, string OrderCode, bool CopyTemplate, DateTime OrderCreationDate);

        bool RemoveCloneItem(long itemID, out List<ArtWorkAttatchment> itemAttatchmetList, out Template clonedTemplateToRemove);

        bool UpdateCloneItem(long clonedItemID, double orderedQuantity, double itemPrice, double addonsPrice, long stockItemID, List<AddOnCostsCenter> newlyAddedCostCenters, int Mode, long OrganisationId, double TaxRate, int CountOfUploads = 0);
    }
}
