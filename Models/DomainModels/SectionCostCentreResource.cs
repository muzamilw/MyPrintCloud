using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
     [Serializable]
    public class SectionCostCentreResource
    {
        public int SectionCostCentreResourceId { get; set; }
        public Nullable<int> SectionCostcentreId { get; set; }
        public Guid? ResourceId { get; set; }
        public Nullable<int> ResourceTime { get; set; }
        public Nullable<short> IsScheduleable { get; set; }
        public Nullable<short> IsScheduled { get; set; }

        public virtual SectionCostcentre SectionCostcentre { get; set; }
    }
}
