//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_estimates
    {
        public tbl_estimates()
        {
            this.tbl_items = new HashSet<tbl_items>();
            this.tbl_PrePayments = new HashSet<tbl_PrePayments>();
        }
    
        public long EstimateID { get; set; }
        public string Estimate_Code { get; set; }
        public string Estimate_Name { get; set; }
        public Nullable<int> EnquiryID { get; set; }
        public int ContactCompanyID { get; set; }
        public Nullable<int> ContactID { get; set; }
        public Nullable<short> StatusID { get; set; }
        public Nullable<double> Estimate_Total { get; set; }
        public Nullable<int> Estimate_ValidUpto { get; set; }
        public string UserNotes { get; set; }
        public Nullable<int> LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public System.DateTime CreationTime { get; set; }
        public Nullable<int> Created_by { get; set; }
        public Nullable<int> SalesPersonID { get; set; }
        public string HeadNotes { get; set; }
        public string FootNotes { get; set; }
        public Nullable<System.DateTime> EstimateDate { get; set; }
        public Nullable<System.DateTime> ProjectionDate { get; set; }
        public string Greeting { get; set; }
        public string AccountNumber { get; set; }
        public string OrderNo { get; set; }
        public Nullable<int> SuccessChanceID { get; set; }
        public int LockedBy { get; set; }
        public int AddressID { get; set; }
        public string CompanyName { get; set; }
        public int SectionFlagID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> SourceID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<bool> IsInPipeLine { get; set; }
        public string Order_Code { get; set; }
        public Nullable<System.DateTime> Order_Date { get; set; }
        public Nullable<System.DateTime> Order_CreationDateTime { get; set; }
        public Nullable<System.DateTime> Order_DeliveryDate { get; set; }
        public Nullable<System.DateTime> Order_ConfirmationDate { get; set; }
        public short Order_Status { get; set; }
        public Nullable<System.DateTime> Order_CompletionDate { get; set; }
        public int CompanySiteID { get; set; }
        public Nullable<int> OrderManagerID { get; set; }
        public Nullable<System.DateTime> ArtworkByDate { get; set; }
        public Nullable<System.DateTime> DataByDate { get; set; }
        public Nullable<System.DateTime> TargetPrintDate { get; set; }
        public Nullable<System.DateTime> StartDeliveryDate { get; set; }
        public Nullable<System.DateTime> PaperByDate { get; set; }
        public Nullable<System.DateTime> TargetBindDate { get; set; }
        public Nullable<System.DateTime> FinishDeliveryDate { get; set; }
        public Nullable<int> Classification1ID { get; set; }
        public Nullable<int> Classification2ID { get; set; }
        public Nullable<int> IsOfficialOrder { get; set; }
        public string CustomerPO { get; set; }
        public Nullable<int> OfficialOrderSetBy { get; set; }
        public Nullable<System.DateTime> OfficialOrderSetOnDateTime { get; set; }
        public Nullable<int> IsCreditApproved { get; set; }
        public Nullable<double> CreditLimitForJob { get; set; }
        public Nullable<int> CreditLimitSetBy { get; set; }
        public Nullable<System.DateTime> CreditLimitSetOnDateTime { get; set; }
        public Nullable<int> IsJobAllowedWOCreditCheck { get; set; }
        public Nullable<int> AllowJobWOCreditCheckSetBy { get; set; }
        public Nullable<System.DateTime> AllowJobWOCreditCheckSetOnDateTime { get; set; }
        public Nullable<System.DateTime> NotesUpdateDateTime { get; set; }
        public Nullable<int> NotesUpdatedByUserID { get; set; }
        public Nullable<short> OrderSourceID { get; set; }
        public bool IsRead { get; set; }
        public Nullable<short> EstimateSentTo { get; set; }
        public Nullable<bool> EstimateValueChanged { get; set; }
        public Nullable<bool> NewItemAdded { get; set; }
        public Nullable<bool> isEstimate { get; set; }
        public Nullable<bool> isDirectSale { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public Nullable<int> NominalCode { get; set; }
        public Nullable<int> BillingAddressID { get; set; }
        public Nullable<int> DeliveryCostCenterID { get; set; }
        public Nullable<double> DeliveryCost { get; set; }
        public Nullable<int> DeliveryCompletionTime { get; set; }
        public Nullable<double> VoucherDiscountRate { get; set; }
        public Nullable<int> ReportSignedBy { get; set; }
        public Nullable<int> InvoiceID { get; set; }
        public Nullable<int> OrderReportSignedBy { get; set; }
        public Nullable<System.DateTime> OrderReportLastPrinted { get; set; }
        public Nullable<System.DateTime> EstimateReportLastPrinted { get; set; }
        public Nullable<bool> isEmailSent { get; set; }
        public Nullable<int> DiscountVoucherID { get; set; }
        public Nullable<double> Estimate_TotalBroker { get; set; }
        public string BrokerPO { get; set; }
        public Nullable<int> BrokerID { get; set; }
        public Nullable<short> ClientStatus { get; set; }
        public Nullable<int> ParentID { get; set; }
        public Nullable<int> Version { get; set; }
        public string XeroAccessCode { get; set; }
        public Nullable<long> RefEstimateID { get; set; }
    
        public virtual tbl_company tbl_company { get; set; }
        public virtual tbl_company_sites tbl_company_sites { get; set; }
        public virtual tbl_contactcompanies tbl_contactcompanies { get; set; }
        public virtual tbl_contacts tbl_contacts { get; set; }
        public virtual tbl_enquiries tbl_enquiries { get; set; }
        public virtual tbl_Statuses tbl_Statuses { get; set; }
        public virtual ICollection<tbl_items> tbl_items { get; set; }
        public virtual ICollection<tbl_PrePayments> tbl_PrePayments { get; set; }
    }
}
