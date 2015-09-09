using System;
using System.Collections.Generic;
using System.Linq;
using MPC.Models.DomainModels;

namespace MPC.Models.ModelMappers
{
    /// <summary>
    /// Goods Received Note Domain Mapper
    /// </summary>
    public static class GoodsReceivedNoteMapper
    {
        #region Private

        #region Goods Received Note

        #region Goods Received Note

        /// <summary>
        /// Update the Goods Received Note
        /// </summary>
        private static void UpdateGoodsReceivedNote(GoodsReceivedNote source, GoodsReceivedNote target)
        {
            target.Discount = source.Discount;
            target.DeliveryDate = source.DeliveryDate;
            target.PurchaseId = source.PurchaseId;
            target.CreatedBy = source.CreatedBy;
            target.RefNo = source.RefNo;
            target.Tel1 = source.Tel1;
            target.Status = source.Status;
            target.Reference2 = source.Reference2;
            target.Reference1 = source.Reference1;
            target.NetTotal = source.NetTotal;
            target.SupplierId = source.SupplierId;
            target.TotalTax = source.TotalTax;
            target.UserNotes = source.UserNotes;
            target.TotalPrice = source.TotalPrice;
            target.CarrierId = source.CarrierId;
            target.FlagId = source.FlagId;
            target.Comments = source.Comments;
            target.ContactId = source.ContactId;
            target.discountType = source.discountType;
            target.GoodsReceivedId = source.GoodsReceivedId;
            target.date_Received = source.date_Received;
            target.grandTotal = source.grandTotal;
            target.isProduct = source.isProduct;
        }

        #endregion Goods Received Note


        #region Goods ReceivedNo te Details

        /// <summary>
        /// True if the GRN Detail is new
        /// </summary>
        private static bool IsNewGoodsReceivedNoteDetail(GoodsReceivedNoteDetail sourceGoodsReceivedNoteDetail)
        {
            return sourceGoodsReceivedNoteDetail.GoodsReceivedDetailId == 0;
        }

        /// <summary>
        /// Initialize target GRN Detail
        /// </summary>
        private static void InitializGoodsReceivedNoteDetails(GoodsReceivedNote item)
        {
            if (item.GoodsReceivedNoteDetails == null)
            {
                item.GoodsReceivedNoteDetails = new List<GoodsReceivedNoteDetail>();
            }
        }

        /// <summary>
        /// Update Goods Received Note Details
        /// </summary>
        private static void UpdateGoodsReceivedNoteDetail(GoodsReceivedNote source, GoodsReceivedNote target, GoodsReceivedNoteMapperActions actions)
        {
            InitializGoodsReceivedNoteDetails(source);
            InitializGoodsReceivedNoteDetails(target);

            UpdateOrAddGoodsReceivedNoteDetails(source, target, actions);
            DeleteGoodsReceivedNoteDetail(source, target, actions);
        }

        /// <summary>
        /// Delete lines no longer needed
        /// </summary>
        private static void DeleteGoodsReceivedNoteDetail(GoodsReceivedNote source, GoodsReceivedNote target, GoodsReceivedNoteMapperActions actions)
        {
            List<GoodsReceivedNoteDetail> linesToBeRemoved = target.GoodsReceivedNoteDetails.Where(
                pre => !IsNewGoodsReceivedNoteDetail(pre) && source.GoodsReceivedNoteDetails.All(sourcePre => sourcePre.GoodsReceivedDetailId != pre.GoodsReceivedDetailId))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.GoodsReceivedNoteDetails.Remove(line);
                actions.DeleteGoodsReceivedNoteDetail(line);
            });
        }

        /// <summary>
        /// Update or add Goods Received Note Details
        /// </summary>
        private static void UpdateOrAddGoodsReceivedNoteDetails(GoodsReceivedNote source, GoodsReceivedNote target, GoodsReceivedNoteMapperActions actions)
        {
            foreach (GoodsReceivedNoteDetail sourceLine in source.GoodsReceivedNoteDetails.ToList())
            {
                UpdateOrAddGoodsReceivedNoteDetail(sourceLine, target, actions);
            }
        }

        private static void UpdateTo(this GoodsReceivedNoteDetail source, GoodsReceivedNoteDetail target)
        {
            target.Discount = source.Discount;
            target.ProductType = source.ProductType;
            target.QtyReceived = source.QtyReceived;
            target.Price = source.Price;
            target.PackQty = source.PackQty;
            target.NetTax = source.NetTax;
            target.FreeItems = source.FreeItems;
            target.RefItemId = source.RefItemId;
            target.ItemCode = source.ItemCode;
            target.TaxValue = source.TaxValue;
            target.Details = source.Details;
            target.TotalOrderedqty = source.TotalOrderedqty;
            target.TotalPrice = source.TotalPrice;
        }

        /// <summary>
        /// Update target Pre Payments
        /// </summary>
        private static void UpdateOrAddGoodsReceivedNoteDetail(GoodsReceivedNoteDetail source, GoodsReceivedNote target, GoodsReceivedNoteMapperActions actions)
        {
            GoodsReceivedNoteDetail targetLine;
            if (IsNewGoodsReceivedNoteDetail(source))
            {
                targetLine = actions.CreateGoodsReceivedNoteDetail();
                target.GoodsReceivedNoteDetails.Add(targetLine);
            }
            else
            {
                targetLine = target.GoodsReceivedNoteDetails.FirstOrDefault(pre => pre.GoodsReceivedDetailId == source.GoodsReceivedDetailId);
            }

            source.UpdateTo(targetLine);
        }

        #endregion Goods Received Note Details

        #endregion Goods Received Note


        #endregion Private

        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this GoodsReceivedNote source, GoodsReceivedNote target,
            GoodsReceivedNoteMapperActions actions)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            if (actions == null)
            {
                throw new ArgumentNullException("actions");
            }

            UpdateGoodsReceivedNote(source, target);
            UpdateGoodsReceivedNoteDetail(source, target, actions);
        }

        #endregion
    }
}
