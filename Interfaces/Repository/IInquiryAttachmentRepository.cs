using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface IInquiryAttachmentRepository : IBaseRepository<InquiryAttachment, long>
    {
        void SaveInquiryAttachments(List<InquiryAttachment> AttachmentList);
    }
}
