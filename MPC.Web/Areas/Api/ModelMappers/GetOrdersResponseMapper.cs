using System.Linq;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    using ResponseModels = MPC.Models.ResponseModels;

    /// <summary>
    /// Get Orders Response Mapper
    /// </summary>
    public static class GetOrdersResponseMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static GetOrdersResponse CreateFrom(this ResponseModels.GetOrdersResponse source)
        {
            return new GetOrdersResponse
            {
                Orders = source.Orders.Select(order => order.CreateFromForListView()).ToList(),
                TotalCount = source.TotalCount,
                HeadNote = source.HeadNote,
                FootNote = source.FootNote
            };
        }
    }
}