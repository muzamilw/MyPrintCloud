using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Common;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.WebStoreServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.Common;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;
using Newtonsoft.Json;


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
            var updatedSection = itemsectionService.GetUpdatedSectionWithSystemCostCenters(request.CreateFromForOrder()).CreateFromForOrder();
           
            return updatedSection;
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
