using MPC.Models.DomainModels;

namespace MPC.Interfaces.Repository
{
    public interface IRaveReviewRepository : IBaseRepository<RaveReview, long>
    {
        RaveReview GetRaveReview(long companyId);
    }
}
