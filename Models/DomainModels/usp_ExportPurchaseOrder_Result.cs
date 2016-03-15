using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class usp_ExportPurchaseOrder_Result
    {
        public int PurchaseId { get; set; }
        public string Code { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string RefNo { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int? PackQty { get; set; }
        public double? Quantity { get; set; }
        public double? Price { get; set; }
        public double? TaxValue { get; set; }
        public double? TotalPrice { get; set; }
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string Comments { get; set; }
        public string Address { get; set; }
    }
}
