using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;


namespace MPC.Interfaces.MISServices
{
    public interface IMachineService
    {
        MachineListResponseModel GetAll(MachineRequestModel request);
        Machine Add(Machine machine);
        bool UpdateMachine(Machine machine, IEnumerable<MachineSpoilage> MachineSpoilages);
        bool archiveMachine(long machineId);
        MachineResponseModel GetMachineById(long id);
       // IEnumerable<LookupMethod> GetAllLookupMethod();

        
    }
}
