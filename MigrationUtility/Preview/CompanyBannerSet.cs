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
    
    public partial class CompanyBannerSet
    {
        public CompanyBannerSet()
        {
            this.CompanyBanners = new HashSet<CompanyBanner>();
        }
    
        public long CompanySetId { get; set; }
        public string SetName { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public Nullable<long> OrganisationId { get; set; }
    
        public virtual ICollection<CompanyBanner> CompanyBanners { get; set; }
        public virtual Company Company { get; set; }
    }
}