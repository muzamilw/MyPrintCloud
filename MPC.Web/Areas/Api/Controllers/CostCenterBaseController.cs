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
    /// Item Base API Controller
    /// </summary>
    public class CostCenterBaseController : ApiController
    {
        #region Private

        private readonly ICostCentersService _costCentersService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CostCenterBaseController(ICostCentersService costCenterService)
        {
            if (costCenterService == null)
            {
                throw new ArgumentNullException("costCenterService");
            }

            this._costCentersService = costCenterService;
        }

        #endregion

        #region Public
       /// <summary>
        /// CostCenterBaseResponse
       /// </summary>
       /// <returns></returns>
        public Models.CostCenterBaseResponse Get()
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            return _costCentersService.GetBaseData().CreateFrom();
        }

        #endregion
    }
}