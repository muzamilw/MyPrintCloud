using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class Country
    {
        public long CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }

        public virtual ICollection<ItemStateTax> ItemStateTaxes { get; set; }
        public virtual ICollection<Organisation> Organisations { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
