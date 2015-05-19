using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class InquiryAttachmentMapper
    {
        /// <summary>
        /// Create From Web Model
        /// </summary>
        public static DomainModels.InquiryAttachment CreateFrom(this InquiryAttachment source)
        {
            return new DomainModels.InquiryAttachment
            {
                AttachmentId = source.AttachmentId,
                AttachmentPath = source.AttachmentPath,
                Extension = source.Extension,
                InquiryId = source.InquiryId,
                OrignalFileName = source.OrignalFileName
            };
        }
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static InquiryAttachment CreateFrom(this DomainModels.InquiryAttachment source)
        {
            return new InquiryAttachment
            {
                AttachmentId = source.AttachmentId,
                AttachmentPath = source.AttachmentPath,
                Extension = source.Extension,
                InquiryId = source.InquiryId,
                OrignalFileName = source.OrignalFileName
            };
        }
    }
}