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

            string url = GetfilePath(filePath);
            return new InquiryAttachment
            {
                AttachmentId = source.AttachmentId,
                AttachmentPath = filePath,
               AttachmentFileURL = url,
                Extension = source.Extension,
                InquiryId = source.InquiryId,
                OrignalFileName = source.OrignalFileName
            };
        }

        public static string GetfilePath(string Path)
        {
            var url = "/mis/Content/Images/AnyFile.png";

            // for pdf
            if (Path.Contains(".pdf"))
            {
                url = "/mis/Content/Images/PDFFile.png";

            }// for psd
            else if (Path.Contains(".psd"))
            {
                url = "/mis/Content/Images/PSDFile.png";

            }// for ai
            else if (Path.Contains(".ai"))
            {
                url = "/mis/Content/Images/IllustratorFile.png";

            } // for indd
            else if (Path.Contains(".indd"))
            {
                url = "/mis/Content/Images/InDesignFile.png";

            }// for jpg
            else if (Path.Contains(".jpg") || Path.Contains(".jpeg"))
            {
                url = "/mis/Content/Images/JPGFile.png";

            }//for png
            else if (Path.Contains(".png"))
            {
                url = "/mis/Content/Images/PNGFile.png";

            }

            return url;
        }


    }
}