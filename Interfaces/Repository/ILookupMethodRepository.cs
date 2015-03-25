using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    public interface ILookupMethodRepository: IBaseRepository<LookupMethod, long>
    {
        LookupMethodResponse GetlookupById(long MethodId);
        bool UpdateLookup(LookupMethodResponse response);
    }
}
