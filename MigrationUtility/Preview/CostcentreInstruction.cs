//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility.Preview
{
    using System;
    using System.Collections.Generic;
    
    public partial class CostcentreInstruction
    {
        public CostcentreInstruction()
        {
            this.CostcentreWorkInstructionsChoices = new HashSet<CostcentreWorkInstructionsChoice>();
        }
    
        public long InstructionId { get; set; }
        public string Instruction { get; set; }
        public Nullable<long> CostCentreId { get; set; }
        public Nullable<int> CostCenterOption { get; set; }
    
        public virtual CostCentre CostCentre { get; set; }
        public virtual ICollection<CostcentreWorkInstructionsChoice> CostcentreWorkInstructionsChoices { get; set; }
    }
}