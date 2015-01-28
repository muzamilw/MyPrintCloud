using System.Collections.Generic;
using System.Linq;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.ResponseModels;

    /// <summary>
    /// Item Vdp Price Mapper
    /// </summary>
    public static class ItemDesignerTemplateBaseResponseMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ItemDesignerTemplateBaseResponse CreateFrom(this DomainModels.ItemDesignerTemplateBaseResponse source)
        {
            return new ItemDesignerTemplateBaseResponse
            {
                TemplateCategories = source.TemplateCategories != null ? source.TemplateCategories.Select(cc => cc.CreateFromForTemplate()) : 
                new List<ProductCategoryForTemplate>(),
                CategoryRegions = source.CategoryRegions != null ? source.CategoryRegions.Select(cc => cc.CreateFrom()) :
                new List<CategoryRegion>(),
                CategoryTypes = source.CategoryTypes != null ? source.CategoryTypes.Select(cc => cc.CreateFrom()) : 
                new List<CategoryType>()
            };
        }
        
    }
}