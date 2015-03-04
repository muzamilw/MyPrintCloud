using System;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Cms Page For List View
    /// </summary>
    public class CmsPageForListView
    {
        /// <summary>
        /// Page Id
        /// </summary>
        public long PageId { get; set; }

        /// <summary>
        /// Meta Title
        /// </summary>
        public string Meta_Title { get; set; }

        /// <summary>
        /// Page Title
        /// </summary>
        public string PageTitle { get; set; }

        /// <summary>
        /// Is Enabled
        /// </summary>
        public bool? IsEnabled { get; set; }

        /// <summary>
        /// Is user defined or system
        /// </summary>
        public bool? IsUserDefined { get; set; }

        /// <summary>
        /// Is Display
        /// </summary>
        public bool? IsDisplay { get; set; }

        /// <summary>
        /// Category Name
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Image 
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