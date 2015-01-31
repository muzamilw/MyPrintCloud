using System;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Campaign Image Api Model
    /// </summary>
    public class CampaignImage
    {
        public long CampaignImageId { get; set; }
        public long? CampaignId { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public string ImageByteSource { get; set; }

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