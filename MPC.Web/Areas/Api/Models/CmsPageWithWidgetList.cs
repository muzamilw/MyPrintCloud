using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Cms Page With Skin Widget List,Use for save
    /// </summary>
    public class CmsPageWithWidgetList
    {
        public long PageId { get; set; }

        public List<CmsSkinPageWidget> CmsSkinPageWidgets { get; set; }
    }
}