using System.Collections.Generic;
using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;


namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Purchase Detail API Mapper
    /// </summary>
    public static class PurchaseDetailMapper
    {
        /// <summary>
        /// Create From  Domain Model
        /// </summary>
        public static PurchaseDetail CreateFrom(this DomainModels.PurchaseDetail source)
        {

            return new PurchaseDetail
            {
                PurchaseDetailId = source.PurchaseDetailId,
                Discount = source.Discount,
                NetTax = source.NetTax,
                ProductType = source.ProductType,
                TotalPrice = source.TotalPrice,
                ItemCode = source.ItemCode,
                RefItemId = source.RefItemId,
                ServiceDetail = source.ServiceDetail,
                TaxValue = source.TaxValue,
                freeitems = source.freeitems,
                packqty = source.packqty,
                price = source.price,
                quantity = source.quantity
            };


        }

        /// <summary>
        /// Create From API Model
        /// </summary>
        public static DomainModels.PurchaseDetail CreateFrom(this PurchaseDetail source)
        {

            return new DomainModels.PurchaseDetail
            {
                PurchaseDetailId = source.PurchaseDetailId,
                Discount = source.Discount,
                NetTax = source.NetTax,
                ProductType = source.ProductType,
                TotalPrice = source.TotalPrice,
                ItemCode = source.ItemCode,
                RefItemId = source.RefItemId,
                ServiceDetail = source.ServiceDetail,
                TaxValue = source.TaxValue,
                freeitems = source.freeitems,
                packqty = source.packqty,
                price = source.price,
                quantity = source.quantity,
            };
        }
    }
}