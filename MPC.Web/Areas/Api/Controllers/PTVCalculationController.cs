using System;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class PtvCalculationController : ApiController
    {
        #region Private

        private readonly IItemSectionService itemsectionService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public PtvCalculationController(IItemSectionService _itemsectionService)
        {
            if (_itemsectionService == null)
            {
                throw new ArgumentNullException("orderService");
            }
            this.itemsectionService = _itemsectionService;
        }

        #endregion

        #region Public
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        [CompressFilterAttribute]
        public PtvDTO Get([FromUri] MPC.Models.RequestModels.PTVRequestModel request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return itemsectionService.GetPTVCalculation(request).CreateFrom();
        }
        #endregion
    }
}