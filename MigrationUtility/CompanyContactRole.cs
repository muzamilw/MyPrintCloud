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
    
    public partial class CompanyContactRole
    {
        public CompanyContactRole()
        {
            this.CompanyContacts = new HashSet<CompanyContact>();
        }
    
        public int ContactRoleId { get; set; }
        public string ContactRoleName { get; set; }
    
        public virtual ICollection<CompanyContact> CompanyContacts { get; set; }
    }
}
