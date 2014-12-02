using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class Status
    {
        public short StatusId { get; set; }
        public string StatusName { get; set; }
        public string Description { get; set; }
        public Nullable<int> StatusType { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
