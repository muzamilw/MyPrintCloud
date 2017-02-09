namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Shipping Information Domain Model
    /// </summary>
    public class ShippingInformation
    {
        public int ShippingId { get; set; }
        public long? ItemId { get; set; }
        public long? AddressId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public double? DeliveryCost { get; set; }
        public bool? DeliveryNoteRaised { get; set; }
        public System.DateTime DeliveryDate { get; set; }
        public long? EstimateId { get; set; }

        public virtual Estimate Estimate { get; set; }
        public virtual Item Item { get; set; }
        public virtual Address Address { get; set; }
        public long? CarrierId { get; set; }
        public string ConsignmentNumber { get; set; }
    }
}
