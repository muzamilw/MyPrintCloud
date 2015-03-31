using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.ResponseModels
{
   public class LookupMethodListResponse
    {
        public IEnumerable<LookupMethod> LookupMethods { get; set; }

        public string WeightUnit { get; set; }
        public string LengthUnit { get; set; }
        public string CurrencySymbol { get; set; }
    }
}
