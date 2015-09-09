using System.Linq;
using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using ResponseModels = MPC.Models.ResponseModels;
    public static class GetInquiryResponseMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static GetInquiriesResponse CreateFrom(this ResponseModels.GetInquiryResponse source)
        {
            return new GetInquiriesResponse
            {
                Inquiries = source.Inquiries.Select(order => order.CreateFromForListView()).ToList(),
                TotalCount = source.TotalCount
            };
        }
    }
}