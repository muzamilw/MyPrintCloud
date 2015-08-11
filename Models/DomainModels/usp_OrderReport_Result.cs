using System;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Order Report Stored Procedure Domain Model
    /// </summary>
    public class usp_OrderReport_Result
    {
        public long ItemID { get; set; }
        public long companyid { get; set; }
        public string Title { get; set; }
        public int Qty1 { get; set; }
        public Nullable<int> Qty2 { get; set; }
        public Nullable<int> Qty3 { get; set; }
        public Nullable<double> Qty1NetTotal { get; set; }
        public Nullable<double> Qty2NetTotal { get; set; }
        public Nullable<double> Qty3NetTotal { get; set; }
        public string ProductCode { get; set; }
        public string JobDescription1 { get; set; }
        public string JobDescription2 { get; set; }
        public string JobDescription3 { get; set; }
        public string JobDescription4 { get; set; }
        public string JobDescription5 { get; set; }
        public string JobDescription6 { get; set; }
        public string JobDescription7 { get; set; }
        public string JobDescriptionTitle1 { get; set; }
        public string JobDescriptionTitle2 { get; set; }
        public string JobDescriptionTitle3 { get; set; }
        public string JobDescriptionTitle4 { get; set; }
        public string JobDescriptionTitle5 { get; set; }
        public string JobDescriptionTitle6 { get; set; }
        public string JobDescriptionTitle7 { get; set; }
        public string JobDescription { get; set; }
        public string Estimate_Name { get; set; }
        public string Order_Code { get; set; }
        public Nullable<double> Estimate_Total { get; set; }
        public string FootNotes { get; set; }
        public string HeadNotes { get; set; }
        public Nullable<System.DateTime> Order_Date { get; set; }
        public string Greeting { get; set; }
        public string CustomerPO { get; set; }
        public string AddressName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string URL { get; set; }
        public string Tel1 { get; set; }
        public string AccountNumber { get; set; }
        public string PostCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerURL { get; set; }
        public long EstimateID { get; set; }
        public string ProductName { get; set; }
        public string ContactName { get; set; }
        public string ReportTitle { get; set; }
        public string ReportBanner { get; set; }
        public string Greetings { get; set; }
        public string FullProductName { get; set; }
        public string StockName { get; set; }
        public string AttachmentsList { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public string paymentType { get; set; }
        public string paymentRefNo { get; set; }
        public Nullable<double> TaxLabel { get; set; }
        public string BAddressName { get; set; }
        public string BPostCode { get; set; }
        public string BCountry { get; set; }
        public string BAddress1 { get; set; }
        public string BAddress2 { get; set; }
        public string BCity { get; set; }
        public string BState { get; set; }
        public Nullable<double> Qty1Tax1Value { get; set; }
        public string CurrencySymbol { get; set; }
        public string EstimateCodeLabel { get; set; }
        public string Estimate_Code { get; set; }
        public string UserNotes { get; set; }
    }
}
