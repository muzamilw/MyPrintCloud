using MPC.Models.DomainModels;
using CompanyTerritory = MPC.MIS.Areas.Api.Models.CompanyTerritory;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CategoryTerritoryMapper
    {
        /// <summary>
        /// Create From Company Territory
        /// </summary>
        public static MPC.Models.DomainModels.CategoryTerritory CreateFromTerritory(this CompanyTerritory source)
        {
            return new MPC.Models.DomainModels.CategoryTerritory
            {
                CompanyId = source.CompanyId,
                TerritoryId = source.TerritoryId,
            };
        }

        public static CompanyTerritory CreateFromCategoryTerritory(this MPC.Models.DomainModels.CategoryTerritory source)
        {
            return new CompanyTerritory
            {
                TerritoryId = source.TerritoryId != null? (long)source.TerritoryId: 0
            };
        }
    }
}