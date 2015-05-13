namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Address WebAPi Model
    /// </summary>
    public class AddressDropDown
    {

        public long AddressId { get; set; }
        public long? CompanyId { get; set; }
        public string AddressName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Tel1 { get; set; }
        public bool? IsDefaultAddress { get; set; }
    }
}