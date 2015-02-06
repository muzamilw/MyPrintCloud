using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    public interface ICostCenterTypeRepository : IBaseRepository<CostCentreType, long>
    {
        IEnumerable<CostCentreType> GetAll();
        CostCentreType GetCostCenterTypeById(int Id);
    }
}
