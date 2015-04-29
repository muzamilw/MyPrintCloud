using System;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Shipping Information Web Model
    /// </summary>
    public class ShippingInformation
    {
        public int ShippingId { get; set; }
        public int? ItemId { get; set; }
        public int? AddressId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public long? EstimateId { get; set; }
        public bool? DeliveryNoteRaised { get; set; }
        public DateTime DeliveryDate { get; set; }

    }
}