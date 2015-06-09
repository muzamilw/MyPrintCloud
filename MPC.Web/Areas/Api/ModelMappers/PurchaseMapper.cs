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

            PurchaseListView purchase = new PurchaseListView
            {
                PurchaseId = source.PurchaseId,
                Code = source.Code,
                DatePurchase = source.date_Purchase,
                SupplierName = source.Company != null ? source.Company.Name : string.Empty,
                TotalPrice = source.TotalPrice,
                RefNo = source.RefNo,
                FlagColor = source.SectionFlag != null ? source.SectionFlag.FlagColor : string.Empty

            };

            return purchase;
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
                SectionFlags = source.SectionFlags != null ? source.SectionFlags.Select(flag => flag.CreateFromDropDown()) : new List<SectionFlagDropDown>(),
                SystemUsers = source.SystemUsers != null ? source.SystemUsers.Select(cc => cc.CreateFrom()) :
           new List<SystemUserDropDown>(),
            };
        }
    }
}