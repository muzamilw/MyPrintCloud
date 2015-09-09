using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
namespace MPC.Interfaces.MISServices
{
    public interface IGoodsReceivedNoteService
    {
        /// <summary>
        /// Get Goods Received Notes
        /// </summary>
        GoodsReceivedNoteResponseModel GetGoodsReceivedNotes(GoodsReceivedNoteRequestModel model);

        /// <summary>
        /// Save GRN
        /// </summary>
        GoodsReceivedNote SaveGRN(GoodsReceivedNote grn);

        /// <summary>
        /// Get By Id
        /// </summary>
        GoodsReceivedNote GetById(long grnId);

    }
}
