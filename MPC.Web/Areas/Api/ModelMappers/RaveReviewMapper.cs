using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class RaveReviewMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static RaveReview CreateFrom(this MPC.Models.DomainModels.RaveReview source)
        {
            return new RaveReview
            {
                ReviewId = source.ReviewId,
                ReviewBy = source.ReviewBy,
                Review = source.Review,
                ReviewDate = source.ReviewDate,
                isDisplay = source.isDisplay,
                SortOrder = source.SortOrder,
                OrganisationId = source.OrganisationId,
                CompanyId = source.CompanyId
            };
        }
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static MPC.Models.DomainModels.RaveReview CreateFrom(this RaveReview source)
        {
            var raveReview = new MPC.Models.DomainModels.RaveReview
            {
                ReviewId = source.ReviewId,
                ReviewBy = source.ReviewBy,
                Review = source.Review,
                ReviewDate = source.ReviewDate,
                isDisplay = source.isDisplay,
                SortOrder = source.SortOrder,
                OrganisationId = source.OrganisationId,
                CompanyId = source.CompanyId
            };
            
            return raveReview;
        }
    }
}