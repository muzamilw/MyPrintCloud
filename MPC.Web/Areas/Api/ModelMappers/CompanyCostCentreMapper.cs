using MPC.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CompanyCostCentreMapper
    {
        /// <summary>
        /// Company Cost Centre to Cost Centre [Domain to Web]
        /// </summary>
        public static CostCentre CreateFromServer(this MPC.Models.DomainModels.CompanyCostCentre source)
        {
            MPC.Models.DomainModels.CostCentre costCentre= source.CostCentre;
          return new CostCentre
            {
                Name = costCentre.Name,
                CostCentreId = costCentre.CostCentreId,
                Description = costCentre.Description,
                SetupCost = costCentre.SetupCost,
                PricePerUnitQuantity = costCentre.PricePerUnitQuantity
            };
        }

        public static MPC.MIS.Areas.Api.Models.CostCentreResponseModel CreateFrom(this MPC.Models.ResponseModels.CostCentreResponse source)
        {
            return new MPC.MIS.Areas.Api.Models.CostCentreResponseModel
            {
                RowCount= source.RowCount,
                CostCentres= source.CostCentres.Select(obj => obj.CreateFromServer())
            };
        }
    }
}