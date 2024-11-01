//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_stockitems
    {
        public tbl_stockitems()
        {
            this.tbl_section_costcentre_detail = new HashSet<tbl_section_costcentre_detail>();
            this.tbl_stock_cost_and_price = new HashSet<tbl_stock_cost_and_price>();
            this.tbl_stockitems_colors = new HashSet<tbl_stockitems_colors>();
        }
    
        public int StockItemID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string AlternateName { get; set; }
        public Nullable<int> ItemWeight { get; set; }
        public string ItemColour { get; set; }
        public Nullable<short> ItemSizeCustom { get; set; }
        public Nullable<int> ItemSizeID { get; set; }
        public Nullable<double> ItemSizeHeight { get; set; }
        public Nullable<double> ItemSizeWidth { get; set; }
        public Nullable<int> ItemSizeDim { get; set; }
        public Nullable<int> ItemUnitSize { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<double> CostPrice { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> SubCategoryID { get; set; }
        public Nullable<System.DateTime> LastModifiedDateTime { get; set; }
        public Nullable<int> LastModifiedBy { get; set; }
        public Nullable<int> StockLevel { get; set; }
        public Nullable<double> PackageQty { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<double> ReOrderLevel { get; set; }
        public string StockLocation { get; set; }
        public Nullable<short> ItemCoatedType { get; set; }
        public Nullable<short> ItemExposure { get; set; }
        public Nullable<int> ItemExposureTime { get; set; }
        public Nullable<double> ItemProcessingCharge { get; set; }
        public string ItemType { get; set; }
        public Nullable<System.DateTime> StockCreated { get; set; }
        public Nullable<double> PerQtyRate { get; set; }
        public Nullable<double> PerQtyQty { get; set; }
        public string ItemDescription { get; set; }
        public Nullable<int> LockedBy { get; set; }
        public Nullable<int> ReorderQty { get; set; }
        public Nullable<int> LastOrderQty { get; set; }
        public Nullable<System.DateTime> LastOrderDate { get; set; }
        public Nullable<double> inStock { get; set; }
        public Nullable<double> onOrder { get; set; }
        public Nullable<double> Allocated { get; set; }
        public Nullable<int> TaxID { get; set; }
        public Nullable<double> unitRate { get; set; }
        public Nullable<bool> ItemCoated { get; set; }
        public Nullable<int> ItemSizeSelectedUnit { get; set; }
        public Nullable<int> ItemWeightSelectedUnit { get; set; }
        public Nullable<int> FlagID { get; set; }
        public Nullable<double> InkAbsorption { get; set; }
        public Nullable<int> WashupCounter { get; set; }
        public Nullable<double> InkYield { get; set; }
        public Nullable<int> PaperBasicAreaID { get; set; }
        public Nullable<int> PaperType { get; set; }
        public Nullable<int> PerQtyType { get; set; }
        public Nullable<double> RollWidth { get; set; }
        public Nullable<double> RollLength { get; set; }
        public Nullable<int> RollStandards { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public Nullable<int> InkYieldStandards { get; set; }
        public Nullable<double> PerQtyPrice { get; set; }
        public Nullable<double> PackPrice { get; set; }
        public string Region { get; set; }
        public Nullable<bool> isDisabled { get; set; }
        public Nullable<int> InkStandards { get; set; }
        public string BarCode { get; set; }
        public string Image { get; set; }
        public string XeroAccessCode { get; set; }
    
        public virtual ICollection<tbl_section_costcentre_detail> tbl_section_costcentre_detail { get; set; }
        public virtual ICollection<tbl_stock_cost_and_price> tbl_stock_cost_and_price { get; set; }
        public virtual tbl_stockcategories tbl_stockcategories { get; set; }
        public virtual ICollection<tbl_stockitems_colors> tbl_stockitems_colors { get; set; }
        public virtual tbl_stocksubcategories tbl_stocksubcategories { get; set; }
    }
}
