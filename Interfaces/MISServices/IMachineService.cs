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
        bool UpdateMachine(Machine machine, MachineClickChargeZone ClickCharge, MachineMeterPerHourLookup MeterPerHour, MachineGuillotineCalc GuillotineLookup, IEnumerable<MachineGuilotinePtv> GuillotinePtv,int type, MachineSpeedWeightLookup speedWeightLookup);
        long AddMachine(Machine machine, MachineClickChargeZone ClickCharge, MachineMeterPerHourLookup MeterPerHour, MachineGuillotineCalc GuillotineLookup, IEnumerable<MachineGuilotinePtv> GuillotinePtv,int Type, MachineSpeedWeightLookup speedWeightLookup);
        
        bool archiveMachine(long machineId);
        MachineResponseModel GetMachineById(long id);
        MachineResponseModel CreateMachineByType(bool isGuillotine);
        
       // IEnumerable<LookupMethod> GetAllLookupMethod();

        
    }
}
