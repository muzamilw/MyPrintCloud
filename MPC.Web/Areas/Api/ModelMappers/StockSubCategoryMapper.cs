using MPC.MIS.Areas.Api.Models;
using StockSubCategory = MPC.MIS.Areas.Api.Models.StockSubCategory;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class StockSubCategoryMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static StockSubCategory CreateFrom(this MPC.Models.DomainModels.StockSubCategory source)
        {
            return new StockSubCategory
                   {
                       CategoryId = source.CategoryId,
                       Code = source.Code,
                       Description = source.Description,
                       Fixed = source.Fixed,
                       Name = source.Name,
                       SubCategoryId = source.SubCategoryId,
                       OrganisationId = source.OrganisationId
                   };
        }
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static StockSubCategoryDropDown CreateFromDropDown(this MPC.Models.DomainModels.StockSubCategory source)
        {
            return new StockSubCategoryDropDown
            {
                CategoryId = source.CategoryId,
                Name = source.Name,
                SubCategoryId = source.SubCategoryId
            };
        }
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static MPC.Models.DomainModels.StockSubCategory CreateFrom(this StockSubCategory source)
        {
            return new MPC.Models.DomainModels.StockSubCategory
                   {
                       CategoryId = source.CategoryId,
                       Code = source.Code,
                       Description = source.Description,
                       Fixed = source.Fixed,
                       Name = source.Name,
                       SubCategoryId = source.SubCategoryId,
                       OrganisationId = source.OrganisationId
                   };
        }
    }
}