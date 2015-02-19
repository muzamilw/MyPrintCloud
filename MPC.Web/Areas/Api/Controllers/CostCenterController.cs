﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class CostCenterController : ApiController
    {
        #region Private
        private readonly ICostCentersService _costCenterService;
        #endregion


        #region Constructor

       /// <summary>
       /// 
       /// </summary>
       /// <param name="costCenterService"></param>
        public CostCenterController(ICostCentersService costCenterService)
        {
            this._costCenterService = costCenterService;
        }
        #endregion

        #region Public

        public CostCenterResponse GetCostCentersList([FromUri] CostCenterRequestModel request)
        {
            var result = _costCenterService.GetUserDefinedCostCenters(request);
            return new CostCenterResponse
            {
                CostCenters = result.CostCenters.Select(s => s.ListViewModelCreateFrom()),
                RowCount = result.RowCount
            };
        }
        public CostCentre Get(int id)
        {
            return _costCenterService.GetCostCentreById(id).CreateFrom();
        }

        public CostCentre Post(CostCentre costcenter)
        {
            if (ModelState.IsValid)
            {
                return _costCenterService.Update(costcenter.CreateFrom()).CreateFrom();
            }
            throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
        }

       
        #endregion
    }
}