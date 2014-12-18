using System.Linq;
using MPC.MIS.Areas.Api.Models;
using StockCategory = MPC.MIS.Areas.Api.Models.StockCategory;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class StockCategoryMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static StockCategory CreateFrom(this MPC.Models.DomainModels.StockCategory source)
        {
            return new StockCategory
            {
                CategoryId = source.CategoryId,
                Code = source.Code,
                Name = source.Name,
                Description = source.Description,
                Fixed = source.Fixed,
                ItemWeight = source.ItemWeight,
                ItemColour = source.ItemColour,
                ItemSizeCustom = source.ItemSizeCustom,
                ItemPaperSize = source.ItemPaperSize,
                ItemCoatedType = source.ItemCoatedType,
                ItemCoated = source.ItemCoated,
                ItemExposure = source.ItemExposure,
                ItemCharge = source.ItemCharge,
                RecLock = source.RecLock,
                TaxId = source.TaxId,
                Flag1 = source.Flag1,
                Flag2 = source.Flag2,
                Flag3 = source.Flag3,
                Flag4 = source.Flag4,
                CompanyId = source.CompanyId,
                StockSubCategories = source.StockSubCategories.Select(x => x.CreateFrom()).ToList()
            };
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static StockCategoryDropDown CreateFromDropDown(this MPC.Models.DomainModels.StockCategory source)
        {
            return new StockCategoryDropDown
            {
                CategoryId = source.CategoryId,
                Name = source.Name,
            };
        }
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static MPC.Models.DomainModels.StockCategory CreateFrom(this StockCategory source)
        {
            var stockCategory = new MPC.Models.DomainModels.StockCategory
            {
                CategoryId = source.CategoryId,
                Code = source.Code,
                Name = source.Name,
                Description = source.Description,
                Fixed = source.Fixed,
                ItemWeight = source.ItemWeight,
                ItemColour = source.ItemColour,
                ItemSizeCustom = source.ItemSizeCustom,
                ItemPaperSize = source.ItemPaperSize,
                ItemCoatedType = source.ItemCoatedType,
                ItemCoated = source.ItemCoated,
                ItemExposure = source.ItemExposure,
                ItemCharge = source.ItemCharge,
                RecLock = source.RecLock,
                TaxId = source.TaxId,
                Flag1 = source.Flag1,
                Flag2 = source.Flag2,
                Flag3 = source.Flag3,
                Flag4 = source.Flag4,
                CompanyId = source.CompanyId,
            };
            if (source.StockSubCategories != null)
            {
                foreach (var stockSubCategory in source.StockSubCategories)
                {
                    stockCategory.StockSubCategories.Add(stockSubCategory.CreateFrom());
                }
            }
            return stockCategory;
        }
    }
}