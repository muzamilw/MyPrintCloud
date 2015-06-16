using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using System.Web.Http;
using DeliveryNoteBaseResponse = MPC.MIS.Areas.Api.Models.DeliveryNoteBaseResponse;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Delivery Notes API Controller
    /// </summary>
    public class DeliveryNoteBaseController : ApiController
    {
        #region Private

        private readonly IDeliveryNotesService _deliveryNotesService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DeliveryNoteBaseController(IDeliveryNotesService deliveryNotesService)
        {
            _deliveryNotesService = deliveryNotesService;
        }

        #endregion

        #region Public


        /// <summary>
        /// Delivery note base data 
        /// </summary>
        public DeliveryNoteBaseResponse Get()
        {
            return _deliveryNotesService.GetBaseData().CreateFrom();
        }


        #endregion
    }
}