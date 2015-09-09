using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Models.ResponseModels;
using MPC.WebBase.Mvc;
using PurchaseResponseModel = MPC.MIS.Areas.Api.Models.PurchaseResponseModel;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class PurchaseController : ApiController
    {
        #region Private

        private readonly IPurchaseService purchaseService;
        #endregion
      
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public PurchaseController(IPurchaseService purchaseService)
        {
            this.purchaseService = purchaseService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Get Purchases
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewCrm })]
        public PurchaseResponseModel Get([FromUri] PurchaseRequestModel request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return purchaseService.GetPurchases(request).CreateFrom();
        }
        #endregion
    }
}