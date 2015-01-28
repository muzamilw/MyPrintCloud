using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.Common;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// CategoryType Mapper
    /// </summary>
    public static class CategoryTypeMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static CategoryType CreateFrom(this DomainModels.CategoryType source)
        {
            return new CategoryType
            {
                TypeId = source.TypeId,
                TypeName = source.TypeName
            };
        }
        
    }
}