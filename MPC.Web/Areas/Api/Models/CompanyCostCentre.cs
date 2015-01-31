using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class CompanyCostCentre
    {
        public long CompanyCostCenterId { get; set; }
        public long? CompanyId { get; set; }
        public long? CostCentreId { get; set; }
        public double? BrokerMarkup { get; set; }
        public double? ContactMarkup { get; set; }
        public bool? isDisplayToUser { get; set; }
        public long? OrganisationId { get; set; }
    }
}