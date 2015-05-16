namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Company Contact Model For Order
    /// </summary>
    public class CompanyContactDropDownForOrder
    {
        public long ContactId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? IsDefaultContact { get; set; }
    }
}