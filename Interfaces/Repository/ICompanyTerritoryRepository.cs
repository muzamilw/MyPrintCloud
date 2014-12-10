using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    public interface ICompanyTerritoryRepository:IBaseRepository<CompanyTerritory,long>
    {
        CompanyTerritoryResponse GetCompanyTerritory(CompanyTerritoryRequestModel request);
    }
}
