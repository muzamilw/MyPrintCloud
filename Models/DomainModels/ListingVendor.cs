namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Listing Vendor Domain Model
    /// </summary>
    public class ListingVendor
    {
        public long VendorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Solutation { get; set; }
        public string MailingSolutation { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public long? ListingId { get; set; }
    }
}
