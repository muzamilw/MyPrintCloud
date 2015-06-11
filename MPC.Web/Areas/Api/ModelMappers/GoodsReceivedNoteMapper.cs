using System.Linq;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;
    using System.Collections.Generic;
    public static class GoodsReceivedNoteMapper
    {
        /// <summary>
        /// Create From 
        /// </summary>
        public static GoodsReceivedNoteListView CreateFromForListView(this DomainModels.GoodsReceivedNote source)
        {

            GoodsReceivedNoteListView goodsReceivedNote = new GoodsReceivedNoteListView
            {
                GoodsReceivedId = source.GoodsReceivedId,
                Code = source.code,
                DateReceived = source.date_Received,
                //todo SupplierName = source.SupplierId,
                TotalPrice = source.TotalPrice

            };

            return goodsReceivedNote;
        }
        /// <summary>
        /// Purchases List
        /// </summary>
        public static GoodsReceivedNoteResponseModel CreateFrom(this MPC.Models.ResponseModels.GoodsReceivedNoteResponseModel source)
        {
            return new GoodsReceivedNoteResponseModel
            {
                RowCount = source.TotalCount,
                GoodsReceivedNotesList = source.GoodsReceivedNotes.Select(order => order.CreateFromForListView())
            };
        }


        /// <summary>
        /// Create From 
        /// </summary>
        public static PurchaseListView CreateFromForGRN(this DomainModels.GoodsReceivedNote source)
        {

            return new PurchaseListView
            {
                PurchaseId = source.GoodsReceivedId,
                Code = source.code,
                DatePurchase = source.date_Received,
                //todo SupplierName = source.SupplierId,
                TotalPrice = source.TotalPrice

            };
        }
    }
}