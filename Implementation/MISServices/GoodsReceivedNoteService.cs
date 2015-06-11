using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    public class GoodsReceivedNoteService: IGoodsReceivedNoteService
    {
        #region Private

        private readonly IGoodRecieveNoteRepository goodRecieveNoteRepository;

        #endregion
        
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public GoodsReceivedNoteService(IGoodRecieveNoteRepository goodRecieveNoteRepository)
        {
            this.goodRecieveNoteRepository = goodRecieveNoteRepository;
        }

        #endregion


        public GoodsReceivedNoteResponseModel GetGoodsReceivedNotes(GoodsReceivedNoteRequestModel requestModel)
        {
            return goodRecieveNoteRepository.GetGoodsReceivedNotes(requestModel);
        }
    }
}
