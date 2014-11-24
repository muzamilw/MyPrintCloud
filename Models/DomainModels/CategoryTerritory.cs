using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public partial class CategoryTerritory
    {
        public int CategoryTerritoryId { get; set; }
        public Nullable<int> ContactCompanyId { get; set; }
        public Nullable<int> ProductCategoryId { get; set; }
        public Nullable<int> TerritoryId { get; set; }
        public Nullable<long> OrganisationId { get; set; }
    }
}
