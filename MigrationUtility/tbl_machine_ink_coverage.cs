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
    
    public partial class tbl_machine_ink_coverage
    {
        public long ID { get; set; }
        public Nullable<int> SideInkOrder { get; set; }
        public Nullable<int> SideInkOrderCoverage { get; set; }
        public Nullable<int> MachineID { get; set; }
    
        public virtual tbl_machines tbl_machines { get; set; }
    }
}
