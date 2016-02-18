using System;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Shipping Information Web Model
    /// </summary>
    public class ShippingInformation
    {
        public int ShippingId { get; set; }
        public long? ItemId { get; set; }
        public long? AddressId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public long? EstimateId { get; set; }
        public bool? DeliveryNoteRaised { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string ItemName { get; set; }
        public string AddressName { get; set; }
        public long? CarrierId { get; set; }
        public string ConsignmentNumber { get; set; }
        
    }
}