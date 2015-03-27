namespace MPC.Models.RequestModels
{
    /// <summary>
    /// Company Request Model For Calendar
    /// </summary>
    public class CompanyRequestModelForCalendar : GetPagedListRequest
    {
        public int IsCustomerType { get; set; }

        /// <summary>
        /// True if being opened from Order Screen
        /// </summary>
        public bool ForOrder { get; set; }
    }
}
