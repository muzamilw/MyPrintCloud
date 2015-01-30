<<<<<<< HEAD
﻿using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.Common;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
namespace MPC.Interfaces.Repository
{
    public interface IMachineRepository
    {
        MachineResponseModel GetAllMachine(MachineRequestModel request);
        //bool Delete(long MachineID);
       // bool UpdateSystemMachine(long MachineID, string Name, string Description);
        Machine GetMachineByID(long MachineID);
      //   List<Machine> GetMachineList();
 
=======
﻿using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Machine Repository 
    /// </summary>
    public interface IMachineRepository : IBaseRepository<Machine, int>
    {
        /// <summary>
        /// Get Machines For Product
        /// </summary>
        MachineSearchResponse GetMachinesForProduct(MachineSearchRequestModel request);
>>>>>>> 3cb9509da1f9d30d38939aa30e82be032751d932
    }
}
