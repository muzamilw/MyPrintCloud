namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Activity Type Domain Model
    /// </summary>
    public class ActivityType
    {
        public int ActivityTypeId { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public string ActivityColor { get; set; }
    }
}
