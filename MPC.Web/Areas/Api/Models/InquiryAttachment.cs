using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPC.MIS.Areas.Api.Models
{
    public class InquiryAttachment
    {
        public int AttachmentId { get; set; }
        public string OrignalFileName { get; set; }
        public string AttachmentPath { get; set; }
        public int InquiryId { get; set; }
        public string Extension { get; set; }
        [NotMapped]
        public string AttachmentFileURL { get; set; }
        #region Additional Properties

        /// <summary>
        /// File in Base64
        /// </summary>
        [NotMapped]
        public string FileSource { get; set; }

        /// <summary>
        /// File Source Bytes - byte[] representation of Base64 string FileSource
        /// </summary>
        [NotMapped]
        public byte[] FileSourceBytes
        {
            get
            {
                if (string.IsNullOrEmpty(FileSource))
                {
                    return null;
                }

                int firtsAppearingCommaIndex = FileSource.IndexOf(',');

                if (firtsAppearingCommaIndex < 0)
                {
                    return null;
                }

                if (FileSource.Length < firtsAppearingCommaIndex + 1)
                {
                    return null;
                }

                string sourceSubString = FileSource.Substring(firtsAppearingCommaIndex + 1);

                try
                {
                    return Convert.FromBase64String(sourceSubString.Trim('\0'));
                }
                catch (FormatException)
                {
                    return null;
                }
            }
        }

        #endregion
    }
}