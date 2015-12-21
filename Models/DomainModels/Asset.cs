using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class Asset
    {
        public long AssetId { get; set; }
        public string AssetName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Keywords { get; set; }
        public Nullable<System.DateTime> CreationDateTime { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string FileType { get; set; }
        public Nullable<long> FolderId { get; set; }
        public Nullable<long> CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Folder Folder { get; set; }
    }
}
