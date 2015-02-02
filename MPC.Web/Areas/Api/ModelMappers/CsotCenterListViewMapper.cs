﻿using System.IO;
using ApiModels = MPC.MIS.Areas.Api.Models;
using DomainResponseModel = MPC.Models.ResponseModels;
using DomainModels = MPC.Models.DomainModels;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CsotCenterListViewMapper
    {
        public static ApiModels.CostCenterListViewModel ListViewModelCreateFrom(this DomainModels.CostCentre source)
        {
            
            return new ApiModels.CostCenterListViewModel
            {
                CostCentreId = source.CostCentreId,
                Name = source.Name,
                Description = source.Description,
                Type = source.CostCentreType.TypeName,
                CalculationMethodType = source.CalculationMethodType
            };
        }
    }
}