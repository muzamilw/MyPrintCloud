namespace MPC.MIS.Areas.Api.Models
{
    public class ImportCompanyContactCsv
    {
        public string FileName { get; set; }
        public string FileBytes { get; set; }
        public long CompanyId { get; set; }
    }
}