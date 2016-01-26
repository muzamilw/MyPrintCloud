using System.Collections.Generic;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    /// <summary>
    /// Item Service Interface
    /// </summary>
    public interface IItemService
    {
        /// <summary>
        /// Deletes Product Permanently
        /// </summary>
        void DeleteProduct(long itemId);

        /// <summary>
        /// Load Items, based on search filters
        /// </summary>
        ItemListViewSearchResponse GetItems(ItemSearchRequestModel request);

        /// <summary>
        /// Get by Id
        /// </summary>
        Item GetById(long id, bool changeTemplateSizeUnits = true);

        /// <summary>
        /// Save Product Image
        /// </summary>
        Item SaveProductImage(string filePath, long itemId, ItemFileType itemFileType);

        /// <summary>
        /// Delete Image
        /// </summary>
        string DeleteProductImage(long itemId, ItemFileType itemFileType);

        /// <summary>
        /// Save Product
        /// </summary>
        Item SaveProduct(Item item);

        /// <summary>
        /// Archive Product
        /// </summary>
        void ArchiveProduct(long itemId);

        /// <summary>
        /// Get Base Data
        /// </summary>
        ItemBaseResponse GetBaseData();

        /// <summary>
        /// Get Stock Items for Stock Selection Dialog
        /// Used in Products
        /// </summary>
        InventorySearchResponse GetStockItems(StockItemRequestModel request);
       
        /// <summary>
        /// Get Item Price Matrices for Section Flag & Item 
        /// </summary>
        IEnumerable<ItemPriceMatrix> GetItemPriceMatricesBySectionFlagForItem(long sectionFlagId, long itemId);

        /// <summary>
        /// Get Base Data For Designer Template
        /// </summary>
        ItemDesignerTemplateBaseResponse GetBaseDataForDesignerTemplate(long? companyId);

        /// <summary>
        /// Get Machines for Press Selection Dialog
        /// Used in Products
        /// </summary>
        MachineSearchResponse GetMachines(MachineSearchRequestModel request);

        /// <summary>
        /// Clone Product
        /// </summary>
        Item CloneProduct(long itemId);

        /// <summary>
        /// Get Items By Company Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ItemSearchResponse GetItemsByCompanyId(ItemSearchRequestModel request);

        /// <summary>
        /// Get Parent Product Categories for Company
        /// </summary>
        IEnumerable<ProductCategory> GetProductCategoriesForCompany(long? companyId);

        bool DeleteItem(long ItemID, long OrganisationID);
        IEnumerable<ProductCategory> GetProductCategoriesIncludingArchived(long? companyId);

        IEnumerable<Item> GetProductsByCompanyId(long? companyId);
        ItemSection GetItemFirstSectionByItemId(long itemId);
        ItemSection UpdateItemFirstSectionByItemId(long itemId, int quantity);

        string ExportItems(long CompanyId);

    }
}
