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
    public class ExportPurchaseOrderController : ApiController
    {
        #region Private

        private readonly IPurchaseService _purchaseService;

        #endregion
        #region constructor

        public ExportPurchaseOrderController(IPurchaseService purchaseService)
        {
            if (purchaseService == null)
            {
                throw new ArgumentNullException("purchaseService");
            }

            this._purchaseService = purchaseService;
        }
        #endregion
        [ApiException]
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewOrder })]
        [CompressFilterAttribute]
        public string Get(long purchaseId)
        {
            return _purchaseService.ExportPurchaseOrderToCsv(purchaseId);
        }

        
    }
}