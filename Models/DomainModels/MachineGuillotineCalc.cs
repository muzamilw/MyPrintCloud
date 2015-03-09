using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class MachineGuillotineCalc
    {
        public long Id { get; set; }
        public long? MethodId { get; set; }
        public long? PaperWeight1 { get; set; }
        public long? PaperThroatQty1 { get; set; }
        public long? PaperWeight2 { get; set; }
        public long? PaperThroatQty2 { get; set; }
        public long? PaperWeight3 { get; set; }
        public long? PaperThroatQty3 { get; set; }
        public long? PaperWeight4 { get; set; }
        public long? PaperThroatQty4 { get; set; }
        public long? PaperWeight5 { get; set; }
        public long? PaperThroatQty5 { get; set; }

        public virtual LookupMethod LookupMethod { get; set; }
    }
}
