using System.Linq;
using MPC.WebBase.Mvc;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using System.Web;
using System.Net;

//using System;
//using System.Net;
//using System.Web;
//using System.Web.Http;



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
            MachineResponse MR= _machineService.GetMachineById(id).CreateFrom() ;

            return MR;
        }
        public MachineResponse Get([FromUri] bool IsGuillotine)
        {
            MachineResponse MR = _machineService.CreateMachineByType(IsGuillotine).CreateFrom();

            return MR;
        }
        public MachineListResponse Get([FromUri] MachineRequestModel request)
        {
            var result = _machineService.GetAll(request);
            return new MachineListResponse
            {
                machine = result.MachineList.Select(s => s.ListViewModelCreateFrom(result.lookupMethod)),
                RowCount = result.RowCount
            };
        }

        public long Put(MachineUpdateRequestModel request)
        {
            if (ModelState.IsValid)
            {
                return _machineService.AddMachine(request.machine.CreateFrom(),request.Type == 5 ? request.ClickChargeZone.CreateFrom() : null,request.Type == 8 ? request.MeterPerHourLookup.CreateFrom() : null,request.Type == 6 ? request.GuillotineCalc.CreateFrom() : null,request.Type == 6 ?  request.GuilotinePtv.Select(x => x.CreateFrom()) : null,request.Type);
            }
            throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
        }
        public bool Delete(MachineDeleteRequest request)
        {
            return _machineService.archiveMachine(request.machineId);
          

        }
        
        [ApiException]
        public bool Post(MachineUpdateRequestModel request)
        
        {


            return _machineService.UpdateMachine(request.machine.CreateFrom(), request.Type == 0 ? request.ClickChargeZone.CreateFrom() : null, request.Type == 1 ? request.MeterPerHourLookup.CreateFrom() : null, request.Type == 2 ? request.GuillotineCalc.CreateFrom() : null, request.Type == 2 ? request.GuilotinePtv.Select(x => x.CreateFrom()) : null, request.Type);
           


        }
        #endregion
    }
}