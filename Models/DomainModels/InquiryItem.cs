namespace MPC.Models.DomainModels
{
    public class InquiryItem
    {
        public int InquiryItemId { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public System.DateTime DeliveryDate { get; set; }
        public int InquiryId { get; set; }
        public int? ProductId { get; set; }

        public virtual Inquiry Inquiry { get; set; }
        public virtual PipeLineProduct PipeLineProduct { get; set; }
    }
}
