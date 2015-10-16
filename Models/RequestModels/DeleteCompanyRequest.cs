namespace MPC.Models.RequestModels
{
    /// <summary>
    /// Delete Company Request
    /// </summary>
    public class DeleteCompanyRequest
    {
        /// <summary>
        /// Company Id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Comment to delete
        /// </summary>
        public string Comment { get; set; }

    }
}
