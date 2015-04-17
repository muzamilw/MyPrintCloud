using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Models.RequestModels
{
    public class UpdateSectionCostCentersRequest
    {
        public ItemSection CurrentSection { get; set; }
        public int PressId { get; set; }
      //  public List<SectionInkCoverage> AllSectionInks { get; set; }
    }
}
