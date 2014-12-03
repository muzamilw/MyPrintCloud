using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class LengthUnit
    {
        public long Id { get; set; }
        public string UnitName { get; set; }
        public double? MM { get; set; }
        public double? CM { get; set; }
        public double? Inch { get; set; }
    }
}
