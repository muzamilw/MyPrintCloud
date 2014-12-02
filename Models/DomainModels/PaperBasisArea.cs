using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class PaperBasisArea
    {
        public int PaperBasisAreaId { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public short IsSystem { get; set; }
    }
}
