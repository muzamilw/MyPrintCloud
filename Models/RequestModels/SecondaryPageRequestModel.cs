using System;

namespace MPC.Models.RequestModels
{
    /// <summary>
    /// Secondary Page Request Model
    /// </summary>
    public class SecondaryPageRequestModel : GetPagedListRequest
    {
        /// <summary>
        /// Company Id
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// Is User Defined Pages
        /// </summary>
        public Boolean IsUserDefined { get; set; }
    }
}
