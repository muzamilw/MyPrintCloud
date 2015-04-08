namespace MPC.Models.RequestModels
{
    /// <summary>
    /// Company Contact For Calendar Request Model
    /// </summary>
    public class CompanyContactForCalendarRequestModel : GetPagedListRequest
    {
        public string SearchFilter { get; set; }
        public int CustomerType { get; set; }
    }
}
