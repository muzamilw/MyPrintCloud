using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{

    /// <summary>
    /// Purchase Order API Controller
    /// </summary>
    public class PurchaseOrderController : ApiController
    {
        #region Private

        private readonly IPurchaseService purchaseService;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public PurchaseOrderController(IPurchaseService purchaseService)
        {
            this.purchaseService = purchaseService;
        }

        #endregion

        #region Public

        /// <summary>
        /// 
        /// </summary>
        public PurchaseResponseModel Get([FromUri] PurchaseOrderSearchRequestModel request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            // In case Of Purcahse Order ,PurchaseOrderType is 1
            if (request.PurchaseOrderType == 1)
            {
                return purchaseService.GetPurchaseOrders(request).CreateFrom();
            }
            // PurchaseOrderType 2, that is GRN
            return purchaseService.GetGoodsReceivedNotes(request).CreateFromGRN();

        }

        public Purchase Get(int purchaseId)
        {
            return purchaseService.GetPurchaseById(purchaseId).CreateFrom();

        }

        [ApiException]
        public PurchaseListView Post(Purchase purchase)
        {
            if (purchase == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return purchaseService.SavePurchase(purchase.CreateFrom()).CreateFromForListView();
        }

        [ApiException]
        [HttpDelete]
        public int Delete(Purchase purchase)
        {
            if (purchase == null)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            purchaseService.DeletePurchaseOrder(purchase.PurchaseId);
            return 1;
        }
        #endregion
    }
}