using MPC.Models.DomainModels;
using CategoryTerritory = MPC.MIS.Areas.Api.Models.CategoryTerritory;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CategoryTerritoryMapper
    {
        /// <summary>
        /// Create From Company Territory
        /// </summary>
        public static MPC.Models.DomainModels.CategoryTerritory CreateFromTerritory(this CategoryTerritory source)
        {
            return new MPC.Models.DomainModels.CategoryTerritory
            {
                CompanyId = source.CompanyId,
                TerritoryId = source.TerritoryId,
                CategoryTerritoryId = source.CategoryTerritoryId,
                ProductCategoryId = source.ProductCategoryId
            };
        }

        public static CategoryTerritory CreateFromCategoryTerritory(this MPC.Models.DomainModels.CategoryTerritory source)
        {
            return new CategoryTerritory
            {
                TerritoryId = source.TerritoryId != null? (long)source.TerritoryId: 0,
                CompanyId = source.CompanyId,
                CategoryTerritoryId = source.CategoryTerritoryId,
                ProductCategoryId = source.ProductCategoryId
            };
        }
    }
}