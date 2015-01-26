using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class CompanyBanner
    {
        public long CompanyBannerId { get; set; }
        public int? PageId { get; set; }
        public string ImageURL { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string ItemURL { get; set; }
        public string ButtonURL { get; set; }
        public bool isActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? ModifyId { get; set; }
        public DateTime? ModifyDate { get; set; }
        public long? CompanySetId { get; set; }

        public virtual CompanyBannerSet CompanyBannerSet { get; set; }

    }
}
