using System;
using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Purchase Domain Model
    /// </summary>
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public string Code { get; set; }
        public DateTime? date_Purchase { get; set; }
        public long? SupplierId { get; set; }
        public int ContactId { get; set; }
        public string SupplierContactCompany { get; set; }
        public int SupplierContactAddressID { get; set; }
        public double? TotalPrice { get; set; }
        public int? UserID { get; set; }
        public int? JobID { get; set; }
        public string RefNo { get; set; }
        public string Comments { get; set; }
        public string FootNote { get; set; }
        public string UserNotes { get; set; }
        public DateTime? NotesUpdateDateTime { get; set; }
        public int? NotesUpdatedByUserId { get; set; }
        public int? isproduct { get; set; }
        public int? Status { get; set; }
        public int? LockedBy { get; set; }
        public double? TotalTax { get; set; }
        public double? Discount { get; set; }
        public int? discountType { get; set; }
        public double? GrandTotal { get; set; }
        public double? NetTotal { get; set; }
        public Guid? CreatedBy { get; set; }
        public int? LastChangedBy { get; set; }
        public int? FlagID { get; set; }
        public int? SystemSiteId { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsPrinted { get; set; }
        public string XeroAccessCode { get; set; }

        public long? OrganisationId { get; set; }
        public virtual Company Company { get; set; }
        public virtual SectionFlag SectionFlag { get; set; }
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
