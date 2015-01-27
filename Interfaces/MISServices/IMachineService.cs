using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;


namespace MPC.Interfaces.MISServices
{
    public interface IMachineService
    {
        IEnumerable<Machine> GetAll(MachineRequestModel request);
        Machine Add(Machine machine);
        Machine Update(Machine machine);
        bool Delete(long machineId);
        Machine GetMachineById(long id);
        
        
    }
}
