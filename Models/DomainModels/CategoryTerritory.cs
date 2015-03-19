using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public partial class CategoryTerritory
    {
        public long CategoryTerritoryId { get; set; }
        public long? CompanyId { get; set; }
        public long? ProductCategoryId { get; set; }
        public long? TerritoryId { get; set; }
        public long? OrganisationId { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
    }
}
