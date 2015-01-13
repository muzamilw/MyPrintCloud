using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Listing Image Domain Model
    /// </summary>
    public class ListingImage
    {
        public long ListingImageId { get; set; }
        public long? ListingId { get; set; }
        public string ImageURL { get; set; }
        public string ImageType { get; set; }
        public int? ImageOrder { get; set; }
        public DateTime? LastMode { get; set; }
        public string ImageRef { get; set; }
        public string PropertyRef { get; set; }
        public string ClientImageId { get; set; }
    }
}
