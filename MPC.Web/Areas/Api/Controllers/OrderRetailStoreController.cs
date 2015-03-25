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
    public class OrderRetailStoreController : ApiController
    {
        #region Private

        private readonly IItemService itemService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public OrderRetailStoreController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        #endregion

        #region Public
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        public ItemSearchResponse Get([FromUri]int companyId)
        {
            IEnumerable<Item> items = itemService.GetItemsByCompanyId(companyId);
            ItemSearchResponse itemSearchResponse = new ItemSearchResponse
            {
                TotalCount = items.Count(),
                Items = items.Select(x => x.CreateFromForOrderAddProduct())
            };
            return itemSearchResponse;
        }
        #endregion
    }
}