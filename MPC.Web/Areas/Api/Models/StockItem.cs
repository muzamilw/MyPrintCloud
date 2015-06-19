using System;
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Stock Item Web Model
    /// </summary>
    public class StockItem
    {
        #region Public Properties

        /// <summary>
        /// Stock Item ID
        /// </summary>
        public long StockItemId { get; set; }

        /// <summary>
        /// Item Code
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// Item Name
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Alternate Name
        /// </summary>
        public string AlternateName { get; set; }

        /// <summary>
        /// Item Weight
        /// </summary>
        public int? ItemWeight { get; set; }

        /// <summary>
        /// Item Colour
        /// </summary>
        public string ItemColour { get; set; }

        /// <summary>
        /// Item Size Custom
        /// </summary>
        public short? ItemSizeCustom { get; set; }

        /// <summary>
        /// Item Size Id
        /// </summary>
        public int? ItemSizeId { get; set; }

        /// <summary>
        /// Item Size Height
        /// </summary>
        public double? ItemSizeHeight { get; set; }

        /// <summary>
        /// Item Size Width
        /// </summary>
        public double? ItemSizeWidth { get; set; }

        /// <summary>
        /// Item Size Dim
        /// </summary>
        public int? ItemSizeDim { get; set; }

        /// <summary>
        /// Item Unit Size
        /// </summary>
        public int? ItemUnitSize { get; set; }

        /// <summary>
        /// Supplier Id
        /// </summary>
        public long? SupplierId { get; set; }

        /// <summary>
        /// SupplierName
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// Cost Price
        /// </summary>
        public double? CostPrice { get; set; }

        /// <summary>
        /// Category Id
        /// </summary>
        public long? CategoryId { get; set; }

        /// <summary>
        /// Sub Category Id
        /// </summary>
        public long? SubCategoryId { get; set; }

        /// <summary>
        /// Last Modified Date Time
        /// </summary>
        public DateTime? LastModifiedDateTime { get; set; }

        /// <summary>
        /// Last Modifie dBy
        /// </summary>
        public int? LastModifiedBy { get; set; }

        /// <summary>
        /// StockLevel
        /// </summary>
        public int? StockLevel { get; set; }

        /// <summary>
        /// Package Quantity
        /// </summary>
        public double? PackageQty { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// Re-Order Level
        /// </summary>
        public double? ReOrderLevel { get; set; }

        /// <summary>
        /// Stock Location
        /// </summary>
        public string StockLocation { get; set; }

        /// <summary>
        /// Item Coated Type
        /// </summary>
        public short? ItemCoatedType { get; set; }

        /// <summary>
        /// Item Exposure
        /// </summary>
        public short? ItemExposure { get; set; }

        /// <summary>
        /// Item Exposure Time
        /// </summary>
        public int? ItemExposureTime { get; set; }

        /// <summary>
        /// Item Processing Charge
        /// </summary>
        public double? ItemProcessingCharge { get; set; }

        /// <summary>
        /// Item Type
        /// </summary>
        public string ItemType { get; set; }

        /// <summary>
        /// Stock Created
        /// </summary>
        public DateTime? StockCreated { get; set; }

        /// <summary>
        /// Per Qty Rate
        /// </summary>
        public double? PerQtyRate { get; set; }

        /// <summary>
        /// Per Qty Qty
        /// </summary>
        public double? PerQtyQty { get; set; }

        /// <summary>
        /// Item Description
        /// </summary>
        public string ItemDescription { get; set; }

        /// <summary>
        /// Locked By
        /// </summary>
        public int? LockedBy { get; set; }

        /// <summary>
        /// Reorder Qty
        /// </summary>
        public int? ReorderQty { get; set; }

        /// <summary>
        /// Last Order Qty
        /// </summary>
        public int? LastOrderQty { get; set; }

        /// <summary>
        /// Last Order Date
        /// </summary>
        public DateTime? LastOrderDate { get; set; }

        /// <summary>
        /// In Stock
        /// </summary>
        public double? inStock { get; set; }

        /// <summary>
        /// On Order
        /// </summary>
        public double? onOrder { get; set; }

        /// <summary>
        /// Allocated
        /// </summary>
        public double? Allocated { get; set; }

        /// <summary>
        /// TaxID
        /// </summary>
        public int? TaxID { get; set; }

        /// <summary>
        /// Unit Rate
        /// </summary>
        public double? unitRate { get; set; }

        /// <summary>
        /// Item Coated
        /// </summary>
        public bool? ItemCoated { get; set; }

        /// <summary>
        /// Item Size Selected Unit
        /// </summary>
        public int? ItemSizeSelectedUnit { get; set; }

        /// <summary>
        /// Item Weight Selected Unit
        /// </summary>
        public int? ItemWeightSelectedUnit { get; set; }

        /// <summary>
        /// Flag ID
        /// </summary>
        public int? FlagID { get; set; }

        /// <summary>
        /// Ink Absorption
        /// </summary>
        public double? InkAbsorption { get; set; }

        /// <summary>
        /// Washup Counter
        /// </summary>
        public int? WashupCounter { get; set; }

        /// <summary>
        /// Ink Yield
        /// </summary>
        public double? InkYield { get; set; }

        /// <summary>
        /// Paper Basic Area Id
        /// </summary>
        public int? PaperBasicAreaId { get; set; }

        /// <summary>
        /// Paper Type
        /// </summary>
        public int? PaperType { get; set; }

        /// <summary>
        /// Per Qty Type
        /// </summary>
        public int? PerQtyType { get; set; }

        /// <summary>
        /// Roll Width
        /// </summary>
        public double? RollWidth { get; set; }

        /// <summary>
        /// Roll Length
        /// </summary>
        public double? RollLength { get; set; }

        /// <summary>
        /// Roll Standards
        /// </summary>
        public int? RollStandards { get; set; }

        /// <summary>
        /// Department Id
        /// </summary>
        public int? DepartmentId { get; set; }

        /// <summary>
        /// Ink Yield Standards
        /// </summary>
        public int? InkYieldStandards { get; set; }

        /// <summary>
        /// Per Qty Price
        /// </summary>
        public double? PerQtyPrice { get; set; }

        /// <summary>
        /// Pack Price
        /// </summary>
        public double? PackPrice { get; set; }

        /// <summary>
        /// Region
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Is Disabled
        /// </summary>
        public bool? isDisabled { get; set; }

        /// <summary>
        /// Ink Standards
        /// </summary>
        public int? InkStandards { get; set; }

        /// <summary>
        /// Bar Code
        /// </summary>
        public string BarCode { get; set; }

        /// <summary>
        /// Image
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Xero Access Code
        /// </summary>
        public string XeroAccessCode { get; set; }

        /// <summary>
        /// Organisation Id
        /// </summary>
        public long? OrganisationId { get; set; }

        /// <summary>
        /// Threshold Level
        /// </summary>
        public int? ThresholdLevel { get; set; }

        /// <summary>
        /// ThresholdProductionQuantity
        /// </summary>
        public int? ThresholdProductionQuantity { get; set; }

        /// <summary>
        /// isAllowBackOrder
        /// </summary>
        public bool? isAllowBackOrder { get; set; }

        public bool? IsImperical { get; set; }

        #endregion

        #region Reference Properties
        /// <summary>
        /// Stock Cost And Prices
        /// </summary>
        public IEnumerable<StockCostAndPrice> StockCostAndPrices { get; set; }

        /// <summary>
        /// Item Stock Update History
        /// </summary>
        public IEnumerable<ItemStockUpdateHistory> ItemStockUpdateHistories { get; set; }
        #endregion

    }
}