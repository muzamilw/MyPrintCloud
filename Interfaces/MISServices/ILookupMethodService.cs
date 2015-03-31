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
        LookupMethodListResponse GetAll();
        LookupMethodResponse GetlookupById(long MethodId);
        bool UpdateLookup(LookupMethodResponse response);
        LookupMethod AddLookup(LookupMethodResponse response);

        bool DeleteMachineLookup(long id);
        bool DeleteGuillotinePTVId(long id);
    }
}
