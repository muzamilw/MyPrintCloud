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
    
    public partial class tbl_userreports
    {
        public int UserReportID { get; set; }
        public int SystemUserID { get; set; }
        public int ReportID { get; set; }
        public short IsDefault { get; set; }
        public Nullable<int> ReportCategoryID { get; set; }
    }
}
