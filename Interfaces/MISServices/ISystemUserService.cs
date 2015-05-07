using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.MISServices
{
    public interface ISystemUserService
    {
        bool Update(System.Guid Id, string Email, string FullName, int OrganizationId);
        bool Add(System.Guid Id, string Email, string FullName, int OrganizationId);
    }
}
