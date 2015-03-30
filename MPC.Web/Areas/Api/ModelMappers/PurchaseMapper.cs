using System.Linq;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;
    using System.Collections.Generic;
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
                //todo SupplierName = source.SupplierId,
                TotalPrice = source.TotalPrice

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
    }
}