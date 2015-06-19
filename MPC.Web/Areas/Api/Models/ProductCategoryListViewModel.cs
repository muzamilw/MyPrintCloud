using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class ProductCategoryListViewModel
    {
        public long ProductCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ContentType { get; set; }
        public int LockedBy { get; set; }
        public int? ParentCategoryId { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsArchived { get; set; }
    }
}