using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;


namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Goods Received Note Detail API Mapper
    /// </summary>
    public static class GoodsReceivedNoteDetailMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static GoodsReceivedNoteDetail CreateFrom(this DomainModels.GoodsReceivedNoteDetail source)
        {

            return new GoodsReceivedNoteDetail
            {
                GoodsReceivedDetailId = source.GoodsReceivedDetailId,
                Discount = source.Discount,
                ProductType = source.ProductType,
                QtyReceived = source.QtyReceived,
                Price = source.Price,
                PackQty = source.PackQty,
                NetTax = source.NetTax,
                FreeItems = source.FreeItems,
                RefItemId = source.RefItemId,
                ItemCode = source.ItemCode,
                TaxValue = source.TaxValue,
                Details = source.Details,
                TotalOrderedqty = source.TotalOrderedqty,
                TotalPrice = source.TotalPrice
            };
        }

        /// <summary>
        /// Create From API Model
        /// </summary>
        public static DomainModels.GoodsReceivedNoteDetail CreateFrom(this GoodsReceivedNoteDetail source)
        {

            return new DomainModels.GoodsReceivedNoteDetail
            {
                GoodsReceivedDetailId = source.GoodsReceivedDetailId,
                Discount = source.Discount,
                ProductType = source.ProductType,
                QtyReceived = source.QtyReceived,
                Price = source.Price,
                PackQty = source.PackQty,
                NetTax = source.NetTax,
                FreeItems = source.FreeItems,
                RefItemId = source.RefItemId,
                ItemCode = source.ItemCode,
                TaxValue = source.TaxValue,
                Details = source.Details,
                TotalOrderedqty = source.TotalOrderedqty,
                TotalPrice = source.TotalPrice

            };
        }
    }
}