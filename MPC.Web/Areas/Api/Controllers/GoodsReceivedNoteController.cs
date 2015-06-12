using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class GoodsReceivedNoteController : ApiController
    {
        #region Private

        private readonly IGoodsReceivedNoteService goodsReceivedNoteService;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GoodsReceivedNoteController(IGoodsReceivedNoteService goodsReceivedNoteService)
        {
            this.goodsReceivedNoteService = goodsReceivedNoteService;
        }

        #endregion

        #region Public

        /// <summary>
        /// Get Purchases
        /// </summary>
        [ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewCrm })]
        public GoodsReceivedNoteResponseModel Get([FromUri] GoodsReceivedNoteRequestModel request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return goodsReceivedNoteService.GetGoodsReceivedNotes(request).CreateFrom();
        }

        [ApiException]
        public PurchaseListView Post(GoodsReceivedNote grn)
        {
            if (grn == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return goodsReceivedNoteService.SaveGRN(grn.CreateFrom()).CreateFromForGRN();
        }

        [HttpGet]
        public GoodsReceivedNote Get( int grnId)
        {
            return goodsReceivedNoteService.GetById(grnId).CreateFrom();

        }
        #endregion
    }
}