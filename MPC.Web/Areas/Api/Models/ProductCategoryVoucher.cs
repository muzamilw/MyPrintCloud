using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class ProductCategoryVoucher
    {
        public long CategoryVoucherId { get; set; }
        public long? ProductCategoryId { get; set; }
        public long? VoucherId { get; set; }

        public bool IsSelected { get; set; }
        public string CategoryName { get; set; }
        public virtual DiscountVoucher DiscountVoucher { get; set; }
    }
}