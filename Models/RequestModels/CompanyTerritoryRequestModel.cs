using MPC.Models.DomainModels;

namespace MPC.Models.RequestModels
{
    public class CompanyTerritoryRequestModel : GetPagedListRequest
    {
        public string SearchFilter { get; set; }
        public long CompanyId { get; set; }

    }
}
