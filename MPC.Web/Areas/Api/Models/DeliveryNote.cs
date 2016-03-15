using System;
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// API Model
    /// </summary>
    public class DeliveryNote
    {
        public int DeliveryNoteId { get; set; }
        public string Code { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public long? CompanyId { get; set; }
        public string OrderReff { get; set; }
        public string Comments { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public short? IsStatus { get; set; }
        public long? ContactId { get; set; }
        public string ContactCompany { get; set; }
        public int? AddressId { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierTelNo { get; set; }
        public string CsNo { get; set; }
        public Guid? RaisedBy { get; set; }
        public int? FlagId { get; set; }
        public string UserNotes { get; set; }
        public long? StoreId { get; set; }
        public short IsCustomer { get; set; }
        public string CompanyName { get; set; }
        public string FlagColor { get; set; }
        public string ContactName { get; set; }
        public long? OrganisationId { get; set; }
        public int? OrderId { get; set; }
        
        public List<DeliveryNoteDetail> DeliveryNoteDetails { get; set; }
    }
}