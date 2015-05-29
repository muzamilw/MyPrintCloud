namespace MPC.Models.RequestModels
{
    public class ProgressInquiryToEstimateParams
    {
        /// <summary>
        /// Inquiry Id
        /// </summary>
        public int InquiryId { get; set; }
        /// <summary>
        /// Company Id
        /// </summary>
        public long CompanyId { get; set; }
        /// <summary>
        /// Contact Id
        /// </summary>
        public long ContactId { get; set; }
        /// <summary>
        /// Flag Id
        /// </summary>
        public int FlagId { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
          
    }
}
