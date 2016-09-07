using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.RequestModels
{
    public class SpotColorArchiveRequestModel
    {
        public long SpotColorId { get; set; }
        public long TerritoryId { get; set; }
        public long StoreId { get; set; }
        public bool IsStoreColors { get; set; }
    }
}
