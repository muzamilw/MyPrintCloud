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
    
    public partial class tbl_costcentretypes
    {
        public tbl_costcentretypes()
        {
            this.tbl_costcentres = new HashSet<tbl_costcentres>();
        }
    
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public short IsSystem { get; set; }
        public short IsExternal { get; set; }
        public Nullable<int> CompanyID { get; set; }
    
        public virtual ICollection<tbl_costcentres> tbl_costcentres { get; set; }
    }
}