namespace MPC.WebApi.Models
{
    /// <summary>
    /// Web Api Operation Workplace
    /// </summary>
    public sealed class WebApiOperationWorkplace
    {
        /// <summary>
        /// Workplace name
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// Workplace id
        /// </summary>
        public long OperationWorkplaceId { get; set; }
    }
}