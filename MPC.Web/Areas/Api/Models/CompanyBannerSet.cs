using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Company Banner Set Api Model
    /// </summary>
    public class CompanyBannerSet
    {
        /// <summary>
        /// Company Set Id
        /// </summary>
        public long CompanySetId { get; set; }

        /// <summary>
        /// Set Name
        /// </summary>
        public string SetName { get; set; }

        /// <summary>
        /// Company Banners
        /// </summary>
        public List<CompanyBanner> CompanyBanners { get; set; }
    }
}