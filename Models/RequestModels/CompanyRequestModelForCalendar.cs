using PagedList;

namespace MPC.Models.RequestModels
{
    /// <summary>
    /// Company Request Model For Calendar
    /// </summary>
    public class CompanyRequestModelForCalendar : GetPagedListRequest
    {
        public int IsCustomerType { get; set; }
    }
}
