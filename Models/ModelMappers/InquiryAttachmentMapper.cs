using System;
using MPC.Models.DomainModels;

namespace MPC.Models.ModelMappers
{
    public static class InquiryAttachmentMapper
    {
         /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this InquiryAttachment source, InquiryAttachment target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.AttachmentId = source.AttachmentId;
            target.OrignalFileName = source.OrignalFileName;
           // target.AttachmentPath = source.AttachmentPath;
            target.FileSource = source.FileSource;
            target.InquiryId = source.InquiryId;
            target.Extension = source.Extension;
        }
    }
}
