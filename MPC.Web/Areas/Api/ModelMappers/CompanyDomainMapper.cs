using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CompanyDomainMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static CompanyDomain CreateFrom(this DomainModels.CompanyDomain source)
        {
            return new CompanyDomain
            {
                CompanyDomainId = source.CompanyDomainId,
                Domain = source.Domain,
                CompanyId = source.CompanyId
            };
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static DomainModels.CompanyDomain CreateFrom(this CompanyDomain source)
        {
            return new DomainModels.CompanyDomain
            {
                CompanyDomainId = source.CompanyDomainId,
                Domain = source.Domain,
                CompanyId = source.CompanyId
            };
        }
    }
}