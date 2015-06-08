using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.Controllers
{

    /// <summary>
    /// Purchase Base API Controller
    /// </summary>
    public class PurchaseBaseController : ApiController
    {
        #region Private

        private readonly IPurchaseService purchaseService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public PurchaseBaseController(IPurchaseService purchaseService)
        {
            this.purchaseService = purchaseService;
        }

        #endregion

        #region Public


        /// <summary>
        /// Purchase base data 
        /// </summary>
        public PurchaseBaseResponse Get()
        {
            return purchaseService.GetBaseData().CreateFrom();
        }


        #endregion
    }
}