using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Company Contact Variable Repository
    /// </summary>
    public interface ICompanyContactVariableRepository : IBaseRepository<CompanyContactVariable,long>
    {

        /// <summary>
        /// Get Company Contact Variable  By Contact ID
        /// </summary>
        IEnumerable<CompanyContactVariable> GetContactVariableByContactId(long contactId);
    }
}
