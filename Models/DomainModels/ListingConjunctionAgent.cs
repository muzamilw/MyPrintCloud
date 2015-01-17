namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Listing Conjunction Agent Domain Model
    /// </summary>
    public class ListingConjunctionAgent
    {
        public long ConjunctionAgentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public long? ListingId { get; set; }
    }
}
