using System;
using MPC.Models.DomainModels;

namespace MPC.Models.ModelMappers
{
    public sealed class InquiryMapperActions
    {
        /// <summary>
        /// Action to create a Inquiry item 
        /// </summary>
        public Func<InquiryItem> CreateInquiryItem { get; set; }

        /// <summary>
        /// Action to Delete Inquiry item 
        /// </summary>
        public Action<InquiryItem> DeleteInquiryItem { get; set; }

        /// <summary>
        /// Action to create a Inquiry Attachment 
        /// </summary>
        public Func<InquiryAttachment> CreateInquiryAttachment { get; set; }

        /// <summary>
        /// Action to Delete Inquiry Attachment 
        /// </summary>
        public Action<InquiryAttachment> DeleteInquiryAttachment { get; set; }
    }
}
