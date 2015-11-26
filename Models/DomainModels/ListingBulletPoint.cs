using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class ListingBulletPoint
    {
        public long BulletPointId { get; set; }
        public string BulletPoint { get; set; }
        public Nullable<long> ListingId { get; set; }

        public virtual Listing Listing { get; set; }
    }
}
