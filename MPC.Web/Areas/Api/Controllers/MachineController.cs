﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class MachineController : ApiController
    {
        #region Private
        private readonly IMachineService _machineService;
        #endregion


        #region Constructor
        public MachineController(IMachineService machineService)
        {

            this._machineService = machineService;
        }
        #endregion


        #region Public
        public MachineResponse Get(long id)
        {
            return _machineService.GetMachineById(id).CreateFrom() ;
            
           
        }

        public MachineListResponse GetMachineList([FromUri] MachineRequestModel request)
        {
            var result = _machineService.GetAll(request);
            return new MachineListResponse
            {
                machine = result.MachineList.Select(s => s.ListViewModelCreateFrom(result.lookupMethod)),
                RowCount = result.RowCount
            };
        }


       
        #endregion
    }
}