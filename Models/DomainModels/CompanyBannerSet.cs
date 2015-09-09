using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class CompanyBannerSet
    {
        public long CompanySetId { get; set; }
        public string SetName { get; set; }
        public long? CompanyId { get; set; }
        public long? OrganisationId { get; set; }

        public virtual ICollection<CompanyBanner> CompanyBanners { get; set; }
        public virtual Company Company { get; set; }

        /// <summary>
        /// Company Set Fake Id
        /// </summary>
        [NotMapped]
        public long? FakeId { get; set; }


        /// <summary>
        /// Creates Copy of companyBannerSet
        /// </summary>
        public void Clone(CompanyBannerSet target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemSectionClone_InvalidItemSection, "target");
            }

            target.SetName = SetName;
            target.OrganisationId = OrganisationId;
           
        }

    }
}
