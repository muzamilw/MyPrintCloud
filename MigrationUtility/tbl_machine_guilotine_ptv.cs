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
    
    public partial class tbl_machine_guilotine_ptv
    {
        public int ID { get; set; }
        public int NoofSections { get; set; }
        public int NoofUps { get; set; }
        public int Noofcutswithoutgutters { get; set; }
        public int Noofcutswithgutters { get; set; }
        public int GuilotineID { get; set; }
    
        public virtual tbl_machines tbl_machines { get; set; }
    }
}
