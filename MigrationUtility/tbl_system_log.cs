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
    
    public partial class tbl_system_log
    {
        public int LogID { get; set; }
        public Nullable<int> SectionID { get; set; }
        public Nullable<int> ID { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> LogDate { get; set; }
        public Nullable<System.DateTime> LogTime { get; set; }
        public Nullable<int> SystemUserID { get; set; }
    }
}
