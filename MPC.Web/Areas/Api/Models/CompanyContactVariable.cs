namespace MPC.MIS.Areas.Api.Models
{
    public class CompanyContactVariable
    {
        public long ContactVariableId { get; set; }
        public long ContactId { get; set; }
        public long VariableId { get; set; }
        public string Value { get; set; }
        public long Type { get; set; }
        public string Title { get; set; }

    }
}