using System.Collections.Generic;

namespace MPC.Models.RequestModels
{
    public class CustomerType
    {
        public short IsCustomer { get; set; }
    }
    /// <summary>
    /// Company Request Model For Calendar
    /// </summary>
    public class CompanyRequestModelForCalendar : GetPagedListRequest
    {
        public int IsCustomerType { get; set; }

        public List<int> CustomerTypes { get; set; }
        /// <summary>
        /// True if being opened from Order Screen
        /// </summary>
        public bool ForOrder { get; set; }
    }
}
