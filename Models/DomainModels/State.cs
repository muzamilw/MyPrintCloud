using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class State
    {
        public long StateId { get; set; }
        public long CountryId { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }

        public virtual ICollection<ItemStateTax> ItemStateTaxes { get; set; }
        public virtual ICollection<Organisation> Organisations { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
