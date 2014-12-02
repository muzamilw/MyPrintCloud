using System.Linq;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    using ResponseModels = MPC.Models.ResponseModels;

    /// <summary>
    /// Item Response Mapper
    /// </summary>
    public static class ItemResponseMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ItemSearchResponse CreateFrom(this ResponseModels.ItemSearchResponse source)
        {
            return new ItemSearchResponse
            {
                Items = source.Items.Select(stockItem => stockItem.CreateFrom()).ToList(),
                TotalCount = source.TotalCount
            };
        }
    }
}