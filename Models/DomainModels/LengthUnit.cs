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
        public Nullable<double> MM { get; set; }
        public Nullable<double> CM { get; set; }
        public Nullable<double> Inch { get; set; }
    }
}
