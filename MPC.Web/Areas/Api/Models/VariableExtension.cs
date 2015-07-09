namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Variable Extension API Model
    /// </summary>
    public class VariableExtension
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public long? OrganisationId { get; set; }
        public string VariablePrefix { get; set; }
        public string VariablePostfix { get; set; }
        public bool? CollapsePrefix { get; set; }
        public bool? CollapsePostfix { get; set; }
    }
}