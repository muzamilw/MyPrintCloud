using System.Net;
using System.Web;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using System.Web.Http;
using MPC.WebBase.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Delivery Notes API Controller
    /// </summary>
    public class DeliveryNotesController : ApiController
    {
        #region Private

        private readonly IDeliveryNotesService _deliveryNotesService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DeliveryNotesController(IDeliveryNotesService deliveryNotesService)
        {
            _deliveryNotesService = deliveryNotesService;
        }

        #endregion

        #region Public



        public DeliverNotesResposne Get([FromUri]DeliveryNotesRequest request)
        {
            return _deliveryNotesService.GetDeliveryNotes(request).CreateFrom();
        }


        public DeliveryNote Get(int deliverNoteId)
        {
            return _deliveryNotesService.GetDetailDeliveryNote(deliverNoteId).CreateFrom();
        }


        [ApiException]
        public DeliveryNote Post(DeliveryNote deliveryNote)
        {
            if (deliveryNote == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            return _deliveryNotesService.SaveDeliveryNote(deliveryNote.CreateFrom()).CreateFromListView();
        }

        [ApiException]
        [HttpDelete]
        public void Delete(DeliveryNote deliveryNote)
        {
            if (deliveryNote == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            _deliveryNotesService.DeleteDeliveryNote(deliveryNote.DeliveryNoteId);
        }
        #endregion
    }
}