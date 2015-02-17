﻿using System;
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
        public bool UpdateMachine(Machine machine, IEnumerable<MachineSpoilage> MachineSpoilages)
        {
            return _machineRepository.UpdateMachine(machine, MachineSpoilages);
        }
        public bool archiveMachine(long machineId)
        {
            return _machineRepository.archiveMachine(machineId);
        }
        public MachineResponseModel GetMachineById(long id)
        {
           return _machineRepository.GetMachineByID(id);
        }

        public MachineListResponseModel GetAll(MachineRequestModel request)
        {
            return _machineRepository.GetAllMachine(request);
        }
        
        #endregion
    }
}
