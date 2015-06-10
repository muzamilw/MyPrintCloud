using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
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
        [CompressFilterAttribute]
        public ItemSearchResponse Get([FromUri]ItemSearchRequestModel request)
        {
            var response = itemService.GetItemsByCompanyId(request);
            ItemSearchResponse itemSearchResponse = new ItemSearchResponse
            {
                TotalCount = response.TotalCount,
                Items = response.Items.Select(x => x.CreateFromForOrderAddProduct())
            };
            return itemSearchResponse;
        }
        #endregion
    }
}