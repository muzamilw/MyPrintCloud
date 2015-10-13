using System;
namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Get Real EState Products Result Domain Model
    /// </summary>
    public class usp_GetRealEstateProducts_Result
    {
        public string ProductName { get; set; }
        public Nullable<long> CategoryId { get; set; }
        public string ProductCode { get; set; }
        public string ThumbnailPath { get; set; }
        public long ItemId { get; set; }
        public Nullable<int> TemplateType { get; set; }
        public Nullable<int> ProductType { get; set; }
        public Nullable<int> isTemplateDesignMode { get; set; }
        public Nullable<long> TemplateId { get; set; }
    }
}
