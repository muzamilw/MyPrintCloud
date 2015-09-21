using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class vw_RealEstateProperties
    {
        public long ListingID { get; set; }
        public string WebLink { get; set; }
        public string AddressDisplay { get; set; }
        public string StreetAddress { get; set; }
        public Nullable<int> StreetNumber { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public string PropertyCategory { get; set; }
        public Nullable<double> DisplayPrice { get; set; }
        public string MainHeadLine { get; set; }
        public string MainDescription { get; set; }
        public Nullable<int> BedRooms { get; set; }
        public Nullable<int> BathRooms { get; set; }
        public Nullable<int> LoungeRooms { get; set; }
        public Nullable<int> Toilets { get; set; }
        public Nullable<int> Studies { get; set; }
        public Nullable<int> Pools { get; set; }
        public Nullable<int> Garages { get; set; }
        public Nullable<int> Carports { get; set; }
        public string Features { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public string ListingImage { get; set; }
        public string ListingAgent { get; set; }
    }
}