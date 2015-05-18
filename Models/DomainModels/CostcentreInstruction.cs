using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Cost Centre Instruction
    /// </summary>
    /// 
    [Serializable()]
    public class CostcentreInstruction
    {
        public long InstructionId { get; set; }
        public string Instruction { get; set; }
        public long? CostCentreId { get; set; }
        public int? CostCenterOption { get; set; }

        public virtual CostCentre CostCentre { get; set; }
        public virtual ICollection<CostcentreWorkInstructionsChoice> CostcentreWorkInstructionsChoices { get; set; }
    }
}
