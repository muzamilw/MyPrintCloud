﻿using System;
using System.Collections.Generic;
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
        public Nullable<int> EnquiryId { get; set; }
        public long CompanyId { get; set; }
        public Nullable<long> ContactId { get; set; }
        public Nullable<short> StatusId { get; set; }
        public Nullable<double> Estimate_Total { get; set; }
        public Nullable<int> Estimate_ValidUpto { get; set; }
        public string UserNotes { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public System.DateTime CreationTime { get; set; }
        public Guid? Created_by { get; set; }
        public Guid? SalesPersonId { get; set; }
        public string HeadNotes { get; set; }
        public string FootNotes { get; set; }
        public Nullable<System.DateTime> EstimateDate { get; set; }
        public Nullable<System.DateTime> ProjectionDate { get; set; }
        public string Greeting { get; set; }
        public string AccountNumber { get; set; }
        public string OrderNo { get; set; }
        public Nullable<int> SuccessChanceId { get; set; }
        public int LockedBy { get; set; }
        public int AddressId { get; set; }
        public string CompanyName { get; set; }
        public int SectionFlagId { get; set; }
        public Nullable<int> SourceId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<bool> IsInPipeLine { get; set; }
        public string Order_Code { get; set; }
        public Nullable<System.DateTime> Order_Date { get; set; }
        public Nullable<System.DateTime> Order_CreationDateTime { get; set; }
        public Nullable<System.DateTime> Order_DeliveryDate { get; set; }
        public Nullable<System.DateTime> Order_ConfirmationDate { get; set; }
        public short Order_Status { get; set; }
        public Nullable<System.DateTime> Order_CompletionDate { get; set; }
        public Guid? OrderManagerId { get; set; }
        public Nullable<System.DateTime> ArtworkByDate { get; set; }
        public Nullable<System.DateTime> DataByDate { get; set; }
        public Nullable<System.DateTime> TargetPrintDate { get; set; }
        public Nullable<System.DateTime> StartDeliveryDate { get; set; }
        public Nullable<System.DateTime> PaperByDate { get; set; }
        public Nullable<System.DateTime> TargetBindDate { get; set; }
        public Nullable<System.DateTime> FinishDeliveryDate { get; set; }
        public Nullable<int> Classification1Id { get; set; }
        public Nullable<int> Classification2ID { get; set; }
        public Nullable<int> IsOfficialOrder { get; set; }
        public string CustomerPO { get; set; }
        public Guid? OfficialOrderSetBy { get; set; }
        public Nullable<System.DateTime> OfficialOrderSetOnDateTime { get; set; }
        public Nullable<int> IsCreditApproved { get; set; }
        public Nullable<double> CreditLimitForJob { get; set; }
        public Nullable<int> CreditLimitSetBy { get; set; }
        public Nullable<System.DateTime> CreditLimitSetOnDateTime { get; set; }
        public Nullable<int> IsJobAllowedWOCreditCheck { get; set; }
        public Nullable<int> AllowJobWOCreditCheckSetBy { get; set; }
        public Nullable<System.DateTime> AllowJobWOCreditCheckSetOnDateTime { get; set; }
        public Nullable<System.DateTime> NotesUpdateDateTime { get; set; }
        public Nullable<int> NotesUpdatedByUserId { get; set; }
        public Nullable<short> OrderSourceId { get; set; }
        public bool IsRead { get; set; }
        public Nullable<short> EstimateSentTo { get; set; }
        public Nullable<bool> EstimateValueChanged { get; set; }
        public Nullable<bool> NewItemAdded { get; set; }
        public Nullable<bool> isEstimate { get; set; }
        public Nullable<bool> isDirectSale { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public Nullable<int> NominalCode { get; set; }
        public Nullable<int> BillingAddressId { get; set; }
        public Nullable<int> DeliveryCostCenterId { get; set; }
        public Nullable<double> DeliveryCost { get; set; }
        public Nullable<int> DeliveryCompletionTime { get; set; }
        public Nullable<double> VoucherDiscountRate { get; set; }
        public Guid? ReportSignedBy { get; set; }
        public Nullable<int> InvoiceId { get; set; }
        public Nullable<int> OrderReportSignedBy { get; set; }
        public Nullable<System.DateTime> OrderReportLastPrinted { get; set; }
        public Nullable<System.DateTime> EstimateReportLastPrinted { get; set; }
        public Nullable<bool> isEmailSent { get; set; }
        public Nullable<int> DiscountVoucherID { get; set; }
        public Nullable<short> ClientStatus { get; set; }
        public Nullable<long> RefEstimateId { get; set; }
        public string XeroAccessCode { get; set; }
        public Nullable<long> OrganisationId { get; set; }

        public virtual Company Company { get; set; }
        public virtual CompanyContact CompanyContact { get; set; }
        public virtual Organisation Organisation { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<PrePayment> PrePayments { get; set; }
    }
}
