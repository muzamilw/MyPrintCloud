using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// Product Category Item WebApi Mapper
    /// </summary>
    public static class ProductCategoryItemMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static ProductCategoryItem CreateFrom(this DomainModels.ProductCategoryItem source)
        {
            if (source == null)
            {
                return null;
            }

            return new ProductCategoryItem
            {
                CategoryId = source.CategoryId,
                ItemId = source.ItemId,
                ProductCategoryItemId = source.ProductCategoryItemId
            };
        }

        /// <summary>
        /// Create From WebApi Model
        /// </summary>
        public static DomainModels.ProductCategoryItemCustom CreateFrom(this ProductCategoryItem source)
        {
            if (source == null)
            {
                return null;
            }

            return new DomainModels.ProductCategoryItemCustom
            {
                CategoryId = source.CategoryId,
                ItemId = source.ItemId,
                ProductCategoryItemId = source.ProductCategoryItemId,
                IsSelected = source.IsSelected
            };
        }

    }
}