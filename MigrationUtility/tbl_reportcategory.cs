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
    
    public partial class tbl_reportcategory
    {
        public tbl_reportcategory()
        {
            this.tbl_report_notes = new HashSet<tbl_report_notes>();
            this.tbl_reports = new HashSet<tbl_reports>();
        }
    
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<tbl_report_notes> tbl_report_notes { get; set; }
        public virtual ICollection<tbl_reports> tbl_reports { get; set; }
    }
}
