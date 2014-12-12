namespace MPC.Models.RequestModels
{
    public class AddressRequestModel : GetPagedListRequest
    {
        public string SearchFilter { get; set; }
        public long CompanyId { get; set; }
        public long TerritoryId { get; set; }
    }
}
