using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class DeliveryNoteDetail
    {
        public int DeliveryDetailid { get; set; }
        public int DeliveryNoteId { get; set; }
        public string Description { get; set; }
        public Nullable<long> ItemId { get; set; }
        public Nullable<int> ItemQty { get; set; }
        public Nullable<double> GrossItemTotal { get; set; }

        public virtual DeliveryNote DeliveryNote { get; set; }
        public virtual Item Item { get; set; }
    }
}
