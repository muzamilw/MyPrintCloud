using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GrapeCity.ActiveReports.PageReportModel;
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

        [CompressFilterAttribute]
        public OrderRetailItemDetail Get([FromUri] int itemId)
        {
            Item item = itemService.GetById(itemId);
            return new OrderRetailItemDetail
            {
                ItemPriceMatrices = (item.ItemPriceMatrices != null && item.ItemPriceMatrices.Count > 0) ? item.ItemPriceMatrices.Where(price => !price.SupplierId.HasValue && price.FlagId == item.FlagId).Select(x => x.CreateFrom())
                : new List<ItemPriceMatrix>(),
                ItemStockOptions = (item.ItemStockOptions != null && item.ItemStockOptions.Count > 0) ? item.ItemStockOptions.Select(x => x.CreateFrom()) : new List<ItemStockOption>(),
                ItemSection = (item.ItemSections != null && item.ItemSections.Count > 0) ? item.ItemSections.FirstOrDefault().CreateFrom() : new ItemSection()
            };
        }
    }
}