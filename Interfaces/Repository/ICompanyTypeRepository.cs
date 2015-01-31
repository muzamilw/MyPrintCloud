using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Company Type Repository
    /// </summary>
    public interface ICompanyTypeRepository : IBaseRepository<CompanyType, long>
    {
        /// <summary>
        /// Get All Company Types For Campaign
        /// </summary>
        IEnumerable<CompanyType> GetAllForCampaign();
    }
}
