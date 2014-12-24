using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Cms Page Widget Detail ForLayout
    /// </summary>
    public class CmsPageWidgetResponse
    {
        /// <summary>
        /// Page Widgets
        /// </summary>
        public List<CmsSkinPageWidget> PageWidgets { get; set; }
    }
}