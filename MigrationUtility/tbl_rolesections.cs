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
    
    public partial class tbl_rolesections
    {
        public int RSID { get; set; }
        public int RoleID { get; set; }
        public int SectionID { get; set; }
    
        public virtual tbl_roles tbl_roles { get; set; }
        public virtual tbl_sections tbl_sections { get; set; }
    }
}
