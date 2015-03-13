namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Ink Coverage Group Domain Model
    /// </summary>
    public class InkCoverageGroup
    {
        public int CoverageGroupId { get; set; }
        public string GroupName { get; set; }
        public double? Percentage { get; set; }
        public int IsFixed { get; set; }
        public int OrganisationId { get; set; }
    }
}
