using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.ExceptionHandling;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;

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

        [CompressFilterAttribute]
        public CostCenterResponse GetCostCentersList([FromUri] CostCenterRequestModel request)
        {
            var result = _costCenterService.GetUserDefinedCostCenters(request);
            return new CostCenterResponse
            {
                CostCenters = result.CostCenters.Select(s => s.ListViewModelCreateFrom()),
                RowCount = result.RowCount
            };
        }
        [CompressFilterAttribute]
        public CostCentre Get(int id)
        {
            return _costCenterService.GetCostCentreById(id).CreateFrom();
        }
        [ApiException]
        [CompressFilterAttribute]
        public CostCentre Post(CostCentre costcenter)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (costcenter.CostCentreId <= 0)
                    {
                        return _costCenterService.Add(costcenter.CreateFrom()).CreateFrom();
                    }
                    else
                    {
                        return _costCenterService.Update(costcenter.CreateFrom()).CreateFrom();
                    }

                }
                catch (Exception exception)
                {
                    throw new MPCException(exception.Message, 0);
                }
                
            }
            throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
        }


        public bool Get(int id,int id2)
        {
            return _costCenterService.ReCompileAllCostCentres(id);
        }

       
        #endregion
    }
}