namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Shipping Information Domain Model
    /// </summary>
    public class ShippingInformation
    {
        public int ShippingId { get; set; }
        public int? ItemId { get; set; }
        public int? AddressId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public bool? DeliveryNoteRaised { get; set; }
        public System.DateTime DeliveryDate { get; set; }
        public long? EstimateId { get; set; }

        public virtual Estimate Estimate { get; set; }
    }
}
