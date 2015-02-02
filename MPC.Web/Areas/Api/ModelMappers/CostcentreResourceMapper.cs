using System.Linq;
using System.Security.Cryptography.X509Certificates;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CostcentreResourceMapper
    {
        public static CostcentreResource CreateFrom(this MPC.Models.DomainModels.CostcentreResource source)
        {
            return new CostcentreResource
            {
                CostCentreId = source.CostCentreId,
                CostCenterResourceId = source.CostCenterResourceId
            };
        }

        public static MPC.Models.DomainModels.CostcentreResource CreateFrom(this CostcentreResource source)
        {
            return new MPC.Models.DomainModels.CostcentreResource
            {
                CostCentreId = source.CostCentreId,
                CostCenterResourceId = source.CostCenterResourceId
            };
        }
    }
}