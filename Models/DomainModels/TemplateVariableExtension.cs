
namespace MPC.Models.DomainModels
{
    public class TemplateVariableExtension
    {
        public long TemplateVariableExtId { get; set; }
        public long? TemplateId { get; set; }
        public long? FieldVariableId { get; set; }
        public bool? HasPrefix { get; set; }
        public bool? HasPostFix { get; set; }
    }
}
