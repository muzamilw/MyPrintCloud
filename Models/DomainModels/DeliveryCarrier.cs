namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Delivery Carrier Domain Model
    /// </summary>
    public class DeliveryCarrier
    {
        public long CarrierId { get; set; }
        public string CarrierName { get; set; }
        public string Url { get; set; }
        public string ApiKey { get; set; }
        public string ApiPassword { get; set; }
        public bool? isEnable { get; set; }

        public string CarrierPhone { get; set; }
    }
}
