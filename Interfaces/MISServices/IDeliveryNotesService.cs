
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.MISServices
{
    public interface IDeliveryNotesService
    {
        /// <summary>
        /// Get Delivery Notes
        /// </summary>
        GetDeliveryNoteResponse GetDeliveryNotes(DeliveryNotesRequest request);

        /// <summary>
        /// Get details of delivery note
        /// </summary>
        /// <returns></returns>
        DeliveryNote GetDetailDeliveryNote(int id);

        /// <summary>
        /// base Data for delivery NOte
        /// </summary>
        /// <returns></returns>
        DeliveryNoteBaseResponse GetBaseData();

        /// <summary>
        /// Save Delivery Note
        /// </summary>
        DeliveryNote SaveDeliveryNote(DeliveryNote deliveryNote);

        /// <summary>
        /// Delete Delivery Note
        /// </summary>
        void DeleteDeliveryNote(int deliveryNoteId);
    }
}
