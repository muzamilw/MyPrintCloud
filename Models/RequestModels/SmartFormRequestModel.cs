namespace MPC.Models.RequestModels
{
    /// <summary>
    /// Smart Form Request Model
    /// </summary>
    public class SmartFormRequestModel : GetPagedListRequest
    {
        public long CompanyId { get; set; }
    }
}
