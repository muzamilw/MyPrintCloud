namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Delivery Note Detail API Model
    /// </summary>
    public class DeliveryNoteDetail
    {
        public int DeliveryDetailid { get; set; }
        public string Description { get; set; }
        public long? ItemId { get; set; }
        public int? ItemQty { get; set; }
        public double? GrossItemTotal { get; set; }
    }
}