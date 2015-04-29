using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.ResponseModels;

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
        public ItemJobStatusResponse Get()
        {
            return new ItemJobStatusResponse
            {
                Items = itemJobStatusService.GetItemsForItemJobStatus(),
                CurrencySymbol = itemJobStatusService.GetCurrencySymbol()
            };

        }


        /// <summary>
        /// Save Item
        /// </summary>
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