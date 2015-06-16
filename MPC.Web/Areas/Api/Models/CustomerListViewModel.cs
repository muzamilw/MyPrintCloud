using System;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Model while displaying customer list
    /// </summary>
    public class CustomerListViewModel
    {
        /// <summary>
        /// Company Id
        /// </summary>
        public long  CompnayId { get; set; }

        /// <summary>
        /// Customer Name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Customer Creation date
        /// </summary>
        public DateTime? DateCreted { get; set; }

        /// <summary>
        /// Customer Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Customer TYpe 
        /// </summary>
        public short CustomerType { get; set; }

        /// <summary>
        /// Default Contact Name
        /// </summary>
        public string DefaultContactName { get; set; }

        /// <summary>
        /// Default Contact Email
        /// </summary>
        public string DefaultContactEmail { get; set; }

        /// <summary>
        /// Customer Email
        /// </summary>
        public string Email { get; set; }
        public byte[] Image { get; set; }
        public string ImageBytes { get; set; }
        public string StoreImagePath { get; set; }

        public string StoreName { get; set; }
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