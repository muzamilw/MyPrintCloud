﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class ItemStockControl
    {
        public long Id { get; set; }
        public Nullable<long> ItemId { get; set; }
        public string ThumbnailURL { get; set; }
        public string ImageURL { get; set; }
        public string ContentType { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public string Description3 { get; set; }
        public int LockedBy { get; set; }
        public int InStock { get; set; }
        public int Allocated { get; set; }
        public Nullable<int> ThresholdLevel { get; set; }
        public Nullable<int> ThresholdProductionQuantity { get; set; }
        public string Location { get; set; }
        public Nullable<short> FinishedGoodType { get; set; }
        public Nullable<int> FinishedGoodStockItemId { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public string SupplierCode { get; set; }
        public string ProductCode { get; set; }
        public string NominalCode { get; set; }
        public string BarCode { get; set; }
        public string FinishedGoodCode { get; set; }
        public Nullable<int> TaxId { get; set; }
        public Nullable<int> RangeThresholdLevel { get; set; }
        public Nullable<System.DateTime> FromThresholdDate { get; set; }
        public Nullable<System.DateTime> ToThresholdDate { get; set; }
        public double Cost { get; set; }
        public int UnitQuantity { get; set; }
        public int PackQuantity { get; set; }
        public Nullable<int> RangeThresholdLevel1 { get; set; }
        public Nullable<int> RangeThresholdLevel2 { get; set; }
        public Nullable<System.DateTime> FromThresholdDate1 { get; set; }
        public Nullable<System.DateTime> FromThresholdDate2 { get; set; }
        public Nullable<System.DateTime> ToThresholdDate1 { get; set; }
        public Nullable<System.DateTime> ToThresholdDate2 { get; set; }
        public Nullable<int> SystemSiteId { get; set; }
        public string File1URL { get; set; }
        public string File2URL { get; set; }
        public Nullable<bool> IsForWeb { get; set; }
        public Nullable<bool> IsShowStockOnWeb { get; set; }
        public Nullable<bool> IsShowPriceOnWeb { get; set; }
        public Nullable<bool> IsShowFreeStockOnWeb { get; set; }
        public Nullable<bool> IsShowAllocatedStockOnWeb { get; set; }
        public Nullable<bool> IsDisabled { get; set; }
        public Nullable<int> FlagId { get; set; }
        public Nullable<bool> IsRead { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> SiteDepartmentId { get; set; }
        public Nullable<bool> isAllowBackOrder { get; set; }
    }
}
