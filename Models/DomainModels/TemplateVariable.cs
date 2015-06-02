namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Template Variable
    /// </summary>
    public class TemplateVariable
    {
        public long ProductVariableId { get; set; }
        public long? TemplateId { get; set; }
        public long? VariableId { get; set; }
        public virtual FieldVariable FieldVariable { get; set; }
    }
}
