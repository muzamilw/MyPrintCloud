using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    public interface IStockCategoryRepository : IBaseRepository<StockCategory, long>
    {
        StockCategoryResponse SearchStockCategory(StockCategoryRequestModel request);

        List<StockCategory> GetStockCategoriesByOrganisationID(long OrganisationID);

        /// <summary>
        /// Get Stock Categories For Inventory
        /// </summary>
        IEnumerable<StockCategory> GetStockCategoriesForInventory();

        List<StockCategory> getStockCatByOrgid();

        List<StockCategory> getDefaulStockCat();

        void UpdateStockItemForCatDeleteion(long StockId, long CategoryId, long SubCategoryId);

        List<StockSubCategory> getStockSubCategoryByCategoryId(long CatId);

        StockCategory getStockCategoryByCategoryId(long CatId);

    }
}
