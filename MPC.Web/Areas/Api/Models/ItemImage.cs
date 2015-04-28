using System;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Item Image WebApi Model
    /// </summary>
    public class ItemImage
    {
        public int ProductImageId { get; set; }
        public long? ItemId { get; set; }
        public string ImageTitle { get; set; }
        public string ImageURL { get; set; }
        public string ImageType { get; set; }
        public string ImageName { get; set; }
        public DateTime? UploadDate { get; set; }

        /// <summary>
        /// ImageUrl
        /// </summary>
        public byte[] ImageUrlBytes { get; set; }

        /// <summary>
        /// ImageUrl Source
        /// </summary>
        public string ImageUrlSource
        {
            get
            {
                if (ImageUrlBytes == null)
                {
                    return string.Empty;
                }

                string base64 = Convert.ToBase64String(ImageUrlBytes);
                return string.Format("data:{0};base64,{1}", "image/jpg", base64);
            }
        }

        /// <summary>
        /// File in Base64
        /// </summary>
        public string FileSource { get; set; }

        /// <summary>
        /// File Name
        /// </summary>
        public string FileName { get; set; }
    }
}