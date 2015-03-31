using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.WebBase.Mvc;
using Item = MPC.Models.DomainModels.Item;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class OrderRetailStoreDetailController : ApiController
    {
        #region Private

        private readonly IItemService itemService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public OrderRetailStoreDetailController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        #endregion


        public OrderRetailItemDetail Get([FromUri] int itemId)
        {
            Item item = itemService.GetById(itemId);
            return new OrderRetailItemDetail
            {
                ItemPriceMatrices = item.ItemPriceMatrices.Select(x => x.CreateFrom()),
                ItemStockOptions = item.ItemStockOptions.Select(x => x.CreateFrom())
            };
        }
    }
}