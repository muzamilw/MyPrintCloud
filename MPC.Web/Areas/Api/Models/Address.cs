using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Address Web Model
    /// </summary>
    public class Address
    {

        public long AddressId { get; set; }
        public long? CompanyId { get; set; }
        public string AddressName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public long? StateId { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public long? CountryId { get; set; }
        public string CountryName { get; set; }
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
        public bool? IsDefaultAddress { get; set; }
        public bool? IsDefaultShippingAddress { get; set; }
        public bool? isArchived { get; set; }
        public long? TerritoryId { get; set; }
        public string GeoLatitude { get; set; }
        public string GeoLongitude { get; set; }
        public bool? isPrivate { get; set; }
        public long? ContactId { get; set; }
        public bool? isDefaultTerrorityBilling { get; set; }
        public bool? isDefaultTerrorityShipping { get; set; }
        public long? OrganisationId { get; set; }
        public CompanyTerritory Territory { get; set; }

        public List<ScopeVariable> ScopeVariables { get; set; }
    }
}