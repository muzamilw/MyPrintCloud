namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Listing Link Domain Model
    /// </summary>
    public class ListingLink
    {
        public string LinkType { get; set; }
        public string LinkURL { get; set; }
        public string LinkTitle { get; set; }
        public long? ListingId { get; set; }
        public long LinkId { get; set; }
    }
}
