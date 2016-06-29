using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class ClickChargeZoneController : ApiController
    {
        #region Private
        private readonly ICostCentersService _costCenterService;
        #endregion
        #region Constructor

        public ClickChargeZoneController(ICostCentersService costCenterService)
        {
            if (costCenterService == null)
            {
                throw new ArgumentNullException("costCenterService");
            }

            this._costCenterService = costCenterService;
        }
        #endregion

        // POST api/<controller>
        [ApiException]
        [CompressFilterAttribute]
        public MachineClickChargeZone Post(MachineClickChargeZone value)
        {
            return _costCenterService.SaveClickChargeZone(value.CreateFrom()).CreateFrom();
        }
        [ApiException]
        [CompressFilterAttribute]
        public Boolean Delete(MachineClickChargeZone value)
        {
            return _costCenterService.DeleteClickChargeZone(value.Id);
        }
       
        
    }
}