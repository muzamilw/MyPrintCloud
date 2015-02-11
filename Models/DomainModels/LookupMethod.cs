namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Lookup Method Domain Model
    /// </summary>
    public class LookupMethod
    {
        public long MethodId { get; set; }
        public string Name { get; set; }
        public long? Type { get; set; }
        public int? LockedBy { get; set; }
        public int OrganisationId { get; set; }
        public int? FlagId { get; set; }
        public int SystemSiteId { get; set; }
    }
}
