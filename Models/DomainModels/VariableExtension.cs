namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Variable Extension Domain Model
    /// </summary>
    public class VariableExtension
    {
        public int Id { get; set; }
        public long? FieldVariableId { get; set; }
        public int? CompanyId { get; set; }
        public int? OrganisationId { get; set; }
        public string VariablePrefix { get; set; }
        public string VariablePostfix { get; set; }
        public bool? CollapsePrefix { get; set; }
        public bool? CollapsePostfix { get; set; }
        public virtual FieldVariable FieldVariable { get; set; }
    }
}
