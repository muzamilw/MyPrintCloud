using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    public interface IMachineRepository : IBaseRepository<Machine, int>
    {
        MachineListResponseModel GetAllMachine(MachineRequestModel request);
        //IEnumerable<LookupMethod> GetAllLookupMethodList();
        //bool Delete(long MachineID);
        // bool UpdateSystemMachine(long MachineID, string Name, string Description);
        MachineResponseModel GetMachineByID(long MachineID);
        bool archiveMachine(long id);
        //   List<Machine> GetMachineList();
        MachineSearchResponse GetMachinesForProduct(MachineSearchRequestModel request);

        List<Machine> GetMachinesByOrganisationID(long OID);

        List<LookupMethod> getLookupmethodsbyOrganisationID(long OID);
    }
}


