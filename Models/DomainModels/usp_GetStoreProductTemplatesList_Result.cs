using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class usp_GetStoreProductTemplatesList_Result
    {
        public long ItemId { get; set; }
        public string ProductName { get; set; }
        public long? Templateid { get; set; }
        public string CategoryName { get; set; }
        public string ParentCategory { get; set; }
        public string TemplatePath { get; set; }
    }
}
