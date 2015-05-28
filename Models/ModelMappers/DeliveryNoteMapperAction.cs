using System;
using MPC.Models.DomainModels;

namespace MPC.Models.ModelMappers
{
    /// <summary>
    /// Delivery Note Mapper Action
    /// </summary>
    public sealed class DeliveryNoteMapperAction
    {
        /// <summary>
        /// Action to create a Delivery Note Detail
        /// </summary>
        public Func<DeliveryNoteDetail> CreateDeliveryNoteDetail { get; set; }

        /// <summary>
        /// Action to delete a Delivery Note Detail
        /// </summary>
        public Action<DeliveryNoteDetail> DeleteDeliveryNoteDetail { get; set; }
    }
}
