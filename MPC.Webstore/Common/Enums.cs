using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.Webstore.Common
{
    public enum StoreMode : int
    {
        Retail = 1,
        Corp = 2,
        Broker = 3,
        NotSet = 99

    }
}