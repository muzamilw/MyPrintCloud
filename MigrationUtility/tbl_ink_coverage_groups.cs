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
    
    public partial class tbl_ink_coverage_groups
    {
        public int CoverageGroupID { get; set; }
        public string GroupName { get; set; }
        public Nullable<double> Percentage { get; set; }
        public int IsFixed { get; set; }
        public int SystemSiteID { get; set; }
    }
}
