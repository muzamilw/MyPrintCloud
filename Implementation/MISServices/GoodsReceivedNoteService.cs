using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.ModelMappers;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.MISServices
{
    public class GoodsReceivedNoteService : IGoodsReceivedNoteService
    {
        #region Private

        private readonly IGoodRecieveNoteRepository goodRecieveNoteRepository;
        private readonly IPrefixRepository prefixRepository;
        private readonly IGoodsReceivedNoteDetailRepository goodsReceivedNoteDetailRepository;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        public GoodsReceivedNoteService(IGoodRecieveNoteRepository goodRecieveNoteRepository, IPrefixRepository prefixRepository,
            IGoodsReceivedNoteDetailRepository goodsReceivedNoteDetailRepository)
        {
            this.goodRecieveNoteRepository = goodRecieveNoteRepository;
            this.prefixRepository = prefixRepository;
            this.goodsReceivedNoteDetailRepository = goodsReceivedNoteDetailRepository;
        }

        #endregion


        public GoodsReceivedNoteResponseModel GetGoodsReceivedNotes(GoodsReceivedNoteRequestModel requestModel)
        {
            return goodRecieveNoteRepository.GetGoodsReceivedNotes(requestModel);
        }

        /// <summary>
        /// Save GRN
        /// </summary>
        public GoodsReceivedNote SaveGRN(GoodsReceivedNote grn)
        {
            // Get Goods Received Note if exists else create new
            GoodsReceivedNote grnTarget = GetById(grn.GoodsReceivedId) ?? CreateGoodsReceivedNote();

            // Update GRN
            grn.UpdateTo(grnTarget, new GoodsReceivedNoteMapperActions
            {
                CreateGoodsReceivedNoteDetail = CreateGoodsReceivedNoteDetail,
                DeleteGoodsReceivedNoteDetail = DeleteGoodsReceivedNoteDetail,
            });

            // Save Changes
            goodRecieveNoteRepository.SaveChanges();
            return GetById(grnTarget.GoodsReceivedId);
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        public GoodsReceivedNote GetById(long grnId)
        {
            return goodRecieveNoteRepository.Find(grnId);
        }

        /// <summary>
        /// Creates New GRN and assigns new generated code
        /// </summary>
        private GoodsReceivedNote CreateGoodsReceivedNote()
        {
            string code = prefixRepository.GetNextGRNCodePrefix();
            GoodsReceivedNote itemTarget = goodRecieveNoteRepository.Create();
            goodRecieveNoteRepository.Add(itemTarget);
            itemTarget.code = code;
            return itemTarget;
        }

        /// <summary>
        ///Create Goods Received Note Detail
        /// </summary>
        private GoodsReceivedNoteDetail CreateGoodsReceivedNoteDetail()
        {
            GoodsReceivedNoteDetail itemTarget = goodsReceivedNoteDetailRepository.Create();
            goodsReceivedNoteDetailRepository.Add(itemTarget);
            return itemTarget;
        }

        /// <summary>
        /// Delete Goods Received Note Detail
        /// </summary>
        private void DeleteGoodsReceivedNoteDetail(GoodsReceivedNoteDetail goodsReceivedNoteDetail)
        {
            goodsReceivedNoteDetailRepository.Delete(goodsReceivedNoteDetail);
        }
    }
}
