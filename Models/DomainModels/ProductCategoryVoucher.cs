using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace MPC.Models.DomainModels
{
    /// <summary>
    /// ProductCategory Voucher Domain Model
    /// </summary>
    public class ProductCategoryVoucher
    {
        public long CategoryVoucherId { get; set; }
        public long? ProductCategoryId { get; set; }
        public long? VoucherId { get; set; }
        [NotMapped]
        public string CategoryName { get; set; }
        public virtual DiscountVoucher DiscountVoucher { get; set; }
    }
}
