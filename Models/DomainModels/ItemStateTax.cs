using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class ItemStateTax
    {
        public long ItemStateTaxId { get; set; }
        public Nullable<long> CountryId { get; set; }
        public Nullable<long> StateId { get; set; }
        public Nullable<double> TaxRate { get; set; }
        public Nullable<long> ItemId { get; set; }

        public virtual Item Item { get; set; }
        public virtual ItemStateTax ItemStateTax1 { get; set; }
        public virtual ItemStateTax ItemStateTax2 { get; set; }
    }
}
