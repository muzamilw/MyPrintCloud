using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class InvoicesListModel
    {
        public long InvoiceId { get; set; }
        public string InvoiceName { get; set; }
        public int? InvoiceType { get; set; }
        public string InvoiceCode { get; set; }
        public string CompanyName { get; set; }
        public DateTime ? InvoiceDate { get; set; }
        public double? InvoiceTotal { get; set; }
        public int ItemsCount { get; set; }
        public int? FlagId { get; set; }
        public double GrandTotal { get; set; }
        public int StatusId { get; set; }
        public bool isDirectSale { get; set; }
        public string OrderNo{ get; set; }
        public string Status{ get; set; }
        public string ContactName { get; set; }
    }
}