//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility.Preview
{
    using System;
    using System.Collections.Generic;
    
    public partial class CmsPageTag
    {
        public Nullable<long> TagId { get; set; }
        public Nullable<long> PageId { get; set; }
        public int PageTagId { get; set; }
        public Nullable<long> CompanyId { get; set; }
    
        public virtual CmsPage CmsPage { get; set; }
        public virtual CmsTag CmsTag { get; set; }
    }
}
