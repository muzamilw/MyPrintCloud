using System;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using  MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// CostCenterTreeController
    /// </summary>
    public class CostCenterTreeController : ApiController
    {
        #region Private

        private readonly ICostCentersService _costCentersService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CostCenterTreeController(ICostCentersService costCenterService)
        {
            if (costCenterService == null)
            {
                throw new ArgumentNullException("costCenterService");
            }

            this._costCentersService = costCenterService;
        }

        #endregion

        #region Public
       
        //public Models.CostCenterVariablesResponseModel Get()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
        //    }

        //    return _costCentersService.GetCostCenterVariablesTree().CreateFrom();
        //}
        public Models.CostCenterVariablesResponseModel GetListById(int Id)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            return _costCentersService.GetCostCenterVariablesTree(Id).CreateFrom();
        }

        #endregion
    }
}