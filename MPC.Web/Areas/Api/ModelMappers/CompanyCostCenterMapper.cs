using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CompanyCostCenterMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static CompanyCostCentre CreateFrom(this DomainModels.CompanyCostCentre source)
        {
            return new CompanyCostCentre
            {
                CompanyCostCenterId = source.CompanyCostCenterId,
                CompanyId = source.CompanyId,
                CostCentreId = source.CostCentreId,
                BrokerMarkup = source.BrokerMarkup,
                ContactMarkup = source.ContactMarkup,
                isDisplayToUser = source.isDisplayToUser,
                OrganisationId = source.OrganisationId,
            };
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static DomainModels.CompanyCostCentre CreateFrom(this CompanyCostCentre source)
        {
            return new DomainModels.CompanyCostCentre
            {
                CompanyCostCenterId = source.CompanyCostCenterId,
                CompanyId = source.CompanyId,
                CostCentreId = source.CostCentreId,
                BrokerMarkup = source.BrokerMarkup,
                ContactMarkup = source.ContactMarkup,
                isDisplayToUser = source.isDisplayToUser,
                OrganisationId = source.OrganisationId,
            };
        }

        public static DomainModels.CompanyCostCentre CreateFrom(this CostCentreDropDown source)
        {
            return new DomainModels.CompanyCostCentre
            {
                CostCentreId = source.CostCentreId,
            };
        }
    }
}