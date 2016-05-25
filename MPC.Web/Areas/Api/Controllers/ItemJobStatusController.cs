using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Item Job Status Api Controller 
    /// </summary>
    public class ItemJobStatusController : ApiController
    {
        #region Private

        private readonly IItemJobStatusService itemJobStatusService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemJobStatusController(IItemJobStatusService itemJobStatusService)
        {
            this.itemJobStatusService = itemJobStatusService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Get Items For Item Job Status
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewProductionBoard })]
        [CompressFilterAttribute]
        public ItemJobStatusResponse Get([FromUri] ItemJobRequestModel request)
        {
          IEnumerable<ItemForItemJobStatus> items = request.IsLateItemScreen ? itemJobStatusService.GetItemsForLateItems() :
          itemJobStatusService.GetItemsForItemJobStatus();
            var boardLabels = itemJobStatusService.GetProductionBoardLabels();
          return new ItemJobStatusResponse
            {
                Items = items,
                CurrencySymbol = itemJobStatusService.GetCurrencySymbol(),
                ProductionBoardLabel1 = boardLabels != null ? boardLabels.FirstOrDefault(a => a.Key == 1).Value : string.Empty,
                ProductionBoardLabel2 = boardLabels != null ? boardLabels.FirstOrDefault(a => a.Key == 2).Value : string.Empty,
                ProductionBoardLabel3 = boardLabels != null ? boardLabels.FirstOrDefault(a => a.Key == 3).Value : string.Empty,
                ProductionBoardLabel4 = boardLabels != null ? boardLabels.FirstOrDefault(a => a.Key == 4).Value : string.Empty,
                ProductionBoardLabel5 = boardLabels != null ? boardLabels.FirstOrDefault(a => a.Key == 5).Value : string.Empty,
                ProductionBoardLabel6 = boardLabels != null ? boardLabels.FirstOrDefault(a => a.Key == 6).Value : string.Empty
                
            };
        }


        /// <summary>
        /// Save Item
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewProductionBoard })]
        [CompressFilterAttribute]
        public void Post(ItemForItemJobStatus item)
        {
            if (item == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            itemJobStatusService.UpdateItem(item);
        }

        #endregion
    }
}