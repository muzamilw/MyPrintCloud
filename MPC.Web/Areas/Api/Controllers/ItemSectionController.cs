using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class ItemSectionController : ApiController
    {
        private readonly IItemSectionService itemsectionService;

        #region Constructor
        public ItemSectionController(IItemSectionService _itemsectionService)
        {
            if (_itemsectionService == null)
            {
                throw new ArgumentNullException("itemsectionService");
            }
            this.itemsectionService = _itemsectionService;
        }
        #endregion
        #region Public
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        [CompressFilterAttribute]
        public ItemSection Post(ItemSection request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return itemsectionService.GetUpdatedSectionWithSystemCostCenters(request.CreateFromForOrder()).CreateFromForOrder();
        }

        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        [CompressFilterAttribute]
        public bool Get(ItemSection request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            return itemsectionService.SaveItemSection(request.CreateFromForOrder());
        }

        #endregion
    }
}
