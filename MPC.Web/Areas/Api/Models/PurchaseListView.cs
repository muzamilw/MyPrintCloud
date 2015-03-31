﻿using System;

namespace MPC.MIS.Areas.Api.Models
{
    public class PurchaseListView
    {
        public int PurchaseId { get; set; }
        public string Code { get; set; }
        public DateTime? DatePurchase { get; set; }
        public string SupplierName { get; set; }
        public double? TotalPrice { get; set; }

    }
}