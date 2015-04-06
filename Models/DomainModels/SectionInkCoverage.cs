namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Section Ink Coverage Domain Model
    /// </summary>
    public class SectionInkCoverage
    {
        public int Id { get; set; }
        public int? SectionId { get; set; }
        public int? InkOrder { get; set; }
        public int? InkId { get; set; }
        public int? CoverageGroupId { get; set; }
        public int? Side { get; set; }
    }
}
