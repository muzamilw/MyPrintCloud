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
    
    public partial class tbl_contactcompanytypes
    {
        public tbl_contactcompanytypes()
        {
            this.tbl_contactcompanies = new HashSet<tbl_contactcompanies>();
        }
    
        public int TypeID { get; set; }
        public Nullable<bool> IsFixed { get; set; }
        public string TypeName { get; set; }
    
        public virtual ICollection<tbl_contactcompanies> tbl_contactcompanies { get; set; }
    }
}