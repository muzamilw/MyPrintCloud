using System;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Invoice Report Result Domain Model
    /// </summary>
    public class usp_InvoiceReport_Result
    {
        public Nullable<long> InvoiceID { get; set; }
        public string InvoiceCode { get; set; }
        public string OrderNo { get; set; }
        public Nullable<double> InvoiceTotal { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string AccountNumber { get; set; }
        public string HeadNotes { get; set; }
        public string FootNotes { get; set; }
        public Nullable<double> TaxValue { get; set; }
        public string Name { get; set; }
        public string AddressName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string Email { get; set; }
        public string StatePostCode { get; set; }
        public string URL { get; set; }
        public string PostCode { get; set; }
        public double Quantity { get; set; }
        public double ItemTaxValue { get; set; }
        public string InvoiceDescription { get; set; }
        public Nullable<double> Qty1BaseCharge1 { get; set; }
        public Nullable<double> Qty1Tax1Value { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public Nullable<double> GrandTotal { get; set; }
        public string ProductName { get; set; }
        public string ReportBanner { get; set; }
        public string BannerPath { get; set; }
        public string ReportFootNotes { get; set; }
        public string TaxLabel { get; set; }
        public string CurrencySymbol { get; set; }
        public string OrderCode { get; set; }
        public string Estimate_Code { get; set; }
        public Nullable<System.DateTime> FinishDeliveryDate { get; set; }
        public string BAddressName { get; set; }
        public string BAddress1 { get; set; }
        public string BAddress2 { get; set; }
        public string BCity { get; set; }
        public string BState { get; set; }
        public string BPostCode { get; set; }
        public string BCountry { get; set; }
        public string EstimateCodeLabel { get; set; }
        public string OrderCodeLabel { get; set; }
    }
}
