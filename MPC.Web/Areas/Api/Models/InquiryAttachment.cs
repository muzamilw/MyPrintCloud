namespace MPC.MIS.Areas.Api.Models
{
    public class InquiryAttachment
    {
        public int AttachmentId { get; set; }
        public string OrignalFileName { get; set; }
        public string AttachmentPath { get; set; }
        public int InquiryId { get; set; }
        public string Extension { get; set; }
    }
}