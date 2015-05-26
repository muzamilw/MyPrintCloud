using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using System.Web.Http;

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
        public DeliveryNotesController( IDeliveryNotesService deliveryNotesService)
        {
            _deliveryNotesService = deliveryNotesService;
        }

        #endregion
        #region Public



        public DeliverNotesResposne Get([FromUri]DeliveryNotesRequest request)
        {
          return _deliveryNotesService.GetDeliveryNotes(request).CreateFrom();
        }


        public Models.DeliveryNotes Get(int deliverNoteId)
        {
           return _deliveryNotesService.GetDetailDeliveryNote(deliverNoteId).CreateDetailFromModel();
        }

     
        //[ApiException]
        //[ApiAuthorize(AccessRights = new[] { SecurityAccessRight.CanViewCalendar })]
        //[CompressFilterAttribute]
        //public void Post(Activity activity)
        //{
        //    if (activity == null || !ModelState.IsValid)
        //    {
        //        throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
        //    }
        //    calendarService.SaveActivityDropOrResize(activity.CreateFrom());
        //}
        #endregion
    }
}