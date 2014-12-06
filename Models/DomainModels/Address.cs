using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class Address
    {
        public long AddressId { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public string AddressName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
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
        public Nullable<int> ContactId { get; set; }
        public Nullable<bool> isDefaultTerrorityBilling { get; set; }
        public Nullable<bool> isDefaultTerrorityShipping { get; set; }
        public Nullable<long> OrganisationId { get; set; }

        public virtual CompanyTerritory CompanyTerritory { get; set; }
        public virtual ICollection<CompanyContact> CompanyContacts { get; set; }
    }
}
