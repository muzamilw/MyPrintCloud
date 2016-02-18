using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Company Contact Variable Repository
    /// </summary>
    public interface IScopeVariableRepository : IBaseRepository<ScopeVariable, long>
    {

        /// <summary>
        /// Get Company Contact Variable  By Contact ID
        /// </summary>
        IEnumerable<ScopeVariable> GetContactVariableByContactId(long contactId,int scope);
        void AddScopeVariables(long ContactId, long CompanyId);
    }
}
