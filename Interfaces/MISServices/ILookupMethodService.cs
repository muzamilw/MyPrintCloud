using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
namespace MPC.Interfaces.MISServices
{
    public interface ILookupMethodService
    {
        IEnumerable<LookupMethod> GetAll();
        LookupMethodResponse GetlookupById(long MethodId);
        bool UpdateLookup(LookupMethodResponse response);
        
    }
}
