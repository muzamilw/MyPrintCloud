
namespace MPC.MIS.Areas.Api.Models
{
    public class CostcentreWorkInstructionsChoice
    {
        public long Id { get; set; }
        public string Choice { get; set; }
        public long InstructionId { get; set; }
        public virtual CostcentreInstruction CostcentreInstruction { get; set; }
    }
}