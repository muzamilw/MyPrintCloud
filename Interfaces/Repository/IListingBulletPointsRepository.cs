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
        void UpdateBulletPoints(List<ListingBulletPoint> BulletPoints);
        void AddBulletPoint(ListPointsModel model, long listingId);
    }
}
