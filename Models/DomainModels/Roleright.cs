using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class Roleright
    {
        public int RRId { get; set; }
        public int RoleId { get; set; }
        public int RightId { get; set; }

        public virtual AccessRight AccessRight { get; set; }
        public virtual Role Role { get; set; }
    }
}
