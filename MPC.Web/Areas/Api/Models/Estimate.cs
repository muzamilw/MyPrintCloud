using System;
using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Estimate Webapi Model
    /// </summary>
    public class Estimate
    {
        public long EstimateId { get; set; }
        public string EstimateCode { get; set; }
        public string EstimateName { get; set; }
        public int? EnquiryId { get; set; }
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? CreatedBy { get; set; }
        public int SectionFlagId { get; set; }
        public string OrderCode { get; set; }
        public bool? IsEstimate { get; set; }
        public long? ContactId { get; set; }
        public short? StatusId { get; set; }
        public double? EstimateTotal { get; set; }
        public int? EstimateValidUpto { get; set; }
        public string UserNotes { get; set; }
        public Guid? LastUpdatedBy { get; set; }
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
        public int? SourceId { get; set; }
        public int? ProductId { get; set; }
        public bool? IsInPipeLine { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? OrderCreationDateTime { get; set; }
        public DateTime? OrderDeliveryDate { get; set; }
        public DateTime? OrderConfirmationDate { get; set; }
        public short OrderStatus { get; set; }
        public DateTime? OrderCompletionDate { get; set; }
        public Guid? OrderManagerId { get; set; }
        public DateTime? ArtworkByDate { get; set; }
        public DateTime? DataByDate { get; set; }
        public DateTime? TargetPrintDate { get; set; }
        public DateTime? StartDeliveryDate { get; set; }
        public DateTime? PaperByDate { get; set; }
        public DateTime? TargetBindDate { get; set; }
        public DateTime? FinishDeliveryDate { get; set; }
        public int? Classification1Id { get; set; }
        public int? Classification2Id { get; set; }
        public bool? IsOfficialOrder { get; set; }
        public string CustomerPo { get; set; }
        public Guid? OfficialOrderSetBy { get; set; }
        public DateTime? OfficialOrderSetOnDateTime { get; set; }
        public bool? IsCreditApproved { get; set; }
        public double? CreditLimitForJob { get; set; }
        public Guid? CreditLimitSetBy { get; set; }
        public DateTime? CreditLimitSetOnDateTime { get; set; }
        public bool? IsJobAllowedWOCreditCheck { get; set; }
        public Guid? AllowJobWOCreditCheckSetBy { get; set; }
        public DateTime? AllowJobWOCreditCheckSetOnDateTime { get; set; }
        public DateTime? NotesUpdateDateTime { get; set; }
        public int? NotesUpdatedByUserId { get; set; }
        public short? OrderSourceId { get; set; }
        public bool IsRead { get; set; }
        public short? EstimateSentTo { get; set; }
        public bool? EstimateValueChanged { get; set; }
        public bool? NewItemAdded { get; set; }
        public bool? IsDirectSale { get; set; }
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
        public bool? IsEmailSent { get; set; }
        public int? DiscountVoucherId { get; set; }
        public short? ClientStatus { get; set; }
        public long? RefEstimateId { get; set; }
        public string XeroAccessCode { get; set; }
        public int? ItemsCount { get; set; }
        public string Status { get; set; }
        public long? StoreId { get; set; }
        public short IsCustomer { get; set; }
        public int? InvoiceStatus { get; set; }
        public bool IsExtraOrder { get; set; }
        public string ContactName { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
        public IEnumerable<PrePayment> PrePayments { get; set; }
        public IEnumerable<ShippingInformation> ShippingInformations { get; set; }
    }
}