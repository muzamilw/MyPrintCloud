using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class AddOnCostsCenter
    {
        private long _productAddOnID;

        public long ProductAddOnID
        {
            get { return _productAddOnID; }
            set { _productAddOnID = value; }
        }
        private long _itemID;

        public long ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }
        private long _costCenterID;

        public long CostCenterID
        {
            get { return _costCenterID; }
            set { _costCenterID = value; }
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


        public int Type { get; set; }

        private string _WebStoreDesc;
        public string WebStoreDesc { get { return _WebStoreDesc; } set { _WebStoreDesc = value; } }

        public double? SetupCost { get; set; }

        public double? MinimumCost { get; set; }

        public double? PricePerUnitQuantity { get; set; }

        public double ActualPrice { get; set; }


        public double? Qty1NetTotal { get; set; }

        public decimal EstimateProductionTime { get; set; }

        public double? ProfitMargin { get; set; }

        public int Priority { get; set; }

        public long ItemStockId { get; set; }

        public string CostCentreDescription { get; set; }

        public string CostCentreJsonData { get; set; }
        public int IsMandatory { get; set; }
        public int QuantitySourceType { get; set; }
        public int TimeSourceType { get; set; }
        public long ItemStockOptionId { get; set; }
    }
}
