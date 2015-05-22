using System;
using System.Globalization;
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
                OrignalFileName = source.OrignalFileName,
                FileSource = source.AttachmentPath
            };
        }

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static InquiryAttachment CreateFrom(this DomainModels.InquiryAttachment source)
        {
            string filePath = !string.IsNullOrEmpty(source.AttachmentPath) ? source.AttachmentPath : string.Empty;
            string fileName = !string.IsNullOrEmpty(source.OrignalFileName) ? source.OrignalFileName + "?" + DateTime.Now.ToString(CultureInfo.InvariantCulture) : string.Empty;
            filePath += "/" + fileName;
            return new InquiryAttachment
            {
                AttachmentId = source.AttachmentId,
                AttachmentPath = filePath,
                Extension = source.Extension,
                InquiryId = source.InquiryId,
                OrignalFileName = source.OrignalFileName
            };
        }


    }
}