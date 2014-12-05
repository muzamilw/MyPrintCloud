using System;

namespace MPC.MIS.Areas.Api.Models
{
    public class RaveReview
    {
        #region Public

        public long ReviewId { get; set; }
        public string ReviewBy { get; set; }
        public string Review { get; set; }
        public DateTime? ReviewDate { get; set; }
        public bool? isDisplay { get; set; }
        public int? SortOrder { get; set; }
        public long? OrganisationId { get; set; }
        public long? CompanyId { get; set; }

        #endregion


    }
}