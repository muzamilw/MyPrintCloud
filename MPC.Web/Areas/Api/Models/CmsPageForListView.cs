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
        /// Is Display
        /// </summary>
        public bool? IsDisplay { get; set; }

        /// <summary>
        /// Category Name
        /// </summary>
        public string CategoryName { get; set; }
    }
}