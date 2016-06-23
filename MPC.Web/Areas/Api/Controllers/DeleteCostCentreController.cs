using MPC.ExceptionHandling;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class DeleteCostCentreController :  ApiController
    {
         #region Private

        private readonly ICostCentersService costCentreService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DeleteCostCentreController(ICostCentersService costCentreService)
        {
            this.costCentreService = costCentreService;
        }

        #endregion

        [ApiException]
        [CompressFilterAttribute]
        public long Post(CostCentre costcenter)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return costCentreService.CopyCostCenter(costcenter.CreateFrom());

                }
                catch (Exception exception)
                {
                    throw new MPCException(exception.Message, 0);
                }

            }
            throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
        }


        public bool Delete(CostCentreDeleteModel costcentre)
        {
            if (costcentre.CostCentreId == 0 || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            costCentreService.DeleteCostCentre(costcentre.CostCentreId);
            return true;
        }

    }
}