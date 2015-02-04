using System;
using System.Collections.Generic;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    class MachineService : IMachineService
    {
        #region Private

        private readonly IMachineRepository _machineRepository;

        #endregion
        #region Constructor

        public MachineService(IMachineRepository machineRepository)
        {
            this._machineRepository = machineRepository;
        }

        #endregion
        #region Public
        public Machine Add(Machine machine)
        {
            return machine;
        }
        public Machine Update(Machine machine)
        {
            return machine;
        }
        public bool Delete(long machineId)
        {
            return true;
        }
        public Machine GetMachineById(long id)
        {
           return _machineRepository.GetMachineByID(id);
        }

        public MachineResponseModel GetAll(MachineRequestModel request)
        {
            return _machineRepository.GetAllMachine(request);
        }
        public IEnumerable<LookupMethod> GetAllLookupMethod()
        {
            return _machineRepository.GetAllLookupMethodList();
        }
        #endregion
    }
}
