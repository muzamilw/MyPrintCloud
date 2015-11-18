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
        public DateTime? date_Purchase { get; set; }
        public int? SupplierID { get; set; }
        public double? TotalPrice { get; set; }
        public int? UserID { get; set; }
        public string RefNo { get; set; }
        public int ContactID { get; set; }
        public int? isproduct { get; set; }
        public double? TotalTax { get; set; }
        public double? Discount { get; set; }
        public int? discountType { get; set; }
        public double? NetTotal { get; set; }
        public int? ItemID { get; set; }
        public double? quantity { get; set; }
        public double? DetailPrice { get; set; }
        public int? packqty { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public double? PriceIncTax { get; set; }
        public double? TaxSum { get; set; }
        public double? GrandTotal { get; set; }
        public string ProductDetail { get; set; }
        public string ProductCode { get; set; }
        public double? DetailTotalPrice { get; set; }
        public string ReportTitle { get; set; }
        public string ReportBanner { get; set; }
        public double? DetailDiscount { get; set; }
        public double? DetailNetTax { get; set; }
        public int? freeitems { get; set; }
        public string Comments { get; set; }
        public string FootNote { get; set; }
        public string UserNotes { get; set; }
        public int? Status { get; set; }
        public Guid? CreatedBy { get; set; }
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
        public DateTime? FinishDeliveryDate { get; set; }
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
