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
        public Nullable<long> CompanyId { get; set; }
        public Nullable<long> ProductCategoryId { get; set; }
        public Nullable<long> TerritoryId { get; set; }
        public Nullable<long> OrganisationId { get; set; }
    }
}
