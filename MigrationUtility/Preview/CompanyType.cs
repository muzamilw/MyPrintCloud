//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility.Preview
{
    using System;
    using System.Collections.Generic;
    
    public partial class CompanyType
    {
        public CompanyType()
        {
            this.Companies = new HashSet<Company>();
        }
    
        public long TypeId { get; set; }
        public Nullable<bool> IsFixed { get; set; }
        public string TypeName { get; set; }
    
        public virtual ICollection<Company> Companies { get; set; }
    }
}