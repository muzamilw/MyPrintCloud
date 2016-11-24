using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class ProductTemplateNotificationController : ApiController
    {
         #region Private
        private readonly IItemService _itemService;
        #endregion

        #region Constructor

        public ProductTemplateNotificationController(IItemService itemService)
        {
            if (itemService == null)
            {
                throw new ArgumentNullException("itemService");
            }

            this._itemService = itemService;
        }
        #endregion
        #region Public

        
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewStore })]
        [CompressFilterAttribute]
        public void Get(long id)
        {
            _itemService.ProductTemplateNotificationEmail(id);
        }

        
        #endregion
        
    }
}