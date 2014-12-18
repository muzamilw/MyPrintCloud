using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Cms Tag Domain Model
    /// </summary>
    public class CmsTag
    {
        public long TagId { get; set; }
        public string TagName { get; set; }
        public string TagSlug { get; set; }
        public string Description { get; set; }
        public bool? IsDisplay { get; set; }

        public virtual ICollection<CmsPageTag> CmsPageTags { get; set; }
    }
}
