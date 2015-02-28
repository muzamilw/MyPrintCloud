using System.Collections.Generic;
using System.Linq;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// Company Product Category WebApi Mapper
    /// </summary>
    public static class CompanyProductCategoryMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static IEnumerable<ProductCategoryDropDown> CreateFrom(this IEnumerable<DomainModels.ProductCategory> source)
        {
            return source == null ? new List<ProductCategoryDropDown>() : source.Select(pc => pc.CreateFromDropDown());
        }
        
    }
}