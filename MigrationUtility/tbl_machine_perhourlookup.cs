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
    
    public partial class tbl_machine_perhourlookup
    {
        public long ID { get; set; }
        public Nullable<long> MethodID { get; set; }
        public Nullable<double> SpeedCost { get; set; }
        public Nullable<double> Speed { get; set; }
        public double SpeedPrice { get; set; }
    
        public virtual tbl_lookup_methods tbl_lookup_methods { get; set; }
    }
}
