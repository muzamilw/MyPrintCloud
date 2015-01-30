using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Machine Repository 
    /// </summary>
    public interface IMachineRepository : IBaseRepository<Machine, int>
    {
        /// <summary>
        /// Get Machines For Product
        /// </summary>
        MachineSearchResponse GetMachinesForProduct(MachineSearchRequestModel request);
    }
}
