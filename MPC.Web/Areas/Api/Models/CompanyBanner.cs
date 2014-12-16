using System;
using System.IO;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Company Banner Mis Model
    /// </summary>
    public class CompanyBanner
    {
        /// <summary>
        /// Company Banner ID
        /// </summary>
        public long CompanyBannerId { get; set; }

        /// <summary>
        /// Page ID
        /// </summary>
        public int? PageId { get; set; }

        /// <summary>
        /// Image URL
        /// </summary>
        public string ImageURL { get; set; }

        /// <summary>
        /// Heading
        /// </summary>
        public string Heading { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Item URL
        /// </summary>
        public string ItemURL { get; set; }

        /// <summary>
        /// Button URL
        /// </summary>
        public string ButtonURL { get; set; }


        /// <summary>
        /// Company Set Id
        /// </summary>
        public long? CompanySetId { get; set; }

        /// <summary>
        /// File Bytes
        /// </summary>
        public string Bytes { get; set; }

        /// <summary>
        /// File Name
        /// </summary>
        public string FileName { get; set; }


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