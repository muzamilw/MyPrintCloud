using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    public class LengthUnit
    {
        public long Id { get; set; }
        public string UnitName { get; set; }
        public double? MM { get; set; }
        public double? CM { get; set; }
        public double? Inch { get; set; }

        public virtual ICollection<Organisation> Organisations { get; set; } 
    }
}
