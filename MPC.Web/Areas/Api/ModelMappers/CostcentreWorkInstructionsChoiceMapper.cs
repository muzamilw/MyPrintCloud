using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CostcentreWorkInstructionsChoiceMapper
    {
        public static CostcentreWorkInstructionsChoice CreatFrom(
            this MPC.Models.DomainModels.CostcentreWorkInstructionsChoice source)
        {
            return new CostcentreWorkInstructionsChoice
            {
                Choice = source.Choice,
                Id = source.Id,
                InstructionId = source.InstructionId
            };
        }

        public static MPC.Models.DomainModels.CostcentreWorkInstructionsChoice CreatFrom(
            this CostcentreWorkInstructionsChoice source)
        {
            return new MPC.Models.DomainModels.CostcentreWorkInstructionsChoice
            {
                Choice = source.Choice,
                Id = source.Id,
                InstructionId = source.InstructionId
            };
        }
    }
}