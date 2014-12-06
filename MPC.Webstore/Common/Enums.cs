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
    public enum ContactCompanyTypes
    {
      
        TemporaryCustomer = 53,

       
        SalesCustomer = 57
    }

    public enum HashAlgos
    {
        MD5,
        SHA1,
        SHA256,
        SHA384,
        SHA512
    }
}