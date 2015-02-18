using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CostcentreInstructionMapper
    {
        public static CostcentreInstruction CreateFrom(this MPC.Models.DomainModels.CostcentreInstruction source)
        {
            return new CostcentreInstruction
            {
                InstructionId = source.InstructionId,
                Instruction = source.Instruction,
                CostCentreId = source.CostCentreId,
                CostCenterOption = source.CostCenterOption
                
            };
        }

        public static MPC.Models.DomainModels.CostcentreInstruction CreateFrom(this CostcentreInstruction source)
        {
            return new MPC.Models.DomainModels.CostcentreInstruction
            {
                InstructionId = source.InstructionId,
                Instruction = source.Instruction,
                CostCentreId = source.CostCentreId,
                CostCenterOption = source.CostCenterOption
            };
        }
    }
}