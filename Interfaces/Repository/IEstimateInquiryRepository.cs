using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    public interface IEstimateInquiryRepository : IBaseRepository<Inquiry, long>
    {

        GetInquiryResponse GetInquiries(GetInquiryRequest request);
        IEnumerable<InquiryItem> GetInquiryItems(int inquiryId);
    }
}
