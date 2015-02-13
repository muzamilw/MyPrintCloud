using System.Linq;
using MPC.WebBase.Mvc;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;

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
            return _machineService.GetMachineById(id).CreateFrom() ;
            
           
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

        public bool Delete(MachineDeleteRequest request)
        {
            return _machineService.archiveMachine(request.machineId);
          

        }
        
        [ApiException]
        public bool Post(MachineUpdateRequestModel request)
       // public Machine Post(Machine machine)
        
        {

            return _machineService.UpdateMachine(request.machine.CreateFrom());
           


        }
        #endregion
    }
}