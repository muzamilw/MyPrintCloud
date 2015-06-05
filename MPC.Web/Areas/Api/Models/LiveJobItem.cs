using System;
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class LiveJobItem
    {
        public long? EstimateId { get; set; }
        public long ItemId { get; set; }
        public string CompanyName { get; set; }
        public DateTime? EstimateDate { get; set; }
        public bool? isDirectSale { get; set; }
        public int? Qty1 { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public long CompanyId { get; set; }
        public short StatusId { get; set; }
        public string StatusName { get; set; }
        public string JobCode { get; set; }
        public Guid? JobManagerId { get; set; }
        public List<ItemAttachmentForLiveJobs> ItemAttachments { get; set; }
    }
}