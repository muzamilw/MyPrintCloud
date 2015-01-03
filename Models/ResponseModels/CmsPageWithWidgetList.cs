using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Cms Page Wit hWidget List
    /// </summary>
    public class CmsPageWithWidgetList
    {

        public long PageId { get; set; }

        public List<CmsSkinPageWidget> CmsSkinPageWidgets { get; set; }
    }
}
