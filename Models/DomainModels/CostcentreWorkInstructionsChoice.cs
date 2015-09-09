using System;
namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Cost Centre Work Instructions Choice Domain Model
    /// </summary>
    /// 
    [Serializable()]
    public class CostcentreWorkInstructionsChoice
    {
        public long Id { get; set; }
        public string Choice { get; set; }
        public long InstructionId { get; set; }

        public virtual CostcentreInstruction CostcentreInstruction { get; set; }
    }
}
