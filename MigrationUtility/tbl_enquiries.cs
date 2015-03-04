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
    
    public partial class tbl_enquiries
    {
        public tbl_enquiries()
        {
            this.tbl_estimates = new HashSet<tbl_estimates>();
        }
    
        public int EnquiryID { get; set; }
        public string EnquiryTitle { get; set; }
        public string EnquiryCode { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        public int ContactCompanyID { get; set; }
        public int ContactID { get; set; }
        public Nullable<System.DateTime> RequiredByDate { get; set; }
        public Nullable<System.DateTime> DeliveryByDate { get; set; }
        public int Status { get; set; }
        public int Origination { get; set; }
        public string PreviousQuoteNO { get; set; }
        public string PreviousOrderNO { get; set; }
        public int QuoteDestinition { get; set; }
        public int SendUsing { get; set; }
        public int SalesPersonID { get; set; }
        public int ProcessID { get; set; }
        public int ArtworkOriginationTypeID { get; set; }
        public int ProofRequiredID { get; set; }
        public int ProductTypeID { get; set; }
        public int ProductCode { get; set; }
        public int FrequencyID { get; set; }
        public string DataFormat { get; set; }
        public string DataAvailable { get; set; }
        public string EnquiryNotes { get; set; }
        public string PrintingNotes { get; set; }
        public string ItemNotes { get; set; }
        public int CoverSide1Colors { get; set; }
        public int CoverSide2Colors { get; set; }
        public double CoverInkCoveragePercentage { get; set; }
        public int CoverSpecialColorsID { get; set; }
        public int TextSide1Colors { get; set; }
        public int TextSide2Colors { get; set; }
        public double TextInkCoveragePercentage { get; set; }
        public int TextSpecialColorsID { get; set; }
        public int OtherSide1Colors { get; set; }
        public int OtherSide2Colors { get; set; }
        public double OtherInkCoveragePercentage { get; set; }
        public int OtherSpecialColorsID { get; set; }
        public short ISCoverPaperSupplied { get; set; }
        public short ISTextPaperSupplied { get; set; }
        public short ISOtherPaperSupplied { get; set; }
        public int CoverPaperTypeID { get; set; }
        public int TextPaperTypeID { get; set; }
        public int OtherPaperTypeID { get; set; }
        public string PaperNotes { get; set; }
        public int Quantity1 { get; set; }
        public int Quantity2 { get; set; }
        public int Quantity3 { get; set; }
        public int FinishingStyleID { get; set; }
        public int CoverStyleID { get; set; }
        public int CoverFinishingID { get; set; }
        public int PackingStyleID { get; set; }
        public int InsertStyleID { get; set; }
        public int NoOfInserts { get; set; }
        public string FinishingNotes { get; set; }
        public int DeliveryAddressID { get; set; }
        public int BillingAddressID { get; set; }
        public string DeclinedNotes { get; set; }
        public Nullable<int> SystemSiteID { get; set; }
        public Nullable<short> SalesPersonRead { get; set; }
        public Nullable<short> EstimatorRead { get; set; }
        public string CompanyName { get; set; }
        public Nullable<int> FlagID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<bool> CreatedByCustomer { get; set; }
        public Nullable<System.DateTime> CreationDateTime { get; set; }
        public Nullable<int> PaperSizeID { get; set; }
        public Nullable<bool> CustomeSize { get; set; }
        public Nullable<int> PaperWeight { get; set; }
        public Nullable<int> CoverSpecialColorSide2ID { get; set; }
        public Nullable<int> TextSpecialColorSide2ID { get; set; }
        public Nullable<int> OtherSpecialColorSide2ID { get; set; }
        public Nullable<int> NCRSet { get; set; }
    
        public virtual ICollection<tbl_estimates> tbl_estimates { get; set; }
    }
}