using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class ItemRelatedItem
    {
        public long? ItemId { get; set; }
        public int Id { get; set; }
        public long? RelatedItemId { get; set; }

        public virtual Item Item { get; set; }
        public virtual Item RelatedItem { get; set; }
    }
}
