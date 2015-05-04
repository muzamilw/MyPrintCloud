using System;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Cms Page API Model
    /// </summary>
    public class CmsPage
    {
        #region Public Properties
        public long PageId { get; set; }
        public string PageTitle { get; set; }
        public string PageKeywords { get; set; }
        public string Meta_Title { get; set; }
        public string Meta_DescriptionContent { get; set; }
        public string Meta_CategoryContent { get; set; }
        public string Meta_RobotsContent { get; set; }
        public string Meta_AuthorContent { get; set; }
        public bool? IsUserDefined { get; set; }
        public string Meta_LanguageContent { get; set; }
        public string Meta_RevisitAfterContent { get; set; }
        public long? CategoryId { get; set; }
        public string PageHTML { get; set; }
        public string FileName { get; set; }
        public string DefaultPageKeyWords { get; set; }
        public string PageBanner { get; set; }
        public bool? isEnabled { get; set; }
        public long? CompanyId { get; set; }

        /// <summary>
        /// File Bytes
        /// </summary>
        public string Bytes { get; set; }

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
        #endregion
    }
}