using System;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.WebBase.Mvc;
using DomainResponseModel = MPC.Models.ResponseModels;
using APIModels = MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Product Machines API Controller
    /// </summary>
    public class ProductMachinesController : ApiController
    {
        #region Private

        private readonly IItemService itemService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductMachinesController(IItemService itemService)
        {
            if (itemService == null)
            {
                throw new ArgumentNullException("itemService");
            }

            this.itemService = itemService;
        }

        #endregion

        #region Public
        
        /// <summary>
        /// Get ProductMachines
        /// </summary>
        [CompressFilter]
        public APIModels.MachineSearchResponse Get([FromUri] MachineSearchRequestModel request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return itemService.GetMachines(request).CreateFrom();
        }

        #endregion
    }
}