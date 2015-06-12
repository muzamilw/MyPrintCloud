using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class GoodsReceivedNoteMapper
    {
        /// <summary>
        /// Create From 
        /// </summary>
        public static GoodsReceivedNoteListView CreateFromForListView(this DomainModels.GoodsReceivedNote source)
        {

            return new GoodsReceivedNoteListView
            {
                GoodsReceivedId = source.GoodsReceivedId,
                Code = source.code,
                DateReceived = source.date_Received,
                //todo SupplierName = source.SupplierId,
                TotalPrice = source.TotalPrice

            };
        }
        /// <summary>
        /// GRN List
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
        /// Create From Domain MOdel
        /// </summary>
        public static PurchaseListView CreateFromForGRN(this DomainModels.GoodsReceivedNote source)
        {

            return new PurchaseListView
            {
                PurchaseId = source.GoodsReceivedId,
                Code = source.code,
                DatePurchase = source.date_Received,
                SupplierName = source.Company != null ? source.Company.Name : string.Empty,
                FlagColor = source.SectionFlag != null ? source.SectionFlag.FlagColor : string.Empty,
                TotalPrice = source.TotalPrice,
                RefNo = source.RefNo,
                Status = source.Status,

            };
        }

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static GoodsReceivedNote CreateFrom(this DomainModels.GoodsReceivedNote source)
        {

            return new GoodsReceivedNote
            {
                PurchaseId = source.PurchaseId,
                Discount = source.Discount,
                DeliveryDate = source.DeliveryDate,
                CreatedBy = source.CreatedBy,
                RefNo = source.RefNo,
                Tel1 = source.Tel1,
                Status = source.Status,
                Reference2 = source.Reference2,
                Reference1 = source.Reference1,
                NetTotal = source.NetTotal,
                SupplierId = source.SupplierId,
                TotalTax = source.TotalTax,
                UserNotes = source.UserNotes,
                TotalPrice = source.TotalPrice,
                CarrierId = source.CarrierId,
                FlagId = source.FlagId,
                Comments = source.Comments,
                ContactId = source.ContactId,
                discountType = source.discountType,
                GoodsReceivedId = source.GoodsReceivedId,
                code = source.Comments,
                date_Received = source.date_Received,
                grandTotal = source.grandTotal,
                isProduct = source.isProduct,
                GoodsReceivedNoteDetails = source.GoodsReceivedNoteDetails != null ? source.GoodsReceivedNoteDetails.Select(x => x.CreateFrom()).ToList() : null
            };
        }

        /// <summary>
        /// Create From API Model
        /// </summary>
        public static DomainModels.GoodsReceivedNote CreateFrom(this GoodsReceivedNote source)
        {

            return new DomainModels.GoodsReceivedNote
            {
                PurchaseId = source.PurchaseId,
                Discount = source.Discount,
                DeliveryDate = source.DeliveryDate,
                CreatedBy = source.CreatedBy,
                RefNo = source.RefNo,
                Tel1 = source.Tel1,
                Status = source.Status,
                Reference2 = source.Reference2,
                Reference1 = source.Reference1,
                NetTotal = source.NetTotal,
                SupplierId = source.SupplierId,
                TotalTax = source.TotalTax,
                UserNotes = source.UserNotes,
                TotalPrice = source.TotalPrice,
                CarrierId = source.CarrierId,
                FlagId = source.FlagId,
                Comments = source.Comments,
                ContactId = source.ContactId,
                discountType = source.discountType,
                GoodsReceivedId = source.GoodsReceivedId,
                code = source.Comments,
                date_Received = source.date_Received,
                grandTotal = source.grandTotal,
                isProduct = source.isProduct,
                GoodsReceivedNoteDetails = source.GoodsReceivedNoteDetails != null ? source.GoodsReceivedNoteDetails.Select(x => x.CreateFrom()).ToList() : null
            };
        }
    }
}