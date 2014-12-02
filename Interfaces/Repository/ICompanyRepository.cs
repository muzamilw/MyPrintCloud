using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    public interface ICompanyRepository : IBaseRepository<Company, long>
    {
        Company GetCompanyById(long companyId);

        long GetCompanyIdByDomain(string domain);
        CompanyResponse SearchCompanies(CompanyRequestModel request);

    }
}
