using System;

namespace MPC.Models.DomainModels
{

    /// <summary>
    /// Purchase Order Report Domain Model
    /// </summary>
    public class usp_PurchaseOrderReport_Result
    {
        public int PurchaseID { get; set; }
        public string Code { get; set; }
        public Nullable<System.DateTime> date_Purchase { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public Nullable<int> UserID { get; set; }
        public string RefNo { get; set; }
        public int ContactID { get; set; }
        public Nullable<int> isproduct { get; set; }
        public Nullable<double> TotalTax { get; set; }
        public Nullable<double> Discount { get; set; }
        public Nullable<int> discountType { get; set; }
        public Nullable<double> NetTotal { get; set; }
        public int ItemID { get; set; }
        public Nullable<double> quantity { get; set; }
        public Nullable<double> DetailPrice { get; set; }
        public Nullable<int> packqty { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public Nullable<double> PriceIncTax { get; set; }
        public Nullable<double> TaxSum { get; set; }
        public Nullable<double> GrandTotal { get; set; }
        public string ProductDetail { get; set; }
        public string DescriptionOrName { get; set; }
        public string JobDescriptionTitle1 { get; set; }
        public string JobDescriptionTitle2 { get; set; }
        public string JobDescriptionTitle3 { get; set; }
        public string JobDescriptionTitle4 { get; set; }
        public string JobDescriptionTitle5 { get; set; }
        public string JobDescriptionTitle6 { get; set; }
        public string JobDescriptionTitle7 { get; set; }
        public string JobDescription1 { get; set; }
        public string JobDescription2 { get; set; }
        public string JobDescription3 { get; set; }
        public string JobDescription4 { get; set; }
        public string JobDescription5 { get; set; }
        public string JobDescription6 { get; set; }
        public string JobDescription7 { get; set; }
        public string Code1 { get; set; }
        public string ProductCode { get; set; }
        public Nullable<double> DetailTotalPrice { get; set; }
        public string ReportTitle { get; set; }
        public string ReportBanner { get; set; }
        public Nullable<double> DetailDiscount { get; set; }
        public Nullable<double> DetailNetTax { get; set; }
        public Nullable<int> freeitems { get; set; }
        public string Comments { get; set; }
        public string FootNote { get; set; }
        public string UserNotes { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public string Name { get; set; }
        public string HeadNotes { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string FullAddress { get; set; }
        public string SuppContactFirstName { get; set; }
        public string SuppContactMiddleName { get; set; }
        public string SuppContactLastName { get; set; }
        public string FullName { get; set; }
        public string SupplierContactFullName { get; set; }
        public string CurrencySymbol { get; set; }
        public Nullable<System.DateTime> FinishDeliveryDate { get; set; }
        public string dAddressName { get; set; }
        public string DAddress1 { get; set; }
        public string DAddress2 { get; set; }
        public string DCity { get; set; }
        public string DState { get; set; }
        public string DPostCode { get; set; }
        public string DCountry { get; set; }
        public string EstimateUserNotes { get; set; }
        public string OrderCode { get; set; }
        public string CompanyName { get; set; }
        public string ContactFullName { get; set; }
    }
}
