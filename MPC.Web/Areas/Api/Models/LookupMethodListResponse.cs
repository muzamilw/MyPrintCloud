using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class LookupMethodListResponse
    {
        public IEnumerable<LookupMethod>  LookupMethods { get; set; }

        public string WeightUnit { get; set; }
        public string LengthUnit { get; set; }
        public string CurrencySymbol { get; set; }

    }
}