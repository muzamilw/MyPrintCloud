using System.Collections.Generic;
using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class PurchaseMapper
    {
        /// <summary>
        /// Create From 
        /// </summary>
        public static PurchaseListView CreateFromForListView(this DomainModels.Purchase source)
        {

            return new PurchaseListView
            {
                PurchaseId = source.PurchaseId,
                Code = source.Code,
                DatePurchase = source.date_Purchase,
                SupplierName = source.Company != null ? source.Company.Name : string.Empty,
                TotalPrice = source.TotalPrice,
                RefNo = source.RefNo,
                Status = source.Status,
                FlagColor = source.SectionFlag != null ? source.SectionFlag.FlagColor : string.Empty

            };
        }

        /// <summary>
        /// Purchases List
        /// </summary>
        public static PurchaseResponseModel CreateFrom(this MPC.Models.ResponseModels.PurchaseResponseModel source)
        {
            return new PurchaseResponseModel
            {
                RowCount = source.TotalCount,
                PurchasesList = source.Purchases.Select(order => order.CreateFromForListView())
            };
        }

        /// <summary>
        /// Create Form Base response
        /// </summary>
        public static PurchaseBaseResponse CreateFrom(this MPC.Models.ResponseModels.PurchaseBaseResponse source)
        {
            return new PurchaseBaseResponse
            {
                LoggedInUser = source.LoggedInUser,
                SectionFlags = source.SectionFlags != null ? source.SectionFlags.Select(flag => flag.CreateFromDropDown()) : new List<SectionFlagDropDown>(),
                DeliveryCarriers = source.DeliveryCarriers != null ? source.DeliveryCarriers.Select(flag => flag.CreateFromDropDown()) : new List<DeliveryCarrier>(),
                SystemUsers = source.SystemUsers != null ? source.SystemUsers.Select(cc => cc.CreateFrom()) :
           new List<SystemUserDropDown>(),
                CurrencySymbol = source.CurrencySymbol
            };
        }

        /// <summary>
        /// Create From 
        /// </summary>
        public static Purchase CreateFrom(this DomainModels.Purchase source)
        {

            Purchase purchase = new Purchase
            {
                PurchaseId = source.PurchaseId,
                Code = source.Code,
                FlagID = source.FlagID,
                StoreId = source.Company != null ? source.Company.StoreId : null,
                IsCustomer = source.Company != null ? source.Company.IsCustomer : (short)0,
                RefNo = source.RefNo,
                TotalPrice = source.TotalPrice,
                Comments = source.Comments,
                SupplierId = source.SupplierId,
                ContactId = source.ContactId,
                CreatedBy = source.CreatedBy,
                Discount = source.Discount,
                FootNote = source.FootNote,
                GrandTotal = source.GrandTotal,
                Status = source.Status,
                NetTotal = source.NetTotal,
                TotalTax = source.TotalTax,
                date_Purchase = source.date_Purchase,
                discountType = source.discountType,
                SupplierContactCompany = source.SupplierContactCompany,
                isproduct = source.isproduct,
                SupplierContactAddressID = source.SupplierContactAddressID,
                PurchaseDetails = source.PurchaseDetails != null ? source.PurchaseDetails.Select(pd => pd.CreateFrom()).ToList() : new List<PurchaseDetail>(),
            };

            return purchase;
        }

        /// <summary>
        /// Create From 
        /// </summary>
        public static DomainModels.Purchase CreateFrom(this Purchase source)
        {

            return new DomainModels.Purchase
            {
                PurchaseId = source.PurchaseId,
                Code = source.Code,
                FlagID = source.FlagID,
                RefNo = source.RefNo,
                TotalPrice = source.TotalPrice,
                Comments = source.Comments,
                SupplierId = source.SupplierId,
                ContactId = source.ContactId,
                CreatedBy = source.CreatedBy,
                Discount = source.Discount,
                FootNote = source.FootNote,
                GrandTotal = source.GrandTotal,
                Status = source.Status,
                NetTotal = source.NetTotal,
                TotalTax = source.TotalTax,
                date_Purchase = source.date_Purchase,
                discountType = source.discountType,
                isproduct = source.isproduct,
                SupplierContactAddressID = source.SupplierContactAddressID,
                SupplierContactCompany = source.SupplierContactCompany,
                PurchaseDetails = source.PurchaseDetails != null ? source.PurchaseDetails.Select(pd => pd.CreateFrom()).ToList() : null,


            };
        }

        /// <summary>
        /// Purchases List
        /// </summary>
        public static PurchaseResponseModel CreateFromGRN(this MPC.Models.ResponseModels.GoodsReceivedNotesResponseModel source)
        {
            return new PurchaseResponseModel
            {
                RowCount = source.TotalCount,
                PurchasesList = source.GoodsReceivedNotes.Select(order => order.CreateFromForGRN())
            };
        }
    }
}