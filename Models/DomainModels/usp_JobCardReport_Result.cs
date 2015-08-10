using System;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Job Card Report Stored Procedure Domain Model
    /// </summary>
    public class usp_JobCardReport_Result
    {
        public long EstimateID { get; set; }
        public long ItemID { get; set; }
        public string UserNotes { get; set; }
        public string ItemCode { get; set; }
        public string FirstName { get; set; }
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
        public string ContactFullName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string CompanyName { get; set; }
        public string Address1 { get; set; }
        public string AddressName { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string URL { get; set; }
        public string Tel1 { get; set; }
        public Nullable<int> Qty1 { get; set; }
        public string ProductName { get; set; }
        public string WebDescription { get; set; }
        public string JobDescription { get; set; }
        public string SectionName { get; set; }
        public Nullable<int> SectionNo { get; set; }
        public string BAddress1 { get; set; }
        public string BAddress2 { get; set; }
        public string BCity { get; set; }
        public string BState { get; set; }
        public string BEmail { get; set; }
        public Nullable<System.DateTime> FinishDeliveryDate { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> StartDeliveryDate { get; set; }
        public string CustomerPO { get; set; }
        public string BAddressName { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> Order_Date { get; set; }
        public string Qty1WorkInstructions { get; set; }
        public string CostCenterName { get; set; }
        public string ItemNotes { get; set; }
        public Nullable<double> Qty1NetTotal { get; set; }
        public Nullable<double> Qty1Tax1Value { get; set; }
        public Nullable<double> GrossTotal { get; set; }
        public string FullProductName { get; set; }
        public string BannerPath { get; set; }
        public string EstimateDescription { get; set; }
        public string ReportBanner { get; set; }
        public string OtherItems { get; set; }
        public string BPostCode { get; set; }
        public string BCountry { get; set; }
        public string AttachmentsList { get; set; }
        public string PressName { get; set; }
        public string StockName { get; set; }
        public string DirectOrderLabel { get; set; }
        public string JobCode { get; set; }
        public string PaymentType { get; set; }
        public string PaymentRefNo { get; set; }
        public string DeliveryMethod { get; set; }
        public string CurrencySymbol { get; set; }
        public string StateTaxLabel { get; set; }
    }
}
