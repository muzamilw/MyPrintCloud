using MPC.Models.Common;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Stock Item For Lit View
    /// </summary>
    public class StockItemForListView
    {
        /// <summary>
        /// Stock Item ID
        /// </summary>
        public long StockItemId { get; set; }

        /// <summary>
        /// Item Name
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Item Weight
        /// </summary>
        public int? ItemWeight { get; set; }

        /// <summary>
        /// Item Description
        /// </summary>
        public string ItemDescription { get; set; }

        /// <summary>
        /// Weight Unit Name
        /// </summary>
        public string WeightUnitName { get; set; }

        /// <summary>
        /// Per Qty Qty(/1000)
        /// </summary>
        public double? PerQtyQty { get; set; }

        /// <summary>
        /// Package Quantity
        /// </summary>
        public double? PackageQty { get; set; }

        /// <summary>
        /// Item Colour(Code)
        /// </summary>
        public string FlagColor { get; set; }

        /// <summary>
        /// Category Name
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Sub Category Name
        /// </summary>
        public string SubCategoryName { get; set; }

        /// <summary>
        /// Full Category Name(Category and Sub Category Name)
        /// </summary>
        public string FullCategoryName { get; set; }

        /// <summary>
        /// Supplier Company Name
        /// </summary>
        public string SupplierCompanyName { get; set; }

        /// <summary>
        /// Per Qty Type
        /// </summary>
        public int? PerQtyType { get; set; }

        /// <summary>
        /// Organisation region
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// In Stock
        /// </summary>
        public double? InStock { get; set; }

        /// <summary>
        /// Allocated
        /// </summary>
        public double? Allocated { get; set; }

        /// <summary>
        /// Pack Cost Price
        /// </summary>
        public double? PackCostPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PerQtyWithUnitName
        {
            get { return PerQtyQty + " / " + PerQtyUnitName; }
        }
        /// <summary>
        /// Per Qty Unit Name
        /// </summary>
        public string PerQtyUnitName
        {
            get
            {
                if (PerQtyType == null)
                {
                    return string.Empty;
                }
                switch ((PerQtyTypeUnits)PerQtyType)
                {
                    case PerQtyTypeUnits.Sheet:
                        return "Sheets";
                        break;
                    case PerQtyTypeUnits.Items:
                        return "Items";
                        break;
                    case PerQtyTypeUnits.SqFeet:
                        return "Sq Feet";
                        break;
                    case PerQtyTypeUnits.SqMeter:
                        return "Sq Meter";
                        break;
                    case PerQtyTypeUnits.Ton:
                        return "Ton";
                        break;
                    case PerQtyTypeUnits.lbs:
                        return "lbs";
                        break;

                }
                return string.Empty;
            }
        }
    }
}