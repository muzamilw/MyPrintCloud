using MPC.Interfaces.MISServices;
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