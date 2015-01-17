using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// ItemPriceMatrix API Controller
    /// </summary>
    public class ItemPriceMatrixController : ApiController
    {
        #region Private

        private readonly IItemService itemService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemPriceMatrixController(IItemService itemService)
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
        /// Get All ItemPriceMatrices For Item By Flag
        /// </summary>
        public IEnumerable<ItemPriceMatrix> Get([FromUri] ItemPriceMatrixSearchRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return itemService
                        .GetItemPriceMatricesBySectionFlagForItem(request.FlagId, request.ItemId)
                        .Select(priceMatrix => priceMatrix.CreateFrom());
        }
        
        #endregion
    }
}