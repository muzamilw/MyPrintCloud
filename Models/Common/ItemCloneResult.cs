using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class ItemCloneResult
    {
        public long TemporaryCustomerId { get; set; }

        public long OrderId { get; set; }

        public string RedirectUrl { get; set; }

        public long ItemId { get; set; }

    }
}
