using System;
using System.Collections.Generic;
using System.Linq;
using MPC.Models.Common;
using MPC.Models.DomainModels;

namespace MPC.Models.ModelMappers
{
    /// <summary>
    /// Purchase Domain Mapper
    /// </summary>
    public static class PurchaseMapper
    {
        #region Private

        #region Purchase

        #region Purchase Order

        /// <summary>
        /// Update the Purchase Order
        /// </summary>
        private static void UpdatePurchase(Purchase source, Purchase target)
        {
            target.FlagID = source.FlagID;
            target.RefNo = source.RefNo;
            target.TotalPrice = source.TotalPrice;
            target.Comments = source.Comments;
            target.SupplierId = source.SupplierId;
            target.ContactId = source.ContactId;
            target.CreatedBy = source.CreatedBy;
            target.SupplierContactCompany = source.SupplierContactCompany;
            target.Discount = source.Discount;
            target.FootNote = source.FootNote;
            target.GrandTotal = source.GrandTotal;
            target.Status = source.Status;
            target.NetTotal = source.NetTotal;
            target.TotalTax = source.TotalTax;
            target.date_Purchase = source.date_Purchase;
            target.discountType = source.discountType;
            target.isproduct = source.isproduct;
            target.SupplierContactAddressID = source.SupplierContactAddressID;
        }

        #endregion Purchase Order


        #region Purchase Details

        /// <summary>
        /// True if the Purchas eDetail is new
        /// </summary>
        private static bool IsNewPurchaseDetail(PurchaseDetail sourcePurchaseDetail)
        {
            return sourcePurchaseDetail.PurchaseDetailId == 0;
        }

        /// <summary>
        /// Initialize target Purchase Detail
        /// </summary>
        private static void InitializePurchaseDetails(Purchase item)
        {
            if (item.PurchaseDetails == null)
            {
                item.PurchaseDetails = new List<PurchaseDetail>();
            }
        }

        /// <summary>
        /// Update Purchase Detail
        /// </summary>
        private static void UpdatePurchaseDetail(Purchase source, Purchase target, PurchaseMapperActions actions)
        {
            InitializePurchaseDetails(source);
            InitializePurchaseDetails(target);

            UpdateOrAddPurchaseDetails(source, target, actions);
            DeletePurchaseDetails(source, target, actions);
        }

        /// <summary>
        /// Delete lines no longer needed
        /// </summary>
        private static void DeletePurchaseDetails(Purchase source, Purchase target, PurchaseMapperActions actions)
        {
            List<PurchaseDetail> linesToBeRemoved = target.PurchaseDetails.Where(
                pre => !IsNewPurchaseDetail(pre) && source.PurchaseDetails.All(sourcePre => sourcePre.PurchaseDetailId != pre.PurchaseDetailId))
                  .ToList();
            linesToBeRemoved.ForEach(line =>
            {
                target.PurchaseDetails.Remove(line);
                actions.DeletePurchaseDetail(line);
            });
        }

        /// <summary>
        /// Update or add Purchase Details
        /// </summary>
        private static void UpdateOrAddPurchaseDetails(Purchase source, Purchase target, PurchaseMapperActions actions)
        {
            foreach (PurchaseDetail sourceLine in source.PurchaseDetails.ToList())
            {
                UpdateOrAddPurchaseDetail(sourceLine, target, actions);
            }
        }

        private static void UpdateTo(this PurchaseDetail source, PurchaseDetail target)
        {
            target.Discount = source.Discount;
            target.NetTax = source.NetTax;
            target.ProductType = source.ProductType;
            target.TotalPrice = source.TotalPrice;
            target.ItemCode = source.ItemCode;
            target.RefItemId = source.RefItemId;
            target.ServiceDetail = source.ServiceDetail;
            target.TaxValue = source.TaxValue;
            target.freeitems = source.freeitems;
            target.packqty = source.packqty;
            target.price = source.price;
            target.quantity = source.quantity;
        }

        /// <summary>
        /// Update target Pre Payments
        /// </summary>
        private static void UpdateOrAddPurchaseDetail(PurchaseDetail sourcePurchaseDetail, Purchase target, PurchaseMapperActions actions)
        {
            PurchaseDetail targetLine;
            if (IsNewPurchaseDetail(sourcePurchaseDetail))
            {
                targetLine = actions.CreatePurchaseDetail();
                target.PurchaseDetails.Add(targetLine);
            }
            else
            {
                targetLine = target.PurchaseDetails.FirstOrDefault(pre => pre.PurchaseDetailId == sourcePurchaseDetail.PurchaseDetailId);
            }

            sourcePurchaseDetail.UpdateTo(targetLine);
        }

        #endregion Purchase Details

        #endregion Purchase


        #endregion Private

        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this Purchase source, Purchase target,
            PurchaseMapperActions actions)
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

            UpdatePurchase(source, target);
            UpdatePurchaseDetail(source, target, actions);
        }

        #endregion
    }
}
