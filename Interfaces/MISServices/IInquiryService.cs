using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    public interface IInquiryService
    {
        GetInquiryResponse GetAll(GetInquiryRequest request);
        Inquiry Add(Inquiry inquiry);
        Inquiry Update(Inquiry inquiry);
        bool Delete(int inquiryId);
        Inquiry GetInquiryById(int id);
        void ProgressInquiryToEstimate(long inquiryId);
        IEnumerable<InquiryItem> GetInquiryItems(int inquiryId);
    }
}
