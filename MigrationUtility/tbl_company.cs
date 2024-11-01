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
    
    public partial class tbl_company
    {
        public tbl_company()
        {
            this.tbl_company_sites = new HashSet<tbl_company_sites>();
            this.tbl_estimates = new HashSet<tbl_estimates>();
        }
    
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string WebsiteLogo { get; set; }
        public string MISLogo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<int> Country { get; set; }
        public string ZipCode { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string URL { get; set; }
        public string RegNo { get; set; }
        public string TaxNo { get; set; }
        public string TaxName { get; set; }
        public string Language { get; set; }
        public byte[] CostCentreDLL { get; set; }
        public Nullable<System.DateTime> CostCentreUpdationDate { get; set; }
    
        public virtual ICollection<tbl_company_sites> tbl_company_sites { get; set; }
        public virtual ICollection<tbl_estimates> tbl_estimates { get; set; }
    }
}
