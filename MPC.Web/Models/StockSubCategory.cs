using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Models
{
    public class StockSubCategory
    {
        public int SubCategoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Fixed { get; set; }
        public int CategoryId { get; set; }
    }
}