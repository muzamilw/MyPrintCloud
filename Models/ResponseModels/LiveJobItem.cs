using System;
using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Live Job Item
    /// </summary>
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
        public short StatusId { get; set; }
        public string StatusName { get; set; }
        public Guid? JobManagerId { get; set; }
        public string JobCode { get; set; }
        public List<ItemAttachment> ItemAttachments { get; set; }
    }
}
