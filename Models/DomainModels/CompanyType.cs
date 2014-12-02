using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class CompanyType
    {
        public long TypeId { get; set; }
        public bool? IsFixed { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}
