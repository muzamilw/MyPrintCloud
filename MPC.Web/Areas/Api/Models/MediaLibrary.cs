using System;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Media Library Api Model
    /// </summary>
    public class MediaLibrary
    {
        public long MediaId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long CompanyId { get; set; }
        public string FileSource { get; set; }

        /// <summary>
        /// Image Bytes
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// Image Source
        /// </summary>
        public string ImageSource
        {
            get
            {
                if (Image == null)
                {
                    return string.Empty;
                }

                string base64 = Convert.ToBase64String(Image);
                return string.Format("data:{0};base64,{1}", "image/jpg", base64);
            }
        }
    }
}