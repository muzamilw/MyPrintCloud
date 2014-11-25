using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class CompanyBanner
    {
        public long CompanyBannerId { get; set; }
        public Nullable<int> PageId { get; set; }
        public string ImageURL { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string ItemURL { get; set; }
        public string ButtonURL { get; set; }
        public bool isActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> ModifyId { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<long> CompanySetId { get; set; }

        public virtual CompanyBannerSet CompanyBannerSet { get; set; }
    }
}
