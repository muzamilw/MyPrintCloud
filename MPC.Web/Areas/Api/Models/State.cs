namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// State WebApi Model
    /// </summary>
    public class State
    {
        public long StateId { get; set; }
        public long CountryId { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
    }
}