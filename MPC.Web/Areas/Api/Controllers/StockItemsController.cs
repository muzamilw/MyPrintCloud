using System;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using System.Collections.Generic;
using System.Linq;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Stock Items API Controller
    /// </summary>
    public class StockItemsController : ApiController
    {
        #region Private

        private readonly IItemService itemService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public StockItemsController(IItemService itemService)
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
        /// Get Stock Items
        /// </summary>
        public InventorySearchResponse Get([FromUri] StockItemRequestModel request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return itemService.GetStockItems(request).CreateFrom();
        }
        public IEnumerable<StockItem> GetAllStockItemList([FromUri] long stockID)
        {
            //return new StockItemResponse
            //{
            //    StockItems = 
            //};
            return itemService.GetAllStockItemList().Select(g => g.CreateFromDetail());
        }

       
        
        #endregion
    }
}