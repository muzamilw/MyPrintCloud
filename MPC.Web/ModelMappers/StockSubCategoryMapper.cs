using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.MIS.Models;

namespace MPC.MIS.ModelMappers
{
    public static class StockSubCategoryMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static StockSubCategory CreateFrom(this DomainModels.StockSubCategory source)
        {
            return new StockSubCategory
                   {
                       CategoryId = source.CategoryId,
                       Code = source.Code,
                       Description = source.Description,
                       Fixed = source.Fixed,
                       Name = source.Name,
                       SubCategoryId = source.SubCategoryId
                   };
        }
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiModels.StockSubCategoryDropDown CreateFromDropDown(this DomainModels.StockSubCategory source)
        {
            return new ApiModels.StockSubCategoryDropDown
            {
                CategoryId = source.CategoryId,
                Name = source.Name,
                SubCategoryId = source.SubCategoryId
            };
        }
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.StockSubCategory CreateFrom(this StockSubCategory source)
        {
            return new DomainModels.StockSubCategory
                   {
                       CategoryId = source.CategoryId,
                       Code = source.Code,
                       Description = source.Description,
                       Fixed = source.Fixed,
                       Name = source.Name,
                       SubCategoryId = source.SubCategoryId
                   };
        }
    }
}