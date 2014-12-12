namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Company Banner Mis Model
    /// </summary>
    public class CompanyBanner
    {
        /// <summary>
        /// Company Banner ID
        /// </summary>
        public long CompanyBannerId { get; set; }

        /// <summary>
        /// Page ID
        /// </summary>
        public int? PageId { get; set; }

        /// <summary>
        /// Image URL
        /// </summary>
        public string ImageURL { get; set; }

        /// <summary>
        /// Heading
        /// </summary>
        public string Heading { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Item URL
        /// </summary>
        public string ItemURL { get; set; }

        /// <summary>
        /// Button URL
        /// </summary>
        public string ButtonURL { get; set; }


        /// <summary>
        /// Company Set Id
        /// </summary>
        public long? CompanySetId { get; set; }

    }
}