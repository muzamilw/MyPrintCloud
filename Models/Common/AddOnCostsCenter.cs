using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class AddOnCostsCenter
    {
        private int _productAddOnID;

        public int ProductAddOnID
        {
            get { return _productAddOnID; }
            set { _productAddOnID = value; }
        }
        private int? _itemID;

        public int? ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }
        private int? _costCenterID;

        public int? CostCenterID
        {
            get { return _costCenterID; }
            set { _costCenterID = value; }
        }
        private double? _discountRate;

        public double? DiscountRate
        {
            get { return _discountRate; }
            set { _discountRate = value; }
        }
        private string _addOnName;

        public string AddOnName
        {
            get { return _addOnName; }
            set { _addOnName = value; }
        }
        private string _addOnImage;
        public string AddOnImage
        {
            get { return _addOnImage; }
            set { _addOnImage = value; }
        }

        private double? _addOnPrice;

        public double? AddOnPrice
        {
            get { return _addOnPrice; }
            set { _addOnPrice = value; }
        }
        private bool? _isDiscounted;

        public bool? IsDiscounted
        {
            get { return _isDiscounted; }
            set { _isDiscounted = value; }
        }

        public int Type { get; set; }

        private string _WebStoreDesc;
        public string WebStoreDesc { get { return _WebStoreDesc; } set { _WebStoreDesc = value; } }

        public double? SetupCost { get; set; }

        public double? MinimumCost { get; set; }

        public double? PricePerUnitQuantity { get; set; }

        public double ActualPrice { get; set; }

        public double DiscountedPrice { get; set; }

        public double? Qty1NetTotal { get; set; }

        public int? EstimateProductionTime { get; set; }

        public double? ProfitMargin { get; set; }

        public int Priority { get; set; }
    }
}
