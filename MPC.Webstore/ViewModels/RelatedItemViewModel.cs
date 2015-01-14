using MPC.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.Webstore.ViewModels
{
    public class RelatedItemViewModel
    {
        List<ProductItem> _productItems = null;
        public List<ProductItem> ProductItems
        {
            get { return _productItems; }
            set { _productItems = value; }
        }

        public string ProductName { get; set; }

        public string CurrencySymbol { get; set; }

        public bool isShowPrices { get; set; }
    }
}