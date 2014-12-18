using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.Webstore.ViewModels
{
    public class ProductPriceMatrixViewModel
    {
        public string Quantity { get; set; }
     
        public string Price { get; set; }

        public string DiscountPrice { get; set; }

        public bool isDiscounted { get; set; }
    }
}