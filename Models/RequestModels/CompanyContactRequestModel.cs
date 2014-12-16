namespace MPC.Models.RequestModels
{
    public class CompanyContactRequestModel : GetPagedListRequest
    {
        public string SearchFilter { get; set; }
        public long CompanyId { get; set; }
    }
}
