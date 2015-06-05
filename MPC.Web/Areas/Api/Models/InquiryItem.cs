namespace MPC.MIS.Areas.Api.Models
{
    public class InquiryItem
    {
        public int InquiryItemId { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public string MarketingSource { get; set; }
        public System.DateTime DeliveryDate { get; set; }
        public int InquiryId { get; set; }
        public int? ProductId { get; set; }

    }
}