//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_cmsPageTags
    {
        public Nullable<int> TagID { get; set; }
        public Nullable<int> PageID { get; set; }
        public int PageTagID { get; set; }
    
        public virtual tbl_cmsPages tbl_cmsPages { get; set; }
        public virtual tbl_cmsTags tbl_cmsTags { get; set; }
    }
}
