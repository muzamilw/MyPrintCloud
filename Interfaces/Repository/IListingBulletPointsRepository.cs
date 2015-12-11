using MPC.Models.DomainModels;
using System;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    using MPC.Webstore.Common;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public interface IListingBulletPointsRepository : IBaseRepository<ListingBulletPoint, long>
    {
        void UpdateBulletPoints(List<ListingBulletPoint> BulletPoints, long ListingId);
        void AddBulletPoint(List<ListingBulletPoint> model, long listingId);
        void AddSingleBulletPoint(ListingBulletPoint BullentPoint);
        void UpdateSingleBulletPoint(ListingBulletPoint BullentPoint);
        List<ListingBulletPoint> GetAllListingBulletPoints(long ListingID);
        void DeleteBulletPoint(long BulletPointId, long ListingId);
    }
}
