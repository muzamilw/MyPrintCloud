using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.Webstore.Models;

namespace MPC.Webstore.ModelMappers
{
    public class CompanyDomainMapper
    {
        public static ApiModels.CompanyDomain CreateFrom(DomainModels.CompanyDomain source)
        {
            return new ApiModels.CompanyDomain
            {
                CompanyDomainId = source.CompanyDomainId,
                Domain = source.Domain,
                CompanyId = source.CompanyId,
            };
        }
    }
}