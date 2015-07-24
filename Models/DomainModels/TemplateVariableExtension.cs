
namespace MPC.Models.DomainModels
{
    public class TemplateVariableExtension
    {
        public int TemplateVariableExtId { get; set; }
        public int? TemplateId { get; set; }
        public int? FieldVariableId { get; set; }
        public bool? HasPrefix { get; set; }
        public bool? HasPostFix { get; set; }
    }
}
