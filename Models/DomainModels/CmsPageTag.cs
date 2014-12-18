namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Cms Page Tag Domain Model
    /// </summary>
    public class CmsPageTag
    {
        public long? TagId { get; set; }
        public long? PageId { get; set; }
        public int PageTagId { get; set; }
        public long? CompanyId { get; set; }

        public virtual CmsPage CmsPage { get; set; }
        public virtual CmsTag CmsTag { get; set; }
    }
}
