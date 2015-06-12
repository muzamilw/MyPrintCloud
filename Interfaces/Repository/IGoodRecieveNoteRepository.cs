using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    public interface IGoodRecieveNoteRepository : IBaseRepository<GoodsReceivedNote, long>
    {
        GoodsReceivedNoteResponseModel GetGoodsReceivedNotes(GoodsReceivedNoteRequestModel request);

        /// <summary>
        /// Get Goods Received Notes For Purchase Order Secreen
        /// </summary>
        GoodsReceivedNotesResponseModel GetGoodsReceivedNotes(PurchaseOrderSearchRequestModel request);
    }
}
