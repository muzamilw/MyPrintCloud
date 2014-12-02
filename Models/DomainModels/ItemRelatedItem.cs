using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class ItemRelatedItem
    {
        public Nullable<long> ItemId { get; set; }
        public int Id { get; set; }
        public Nullable<long> RelatedItemId { get; set; }

        public virtual Item Item { get; set; }
        public virtual Item Item1 { get; set; }
    }
}
