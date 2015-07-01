using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class Estimate
    {
        public long EstimateId { get; set; }
        public string Estimate_Code { get; set; }
        public string Estimate_Name { get; set; }
        public int? EnquiryId { get; set; }
        public long CompanyId { get; set; }
        public long? ContactId { get; set; }
        public short? StatusId { get; set; }
        public double? Estimate_Total { get; set; }
        public int? Estimate_ValidUpto { get; set; }
        public string UserNotes { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public System.DateTime CreationTime { get; set; }
        public Guid? Created_by { get; set; }
        public Guid? SalesPersonId { get; set; }
        public string HeadNotes { get; set; }
        public string FootNotes { get; set; }
        public DateTime? EstimateDate { get; set; }
        public DateTime? ProjectionDate { get; set; }
        public string Greeting { get; set; }
        public string AccountNumber { get; set; }
        public string OrderNo { get; set; }
        public int? SuccessChanceId { get; set; }
        public int LockedBy { get; set; }
        public int AddressId { get; set; }
        public int SectionFlagId { get; set; }
        public int? SourceId { get; set; }
        public int? ProductId { get; set; }
        public bool? IsInPipeLine { get; set; }
        public string Order_Code { get; set; }
        public DateTime? Order_Date { get; set; }
        public DateTime? Order_CreationDateTime { get; set; }
        public DateTime? Order_DeliveryDate { get; set; }
        public DateTime? Order_ConfirmationDate { get; set; }
        public short Order_Status { get; set; }
        public DateTime? Order_CompletionDate { get; set; }
        public Guid? OrderManagerId { get; set; }
        public DateTime? ArtworkByDate { get; set; }
        public DateTime? DataByDate { get; set; }
        public DateTime? TargetPrintDate { get; set; }
        public DateTime? StartDeliveryDate { get; set; }
        public DateTime? PaperByDate { get; set; }
        public DateTime? TargetBindDate { get; set; }
        public DateTime? FinishDeliveryDate { get; set; }
        public int? Classification1Id { get; set; }
        public int? Classification2ID { get; set; }
        public int? IsOfficialOrder { get; set; }
        public string CustomerPO { get; set; }
        public Guid? OfficialOrderSetBy { get; set; }
        public DateTime? OfficialOrderSetOnDateTime { get; set; }
        public int? IsCreditApproved { get; set; }
        public double? CreditLimitForJob { get; set; }
        public Guid? CreditLimitSetBy { get; set; }
        public DateTime? CreditLimitSetOnDateTime { get; set; }
        public int? IsJobAllowedWOCreditCheck { get; set; }
        public Guid? AllowJobWOCreditCheckSetBy { get; set; }
        public DateTime? AllowJobWOCreditCheckSetOnDateTime { get; set; }
        public DateTime? NotesUpdateDateTime { get; set; }
        public int? NotesUpdatedByUserId { get; set; }
        public short? OrderSourceId { get; set; }
        public bool IsRead { get; set; }
        public short? EstimateSentTo { get; set; }
        public bool? EstimateValueChanged { get; set; }
        public bool? NewItemAdded { get; set; }
        public bool? isEstimate { get; set; }
        public bool? isDirectSale { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int? NominalCode { get; set; }
        public int? BillingAddressId { get; set; }
        public int? DeliveryCostCenterId { get; set; }
        public double? DeliveryCost { get; set; }
        public int? DeliveryCompletionTime { get; set; }
        public double? VoucherDiscountRate { get; set; }
        public Guid? ReportSignedBy { get; set; }
        public int? InvoiceId { get; set; }
        public Guid? OrderReportSignedBy { get; set; }
        public DateTime? OrderReportLastPrinted { get; set; }
        public DateTime? EstimateReportLastPrinted { get; set; }
        public bool? isEmailSent { get; set; }
        public int? DiscountVoucherID { get; set; }
        public short? ClientStatus { get; set; }
        public long? RefEstimateId { get; set; }
        public string XeroAccessCode { get; set; }
        public long? OrganisationId { get; set; }
        [NotMapped]
        public int? InvoiceStatus { get; set; }

        public virtual Company Company { get; set; }
        public virtual CompanyContact CompanyContact { get; set; }
        public virtual Organisation Organisation { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<PrePayment> PrePayments { get; set; }
        public virtual SectionFlag SectionFlag { get; set; }
        public virtual ICollection<ShippingInformation> ShippingInformations { get; set; }

        public void Clone(Estimate target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ProductCategoryItemClone_InvalidItem, "target");
            }

            target.CompanyId = CompanyId;
            target.Estimate_Code = Estimate_Code;
            target.Estimate_Name = Estimate_Name;
            target.EnquiryId = EnquiryId;
            target.SectionFlagId = SectionFlagId;
            target.ContactId = ContactId;
            target.AddressId = AddressId;
            target.isDirectSale = isDirectSale;
            target.IsCreditApproved = IsCreditApproved;
            target.IsOfficialOrder = IsOfficialOrder;
            target.Order_Date = Order_Date;
            target.StartDeliveryDate = StartDeliveryDate;
            target.FinishDeliveryDate = FinishDeliveryDate;
            target.HeadNotes = HeadNotes;
            target.FootNotes = FootNotes;
            target.ArtworkByDate = ArtworkByDate;
            target.DataByDate = DataByDate;
            target.PaperByDate = PaperByDate;
            target.TargetBindDate = TargetBindDate;
            target.XeroAccessCode = XeroAccessCode;
            target.TargetPrintDate = TargetPrintDate;
            target.Order_CreationDateTime = Order_CreationDateTime;
            target.SalesPersonId = SalesPersonId;
            target.SourceId = SourceId;
            target.CreditLimitForJob = CreditLimitForJob;
            target.CreditLimitSetBy = CreditLimitSetBy;
            target.CreditLimitSetOnDateTime = CreditLimitSetOnDateTime;
            target.IsJobAllowedWOCreditCheck = IsJobAllowedWOCreditCheck;
            target.AllowJobWOCreditCheckSetOnDateTime = AllowJobWOCreditCheckSetOnDateTime;
            target.AllowJobWOCreditCheckSetBy = AllowJobWOCreditCheckSetBy;
            target.CustomerPO = CustomerPO;
            target.OfficialOrderSetBy = OfficialOrderSetBy;
            target.OfficialOrderSetOnDateTime = OfficialOrderSetOnDateTime;
            target.OrderReportSignedBy = OrderReportSignedBy;
            target.Estimate_Total = Estimate_Total;
            target.CreationDate = CreationDate;
            target.CreationTime = CreationTime;
            target.RefEstimateId = RefEstimateId;
        }
    }

}
