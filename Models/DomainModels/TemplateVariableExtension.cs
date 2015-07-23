
namespace MPC.Models.DomainModels
{
    public class TemplateVariableExtension
    {
        public int TemplateVariableExtId { get; set; }
        public int? TemplateId { get; set; }
        public bool? HasPrefix { get; set; }
        public bool? HasPostFix { get; set; }
    }
}
