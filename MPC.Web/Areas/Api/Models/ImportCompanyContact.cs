using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileHelpers;

namespace MPC.MIS.Areas.Api.Models
{
    [DelimitedRecord(",")] 
    public class ImportCompanyContact
    {
        //public long StagingId { get; set; }
        //public string CompanyName { get; set; }
        //public long? CompanyId { get; set; }
        //public long? AddressId { get; set; }
        public string AddressName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        //public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        //public long? StateId { get; set; }
        public string Country { get; set; }
        //public long? CountryId { get; set; }
        public string Postcode { get; set; }
        public string AddressPhone { get; set; }
        public string AddressFax { get; set; }
        //public long? TerritoryId { get; set; }
        public string TerritoryName { get; set; }
        //public long? ContactId { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string JobTitle { get; set; }
        public string ContactPhone { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string ContactFax { get; set; }
        public string AddInfo1 { get; set; }
        public string AddInfo2 { get; set; }
        public string AddInfo3 { get; set; }
        public string AddInfo4 { get; set; }
        public string AddInfo5 { get; set; }

        //public string password { get; set; }
        //public long? RoleId { get; set; }
        //public long? OrganisationId { get; set; }
    }
}