using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class AssetItem
    {
        public long AssetItemId { get; set; }
        public Nullable<long> AssetId { get; set; }
        public string FileUrl { get; set; }

        public virtual Asset Asset { get; set; }

        [NotMapped]
        public string AssetFolderName { get; set; }
    }
}
