using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Items Voucher Domain Model
    /// </summary>
    public class ItemsVoucher
    {
        public long ItemVoucherId { get; set; }
        public long? ItemId { get; set; }
        public long? VoucherId { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
        public virtual DiscountVoucher DiscountVoucher { get; set; }
    }
}
