namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Listing Agent Domain Model
    /// </summary>
    public class ListingAgent
    {
        public long AgentId { get; set; }
        public long? MemberId { get; set; }
        public int? AgentOrder { get; set; }
        public long? ListingId { get; set; }
        public string UserRef { get; set; }
        public string Name { get; set; }
        public bool? Admin { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Mobile { get; set; }
        public bool? Deleted { get; set; }
    }
}
