using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class ItemImage
    {
        public int ProductImageId { get; set; }
        public Nullable<long> ItemId { get; set; }
        public string ImageTitle { get; set; }
        public string ImageURL { get; set; }
        public string ImageType { get; set; }
        public string ImageName { get; set; }
        public Nullable<System.DateTime> UploadDate { get; set; }

        public virtual Item Item { get; set; }
    }
}
