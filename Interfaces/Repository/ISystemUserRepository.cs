using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    public interface ISystemUserRepository : IBaseRepository<SystemUser, long>
    {
        IEnumerable<SystemUser> GetAll();
        SystemUser GetSalesManagerById(long SytemUserId);
    }
}
