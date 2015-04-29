using System;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Delivery Report Result Domain Model
    /// </summary>
    public class usp_DeliveryReport_Result
    {
        public string CompanyName { get; set; }
        public string SupplierName { get; set; }
        public long? OrganisationId { get; set; }
        public string CotactFullName { get; set; }
        public string AddressName { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string StateName { get; set; }
        public string PostCode { get; set; }
        public string Tel1 { get; set; }
        public string CountryName { get; set; }
        public string Description { get; set; }
        public int? ItemQty { get; set; }
        public double? GrossItemTotal { get; set; }
        public string Code { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string OrderReff { get; set; }
        public string CustomerOrderReff { get; set; }
        public string CsNo { get; set; }
        public int DeliveryNoteID { get; set; }
        public string DeliveryStatus { get; set; }
        public string ReportBanner { get; set; }
    }
}
