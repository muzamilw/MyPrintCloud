using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class OrderMenuCount
    {
        public int AllOrdersCount { get; set; }
        public int PendingOrders { get; set; }
        public int ConfirmedStarts { get; set; }
        public int InProduction { get; set; }
        public int ReadyForShipping { get; set; }
        public int Invoiced { get; set; }
        public int CancelledOrders { get; set; }
    }
}
