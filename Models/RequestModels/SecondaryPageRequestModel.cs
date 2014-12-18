namespace MPC.Models.RequestModels
{
    /// <summary>
    /// Secondary Page Request Model
    /// </summary>
    public class SecondaryPageRequestModel : GetPagedListRequest
    {
        /// <summary>
        /// Company Id
        /// </summary>
        public long CompanyId { get; set; }
    }
}
