using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.ResponseModels
{
    public class StoresListResponse
    {
        /// <summary>
        /// StoreID
        /// </summary>
        public long StoreID { get; set; }

        /// <summary>
        /// Store Name
        /// </summary>
        public string StoreName { get; set; }
    }
}
