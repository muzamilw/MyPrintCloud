using PagedList;

namespace MPC.Models.RequestModels
{
    /// <summary>
    /// Field Variable Request Model
    /// </summary>
    public class FieldVariableRequestModel : GetPagedListRequest
    {
        public long CompanyId { get; set; }
    }
}
