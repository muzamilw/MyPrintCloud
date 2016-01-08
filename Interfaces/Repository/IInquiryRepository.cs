using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    public interface IInquiryRepository
    {
        int AddInquiryAndItems(Inquiry Inquiry, List<InquiryItem> InquiryItems);
    }
}
