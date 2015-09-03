

namespace MPC.Models.Common
{
    public class LiveStoreDetails
    {
        /// <summary>
        /// Organisation ID
        /// </summary>
        public long OrganisationId { get; set; }
        /// <summary>
        /// Store ID
        /// </summary>
        public long StoreId { get; set; }
        /// <summary>
        /// Store Unique Web Access Code
        /// </summary>
        public string StoreCode { get; set; }
        /// <summary>
        /// Store Name
        /// </summary>
        public string StoreName { get; set; }
        /// <summary>
        /// Store Type Retail or Corporate
        /// </summary>
        public int StoreType { get; set; }
        /// <summary>
        /// Logo URL
        /// </summary>
        public string LogoUrl { get; set; }
        /// <summary>
        /// Live Domain
        /// </summary>
        public string DefaultDomain { get; set; }
        /// <summary>
        /// Address Name
        /// </summary>
        public string AddressName { get; set; }
        /// <summary>
        /// Address 1
        /// </summary>
        public string Address1 { get; set; }
        /// <summary>
        /// Address 2
        /// </summary>
        public string Address2 { get; set; }
        /// <summary>
        /// Address City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Address State
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Address Country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Latitude
        /// </summary>
        public string GeoLatitude { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        public string GeoLongitude { get; set; }
    }
}
