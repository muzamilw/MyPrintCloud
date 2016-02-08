using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class DeliveryCarrier
    {
        public long CarrierId { get; set; }
        public string CarrierName { get; set; }
        public string Url { get; set; }
        public string ApiKey { get; set; }
        public string ApiPassword { get; set; }

        public string CarrierPhone { get; set; }
        public bool? isEnable { get; set; }
        public long OrganisationId { get; set; }
    }
}