using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class InquiryAttachment
    {
        public int AttachmentId { get; set; }
        public string OrignalFileName { get; set; }
        public string AttachmentPath { get; set; }
        public int InquiryId { get; set; }
        public string Extension { get; set; }

        public virtual Inquiry Inquiry { get; set; }
    }
}
