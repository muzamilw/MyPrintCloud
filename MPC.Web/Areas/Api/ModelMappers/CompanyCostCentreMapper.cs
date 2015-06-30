using MPC.MIS.Areas.Api.Models;
using System.Linq;

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
                PricePerUnitQuantity = costCentre.PricePerUnitQuantity,
                DeliveryCharges = costCentre.DeliveryCharges
            };
        }
        /// <summary>
        /// Company Cost Centre to Cost Centre [Domain to Web]
        /// </summary>
        public static CostCentre CreateFromServerForProduct(this MPC.Models.DomainModels.CostCentre source)
        {
            
          return new CostCentre
            {
                Name = source.Name,
                CostCentreId = source.CostCentreId,
                Description = source.Description,
                SetupCost = source.SetupCost,
                PricePerUnitQuantity = source.PricePerUnitQuantity,
                Type = source.CalculationMethodType,
                QuantitySourceType = source.QuantitySourceType,
                TimeSourceType = source.TimeSourceType
            };
        }

        public static CostCentreResponseModel CreateFrom(this MPC.Models.ResponseModels.CostCentreResponse source)
        {
            return new CostCentreResponseModel
            {
                RowCount= source.RowCount,
                CostCentres= source.CostCentres.Select(obj => obj.CreateFromServer())
            };
        }
        public static CostCentreResponseModel CreateFromForProducts(this MPC.Models.ResponseModels.CostCentreResponse source)
        {
            return new CostCentreResponseModel
            {
                RowCount= source.RowCount,
                CostCentres = source.CostCentresForproducts.Select(obj => obj.CreateFromServerForProduct())
            };
        }
    }
}