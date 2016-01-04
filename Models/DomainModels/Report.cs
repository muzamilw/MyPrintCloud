namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Report Domain Model
    /// </summary>
    public class Report
    {
        public int ReportId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string ReportDataSource { get; set; }
        public string NameSpace { get; set; }
        public int IsExternal { get; set; }
        public int IsFixed { get; set; }
        public string ReportTemplate { get; set; }
        public string ReportTemplateOriginal { get; set; }
        public short IsEditable { get; set; }
        public int ParentReportId { get; set; }
        public short IsByReflection { get; set; }
        public int CompanyId { get; set; }
        public short? IsSystemReport { get; set; }
        public int ReportOrder { get; set; }
        public bool? HasSubReport { get; set; }
        public string SubReportTemplate { get; set; }
        public string SubReportDataSource { get; set; }
        public long? OrganisationId { get; set; }
        public string SortOrder { get; set; }
        public virtual ReportCategory ReportCategory { get; set; }
    }
}
