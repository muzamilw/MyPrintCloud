using System.Collections.Generic;
namespace MPC.MIS.Areas.Api.Models
{
    public class CostcentreInstruction
    {
        public long InstructionId { get; set; }
        public string Instruction { get; set; }
        public long? CostCentreId { get; set; }
        public int? CostCenterOption { get; set; }

        public virtual CostCentre CostCentre { get; set; }
        public ICollection<CostcentreWorkInstructionsChoice> CostcentreWorkInstructionsChoices { get; set; }
    }
}