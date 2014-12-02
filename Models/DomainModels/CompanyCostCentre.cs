using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class CompanyCostCentre
    {
        public int CompanyCostCenterId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> CostCentreId { get; set; }
        public Nullable<double> BrokerMarkup { get; set; }
        public Nullable<double> ContactMarkup { get; set; }
        public Nullable<bool> isDisplayToUser { get; set; }
        public Nullable<long> OrganisationId { get; set; }
    }
}
