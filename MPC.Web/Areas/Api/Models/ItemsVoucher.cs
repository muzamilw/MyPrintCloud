using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Items Voucher Domain Model
    /// </summary>
    public class ItemsVoucher
    {
        public long ItemVoucherId { get; set; }
        public long? ItemId { get; set; }
        public long? VoucherId { get; set; }

        public bool IsSelected { get; set; }
        public string ProductName { get; set; }
       

        public virtual DiscountVoucher DiscountVoucher { get; set; }
    }
}