﻿using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class StockCategory
    {
        public long CategoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short Fixed { get; set; }
        public short ItemWeight { get; set; }
        public short ItemColour { get; set; }
        public short ItemSizeCustom { get; set; }
        public short ItemPaperSize { get; set; }
        public short ItemCoatedType { get; set; }
        public short? ItemCoated { get; set; }
        public short ItemExposure { get; set; }
        public short ItemCharge { get; set; }
        public short RecLock { get; set; }
        public int? TaxId { get; set; }
        public short? Flag1 { get; set; }
        public short? Flag2 { get; set; }
        public short? Flag3 { get; set; }
        public short? Flag4 { get; set; }
        public long OrganistionId { get; set; }
        public List<StockSubCategory> StockSubCategories { get; set; }
    }
}