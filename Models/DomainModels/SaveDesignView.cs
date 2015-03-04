using System;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// vw_SaveDesign Domain Model
    /// </summary>
    public class SaveDesignView
    {
        public long ItemID { get; set; }
        public long? AttachmentItemId { get; set; }
        public string AttachmentFileName { get; set; }
        public string AttachmentFolderPath { get; set; }
        public long? EstimateID { get; set; }
        public string ProductName { get; set; }
        public string ProductCategoryName { get; set; }
        public long ProductCategoryID { get; set; }
        public int? ParentCategoryID { get; set; }
        public double MinPrice { get; set; }
        public bool? IsEnabled { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsArchived { get; set; }
        public long? InvoiceID { get; set; }
        public long ContactID { get; set; }
        public long CompanyId { get; set; }
        public short IsCustomer { get; set; }
        public long? RefItemID { get; set; }
        public short StatusID { get; set; }
        public string StatusName { get; set; }
        public bool? IsOrderedItem { get; set; }
        public DateTime? ItemCreationDateTime { get; set; }
        public long? TemplateID { get; set; }
    }
}
