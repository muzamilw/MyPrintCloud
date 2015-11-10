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
        MachineResponseModel CreateMachineByType(bool isGuillotine);
        bool archiveMachine(long id);
        bool UpdateMachine(Machine machine, MachineClickChargeZone ClickCharge, MachineMeterPerHourLookup MeterPerHour, MachineGuillotineCalc GuillotineLookup, IEnumerable<MachineGuilotinePtv> GuillotinePtv,int type);
        long AddMachine(Machine machine, MachineClickChargeZone ClickChargeZone, MachineMeterPerHourLookup MeterPerHour, MachineGuillotineCalc GuillotineLookup, IEnumerable<MachineGuilotinePtv> GuillotinePtv,int Type);
        //   List<Machine> GetMachineList();
        MachineSearchResponse GetMachinesForProduct(MachineSearchRequestModel request);

        List<Machine> GetMachinesByOrganisationID(long OID);

        List<LookupMethod> getLookupmethodsbyOrganisationID(long OID);

        string GetInkPlatesSidesByInkID(long InkID);

        string GetMachineByID(int MachineID);
        Machine GetDefaultGuillotine();

        List<MachineGuilotinePtv> getGuilotinePtv(long GuilotineId);
    }
}


