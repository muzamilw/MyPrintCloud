using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class CategoryTerritory
    {
        public long CategoryTerritoryId { get; set; }
        public long? CompanyId { get; set; }
        public long? ProductCategoryId { get; set; }
        public long? TerritoryId { get; set; }
    }
}