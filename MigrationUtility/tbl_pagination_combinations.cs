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
    
    public partial class tbl_pagination_combinations
    {
        public int ID { get; set; }
        public Nullable<int> Pagination { get; set; }
        public Nullable<int> Combination { get; set; }
        public Nullable<int> Sequence { get; set; }
        public Nullable<int> Multiplier { get; set; }
        public Nullable<int> Sections { get; set; }
        public string Description { get; set; }
        public int SystemSiteID { get; set; }
    }
}
