using System;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Estimate Webapi Model
    /// </summary>
    public class EstimateListView
    {
        public long EstimateId { get; set; }
        public string EstimateCode { get; set; }
        public string EstimateName { get; set; }
        public short? StatusId { get; set; }
        public int? EnquiryId { get; set; }
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public int SectionFlagId { get; set; }
        public string OrderCode { get; set; }
        public bool? IsEstimate { get; set; }
        public int? ItemsCount { get; set; }
        public string Status { get; set; }
        public string SectionFlagColor { get; set; }
        public double? EstimateTotal { get; set; }
        public Boolean? IsDirectSale { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}