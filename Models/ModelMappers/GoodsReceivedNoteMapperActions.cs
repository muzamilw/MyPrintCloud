using System;
using MPC.Models.DomainModels;

namespace MPC.Models.ModelMappers
{
    /// <summary>
    /// Goods Received Note MApper Actions
    /// </summary>
    public sealed class GoodsReceivedNoteMapperActions
    {
        /// <summary>
        /// Action to create a Goods Received Note Detail
        /// </summary>
        public Func<GoodsReceivedNoteDetail> CreateGoodsReceivedNoteDetail { get; set; }

        /// <summary>
        /// Action to delete a Goods Received Note Detail
        /// </summary>
        public Action<GoodsReceivedNoteDetail> DeleteGoodsReceivedNoteDetail { get; set; }
    }
}
