using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.Common;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    public interface IMachineRepository : IBaseRepository<Machine, int>
    {
        MachineResponseModel GetAllMachine(MachineRequestModel request);
        //bool Delete(long MachineID);
        // bool UpdateSystemMachine(long MachineID, string Name, string Description);
        Machine GetMachineByID(long MachineID);
        //   List<Machine> GetMachineList();
        MachineSearchResponse GetMachinesForProduct(MachineSearchRequestModel request);
    }
}


