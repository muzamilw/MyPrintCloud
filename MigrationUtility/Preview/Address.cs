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
    
    public partial class Address
    {
        public Address()
        {
            this.CompanyContacts = new HashSet<CompanyContact>();
            this.CompanyContacts1 = new HashSet<CompanyContact>();
        }
    
        public long AddressId { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public string AddressName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public Nullable<long> StateId { get; set; }
        public Nullable<long> CountryId { get; set; }
        public string PostCode { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string URL { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Extension1 { get; set; }
        public string Extension2 { get; set; }
        public string Reference { get; set; }
        public string FAO { get; set; }
        public Nullable<bool> IsDefaultAddress { get; set; }
        public Nullable<bool> IsDefaultShippingAddress { get; set; }
        public Nullable<bool> isArchived { get; set; }
        public Nullable<long> TerritoryId { get; set; }
        public string GeoLatitude { get; set; }
        public string GeoLongitude { get; set; }
        public Nullable<bool> isPrivate { get; set; }
        public Nullable<long> ContactId { get; set; }
        public Nullable<bool> isDefaultTerrorityBilling { get; set; }
        public Nullable<bool> isDefaultTerrorityShipping { get; set; }
        public Nullable<long> OrganisationId { get; set; }
        public Nullable<bool> DisplayOnContactUs { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<CompanyContact> CompanyContacts { get; set; }
        public virtual Company Company { get; set; }
        public virtual CompanyTerritory CompanyTerritory { get; set; }
        public virtual ICollection<CompanyContact> CompanyContacts1 { get; set; }
    }
}
