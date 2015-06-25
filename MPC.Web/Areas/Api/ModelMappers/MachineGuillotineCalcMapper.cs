using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel = MPC.Models.DomainModels;
using ApiModel = MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class MachineGuillotineCalcMapper
    {
        public static DomainModel.MachineGuillotineCalc CreateFrom(this ApiModel.MachineGuillotineCalc source)
        {
            return new DomainModel.MachineGuillotineCalc
            {
                Id = source.Id,
                MethodId = source.MethodId,
                PaperWeight1 = source.PaperWeight1,
                PaperThroatQty1 = source.PaperThroatQty1,
                PaperWeight2 = source.PaperWeight2,
                PaperThroatQty2 = source.PaperThroatQty2,
                PaperWeight3 = source.PaperWeight3,
                PaperThroatQty3 = source.PaperThroatQty3,
                PaperWeight4 = source.PaperWeight4,
                PaperThroatQty4 = source.PaperThroatQty4,
                PaperWeight5 = source.PaperWeight5,
                PaperThroatQty5 = source.PaperThroatQty5
               
            };
        }
        public static ApiModel.MachineGuillotineCalc CreateFrom(this DomainModel.MachineGuillotineCalc source)
        {
            return new ApiModel.MachineGuillotineCalc
            {
                Id = source.Id,
                MethodId = source.MethodId,
                PaperWeight1 = source.PaperWeight1,
                PaperThroatQty1 = source.PaperThroatQty1,
                PaperWeight2 = source.PaperWeight2,
                PaperThroatQty2 = source.PaperThroatQty2,
                PaperWeight3 = source.PaperWeight3,
                PaperThroatQty3 = source.PaperThroatQty3,
                PaperWeight4 = source.PaperWeight4,
                PaperThroatQty4 = source.PaperThroatQty4,
                PaperWeight5 = source.PaperWeight5,
                PaperThroatQty5 = source.PaperThroatQty5

            };
        }
    }
}